using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib;

namespace WindEnergy.UI.Tools
{
    public partial class FormLoadData : Form
    {
        public bool stop = false;

        public FormLoadData()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            progressBarStatus.Value = 0;
            progressBarStatus.Maximum = 100;
            progressBarStatus.Step = 1;
            Action<int, int, string, int, int> act = new Action<int, int, string, int, int>((perc, all, q, count, pcQ) =>
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(() =>
                    {
                        progressBarStatus.Value = perc;
                        labelStatus.Text = $"Всего обработано: {all}, текущее сочетание {q} готово на {pcQ}%, выбрано МС: {count}";

                        Application.DoEvents();
                    }));
                else
                {
                    progressBarStatus.Value = perc;
                    labelStatus.Text = $"Всего обработано: {all}, текущее сочетание {q} готово на {pcQ}%, выбрано МС: {count}";
                    Application.DoEvents();
                }
            });
            Func<bool> checkStop = new Func<bool>(() => { return stop; });

            Task.Run(() =>
            {
                Scripts.LoadAllRP5Meteostations(Application.StartupPath + "\\all_mts_test.txt", act, checkStop);
                MessageBox.Show("Операция завершена!");
            });
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            stop = true;
        }
    }
}
