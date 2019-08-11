﻿using System;
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
using WindEnergy.Lib.Classes;

namespace WindEnergy.Lib.Data.Providers.InternetServices
{
    /// <summary>
    /// Взаимодействие с сайтом TrackConverter
    /// </summary>
    public class Velomapa : BaseConnection
    {
        public Velomapa() : base(Vars.Options.SiteAddress,null) { }

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

        public override TimeSpan SessionLifetime { get { return TimeSpan.FromDays(30); } }

        /// <summary>
        /// отправить отчет о запуске программы
        /// </summary>
        /// <param name="guid">guid экземпляра</param>
        private void AttachGuid(string guid)
        {
            string site = Vars.Options.SiteAddress;
            string ver = "WindEnergy " + Vars.Options.VersionText;
            string url = string.Format("{0}/receiver.php?mode=attach&program_guid={1}&version={2}", site, guid, ver);
            string ans = this.SendStringGetRequest(url, false);
            if (ans != "OK")
                throw new WebException(ans);
        }

        /// <summary>
        /// узнать последнюю версию на сайте
        /// </summary>
        /// <returns></returns>
        public VersionInfo GetVersion()
        {
            string site = Vars.Options.SiteAddress;
            string url = string.Format("{0}/receiver.php?mode=version&owner_version={1}&program=windenergy", site, Vars.Options.VersionInt);
            JObject jobj = SendJsonGetRequest(url);
            int version_int = int.Parse(jobj["version_int"].ToString());
            string version_text = jobj["version_text"].ToString();
            string chang = jobj["changes"].ToString().Replace("\n", "\r\n");
            string date = jobj["release_date"].ToString();
            string dl_link = jobj["download_link"].ToString();
            DateTime Date = DateTime.Parse(date);
            return new VersionInfo() { VersionInt = version_int, Changes = chang, ReleaseDate = Date, VersionText = version_text, DownloadLink = dl_link };
        }

        /// <summary>
        /// узнать последнюю версию на сайте
        /// </summary>
        /// <returns></returns>
        public void GetVersionAsync(Action<VersionInfo> after_action)
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
                        VersionInfo actVer = GetVersion();
                        after_action.Invoke(actVer);
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

        /// <summary>
        /// отправить статистику на сервер
        /// </summary>
        public void SendStatisticAsync()
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
                        AttachGuid(guid);
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