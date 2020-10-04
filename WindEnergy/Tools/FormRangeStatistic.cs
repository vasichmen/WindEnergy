using CommonLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WindEnergy.UI.Ext;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Data.Providers.FileSystem;
using WindEnergy.WindLib.Statistic.Structures;
using WindLib;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// окно статистики наблюдений в ряде
    /// </summary>
    public partial class FormRangeStatistic : Form
    {
        private RawRange range;
        private QualityInfo qualityInfo;

        /// <summary>
        /// создаёт новое окно с заданным рядом наблюдений
        /// </summary>
        /// <param name="rang"></param>
        public FormRangeStatistic(RawRange rang)
        {
            rang = rang ?? throw new ArgumentNullException(nameof(rang));
            InitializeComponent();
            dataGridViewExt1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Text = rang.Name + " - Статистика ряда";
            range = rang;
        }

        private void FormRangeStatistic_Shown(object sender, EventArgs e)
        {
            range.PerformRefreshQuality();
            qualityInfo = range.Quality;
            labelCompletness.Text = "Полнота ряда: " + (qualityInfo.Completeness * 100).ToString("0.0") + "%";
            labelExpectAmount.Text = "Ожидаемое число измерений: " + qualityInfo.ExpectAmount.ToString() + " штук";
            labelLength.Text = "Длительность ряда наблюдений: " + range.Length.ToText();
            labelMaxEmpty.Text = "Максимальная длительность пропуска данных: " + qualityInfo.MaxEmptySpace.ToText();
            labelMeasureAmount.Text = "Общее количество наблюдений: " + qualityInfo.MeasureAmount.ToString() + " штук";
            dataGridViewExt1.DataSource = null;
            dataGridViewExt1.DataSource = qualityInfo.Intervals;
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

        /// <summary>
        /// сохранение статистики в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.InitialDirectory = Vars.Options.LastDirectory;
            sf.Filter = "Файл MS Excel *.xlsx | *.xlsx";
            sf.Filter += "|Файл *.csv | *.csv";
            sf.AddExtension = true;
            sf.FileName = "Статистика наблюдений для  " + range.Name;
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
                provider.SaveRangeQualityInfo(sf.FileName, qualityInfo, range.Length);
                _ = Process.Start(sf.FileName);
            }
        }
    }
}
