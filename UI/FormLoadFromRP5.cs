using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private RP5ru.MeteostationInfo selectedMeteostation = null;

        public FormLoadFromRP5()
        {
            InitializeComponent();
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
            //RP5ru.WmoInfo selectedwmo = comboBoxWMO.SelectedItem as RP5ru.WmoInfo;
            if (selectedMeteostation == null)
            {
                MessageBox.Show(this, "Точка не выбрана", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RawRange res = new RP5ru().GetFromServer(dateTimePickerFromDate.Value, dateTimePickerToDate.Value, selectedMeteostation);
            Result = res;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// фильтрация по началу города
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxWMO_TextUpdate(object sender, EventArgs e)
        {
            selectedMeteostation = null;
            string query = comboBoxWMO.Text.Trim();
            if (query.Length < 2)
                return;
            List<RP5ru.WmoInfo> results = new RP5ru().Search(query);
            comboBoxWMO.Items.Clear();
            comboBoxWMO.Items.AddRange(results.ToArray());
            comboBoxWMO.SelectionStart = comboBoxWMO.Text.Length;
        }

        /// <summary>
        /// проверка после выбора метеостанции 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxWMO_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //после того, как из поиска выбрана точка, надо проверить, есть ли на ней архив погоды. и вывести предупреждение, если ближайший архив далеко
            List<RP5ru.MeteostationInfo> meteost = new RP5ru().GetNearestMeteostations(comboBoxWMO.SelectedItem as RP5ru.WmoInfo);

            //выбор метеостанции
            RP5ru.MeteostationInfo meteostation;
            if (meteost.Count == 1)
                meteostation = meteost[0];
            else
            {
                string text = "В выбранной точке нет метеостанции. Ближайшие находятся:\r\n";
                text += meteost[0].Name + ", (" + meteost[0].OwnerDistance + " км)\r\n";
                text += meteost[1].Name + ", (" + meteost[1].OwnerDistance + " км)\r\n";
                FormChooseMeteostAirportDialog dlg = new FormChooseMeteostAirportDialog("Загрузка ряда", text, "Первая", "Вторая");
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    meteostation = meteost[dlg.Result-1];
                else return;
            }
            this.selectedMeteostation = meteostation;

            //установка времени начала наблюдений
            dateTimePickerFromDate.MinDate = selectedMeteostation.MonitoringFrom;
        }
    }
}
