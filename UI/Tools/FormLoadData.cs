﻿using System;
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
        public bool stopMS = false;
        public bool stopMaxSpeed = false;

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
                    this.Invoke(new Action(() =>
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

            Task.Run(() =>
            {
                Scripts.LoadAllRP5Meteostations(Application.StartupPath + "\\all_mts_test.txt", act, checkStop);

                if (this.InvokeRequired)
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Операция завершена!");
                        buttonStartMS.Enabled = true;
                    }));
                else
                {
                    MessageBox.Show("Операция завершена!");
                    buttonStartMS.Enabled = true;
                }
            });
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {

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
                    this.Invoke(new Action(() =>
                    {
                        progressBarStatusMaxSpeed.Value = (int)((processedRegions / totalCurrentRegion) * 100d);
                        labelStatusMaxSpeed.Text = $"Обработано: {processedRegions} регионов из {totalRegions}, текущий регион \"{currentRegion}\", готов на {processedCurrentRegion}/{totalCurrentRegion}";
                        Application.DoEvents();
                    }));
                else
                {
                    progressBarStatusMaxSpeed.Value = (int)((processedRegions / totalCurrentRegion) * 100d);
                    labelStatusMaxSpeed.Text = $"Обработано: {processedRegions} регионов из {totalRegions}, текущий регион \"{currentRegion}\", готов на {processedCurrentRegion}/{totalCurrentRegion}";
                    Application.DoEvents();
                }
            });
            Func<bool> checkStop = new Func<bool>(() => { return stopMaxSpeed; });

            Task.Run(() =>
            {
                Scripts.LoadAllEnergywindLimits(Application.StartupPath + "\\all_limits_test.txt", act, checkStop);

                if (this.InvokeRequired)
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Операция завершена!");
                        buttonStartMaxSpeed.Enabled = true;
                    }));
                else
                {
                    MessageBox.Show("Операция завершена!");
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
    }
}
