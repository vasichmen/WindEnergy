using CommonLib.Operations;
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

namespace CommonLib.UITools
{
    /// <summary>
    /// окно расчета среднесуточных графиков по месяцам
    /// </summary>
    public partial class FormDailyAverageGraphs : Form
    {
        public string Directory;

        public FormDailyAverageGraphs(string directory, Icon icon)
        {
            InitializeComponent();
            Directory = directory;
            Icon = icon;
        }

        private void buttonSelectFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.InitialDirectory = Directory;
            of.Multiselect = true;
            if (of.ShowDialog(this) == DialogResult.OK)
            {
                if (of.FileNames.Length == 0) return;
                Directory = Path.GetDirectoryName(of.FileNames[0]);
                Averager.ProcessRanges(of.FileNames.ToList());
                MessageBox.Show("Обработка завершена!");
            }

            of.Dispose();
        }
    }
}
