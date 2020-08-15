using CommonLib;
using CommonLib.Classes;
using CommonLib.Data.Providers.InternetServices;
using CommonLibLib.Data.Providers.FileSystem;
using Fizzler.Systems.HtmlAgilityPack;
using GMap.NET;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using WindEnergy.WindLib.Classes;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data.Interfaces;
using WindEnergy.WindLib.Data.Providers.FileSystem;
using WindLib;

namespace WindEnergy.WindLib.Data.Providers.InternetServices
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


        /// <summary>
        /// количество лет в одной загрузке данных 
        /// </summary>
        private const double LOAD_STEP_YEARS = 2.5;

        /// <summary>
        /// данные cookie для этого экземпляра
        /// </summary>
        private string CookieData
        {
            get
            {
                string staticData = "extreme_open=false; tab_wug=1; i=5483%7C5483; iru=5483%7C5483; ru=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0+%28%D0%92%D0%94%D0%9D%D0%A5%29%7C%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0+%28%D0%92%D0%94%D0%9D%D0%A5%29; last_visited_page=http%3A%2F%2Frp5.ru%2F%D0%9F%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0_%D0%B2_%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B5_%28%D0%92%D0%94%D0%9D%D0%A5%29; tab_synop=2;__utma=66441069.285134014.1561820218.1561820218.1561820218.1; __utmc=66441069; __utmz=66441069.1561820218.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); tab_metar=2; is_adblock=0; f_enc=utf8; format=csv; __utmt=1; lang=ru; __utmb=66441069.6.10.1561820218";
                staticData += ";last_visited_page=https://rp5.ru";
                return staticData + string.Format(";PHPSESSID={0}", this.PHPSESSID);
            }
        }

        public override TimeSpan MinQueryInterval { get { return TimeSpan.FromSeconds(0.5); } }
        public override int MaxAttempts { get { return 5; } }

        /// <summary>
        /// заголовки для этого экземпляра
        /// </summary>
        public string Headers
        {
            get
            {
                return "X-Requested-With:XMLHttpRequest;Pragma:no - cache;Cache-Control:no - cache;DNT:1;Accept-Language:ru,en";
            }
        }

        /// <summary>
        /// время жизни сессии
        /// </summary>
        public override TimeSpan SessionLifetime
        {
            get
            {
                return TimeSpan.FromMinutes(15);
            }
        }

        /// <summary>
        /// создаёт новый объект с кэшем в указанной папке и заданной длительностью хранения 
        /// </summary>
        /// <param name="cacheDirectory">папка с кэшем или null, если не надо использоать кэш</param>
        /// <param name="duration">длительность хранения в часах. По умолчанию - неделя</param>
        public RP5ru(string cacheDirectory, double duration = 7 * 24) : base("https://rp5.ru", cacheDirectory, duration)
        {
        }

        /// <summary>
        /// загрузить файл данных с сайта и открыть ряд наблюдений
        /// </summary>
        /// <param name="fromDate">с какой даты</param>
        /// <param name="toDate">до какой даты</param>
        /// <param name="info">Метеостанция, с которой загружается ряд</param>
        /// <param name="onPercentChange"></param>
        /// <param name="checkStop"></param>
        /// <returns></returns>
        public RawRange GetRange(DateTime fromDate, DateTime toDate, RP5MeteostationInfo info, Action<double> onPercentChange = null, Func<bool> checkStop = null)
        {
            if (toDate < fromDate)
                throw new WindEnergyException("Даты указаны неверно");

            switch (Vars.Options.RP5SourceEngine)
            {
                case RP5SourceType.LocalDBSearch:
                    return Vars.RP5Database.GetRange(fromDate, toDate, info, onPercentChange, checkStop);
                case RP5SourceType.OnlineAPI:
                    if (toDate - fromDate > TimeSpan.FromDays(365 * LOAD_STEP_YEARS)) // если надо скачать больше трёх лет, то скачиваем по частям
                    {
                        TimeSpan span = toDate - fromDate;

                        RawRange res1 = new RawRange();
                        DateTime dt;
                        int i = 0;
                        int total = (int)(span.TotalDays / (365 * LOAD_STEP_YEARS)) + 1;
                        for (dt = fromDate; dt <= toDate; dt += TimeSpan.FromDays(365 * LOAD_STEP_YEARS))
                        {
                            if (checkStop != null)
                                if (checkStop.Invoke())
                                    break;

                            if (onPercentChange != null)
                            {
                                double pc = ((i / (double)total) * 100d);
                                onPercentChange.Invoke(pc);
                            }
                            RawRange r = GetRange(dt, dt + TimeSpan.FromDays(365 * LOAD_STEP_YEARS), info, onPercentChange, checkStop);
                            res1.Add(r);
                            res1.Name = r.Name;
                            res1.Position = r.Position;
                            res1.Meteostation = r.Meteostation;
                            i++;
                        }
                        //DateTime fr = dt - TimeSpan.FromDays(365 * LOAD_STEP_YEARS);
                        //RawRange r1 = GetRange(fr, toDate, info);
                        return res1;
                    }

                    #region отправка статистики загрузки

                    //string dataS, linkS;
                    //switch (info.MeteoSourceType)
                    //{
                    //    case MeteoSourceType.Airport:
                    //        dataS = "cc_id={0}&cc_str={1}&stat_p=1&s_date1={2}&s_ed3={4}&s_ed4={4}&s_ed5={5}&s_date2={3}&s_ed9=0&s_ed10=-1&s_pe=1&lng_id=2&s_dtimehi=-Период---";
                    //        linkS = "https://rp5.ru/responses/reStatistMetar.php";
                    //        dataS = string.Format(dataS,
                    //            info.ID, //cc_id
                    //            info.CC_Code, //cc_str
                    //            fromDate.Date.ToString("dd.MM.yyyy"), //from
                    //            toDate.Date.ToString("dd.MM.yyyy"), //to
                    //            DateTime.Now.Month, //f_ed3 - только месяц
                    //            DateTime.Now.Day //f_ed5 - только день
                    //        );
                    //        break;
                    //    case MeteoSourceType.Meteostation:
                    //        dataS = "wmo_id={0}&stat_p=1&s_date1={1}&s_ed3={3}&s_ed4={3}&s_ed5={4}&s_date2={2}&s_ed9=0&s_ed10=-1&s_pe=1&lng_id=2&s_dtimehi=-срок---";
                    //        linkS = "https://rp5.ru/responses/reStatistSynop.php";
                    //        dataS = string.Format(dataS,
                    //            info.ID, //wmo_id
                    //            fromDate.Date.ToString("dd.MM.yyyy"), //from
                    //            toDate.Date.ToString("dd.MM.yyyy"), //to
                    //            DateTime.Now.Month, //f_ed3 - только месяц
                    //            DateTime.Now.Day //f_ed5 - только день
                    //        );
                    //        break;
                    //    default: throw new Exception("Этот тип метеостанций не реализован");
                    //}
                    // string strS = this.SendStringPostRequest(linkS, dataS, referer: "https://rp5.ru/", cookies: this.CookieData, customHeaders: this.Headers);

                    #endregion

                    #region получение ссылки на файл
                    string data, link;
                    //получение ссылки на файл
                    switch (info.MeteoSourceType)
                    {
                        case MeteoSourceType.Airport:
                            data = "metar={0}&a_date1={1}&a_date2={2}&f_ed3={3}&f_ed4={4}&f_ed5={5}&f_pe={6}&f_pe1={7}&lng_id=2";
                            link = "https://rp5.ru/responses/reFileMetar.php";
                            break;
                        case MeteoSourceType.Meteostation:
                            data = "wmo_id={0}&a_date1={1}&a_date2={2}&f_ed3={3}&f_ed4={4}&f_ed5={5}&f_pe={6}&f_pe1={7}&lng_id=2";
                            link = "https://rp5.ru/responses/reFileSynop.php";
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
                        2 //f_pe1- кодировка (1 - ansi, 2 - utf8, 3 - Unicode)
                        );

                    string str = this.SendStringPostRequest(link, data, referer: "https://rp5.ru/", cookies: this.CookieData, customHeaders: this.Headers);


                    //ОШИБКИ rp5.ru
                    //запросы к reFileSynop.php
                    //FS004 несуществующий wmo_id
                    //FS002 ошибки в исходных данных (параметрах запроса)
                    //FS000 Ошибка авторизации 
                    //FS001-
                    //запросы к reStatistSynop.php
                    //S000 Ошибка авторизации
                    //FM000 Время жизни статистики истекло для этой сессии
                    if (str.Contains("FS004"))
                        throw new WindEnergyException("Для этого id нет архива погоды", ErrorReason.FS004);
                    if (str.Contains("FS002"))
                        throw new WindEnergyException("Ошибка в исходных данных", ErrorReason.FS002);
                    if (str.Contains("FS000"))
                        throw new WindEnergyException("Ошибка авторизации", ErrorReason.FS000);
                    if (str.Contains("FS001-"))
                        throw new WindEnergyException("Неправильный метод запроса. Ожидается POST", ErrorReason.FS001);
                    if (str.Contains("FM000"))
                        throw new WindEnergyException("Время жизни статистики истекло для этой сессии", ErrorReason.FM000);
                    if (str.Contains("FM004"))
                        throw new WindEnergyException("Внутренняя ошибка. Архив недоступен или не существует", ErrorReason.FM004);
                    int start = str.IndexOf("href=") + 5;
                    str = str.Substring(start);
                    int end = str.IndexOf(">");
                    string lnk = str.Substring(0, end);

                    #endregion

                    #region  загрузка файла с сервера

                    string tmp_dl_file = Vars.LocalFileSystem.GetTempFileName();
                    WebClient webcl = new WebClient();
                    webcl.DownloadFile(lnk, tmp_dl_file);
                    webcl.Dispose();

                    //распаковка
                    string tmp_unpack_file = tmp_dl_file + ".csv";
                    LocalFileSystem.UnGZip(tmp_dl_file, tmp_unpack_file);

                    //открытие файла
                    RawRange res = LoadCSV(tmp_unpack_file, info);

                    res = new RawRange(res.OrderBy(x => x.Date).ToList());
                    res.Name = info.Name;
                    res.Position = info.Position;
                    res.Meteostation = info;
                    new Task(() => Vars.RP5Meteostations.TryAddMeteostation(info)).Start(); //если такой метеостанции нет в БД, то добавляем
                    return res;

                #endregion

                default: throw new Exception("Этот тип БД не реализован");
            }
        }

        /// <summary>
        /// найти ближайшие метеостанции к выбранной точке прогноза погоды
        /// </summary>
        /// <param name="wmoInfo">информация о точке с погодой</param>
        /// <param name="loadExtInfo">загружать доп информацию о метеостанции (id, дата начала наблюдения)</param>
        /// <param name="forceDisableCache">откюлчение кэша</param>
        /// <returns></returns>
        public List<RP5MeteostationInfo> GetMeteostationsAtPoint(WmoInfo wmoInfo, bool loadExtInfo = true, bool forceDisableCache = false)
        {
            //страница погоды в заданной точке
            HtmlDocument point_page = SendHtmlGetRequest(wmoInfo.Link, out HttpStatusCode code, forceDisableCache: forceDisableCache);

            if (code != HttpStatusCode.OK)
                throw new Exception("При загрузке страницы произошла ошибка " + code.ToString());

            //поиск ссылок на метеостанции
            Dictionary<string, IEnumerable<HtmlNode>> archives = new Dictionary<string, IEnumerable<HtmlNode>>();
            List<RP5MeteostationInfo> res = new List<RP5MeteostationInfo>();
            Dictionary<string, string> selectors = new Dictionary<string, string>()  {
                { "a[id='archive_link']","list"},//архив погоды на метеостанции (старый вариант страницы)
                { "div.ArchiveInfo a.ArchiveStrLink" ,"block"},//архив погоды на метеостанции (новый вариант страницы)
                { "a[id='wug_link']" ,"list"}, //на неофициальной метеостанции (этот архив пока что качать нельзя, но селектор оставим)
                { "a[id='metar_link']" ,"list"} //аэропорт 
            };
            foreach (string i in selectors.Keys)
            {
                var a = point_page.DocumentNode.QuerySelectorAll(i); //получаем все теги по такому селекторы
                if (a != null && a.Count() > 0) //если что-то нашлось, то парсим дальше
                {
                    foreach (HtmlNode some_link in a)
                    {
                        RP5MeteostationInfo nm = new RP5MeteostationInfo();
                        nm.Name = wmoInfo.name; //искомая точка
                        nm.Link = some_link.Attributes["href"].Value; //ссылка на страницу

                        //дальнейшее зависит от типа блока
                        switch (selectors[i])
                        {
                            case "list": //СТАРЫЕ ВАРИАНТЫ БЛОКОВ ССЫЛОК НА МЕТЕОСТАНЦИИ И АЭРОПОРТЫ
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
                                    nm.OwnerDistance = double.Parse(dist.Replace('.', Constants.DecimalSeparator));
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
                                                  //nm.MeteoSourceType = MeteoSourceType.UnofficialMeteostation;
                                                  //break;
                                    default: throw new Exception("Этот тип метеостанции не реализован");
                                }
                                break;


                            case "block": //НОВЫЙ ТИП ССЫЛОК В ВИДЕ БЛОКА С ИНФОРМАЦИЕЙ
                                //название
                                onmouseover = some_link.Attributes["onmouseover"].Value; //значение атрибута вывода подсказки
                                nmm = onmouseover.Replace("tooltip(this, '", "").Replace("' , 'hint')", "");
                                nm.Name = nmm;

                                //расстояние до источника не указывается, значит 0
                                nm.OwnerDistance = 0;

                                //пока в таких блоках только метеостанции
                                nm.MeteoSourceType = MeteoSourceType.Meteostation;

                                break;
                            default: throw new Exception("Этот тип блока ещё не реализован");
                        }




                        if (loadExtInfo)
                            try
                            {
                                GetMeteostationExtInfo(ref nm); //запись информации об id метеостанции, дате начала наблюдений
                            }
                            catch (Exception) { continue; }
                        if (nm != null)
                            res.Add(nm);

                    }



                }
            }
            return res;
        }

        /// <summary>
        /// получить из страницы архива погоды id метеостанции, дату начала наблюдений. Двнные запишутся в структуру info, где уже должна быть записана ссылка на страницу
        /// </summary>
        /// <returns></returns>
        public void GetMeteostationExtInfo(ref RP5MeteostationInfo info)
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
                //символьный код аэропорта
                HtmlNode cc_code_node = page.GetElementbyId("cc_str");
                string cc_code = cc_code_node.Attributes["value"].Value.ToString();
                info.CC_Code = cc_code;

                //id в системе рп5
                string pg1 = page.Text;
                int start1 = pg1.IndexOf("fFileMetarGet(");
                int end1 = pg1.IndexOf(')', start1);
                if (start1 == -1 || end1 == -1)
                    throw new Exception("Что-то не так");
                start1 += +"fFileMetarGet(1317900300".Length;
                string id = pg1.Substring(start1 + 1, end1 - 1 - start1);
                info.ID = id;
                info.MeteoSourceType = MeteoSourceType.Airport;
            }
            else if (wug != null) //неофициальная метеостанция
            {
                info = null;
                return;
            }
            else throw new WindEnergyException("Для этой метеостанции невозможно получить данные");


            //получение даты начала наблюдений
            string pg = page.Text;
            int start = pg.IndexOf("наблюдения с ");
            int end = pg.IndexOf("</span>", start);
            if (start == -1 || end == -1)
                throw new Exception("Что-то не так");
            start += "наблюдения с ".Length;
            string fdate = pg.Substring(start, end - 1 - start);
            DateTime mon_from = DateTime.Parse(fdate);
            info.MonitoringFrom = mon_from;

            //получение координат метеостанции
            string pg2 = page.Text;
            int start2 = pg2.IndexOf("show_map(");
            int end2 = pg2.IndexOf(", 9);", start2);
            if (start2 == -1 || end2 == -1)
                throw new Exception("Что-то не так");
            start2 += "show_map(".Length;
            string coordinates = pg2.Substring(start2, end2 - 1 - start2);
            //55.833333333333, 37.616666666667
            string[] ar = coordinates.Split(',');
            double lat = double.Parse(ar[0].Trim().Replace('.', Constants.DecimalSeparator));
            double lon = double.Parse(ar[1].Trim().Replace('.', Constants.DecimalSeparator));
            info.Position = new PointLatLng(lat, lon);

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
            long timestamp = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            //query = HttpUtility.UrlEncode(query);
            url = string.Format(url, query, timestamp);

            JToken ans = SendJsonGetRequest(url, out HttpStatusCode code, true, "https://rp5.ru/", cookies: this.CookieData);
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
        public static WindDirections16 GetWindDirectionFromString(string direction)
        {
            switch (direction.ToLower())
            {
                case "штиль, безветрие":
                    return WindDirections16.Calm;
                case "ветер, дующий с севера":
                    return WindDirections16.N;
                case "ветер, дующий с северо-северо-востока":
                    return WindDirections16.NNE;
                case "ветер, дующий с северо-востока":
                    return WindDirections16.NE;
                case "ветер, дующий с востоко-северо-востока":
                    return WindDirections16.NEE;
                case "ветер, дующий с востока":
                    return WindDirections16.E;
                case "ветер, дующий с востоко-юго-востока":
                    return WindDirections16.SEE;
                case "ветер, дующий с юго-востока":
                    return WindDirections16.SE;
                case "ветер, дующий с юго-юго-востока":
                    return WindDirections16.SSE;
                case "ветер, дующий с юга":
                    return WindDirections16.S;
                case "ветер, дующий с юго-юго-запада":
                    return WindDirections16.SSW;
                case "ветер, дующий с юго-запада":
                    return WindDirections16.SW;
                case "ветер, дующий с западо-юго-запада":
                    return WindDirections16.SWW;
                case "ветер, дующий с запада":
                    return WindDirections16.W;
                case "ветер, дующий с западо-северо-запада":
                    return WindDirections16.NWW;
                case "ветер, дующий с северо-запада":
                    return WindDirections16.NW;
                case "ветер, дующий с северо-северо-запада":
                    return WindDirections16.NNW;
                case "переменное направление":
                    return WindDirections16.Variable;
                default: throw new Exception("Это направление ветра не реализовано");
            }
        }

        /// <summary>
        /// получить строку по направлению ветра
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static string GetStringFromWindDirection(WindDirections16 direction)
        {
            switch (direction)
            {
                case WindDirections16.N:
                    return "ветер, дующий с севера";
                case WindDirections16.NNE:
                    return "ветер, дующий с северо-северо-востока";
                case WindDirections16.NE:
                    return "ветер, дующий с северо-востока";
                case WindDirections16.NEE:
                    return "ветер, дующий с востоко-северо-востока";
                case WindDirections16.E:
                    return "ветер, дующий с востока";
                case WindDirections16.SEE:
                    return "ветер, дующий с востоко-юго-востока";
                case WindDirections16.SE:
                    return "ветер, дующий с юго-востока";
                case WindDirections16.SSE:
                    return "ветер, дующий с юго-юго-востока";
                case WindDirections16.S:
                    return "ветер, дующий с юга";
                case WindDirections16.SSW:
                    return "ветер, дующий с юго-юго-запада";
                case WindDirections16.SW:
                    return "ветер, дующий с юго-запада";
                case WindDirections16.SWW:
                    return "ветер, дующий с западо-юго-запада";
                case WindDirections16.W:
                    return "ветер, дующий с запада";
                case WindDirections16.NWW:
                    return "ветер, дующий с западо-северо-запада";
                case WindDirections16.NW:
                    return "ветер, дующий с северо-запада";
                case WindDirections16.NNW:
                    return "ветер, дующий с северо-северо-запада";
                case WindDirections16.Variable:
                    return "переменное направление";
                default: throw new Exception("Это направление ветра не реализовано");
            }
        }

        /// <summary>
        /// Загрузить ряд из файла csv, полученного с сайта
        /// </summary>
        /// <param name="file">файл csv</param>
        /// <param name="meteostation">Привязка к метеостанции. Если null, то будет найдена из БД по ID из заголовка</param>
        /// <returns></returns>
        public static RawRange LoadCSV(string file, RP5MeteostationInfo meteostation = null)
        {
            using (StreamReader sr = new StreamReader(file, Encoding.UTF8, true))
            {
                RawRange res = new RawRange();
                res.BeginChange(); //приостановка обработки событий изменения ряда

                //определение формата файла csv
                MeteoSourceType type;
                string title = sr.ReadLine();
                if (title.Contains("WMO_ID"))
                    type = MeteoSourceType.Meteostation;
                else if (title.Contains("METAR"))
                    type = MeteoSourceType.Airport;
                else throw new Exception("Файл повреждён или имеет неизвестный формат данных rp5.ru");

                //пропуск пустых строк (одна уже пропущена при чтении заголовка)
                for (int i = 0; i < 6; i++)
                    sr.ReadLine();
                switch (type)
                {
                    case MeteoSourceType.Meteostation: //загрузка архива с метеостанции

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

                            double temp = elems[1] == "" ? double.NaN : double.Parse(elems[1].Replace('.', Constants.DecimalSeparator));
                            DateTime dt = DateTime.Parse(elems[0]);
                            double spd = double.Parse(elems[7].Replace('.', Constants.DecimalSeparator));
                            double wet = elems[5] == "" ? double.NaN : double.Parse(elems[5].Replace('.', Constants.DecimalSeparator));
                            double press = elems[2] == "" ? double.NaN : double.Parse(elems[2].Replace('.', Constants.DecimalSeparator));
                            string dirs = elems[6];
                            WindDirections16 direct = GetWindDirectionFromString(dirs);
                            try
                            { res.Add(new RawItem() { Date = dt, DirectionRhumb = direct, Speed = spd, Temperature = temp, Wetness = wet, Pressure = press }); }
                            catch (Exception)
                            { continue; }
                        }

                        //поиск информации о МС
                        if (meteostation == null)
                        {
                            int start = title.IndexOf("WMO_ID=") + "WMO_ID=".Length;
                            int end = title.IndexOf(',', start);
                            string id_s = title.Substring(start, end - start);
                            meteostation = Vars.RP5Meteostations.GetByID(id_s);
                        }
                        break;
                    case MeteoSourceType.Airport: //загрузка архива с аэропорта

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

                            double temp = elems[1] == "" ? double.NaN : double.Parse(elems[1].Replace('.', Constants.DecimalSeparator));
                            DateTime dt = DateTime.Parse(elems[0]);
                            double spd = double.Parse(elems[6].Replace('.', Constants.DecimalSeparator));
                            double wet = elems[4] == "" ? double.NaN : double.Parse(elems[4].Replace('.', Constants.DecimalSeparator));
                            double press = elems[2] == "" ? double.NaN : double.Parse(elems[2].Replace('.', Constants.DecimalSeparator));
                            string dirs = elems[5];
                            WindDirections16 direct = RP5ru.GetWindDirectionFromString(dirs);
                            res.Add(new RawItem() { Date = dt, DirectionRhumb = direct, Speed = spd, Temperature = temp, Wetness = wet, Pressure = press });
                        }
                        //поиск информации о МС
                        if (meteostation == null)
                        {
                            int start = title.IndexOf("METAR=") + "METAR=".Length;
                            int end = title.IndexOf(',', start);
                            string id_s = title.Substring(start, end - start);
                            meteostation = Vars.RP5Meteostations.GetByCC_code(id_s);
                        }
                        break;
                    case MeteoSourceType.UnofficialMeteostation:
                        throw new Exception("Этот тип файла не поддерживается");
                }

                res.Meteostation = meteostation;
                res.EndChange();
                return res;
            }
        }

    }
}
