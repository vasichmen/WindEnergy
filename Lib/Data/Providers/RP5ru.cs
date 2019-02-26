﻿using GMap.NET;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data.Interfaces;

namespace WindEnergy.Lib.Data.Providers
{
    /// <summary>
    /// взаимодействие с сайтом RP5.ru
    /// </summary>
    public class RP5ru : BaseConnection, IRangeProvider
    {
        /// <summary>
        /// информация о точке с погодой
        /// </summary>
        public class WmoInfo
        {
            /// <summary>
            /// wmo_id для этой точки
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// название
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// строка ссылки
            /// </summary>
            public string altName { get; set; }

            /// <summary>
            /// ссылка на страницу
            /// </summary>
            public string Link { get { return @"https://rp5.ru/" + altName; } }

            public override string ToString()
            {
                return name;
            }
        }



        public override TimeSpan MinQueryInterval { get { return TimeSpan.FromSeconds(1); } }
        public override int MaxAttempts { get { return 5; } }

        /// <summary>
        /// создаёт новый объект с кэшем в указанной папке и заданной длительностью хранения 
        /// </summary>
        /// <param name="cacheDirectory">папка с кэшем или null, если не надо использоать кэш</param>
        /// <param name="duration">длительность хранения в часах. По умолчанию - неделя</param>
        public RP5ru(string cacheDirectory, double duration = 7 * 24) : base(cacheDirectory, duration) { }

        /// <summary>
        /// загрузить файл данных с сайта и открыть ряд наблюдений
        /// </summary>
        /// <param name="fromDate">с какой даты</param>
        /// <param name="toDate">до какой даты</param>
        /// <param name="point_info">объект MeteostationInfo - информация о метеостанции</param>
        /// <returns></returns>
        public RawRange GetRange(DateTime fromDate, DateTime toDate, object point_info)
        {
            MeteostationInfo info = point_info as MeteostationInfo;

            //получение ссылки на файл
            string data, link;
            switch (info.MeteoSourceType)
            {
                case MeteoSourceType.Airport:
                    data = "metar={0}&a_date1={1}&a_date2={2}&f_ed3={3}&f_ed4={4}&f_ed5={5}&f_pe={6}&f_pe1={7}&lng_id=2";
                    link = "http://rp5.ru/responses/reFileMetar.php";
                    break;
                case MeteoSourceType.Meteostation:
                    data = "wmo_id={0}&a_date1={1}&a_date2={2}&f_ed3={3}&f_ed4={4}&f_ed5={5}&f_pe={6}&f_pe1={7}&lng_id=2";
                    link = "http://rp5.ru/responses/reFileSynop.php";
                    break;
                default: throw new Exception("Этот тип метеостанций не реализован");
            }

            data = string.Format(data,
                info.ID, //id
                fromDate.Date.ToString("dd.MM.yyyy"), //from
                toDate.Date.ToString("dd.MM.yyyy"), //to
                DateTime.Now.Month, //f_ed3 - только месяц
                DateTime.Now.Month, //f_ed4 - только месяц
                DateTime.Now.Day, //f_ed5 - только день
                1, //f_pe
                2 //f_pe1
                );

            string str = this.SendStringPostRequest(link, data, null, "https://rp5.ru/");

            if (str.Contains("FS004"))
                throw new Exception("Для этого id нет архива погоды");
            if (str.Contains("FS002"))
                throw new Exception("Ошибка в исходных данных");

            int start = str.IndexOf("href=") + 5;
            str = str.Substring(start);
            int end = str.IndexOf(">");
            string lnk = str.Substring(0, end);

            //загрузка файла с сервера
            string tmp_dl_file = Vars.LocalFileSystem.GetTempFileName();
            WebClient webcl = new WebClient();
            webcl.DownloadFile(lnk, tmp_dl_file);

            //распаковка
            string tmp_unpack_file = tmp_dl_file + ".csv";
            LocalFileSystem.UnGZip(tmp_dl_file, tmp_unpack_file);

            //открытие файла
            RawRange res = RawRangeSerializer.DeserializeFile(tmp_unpack_file);
            return res;
        }

