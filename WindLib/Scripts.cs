using CommonLib;
using CommonLib.Classes;
using CommonLib.Data.Providers.InternetServices;
using CommonLibLib.Data.Interfaces;
using CommonLibLib.Data.Providers.InternetServices;
using GMap.NET;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.WindLib.Classes;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data;
using WindEnergy.WindLib.Data.Interfaces;
using WindEnergy.WindLib.Data.Providers;
using WindEnergy.WindLib.Data.Providers.DB;
using WindEnergy.WindLib.Data.Providers.FileSystem;
using WindEnergy.WindLib.Data.Providers.InternetServices;
using WindEnergy.WindLib.Operations.Limits;
using WindLib;

namespace WindEnergy.WindLib
{
    /// <summary>
    /// вспомогательные скрипты (обработка данных, загрузка информации с сайтов)
    /// </summary>
    public static class Scripts
    {
        private class Requester : BaseConnection
        {
            public Requester() : base("", null) { }

            public override TimeSpan MinQueryInterval { get { return TimeSpan.FromSeconds(0.1); } }
            public override int MaxAttempts { get { return 5; } }
            public override TimeSpan SessionLifetime { get => TimeSpan.FromMinutes(60); }

            public JObject GetJson(string url)
            {
                return base.SendJsonGetRequest(url, out HttpStatusCode code, false);
            }
        }


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

        #region обновление БД Классы открытости МС

        /// <summary>
        /// Получает координаты и ID  метеостанций из созраненного листа Список метеостанций по ФО БД АМС
        /// </summary>
        /// <param name="fromFile">сохраненный лист из БД АМС</param>
        /// <param name="destFile">результирующая БД, файл будет перезаписан</param>
        public static void ConvertFlugerFromFile(string fromFile, string destFile)
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
                        if (arr.Length == 0 || arr[0] == "") break;
                        int id = int.Parse(arr[0]);
                        if (!ids.Contains(id))
                        {
                            //ID;Name;Lat;Lon
                            string name = arr[3];
                            string lat = arr[4];
                            string lon = arr[5];
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
        /// <param name="dataFile"> лист БД АМС "Скорости по ФО"</param>
        /// <param name="destFile">результирующий файл от метода Scripts.ConvertMSIDFromFile</param>
        public static void ConvertFlugerDataFromFile(string dataFile, string destFile)
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
            using (StreamReader sr = new StreamReader(dataFile))
            {
                sr.ReadLine();
                string line = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] arr = line.Split(';');
                    int id = int.Parse(arr[0]);
                    mLines.Add(id, arr.Skip(1).ToArray());
                    line = sr.ReadLine();
                }
            }

