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
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.Lib.Operations;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Calculations;
using WindEnergy.Lib.Statistic.Collections;
using WindEnergy.Lib.Statistic.Structures;
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
        private StatisticalRange<WindDirections> stat_directions;

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
            comboBoxYear.Items.Add("Все");
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
                MessageBox.Show(this, exx.Message, "Расчёт энергетических характеристик", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            sf.Filter = "Файл MS Excel с разделителями-запятыми *.csv | *.csv";
            sf.AddExtension = true;
            if (sf.ShowDialog(this) == DialogResult.OK)
            {
                Vars.Options.LastDirectory = Path.GetDirectoryName(sf.FileName);
                //формирование заголовка
                string cap = "Год;Месяц;кол-во изм";
                foreach (GradationItem grad in Vars.Options.CurrentSpeedGradation.Items)
                    cap += ";" + grad.Average.ToString("0.00");
                cap += ";Vmin, м/с;Vmax, м/с;Vср, м/с;Cv(V);параметр γ;параметр β;Nвал уд., Вт/м^2;Эвал уд., Вт*ч/м^2";
                foreach (WindDirections wd in WindDirections.Calm.GetEnumItems().GetRange(0, 17))
                    cap += ";" + wd.Description();

                //запись в файл
                MSExcel.SaveEnergyInfoCSV(sf.FileName, null, null, null, null, cap, "", "", 0, false); //запись заголовка

                //запись данных обо всём периоде
                EnergyInfo ri1 = StatisticEngine.ProcessRange(range);
                StatisticalRange<WindDirections> sd1 = StatisticEngine.GetDirectionExpectancy(range, GradationInfo<WindDirections>.Rhumb16Gradations);
                StatisticalRange<GradationItem> ss1 = StatisticEngine.GetExpectancy(range, Vars.Options.CurrentSpeedGradation);
                EnergyInfo ei1 = StatisticEngine.ProcessRange(ss1);
                MSExcel.SaveEnergyInfoCSV(sf.FileName, ri1, ei1, sd1, ss1, null, "Все года","Все месяцы", range.Count, true);

                //запись данных для каждого года
                foreach (int year in years) //цикл по годам
                {
                    //для каждого месяца в году
                    for (int mt = 0; mt <= 12; mt++)//по месяцам, начиная со всех
                    {
                        Months month = (Months)mt;
                        RawRange rn = getRange(false, true, DateTime.Now, DateTime.Now, year, month.Description());
                        if (rn == null || rn.Count == 0)
                            continue;
                        EnergyInfo ri = StatisticEngine.ProcessRange(rn);
                        StatisticalRange<WindDirections> sd = StatisticEngine.GetDirectionExpectancy(rn, GradationInfo<WindDirections>.Rhumb16Gradations);
                        StatisticalRange<GradationItem> ss = StatisticEngine.GetExpectancy(rn, Vars.Options.CurrentSpeedGradation);
                        EnergyInfo ei = StatisticEngine.ProcessRange(ss);
                        MSExcel.SaveEnergyInfoCSV(sf.FileName, ri, ei, sd, ss, null, year.ToString(), month.Description(), rn.Count, true);
                    }
                }
                Process.Start(sf.FileName);
            }
        }

        /// <summary>
        /// обновление всех элементов в соответствии с выбранными параметрами
        /// </summary>
        private void refreshInfo()
        {
            RawRange tempr = getRange(
                radioButtonSelectPeriod.Checked,
                radioButtonSelectYearMonth.Checked,
                dateTimePickerFrom.Value,
                dateTimePickerTo.Value,
                comboBoxYear.SelectedItem,
                comboBoxMonth.SelectedItem
                );
            tempr.Position = range.Position;
            tempr.Name = range.Name;
            if (tempr == null)
                throw new Exception("что-то совсем не так!!");

            //расчет параметров
            try
            {
                range_info = StatisticEngine.ProcessRange(tempr);
                stat_speeds = StatisticEngine.GetExpectancy(tempr, Vars.Options.CurrentSpeedGradation);
                stat_directions = StatisticEngine.GetDirectionExpectancy(tempr, GradationInfo<WindDirections>.Rhumb16Gradations);
                exp_info = StatisticEngine.ProcessRange(stat_speeds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Расчёт энергетических характеристик", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (range_info == null || exp_info == null)
            {
                MessageBox.Show(this, "Для заданного ряда невозможно рассчитать характеристики на выбранном интервале", "Расчёт энергетических характеристик", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //вывод параметров
            labelEnergyDensity.Text = (range_info.EnergyDensity / 1000).ToString("0.00") + " кВт*ч/м^2";
            labelPowerDensity.Text = range_info.PowerDensity.ToString("0.00") + " Вт/м^2";
            labelCv.Text = range_info.Cv.ToString("0.000");
            labelV0.Text = range_info.V0.ToString("0.0") + " м/с";
            labelVmax.Text = range_info.Vmax.ToString("0.0") + " м/с";
            labelVmin.Text = range_info.Vmin.ToString("0.0") + " м/с";
            labelGamma.Text = range_info.VeybullGamma.ToString("0.000");
            labelBeta.Text = range_info.VeybullBeta.ToString("0.000") + " м/с";
            labelAirDensity.Text = tempr.AirDensity.ToString("0.000") + " кг/м^3" + (Vars.Options.CalculateAirDensity ? " (рассчёт)" : "");

            labelEnergyDensityTV.Text = (exp_info.EnergyDensity / 1000).ToString("0.00") + " кВт*ч/м^2";
            labelPowerDensityTV.Text = exp_info.PowerDensity.ToString("0.00") + " Вт/м^2";
            labelCvTV.Text = exp_info.Cv.ToString("0.000");
            labelV0TV.Text = exp_info.V0.ToString("0.0") + " м/с";
            labelGammaTV.Text = exp_info.VeybullGamma.ToString("0.000");
            labelBetaTV.Text = exp_info.VeybullBeta.ToString("0.000") + " м/с";
        }


        /// <summary>
        /// выбор ряда из исходного по заданной фильтрации
        /// </summary>
        /// <param name="isPeriod">истина, если надо выбрать период от fromDate до toDate</param>
        /// <param name="isYearMonth">истина, если надо выбрать по месяцу и году</param>
        /// <param name="fromDate">начальная дата</param>
        /// <param name="toDate">конечная дата</param>
        /// <param name="Year">значение selectedItem в combobox года(СТРОКА)</param>
        /// <param name="Month">значение selectedItem в combobox месяца(СТРОКА)</param>
        /// <returns></returns>
        private RawRange getRange(bool isPeriod, bool isYearMonth, DateTime fromDate, DateTime toDate, object Year, object Month)
        {
            if (isPeriod != !isYearMonth)
                return null;

            RawRange res = null;

            //для выбранного периода времени
            if (isPeriod)
            {
                res = new RawRange((from ttt in range
                                    where ttt.Date > fromDate && ttt.Date < toDate
                                    orderby ttt.Date
                                    select ttt).ToList());
            }
            else
            {
                //для выбранного года или месяцев
                if (isYearMonth)
                {
                    Months mt = (Months)(new EnumTypeConverter<Months>().ConvertFrom(Month));
                    int month = (int)mt;

                    if (month == 0) //любой месяц
                    {
                        if (Year.GetType() == typeof(string) && (string)Year == "Все") //любой год, любой месяц
                            res = new RawRange((from ttt in range
                                                orderby ttt.Date
                                                select ttt).ToList());
                        else //любой месяц, заданный год
                        {
                            int yr = (int)Year;
                            res = new RawRange((from ttt in range
                                                where ttt.Date.Year == yr
                                                orderby ttt.Date
                                                select ttt).ToList());
                        }
                    }
                    else //заданный месяц
                    {
                        if (Year.GetType() == typeof(string) && (string)Year == "Все") //любой год, заданный месяц
                            res = new RawRange((from ttt in range
                                                where ttt.Date.Month == month
                                                orderby ttt.Date
                                                select ttt).ToList());
                        else //заданный месяц, заданный год
                        {
                            int yr = (int)Year;
                            res = new RawRange((from ttt in range
                                                where ttt.Date.Year == yr && ttt.Date.Month == month
                                                orderby ttt.Date
                                                select ttt).ToList());
                        }
                    }
                }
            }
            return res;
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
            spane.AddCurve("t(V)", slist, Color.Red);
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
                string txt = ((WindDirections)stat_directions.Keys[i]).Description();
                double x = r * Math.Sin(((i) * 22.5d) * Math.PI / 180d);
                double y = r * Math.Cos(((i) * 22.5d) * Math.PI / 180d);
                TextObj t = new TextObj(txt, x, y);
                dpane.GraphObjList.Add(t);
            }
            dpane.AddCurve("t(DD)", dlist, Color.Blue);
            zedGraphControlDirection.AxisChange();
            zedGraphControlDirection.Invalidate();
        }
    }
}
