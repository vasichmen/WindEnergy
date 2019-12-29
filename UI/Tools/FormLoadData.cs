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
    /// <summary>
    /// окно загрузки данных с сайтов
    /// </summary>
    public partial class FormLoadData : Form
    {
        private bool stopMS = false;
        private bool stopMaxSpeed = false;
        private bool stopRP5 = false;
        private object RP5locker = new object();

        public FormLoadData()
        {
            InitializeComponent();
        }

        #region обновление БД метеостанций
        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStartMS.Enabled = false;
            buttonStopMS.Enabled = true;
            progressBarStatusMS.Value = 0;
            progressBarStatusMS.Maximum = 100;
            progressBarStatusMS.Step = 1;
            stopMS = false;

            Action<int, int, string, int, int, int, int> act = new Action<int, int, string, int, int, int, int>((perc, all, q, count, pcQ, pc1, pc2) =>
            {
                if (this.InvokeRequired)
                    _ = this.Invoke(new Action(() =>
                      {
                          progressBarStatusMS.Value = perc;
                          labelStatusMS.Text = $"Обработано: {all}, текущее сочетание \"{q}\" готово на {pcQ}% ({pc1}/{pc2}), найдено МС: {count}";

                          Application.DoEvents();
                      }));
                else
                {
                    progressBarStatusMS.Value = perc;
                    labelStatusMS.Text = $"Обработано: {all}, текущее сочетание \"{q}\" готово на {pcQ}% ({pc1}/{pc2}), найдено МС: {count}";
                    Application.DoEvents();
                }
            });
            Func<bool> checkStop = new Func<bool>(() => { return stopMS; });

            _ = Task.Run(() =>
              {
                  Scripts.LoadAllRP5Meteostations(Application.StartupPath + "\\all_mts_test.txt", act, checkStop);

                  if (InvokeRequired)
                      _ = this.Invoke(new Action(() =>
                        {
                            _ = MessageBox.Show("Операция завершена!");
                            buttonStartMS.Enabled = true;
                        }));
                  else
                  {
                      _ = MessageBox.Show("Операция завершена!");
                      buttonStartMS.Enabled = true;
                  }
              });
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            stopMS = true;
            buttonStopMS.Enabled = false;
        }

        #endregion

        #region обновление БД максимальных скоростей

        private void buttonStartMaxSpeed_Click(object sender, EventArgs e)
        {
            buttonStartMaxSpeed.Enabled = false;
            buttonStopMaxSpeed.Enabled = true;
            progressBarStatusMaxSpeed.Value = 0;
            progressBarStatusMaxSpeed.Maximum = 100;
            progressBarStatusMaxSpeed.Step = 1;
            stopMaxSpeed = false;

            Action<int, int, string, int, int> act = new Action<int, int, string, int, int>((processedRegions, totalRegions, currentRegion, processedCurrentRegion, totalCurrentRegion) =>
            {
                if (this.InvokeRequired)
                    _ = this.Invoke(new Action(() =>
                      {
                          progressBarStatusMaxSpeed.Value = (int)((processedRegions / (double)totalRegions) * 100d);
                          labelStatusMaxSpeed.Text = $"Обработано: {processedRegions} регионов из {totalRegions}, текущий регион \"{currentRegion}\", готов на {processedCurrentRegion}/{totalCurrentRegion}";
                          Application.DoEvents();
                      }));
                else
                {
                    progressBarStatusMaxSpeed.Value = (int)((processedRegions / (double)totalRegions) * 100d);
                    labelStatusMaxSpeed.Text = $"Обработано: {processedRegions} регионов из {totalRegions}, текущий регион \"{currentRegion}\", готов на {processedCurrentRegion}/{totalCurrentRegion}";
                    Application.DoEvents();
                }
            });
            Func<bool> checkStop = new Func<bool>(() => { return stopMaxSpeed; });

            _ = Task.Run(() =>
              {
                  Scripts.LoadAllEnergywindLimits(Application.StartupPath + "\\all_limits_test.txt", act, checkStop);

                  if (this.InvokeRequired)
                      _ = this.Invoke(new Action(() =>
                        {
                            _ = MessageBox.Show("Операция завершена!");
                            buttonStartMaxSpeed.Enabled = true;
                        }));
                  else
                  {
                      _ = MessageBox.Show("Операция завершена!");
                      buttonStartMaxSpeed.Enabled = true;
                  }
              });
        }

        private void buttonStopMaxSpeed_Click(object sender, EventArgs e)
        {
            stopMaxSpeed = true;
            buttonStopMaxSpeed.Enabled = false;
        }

        #endregion

        #region Загрузка БД РП5


        private void buttonStartRP5_Click(object sender, EventArgs e)
        {
            buttonStartRP5.Enabled = false;
            buttonUpdateRP5.Enabled = false;
            buttonStopRP5.Enabled = true;
            progressBarStatusRP5.Value = 0;
            progressBarStatusRP5.Maximum = 100;
            progressBarStatusRP5.Step = 1;
            stopRP5 = false;

            Action<int, string> act = new Action<int, string>((perc, text) =>
            {
                if (this.InvokeRequired)
                    _ = this.Invoke(new Action(() =>
                    {
                        progressBarStatusRP5.Value = perc;
                        labelStatusRP5.Text = text;

                        Application.DoEvents();
                    }));
                else
                {
                    progressBarStatusRP5.Value = perc;
                    labelStatusRP5.Text = text;
                    Application.DoEvents();
                }
            });
            Func<bool> checkStop = new Func<bool>(() => { return stopRP5; });

            _ = Task.Run(() =>
            {
                Scripts.LoadAllRP5Database(Vars.Options.StaticRP5DatabaseSourceDirectory, checkBoxSkipErrors.Checked, act, checkStop);

                if (InvokeRequired)
                    _ = this.Invoke(new Action(() =>
                    {
                        _ = MessageBox.Show("Операция завершена!");
                        buttonStartRP5.Enabled = true;
                        buttonUpdateRP5.Enabled = true;
                    }));
                else
                {
                    _ = MessageBox.Show("Операция завершена!");
                    buttonUpdateRP5.Enabled = true;
                    buttonStartRP5.Enabled = true;
                }
            });
        }

        private void buttonStopRP5_Click(object sender, EventArgs e)
        {
            stopRP5 = true;
            buttonStopRP5.Enabled = false;
            buttonUpdateRP5.Enabled = false;
        }

        /// <summary>
        /// обновление рп5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdateRP5_Click(object sender, EventArgs e)
        {
            buttonStartRP5.Enabled = false;
            buttonUpdateRP5.Enabled = false;
            buttonStopRP5.Enabled = true;
            progressBarStatusRP5.Value = 0;
            progressBarStatusRP5.Maximum = 100;
            progressBarStatusRP5.Step = 1;
            stopRP5 = false;

            Action<int, string> act = new Action<int, string>((perc, text) =>
            {
                if (this.InvokeRequired)
                    _ = this.Invoke(new Action(() =>
                    {
                        progressBarStatusRP5.Value = perc;
                        labelStatusRP5.Text = text;

                        Application.DoEvents();
                    }));
                else
                {
                    progressBarStatusRP5.Value = perc;
                    labelStatusRP5.Text = text;
                    Application.DoEvents();
                }
            });
            Func<bool> checkStop = new Func<bool>(() => { lock (RP5locker) { return stopRP5; } });

            _ = Task.Run(() =>
            {
                Scripts.UpdateAllRP5Database(Vars.Options.StaticRP5DatabaseSourceDirectory, act, checkStop);

                if (InvokeRequired)
                    _ = this.Invoke(new Action(() =>
                    {
                        _ = MessageBox.Show("Операция завершена!");
                        buttonStartRP5.Enabled = true;
                        buttonUpdateRP5.Enabled = true;
                    }));
                else
                {
                    _ = MessageBox.Show("Операция завершена!");
                    buttonUpdateRP5.Enabled = true;
                    buttonStartRP5.Enabled = true;
                }
            });
        }
        #endregion

    }
}
