using CommonLib;
using CommonLib.UITools;
using GMap.NET;
using SolarEnergy.SolarLib;
using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using SolarEnergy.UI.Properties;
using SolarLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SolarEnergy.UI.Tools
{
    public partial class FormLoadFromNPS : Form
    {
        private PointLatLng point = PointLatLng.Empty;
        private MonthTransformationModels monthTransformator = MonthTransformationModels.None;

        public DataRange Result { get; private set; }

        public FormLoadFromNPS()
        {
            InitializeComponent();

            comboBoxMonthTransformer.Items.AddRange(MonthTransformationModels.None.GetItems().ToArray());
            comboBoxMonthTransformer.SelectedItem = MonthTransformationModels.LinearInterpolation.Description();
            DialogResult = DialogResult.Cancel;

            updateUI();
        }

        private void buttonSelectPoint_Click(object sender, EventArgs e)
        {
            FormSelectMapPointDialog fsmp = new FormSelectMapPointDialog("Выберите точку на карте", PointLatLng.Empty, Vars.Options.CacheFolder, Resources.rp5_marker, Vars.Options.MapProvider);
            if (fsmp.ShowDialog(this) == DialogResult.OK)
            {
                point = fsmp.Result;
                updateUI();
            }
        }

        /// <summary>
        /// обновление контролов 
        /// </summary>
        private void updateUI()
        {
            labelPointCoordinates.ForeColor = point.IsEmpty ? Color.Red : Color.Green;
            labelPointCoordinates.Text = point.IsEmpty ? "Не выбрана" : point.ToString(2);
            buttonLoad.Enabled = labelPointCoordinates.ForeColor == Color.Green && monthTransformator != MonthTransformationModels.None;
        }

        private void comboBoxMonthTransformer_SelectedValueChanged(object sender, EventArgs e)
        {
            monthTransformator = (MonthTransformationModels)(new EnumTypeConverter<MonthTransformationModels>().ConvertFrom(comboBoxMonthTransformer.SelectedItem));
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                DataItem data = Vars.NPSMeteostationDatabase.GetDataItem(point);
                DataRange range = new DataRange(data, monthTransformator);
                range.Name = "Ряд НПС в точке " + point.ToString();
                Result = range;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "При загрузке данных произошла ошибка. Причина:\r\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
