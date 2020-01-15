using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.WindLib.Classes;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Data.Providers;
using WindEnergy.WindLib.Data.Providers.FileSystem;
using WindEnergy.WindLib.Operations.Structures;
using WindEnergy.WindLib.Statistic.Calculations;
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
            try
            {
                years = YearCalculator.ProcessRange(range);
                dataGridViewExt1.DataSource = years.Years;
                dataGridViewExt1.ReadOnly = true;

                if (years.RecomendedYear != null) //если расчётный год найден
                {
                    labelRecomendedYear.Text = "Рекомендуется в качестве расчетного принять " + years.RecomendedYear.Year + " год:";
                    labelAverageCalcYearSpeed.Text = "Средняя скорость: " + years.RecomendedYear.AverageSpeed.ToString("0.0") + " м/с";
                    labelCompletness.Text = "Полнота ряда: " + years.RecomendedYear.Completness.ToString("0.00") + " %";
                    labelExpectDeviation.Text = "Отклонение повторяемости скорости: " + years.RecomendedYear.ExpectancyDeviation.ToString("0.00") + "%";
                    labelInterval.Text = "Δt: " + years.RecomendedYear.Interval.Description() + "";
                    labelMaxSpeed.Text = "Максимальная скорость: " + years.RecomendedYear.Vmax.ToString("0.0") + " м/с";
                    labelSpeedDeviation.Text = "Отклонение скорости от многолетней: " + years.RecomendedYear.SpeedDeviation.ToString("0.00") + " м/с";
                }
                else //если расчётный год не найден
                {
                    _ = MessageBox.Show(this, "Не удалось найти расчётный год.\r\nРяд очень маленький или не содержит года необходимого качества");
                    labelAverageCalcYearSpeed.Text = "Средняя скорость: ";
                    labelCompletness.Text = "Полнота ряда: ";
                    labelExpectDeviation.Text = "Отклонение повторяемости скорости: ";
                    labelInterval.Text = "Δt: ";
                    labelMaxSpeed.Text = "Максимальная скорость: ";
                    labelSpeedDeviation.Text = "Отклонение скорости от многолетней: ";
                }
                labelAverageYearsSpeed.Text = "Среднемноголетняя скорость: " + years.AverageSpeed.ToString("0.0") + " м/с";
            }
            catch (ArgumentException wex)
            {
                _ = MessageBox.Show(this, wex.Message, "Выбор расчётного года", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        /// <summary>
        /// сохранение всех результатов в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveResults_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.InitialDirectory = Vars.Options.LastDirectory;
            sf.Filter = "Файл MS Excel *.xlsx | *.xlsx";
            sf.Filter += "|Файл *.csv | *.csv";
            sf.AddExtension = true;
            sf.FileName = "Выбор расчётного года для " + range.Name;
            sf.AddExtension = true;
            if (sf.ShowDialog(this) == DialogResult.OK)
            {
                Vars.Options.LastDirectory = Path.GetDirectoryName(sf.FileName);
                FileProvider provider;
                switch (Path.GetExtension(sf.FileName))
                {
                    case ".csv":
                        provider = new CSVFile();
                        break;
                    case ".xlsx":
                        provider = new ExcelFile();
                        break;
                    default: throw new Exception("Этот тип файла не реализован");
                }
                provider.SaveCalcYearInfo(sf.FileName, years);
                _ = Process.Start(sf.FileName);
            }
        }

        /// <summary>
        /// переименование столбцов таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewExt1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e= e ?? throw new ArgumentNullException(nameof(e));
            switch (e.Column.Name.ToLower())
            {
                case "year":
                    e.Column.HeaderText = "Год";
                    e.Column.Width = 40;
                    break;
                case "interval":
                    e.Column.HeaderText = "Δt";
                    e.Column.CellTemplate = new DataGridViewComboboxCell<StandartIntervals>();
                    e.Column.Width = 80;
                    break;
                case "completness":
                    e.Column.HeaderText = "Полнота ряда, %";
                    e.Column.DefaultCellStyle.Format = "n2";
                    e.Column.Width = 80;
                    break;
                case "speeddeviation":
                    e.Column.HeaderText = "Отклонение скорости, м/с";
                    e.Column.DefaultCellStyle.Format = "n2";
                    e.Column.Width = 120;
                    break;
                case "expectancydeviation":
                    e.Column.HeaderText = "Отклонение повторяемости, %";
                    e.Column.DefaultCellStyle.Format = "n2";
                    e.Column.Width = 130;
                    break;
                case "averagespeed":
                    e.Column.HeaderText = "Средняя скорость, м/с";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "speeddeviationpercent":
                    e.Column.HeaderText = "Отклонение скорости, %";
                    e.Column.DefaultCellStyle.Format = "n2";
                    break;

                //удаляемые колонки пишем тут:
                case "from":
                case "to":
                case "vmax":
                    e.Column.DataGridView.Columns.Remove(e.Column);
                    break;
                default: throw new Exception("Для этой колонки нет названия");
            }
        }
    }
}
