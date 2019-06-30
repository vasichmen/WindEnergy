using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GMap.NET;
using WindEnergy.Lib.Data.Interfaces;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.IO;
using System.Windows.Forms;
using WindEnergy.Lib.Data.Providers;

namespace WindEnergy.Lib.Data.Providers
{
    /// <summary>
    /// Работа с API Google
    /// https://developers.google.com/maps/documentation/geocoding/intro?hl=ru
    /// https://developers.google.com/maps/documentation/elevation/intro?hl=ru
    /// https://developers.google.com/maps/documentation/directions/intro?hl=ru
    /// </summary>
   public class Google : BaseConnection, IGeoInfoProvider
    {
        /// <summary>
        /// Создаёт новый объект связи с сервисом с заданной папкой кэша запросов и временем хранения кэша
        /// </summary>
        /// <param name="cacheDirectory">папка с кэшем или null, если не надо использовать кэш</param>
        /// <param name="duration">время хранения кэша в часах. По умолчанию - неделя</param>
        public Google(string cacheDirectory, double duration = 24 * 7) : base("https://google.com", cacheDirectory, duration) { }


        /// <summary>
        ///  минимальное время между запросами
        /// </summary>
        public override TimeSpan MinQueryInterval
        {
            get
            {
                //гугл, если с одинаковой частотой подавать запросы, кидает OVER_QUERY_LIMIT. 
                //для обхода используется рандомная задержка запросов
                //https://developers.google.com/maps/documentation/directions/usage-limits?hl=ru
                return TimeSpan.FromMilliseconds(new Random().Next(500) + 50);
            }
        }

        /// <summary>
        /// максимальное чилос попыток переподключения
        /// </summary>
        public override int MaxAttempts
        {
            get
            {
                return 5;
            }
        }

        /// <summary>
        /// если истина, то это локальный источник данных
        /// </summary>
        public bool isLocal
        {
            get
            {
                return false;
            }
        }

        public override TimeSpan SessionLifetime { get { return TimeSpan.FromMinutes(10); } }



        /// <summary>
        /// узнать высоту в указанном месте в метрах над у. м.
        /// </summary>
        /// <param name="coordinate">координаты точки</param>
        /// <returns></returns>
        public double GetElevation(PointLatLng coordinate)
        {
            //https://maps.googleapis.com/maps/api/elevation/json?locations=39.7391536,-104.9847034&api_key=

            string url = string.Format("https://maps.googleapis.com/maps/api/elevation/xml?locations={0},{1}&api_key={2}",
                coordinate.Lat.ToString().Replace(Vars.DecimalSeparator, '.'),
                coordinate.Lng.ToString().Replace(Vars.DecimalSeparator, '.'),
                "AIzaSyD_HQaeF2zZDgK7V22BJ_cI-iczZQD-ODo");

            XmlDocument xml = SendXmlGetRequest(url);
            XmlNode status = xml.GetElementsByTagName("status")[0];
            if (status.InnerText == "OK")
            {
                string el = xml.GetElementsByTagName("elevation")[0].InnerText;
                double ell = double.Parse(el.Replace('.', Vars.DecimalSeparator));
                return ell;
            }
            else
                throw new ApplicationException("Ошибка при обработке запроса. \r\nGoogle error: " + status.InnerText);
        }

    }
}
