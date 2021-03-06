﻿using CommonLib.UITools;
using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.UI.Helpers;
using SolarEnergy.UI.Tools;
using SolarLib;
using System;
using System.IO;
using System.Windows.Forms;
using WindEnergy.UI.Ext;

namespace SolarEnergy.UI
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// вспомогательные методы интерфейса
        /// </summary>
        public MainHelper mainHelper { get; set; }

        public FormMain()
        {
            InitializeComponent();
            mainHelper = new MainHelper(this);
#if DEBUG
            button1.Visible = true;
#endif
        }
        #region Главное меню


        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
            saveToolStripMenuItem.Enabled = tabControlMain.SelectedTab != null;
            saveAsToolStripMenuItem.Enabled = tabControlMain.SelectedTab != null;
        }

        #region Файл


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab == null)
                return;
            mainHelper.Save(tabControlMain.SelectedTab);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab == null)
                return;
            DataRange rang = (tabControlMain.SelectedTab as TabPageExt).DataRange;
            string name = mainHelper.SaveAsFile(rang);
            if (!string.IsNullOrWhiteSpace(name))
            {
                tabControlMain.SelectedTab.Text = Path.GetFileName(name);
                tabControlMain.SelectedTab.ToolTipText = name;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void openNasaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLoadFromNASA fln = new FormLoadFromNASA();
            if (fln.ShowDialog(this) == DialogResult.OK)
            {
                this.tabControlMain.OpenNewTab(fln.Result, fln.Result.Name);
            }
        }

        private void openNpsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLoadFromNPS fln = new FormLoadFromNPS();
            if (fln.ShowDialog(this) == DialogResult.OK)
            {
                this.tabControlMain.OpenNewTab(fln.Result, fln.Result.Name);
            }
        }

        private void openFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var range = mainHelper.OpenFile(this);
            if (range == null)
                return;
            this.tabControlMain.OpenNewTab(range, range.Name);
        }


        #endregion

        #region Операции

        private void dailyAverageGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var w = new FormDailyAverageGraphs(Vars.Options.LastDirectory, this.Icon);
            w.Show();
            Vars.Options.LastDirectory = w.Directory;
        }


        private void equalizeRangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var w = new FormEqualizer(Vars.Options.LastDirectory, this.Icon);
            w.Show(this);
            Vars.Options.LastDirectory = w.Directory;
        }

        #endregion

        #region Помощь

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAbout().Show();
        }

        #endregion

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            var s = Vars.NPSMeteostationDatabase.List;
        }

    }
}
