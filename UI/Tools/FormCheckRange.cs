﻿using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.Lib.Operations;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.UI.Dialogs;
using WindEnergy.UI.Ext;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// окно исправлений и восстановления ряда наблюдений
    /// </summary>
    public partial class FormCheckRange : Form
    {
        private RawRange range = null;

        /// <summary>
        /// точка на карте для которой происходит проверка ряда
        /// </summary>
        private PointLatLng checkPoint = PointLatLng.Empty;

        private List<Diapason<double>> speedDiapasons;
        private List<Diapason<double>> directionDiapasons;


        /// <summary>
        /// результат работы диалогового окна (новый ряд данных)
        /// </summary>
        public RawRange Result { get; private set; }



        /// <summary>
        /// открыть окно с заданным рядом
        /// </summary>
        /// <param name="range"></param>
        public FormCheckRange(RawRange range)
        {
            InitializeComponent();
            this.range = range;
        }

    

        #region проверка ряда

        /// <summary>
        /// кнопка проверить ряд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheckRange_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                //если выбран способ с помощью выбранных истоников ограничений
                if (radioButtonSelectLimitsProvider.Checked)
                {
                    LimitsProviders provider = radioButtonSelectLimitsProvider.Checked ? LimitsProviders.StaticLimits : LimitsProviders.Manual;
                    if (provider == LimitsProviders.None)
                    {
                        MessageBox.Show(this, "Необходимо выбрать истоник ограничений", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (provider == LimitsProviders.Manual)
                    {
                        MessageBox.Show(this, "Для ручного ввода ограничений выберите соответствующий пункт", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (checkPoint.IsEmpty)
                    {
                        MessageBox.Show(this, "Необходимо выбрать точку на карте", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    range = Checker.ProcessRange(range, new CheckerParameters(provider, checkPoint), out CheckerInfo stats);
                    MessageBox.Show(this, $"Ряд исправлен, результаты:\r\nНаблюдений в исходном ряде: {stats.Total}\r\nПовторов дат: {stats.DateRepeats}\r\nПревышений диапазонов: {stats.OverLimits}\r\nДругих ошибок: {stats.OtherErrors}\r\nОсталось наблюдений: {stats.Remain}", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    range.Name = "Исправленный ряд";
                }

                //если выбран способ вручную вводить ограничения
                if (radioButtonEnterLimits.Checked)
                {
                    if (speedDiapasons == null)
                    {
                        MessageBox.Show(this, "Необходимо ввести ограничения для скоростей ветра", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (directionDiapasons == null)
                    {
                        MessageBox.Show(this, "Необходимо ввести ограничения для направлений ветра", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    range = Checker.ProcessRange(range, new CheckerParameters(speedDiapasons, directionDiapasons), out CheckerInfo stats);
                    range.Name = "Исправленный ряд";
                    MessageBox.Show(this, $"Ряд исправлен, результаты:\r\nНаблюдений в исходном ряде: {stats.Total}\r\nПовторов дат: {stats.DateRepeats}\r\nПревышений диапазонов: {stats.OverLimits}\r\nДругих ошибок: {stats.OtherErrors}\r\nОсталось наблюдений: {stats.Remain}", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ApplicationException exc)
            {
                MessageBox.Show(this, exc.Message, "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }

            if (range == null)
                DialogResult = DialogResult.Cancel;
            else
            {
                DialogResult = DialogResult.OK;
                Result = range;
            }
            Close();
        }

        /// <summary>
        /// переключение выключателей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonLimits_CheckedChanged(object sender, EventArgs e)
        {
            buttonSelectPoint.Enabled = radioButtonSelectLimitsProvider.Checked;
           // comboBoxLimitsProvider.Enabled = radioButtonSelectLimitsProvider.Checked;
            buttonEnterDirectionDiapason.Enabled = radioButtonEnterLimits.Checked;
            buttonEnterSpeedDiapason.Enabled = radioButtonEnterLimits.Checked;
        }

        /// <summary>
        /// выбор точки на карте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectPoint_Click(object sender, EventArgs e)
        {
            FormSelectMapPointDialog spt = new FormSelectMapPointDialog("Выберите точку на карте", PointLatLng.Empty);
            if (spt.ShowDialog(this) == DialogResult.OK)
            {
                labelPointCoordinates.Text = $"Широта: {spt.Result.Lat.ToString("0.000")} Долгота: {spt.Result.Lng.ToString("0.000")}";
                labelPointAddress.Text = new Arcgis(Vars.Options.CacheFolder + "\\arcgis").GetAddress(spt.Result);
                toolTip1.SetToolTip(labelPointAddress, labelPointAddress.Text);
                checkPoint = spt.Result;
            }
        }

        /// <summary>
        /// ввод ограничений скоростей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEnterSpeedDiapason_Click(object sender, EventArgs e)
        {
            FormEditDiapasons fed = new FormEditDiapasons(speedDiapasons, "Редактирование диапазонов скоростей ветра");
            if (fed.ShowDialog(this) == DialogResult.OK)
            {
                this.speedDiapasons = fed.Result;
                labelspeedDiap.Text = "Выбрано " + speedDiapasons.Count + " диапазонов";
            }
        }

        /// <summary>
        /// ввод ограничений направлений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEnterDirectionDiapason_Click(object sender, EventArgs e)
        {
            FormEditDiapasons fed = new FormEditDiapasons(directionDiapasons, "Редактирование диапазонов направлений ветра");
            if (fed.ShowDialog(this) == DialogResult.OK)
            {
                this.directionDiapasons = fed.Result;
                labelspeedDiap.Text = "Выбрано " + directionDiapasons.Count + " диапазонов";
            }
        }

        #endregion


        /// <summary>
        /// открытие окна и заполнение элементов в combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formCheckRepairRange_Shown(object sender, EventArgs e)
        {
          
            //comboBoxLimitsProvider.SelectedItem = LimitsProviders.StaticLimits.Description();
            checkPoint = range.Position;
            labelPointCoordinates.Text = $"Широта: {checkPoint.Lat.ToString("0.000")} Долгота: {checkPoint.Lng.ToString("0.000")}";
            try
            { labelPointAddress.Text = new Arcgis(Vars.Options.CacheFolder + "\\arcgis").GetAddress(checkPoint); }
            catch (Exception) { }
            radioButtonSelectLimitsProvider.Checked = true;
        }


    }
}