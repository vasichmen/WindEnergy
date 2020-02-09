using CommonLib;
using CommonLib.Data.Providers.InternetServices;
using GMap.NET;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data.Interfaces;

namespace WindEnergy.WindLib.Data.Providers.InternetServices
{
    /// <summary>
    /// взаимодействие с БД NASA https://power.larc.nasa.gov/docs/v1/
    /// </summary>
    public class NASA : BaseConnection, IRangeProvider
    {
        /// <summary>
        /// выбранные поля для загрузки https://power.larc.nasa.gov/docs/v1/#box
        /// </summary>
        Dictionary<MeteorologyParameters, string> parameters = new Dictionary<MeteorologyParameters, string>() {
            {MeteorologyParameters.Speed, "WS10M" },
            //{MeteorologyParameters.Direction, "WD10M" },
            {MeteorologyParameters.Temperature, "T10M" },
            {MeteorologyParameters.Wetness, "RH2M" },
            {MeteorologyParameters.Pressure, "PS" },
        };

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
        /// <param name="onPercentChange">Метод, вызываемый при изменении процента выполнения</param>
        /// <param name="checkStop"></param>
        /// <returns></returns>
        public RawRange GetRange(DateTime fromDate, DateTime toDate, RP5MeteostationInfo point_info, Action<double> onPercentChange = null, Func<bool> checkStop = null)
        {
            PointLatLng coord = point_info.Position;

            string fields = "";
            foreach (string param in parameters.Values)
                fields += param + ",";
            fields = fields.Trim(',');

            //https://power.larc.nasa.gov/cgi-bin/v1/DataAccess.py?request=execute&identifier=SinglePoint&parameters=T2M,PS,ALLSKY_SFC_SW_DWN&startDate=20160301&endDate=20160331&userCommunity=SSE&tempAverage=DAILY&outputList=JSON,ASCII&lat=36&lon=45&user=anonymous
            string url = "https://power.larc.nasa.gov/cgi-bin/v1/DataAccess.py?request=execute&identifier=SinglePoint&parameters={0}&startDate={1}&endDate={2}&userCommunity=SSE&tempAverage=DAILY&outputList=ASCII&lat={3}&lon={4}&user=anonymous";


            url = string.Format(url,
                fields,
                fromDate.ToString("yyyyMMdd"),
                toDate.ToString("yyyyMMdd"),
                coord.Lat.ToString("00.00").Replace(Constants.DecimalSeparator, '.'),
                coord.Lng.ToString("00.00").Replace(Constants.DecimalSeparator, '.'));

            JToken ans = SendJsonGetRequest(url, out HttpStatusCode code, false);

            if (checkStop != null && checkStop.Invoke()) //проверка остановки процесса
                return null;

            if (ans["messages"].HasValues) //если есть ошибки, то выход с ошибкой
            {
                JToken alert = ans["messages"][0];
                string msg = alert["Alert"]["Description"]["Issue"].ToString();
                throw new ApplicationException(msg);
            }

            RawRange res = new RawRange();
            string txt_url = ans["outputs"]["ascii"].ToString();
            string data = SendStringGetRequest(txt_url, false);

            if (checkStop != null && checkStop.Invoke()) //проверка остановки процесса
                return null;

            string dlines = data.Substring(data.IndexOf("-END HEADER-") + "-END HEADER-".Length);
            string[] lines = dlines.Split('\n');
            for (int i = 1; i < lines.Length - 1; i++)
            {
                string line = lines[i].Replace("\r", "");
                line = Regex.Replace(line, @"[ ]+", " ");
                string[] elems = line.Split(' ');
                if (elems.Length != 3 + parameters.Count) //проверка числа параметров (3 для даты, 5 - параметры атмосферы)
                    throw new Exception("Текстовый файл имеет неизвестный формат. Проверьте число параметров");

                try
                {
                    Dictionary<MeteorologyParameters, double> values = new Dictionary<MeteorologyParameters, double>();
                    DateTime dt = DateTime.Parse(elems[0] + "." + elems[1] + "." + elems[2]);
                    int ind = 3;
                    foreach (MeteorologyParameters key in parameters.Keys)
                    {
                        double val = double.Parse(elems[ind].Replace('.', Constants.DecimalSeparator));
                        if (key == MeteorologyParameters.Pressure)
                            val *= Constants.MMHGART_IN_1KPA; //переводим в мм рт. ст.
                        values.Add(key, val);
                        ind++;
                    }
                    RawItem item = new RawItem(dt, values);
                    res.Add(item);
                }
                catch (ArgumentException)
                { continue; }
            }
            res.Position = point_info.Position;
            return res;
        }
    }
}
