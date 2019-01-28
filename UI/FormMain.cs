﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib;
using WindEnergy.Lib.Data;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.UI.Ext;
using WindEnergy.UI.Helpers;

namespace WindEnergy.UI
{
    /// <summary>
    /// главное окно программы
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// вспомогательные методы интерфейса
        /// </summary>
        public MainHelper mainHelper;

        public FormMain()
        {
            InitializeComponent();
            mainHelper = new MainHelper(this);
#if DEBUG
            button1.Visible = true;
#endif

        }

        #region Главное меню

        #region Файл

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// сохранить изменения в документе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainHelper.Save(mainTabControl.SelectedTab);
            (mainTabControl.SelectedTab as TabPageExt).HasNotSavedChanges = false;
        }

        /// <summary>
        /// сохранить как отдельный файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            string name = mainHelper.SaveAsFile(rang);
            if (!string.IsNullOrWhiteSpace(name))
            {
                mainTabControl.SelectedTab.Text = Path.GetFileName(name);
                mainTabControl.SelectedTab.ToolTipText = name;
            }
            (mainTabControl.SelectedTab as TabPageExt).HasNotSavedChanges = false;
        }

        /// <summary>
        /// создать новый документ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawRange r = new RawRange();
            r.Name = "Новый документ";
            TabPageExt tab = mainTabControl.OpenNewTab(r);
            tab.HasNotSavedChanges = true;
        }

        /// <summary>
        /// открыть файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.InitialDirectory = Application.StartupPath;
            of.Multiselect = true;

            if (of.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string file in of.FileNames)
                {
                    string ext = Path.GetExtension(file).ToLower();
                    RawRange rang = RawRangeSerializer.DeserializeFile(file, null);
                    rang.FilePath = file;
                    rang.Name = Path.GetFileNameWithoutExtension(file);
                   TabPageExt tab =  mainTabControl.OpenNewTab(rang, rang.FileName);
                    tab.HasNotSavedChanges = false;
                }
            }
        }


        /// <summary>
        /// загрузить с сайта rp5.ru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloadRP5ruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLoadFromRP5 frm = new FormLoadFromRP5();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                RawRange res = frm.Result;
                TabPageExt tab = mainTabControl.OpenNewTab(res);
                tab.HasNotSavedChanges = true;
            }
        }

        #endregion

        #region Операции

        /// <summary>
        /// привести ряды с разными интервалами наблюдений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void equalizeRangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofMax = new OpenFileDialog()
            {
                DefaultExt = ".csv",
                Filter = "*.csv|*.csv",
                Multiselect = false,
                InitialDirectory = Application.StartupPath
            };
            MessageBox.Show("Выберите ряд с большим интервалом");
            if (ofMax.ShowDialog() == DialogResult.OK)
            {
                OpenFileDialog ofMin = new OpenFileDialog()
                {
                    DefaultExt = ".csv",
                    Filter = "*.csv|*.csv",
                    Multiselect = false,
                    InitialDirectory = Application.StartupPath
                };
                MessageBox.Show("Выберите ряд с меньшим интервалом");
                if (ofMin.ShowDialog() == DialogResult.OK)
                {
                    RangeEqualizer.ProcessRange(ofMax.FileName, ofMin.FileName);
                }
            }
        }

        #endregion


        #endregion

        #region меню управления

        /// <summary>
        /// сохранить все документы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAlltoolStripButton_Click(object sender, EventArgs e)
        {
            foreach (TabPageExt tab in mainTabControl.TabPages)
                mainHelper.Save(tab);
        }

        #endregion



        /// <summary>
        /// подтверждение закрытия окна приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.MdiFormClosing: //если окно закрывается пользователем, то надо проверить все вкладки и сохранить
                case CloseReason.FormOwnerClosing:
                case CloseReason.UserClosing: 
                    foreach (TabPageExt tab in mainTabControl.TabPages)
                    {
                        bool cancelClosing = tab.ClosePage();
                        if(cancelClosing)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                    break;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //new RP5ru().GetFromServer(DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(-1), 29865);
            //new RP5ru().Search("ус");
        }
    }
}
