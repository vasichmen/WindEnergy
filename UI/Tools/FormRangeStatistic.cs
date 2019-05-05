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
using WindEnergy.Lib.Statistic.Structures;
using WindEnergy.UI.Ext;

namespace WindEnergy.UI.Tools
{
    public partial class FormRangeStatistic : Form
    {
        private RawRange range;

        public FormRangeStatistic(RawRange rang)
        {
            InitializeComponent();
            Text = rang.Name + " - Статистика ряда";
            range = rang;
        }

        private void FormRangeStatistic_Shown(object sender, EventArgs e)
        {
            range.PerformRefreshQuality();
            QualityInfo qi = range.Quality;
            labelCompletness.Text = "Полнота ряда: " + (qi.Completeness * 100).ToString("0.0")+"%";
            labelExpectAmount.Text = "Ожидаемое число измерений: " + qi.ExpectAmount.ToString()+ " штук";
            labelLength.Text = "Длительность ряда наблюдений: " + range.Length.ToText();
            labelMaxEmpty.Text = "Максимальная длительность пропуска данных: " + qi.MaxEmptySpace.ToText();
            labelMeasureAmount.Text = "Общее количество наблюдений: " + qi.MeasureAmount.ToString()+" штук";
            dataGridViewExt1.DataSource = null;
            dataGridViewExt1.DataSource = qi.Intervals;
        }

        /// <summary>
        /// переименование столбцов таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewExt1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            switch (e.Column.Name.ToLower())
            {
                case "interval":
                    e.Column.HeaderText = "Δt";
                    e.Column.CellTemplate = new DataGridViewComboboxCell<StandartIntervals>();
                    break;
                case "diapason":
                    e.Column.HeaderText = "Диапазон наблюдений";
                    e.Column.Width = 250;
                    break;
                case "lengthstring":
                    e.Column.HeaderText = "Длительность диапазона";
                    e.Column.Width = 150;
                    break;
                //удаляемые колонки пишем тут:
                case "lengthminutes":
                case "length":
                    e.Column.DataGridView.Columns.Remove(e.Column);
                    break;
                default: throw new Exception("Для этой колонки нет названия");
            }
        }

    }
}
