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
using WindEnergy.Lib.Data.Providers.ETOPO;

namespace WindEnergy.Lib
{
    /// <summary>
    /// вспомогательные скрипты (обработка данных, загрузка информации с сайтов)
    /// </summary>
    public static class Scripts
    {
        /// <summary>
        /// записывает в список метеостанций mts высоты и даты начала наблюдений и сохраняет в файл toFile
        /// </summary>
        /// <param name="mts">список метеостанций</param>
        /// <param name="toFile">файл, в который сохранится список метеостанций после обработки</param>
        /// <param name="alts">источник данных о высотах точек</param>
        /// <param name="action">действие при изменении процента выполнения</param>
        public static void DownloadMeteostationExtInfo(List<MeteostationInfo> mts, string toFile, IGeoInfoProvider alts, Action<int> action = null)
        {
            RP5ru provider = new RP5ru(Vars.Options.CacheFolder + "\\rp5.ru");
            double i = 0;
            foreach (var mt in mts)
            {
                if (Math.IEEERemainder(i++, 50) == 0 && action != null)
                    action.Invoke((int)((i / mts.Count) * 100));
                MeteostationInfo tmp = new MeteostationInfo();
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
            ExportMeteostationList(mts, toFile);
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
        /// экспортирует список метеостанций в указанный файл 
        /// </summary>
        /// <param name="list">список метеостанций</param>
        /// <param name="filename">адрес файла, куда сохраняется список</param>
        public static void ExportMeteostationList(List<MeteostationInfo> list, string filename)
        {
            //удаление повторов
            List<MeteostationInfo> list_unic = new List<MeteostationInfo>();
            foreach (var m in list)
            {
                //проверка существования ID в массиве
                bool contains = false;
                foreach (var cc in list_unic)
                    if (cc.ID == m.ID)
                    {
                        contains = true;
                        break;
                    }
                //если не существует, то  добавляем
                if (!contains)
                    list_unic.Add(m);
            }
            //запись в файл
            StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
            sw.WriteLine("ВМО ID;CC_Code;Название архива;Адрес;Широта;Долгота;Высота над у. м., м;Дата начала наблюдений");
            foreach (var ms in list_unic)
                sw.WriteLine(
                    ms.ID + ";" +
                    ms.CC_Code + ";" +
                    ms.Name + ";" +
                    ms.Address + ";" +
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

        /// <summary>
        /// загружает все метеостанции с расписания погоды и сохраняет их в файл fileOutput в формат списка координат метеостанций
        /// </summary>
        /// <param name="fileOutput"></param>
        public static void LoadAllRP5Meteostations(string fileOutput, Action<int, int, string, int, int, int, int> action = null, Func<bool> checkStop = null)
        {
            RP5ru engine = new RP5ru(Vars.Options.CacheFolder+"\\rp5.ru");
            List<MeteostationInfo> result;
            string lastQ = null;

            //если файл уже есть,то надо продолжить с последнего сочетания
            if (File.Exists(fileOutput))
            {
                result = LocalFileSystem.LoadMeteostationList(fileOutput); //загрузка списка
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
                result = new List<MeteostationInfo>();

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
                        ExportMeteostationList(result, fileOutput);
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
                                List<MeteostationInfo> meteost = engine.GetMeteostationsAtPoint(point, true,true); //получаем архивы для этой точки
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
            ExportMeteostationList(result, fileOutput);
            StreamWriter sw = new StreamWriter(fileOutput, true, Encoding.Default);
            sw.WriteLine(q);
            sw.Close();
        }
    }
}
