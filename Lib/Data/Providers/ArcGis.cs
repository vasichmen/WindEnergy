﻿using GMap.NET;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Data.Interfaces;

namespace WindEnergy.Lib.Data.Providers
{
    /// <summary>
    /// Связь с сервисом ArcGis.com
    /// </summary>
    public class Arcgis : BaseConnection, IGeocoderProvider
    {
        public Arcgis(string folder, double d = 24 * 7) : base("https://arcgis.com", folder, d) { }

        /// <summary>
        /// 
        /// </summary>
        public override TimeSpan MinQueryInterval { get { return TimeSpan.FromMilliseconds(100); } }
        public override int MaxAttempts { get { return 5; } }

        public override TimeSpan SessionLifetime { get { return TimeSpan.FromMinutes(10); } }

        /// <summary>
        /// https://developers.arcgis.com/rest/geocode/api-reference/geocoding-reverse-geocode.htm
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public string GetAddress(PointLatLng coordinate)
        {
            //https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode?outSR=4326&returnIntersection=false&location=37.715334892272956%2C55.759359885308086&f=json
            string url = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode?outSR=4326&returnIntersection=false&location={0}%2C{1}&f=json";
            url = string.Format(url, coordinate.Lng.ToString().Replace(Vars.DecimalSeparator, '.'), coordinate.Lat.ToString().Replace(Vars.DecimalSeparator, '.'));
            JToken ans = SendJsonGetRequest(url);

            JToken err = ans["error"];
            if (err != null)
                throw new Exception(err["message"].ToString());

            string adr = ans["address"]["LongLabel"].ToString();
            return adr;

        }

        /// <summary>
        /// координаты адреса
        /// https://developers.arcgis.com/rest/geocode/api-reference/geocoding-find-address-candidates.htm
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public PointLatLng GetCoordinate(string address)
        {
            //http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates?SingleLine=Москва&category=&forStorage=false&f=pjson
            string url = "http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates?SingleLine={0}&category=&forStorage=false&f=pjson";
            url = string.Format(url, address);
            JToken ans = SendJsonGetRequest(url);

            JToken err = ans["error"];
            if (err != null)
                throw new Exception(err["message"].ToString());

            JToken candidates = ans["candidates"];
            if (candidates == null)
                throw new Exception("Неизвестная ошибка arcgis. Запрос:\r\n" + url);
            if (candidates.Count() != 0)
            {
                double lat, lon;
                lon = candidates[0]["location"]["x"].Value<double>();
                lat = candidates[0]["location"]["y"].Value<double>();

                PointLatLng res = new PointLatLng(lat, lon);
                return res;
            }
            else
                return PointLatLng.Empty;
        }

        public TimeZoneInfo GetTimeZone(PointLatLng coordinate)
        {
            throw new NotImplementedException();
        }
    }
}