        /// <summary>
        /// найти ближайшие метеостанции к выбранной точке
        /// </summary>
        /// <param name="wmoInfo">информация о точке с погодой</param>
        /// <returns></returns>
        public List<MeteostationInfo> GetNearestMeteostations(WmoInfo wmoInfo)
        {
            //страница погоды в заданной точке
            HtmlDocument point_page = SendHtmlGetRequest(wmoInfo.Link, out HttpStatusCode code);

            if (code != HttpStatusCode.OK)
                throw new Exception("При загрузке страницы произошла ошибка " + code.ToString());

            //поиск ссылок на метеостанции
            List<HtmlNode> archives = new List<HtmlNode>() {
                point_page.GetElementbyId("archive_link"), //архив погоды на метеостанции
                point_page.GetElementbyId("metar_link"), //аэропорт
                point_page.GetElementbyId("wug_link"), //на неофициальной метеостанции
            };

            List<MeteostationInfo> res = new List<MeteostationInfo>();
            foreach (HtmlNode some_link in archives)
                if (some_link != null)
                {
                    MeteostationInfo nm = new MeteostationInfo();
                    nm.Name = wmoInfo.name; //искомая точка
                    nm.Link = some_link.Attributes["href"].Value; //ссылка на страницу

                    //название
                    int s1 = some_link.InnerText.IndexOf(" (");
                    string onmouseover = some_link.Attributes["onmouseover"].Value; //значение атрибута вывода подсказки
                    string nmm = onmouseover.Replace("tooltip(this, '", "").Replace("' , 'hint')", "");
                    nm.Name = some_link.InnerText.Substring(0, s1) + " (" + nmm + ")";

                    //расстояние до точки
                    string content = some_link.InnerText;
                    int start = content.IndexOf("( ");
                    int end = content.IndexOf(" км");
                    if (start == -1 || end == -1)
                        nm.OwnerDistance = 0;
                    else
                    {
                        int l = end - (start + 2);
                        string dist = content.Substring(start + 2, l);
                        nm.OwnerDistance = double.Parse(dist.Replace('.', Vars.DecimalSeparator));
                    }

                    //тип источника
                    string a_id = some_link.Attributes["id"].Value;
                    switch (a_id)
                    {
                        case "archive_link":
                            nm.MeteoSourceType = MeteoSourceType.Meteostation;
                            break;
                        case "metar_link":
                            nm.MeteoSourceType = MeteoSourceType.Airport;
                            break;
                        case "wug_link":
                            continue; //для неофициальных метеостанций нельзя получить архив погоды =( 
                            nm.MeteoSourceType = MeteoSourceType.UnofficialMeteostation;
                            break;
                        default: throw new Exception("Этот тип метеостанции не реализован");
                    }
                    GetMeteostationExtInfo(ref nm); //запись информации об id метеостанции, дате начала наблюдений
                    if (nm != null)
                        res.Add(nm);
                }
            return res;
        }
       
