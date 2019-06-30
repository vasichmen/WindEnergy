﻿using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace WindEnergy.Lib.Data.Providers
{
    /// <summary>
    /// базовый класс HTTP запросов к серверу
    /// </summary>
    public abstract class BaseConnection
    {
        /// <summary>
        /// ID PHP сессии для этого экземпляра для заданного host. Если хост не установил заголовок PHPSESSID, то значение равно null
        /// </summary>
        public string PHPSESSID
        {
            get
            {
                //return "08dbd7ed81e0d8bd918cbc23499ce5a3";
                if (_PHPSESSID == null || _SESSID_date == DateTime.MinValue || (DateTime.Now- _SESSID_date) < this.SessionLifetime)
                {
                    _PHPSESSID = GetPHPSessionID(host);
                }
                return _PHPSESSID;
            }
        }
        private string _PHPSESSID = null;
        private DateTime _SESSID_date = DateTime.MinValue;

        /// <summary>
        /// создает новый экземпляр класса Base Connection
        /// </summary>
        private BaseConnection()
        {
            lastQuery = DateTime.MinValue;
        }

        /// <summary>
        /// создаёт новый объект с кэшем в указанной папке и заданной длительностью хранения
        /// </summary>
        /// <param name="cacheDirectory">папка с кэшем или null, если не надо использоать кэш</param>
        /// <param name="duration">длительность хранения в часах. По умолчанию - неделя</param>
        public BaseConnection(string host, string cacheDirectory, double duration = 7 * 24) : this()
        {
            this.host = host;
            useCache = cacheDirectory != null;
            if (useCache)
            {
                this.cacheDirectory = cacheDirectory;
                this.duration = duration;
                this.cache = new FileSystemCache(cacheDirectory, TimeSpan.FromHours(duration));
            }
            else
            {

            }
        }

        /// <summary>
        /// время последнего запроса к сервису
        /// </summary>
        private DateTime lastQuery;
        private readonly string host;
        private readonly bool useCache;
        private readonly string cacheDirectory;
        private readonly double duration;
        public static bool UseProxy = false;
        private readonly string ProxyHost = "127.0.0.1";
        private readonly int ProxyPort = 8118;
        private FileSystemCache cache;

        /// <summary>
        /// загрузка изображения по заданной ссылке
        /// </summary>
        /// <param name="url"></param>
        /// <param name="operation">метод установки прогресса загрузки файла</param>
        /// <param name="afterLoadComplete">действие, выполняемое по окончании загрузки файла</param>
        /// <returns></returns>
        public static void GetFileAsync(string url, Action<string> operation = null, Action<string> afterLoadComplete = null)
        {
            int i = 0;
            string tmp_file = Vars.Options.TempFolder + "\\" + i + ".tmp";
            Directory.CreateDirectory(Path.GetDirectoryName(tmp_file));
            while (File.Exists(tmp_file))
                tmp_file = Vars.Options.TempFolder + "\\" + ++i + ".tmp";

            WebClient client = new WebClient();
            client.DownloadProgressChanged +=
                new DownloadProgressChangedEventHandler((sender, e) =>
                {
                    if (operation != null)
                    {
                        operation.Invoke("Загрузка изображения, завершено " + (e as DownloadProgressChangedEventArgs).ProgressPercentage + "%");
                    }
                }
                );
            client.DownloadFileCompleted += new AsyncCompletedEventHandler((sender, e) =>
            {
                if (afterLoadComplete != null)
                    afterLoadComplete.Invoke(tmp_file);
                client.Dispose();
            });
            client.DownloadFileAsync(new Uri(url), tmp_file);
        }

        /// <summary>
        /// получить изображение по ссылке
        /// </summary>
        /// <param name="url">ссылка на изображение</param>
        /// <returns></returns>
        public static Image GetImage(string url)
        {
            WebClient wc = new WebClient();
            try
            {
                Stream str = wc.OpenRead(url);
                if (wc.ResponseHeaders[HttpResponseHeader.ContentLength] == "0")
                    return new Bitmap(256, 256);
                Image res = Image.FromStream(str);
                str.Close();
                wc.Dispose();
                return res;
            }
            catch (WebException ex)
            {
                Stream resp = ex.Response.GetResponseStream();
                StreamReader sr = new StreamReader(resp);
                string ans = sr.ReadToEnd();
                sr.Close();
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    throw new ApplicationException(ans, ex);
                else
                    return new Bitmap(256, 256);
            }
        }

        /// <summary>
        /// Минимальное время между запросами к серверу. Значение по умолчанию 200 мс.
        /// Если время между запросами не прошло, SendStringRequest и SendXmlRequest будут ждать
        /// </summary>
        public abstract TimeSpan MinQueryInterval { get; }

        /// <summary>
        /// Максимальное число попыток переподключения
        /// </summary>
        public abstract int MaxAttempts { get; }

        /// <summary>
        /// Время жизни сессии
        /// </summary>
        public abstract TimeSpan SessionLifetime { get;  }

        /// <summary>
        /// строка UserAgent 
        /// </summary>
        public string UserAgent
        {
            get
            {
                if (string.IsNullOrEmpty(userAgent))
                    userAgent = getRandomUserAgent();
                return userAgent;
            }
        }
                       private string userAgent = null;

        /// <summary>
        /// отправка запроса с результатом в виде xml
        /// </summary>
        /// <param name="url">запрос</param>
        /// <exception cref="Exception">Если произошла ошибка при подключении</exception>
        /// <returns></returns>
        protected XmlDocument SendXmlGetRequest(string url)
        {
            XmlDocument res = new XmlDocument();
            string ans = SendStringGetRequest(url);
            res.LoadXml(ans);
            return res;
        }

        /// <summary>
        /// отправить запрос с результатом в виде HTML документа
        /// </summary>
        /// <param name="url">url запроса</param>
        /// <param name="code">код ошибки</param>
        /// <returns></returns>
        protected HtmlDocument SendHtmlGetRequest(string url, out HttpStatusCode code)
        {
            string ans = SendStringGetRequest(url, out code);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(ans);

            return doc;
        }

        /// <summary>
        /// отправка запроса POST с ответом в формате HTML
        /// </summary>
        /// <param name="url">url запроса</param>
        /// <param name="data">данные POST  запроса</param>
        /// <returns></returns>
        protected HtmlDocument SendHtmlPostRequest(string url, string data, string referer = "")
        {
            string ans = SendStringPostRequest(url, data, referer);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(ans);

            return doc;
        }

        /// <summary>
        /// отправка POST запроса с ответом в формате JSON
        /// </summary>
        /// <param name="url">адрес</param>
        /// <param name="data">данные</param>
        /// <returns></returns>
        protected JObject SendJsonPostRequest(string url, string data, string referer = "")
        {
            JObject jobj;
            string json = SendStringPostRequest(url, data, referer);
            if (string.IsNullOrWhiteSpace(json))
                throw new Exception("Что-то не так с запросом - пустой ответ сервера");
            json = json.Substring(json.IndexOf('{'));
            json = json.TrimEnd(new char[] { ';', ')' });
            try
            { jobj = JObject.Parse(json); }
            catch (Exception ex) { throw new ApplicationException("Ошибка в парсере JSON. Сервер вернул некорректный объект.", ex); }
            return jobj;
        }

        /// <summary>
        /// отправка запроса с результатом в виде строки
        /// </summary>
        /// <param name="url">запрос</param>
        /// <returns></returns>
        /// <exception cref="WebException">Если произошла ошибка при подключении</exception>
        protected string SendStringGetRequest(string url, bool useGZip = true)
        {
            string ans = SendStringGetRequest(url, out HttpStatusCode code, useGZip);
            return ans;
        }


        /// <summary>
        /// отправка запроса с результатом в виде объекта JSON
        /// </summary>
        /// <param name="url">запрос</param>
        /// <returns></returns>
        /// <exception cref="WebException">Если произошла ошибка при подключении</exception>
        protected JObject SendJsonGetRequest(string url, bool gzip = true, string referer = "", string contentType = "application/json", string cookies = null)
        {
            JObject jobj;
            string json = SendStringGetRequest(url, out HttpStatusCode code, gzip, referer, contentType, "XMLHttpRequest", cookies);
            try
            {
                if (json == "")
                    return null;
                if (json[0] != '[')
                {
                    json = json.Substring(json.IndexOf('{'));
                    json = json.TrimEnd(new char[] { ';', ')' });
                }
                else
                    json = "{ \"array\": " + json + "}";
                try
                { jobj = JObject.Parse(json); }
                catch (Exception ex) { throw new ApplicationException("Ошибка в парсере JSON. Сервер вернул некорректный объект.", ex); }
            }
            catch (Exception exx)
            { throw new ApplicationException("Ошибка в парсере JSON. Сервер вернул некорректный объект: \r\n" + json, exx); }
            return jobj;
        }


        #region Базовые методы GET и POST
        /// <summary>
        /// отправка POST запроса. Бросает ApplicationException при ошибке 500 (внутренняя ошибка сервера)
        /// </summary>
        /// <param name="url">адрес</param>
        /// <param name="data">данные</param>
        /// <returns></returns>
        protected string SendStringPostRequest(string url, string data, string referer = null, string cookies = null, string customHeaders = null)
        {
            try
            {
                //ожидание времени интервала между запросами
                while (DateTime.Now - lastQuery < MinQueryInterval)
                    Thread.Sleep(50);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                if (UseProxy)
                {
                    WebProxy wp = new WebProxy(ProxyHost, ProxyPort);
                    req.Proxy = wp;
                }

                if (!string.IsNullOrWhiteSpace(cookies))
                {
                    string domain = new Uri(url).Host;
                    req.CookieContainer = new CookieContainer();
                    req.CookieContainer.Add(parseCookieString(cookies, domain));
                }

                if (!string.IsNullOrWhiteSpace(customHeaders))
                {
                    req.Headers.Add(parseHeadersString(customHeaders));
                }
                req.ContentType = "application/x-www-form-urlencoded";


                req.Method = "POST";
                req.Timeout = 100000;
                req.UserAgent = UserAgent;
                if (!string.IsNullOrWhiteSpace(referer))
                    req.Referer = referer;
                byte[] sentData = Encoding.GetEncoding(1251).GetBytes(data);
                req.ContentLength = sentData.Length;
                Stream sendStream = req.GetRequestStream();
                sendStream.Write(sentData, 0, sentData.Length);
                sendStream.Close();

                WebResponse res = req.GetResponse();
                Stream ReceiveStream = res.GetResponseStream();
                StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
                //Кодировка указывается в зависимости от кодировки ответа сервера
                char[] read = new char[256];
                int count = sr.Read(read, 0, 256);
                string Out = string.Empty;
                while (count > 0)
                {
                    string str = new string(read, 0, count);
                    Out += str;
                    count = sr.Read(read, 0, 256);
                }
                return Out;
            }
            catch (WebException we)
            {
                if ((we.Response as HttpWebResponse).StatusCode == HttpStatusCode.InternalServerError)
                    throw new ApplicationException("Внутрення ошибка сервера", we);
                throw new WebException("Ошибка подключения.\r\n" + url, we);
            }
        }


        /// <summary>
        /// отправка запроса с результатом в виде строки
        /// </summary>
        /// <param name="url">запрос</param>
        /// <param name="code">код ответа сервера</param>
        /// <returns></returns>
        /// <exception cref="WebException">Если произошла ошибка при подключении</exception>
        protected string SendStringGetRequest(string url, out HttpStatusCode code, bool useGZip = true, string referer = null, string contentType = "application/xml",
            string xrequested = null, string cookies = null, string customHeaders = null)
        {
            if (useCache)
                if (cache.ContainsWebUrl(url))
                {
                    code = HttpStatusCode.OK;
                    return cache.GetWebUrl(url);
                }
            try
            {
                //ожидание времени интервала между запросами
                while (DateTime.Now - lastQuery < MinQueryInterval)
                    Thread.Sleep(50);

                //Выполняем запрос к универсальному коду ресурса (URI).

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (UseProxy)
                {
                    WebProxy wp = new WebProxy(ProxyHost, ProxyPort);
                    request.Proxy = wp;
                }
                request.UserAgent = UserAgent;
                request.ContentType = contentType;
                request.Headers[HttpRequestHeader.AcceptLanguage] = "ru - RU,ru; q = 0.8,en - US; q = 0.6,en; q = 0.4";
                request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                if (!string.IsNullOrWhiteSpace(xrequested))
                    request.Headers.Add("X-Requested-With", xrequested);
                if (!string.IsNullOrWhiteSpace(referer))
                    request.Referer = referer;
                if (!string.IsNullOrWhiteSpace(cookies))
                {
                    string domain = new Uri(url).Host;
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(parseCookieString(cookies, domain));
                }

                if (!string.IsNullOrWhiteSpace(customHeaders))
                {
                    request.Headers.Add(parseHeadersString(customHeaders));
                }

                //Получаем ответ от интернет-ресурса. 

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //string lng = response.Headers[HttpRequestHeader.var];
                //Экземпляр класса System.IO.Stream 
                //для чтения данных из интернет-ресурса.
                Stream dataStream = response.GetResponseStream();

                string responsereader;
                try
                {
                    Stream str;
                    if (useGZip)
                        str = new GZipStream(dataStream, CompressionMode.Decompress);
                    else
                        str = dataStream;

                    //Инициализируем новый экземпляр класса 
                    //System.IO.StreamReader для указанного потока.
                    StreamReader sreader = new StreamReader(str, Encoding.UTF8, true);

                    //Считывает поток от текущего положения до конца.            
                    responsereader = sreader.ReadToEnd();
                }
                catch (InvalidDataException)
                {
                    //Инициализируем новый экземпляр класса 
                    //System.IO.StreamReader для указанного потока.
                    StreamReader sreader = new StreamReader(dataStream, Encoding.UTF8, true);

                    //Считывает поток от текущего положения до конца.            
                    responsereader = sreader.ReadToEnd();
                }
                //Закрываем поток ответа.
                response.Close();

                //запоминание времени запроса
                lastQuery = DateTime.Now;

                //код ошибки
                code = response.StatusCode;

                //запись в кэш, если надо
                if (useCache)
                    cache.PutWebUrl(url, responsereader);

                return responsereader;
            }
            catch (WebException we) { throw new WebException("Ошибка подключения.\r\n" + url, we, we.Status, null); }
        }

        #endregion

        #region вспомогательные методы

        /// <summary>
        /// получить ID сессии PHP для заданного хоста
        /// </summary>
        /// <param name="url">адрес хоста</param>
        /// <returns></returns>
        protected string GetPHPSessionID(string url)
        {
            try
            {
                //ожидание времени интервала между запросами
                while (DateTime.Now - lastQuery < MinQueryInterval)
                    Thread.Sleep(50);

                //Выполняем запрос к универсальному коду ресурса (URI).

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (UseProxy)
                {
                    WebProxy wp = new WebProxy(ProxyHost, ProxyPort);
                    request.Proxy = wp;
                }
                request.UserAgent = UserAgent;
                //Получаем ответ от интернет-ресурса. 
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string PHPSESSID = null;
                string set_cookie = response.Headers[HttpResponseHeader.SetCookie];
                response.Close();

                //если вообще есть куки
                if (set_cookie != null)
                {
                    string[] set_cooks = set_cookie.Split(';');

                    //ищем строку с PHPSESSID
                    //PHPSESSID=08dbd7ed81e0d8bd918cbc23499ce5a3; path=/; domain=rp5.ru; Expires=Tue, 19 Jan 2038 03:14:07 GMT;
                    if (set_cooks.Length > 0)
                    {
                        foreach (string c in set_cooks)
                            if (c.Contains("PHPSESSID"))
                            {
                                PHPSESSID = c.Substring(c.IndexOf('=') + 1);

                                return PHPSESSID;
                            }
                    }
                }
                return null;

            }
            catch (WebException we) { throw new WebException("Ошибка подключения.\r\n" + url, we, we.Status, null); }
        }

        /// <summary>
        /// парсит заголовки из формата название:значение;название:значение
        /// </summary>
        /// <param name="customHeaders"></param>
        /// <returns></returns>
        private NameValueCollection parseHeadersString(string customHeaders)
        {
            NameValueCollection res = new NameValueCollection();
            string[] heads = customHeaders.Split(';');
            foreach (string h in heads)
            {
                string[] ar = h.Split(':');
                res.Add(ar[0].Trim(), ar[1].Trim());
            }
            return res;
        }

        /// <summary>
        /// парсит данные Cookie из формата название=значение;название=значение
        /// </summary>
        /// <param name="cookies">строка данных</param>
        /// <param name="domain">доменное имя для этих куки</param>
        /// <returns></returns>
        private CookieCollection parseCookieString(string cookies, string domain)
        {
            CookieCollection res = new CookieCollection();
            string[] cooks = cookies.Split(';');
            foreach (string cok in cooks)
            {
                string[] ar = cok.Split('=');
                res.Add(new Cookie(ar[0].Trim(), ar[1].Trim()) { Domain = domain });
            }
            return res;
        }

        /// <summary>
        /// сгенерировать случайную строку User Agent
        /// </summary>
        /// <returns></returns>
        private string getRandomUserAgent()
        {
            string[] array = new string[] {
               " Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 74.0.3729.169 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; WOW64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 72.0.3626.121 Safari / 537.36 72",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 74.0.3729.157 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 60.0.3112.113 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 60.0.3112.90 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 72.0.3626.121 Safari / 537.36",
                "Mozilla/5.0(X11; Linux x86_64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 44.0.2403.157 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 74.0.3729.169 Safari / 537.36",
                "Mozilla/5.0(Windows NT 5.1) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 46.0.2490.71 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 69.0.3497.100 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 63.0.3239.132 Safari / 537.36",
                "Mozilla/5.0(Windows NT 5.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 60.0.3112.90 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.2; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 60.0.3112.90 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.3; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 60.0.3112.113 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; WOW64) AppleWebKit / 537.1(KHTML, как Gecko) Chrome / 21.0.1180.83 Safari / 537.1",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 67.0.3396.99 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 68.0.3440.106 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 74.0.3729.131 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 72.0.3626.121 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 65.0.3325.181 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 69.0.3497.100 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 64.0.3282.186 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 63.0.3239.132 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 61.0.3163.100 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 58.0.3029.110 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 70.0.3538.102 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 70.0.3538.110 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 67.0.3396.99 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 70.0.3538.77 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; WOW64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 36.0.1985.143 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 57.0.2987.133 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 59.0.3071.115 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 63.0.3239.84 Safari / 537.36",
                "Mozilla/5.0(Windows NT 5.1) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 49.0.2623.112 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.2; WOW64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 42.0.2311.90 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 65.0.3325.181 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 60.0.3112.90 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 74.0.3729.157 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 68.0.3440.106 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 66.0.3359.181 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 41.0.2228.0 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; WOW64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 56.0.2924.87 Safari / 537.36",
                "Mozilla/5.0(X11; OpenBSD i386) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 36.0.1985.125 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 61.0.3163.100 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 70.0.3538.77 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 63.0.3239.84 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 70.0.3538.102 Safari / 537.36",
                "Mozilla/5.0(Windows NT 6.1) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 72.0.3626.121 Safari / 537.36",
                "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, как Gecko) Chrome / 62.0.3202.94 Safari / 537.36",
                "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 YaBrowser/19.6.1.153 Yowser/2.5 Safari/537.36",
                "Mozilla/5.0(Windows NT 6.1; WOW64) AppleWebKit / 537.31(KHTML, как Gecko) Chrome / 26.0.1410.64 Safari / 537.31" };
            int rand = new Random().Next(array.Length - 1);
            return array[rand];
        }

        #endregion
    }
}
