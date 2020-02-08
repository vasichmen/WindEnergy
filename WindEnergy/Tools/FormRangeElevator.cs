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
using WindEnergy.WindLib.Transformation.Altitude;
using WindLib;

namespace WindEnergy.UI.Tools
{
    public partial class FormRangeElevator : Form
    {
        private RawRange range = null;

        /// <summary>
        /// результат работы диалогового окна (новый ряд данных)
        /// </summary>
        public RawRange Result { get; private set; }

        public FormRangeElevator(RawRange range)
        {
            InitializeComponent();
            this.range = range;
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

            Action<RawRange> actionAfter = new Action<RawRange>((rawRange) =>
            {
                _ = this.Invoke(new Action(() =>
                {
                    rawRange.Name = "Ряд на высоте " + new_height + " м";
                    _ = MessageBox.Show(this, $"Ряд поднят на высоту {new_height} м", "Расчет скорости ветра на высоте башни ВЭУ", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                RangeElevator.ProcessRange(range, new ElevatorParameters() { 
                    FromHeight = old_height, 
                    ToHeight = new_height, 
                    Coordinates = range.Position, 
                    SearchRaduis = checkBoxUseRadius.Checked? radius:double.NaN,
                    CustomMCoefficient = checkBoxCustomCoeffM.Checked?m:double.NaN
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

        }

        private void checkBoxUseRadius_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRadius.Enabled = checkBoxUseRadius.Checked;
            checkBoxCustomCoeffM.Enabled = !checkBoxUseRadius.Checked;
        }
    }
}
