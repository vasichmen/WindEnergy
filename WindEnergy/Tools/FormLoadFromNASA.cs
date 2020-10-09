﻿using CommonLib.UITools;
using CommonLibLib.Data.Interfaces;
using CommonLibLib.Data.Providers.InternetServices;
using GMap.NET;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.UI.Properties;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data.Providers.InternetServices;
using WindLib;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// загрузка данных из БД NASA
    /// </summary>
    public partial class FormLoadFromNASA : Form
    {
        public RawRange Result { get; private set; }
        private NASA engineNASA;
        private IGeocoderProvider geocoder;
        private RP5MeteostationInfo spoint;
        private PointLatLng point = PointLatLng.Empty;

        public FormLoadFromNASA()
        {
            InitializeComponent();
            Result = null;
            DialogResult = DialogResult.None;
            engineNASA = new NASA(Vars.Options.CacheFolder + "\\nasa");
            geocoder = new Arcgis(Vars.Options.CacheFolder + "\\arcgis");
        }

        /// <summary>
        /// кнопка выбора точки на карте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectPoint_Click(object sender, EventArgs e)
        {
            FormSelectMapPointDialog spt = new FormSelectMapPointDialog("Выберите точку на карте", point, Vars.Options.CacheFolder, Resources.rp5_marker, Vars.Options.MapProvider);
            if (spt.ShowDialog(this) == DialogResult.OK)
            {
                spoint = new RP5MeteostationInfo();
                spoint.Position = spt.Result;
                point = spt.Result;
                labelPointCoordinates.Text = $"Широта: {spt.Result.Lat:0.000} Долгота: {spt.Result.Lng:0.000}";

                loadAddressAsync(spt.Result);

                buttonDownload.Enabled = true;
                dateTimePickerFromDate.Enabled = true;
                dateTimePickerToDate.Enabled = true;
            }
        }

        /// <summary>
        /// Асинхронно загружает адрес точки ряда
        /// </summary>
        private async void loadAddressAsync(PointLatLng point)
        {
            labelPointAddress.Text = "Поиск адреса...";
            await Task.Run(() =>
            {
                string adr = "";
                try
                {
                    adr = new Arcgis(Vars.Options.CacheFolder + "\\arcgis").GetAddress(point);
                }
                catch (Exception)
                {
                    adr = "Не удалось найти адрес";
                }

                _ = this.Invoke(new Action(() =>
                {
                    labelPointAddress.Text = adr;
                }));

            }).ConfigureAwait(false);
        }

        /// <summary>
        /// нажатие на кнопку загрузки данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (spoint == null)
            {
                _ = MessageBox.Show(this, "Точка не выбрана", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                buttonDownload.Enabled = false;
                RawRange res = engineNASA.GetRange(dateTimePickerFromDate.Value, dateTimePickerToDate.Value, spoint);
                try
                {
                    res.Name = geocoder.GetAddress(spoint.Position);
                }
                catch(Exception)
                {
                    res.Name = $"Широта {spoint.Position.Lat:0.000} Долгота {spoint.Position.Lng:0.000}";
                }

                Result = res;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (WebException we)
            {
                buttonDownload.Enabled = true;
                _ = MessageBox.Show(this, we.Message, "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ApplicationException exx)
            {
                buttonDownload.Enabled = true;
                _ = MessageBox.Show(this, exx.Message, "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formLoadFromNASA_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.None)
                DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// запись начальных значений выбора дат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formLoadFromNASA_Shown(object sender, EventArgs e)
        {
            dateTimePickerToDate.Value = DateTime.Now.AddDays(-5);
            dateTimePickerFromDate.Value = DateTime.Now.AddDays(-20);
        }

        private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerFromDate.MaxDate = dateTimePickerToDate.Value;
            dateTimePickerToDate.MinDate = dateTimePickerFromDate.Value;
        }

        private void labelPointAddress_TextChanged(object sender, EventArgs e)
        {
            string n = ((Label)sender).Text;
            new ToolTip().SetToolTip(sender as Label, n);
        }
    }
}
