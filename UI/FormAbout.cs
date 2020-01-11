using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using WindEnergy.UI.Common.Dialogs;
using WindEnergy.Lib.Classes;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.Lib.Data.Providers.InternetServices;

namespace WindEnergy.UI
{
    /// <summary>
    /// окно о программе
    /// </summary>
    internal partial class FormAbout : Form
    {
        /// <summary>
        /// создает новое окно
        /// </summary>
        public FormAbout()
        {
            InitializeComponent();
            this.Text = string.Format("О программе {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = string.Format("Версия {0}", AssemblyVersion);
            this.labelCopyright.Text = "© vasich, "+DateTime.Now.Year;
            this.linkLabelSite.Text = Vars.Options.SiteAddress + @"programs.php?item=windenergy";
            this.linkLabelTlg.Text = "Telegram: @vasichmen";
            this.linkLabelGithub.Text = "Репозиторий GitHub";
            this.textBoxDescription.Text = AssemblyDescription;
        }

        #region Методы доступа к атрибутам сборки

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (!string.IsNullOrEmpty (titleAttribute.Title))
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        /// <summary>
        /// закрывание окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// переход на сайт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = Process.Start(Vars.Options.SiteAddress + @"programs.php?item=windenergy");
        }

        /// <summary>
        /// прверка новой версии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Velomapa site = new Velomapa();
                VersionInfo vi = site.GetVersion();
                //действие при проверке версии

                float curVer = Vars.Options.VersionInt;
                if (vi.VersionInt > curVer)
                {
                    FormUpdateDialog fud = new FormUpdateDialog(vi);
                    _ = this.Invoke(new Action(() => fud.ShowDialog()));
                }
                else
                    _ = MessageBox.Show(this, "Обновлений нет!", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (WebException we)
            {
                _ = MessageBox.Show(this, "Ошибка подключения!\r\n" + we.Message, "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// нажатие на ссылку связи в телеграме
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelTlg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            _ = Process.Start(Vars.Options.TelegramAddress);
        }

        /// <summary>
        /// нажатие ссылки на гитхаб
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = Process.Start(Vars.Options.GitHubRepository);
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
