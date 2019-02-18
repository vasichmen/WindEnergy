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
            comboBoxMapProvider.Items.AddRange(MapProviders.GoogleSatellite.GetItems().ToArray());

        }

        /// <summary>
        /// сохранение и закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //сохранение настроек
            //object v = comboBoxMapProvider.SelectedItem;
            Vars.Options.MapProvider =(MapProviders)( new EnumTypeConverter<MapProviders>().ConvertFrom(comboBoxMapProvider.SelectedItem));
           // var bb = v.GetType();

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
