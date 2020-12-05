using CommonLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data.Providers.FileSystem;
using WindEnergy.WindLib.Statistic.Calculations;
using WindEnergy.WindLib.Statistic.Collections;
using WindEnergy.WindLib.Statistic.Structures;
using WindLib;
using ZedGraph;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// окно основных энергетических характеристик
    /// </summary>
    public partial class FormEnergyInfo : Form
    {
        /// <summary>
        /// если истина, то окно загружено и произвеены первоначальные настройки элементов
        /// </summary>
        private bool ready = false;

        /// <summary>
        /// исходный ряд, для которого ведётся расчёт
        /// </summary>
        private readonly RawRange range;

        /// <summary>
        /// список лет в загруженном ряду
        /// </summary>
        private List<object> years;

        /// <summary>
        /// повторяемости скоростей
        /// </summary>
        private StatisticalRange<GradationItem> stat_speeds;

        /// <summary>
        ///  повторяекмости направлений
        /// </summary>
        private StatisticalRange<WindDirections16> stat_directions;

        /// <summary>
        /// характеристики по всему ряду
        /// </summary>
        private EnergyInfo range_info;

        /// <summary>
        /// характеристики по градациям
        /// </summary>
        private EnergyInfo exp_info;

        /// <summary>
        /// создаёт новое окно с заданным рядом
        /// </summary>
        /// <param name="rang">ряд наблюдений, для которого расчитываются характеристики</param>
        public FormEnergyInfo(RawRange rang)
        {
            rang = rang ?? throw new ArgumentNullException(nameof(rang));
            InitializeComponent();
            this.range = rang;
            Text = rang.Name;
            radioButtonSelectPeriod.CheckedChanged += radioButtonSelect_CheckedChanged;
            radioButtonSelectYearMonth.CheckedChanged += radioButtonSelect_CheckedChanged;
            dateTimePickerFrom.ValueChanged += dateTimePickerFrom_ValueChanged;
            dateTimePickerTo.ValueChanged += dateTimePickerTo_ValueChanged;
        }

        /// <summary>
        /// обновление пределов выбора дат начала и конца периода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerFrom.MaxDate = dateTimePickerTo.Value;
        }

        /// <summary>
        /// обновление пределов выбора дат начала и конца периода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerTo.MinDate = dateTimePickerFrom.Value;
        }

        /// <summary>
        /// расчет характеристик ряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formEnergyInfo_Shown(object sender, EventArgs e)
        {
            radioButtonSelectPeriod.Checked = true;
            DateTime min = range.Min((i) => i.Date);
            DateTime max = range.Max((i) => i.Date);
            dateTimePickerFrom.MinDate = min;
            dateTimePickerTo.MinDate = min;
            dateTimePickerFrom.MaxDate = max;
            dateTimePickerTo.MaxDate = max;
            dateTimePickerFrom.Value = min;
            dateTimePickerTo.Value = max;

            years = new List<object>();
            foreach (RawItem item in range)
            {
                if (!years.Contains(item.Date.Year))
                    years.Add(item.Date.Year);
            }
            _ = comboBoxYear.Items.Add("Все");
            comboBoxYear.Items.AddRange(years.ToArray());
            comboBoxYear.SelectedItem = "Все";

            comboBoxMonth.Items.AddRange(Months.January.GetItems().ToArray());
            comboBoxMonth.SelectedItem = Months.All.Description();

            refreshInfo();
            drawGraphs();
            ready = true;
        }

        /// <summary>
        /// обновление доступности элементов настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonSelect_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxYear.Enabled = radioButtonSelectYearMonth.Checked;
            comboBoxMonth.Enabled = radioButtonSelectYearMonth.Checked;
            dateTimePickerFrom.Enabled = radioButtonSelectPeriod.Checked;
            dateTimePickerTo.Enabled = radioButtonSelectPeriod.Checked;
        }

        /// <summary>
        /// перерасчёт всех характеристик при изменении параметров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxYear_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!ready)
                    return;
                refreshInfo();
                drawGraphs();
            }
            catch (Exception exx)
            {
                _ = MessageBox.Show(this, exx.Message, "Расчёт энергетических характеристик", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// сохранение результатов расчетов в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.InitialDirectory = Vars.Options.LastDirectory;
            sf.Filter = "Файл MS Excel *.xlsx | *.xlsx";
            sf.Filter += "|Файл *.csv | *.csv";
            sf.AddExtension = true;
            sf.FileName = "Расчёт ВЭК для " + range.Name;
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
                provider.SaveEnergyInfo(sf.FileName, range);

                _ = Process.Start(sf.FileName);
            }
        }


        private void buttonOpenRange_Click(object sender, EventArgs e)
        {
            RawRange tempr = range.GetRange(
              radioButtonSelectPeriod.Checked,
              radioButtonSelectYearMonth.Checked,
              dateTimePickerFrom.Value,
              dateTimePickerTo.Value,
              comboBoxYear.SelectedItem,
              comboBoxMonth.SelectedItem
              );
            if (tempr == null)
                throw new Exception("что-то совсем не так!!");
            tempr.Name = range.Name + $" {dateTimePickerFrom.Value:dd.MM.yyyy} - {dateTimePickerTo.Value:dd.MM.yyyy}";

            _ = Program.winMain.mainTabControl.OpenNewTab(tempr, tempr.Name).Focus();
            Close(); 
        }

        /// <summary>
        /// обновление всех элементов в соответствии с выбранными параметрами
        /// </summary>
        private void refreshInfo()
        {
            RawRange tempr = range.GetRange(
                radioButtonSelectPeriod.Checked,
                radioButtonSelectYearMonth.Checked,
                dateTimePickerFrom.Value,
                dateTimePickerTo.Value,
                comboBoxYear.SelectedItem,
                comboBoxMonth.SelectedItem
                );
            if (tempr == null)
                throw new Exception("что-то совсем не так!!");

            //расчет параметров
            try
            {
                range_info = StatisticEngine.ProcessRange(tempr);
                stat_speeds = StatisticEngine.GetExpectancy(tempr, Vars.Options.CurrentSpeedGradation);
                stat_directions = StatisticEngine.GetDirectionExpectancy(tempr, GradationInfo<WindDirections16>.Rhumb16Gradations);
                exp_info = StatisticEngine.ProcessRange(stat_speeds);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(this, ex.Message, "Расчёт энергетических характеристик", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (range_info == null || exp_info == null)
            {
                _ = MessageBox.Show(this, "Для заданного ряда невозможно рассчитать характеристики на выбранном интервале", "Расчёт энергетических характеристик", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //вывод параметров
            labelEnergyDensity.Text = (range_info.EnergyDensity / 1000).ToString("0.00") + " кВт*ч/м^2";
            labelPowerDensity.Text = range_info.PowerDensity.ToString("0.00") + " Вт/м^2";
            labelCv.Text = range_info.Cv.ToString("0.000");
            labelV0.Text = range_info.V0.ToString("0.0") + " м/с";
            labelExtremalSpeed.Text = range_info.ExtremalSpeed.ToString("0.0") + " м/с";
            labelVmax.Text = range_info.Vmax.ToString("0.0") + " м/с";
            labelVmin.Text = range_info.Vmin.ToString("0.0") + " м/с";
            labelStandardDeviationSpeed.Text = range_info.StandardDeviationSpeed.ToString("0.000") + " м/с";
            labelGamma.Text = range_info.VeybullGamma.ToString("0.000");
            labelBeta.Text = range_info.VeybullBeta.ToString("0.000") + " м/с";
            labelAirDensity.Text = tempr.AirDensity.ToString("0.000") + " кг/м^3" + (Vars.Options.CalculateAirDensity ? " (расчёт)" : "");

            labelEnergyDensityTV.Text = (exp_info.EnergyDensity / 1000).ToString("0.00") + " кВт*ч/м^2";
            labelPowerDensityTV.Text = exp_info.PowerDensity.ToString("0.00") + " Вт/м^2";
            labelCvTV.Text = exp_info.Cv.ToString("0.000");
            labelV0TV.Text = exp_info.V0.ToString("0.0") + " м/с";
            labelGammaTV.Text = exp_info.VeybullGamma.ToString("0.000");
            labelBetaTV.Text = exp_info.VeybullBeta.ToString("0.000") + " м/с";
        }




        /// <summary>
        /// обновление графиков на основе заданных stat_speeds и stat_directions
        /// </summary>
        private void drawGraphs()
        {
            if (stat_directions == null || stat_speeds == null)
                return;
            //РАСПРЕДЕЛНИЕ СКОРОСТЕЙ ВЕТРА
            GraphPane spane = zedGraphControlSpeed.GraphPane;
            spane.Title.Text = "t(V), %";
            spane.XAxis.Title.Text = "Vj, м/с";
            spane.YAxis.Title.Text = "t(V), %";
            spane.GraphObjList.Clear();
            spane.CurveList.Clear();
            PointPairList slist = new PointPairList();
            for (int i = 0; i < stat_speeds.Values.Count; i++)
                slist.Add((stat_speeds.Keys[i] as GradationItem).Average, stat_speeds.Values[i] * 100);
            _ = spane.AddCurve("t(V)", slist, Color.Red);
            zedGraphControlSpeed.AxisChange();
            zedGraphControlSpeed.Invalidate();

            //РАСПРЕДЕЛЕНИЕ НАПРАВЛЕНИЙ
            RadarPointList dlist = new RadarPointList();
            GraphPane dpane = zedGraphControlDirection.GraphPane;
            dpane.Title.Text = "t(DD), %";
            dpane.XAxis.Title.IsVisible = false;
            dpane.YAxis.Title.IsVisible = false;
            dpane.XAxis.MajorGrid.IsVisible = true;
            dpane.YAxis.MajorGrid.IsVisible = true;
            dpane.YAxis.MajorGrid.IsZeroLine = false;
            dpane.CurveList.Clear();
            dpane.GraphObjList.Clear();
            dlist.Clockwise = true;
            for (int i = 0; i < 16; i++)
            {
                double r = stat_directions.Values[i] * 100;
                dlist.Add(r, 1);
                string txt = ((WindDirections16)stat_directions.Keys[i]).Description();
                double x = r * Math.Sin(((i) * 22.5d) * Math.PI / 180d);
                double y = r * Math.Cos(((i) * 22.5d) * Math.PI / 180d);
                TextObj t = new TextObj(txt, x, y);
                dpane.GraphObjList.Add(t);
            }
            _ = dpane.AddCurve("t(DD)", dlist, Color.Blue);
            zedGraphControlDirection.AxisChange();
            zedGraphControlDirection.Invalidate();
        }
    }
}
