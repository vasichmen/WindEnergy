﻿using CommonLib.UITools;
using SolarEnergy.UI;
using SolarEnergy.UI.Helpers;
using SolarLib;
using SolarLib.Classes.Structures.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        #region Файл

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void openNasaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openNpsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Опериции

        private void dailyAverageGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var w = new FormDailyAverageGraphs(Vars.Options.LastDirectory,  this.Icon);
            w.Show();
            Vars.Options.LastDirectory = w.Directory;
        }


        private void equalizeRangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var w = new FormEqualizer(Vars.Options.LastDirectory,this.Icon);
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
            Vars.Options = new Options();
        }

    }
}
