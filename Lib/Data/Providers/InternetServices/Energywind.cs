using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Limits;

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
        private class BaseLimit {
            public string Name { get; set; }
            public string Link { get; set; }
            public double MaxSpeed { get; set; }
        }

        #endregion



        public override TimeSpan MinQueryInterval { get { return TimeSpan.FromSeconds(0.2); } }
        public override int MaxAttempts { get { return 5; } }
        public override TimeSpan SessionLifetime { get { return TimeSpan.FromMinutes(30); } }

        public Energywind( string cacheDir, double duration = 7 * 24) : base("http://energywind.ru", cacheDir, duration) { }

        /// <summary>
        /// получить список регионов на странице
        /// </summary>
        /// <param name="baseLink">ссылка на страницу</param>
        /// <returns></returns>
        public List<RegionInfo> GetRegions(string baseLink)
        {
            //TODO: парсер страницы регионов http://energywind.ru/recomendacii/karta-rossii
            throw new NotImplementedException();
             HtmlDocument page = SendHtmlGetRequest(baseLink, out HttpStatusCode code);
             
             //поиск строк с регионами
             //выбрать все ссылки, родители которых - элементы td
            var a_list = page.QuerySelectorAll("td a");
            List<RegionInfo> res = new List<RegionInfo>();
            foreach(var a in a_list)
            {
             //получаем ссылку из атрибутов и название, 
            link= 
            name = 
             res.Add(new RgionInfo(){Name = name, Link=link});
            }
             return res;
            //
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
        public List<ManualLimits> GetLimits(RegionInfo region, Yandex geocoder, Func<bool> checkStop=null, Action<int, int, string, int, int> act=null, int current_region=-1,int total_regions=-1)
        {

            List<BaseLimit> bases = getBasesLimits(region);
            List<ManualLimits> res = new List<ManualLimits>();
            int counter = 0;
            foreach (BaseLimit lim in bases)
            {
                if (checkStop.Invoke())
                    return res;
                if (act != null)
                    act.Invoke(total_regions, current_region, region.Name, counter, bases.Count);

                //TODO: создание ограничения, получение координат
                


                counter++;
            }
            return res;
        }

        /// <summary>
        /// парсер страницы с ограничениями в регионе
        /// </summary>
        /// <param name="region"информация о регионе></param>
        /// <returns></returns>
        private List<BaseLimit> getBasesLimits(RegionInfo region)
        {
            //TODO: парсер таблицы с ограничениями http://energywind.ru/recomendacii/karta-rossii/severo-zapad/respublika-komi
            throw new NotImplementedException();
        }
    }
}
