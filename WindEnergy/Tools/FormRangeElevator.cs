using CommonLib;
using CommonLib.Classes;
using CommonLib.UITools;
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
using WindEnergy.UI.Dialogs;
using WindEnergy.UI.Properties;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Transformation.Altitude;
using WindLib;

namespace WindEnergy.UI.Tools
{
    public partial class FormRangeElevator : Form
    {
        private RawRange range = null;
        private Dictionary<Months, double> MonthsHellmanValues = null;
        private HellmanCoefficientSource hellmanCoeffSource = HellmanCoefficientSource.AMSAnalog;

        /// <summary>
        /// результат работы диалогового окна (новый ряд данных)
        /// </summary>
        public RawRange Result { get; private set; }

        public FormRangeElevator(RawRange range)
        {
            InitializeComponent();
            this.range = range;
            refreshOptionsText();
        }

        private void refreshOptionsText()
        {
            if (hellmanCoeffSource == HellmanCoefficientSource.None)
                labelCurrentOptions.ForeColor = Color.Red;
            else
                labelCurrentOptions.ForeColor = Color.Green;

            labelCurrentOptions.Text = $"Режим: {hellmanCoeffSource.Description()}";
            if (hellmanCoeffSource == HellmanCoefficientSource.AMSAnalog && checkBoxUseRadius.Checked && double.TryParse(textBoxRadius.Text.Trim().Replace('.', Constants.DecimalSeparator), out double radius))
                labelCurrentOptions.Text += $" в радиусе {radius} км";

        }