        /// <summary>
        /// получить из страницы архива погоды id метеостанции, дату начала наблюдений. Двнные запишутся в структуру info, где уже должна быть записана ссылка на страницу
        /// </summary>
        /// <param name="page_link">ссылка на страницу архива</param>
        /// <returns></returns>
        public void GetMeteostationExtInfo(ref MeteostationInfo info)
        {
            if (info == null)
                throw new ArgumentException("info");
            if (string.IsNullOrWhiteSpace(info.Link))
                throw new ArgumentException("В структуре info должна быть ссылка на страницу архива погоды");

            HtmlDocument page = SendHtmlGetRequest(info.Link, out HttpStatusCode code);


            HtmlNode wmo = page.GetElementbyId("wmo_id"); //архив погоды на метеостанции
            HtmlNode metar = page.GetElementbyId("cc_str"); //аэропорт
            HtmlNode wug = page.GetElementbyId("wug"); //на неофициальной метеостанции

            //получение ID
            if (wmo != null) //метеостанция
            {
                info.ID = wmo.Attributes["value"].Value;
                info.MeteoSourceType = MeteoSourceType.Meteostation;
            }
            else if (metar != null) //аэропорт
            {
                string pg = page.Text;
                int start = pg.IndexOf("fFileMetarGet(");
                int end = pg.IndexOf(')', start);
                if (start == -1 || end == -1)
                    throw new Exception("Что-то не так");
                start += +"fFileMetarGet(1317900300".Length;
                string id = pg.Substring(start + 1, end - 1 - start);
                info.ID = id;
                info.MeteoSourceType = MeteoSourceType.Airport;
            }
            else //неофициальная метеостанция
            {
                info = null;
                return;
            }
            //получение даты начала наблюдений
            {
                string pg = page.Text;
                int start = pg.IndexOf("наблюдения с ");
                int end = pg.IndexOf("</span>", start);
                if (start == -1 || end == -1)
                    throw new Exception("Что-то не так");
                start += +"наблюдения с ".Length;
                string fdate = pg.Substring(start, end - 1 - start);
                DateTime mon_from = DateTime.Parse(fdate);
                info.MonitoringFrom = mon_from;
            }

        }

        /// <summary>
        /// найти точки на сайте rp5.ru по частичному запросу
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<WmoInfo> Search(string query)
        {
            if (query.Length < 2)
                throw new Exception("длина запроса должна быть больше 2 символов");

            //https://rp5.ru/responses/reJsonSearch.php?langid=2&q=%D1%83%D1%81&limit=500&timestamp=1548533724161
            string url = "https://rp5.ru/responses/reJsonSearch.php?langid=2&q={0}&limit=500&timestamp={1}";
            url = string.Format(url, query, 1548533724161);

            JToken ans = SendJsonGetRequest(url, true, "https://rp5.ru/");
            if (ans == null)
                return new List<WmoInfo>();
            List<WmoInfo> res = new List<WmoInfo>();
            JToken array = ans["array"];
            foreach (JToken place in array)
            {
                string id = place["id"].ToString();
                string name = place["name"].ToString();
                string namealt = place["namealt"].ToString();
                res.Add(new WmoInfo() { altName = namealt, id = id, name = name });
            }
            return res;
        }

        /// <summary>
        /// получить направление ветра из строкового представления
        /// </summary>
        /// <param name="direction">строка направления из файла РП5</param>
        /// <returns></returns>
        public static WindDirections GetWindDirectionFromString(string direction)
        {
            switch (direction.ToLower())
            {
                case "штиль, безветрие":
                    return WindDirections.Calm;
                case "ветер, дующий с севера":
                    return WindDirections.N;
                case "ветер, дующий с северо-северо-востока":
                    return WindDirections.NNE;
                case "ветер, дующий с северо-востока":
                    return WindDirections.NE;
                case "ветер, дующий с востоко-северо-востока":
                    return WindDirections.NEE;
                case "ветер, дующий с востока":
                    return WindDirections.E;
                case "ветер, дующий с востоко-юго-востока":
                    return WindDirections.SEE;
                case "ветер, дующий с юго-востока":
                    return WindDirections.SE;
                case "ветер, дующий с юго-юго-востока":
                    return WindDirections.SSE;
                case "ветер, дующий с юга":
                    return WindDirections.S;
                case "ветер, дующий с юго-юго-запада":
                    return WindDirections.SSW;
                case "ветер, дующий с юго-запада":
                    return WindDirections.SW;
                case "ветер, дующий с западо-юго-запада":
                    return WindDirections.SWW;
                case "ветер, дующий с запада":
                    return WindDirections.W;
                case "ветер, дующий с западо-северо-запада":
                    return WindDirections.NWW;
                case "ветер, дующий с северо-запада":
                    return WindDirections.NW;
                case "ветер, дующий с северо-северо-запада":
                    return WindDirections.NNW;
                case "переменное направление":
                    return WindDirections.Variable;
                default: throw new Exception("Это направление ветра не реализовано");
            }
        }

