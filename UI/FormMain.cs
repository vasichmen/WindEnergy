using System;
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
    public partial class FormMain : Form
    {
        private MainHelper mainHelper;
        private RawRange r;
        public FormMain()
        {
            InitializeComponent();
            mainHelper = new MainHelper(this);
        }

        #region Главное меню

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

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
                    mainHelper.OpenFile(file);
                }
            }
        }


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

        private void downloadRP5ruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLoadFromRP5 frm = new FormLoadFromRP5();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                RawRange res = frm.Result;
                mainTabControl.OpenNewTab(res);
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //new RP5ru().GetFromServer(DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(-1), 29865);
            //new RP5ru().Search("ус");
        }

    }
}
