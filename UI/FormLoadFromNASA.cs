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
using WindEnergy.UI.Dialogs;

namespace WindEnergy.UI
{
    public partial class FormLoadFromNASA : Form
    {
        public RawRange Result = null;
        private NASA engine;
        private NASA.PointInfo spoint;

        public FormLoadFromNASA()
        {
            InitializeComponent();
            DialogResult = DialogResult.None;
            engine = new NASA(Vars.Options.CacheFolder + "\\nasa");
        }

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

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (spoint == null)
            {
                MessageBox.Show(this, "Точка не выбрана", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                RawRange res = engine.GetRange(dateTimePickerFromDate.Value, dateTimePickerToDate.Value, spoint);
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormLoadFromNASA_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.None)
                DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 
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
