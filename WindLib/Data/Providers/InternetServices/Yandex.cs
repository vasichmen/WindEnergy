using CommonLib.Data.Providers.InternetServices;
using CommonLibLib.Data.Interfaces;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Xml;

namespace WindEnergy.WindLib.Data.Providers.InternetServices
{
    /// <summary>
    /// Реализация методов взаимодействия с API Яндекса
    /// </summary>
    public class Yandex : BaseConnection, IGeocoderProvider
    {
        public override TimeSpan MinQueryInterval { get; }
        public override int MaxAttempts { get; }
        public override TimeSpan SessionLifetime { get; }

        public Yandex(string folder, double d = 24 * 7) : base("https://yandex.ru", folder, d) { }

        /// <summary>
        /// получить координаты адреса
        /// </summary>
        /// <param name="address">адрес</param>
        /// <returns></returns>
        public PointLatLng GetCoordinate(string address)
        {

            string url = string.Format("https://geocode-maps.yandex.ru/1.x/?geocode={0}", address);
            XmlDocument xml = SendXmlGetRequest(url);

            XmlNode cord = xml.GetElementsByTagName("featureMember")[0];
            if (cord == null)
                throw new ApplicationException("Не найден адрес: " + address);
            XmlNode nd = cord.ChildNodes[0]["Point"];

            string cd = nd["pos"].InnerText;

            string[] ar = cd.Split(' ');
            double lat = double.Parse(ar[1].Replace('.', Constants.DecimalSeparator));
            double lon = double.Parse(ar[0].Replace('.', Constants.DecimalSeparator));

            PointLatLng res = new PointLatLng(lat, lon);
            return res;
        }

        /// <summary>
        /// получить информацию о временной зоне
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public TimeZoneInfo GetTimeZone(PointLatLng coordinate)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// узнать адрес по координате. Если адрес не найден, то null
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public string GetAddress(PointLatLng coordinate)
        {
            string url = string.Format(
               "https://geocode-maps.yandex.ru/1.x/?geocode={0}&results=1",
               coordinate.Lng.ToString().Replace(Constants.DecimalSeparator, '.') + "," + coordinate.Lat.ToString().Replace(Constants.DecimalSeparator, '.'));
            XmlDocument dc = SendXmlGetRequest(url);

            XmlNode found = dc.GetElementsByTagName("found")[0];
            if (found.InnerText == "0")
                throw new ApplicationException("Яндекс не нашел ни одного объекта");

            XmlNode n001 = dc["ymaps"];
            XmlNode n01 = n001["GeoObjectCollection"];
            XmlNode n1 = n01["featureMember"];
            XmlNode n2 = n1["GeoObject"];
            XmlNode n3 = n2["metaDataProperty"];
            XmlNode n4 = n3["GeocoderMetaData"];
            XmlNode n5 = n4["text"];
            return n5.InnerText;

        }


        /// <summary>
        /// получить координаты, подходящие под запрос. Адреса будут добавлены в словарь 
        /// </summary>
        /// <param name="query">часть адреса</param>
        /// <returns></returns>
        public Dictionary<string, PointLatLng> GetAddresses(string query)
        {
            string url = string.Format("https://geocode-maps.yandex.ru/1.x/?geocode={0}", query);
            XmlDocument dc = SendXmlGetRequest(url);

            XmlNode root = dc["ymaps"];
            XmlNode collection = root["GeoObjectCollection"];

            XmlNodeList features = collection.ChildNodes;
            Dictionary<string, PointLatLng> res = new Dictionary<string, PointLatLng>();
            foreach (XmlNode feature in features)
            {
                if (feature.LocalName == "metaDataProperty")
                    continue;
                if (feature.LocalName == "featureMember")
                {
                    XmlNode geoobj = feature["GeoObject"];
                    string description;
                    if (geoobj["description"] != null)
                        description = geoobj["description"].InnerText;
                    else
                        description = "";
                    string name = geoobj["name"].InnerText;
                    string title = name + ", " + description;
                    string coords = geoobj["Point"]["pos"].InnerText;
                    string lon = coords.Split(' ')[0];
                    string lat = coords.Split(' ')[1];
                    PointLatLng crd = new PointLatLng(double.Parse(lat.Replace('.', Constants.DecimalSeparator)), double.Parse(lon.Replace('.', Constants.DecimalSeparator)));
                    if (!res.ContainsKey(title))
                        res.Add(title, crd);
                }
            }
            return res;
        }
    }
}
