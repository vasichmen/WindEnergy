using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Calculations;
using WindEnergy.UI.Ext;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// окно выбора расчётного года
    /// </summary>
    public partial class FormCalcYear : Form
    {
        private RawRange range;
        private CalculateYearInfo years;

        /// <summary>
        /// создаёт окно с заданным рядом
        /// </summary>
        /// <param name="rang"></param>
        public FormCalcYear(RawRange rang)
        {
            InitializeComponent();
            range = rang;
        }

        /// <summary>
        /// расчёт информации и вывод вариантов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formCalcYear_Shown(object sender, EventArgs e)
        {
            years = YearCalculator.ProcessRange(range);
            dataGridViewExt1.DataSource = years.Years;
            dataGridViewExt1.ReadOnly = true;

            if (years.RecomendedYear != null) //если расчётный год найден
            {
                labelRecomendedYear.Text = "Рекомендуется в качестве расчетного принять "+years.RecomendedYear.Year+" год:";
                labelAverageSpeed.Text = "Средняя скорость: " + years.RecomendedYear.AverageSpeed.ToString("0.0") + " м/с";
                labelCompletness.Text = "Полнота ряда: " + years.RecomendedYear.Completness.ToString("0.00") + " %";
                labelExpectDeviation.Text = "Отклонение повторяемости скорости: " + years.RecomendedYear.ExpectancyDeviation.ToString("0.00") + "";
                labelInterval.Text = "Δt: " + years.RecomendedYear.Interval.Description() + "";
                labelMaxSpeed.Text = "Максимальная скорость: " + years.RecomendedYear.Vmax.ToString("0.0") + " м/с";
                labelSpeedDeviation.Text = "Отклонение скорости от многолетней: " + years.RecomendedYear.SpeedDeviation.ToString("0.00") + " м/с";
            }
            else //если расчётный год не найден
            {
                labelAverageSpeed.Text = "Средняя скорость: ";
                labelCompletness.Text = "Полнота ряда: ";
                labelExpectDeviation.Text = "Отклонение повторяемости скорости: ";
                labelInterval.Text = "Δt: ";
                labelMaxSpeed.Text = "Максимальная скорость: ";
                labelSpeedDeviation.Text = "Отклонение скорости от многолетней: ";
            }

        }

        private void buttonSaveResults_Click(object sender, EventArgs e)
        {
            //TODO: сохранение всех результатов в файл
        }

        /// <summary>
        /// переименование столбцов таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewExt1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            //Interval = qinfo.Intervals.Count == 1 ? qinfo.Intervals[0].Interval : StandartIntervals.Variable,
            //        Completness = qinfo.Completeness,
            //        Vmax = r.Max((t) => t.Speed),
            //        Year = r[0].Date.Year,
            //        From = r[0].Date,
            //        To = r[r.Count - 1].Date,
            //        SpeedDeviation = dinfo.SpeedDeviation,
            //        ExpectancyDeviation = dinfo.ExpDeviation,
            //        AverageSpeed = aver
            //    };

            switch (e.Column.Name.ToLower())
            {
                case "year":
                    e.Column.HeaderText = "Год";
                    e.Column.Width = 40;
                    break;
                case "interval":
                    e.Column.HeaderText = "Δt";
                    e.Column.CellTemplate = new DataGridViewComboboxCell<StandartIntervals>();
                    break;
                case "completness":
                    e.Column.HeaderText = "Полнота ряда, %";
                    e.Column.DefaultCellStyle.Format = "n2";
                    e.Column.Width = 80;
                    break;
                case "vmax":
                    e.Column.HeaderText = "Максимальная корость, м/с";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "speeddeviation":
                    e.Column.HeaderText = "Отклонение скорости";
                    e.Column.DefaultCellStyle.Format = "n2";
                    break;
                case "expectancydeviation":
                    e.Column.HeaderText = "Отклонение повторяемости";
                    e.Column.DefaultCellStyle.Format = "n2";
                    break;
                case "averagespeed":
                    e.Column.HeaderText = "Средняя скорость, м/с";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;

                //удаляемые колонки пишем тут:
                case "from":
                case "to":
                    e.Column.DataGridView.Columns.Remove(e.Column);
                    break;
                default: throw new Exception("Для этой колонки нет названия");
            }
        }
    }
}
