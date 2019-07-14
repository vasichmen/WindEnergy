using GMap.NET;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data.Interfaces;

namespace WindEnergy.Lib.Data.Providers.InternetServices
{
    /// <summary>
    /// взаимодействие с БД NASA https://power.larc.nasa.gov/docs/v1/
    /// </summary>
    public class NASA : BaseConnection, IRangeProvider
    {
        /// <summary>
        /// информация о точке, где можно узнать архив погоды
        /// </summary>
        public class PointInfo
        {
            /// <summary>
            /// коодинаты точки
            /// </summary>
            public PointLatLng Position { get; set; }
        }

        public override TimeSpan MinQueryInterval { get { return TimeSpan.FromSeconds(1); } }

        public override int MaxAttempts { get { return 5; } }

        public override TimeSpan SessionLifetime { get { return TimeSpan.FromMinutes(10); } }

        public NASA(string cacheDirectory, double duration = 7 * 24) : base("https://power.larc.nasa.gov", cacheDirectory, duration) { }

        /// <summary>
        /// получить ряд данных за указанный промежуток в заданной точке
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="point_info"></param>
        /// <returns></returns>
        public RawRange GetRange(DateTime fromDate, DateTime toDate, MeteostationInfo point_info, Action<double> onPercentChange = null)
        {
            PointLatLng coord = point_info.Coordinates;

            //выбранные поля для загрузки https://power.larc.nasa.gov/docs/v1/#box
            //скорость на 10м,направление,температура,влажность
            string fields = "WS10M,WD10M,T10M,RH2M";

            //https://power.larc.nasa.gov/cgi-bin/v1/DataAccess.py?request=execute&identifier=SinglePoint&parameters=T2M,PS,ALLSKY_SFC_SW_DWN&startDate=20160301&endDate=20160331&userCommunity=SSE&tempAverage=DAILY&outputList=JSON,ASCII&lat=36&lon=45&user=anonymous
            string url = "https://power.larc.nasa.gov/cgi-bin/v1/DataAccess.py?request=execute&identifier=SinglePoint&parameters={0}&startDate={1}&endDate={2}&userCommunity=SSE&tempAverage=DAILY&outputList=ASCII&lat={3}&lon={4}&user=anonymous";

           
            url = string.Format(url,
                fields,
                fromDate.ToString("yyyyMMdd"),
                toDate.ToString("yyyyMMdd"),
                coord.Lat.ToString("00.00").Replace(Vars.DecimalSeparator, '.'),
                coord.Lng.ToString("00.00").Replace(Vars.DecimalSeparator, '.'));

            JToken ans = SendJsonGetRequest(url, false);

            if (ans["messages"].HasValues) //если есть ошибки, то выход с ошибкой
            {
                JToken alert = ans["messages"][0];
                string msg = alert["Alert"]["Description"]["Issue"].ToString();
                throw new ApplicationException(msg);
            }

            RawRange res = new RawRange();
            string txt_url = ans["outputs"]["ascii"].ToString();
            string data = SendStringGetRequest(txt_url, false);
            string dlines = data.Substring(data.IndexOf("-END HEADER-") + "-END HEADER-".Length);
            string[] lines = dlines.Split('\n');
            for (int i = 1; i < lines.Length - 1; i++)
            {
                string line = lines[i].Replace("\r", "");
                line = Regex.Replace(line, @"[ ]+", " ");
                string[] elems = line.Split(' ');
                if (elems.Length != 7) //проверка числа параметров (3 для даты, 4 - параметры атмосферы)
                    throw new Exception("Текстовый файл имеет неизвестный формат. Проверьте число параметров");

                DateTime dt = DateTime.Parse(elems[0] + "." + elems[1] + "." + elems[2]);
                double WS10M = double.Parse(elems[3].Replace('.', Vars.DecimalSeparator));
                double WD10M = double.Parse(elems[4].Replace('.', Vars.DecimalSeparator));
                double T10M = double.Parse(elems[5].Replace('.', Vars.DecimalSeparator));
                double RH2M = double.Parse(elems[6].Replace('.', Vars.DecimalSeparator));
                res.Add(new RawItem() {
                    Date = dt,
                    Direction = WD10M == -999 ? double.NaN : WD10M,
                    Speed = WS10M == -999 ? double.NaN : WS10M,
                    Temperature = T10M == -999 ? double.NaN : T10M,
                    Wetness = RH2M == -999 ? double.NaN : RH2M });
            }
            res.Position = point_info.Coordinates;
            return res;
        }
    }
}
