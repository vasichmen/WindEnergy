using CommonLib.Classes;
using CommonLib.Data.Providers.InternetServices;
using CommonLibLib.Data.Interfaces;
using GMap.NET;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace CommonLibLib.Data.Providers.InternetServices
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
        /// Токен для обращения к сервису
        /// </summary>
        private string Token
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_token) || DateTime.Now > _tokenExpires)
                    _token = getToken();
                return _token;
            }
        }
        private string _token = null;
        private DateTime _tokenExpires;

        /// <summary>
        /// https://developers.arcgis.com/rest/geocode/api-reference/geocoding-reverse-geocode.htm
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public string GetAddress(PointLatLng coordinate)
        {
            //https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode?outSR=4326&returnIntersection=false&location=37.715334892272956%2C55.759359885308086&f=json
            string url = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode?outSR=4326&returnIntersection=false&location={0}%2C{1}&f=json&token={2}";
            url = string.Format(url,
                coordinate.Lng.ToString().Replace(Constants.DecimalSeparator, '.'),
                coordinate.Lat.ToString().Replace(Constants.DecimalSeparator, '.'),
                Token
                );
            JToken ans = SendJsonGetRequest(url, out HttpStatusCode code);

            if (ans == null)
                throw new WindEnergyException("Не удалось получить ответ");

            JToken err = ans["error"];
            if (err != null)
                throw new Exception(err["message"].ToString());

            string adr = ans["address"]["LongLabel"].ToString();
            return adr;

        }

        /// <summary>
        /// получить список адресов по части адреса
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Dictionary<string, PointLatLng> GetAddresses(string query)
        {
            //http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates?
            string url = "http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates?";

            string data = $"f=pjson&address={HttpUtility.UrlEncode(query)}&category=City&maxLocations=30&langCode=ru&preferredLabelValues=matchedCity&token={Token}";


            JToken ans = SendJsonGetRequest(url + data, out HttpStatusCode code);
            JToken err = ans["error"];
            if (err != null)
                throw new Exception(err["message"].ToString());

            Dictionary<string, PointLatLng> res = new Dictionary<string, PointLatLng>();
            var vals = ans["candidates"];
            foreach (JToken addr in vals)
            {
                if (!res.ContainsKey(addr["address"].ToString()))
                {
                    double lng = double.Parse(addr["location"]["x"].ToString().Replace(',', Constants.DecimalSeparator));
                    double lat = double.Parse(addr["location"]["y"].ToString().Replace(',', Constants.DecimalSeparator));
                    res.Add(addr["address"].ToString(), new PointLatLng(lat, lng));
                }
            }

            return res;
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
            string url = "http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates?SingleLine={0}&category=&forStorage=false&f=pjson&token={1}";
            url = string.Format(url, address, Token);
            JToken ans = SendJsonGetRequest(url, out HttpStatusCode code);

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


        private string getToken()
        {
            //https://www.arcgis.com/sharing/rest/oauth2/token/
            string url = "https://www.arcgis.com/sharing/rest/oauth2/token/";

            int expMinutes = 1440; //время жизни токена сутки
            string data = $"client_id={CommonLib.Properties.Resources.ArcgisAppId}&client_secret={CommonLib.Properties.Resources.ArcgisSecret}&grant_type=client_credentials&expiration={expMinutes}";
            JToken ans = SendJsonPostRequest(url, data);
            JToken err = ans["error"];
            if (err != null)
                throw new Exception(err["message"].ToString());

            string token = ans["access_token"].ToString();
            string expires = ans["expires_in"].ToString(); //возвращается в секундах
            _tokenExpires = DateTime.Now + TimeSpan.FromSeconds(int.Parse(expires));
            return token;
        }
    }
}
