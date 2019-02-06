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
    /// <summary>
    /// окно загрузки ряда с сайта rp5.ru
    /// </summary>
    public partial class FormLoadFromRP5 : Form
    {
        /// <summary>
        /// список ближайших метеостанций к выбранной точке погоды
        /// </summary>
        private RP5ru.PointInfo selectedMeteostation = null;
        private RP5ru engine;

        public FormLoadFromRP5()
        {
            InitializeComponent();
            engine = new RP5ru(Vars.Options.CacheFolder + "\\rp5.ru");
            DialogResult = DialogResult.None;
        }

        /// <summary>
        /// результат работы окна 
        /// </summary>
        public RawRange Result { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formLoadFromRP5_Shown(object sender, EventArgs e)
        {
            dateTimePickerFromDate.Value = DateTime.Now.AddDays(-2);
            dateTimePickerToDate.Value = DateTime.Now;
        }

        /// <summary>
        /// загрузка данных с сайта и закрытие диалогового окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (selectedMeteostation == null)
            {
                MessageBox.Show(this, "Точка не выбрана", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                RawRange res = engine.GetRange(dateTimePickerFromDate.Value, dateTimePickerToDate.Value, selectedMeteostation);
                Result = res;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (WebException ex)
            {
                MessageBox.Show(this, "Ошибка подключения, проверьте соединение с Интернет", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// фильтрация по началу города
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxWMO_TextUpdate(object sender, EventArgs e)
        {
            selectedMeteostation = null;
            string query = comboBoxPoint.Text.Trim();
            if (query.Length < 2)
                return;
            try
            {
                List<RP5ru.WmoInfo> results = engine.Search(query);
                comboBoxPoint.Items.Clear();
                comboBoxPoint.Items.AddRange(results.ToArray());
                comboBoxPoint.SelectionStart = comboBoxPoint.Text.Length;
            }
            catch (WebException ex)
            {
                MessageBox.Show(this, "Ошибка подключения, проверьте соединение с Интернет", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// проверка после выбора метеостанции 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxWMO_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //после того, как из поиска выбрана точка, надо проверить, есть ли на ней архив погоды. и вывести предупреждение, если ближайший архив далеко

            try
            {
                List<RP5ru.PointInfo> meteost = engine.GetNearestMeteostations(comboBoxPoint.SelectedItem as RP5ru.WmoInfo);
                //выбор метеостанции
                RP5ru.PointInfo meteostation;
                if (meteost.Count == 1)
                    meteostation = meteost[0];
                else
                {
                    string text = "Ближайшие метеостанции к выбранной точке:\r\n";
                    text += meteost[0].Name + ", (" + meteost[0].OwnerDistance + " км)\r\n";
                    text += meteost[1].Name + ", (" + meteost[1].OwnerDistance + " км)\r\n";
                    FormChooseMeteostAirportDialog dlg = new FormChooseMeteostAirportDialog("Загрузка ряда с rp5.ru", text, meteost[0].Name, meteost[1].Name);
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                        meteostation = meteost[dlg.Result - 1];
                    else return;
                }
                this.selectedMeteostation = meteostation;

                //установка времени начала и конца наблюдений
                dateTimePickerFromDate.MinDate = selectedMeteostation.MonitoringFrom;
                dateTimePickerToDate.MaxDate = DateTime.Now;
                labelDateRange.Text = "Выберите диапазон дат: (дата начала наблюдений: " + selectedMeteostation.MonitoringFrom.ToString() + ")";

                //разблокировка элементов
                dateTimePickerFromDate.Enabled = true;
                dateTimePickerToDate.Enabled = true;
                buttonDownload.Enabled = true;

            }
            catch (WebException ex)
            {
                MessageBox.Show(this, "Ошибка подключения, проверьте соединение с Интернет", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormLoadFromRP5_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.None)
                DialogResult = DialogResult.Cancel;
        }
    }
}
