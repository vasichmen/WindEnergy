using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data.Providers.DB;
using WindEnergy.UI.Dialogs;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// окно настроек программы
    /// </summary>
    public partial class FormOptions : Form
    {
        private string coordinateMSFile;
        private string regionLimitsFile;

        public FormOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// сохранение и закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            accept();
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
        private void formOptions_Shown(object sender, EventArgs e)
        {
            //загрузка настроек
            comboBoxMapProvider.Items.Clear();
            comboBoxSpeedGradations.Items.Clear();

            //ОСНОВНЫЕ
            comboBoxMapProvider.Items.AddRange(MapProviders.GoogleSatellite.GetItems().ToArray());
            labelCoordinatesMSFile.Text = Path.GetFileName(Vars.Options.StaticMeteostationCoordinatesSourceFile);
            coordinateMSFile = Vars.Options.StaticMeteostationCoordinatesSourceFile;
            regionLimitsFile = Vars.Options.StaticRegionLimitsSourceFile;
            labelRegionLimitsFile.Text = Path.GetFileName(Vars.Options.StaticRegionLimitsSourceFile);
            comboBoxMapProvider.SelectedItem = Vars.Options.MapProvider.Description();

            //РАСЧЁТ
            textBoxAirDensity.Text = Vars.Options.AirDensity.ToString();
            textBoxDaysToNewInterval.Text = Vars.Options.QualifierDaysToNewInterval.ToString();
            textBoxMinimalSpeedDeviation.Text = Vars.Options.MinimalSpeedDeviation.ToString();
            textBoxSectionLength.Text = Vars.Options.QualifierSectionLength.ToString();
            textBoxMinimalCoeffCorrel.Text = Vars.Options.MinimalCorrelationCoeff.ToString();
            textBoxNearestMSRadius.Text = Vars.Options.NearestMSRadius.ToString();
            checkBoxMinCorrDirection.Checked = Vars.Options.MinimalCorrelationControlParametres.Contains(MeteorologyParameters.Direction);
            checkBoxMinCorrTemp.Checked = Vars.Options.MinimalCorrelationControlParametres.Contains(MeteorologyParameters.Temperature);
            checkBoxMinCorrSpeed.Checked = Vars.Options.MinimalCorrelationControlParametres.Contains(MeteorologyParameters.Speed);
            checkBoxMinCorrWet.Checked = Vars.Options.MinimalCorrelationControlParametres.Contains(MeteorologyParameters.Wetness);
            numericUpDownNormalLawFrom.Value = (decimal)Vars.Options.NormalLawPirsonCoefficientDiapason.From;
            numericUpDownNormalLawTo.Value = (decimal)Vars.Options.NormalLawPirsonCoefficientDiapason.To;
            checkBoxCalculateAirDensity.Checked = Vars.Options.CalculateAirDensity;
            textBoxAirDensity.Text = Vars.Options.AirDensity.ToString();
            textBoxAirDensity.Enabled = !checkBoxCalculateAirDensity.Checked;

            //ГРАДАЦИИ
            comboBoxSpeedGradations.Items.AddRange(GradationTypes.NASA.GetItems().ToArray());
            numericUpDownGradFrom.Value = (decimal)Vars.Options.UserSpeedGradation.From;
            numericUpDownGradTo.Value = (decimal)Vars.Options.UserSpeedGradation.To;
            numericUpDownGradStep.Value = (decimal)Vars.Options.UserSpeedGradation.Step;
            comboBoxSpeedGradations.SelectedItem = Vars.Options.CurrentSpeedGradationType.Description();

            //ИСТОЧНИКИ ДАННЫХ
            radioButtonSearchTypeAPI.Checked = Vars.Options.RP5SearchEngine == RP5SearchEngine.OnlineAPI;
            radioButtonSearchTypeDB.Checked = Vars.Options.RP5SearchEngine == RP5SearchEngine.DBSearch;
        }

        /// <summary>
        /// выбор файла координат метеостанций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectCoordinatesMSFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Текстовые файлы *.txt|*.txt";
            if (of.ShowDialog(this) == DialogResult.OK)
            {
                bool f = new RP5MeteostationDatabase(of.FileName).CheckDatabaseFile();
                if (!f)
                {
                    _ = MessageBox.Show(this, "Не удалось открыть выбранный файл", "Изменение файла координат метеостанций", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.coordinateMSFile = of.FileName;
                labelCoordinatesMSFile.Text = Path.GetFileName(coordinateMSFile);
            }
        }

        /// <summary>
        /// выбор файла ограничений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectRegionLimitsFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Текстовые файлы *.txt|*.txt";
            if (of.ShowDialog(this) == DialogResult.OK)
            {
                bool f = Vars.SpeedLimits.CheckRegionLimitsFile(of.FileName);
                if (!f)
                {
                    _ = MessageBox.Show(this, "Не удалось открыть выбранный файл", "Изменение файла ограничений скоростей", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.regionLimitsFile = of.FileName;
                labelRegionLimitsFile.Text = Path.GetFileName(regionLimitsFile);
            }
        }

        /// <summary>
        /// обновление активности пользовательского шага градаций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSpeedGradations_SelectedValueChanged(object sender, EventArgs e)
        {
            bool f = (GradationTypes)(new EnumTypeConverter<GradationTypes>().ConvertFrom(comboBoxSpeedGradations.SelectedItem)) == GradationTypes.User;
            labelGradFrom.Enabled = f;
            labelGradStep.Enabled = f;
            labelGradTo.Enabled = f;
            numericUpDownGradFrom.Enabled = f;
            numericUpDownGradStep.Enabled = f;
            numericUpDownGradTo.Enabled = f;

        }

        private void checkBoxCalculateAirDensity_CheckedChanged(object sender, EventArgs e)
        {
            textBoxAirDensity.Enabled = !checkBoxCalculateAirDensity.Checked;
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            accept();
        }

        private void accept()
        {
            //ОСНОВНЫЕ
            Vars.Options.MapProvider = (MapProviders)(new EnumTypeConverter<MapProviders>().ConvertFrom(comboBoxMapProvider.SelectedItem));
            Vars.Options.StaticMeteostationCoordinatesSourceFile = coordinateMSFile;
            Vars.Options.StaticRegionLimitsSourceFile = regionLimitsFile;

            //РАСЧЁТ
            try
            {
                Vars.Options.AirDensity = double.Parse(textBoxAirDensity.Text.Replace('.', Vars.DecimalSeparator));
                Vars.Options.CalculateAirDensity = checkBoxCalculateAirDensity.Checked;
                Vars.Options.QualifierDaysToNewInterval = int.Parse(textBoxDaysToNewInterval.Text);
                Vars.Options.MinimalSpeedDeviation = double.Parse(textBoxMinimalSpeedDeviation.Text.Replace('.', Vars.DecimalSeparator));
                Vars.Options.QualifierSectionLength = int.Parse(textBoxSectionLength.Text);
                Vars.Options.MinimalCorrelationCoeff = double.Parse(textBoxMinimalCoeffCorrel.Text.Replace('.', Vars.DecimalSeparator));
                Vars.Options.NearestMSRadius = double.Parse(textBoxNearestMSRadius.Text.Replace('.', Vars.DecimalSeparator));

                //диапазон критерия Пирсона
                double fr = (double)numericUpDownNormalLawFrom.Value;
                double to = (double)numericUpDownNormalLawTo.Value;
                Vars.Options.NormalLawPirsonCoefficientDiapason = new Diapason<double>(fr, to);

                Vars.Options.MinimalCorrelationControlParametres.Clear();
                if (checkBoxMinCorrDirection.Checked)
                    Vars.Options.MinimalCorrelationControlParametres.Add(MeteorologyParameters.Direction);
                if (checkBoxMinCorrTemp.Checked)
                    Vars.Options.MinimalCorrelationControlParametres.Add(MeteorologyParameters.Temperature);
                if (checkBoxMinCorrSpeed.Checked)
                    Vars.Options.MinimalCorrelationControlParametres.Add(MeteorologyParameters.Speed);
                if (checkBoxMinCorrWet.Checked)
                    Vars.Options.MinimalCorrelationControlParametres.Add(MeteorologyParameters.Wetness);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(this, ex.Message + "\r\nПроверьте введённые данные на вкладке \"Расчёт\"", "Сохранение настроек", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //ГРАДАЦИИ
            try
            {
                Vars.Options.CurrentSpeedGradationType = (GradationTypes)(new EnumTypeConverter<GradationTypes>().ConvertFrom(comboBoxSpeedGradations.SelectedItem));
                if (Vars.Options.CurrentSpeedGradationType == GradationTypes.User)
                {
                    Vars.Options.UserSpeedGradation.From = (double)numericUpDownGradFrom.Value;
                    Vars.Options.UserSpeedGradation.To = (double)numericUpDownGradTo.Value;
                    Vars.Options.UserSpeedGradation.Step = (double)numericUpDownGradStep.Value;
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(this, ex.Message + "\r\nПроверьте введённые данные на вкладке \"Градации\"", "Сохранение настроек", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //ИСТОЧНИКИ ДАННЫХ
            try
            {
                if (radioButtonSearchTypeAPI.Checked)
                    Vars.Options.RP5SearchEngine = RP5SearchEngine.OnlineAPI;
                if (radioButtonSearchTypeDB.Checked)
                    Vars.Options.RP5SearchEngine = RP5SearchEngine.DBSearch;
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(this, ex.Message + "\r\nПроверьте введённые данные на вкладке \"Источники данных\"", "Сохранение настроек", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