        private void buttonElevate_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (!double.TryParse(textBoxToHeight.Text.Trim().Replace('.', Constants.DecimalSeparator), out double new_height))
            { _ = MessageBox.Show(this, $"Не удалось распознать {textBoxToHeight.Text} как число", "Расчет скорости ветра на высоте башни ВЭУ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!double.TryParse(textBoxFromHeight.Text.Trim().Replace('.', Constants.DecimalSeparator), out double old_height))
            { _ = MessageBox.Show(this, $"Не удалось распознать {textBoxFromHeight.Text} как число", "Расчет скорости ветра на высоте башни ВЭУ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!double.TryParse(textBoxRadius.Text.Trim().Replace('.', Constants.DecimalSeparator), out double radius))
            { _ = MessageBox.Show(this, $"Не удалось распознать {textBoxRadius.Text} как число", "Расчет скорости ветра на высоте башни ВЭУ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!double.TryParse(textBoxCoeffM.Text.Trim().Replace('.', Constants.DecimalSeparator), out double m))
            { _ = MessageBox.Show(this, $"Не удалось распознать {textBoxCoeffM.Text} как число", "Расчет скорости ветра на высоте башни ВЭУ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            radius *= 1000; //перевод км в метры

            if (hellmanCoeffSource == HellmanCoefficientSource.None)
                _ = MessageBox.Show(this, "Что-то пошло не так, попробуйте другие настройки", "Расчет скорости ветра на высоте башни ВЭУ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Action<int> action = new Action<int>((percent) =>
            {
                try
                {
                    if (this.InvokeRequired)
                        _ = Invoke(new Action(() => { progressBar1.Value = percent; }));
                    else
                        progressBar1.Value = percent;
                }
                catch (Exception) { }
            });

            Action<RawRange, SuitAMSResultItem> actionAfter = new Action<RawRange, SuitAMSResultItem>((rawRange, selectedAMS) =>
             {
                 _ = this.Invoke(new Action(() =>
                 {
                     string AMStext = selectedAMS != null ? $"На основе данных АМС {selectedAMS.AMS.Name} {selectedAMS.AMS.Position}\r\nОтклонение среднемесячных скоростей: {selectedAMS.Deviation:0.000} {(selectedAMS.AllMonthInRange ? "" : "\r\nВНИМАНИЕ!! В исходном ряде представлены не все месяцы. Поэтому подбор подходящей АМС может быть неточным")}" : "";
                     rawRange.Name = "Ряд на высоте " + new_height + " м";
                     _ = MessageBox.Show(this, $"Скорости ветра пересчитаны на высоту {new_height} м\r\n{((!string.IsNullOrWhiteSpace(AMStext)) ? AMStext : "")}", "Расчет скорости ветра на высоте башни ВЭУ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                     if (rawRange == null)
                         DialogResult = DialogResult.Cancel;
                     else
                     {
                         DialogResult = DialogResult.OK;
                         Result = rawRange;
                     }
                     Cursor = Cursors.Arrow;
                     Result = rawRange;
                     Close();
                 }));
             });

            try
            {
                //если в ряде нет координат и надо искать АМС-аналог, то выбираем (для подбора подходящей АМС)
                if (range.Position.IsEmpty && hellmanCoeffSource == HellmanCoefficientSource.AMSAnalog)
                {
                    FormSelectMapPointDialog fsp = new FormSelectMapPointDialog("Выберите координаты ряда " + range.Name, PointLatLng.Empty, Vars.Options.CacheFolder, Resources.rp5_marker, Vars.Options.MapProvider);
                    if (fsp.ShowDialog(this) == DialogResult.OK)
                        range.Position = fsp.Result;
                    else
                    {
                        Cursor = Cursors.Arrow;
                        return;
                    }
                }

                //основные параметры пересчета ряда на высоту
                ElevatorParameters parameters = new ElevatorParameters()
                {
                    FromHeight = old_height,
                    ToHeight = new_height,
                    Coordinates = range.Position,
                    SearchRaduis = checkBoxUseRadius.Checked ? radius : double.NaN,
                    MaximalRelativeSpeedDeviation = Vars.Options.UseSuitAMSMaximalRelativeSpeedDeviation ? Vars.Options.SuitAMSMaximalRelativeSpeedDeviation : double.NaN,
                    CustomMCoefficient = checkBoxCustomCoeffM.Checked ? m : double.NaN,
                    CustomNCoefficientMonths = MonthsHellmanValues,
                    HellmanCoefficientSource = hellmanCoeffSource
                };

                //если способ пересчета АМС-аналог, то надо предоставить выбор АМС для расчетов
                if (hellmanCoeffSource == HellmanCoefficientSource.AMSAnalog)
                {
                    //находим все подходящие АМС
                    SuitAMSResult suitAMSList = AMSSupport.GetSuitAMS(
                        range,
                        range.Position,
                        Vars.AMSMeteostations,
                        checkBoxUseRadius.Checked ? radius : double.NaN,
                        Vars.Options.UseSuitAMSMaximalRelativeSpeedDeviation ? Vars.Options.SuitAMSMaximalRelativeSpeedDeviation : double.NaN
                    );

                    //открывам диалоговое окно списка АМС
                    FormRangeElevatorConfirmation frec = new FormRangeElevatorConfirmation(suitAMSList);
                    if (frec.ShowDialog(this) == DialogResult.OK)
                        parameters.SelectedAMS = frec.Result;
                    else //если ничего не выбрали, то выходим
                    {
                        Cursor = Cursors.Arrow;
                        return;
                    }

                }

                RangeElevator.ProcessRange(range, parameters, action, actionAfter); //запускаем обработку ряда
            }
            catch (WebException exc)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, exc.Message, "Расчет скорости ветра на высоте башни ВЭУ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
            catch (WindEnergyException wex)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, wex.Message, "Расчет скорости ветра на высоте башни ВЭУ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ApplicationException exc)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, exc.Message + "\r\nПопробуйте уменьшить длину ряда", "Расчет скорости ветра на высоте башни ВЭУ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, "Произошла ошибка:\r\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
        }

        private void checkBoxCustomCoeffM_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxUseRadius.Enabled = !checkBoxCustomCoeffM.Checked;
            textBoxRadius.Enabled = !checkBoxCustomCoeffM.Checked;
            textBoxCoeffM.Enabled = checkBoxCustomCoeffM.Checked;
            textBoxFromHeight.Enabled = checkBoxCustomCoeffM.Checked;
            buttonMonths.Enabled = checkBoxCustomCoeffM.Checked;
            hellmanCoeffSource = checkBoxCustomCoeffM.Checked ? HellmanCoefficientSource.CustomOne : HellmanCoefficientSource.AMSAnalog;
            refreshOptionsText();
        }

        private void checkBoxUseRadius_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRadius.Enabled = checkBoxUseRadius.Checked;
            checkBoxCustomCoeffM.Enabled = !checkBoxUseRadius.Checked;
            hellmanCoeffSource = checkBoxUseRadius.Checked ? HellmanCoefficientSource.AMSAnalog : (checkBoxCustomCoeffM.Checked ? HellmanCoefficientSource.CustomOne : HellmanCoefficientSource.AMSAnalog);
            refreshOptionsText();
        }

        private void buttonMonths_Click(object sender, EventArgs e)
        {
            FormMonthsValuesDialogs fmv = new FormMonthsValuesDialogs(MonthsHellmanValues, "Среднемноголетние показатели степени по месяцам");
            if (fmv.ShowDialog() == DialogResult.OK)
            {
                MonthsHellmanValues = fmv.Result;
                textBoxCoeffM.Enabled = false;
                hellmanCoeffSource = HellmanCoefficientSource.CustomMonths;
            }
            else
                hellmanCoeffSource = HellmanCoefficientSource.CustomOne;
            refreshOptionsText();
        }

        private void textBoxRadius_TextChanged(object sender, EventArgs e)
        {
            refreshOptionsText();
        }
    }
}
