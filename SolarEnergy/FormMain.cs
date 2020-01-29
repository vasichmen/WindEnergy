using SolarEnergy.Tools;
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

namespace SolarEnergy
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vars.Options = new SolarLib.Classes.Options.Options();
        }

        private void dailyAverageGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormDailyAverageGraphs().Show();
        }
    }
}
