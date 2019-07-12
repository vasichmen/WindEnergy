using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace WindEnergy.Lib.Data.Providers
{
    /// <summary>
    /// Взаимодействие с сайтом TrackConverter
    /// </summary>
    public class Velomapa : BaseConnection
    {
        public Velomapa() : base("http://velomapa.ru", null) { }

        /// <summary>
        /// минимальное время между запросами
        /// </summary>
        public override TimeSpan MinQueryInterval
        {
            get
            {
                return TimeSpan.FromSeconds(0.01);
            }
        }

        /// <summary>
        /// максимальное число попыток подключения
        /// </summary>
        public override int MaxAttempts
        {
            get
            {
                return 5;
            }
        }

        public override TimeSpan SessionLifetime { get { return TimeSpan.FromMinutes(10); } }

        /// <summary>
        /// отправить отчет о запуске программы
        /// </summary>
        /// <param name="guid">guid экземпляра</param>
        private void AttachGuid(string guid,string ver)
        {
            string site = Vars.Options.SiteAddress;
            string url = string.Format("{0}/receiver.php?mode=attach&program_guid={1}&version={2}", site, guid,ver);
            string ans = this.SendStringGetRequest(url,false);
            if (ans != "OK")
                throw new WebException(ans);
        }
        
        /// <summary>
        /// отправить статистику на сервер
        /// </summary>
        public void SendStatisticAsync(string ver)
        {
            Action act = new Action(() =>
            {
                bool f = true;
                int i = 0;
                while (f && i < 3)
                {
                    try
                    {
                        i++;
                        string guid = Vars.Options.ApplicationGuid;
                        AttachGuid(guid,ver);
                        f = false;
                    }
                    catch (WebException)
                    {
                        f = true;
                        Thread.Sleep(2000);
                    }
                }


            });
            Task ts = new Task(act);
            ts.Start();
        }
    }
}
