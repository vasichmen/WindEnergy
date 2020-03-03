using CommonLib.UITools;
using GMap.NET;
using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using SolarEnergy.SolarLib.Data.Providers.InternetServices;
using SolarEnergy.SolarLib.Models.Hours;
using SolarEnergy.UI;
using SolarEnergy.UI.Helpers;
using SolarEnergy.UI.Properties;
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
            FormSelectMapPointDialog fsmp = new FormSelectMapPointDialog("Выберите точку на карте", PointLatLng.Empty, Vars.Options.CacheFolder, Resources.rp5_marker, Vars.Options.MapProvider);
            if (fsmp.ShowDialog(this) == DialogResult.OK)
            {
                PointLatLng point = fsmp.Result;
                RawRange range = new NASA(Vars.Options.CacheFolder + "\\nasa").GetRange(DateTime.Now - TimeSpan.FromDays(20 * 365),
                    DateTime.Now - TimeSpan.FromDays(5), new NPSMeteostationInfo() { Position = point },
                    null, null );
                DataItem di = new DataItem()
                {
                    DatasetAllsky = new Dataset(range, SolarLib.MeteorologyParameters.AllSkyInsolation, new Uniform()),
                    DatasetClearSky = new Dataset(range, SolarLib.MeteorologyParameters.ClearSkyInsolation, new Uniform())
                };

            }
        }

        private void openNpsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSelectMapPointDialog fsmp = new FormSelectMapPointDialog("Выберите точку на карте", PointLatLng.Empty, Vars.Options.CacheFolder, Resources.rp5_marker, Vars.Options.MapProvider);
            if (fsmp.ShowDialog(this) == DialogResult.OK)
            {
                PointLatLng point = fsmp.Result;
            }
        }

        private void openFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Опериции

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