        /// <summary>
        /// получить строку по направлению ветра
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static string GetStringFromWindDirection(WindDirections direction)
        {
            switch (direction)
            {
                case WindDirections.N:
                    return "ветер, дующий с севера";
                case WindDirections.NNE:
                    return "ветер, дующий с северо-северо-востока";
                case WindDirections.NE:
                    return "ветер, дующий с северо-востока";
                case WindDirections.NEE:
                    return "ветер, дующий с востоко-северо-востока";
                case WindDirections.E:
                    return "ветер, дующий с востока";
                case WindDirections.SEE:
                    return "ветер, дующий с востоко-юго-востока";
                case WindDirections.SE:
                    return "ветер, дующий с юго-востока";
                case WindDirections.SSE:
                    return "ветер, дующий с юго-юго-востока";
                case WindDirections.S:
                    return "ветер, дующий с юга";
                case WindDirections.SSW:
                    return "ветер, дующий с юго-юго-запада";
                case WindDirections.SW:
                    return "ветер, дующий с юго-запада";
                case WindDirections.SWW:
                    return "ветер, дующий с западо-юго-запада";
                case WindDirections.W:
                    return "ветер, дующий с запада";
                case WindDirections.NWW:
                    return "ветер, дующий с западо-северо-запада";
                case WindDirections.NW:
                    return "ветер, дующий с северо-запада";
                case WindDirections.NNW:
                    return "ветер, дующий с северо-северо-запада";
                case WindDirections.Variable:
                    return "переменное направление";
                default: throw new Exception("Это направление ветра не реализовано");
            }
        }

        /// <summary>
        /// Загрузить ряд из файла csv
        /// </summary>
        /// <param name="file">файл csv</param>
        /// <returns></returns>
        public static RawRange LoadCSV(string file)
        {
            StreamReader sr = new StreamReader(file, Encoding.UTF8, true);
            RawRange res = new RawRange();
            res.BeginChange();

            //определение формата файла csv
            MeteoSourceType type;
            string title = sr.ReadLine();
            if (title.Contains("WMO_ID"))
                type = MeteoSourceType.Meteostation;
            else if (title.Contains("METAR"))
                type = MeteoSourceType.Airport;
            else throw new Exception("Неизвестный формат файла");

            switch (type)
            {
                case MeteoSourceType.Meteostation: //загрузка архива с метеостанции
                    //пропуск пустых строк (одна уже пропущена при чтении заголовка)
                    for (int i = 0; i < 6; i++)
                        sr.ReadLine();

                    string data = sr.ReadToEnd();
                    sr.Close();
                    string[] lines = data.Replace("\"", "").Split('\n');
                    foreach (string line in lines)
                    {
                        string[] elems = line.Split(';');
                        if (elems.Length < 8)
                            continue;
                        if (elems[6] == "")
                            continue;
                        if (elems[7] == "")
                            continue;

                        double temp = elems[1] == "" ? double.NaN : double.Parse(elems[1].Replace('.', Vars.DecimalSeparator));
                        DateTime dt = DateTime.Parse(elems[0]);
                        double spd = double.Parse(elems[7].Replace('.', Vars.DecimalSeparator));
                        double wet = elems[5] == "" ? double.NaN : double.Parse(elems[5].Replace('.', Vars.DecimalSeparator));
                        string dirs = elems[6];
                        WindDirections direct = RP5ru.GetWindDirectionFromString(dirs);
                        res.Add(new RawItem() { Date = dt, DirectionRhumb = direct, Speed = spd, Temperature = temp, Wetness = wet });
                    }
                    res.FileFormat = FileFormats.RP5WmoCSV;
                    break;
                case MeteoSourceType.Airport: //загрузка архива с аэропорта
                    //пропуск пустых строк (одна уже пропущена при чтении заголовка)
                    for (int i = 0; i < 6; i++)
                        sr.ReadLine();

                    string data2 = sr.ReadToEnd();
                    sr.Close();
                    string[] lines2 = data2.Replace("\"", "").Split('\n');
                    foreach (string line in lines2)
                    {
                        string[] elems = line.Split(';');
                        if (elems.Length < 8)
                            continue;
                        if (elems[5] == "")
                            continue;
                        if (elems[6] == "")
                            continue;

                        double temp = elems[1] == "" ? double.NaN : double.Parse(elems[1].Replace('.', Vars.DecimalSeparator));
                        DateTime dt = DateTime.Parse(elems[0]);
                        double spd = double.Parse(elems[6].Replace('.', Vars.DecimalSeparator));
                        double wet = elems[4] == "" ? double.NaN : double.Parse(elems[4].Replace('.', Vars.DecimalSeparator));
                        string dirs = elems[5];
                        WindDirections direct = RP5ru.GetWindDirectionFromString(dirs);
                        res.Add(new RawItem() { Date = dt, DirectionRhumb = direct, Speed = spd, Temperature = temp, Wetness = wet });
                    }
                    res.FileFormat = FileFormats.RP5MetarCSV;
                    break;
                case MeteoSourceType.UnofficialMeteostation:
                    throw new Exception("Этот тип файла не поддерживается");
            }
            res.EndChange();
            return res;
        }

