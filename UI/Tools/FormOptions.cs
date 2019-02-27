using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// окно настроек программы
    /// </summary>
    public partial class FormOptions : Form
    {
        public FormOptions()
        {
            InitializeComponent();
            comboBoxMapProvider.Items.Clear();

            //ОСНОВНЫЕ
            comboBoxMapProvider.Items.AddRange(MapProviders.GoogleSatellite.GetItems().ToArray());

            //РАСЧЁТ
            textBoxAirDensity.Text = Vars.Options.AirDensity.ToString();
            textBoxDaysToNewInterval.Text = Vars.Options.QualifierDaysToNewInterval.ToString();
            textBoxMinimalSpeedDeviation.Text = Vars.Options.MinimalSpeedDeviation.ToString();
            textBoxSectionLength.Text = Vars.Options.QualifierSectionLength.ToString();

        }

        /// <summary>
        /// сохранение и закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //ОСНОВНЫЕ
            Vars.Options.MapProvider =(MapProviders)( new EnumTypeConverter<MapProviders>().ConvertFrom(comboBoxMapProvider.SelectedItem));

            //РАСЧЁТ
            try
            {
                Vars.Options.AirDensity = double.Parse(textBoxAirDensity.Text.Replace('.',Vars.DecimalSeparator));
                Vars.Options.QualifierDaysToNewInterval = int.Parse(textBoxDaysToNewInterval.Text);
                Vars.Options.MinimalSpeedDeviation = double.Parse(textBoxMinimalSpeedDeviation.Text.Replace('.', Vars.DecimalSeparator));
                Vars.Options.QualifierSectionLength = int.Parse(textBoxSectionLength.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message+"\r\nПроверьте введённые данные на вкладке \"Расчёт\"", "Сохранение настроек", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Close();
        }

        /// <summary>
        /// закрытие окна без сохранения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// загрузка настроек при открытии окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormOptions_Shown(object sender, EventArgs e)
        {
            //загрузка настроек

            comboBoxMapProvider.SelectedItem = Vars.Options.MapProvider.Description();
        }
    }
}
