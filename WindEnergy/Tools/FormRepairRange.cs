using CommonLib;
using CommonLib.Classes;
using CommonLib.UITools;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using WindEnergy.UI.Properties;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Statistic.Calculations;
using WindEnergy.WindLib.Statistic.Structures;
using WindEnergy.WindLib.Transformation.Restore;
using WindLib;

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


        public FormRepairRange(RawRange range, List<InterpolateMethods> methods, string caption, string buttonText)
        {
            methods = methods ?? throw new ArgumentNullException(nameof(methods));
            InitializeComponent();
            this.range = range;
            this.availableMethods = methods;
            this.Text = caption;
            this.groupBoxMain.Text = caption;
            this.buttonRepairRange.Text = buttonText;

            comboBoxInterpolateMethod.Items.Clear();
            foreach (var item in availableMethods)
                _ = comboBoxInterpolateMethod.Items.Add(item.Description());

            comboBoxRepairInterval.Items.Clear();
            foreach (var item in StandartIntervals.H1.GetEnumItems())
                if ((StandartIntervals)item != StandartIntervals.Variable && (StandartIntervals)item != StandartIntervals.Variable)
                    _ = comboBoxRepairInterval.Items.Add(item.Description());
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
            Action<int, string> action = new Action<int, string>((percent, text) =>
            {
                try
                {
                    if (this.InvokeRequired)
                        _ = Invoke(new Action(() =>
                        {
                            progressBarStatus.Value = percent;
                        }));
                    else
                    {
                        progressBarStatus.Value = percent;
                    }
                }
                catch (Exception) { }
            });

            Action<RawRange, RawRange, double> actionAfter = new Action<RawRange, RawRange, double>((resultRange, baseRange, r) =>
              {
                  _ = this.Invoke(new Action(() =>
                    {
                        string additionalText = "";

                        if (method == InterpolateMethods.NearestMeteostation) //для восстановления ряда выводим доп. информацию
                        {
                            //Информация о коэфф корреляции и базовом ряде
                            if (double.IsNaN(r))
                                additionalText += "Восстановление проводилось на основе ряда наблюдений, заданного пользователем\r\n";
                            else
                                additionalText += $"Восстановление проводилось на основе ряда наблюдений {(baseRange.Meteostation != null ? $" на МС {baseRange.Meteostation.Name} " : "")}с коэффициентом корреляции {r:0.00} \r\n";
                            //предупреждение, что не все данные восстановлены
                            RangeInterval baseInterval = baseRange.Quality.Intervals.OrderByDescending((i) => i.LengthMinutes).First(); //выбираем самый длинный интервал наблюдений в базовом ряде
                            if (baseInterval.LengthMinutes > (int)interval) //если максимальный интервал базового ряда больше, чем требуемый интервал восстановления
                                additionalText += $"\r\nВнимание!! Интервал наблюдений ряда, на основе которого производилось восстановление ({baseInterval.Interval.Description()}), больше, чем требуемый интервал. Поэтому не удалось восстановить все значения ряда до {interval.Description()}\r\n";
                        }

                        resultRange.Name = "Восстановленный ряд до интервала" + interval.Description();
                        _ = MessageBox.Show(this, $"Ряд восстановлен до интервала {interval.Description()}\r\n{additionalText}", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (resultRange == null)
                            DialogResult = DialogResult.Cancel;
                        else
                        {
                            DialogResult = DialogResult.OK;
                            Result = resultRange;
                        }
                        Cursor = Cursors.Arrow;
                        Result = resultRange;
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
                            FormSelectMapPointDialog fsp = new FormSelectMapPointDialog("Выберите координаты ряда " + range.Name, PointLatLng.Empty, Vars.Options.CacheFolder, Resources.rp5_marker, Vars.Options.MapProvider);
                            if (fsp.ShowDialog(this) == DialogResult.OK)
                                range.Position = fsp.Result;
                            else
                            {
                                Cursor = Cursors.Arrow;
                                return;
                            }
                        }
                    }
                    else
                    {
                        baseRange = Program.winMain.mainHelper.OpenFile(this);
                        if (baseRange == null)
                        {
                            Cursor = Cursors.Arrow;
                            return;
                        }
                    }
                }
                Restorer.ProcessRange(range, new RestorerParameters()
                {
                    Interval = interval,
                    Method = method,
                    Coordinates = range.Position,
                    BaseRange = baseRange,
                    ReplaceExistMeasurements = checkBoxReplaceExist.Checked
                }, action, actionAfter);

            }
            catch (WebException exc)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, exc.Message, "Восстановление ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
            catch (WindEnergyException wex)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, wex.Message, "Восстановление ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
            catch (ApplicationException exc)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, exc.Message + "\r\nПопробуйте уменьшить длину ряда", "Восстановление ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }

        }

        #endregion

        private void formRepairRange_Shown(object sender, EventArgs e)
        {
            comboBoxInterpolateMethod.SelectedIndex = 0;
            comboBoxRepairInterval.SelectedIndex = 0;

            rangeQuality = Qualifier.ProcessRange(range);
            if (rangeQuality == null)
            {
                _ = MessageBox.Show(this, "Произошла ошибка при открытии ряда. Возможно, ряд слишком короткий", "Открытие ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

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
            checkBoxReplaceExist.Visible = (InterpolateMethods)(new EnumTypeConverter<InterpolateMethods>().ConvertFrom(comboBoxInterpolateMethod.SelectedItem)) == InterpolateMethods.NearestMeteostation;
        }
    }
}