        /// <summary>
        /// сохранить как файл CSV
        /// </summary>
        /// <param name="rang">ряд для сохранениея</param>
        /// <param name="filename">адрес файла</param>
        /// <param name="rP5MetarCSV">формат</param>
        public static void ExportCSV(RawRange rang, string filename, FileFormats format)
        {
            StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);

            switch (format)
            {
                case FileFormats.RP5MetarCSV:
                    string caption = "\"Местное время\";\"T\";\"P0\";\"P\";\"U\";\"DD\";\"Ff\";\"ff10\";\"WW\";\"W'W'\";\"c\";\"VV\";\"Td\";";
                    string title = "METAR";
                    sw.WriteLine(title);
                    for (int i = 0; i < 5; i++) sw.WriteLine("#");
                    sw.WriteLine(caption);
                    string fm = "\"{0}\";\"{1}\";\"\";\"\";\"{2}\";\"{3}\";\"{4}\";\"\";\"\";\"\";\"\";\"\";\"\";";
                    foreach (RawItem item in rang)
                    {
                        if (item.Direction == double.NaN || item.Speed == double.NaN || item.DirectionRhumb == WindDirections.Undefined)
                            continue;
                        sw.WriteLine(fm,
                            item.Date.ToString(),
                            item.Temperature,
                            item.Wetness,
                            GetStringFromWindDirection(item.DirectionRhumb),
                            item.Speed);
                    }
                    break;

                case FileFormats.RP5WmoCSV:
                    string caption1 = "\"Местное время\";\"T\";\"Po\";\"P\";\"Pa\";\"U\";\"DD\";\"Ff\";\"ff10\";\"ff3\";\"N\";\"WW\";\"W1\";\"W2\";\"Tn\";\"Tx\";\"Cl\";\"Nh\";\"H\";\"Cm\";\"Ch\";\"VV\";\"Td\";\"RRR\";\"tR\";\"E\";\"Tg\";\"E'\";\"sss\"";
                    string title1 = "WMO_ID";
                    sw.WriteLine(title1);
                    for (int i = 0; i < 5; i++) sw.WriteLine("#");
                    sw.WriteLine(caption1);
                    string fm1 = "\"{0}\";\"{1}\";\"\";\"\";\"\";\"{2}\";\"{3}\";\"{4}\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";";
                    foreach (RawItem item in rang)
                    {
                        if (item.Direction == double.NaN || item.Speed == double.NaN || item.DirectionRhumb == WindDirections.Undefined)
                            continue;
                        sw.WriteLine(fm1,
                            item.Date.ToString(),
                            item.Temperature,
                            item.Wetness,
                            GetStringFromWindDirection(item.DirectionRhumb),
                            item.Speed);
                    }
                    break;
                default: throw new Exception("Этот формат не реализован");
            }

            sw.Close();
        }
    }
}
