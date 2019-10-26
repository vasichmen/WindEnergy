using GMap.NET;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Limits;
using Fizzler.Systems.HtmlAgilityPack;
using WindEnergy.Lib.Data.Interfaces;

namespace WindEnergy.Lib.Data.Providers.InternetServices
{
    /// <summary>
    /// работа с сайтом http://energywind.ru
    /// </summary>
    public class Energywind : BaseConnection
    {
        #region Структуры

        /// <summary>
        /// информация о регионе
        /// </summary>
        public class RegionInfo
        {
            public string Name { get; set; }
            public string Link { get; set; }
        }

        /// <summary>
        /// сырое ограничение с сайта
        /// </summary>
        private class BaseLimit
        {
            public string Name { get; set; }
            public string Link { get; set; }
            public double MaxSpeed { get; set; }
        }

        #endregion



        public override TimeSpan MinQueryInterval { get { return TimeSpan.FromSeconds(0.2); } }
        public override int MaxAttempts { get { return 5; } }
        public override TimeSpan SessionLifetime { get { return TimeSpan.FromMinutes(30); } }

        public Energywind(string cacheDir, double duration = 7 * 24) : base("http://energywind.ru", cacheDir, duration) { }

        /// <summary>
        /// получить список регионов на странице
        /// </summary>
        /// <param name="baseLink">ссылка на страницу</param>
        /// <returns></returns>
        public List<RegionInfo> GetRegions(string baseLink)
        {
            HtmlDocument page = SendHtmlGetRequest(baseLink, out _);

            //поиск строк с регионами
            //выбрать все ссылки, родители которых - элементы td
            var a_list = page.DocumentNode.QuerySelectorAll("td a");
            List<RegionInfo> res = new List<RegionInfo>();
            Uri ur = new Uri(baseLink);
            foreach (var a in a_list)
            {
                //получаем ссылку из атрибутов и название,
                string host = ur.AbsoluteUri.Replace(ur.AbsolutePath, ""); //только протокол и хост
                string link = a.Attributes["href"].Value;
                Uri lnk = new Uri(new Uri(host), link);
                string linkf = lnk.AbsoluteUri;
                string name = a.InnerText;
                res.Add(new RegionInfo() { Name = name, Link = linkf });
            }
            return res;
        }

        /// <summary>
        /// получить ограничения скоростей с заданного региона
        /// </summary>
        /// <param name="region">информация о регионе</param>
        /// <param name="geocoder">геокодер для получения координат</param>
        /// <param name="checkStop">функция проверки отмены</param>
        /// <param name="act">действие при изменении процента выполнения</param>
        /// <param name="current_region">номер текущего региона (передавать в  act)</param>
        /// <param name="total_regions">всего регионов (передавать в  act)</param>
        /// <returns></returns>
        public List<ManualLimits> GetLimits(RegionInfo region, IGeocoderProvider geocoder, Func<bool> checkStop = null, Action<int, int, string, int, int> act = null, int current_region = -1, int total_regions = -1)
        {

            List<BaseLimit> bases = getBasesLimits(region);
            List<ManualLimits> res = new List<ManualLimits>();
            int counter = 0;
            foreach (BaseLimit lim in bases)
            {
                try
                {
                    if (checkStop.Invoke())
                        return res;
                    if (act != null)
                        act.Invoke(current_region,total_regions,  region.Name, counter, bases.Count);

                    Diapason<double> speeds = new Diapason<double>(0, lim.MaxSpeed);
                    ManualLimits limit = new ManualLimits(null, new List<Diapason<double>>() { speeds });
                    limit.Name = lim.Name;
                    limit.Position = geocoder.GetCoordinate(lim.Name);
                    res.Add(limit);
                    counter++;
                }
                catch (Exception ex)
                { continue; }
            }
            return res;
        }

        /// <summary>
        /// парсер страницы с ограничениями в регионе
        /// </summary>
        /// <param name="region">информация о регионе></param>
        /// <returns></returns>
        private List<BaseLimit> getBasesLimits(RegionInfo region)
        {
            HtmlDocument page = SendHtmlGetRequest(region.Link, out _);
            var a_list = page.DocumentNode.QuerySelectorAll("tr");
            List<BaseLimit> res = new List<BaseLimit>();
            if (a_list.Count() < 3) return res;
            for (int i = 2; i < a_list.Count(); i++) //начинаем с третьего элемента(первая строка таблицы)
            {
                try
                {
                    //получаем ссылку из атрибутов и название,
                    HtmlNode tr = a_list.ElementAt(i);
                    var cols = tr.QuerySelectorAll("td");
                    if (cols.Count() != 7) continue;
                    string name = cols.ElementAt(0).InnerText;
                    string limit = cols.ElementAt(6).InnerText;
                    double max = double.Parse(limit);
                    res.Add(new BaseLimit() { Link = region.Link, MaxSpeed = max, Name = name });
                }
                catch (Exception ex)
                { continue; }
            }
            return res;
        }
    }
}
