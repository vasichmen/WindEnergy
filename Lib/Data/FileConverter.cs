using GMap.NET;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data.Interfaces;
using WindEnergy.Lib.Data.Providers;

namespace WindEnergy.Lib.Data
{
    /// <summary>
    /// преобразования служебных форматов друг в друга, импорт данных итд
    /// </summary>
    public static class FileConverter
    {
        /// <summary>
        /// преобразование из файла с адресами и ограничениями скоростей в файл с адресами и координатами ограничений скоростей ветра
        /// </summary>
        /// <param name="fileAdr"></param>
        /// <param name="fileCoord"></param>
        public static void ConvertFromAdressToCoordinatesLimitsFile(string fileAdr, string fileCoord)
        {
            bool fe = File.Exists(fileCoord);
            StreamReader sr = new StreamReader(fileAdr);
            StreamWriter sw = new StreamWriter(fileCoord, true, Encoding.UTF8);

            IGeocoderProvider coder = new Arcgis(Vars.Options.CacheFolder + "\\arcgis");

            sr.ReadLine(); // пропуск загогловка
            if (!fe)
                sw.WriteLine("название;широта;долгота;минимальная скорость м/с;максимальная скорость м/с"); //заголовок 
            while (!sr.EndOfStream)
            {
                string ln = sr.ReadLine();
                string[] arr = ln.Split(';');
                if (arr.Length != 2)
                    continue;
                string adr = arr[0];
                string limit = arr[1];
                try
                {
                    PointLatLng coordinates = coder.GetCoordinate(adr);
                    if (coordinates.IsEmpty)
                        continue;
                    sw.WriteLine(string.Format("{0};{1};{2};{3};{4}", adr, coordinates.Lat.ToString().Replace(Vars.DecimalSeparator, '.'), coordinates.Lng.ToString().Replace(Vars.DecimalSeparator, '.'), 0, limit));
                }
                catch (Exception)
                { continue; }
            }

            sr.Close();
            sw.Close();
        }

        /// <summary>
        /// экспортирует список метеостанций в указанный файл 
        /// </summary>
        /// <param name="list">список метеостанций</param>
        /// <param name="filename">адрес файла, куда сохраняется списокЫ</param>
        public static void ExportMeteostationList(List<MeteostationInfo> list, string filename)
        {
            //запись в файл
            StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
            sw.WriteLine("wmo_id;Название;Широта;Долгота;Высота, м;Дата начала наблюдений");
            foreach (var ms in list)
                sw.WriteLine(
                    ms.ID + ";" +
                    ms.Name + ";" +
                    ms.Coordinates.Lat.ToString().Replace(Vars.DecimalSeparator, ',') + ";" +
                    ms.Coordinates.Lng.ToString().Replace(Vars.DecimalSeparator, ',') + ";" +
                    ms.Altitude.ToString("0.00").Replace(Vars.DecimalSeparator, ',') + ";" +
                    ms.MonitoringFrom.ToString());
            sw.Close();
        }
        /// <summary>
        /// преобразование файла из json формата, полученного по ссылке 
        /// http://xn--80afd3balrxz7a.xn--p1ai/maps/interactive/meteostantion/data/json_Meteostantionwithdata.js
        /// в формат файла списка координат метеостанций
        /// </summary>
        public static void ConvertJSONMeteostationCoordinatesList(string fileJSON, string fileCoordList)
        {
            StreamReader sr = new StreamReader(fileJSON);
            string json = sr.ReadToEnd();
            sr.Close();

            List<MeteostationInfo> res = new List<MeteostationInfo>();
            JObject obj = JObject.Parse(json);

            //чтение json
            foreach (JObject jt in obj["features"])
            {
                string link = jt["properties"]["link"].ToString();

                int st = link.IndexOf("wmo_id=") + "wmo_id=".Length;
                int end = link.IndexOf("\"", st);
                int len = end - st;
                string wmo = link.Substring(st, len);

                string name;
                st = link.IndexOf("blank\">") + "blank\">".Length;
                end = link.IndexOf("<", st);
                if (end == -1)
                    name = link.Substring(st);
                else
                {
                    len = end - st;
                    name = link.Substring(st, len);
                }

                var coords = jt["geometry"]["coordinates"];
                string lon = coords[0].ToString();
                string lat = coords[1].ToString();
                double latd = double.Parse(lat);
                double lond = double.Parse(lon);

                res.Add(new MeteostationInfo()
                {
                    Coordinates = new PointLatLng(latd, lond),
                    ID = wmo,
                    Name = name,
                    MeteoSourceType = MeteoSourceType.Meteostation
                });
            }

            //запись в файл
            ExportMeteostationList(res, fileCoordList);
        }
    }
}
