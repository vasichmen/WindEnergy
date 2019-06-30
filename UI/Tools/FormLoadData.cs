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
            buttonStop.Enabled = true;
            progressBarStatus.Value = 0;
            progressBarStatus.Maximum = 100;
            progressBarStatus.Step = 1;
            stop = false;
            
            Action<int, int, string, int, int, int, int> act = new Action<int, int, string, int, int, int, int>((perc, all, q, count, pcQ,pc1,pc2) =>
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(() =>
                    {
                        progressBarStatus.Value = perc;
                        labelStatus.Text = $"Обработано: {all}, текущее сочетание \"{q}\" готово на {pcQ}% ({pc1}/{pc2}), найдено МС: {count}";

                        Application.DoEvents();
                    }));
                else
                {
                    progressBarStatus.Value = perc;
                    labelStatus.Text = $"Обработано: {all}, текущее сочетание \"{q}\" готово на {pcQ}% ({pc1}/{pc2}), найдено МС: {count}";
                    Application.DoEvents();
                }
            });
            Func<bool> checkStop = new Func<bool>(() => { return stop; });

            Task.Run(() =>
            {
                Scripts.LoadAllRP5Meteostations(Application.StartupPath + "\\all_mts_test.txt", act, checkStop);

                if (this.InvokeRequired)
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Операция завершена!");
                        buttonStart.Enabled = true;
                    }));
                else
                {
                    MessageBox.Show("Операция завершена!");
                    buttonStart.Enabled = true;
                }
            });
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            stop = true;
            buttonStop.Enabled = false;
        }
    }
}
