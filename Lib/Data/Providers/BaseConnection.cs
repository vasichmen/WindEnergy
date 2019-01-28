using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

namespace WindEnergy.Lib.Data.Providers
{
    /// <summary>
    /// базовый класс HTTP запросов к серверу
    /// </summary>
    public abstract class BaseConnection
    {
        /// <summary>
        /// создает новый экземпляр класса Base Connection
        /// </summary>
        public BaseConnection()
        {
            lastQuery = DateTime.MinValue;
        }


        /// <summary>
        /// время последнего запроса к сервису
        /// </summary>
        private DateTime lastQuery;
        private readonly bool useCache;
        private readonly string cacheDirectory;
        private readonly double duration;
        public static bool UseProxy = false;
        private readonly string ProxyHost = "127.0.0.1";
        private readonly int ProxyPort = 8118;




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
        protected HtmlDocument SendHtmlPostRequest(string url, string data, string referer)
        {
            string ans = SendStringPostRequest(url, data, null, referer);
            //StreamReader sr = new StreamReader("sr.txt");
            //string ans = sr.ReadToEnd();
            //sr.Close();

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
        protected JObject SendJsonPostRequest(string url, string data, CookieCollection cookies, string referer)
        {
            JObject jobj;
            string json = SendStringPostRequest(url, data, cookies, referer);
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
        /// отправка запроса с результатом в виде строки
        /// </summary>
        /// <param name="url">запрос</param>
        /// <param name="code">код ответа сервера</param>
        /// <returns></returns>
        /// <exception cref="WebException">Если произошла ошибка при подключении</exception>
        protected string SendStringGetRequest(string url, out HttpStatusCode code, bool useGZip = true, string referer = "")
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
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36";
                request.ContentType = "application/xml";
                request.Headers[HttpRequestHeader.AcceptLanguage] = "ru - RU,ru; q = 0.8,en - US; q = 0.6,en; q = 0.4";
                request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                if (referer != "")
                    request.Referer = referer;

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

                return responsereader;
            }
            catch (WebException we) { throw new WebException("Ошибка подключения.\r\n" + url, we, we.Status, null); }
        }

        /// <summary>
        /// отправка запроса с результатом в виде объекта JSON
        /// </summary>
        /// <param name="url">запрос</param>
        /// <returns></returns>
        /// <exception cref="WebException">Если произошла ошибка при подключении</exception>
        protected JObject SendJsonGetRequest(string url, bool gzip = true, string referer = "")
        {
            JObject jobj;
            string json = SendStringGetRequest(url, out HttpStatusCode code, gzip, referer);
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
            return jobj;
        }

        /// <summary>
        /// отправка POST запроса
        /// </summary>
        /// <param name="url">адрес</param>
        /// <param name="data">данные</param>
        /// <returns></returns>
        protected string SendStringPostRequest(string url, string data, CookieCollection cookies, string referer)
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

                if (cookies != null)
                {
                    req.CookieContainer = new CookieContainer();
                    req.CookieContainer.Add(cookies);
                }


                req.Method = "POST";
                req.Timeout = 100000;
                req.ContentType = "application/x-www-form-urlencoded";
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
                req.Referer = referer;
                req.Host = "rp5.ru";
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
            catch (WebException we) { throw new WebException("Ошибка подключения.\r\n" + url, we); }
        }
    }
}
