using CommonLib;
using CommonLib.Classes;
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

            Action<RawRange, AMSMeteostationInfo> actionAfter = new Action<RawRange, AMSMeteostationInfo>((rawRange, AMS) =>
             {
                 _ = this.Invoke(new Action(() =>
                 {
                     string AMStext = AMS != null ? $"На основе данных АМС {AMS.Name} {AMS.Position.ToString()}" : "";
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
                if (range.Position.IsEmpty)
                {
                    FormSelectMapPointDialog fsp = new FormSelectMapPointDialog("Выберите координаты ряда " + range.Name, PointLatLng.Empty);
                    if (fsp.ShowDialog(this) == DialogResult.OK)
                        range.Position = fsp.Result;
                    else
                        return;
                }

                RangeElevator.ProcessRange(range, new ElevatorParameters()
                {
                    FromHeight = old_height,
                    ToHeight = new_height,
                    Coordinates = range.Position,
                    SearchRaduis = checkBoxUseRadius.Checked ? radius : double.NaN,
                    CustomMCoefficient = checkBoxCustomCoeffM.Checked ? m : double.NaN,
                    CustomNCoefficientMonths = MonthsHellmanValues,
                    HellmanCoefficientSource = hellmanCoeffSource
                }, action, actionAfter);

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
                DialogResult = DialogResult.Cancel;
            }
            catch (ApplicationException exc)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, exc.Message + "\r\nПопробуйте уменьшить длину ряда", "Расчет скорости ветра на высоте башни ВЭУ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            hellmanCoeffSource = checkBoxUseRadius.Checked ? HellmanCoefficientSource.AMSAnalog : (checkBoxCustomCoeffM.Checked? HellmanCoefficientSource.CustomOne: HellmanCoefficientSource.AMSAnalog);
            refreshOptionsText();
        }

        private void buttonMonths_Click(object sender, EventArgs e)
        {
            FormMonthsValuesDialogs fmv = new FormMonthsValuesDialogs(MonthsHellmanValues, "Коэффициенты Хеллмана по месяцам");
            if (fmv.ShowDialog() == DialogResult.OK)
            {
                this.MonthsHellmanValues = fmv.Result;
                textBoxCoeffM.Enabled = false;
                hellmanCoeffSource = HellmanCoefficientSource.CustomMonths;
            }
            else
                hellmanCoeffSource = HellmanCoefficientSource.CustomOne;
            refreshOptionsText();
        }

    }
}
