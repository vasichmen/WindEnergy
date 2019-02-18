using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Data;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.Lib.Data.Interfaces;
using WindEnergy.UI.Dialogs;
using WindEnergy.Lib.Classes.Collections;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// загрузка данных из БД NASA
    /// </summary>
    public partial class FormLoadFromNASA : Form
    {
        public RawRange Result = null;
        private NASA engineNASA;
        private IGeocoderProvider geocoder;
        private NASA.PointInfo spoint;

        public FormLoadFromNASA()
        {
            InitializeComponent();
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
            FormSelectMapPointDialog spt = new FormSelectMapPointDialog("Выберите точку на карте", PointLatLng.Empty);
            if (spt.ShowDialog(this) == DialogResult.OK)
            {
                spoint = new NASA.PointInfo();
                spoint.Position = spt.Result;
                labelPointCoordinates.Text = spoint.Position.ToString();

                buttonDownload.Enabled = true;
                dateTimePickerFromDate.Enabled = true;
                dateTimePickerToDate.Enabled = true;
            }
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
                MessageBox.Show(this, "Точка не выбрана", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                RawRange res = engineNASA.GetRange(dateTimePickerFromDate.Value, dateTimePickerToDate.Value, spoint);
                res.Name = geocoder.GetAddress(spoint.Position);
                Result = res;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (WebException ex)
            {
                MessageBox.Show(this, "Ошибка подключения, проверьте соединение с Интернет", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ApplicationException exx)
            {
                MessageBox.Show(this, exx.Message, "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void FormLoadFromNASA_FormClosed(object sender, FormClosedEventArgs e)
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
            dateTimePickerFromDate.Value = DateTime.Now.AddDays(-4);
            dateTimePickerToDate.Value = DateTime.Now.AddDays(-1);
        }
    }
}