            using (StreamWriter sw = new StreamWriter(destFile, false, Encoding.UTF8))
            {
                foreach (var ln in destLines)
                {
                    int id = int.Parse(ln[0]);
                    if (mLines.ContainsKey(id))
                    {
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
                mt.Altitude = alts.GetElevation(mt.Position);
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
                    sw.WriteLine(string.Format("{0};{1};{2};{3};{4}", adr, coordinates.Lat.ToString().Replace(Constants.DecimalSeparator, '.'), coordinates.Lng.ToString().Replace(Constants.DecimalSeparator, '.'), 0, limit));
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
                    Position = new PointLatLng(latd, lond),
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
        /// <param name="fileOutput">результирующий файл</param>
        /// <param name="action">действие при изменении процента</param>
        /// <param name="checkStop">функция, вызываемая для проверки необходимости остановить процесс</param>
        public static void LoadAllRP5Meteostations(string fileOutput, Action<int, int, string, int, int, int, int> action = null, Func<bool> checkStop = null)
        {
            RP5ru engine = new RP5ru(Vars.Options.CacheFolder + "\\rp5.ru");
            List<RP5MeteostationInfo> result;
            string lastQ = null;

            //если файл уже есть,то надо продолжить с последнего сочетания
            if (File.Exists(fileOutput))
            {
                result = new RP5MeteostationDatabase(fileOutput).LoadDatabaseFile().Values.ToList(); //загрузка списка
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
                                        m.Altitude = Vars.ETOPOdatabase.GetElevation(m.Position);
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
                sw.WriteLine($"{lim.Name};{lim.Position.Lat.ToString().Replace(Constants.DecimalSeparator, '.')};{lim.Position.Lng.ToString().Replace(Constants.DecimalSeparator, '.')};{lim.GetMinimal(MeteorologyParameters.Speed)};{lim.GetMaximal(MeteorologyParameters.Speed)}");
            sw.Close();
        }


        /// <summary>
        /// загрузка всей БД рп5
        /// </summary>
        /// <param name="directoryOut">в папку сохраняется БД</param>
        /// <param name="skipErrors">пропускатьизвестные проблеммные МС</param>
        /// <param name="act">действие при смене прогресса</param>
        /// <param name="checkStop">проверка остановки</param>
        public static void LoadAllRP5Database(string directoryOut, bool skipErrors, Action<int, string> act, Func<bool> checkStop)
        {
            const int AUTH_COUNT = 3;
            const int CONNECT_COUNT = 10;

            if (!Directory.Exists(directoryOut))
                Directory.CreateDirectory(directoryOut);

            var meteostations = Vars.RP5Meteostations.List;
            var engine = new RP5ru(null, 0); //нет смысла использовать кэш при загрузке БД
            var saver = new ExcelFile();
            List<string> errors = new List<string>();

            //загрузка известных ошибок
            if (File.Exists(directoryOut + "\\errors.txt"))
            {
                StreamReader sr = new StreamReader(directoryOut + "\\errors.txt");
                while (!sr.EndOfStream)
                    errors.Add(sr.ReadLine());
                sr.Close();
            }

            bool stop = false;
            int i = 0;
            foreach (var mts in meteostations)
            {
                if (checkStop.Invoke() || stop) //если остановка внешняя или внутренняя то выход
                    break;

                if (skipErrors && errors.Contains(mts.ID)) //если включен пропуск ошибок, то проверяем
                    continue;


                double perc = ((double)++i / meteostations.Count) * 100d; //текцщий процент выполнения

                Action<double> loadAction = new Action<double>((loadPercent) => // действие при частичной загрузке
                    {
                        act.Invoke((int)perc, $"Загружается {i} из {meteostations.Count} ({perc.ToString("0.00")})%, загрузка по частям: {loadPercent.ToString("0.0")}%");
                    });

                string filename = directoryOut + "\\" + RP5Database.PREFIX + mts.ID + ".xlsx";
                if (!File.Exists(filename)) //если файл не существует, то скачиваем эту МС
                {
                    //по три попытки на загрузку каждого ряда
                    int step = 0;
                    RawRange range = null;
                    while (true)
                    {
                        try
                        {
                            step++;
                            range = engine.GetRange(mts.MonitoringFrom, DateTime.Now, mts, loadAction, checkStop);
                            break; //ряд скачался, выходим из цикла
                        }
                        catch (WindEnergyException excep) //если ошибку вернкл сервер, то ждем 1с и пробуем ещё раз
                        {
                            if (step <= AUTH_COUNT)
                            {
                                Thread.Sleep(1000);
                                continue;
                            }
                            else { range = null; break; } //если больше 3 раз не вышло, то следующая МС, а ату отменяем
                        }
                        catch (WebException wex) //если нет сети, то ждем 30с
                        {
                            if (step <= CONNECT_COUNT)
                            {
                                act.Invoke((int)perc, $"Загружается {i} из {meteostations.Count} ({perc.ToString("0.00")})%, отсутствует подключение к интернету, жду 30с...");
                                Thread.Sleep(30000);//ждем 30 с до следующего подключения, если нет интернета
                                continue;
                            }
                            else //если за 10 раз не появилась связь, то прекращаем загрузку
                            {
                                stop = true;
                                break;
                            }
                        }
                    }
                    if (range != null) // если ряд всё-таки скачался, то сохраняем
                    {
                        new Task(new Action(() =>
                        {
                            saver.SaveRange(range, filename);
                        })).Start();
                    }
                    else
                    {
                        errors.Add(mts.ID);
                    }
                }
            }

            StreamWriter sw = new StreamWriter(directoryOut + "\\errors.txt");
            foreach (string error in errors)
                sw.WriteLine(error);
            sw.Close();
        }

        /// <summary>
        /// обновление всей БД рп5
        /// </summary>
        /// <param name="directoryOut">папка с БД</param>
        /// <param name="act">действие при изменении прогресса</param>
        /// <param name="checkStop">проверка остановки</param>
        public static void UpdateAllRP5Database(string directoryOut, Action<int, string> act, Func<bool> checkStop)
        {
            var files = new DirectoryInfo(directoryOut).GetFiles("*.xlsx");
            var engine = new RP5ru(null);
            var saver = new ExcelFile();
            bool completed = false;

            ConcurrentBag<FileInfo> progress = new ConcurrentBag<FileInfo>();

            //таск обновления прогресса
            Task progressor = new Task(() =>
            {
                while (!completed)
                {
                    Thread.Sleep(500);
                    double perc = ((double)progress.Count / files.Count()) * 100d;
                    act.Invoke((int)perc, $"Файл {progress.Count} из {files.Count()}");
                }
            });
            progressor.Start();

            var task = Parallel.ForEach(files, new ParallelOptions() { MaxDegreeOfParallelism = 5 }, new Action<FileInfo, ParallelLoopState>((file, state) =>
                  {
                      if (checkStop.Invoke())
                          state.Stop();
                      progress.Add(file);
                      RawRange range = saver.LoadRange(file.FullName);
                      if (range == null || range.Count == 0)
                          return;
                      if (DateTime.Now - range.Last().Date < TimeSpan.FromDays(2))
                          return;
                      var mts = range.Meteostation;
                      try
                      {
                          RawRange newRange = engine.GetRange(range.Last().Date, DateTime.Now, mts);

                          var r = range.Concat(newRange);
                          saver.SaveRange(r, file.FullName);
                      }
                      catch (Exception)
                      {
                      }
                  }));
            completed = true;
        }


        /// <summary>
        /// выгрузка БД НАСА
        /// </summary>
        /// <param name="directoryOut"></param>
        /// <param name="act"></param>
        /// <param name="checkStop"></param>
        public static void LoadAllNasaDatabase(string directoryOut, Action<int, string> act, Func<bool> checkStop)
        {
            string fields = "WS10M,T10M,RH2M,PS,ALLSKY_SFC_SW_DWN,CLRSKY_SFC_SW_DWN";
            string header = "Дата;WS10M, m/s;T10M, ℃;RH2M, %;PS, kPa;ALLSKY_SFC_SW_DWN, kW-hr/m^2/day;CLRSKY_SFC_SW_DWN, kW-hr/m^2/day";

            string filePosition = directoryOut + "\\position.txt";

            DateTime lastLoad = DateTime.Now;
            bool iterate = true;

            if (checkStop != null && checkStop.Invoke()) //проверка остановки процесса
                return;


            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) - TimeSpan.FromDays(365 * 30);
            DateTime toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            double startLat = -89, endLat = 89;
            double startLon = -179, endLon = 179;
            double stepLon = 1;
            object locker = new object();

            double total = (endLon - startLon) * (endLat - startLat);
            double current = 0, loaded = 0;
            DateTime startTime = DateTime.Now;
            PointLatLng coordC = PointLatLng.Empty;
            bool stop = false;

            //обновление статуса
            new Task(() =>
            {
                double remain = 0;
                double oldLoaded = 0;
                while (true)
                {
                    double perc = ((current / total) * 100d);
                    TimeSpan tm = DateTime.Now - startTime;
                    if (oldLoaded != loaded)
                    {
                        double remainInt = total - current;
                        remain = (tm.TotalHours / loaded) * remainInt;
                        oldLoaded = loaded;
                    }
                    act((int)perc, $"Координаты: {coordC.ToString()}, {perc.ToString("0.000")}%, {current}/{total}, прошло: {tm.ToString(@"d\.hh\:mm\:ss")}, осталось {remain.ToString("0.00")} ч");
                    if (stop)
                        break;
                    Thread.Sleep(200);
                }

            }).Start();

            //вычисление случайной строки
            Func<int,string> random = (length) => {
                Random _random = new Random(Environment.TickCount); string chars = "0123456789abcdefghijklmnopqrstuvwxyz"; StringBuilder builder = new StringBuilder(length);
                for (int i = 0; i < length; ++i)
                    builder.Append(chars[_random.Next(chars.Length)]);
                return builder.ToString();
            };

            Parallel.For((int)startLat, (int)endLat, new ParallelOptions() { MaxDegreeOfParallelism = 4 }, (lat) =>
            {

                if (checkStop != null && checkStop.Invoke()) //проверка остановки процесса
                    return;

                string curdir = directoryOut + "\\" + lat;
                if (!Directory.Exists(curdir))
                    Directory.CreateDirectory(curdir);


                for (double lon = startLon; lon <= endLon; lon += stepLon)
                {
                    if (checkStop != null && checkStop.Invoke()) //проверка остановки процесса
                        return;
                    current++;
                    PointLatLng coord = new PointLatLng(lat, lon);
                    coordC = coord;

                    Dictionary<DateTime, double> WS10M = new Dictionary<DateTime, double>();
                    Dictionary<DateTime, double> T10M = new Dictionary<DateTime, double>();
                    Dictionary<DateTime, double> RH2M = new Dictionary<DateTime, double>();
                    Dictionary<DateTime, double> PS = new Dictionary<DateTime, double>();
                    Dictionary<DateTime, double> ALLSKY_SFC_SW_DWN = new Dictionary<DateTime, double>();
                    Dictionary<DateTime, double> CLRSKY_SFC_SW_DWN = new Dictionary<DateTime, double>();

                    //отправка статуса

                    //проверка файла
                    string fname = curdir + "\\" + lon + ".csv";
                    if (File.Exists(fname))
                        continue;
                    iterate = true;
                    JToken ans = null;
                    while (iterate)
                    {
                        try
                        {
                    Requester req = new Requester();
                            //загрузка данных

                            string url = "https://power.larc.nasa.gov/cgi-bin/v1/DataAccess.py?request=execute&identifier=SinglePoint&parameters={0}&startDate={1}&endDate={2}&userCommunity=SSE&tempAverage=DAILY&outputList=ASCII&lat={3}&lon={4}&user={5}";
                            url = string.Format(url,
                                fields,
                                fromDate.ToString("yyyyMMdd"),
                                toDate.ToString("yyyyMMdd"),
                                coord.Lat.ToString("00.00").Replace(Constants.DecimalSeparator, '.'),
                                coord.Lng.ToString("00.00").Replace(Constants.DecimalSeparator, '.'),
                                random(8));
                            ans = req.GetJson(url);

                            iterate = false;
                        }
                        catch (Exception ex)
                        {
                            lock (locker)
                            {
                                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\load_error.log", true, Encoding.UTF8);
                                sw.WriteLine(ex.Message + "\r\n" + DateTime.Now.ToString() + "\r\n" + ex.StackTrace + "\r\n");
                                sw.Close();
                                iterate = true;
                                Thread.Sleep(10000);
                            }
                        }
                    }
                    if (ans == null || ans["messages"].HasValues) //если есть ошибки, то дальше
                        continue;
                    var parameters = ans["features"][0]["properties"]["parameter"];
                    WS10M = getValues(parameters["WS10M"]);
                    T10M = getValues(parameters["T10M"]);
                    RH2M = getValues(parameters["RH2M"]);
                    PS = getValues(parameters["PS"]);
                    ALLSKY_SFC_SW_DWN = getValues(parameters["ALLSKY_SFC_SW_DWN"]);
                    CLRSKY_SFC_SW_DWN = getValues(parameters["CLRSKY_SFC_SW_DWN"]);


                    StreamWriter swr = new StreamWriter(fname, false, Encoding.UTF8);
                    swr.WriteLine(header);
                    for (DateTime date = fromDate; date <= toDate; date += TimeSpan.FromDays(1))
                    {
                        double WS10M_v = WS10M.ContainsKey(date) ? WS10M[date] : double.NaN;
                        double T10M_v = T10M.ContainsKey(date) ? T10M[date] : double.NaN;
                        double RH2M_v = RH2M.ContainsKey(date) ? RH2M[date] : double.NaN;
                        double PS_v = PS.ContainsKey(date) ? PS[date] : double.NaN;
                        double ALLSKY_SFC_SW_DWN_v = ALLSKY_SFC_SW_DWN.ContainsKey(date) ? ALLSKY_SFC_SW_DWN[date] : double.NaN;
                        double CLRSKY_SFC_SW_DWN_v = CLRSKY_SFC_SW_DWN.ContainsKey(date) ? CLRSKY_SFC_SW_DWN[date] : double.NaN;
                        string line = $"{date.ToString()};{WS10M_v};{T10M_v};{RH2M_v};{PS_v};{ALLSKY_SFC_SW_DWN_v};{CLRSKY_SFC_SW_DWN_v}";
                        swr.WriteLine(line);
                    }
                    swr.Close();
                    loaded++;
                }

            });

            if (!iterate)
                stop = true;

        }

        /// <summary>
        /// возвращает список значений из json объекта
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static Dictionary<DateTime, double> getValues(JToken param)
        {
            Dictionary<DateTime, double> res = new Dictionary<DateTime, double>();
            var vals = JsonConvert.DeserializeObject<Dictionary<string, double>>(param.ToString());
            foreach (string key in vals.Keys)
            {
                double val = vals[key];

                DateTime dt = new DateTime(
                    int.Parse(key.Substring(0, 4)),
                    int.Parse(key.Substring(4, 2)),
                    int.Parse(key.Substring(6, 2))
                    );
                res.Add(dt, val);
            }
            return res;
        }

        #endregion


    }
}
