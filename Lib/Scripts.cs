using GMap.NET;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data;
using WindEnergy.Lib.Data.Interfaces;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.Lib.Data.Providers.DB;
using WindEnergy.Lib.Data.Providers.InternetServices;
using WindEnergy.Lib.Operations.Limits;

namespace WindEnergy.Lib
{
    /// <summary>
    /// вспомогательные скрипты (обработка данных, загрузка информации с сайтов)
    /// </summary>
    public static class Scripts
    {
        #region не используются

        #region обновление БД АМС

        /// <summary>
        /// Получает координаты и ID  метеостанций из созраненного листа Список метеостанций по ФО БД АМС
        /// </summary>
        /// <param name="fromFile">сохраненный лист из БД АМС</param>
        /// <param name="destFile">результирующая БД, файл будет перезаписан</param>
        public static void ConvertMSIDFromFile(string fromFile, string destFile)
        {
            using (StreamReader sr = new StreamReader(fromFile))
            {
                using (StreamWriter sw = new StreamWriter(destFile, false, Encoding.UTF8))
                {
                    List<int> ids = new List<int>();
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] arr = line.Split(';');
                        int id = int.Parse(arr[5]);
                        if (!ids.Contains(id))
                        {
                            //ID;Name;Lat;Lon
                            string name = arr[6];
                            string lat = arr[7];
                            string lon = arr[8];
                            string lineW = $"{id};{name};{lat};{lon}";
                            sw.WriteLine(lineW);
                            ids.Add(id);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Загружает данные из сохраненного листа БД АМС "Скорости по ФО"
        /// </summary>
        /// <param name="mFile"> лист БД АМС "Скорости по ФО"</param>
        /// <param name="destFile">результирующий файл от метода Scripts.ConvertMSIDFromFile</param>
        public static void ConvertMCoefficientsFromFile(string mFile, string destFile)
        {
            List<string[]> destLines = new List<string[]>();
            using (StreamReader sr = new StreamReader(destFile))
            {
                while (!sr.EndOfStream)
                {
                    destLines.Add(sr.ReadLine().Split(';'));
                }
            }

            Dictionary<int, string[]> mLines = new Dictionary<int, string[]>();
            using (StreamReader sr = new StreamReader(mFile))
            {
                int nextLine = 0; //считаем, что первый заголовок на 0 строке
                int i = 0;
                string line = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    if (i == nextLine)
                    {
                        string[] arr = line.Split(';');
                        int id = int.Parse(arr[0]);

                        //скорости
                        string speedLine = sr.ReadLine();
                        string[] speeds10 = speedLine.Split(';').Skip(14).Take(12).ToArray();

                        //среднее m
                        string averLine = sr.ReadLine();
                        string[] mAver = averLine.Split(';').Skip(28).Take(1).ToArray();

                        sr.ReadLine(); sr.ReadLine();

                        //месячные m
                        string mLine = sr.ReadLine();
                        string[] ms = mLine.Split(';').Skip(14).Take(12).ToArray();

                        string[] res = speeds10.Concat(ms).Concat(mAver).ToArray();
                        mLines.Add(id, res);

                        nextLine += 20; i += 5; //переход на следующий элемент
                    }
                    i++;
                    line = sr.ReadLine();
                }
            }

            using (StreamWriter sw = new StreamWriter(destFile, false, Encoding.UTF8))
            {
                foreach (var ln in destLines)
                {
                    int id = int.Parse(ln[0]);
                    string[] data = mLines[id];
                    string[] resultLine = ln.Concat(data).ToArray();
                    string w = "";
                    foreach (string s in resultLine)
                        w += s + ";";
                    w = w.Trim(';');
                    sw.WriteLine(w);
                }
            }




        }


        #endregion


        /// <summary>
        /// записывает в список метеостанций mts высоты и даты начала наблюдений и сохраняет в файл toFile
        /// </summary>
        /// <param name="mts">список метеостанций</param>
        /// <param name="toFile">файл, в который сохранится список метеостанций после обработки</param>
        /// <param name="alts">источник данных о высотах точек</param>
        /// <param name="action">действие при изменении процента выполнения</param>
        public static void DownloadMeteostationExtInfo(List<RP5MeteostationInfo> mts, string toFile, IGeoInfoProvider alts, Action<int> action = null)
        {
            RP5ru provider = new RP5ru(Vars.Options.CacheFolder + "\\rp5.ru");
            double i = 0;
            foreach (var mt in mts)
            {
                if (Math.IEEERemainder(i++, 50) == 0 && action != null)
                    action.Invoke((int)((i / mts.Count) * 100));
                RP5MeteostationInfo tmp = new RP5MeteostationInfo();
                tmp.Link = mt.Link;
                try
                {
                    provider.GetMeteostationExtInfo(ref tmp);
                    mt.MonitoringFrom = tmp.MonitoringFrom;
                }
                catch (WindEnergyException wex)
                { }
                mt.Altitude = alts.GetElevation(mt.Coordinates);
            }
            RP5MeteostationDatabase.ExportMeteostationList(mts, toFile);
        }

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
        /// преобразование файла из json формата, полученного по ссылке 
        /// http://xn--80afd3balrxz7a.xn--p1ai/maps/interactive/meteostantion/data/json_Meteostantionwithdata.js
        /// в формат файла списка координат метеостанций
        /// </summary>
        public static void ConvertJSONMeteostationCoordinatesList(string fileJSON, string fileCoordList)
        {
            StreamReader sr = new StreamReader(fileJSON);
            string json = sr.ReadToEnd();
            sr.Close();

            List<RP5MeteostationInfo> res = new List<RP5MeteostationInfo>();
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

                res.Add(new RP5MeteostationInfo()
                {
                    Coordinates = new PointLatLng(latd, lond),
                    ID = wmo,
                    Name = name,
                    MeteoSourceType = MeteoSourceType.Meteostation
                });
            }

            //запись в файл
            RP5MeteostationDatabase.ExportMeteostationList(res, fileCoordList);
        }

        #endregion

        #region Используются в окне загрузки данных

        /// <summary>
        /// загружает все метеостанции с расписания погоды и сохраняет их в файл fileOutput в формат списка координат метеостанций
        /// </summary>
        /// <param name="fileOutput"></param>
        public static void LoadAllRP5Meteostations(string fileOutput, Action<int, int, string, int, int, int, int> action = null, Func<bool> checkStop = null)
        {
            RP5ru engine = new RP5ru(Vars.Options.CacheFolder + "\\rp5.ru");
            List<RP5MeteostationInfo> result;
            string lastQ = null;

            //если файл уже есть,то надо продолжить с последнего сочетания
            if (File.Exists(fileOutput))
            {
                result = RP5MeteostationDatabase.LoadMeteostationList(fileOutput); //загрузка списка
                StreamReader sr = new StreamReader(fileOutput);
                while (!sr.EndOfStream)
                {
                    string alstr = sr.ReadLine();
                    if (sr.EndOfStream)
                        lastQ = alstr; //посленее сочетание
                }
                sr.Close();
            }
            else
                result = new List<RP5MeteostationInfo>();

            List<string> alf_rus = new List<string>() { "a", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            List<string> alf = new List<string>() { "f", ",", "d", "u", "l", "t", "`", ";", "p", "b", "q", "r", "k", "v", "y", "j", "g", "h", "c", "n", "e", "a", "[", "w", "x", "i", "o", "]", "s", "m", "'", ".", "z" };
            double total = Math.Pow(alf.Count, 2);
            int c = 0;
            int all = 0;
            string q = "";
            foreach (string f in alf)
            {
                //выход, если требуется
                if (checkStop != null && checkStop.Invoke())
                    break;
                foreach (string s in alf)
                {
                    c++; //сразу переход на следующее сочетание, чтоб не терялось при поиске последнего сочетания

                    //выход, если требуется
                    if (checkStop != null && checkStop.Invoke())
                        break;

                    //переход на следующее сочетание
                    q = f + s;

                    //если надо начинать с указанного сочетания, то ждём его
                    if (lastQ != null)
                    {
                        if (q == lastQ)
                            lastQ = null;
                        else
                            continue;
                    }

                    try
                    {
                        //Сохранение временного результата перед каждым сочетанием
                        RP5MeteostationDatabase.ExportMeteostationList(result, fileOutput);
                        StreamWriter sw1 = new StreamWriter(fileOutput, true, Encoding.Default);
                        sw1.WriteLine(q);
                        sw1.Close();

                        var points = engine.Search(q);
                        int dd = 0;
                        foreach (var point in points) //цикл по всем найденным точкам в поиске
                        {
                            //выход, если требуется
                            if (checkStop != null && checkStop.Invoke())
                                break;
                            try
                            {
                                dd++;
                                List<RP5MeteostationInfo> meteost = engine.GetMeteostationsAtPoint(point, true, true); //получаем архивы для этой точки
                                all += meteost.Count;
                                foreach (var m in meteost)
                                {
                                    try
                                    {
                                        m.Address = point.name;
                                        m.Altitude = Vars.ETOPOdatabase.GetElevation(m.Coordinates);
                                        result.Add(m);
                                    }
                                    catch (Exception) { continue; }
                                }
                            }
                            catch (Exception) { continue; }

                            //обновление прогресса
                            if (action != null)
                            {
                                //perc, all, q,count,pcQ, pc1, pc2
                                string rr = alf_rus[alf.IndexOf(q[0].ToString())] + alf_rus[alf.IndexOf(q[1].ToString())];
                                action.Invoke((int)Math.Ceiling((c / total) * 100), all, rr, result.Count, (int)Math.Ceiling((dd / (double)points.Count) * 100), dd, points.Count);
                            }
                        }
                    }
                    catch (Exception) { continue; }
                }
            }

            //сохранение информации
            RP5MeteostationDatabase.ExportMeteostationList(result, fileOutput);
            StreamWriter sw = new StreamWriter(fileOutput, true, Encoding.Default);
            sw.WriteLine(q);
            sw.Close();
        }

        /// <summary>
        /// загружает все ограничения скоростей по регионам с сайта http://energywind.ru 
        /// </summary>
        /// <param name="fileOutput"></param>
        /// <param name="act"></param>
        /// <param name="checkStop"></param>
        public static void LoadAllEnergywindLimits(string fileOutput, Action<int, int, string, int, int> act, Func<bool> checkStop)
        {
            string countryLink = "http://energywind.ru/recomendacii/karta-rossii";
            Energywind engine = new Energywind(Vars.Options.CacheFolder + "\\energywind");
            IGeocoderProvider geocoder = new Arcgis(Vars.Options.CacheFolder + "\\arcgis");
            List<Energywind.RegionInfo> regions = engine.GetRegions(countryLink);
            List<ManualLimits> result = new List<ManualLimits>();
            int regs_c = 0;
            foreach (Energywind.RegionInfo region in regions)
            {
                if (checkStop.Invoke())
                    break;
                List<ManualLimits> lims = engine.GetLimits(region, geocoder, checkStop, act, regs_c, regions.Count);
                result.AddRange(lims);
                regs_c++;
            }

            //запись в файл
            StreamWriter sw = new StreamWriter(fileOutput, false, Encoding.UTF8);
            sw.WriteLine("название;широта;долгота;минимальная скорость м/с;максимальная скорость м/с");
            foreach (ManualLimits lim in result)
                sw.WriteLine($"{lim.Name};{lim.Position.Lat.ToString().Replace(Vars.DecimalSeparator, '.')};{lim.Position.Lng.ToString().Replace(Vars.DecimalSeparator, '.')};{lim.GetMinimal(MeteorologyParameters.Speed)};{lim.GetMaximal(MeteorologyParameters.Speed)}");
            sw.Close();
        }

        #endregion


    }
}
