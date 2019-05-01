using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.Lib.Operations;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Calculations;
using WindEnergy.Lib.Statistic.Structures;
using WindEnergy.UI.Dialogs;

namespace WindEnergy.UI.Tools
{
    public partial class FormRepairRange : Form
    {
        private RawRange range = null;
        private QualityInfo rangeQuality;
        private List<InterpolateMethods> availableMethods;

        /// <summary>
        /// результат работы диалогового окна (новый ряд данных)
        /// </summary>
        public RawRange Result { get; private set; }


        public FormRepairRange(RawRange range, List<InterpolateMethods> methods, string caption)
        {
            InitializeComponent();
            this.range = range;
            this.availableMethods = methods;
            this.Text = caption;
            this.groupBoxMain.Text = caption;

            comboBoxInterpolateMethod.Items.Clear();
            foreach (var item in availableMethods)
                comboBoxInterpolateMethod.Items.Add(item.Description());

            comboBoxRepairInterval.Items.Clear();
            foreach (var item in StandartIntervals.H1.GetEnumItems())
                if ((StandartIntervals)item != StandartIntervals.Variable && (StandartIntervals)item != StandartIntervals.Variable)
                    comboBoxRepairInterval.Items.Add(item.Description());
        }


        #region восстановление ряда

        /// <summary>
        /// кнопка восстановить ряд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRepairRange_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            InterpolateMethods method = (InterpolateMethods)(new EnumTypeConverter<InterpolateMethods>().ConvertFrom(comboBoxInterpolateMethod.SelectedItem));
            StandartIntervals interval = (StandartIntervals)(new EnumTypeConverter<StandartIntervals>().ConvertFrom(comboBoxRepairInterval.SelectedItem));
            Action<int> action = new Action<int>((percent) =>
            {
                try
                {
                    if (this.InvokeRequired)
                        this.Invoke(new Action(() => { progressBar1.Value = percent; }));
                    else
                        progressBar1.Value = percent;
                }
                catch (Exception) { }
            });

            Action<RawRange> actionAfter = new Action<RawRange>((rawRange) =>
            {
                this.Invoke(new Action(() =>
                {
                    rawRange.Name = "Восстановленный ряд до интервала" + interval.Description();
                    MessageBox.Show(this, $"Ряд восстановлен до интервала {interval.Description()}", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                //если выбрана ступенчатый метод или линейная интерполяция и в ряде есть пропуски больше, чем 1 интервал, то надо уточнить у пользователя
                if ((method == InterpolateMethods.Linear || method == InterpolateMethods.Stepwise) && rangeQuality.MaxEmptySpace.TotalMinutes > ((int)interval))
                {
                    if (MessageBox.Show(this, "Ряд содержит пропуски данных больше, чем один выбранный интервал наблюдений.\r\nВ таком случае не рекомендуется использовать линейную интерполяцию и ступенчатое восстановление.\r\nВы уверены, что хотите продолжить восстановление?", "Восстановление ряда", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }

                RawRange baseRange = null;
                if (method == InterpolateMethods.NearestMeteostation)
                {
                    if (radioButtonSelPoint.Checked)
                    {
                        if (range.Position.IsEmpty)
                        {
                            FormSelectMapPointDialog fsp = new FormSelectMapPointDialog("Выберите координаты ряда " + range.Name, PointLatLng.Empty);
                            if (fsp.ShowDialog(this) == DialogResult.OK)
                                range.Position = fsp.Result;
                            else
                                return;
                        }
                    }
                    else
                    {
                        OpenFileDialog of = new OpenFileDialog();
                        of.InitialDirectory = Vars.Options.LastDirectory;
                        of.Filter = "Файл csv|*.csv";
                        if (of.ShowDialog(this) == DialogResult.OK)
                        {
                            Vars.Options.LastDirectory = Path.GetDirectoryName(of.FileName);
                            try
                            {
                                baseRange = MSExcel.LoadCSV(of.FileName);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(this, ex.Message, "Открытие файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                            return;
                    }
                }
                Restorer.ProcessRange(range, new RestorerParameters() { Interval = interval, Method = method, Coordinates = range.Position, BaseRange = baseRange }, action, actionAfter);

            }
            catch (WebException exc)
            {
                Cursor = Cursors.Arrow;
                MessageBox.Show(this, exc.Message, "Восстановление ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
            catch (WindEnergyException wex)
            {
                Cursor = Cursors.Arrow;
                MessageBox.Show(this, wex.Message, "Восстановление ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
            catch (ApplicationException exc)
            {
                Cursor = Cursors.Arrow;
                MessageBox.Show(this, exc.Message + "\r\nПопробуйте уменьшить длину ряда", "Восстановление ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }

        }

        #endregion

        private void formRepairRange_Shown(object sender, EventArgs e)
        {
            comboBoxInterpolateMethod.SelectedIndex = 0;
            comboBoxRepairInterval.SelectedIndex = 0;

            rangeQuality = Qualifier.ProcessRange(range);

            labelCompletness.Text = "Полнота ряда: " + (rangeQuality.Completeness * 100).ToString("0.00") + "%";
            labelMaxEmptySpace.Text = "Максимальный перерыв в измерениях: " + rangeQuality.MaxEmptySpace.TotalDays.ToString("0.000") + " дней";
            labelRangeLength.Text = "Длительность ряда: " + range.Length.ToText();

            string dt;
            if (rangeQuality.Intervals.Count > 1)
            {
                StandartIntervals min = rangeQuality.Intervals.Min<RangeInterval, StandartIntervals>((i) => i.Interval);
                StandartIntervals max = rangeQuality.Intervals.Max<RangeInterval, StandartIntervals>((i) => i.Interval);
                dt = $"минимальный: {min.Description()}, максимальный: {max.Description()}";
            }
            else
                dt = rangeQuality.Intervals[0].Interval.Description();
            labelInterval.Text = "Δt: " + dt;
        }


        private void comboBoxInterpolateMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = (InterpolateMethods)(new EnumTypeConverter<InterpolateMethods>().ConvertFrom(comboBoxInterpolateMethod.SelectedItem)) == InterpolateMethods.NearestMeteostation;
        }
    }
}
