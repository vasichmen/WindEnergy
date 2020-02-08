using SolarEnergy.Tools;
using SolarEnergy.UI;
using SolarLib;
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
        public FormMain()
        {
            InitializeComponent();
#if DEBUG
            button1.Visible = true;
#endif
        }
        #region Главное меню

        #region Опериции
        private void dailyAverageGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormDailyAverageGraphs().Show();
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
            Vars.Options = new SolarLib.Classes.Options.Options();
        }
    }
}
