using CommonLib;
using CommonLib.Classes;
using SolarLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolarEnergy.UI.Common.Dialogs
{
    /// <summary>
    /// Окно информации о новой версии
    /// </summary>
    public partial class FormUpdateDialog : Form
    {
        private VersionInfo vi;

        private FormUpdateDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// создает окно новой версии с указанной информацией
        /// </summary>
        /// <param name="vi">Информация о новой версии</param>
        public FormUpdateDialog(VersionInfo vi) : this()
        {
            this.vi = vi ?? throw new ArgumentNullException(nameof(vi));
            Text = "Доступна новая версия " + vi.VersionText;
            labelСur.Text = "Текущая версия: " + Vars.Options.VersionText;
            labelNew.Text = "Новая версия: " + vi.VersionText;
            textBoxChanges.Text = vi.Changes + "\r\nДата: " + vi.ReleaseDate.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// переход на сайт для обновления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (checkBoxRemember.Checked)
                Vars.Options.UpdateMode = UpdateDialogAnswer.AlwaysAccept;
            if (File.Exists(Application.StartupPath + "\\Updater.exe"))
                _ = Process.Start(Application.StartupPath + "\\Updater.exe", "\"" + Vars.Options.SiteAddress + vi.DownloadLink + "\"");
            else
            {
                _ = Process.Start(Vars.Options.SiteAddress + vi.DownloadLink);
                this.Close();
            }
        }

        /// <summary>
        /// отмена обновления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonIgnore_Click(object sender, EventArgs e)
        {
            if (checkBoxRemember.Checked)
                Vars.Options.UpdateMode = UpdateDialogAnswer.AlwaysIgnore;
            Close();
        }
    }
}
