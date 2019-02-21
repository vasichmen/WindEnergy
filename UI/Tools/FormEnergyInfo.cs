using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        private RawRange range;

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

            List<object> years = new List<object>();
            foreach (RawItem item in range)
            {
                if (!years.Contains(item.Date.Year))
                    years.Add(item.Date.Year);
            }
            comboBoxYear.Items.Add("Все");
            comboBoxYear.Items.AddRange(years.ToArray());
            comboBoxYear.SelectedItem = "Все";

            comboBoxYearMonth.Items.AddRange(Months.January.GetItems().ToArray());
            comboBoxYearMonth.SelectedItem = Months.All.Description();

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
            comboBoxYearMonth.Enabled = radioButtonSelectYearMonth.Checked;
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
            if (!ready)
                return;
            refreshInfo();
            drawGraphs();
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
                MSExcel.SaveEnergyInfoCSV(sf.FileName, range_info, exp_info, stat_directions, stat_speeds);
                Process.Start(sf.FileName);
            }
        }

        /// <summary>
        /// обновление всех элементов в соответствии с выбранными параметрами
        /// </summary>
        private void refreshInfo()
        {
            RawRange tempr = null;

            //для выбранного периода времени
            if (radioButtonSelectPeriod.Checked)
            {
                DateTime fr = dateTimePickerFrom.Value;
                DateTime too = dateTimePickerTo.Value;

                tempr = new RawRange((from ttt in range
                                      where ttt.Date > fr && ttt.Date < too
                                      orderby ttt.Date
                                      select ttt).ToList());
            }
            else
            {
                //для выбранного года или месяцев
                if (radioButtonSelectYearMonth.Checked)
                {
                    Months mt = (Months)(new EnumTypeConverter<Months>().ConvertFrom(comboBoxYearMonth.SelectedItem));
                    int month = (int)mt;

                    if (month == 0) //любой месяц
                    {
                        if (comboBoxYear.SelectedItem.GetType() == typeof(string) && (string)comboBoxYear.SelectedItem == "Все") //любой год, любой месяц
                            tempr = new RawRange((from ttt in range
                                                  orderby ttt.Date
                                                  select ttt).ToList());
                        else //любой месяц, заданный год
                        {
                            int yr = (int)comboBoxYear.SelectedItem;
                            tempr = new RawRange((from ttt in range
                                                  where ttt.Date.Year == yr
                                                  orderby ttt.Date
                                                  select ttt).ToList());
                        }
                    }
                    else //заданный месяц
                    {
                        if (comboBoxYear.SelectedItem.GetType() == typeof(string) && (string)comboBoxYear.SelectedItem == "Все") //любой год, заданный месяц
                            tempr = new RawRange((from ttt in range
                                                  where ttt.Date.Month == month
                                                  orderby ttt.Date
                                                  select ttt).ToList());
                        else //заданный месяц, заданный год
                        {
                            int yr = (int)comboBoxYear.SelectedItem;
                            tempr = new RawRange((from ttt in range
                                                  where ttt.Date.Year == yr && ttt.Date.Month == month
                                                  orderby ttt.Date
                                                  select ttt).ToList());
                        }
                    }
                }
            }
            if (tempr == null)
                throw new Exception("что-то совсем не так!!");

            //расчет параметров
            range_info = StatisticEngine.ProcessRange(tempr);
            stat_speeds = StatisticEngine.GetSpeedExpectancy(tempr, GradationInfo<GradationItem>.VoeykowGradations);
            stat_directions = StatisticEngine.GetDirectionExpectancy(tempr, GradationInfo<WindDirections>.Rhumb16Gradations);
            exp_info = StatisticEngine.ProcessRange(stat_speeds);

            //вывод параметров
            labelEnergyDensity.Text = range_info.EnergyDensity.ToString("0.00") + " Вт*ч/м^2";
            labelPowerDensity.Text = range_info.PowerDensity.ToString("0.00") + " Вт/м^2";
            labelCv.Text = range_info.Cv.ToString("0.000");
            labelV0.Text = range_info.V0.ToString("0.0") + " м/с";
            labelVmax.Text = range_info.Vmax.ToString("0.0") + " м/с";

            labelEnergyDensityTV.Text = exp_info.EnergyDensity.ToString("0.00") + " Вт*ч/м^2";
            labelPowerDensityTV.Text = exp_info.PowerDensity.ToString("0.00") + " Вт/м^2";
            labelCvTV.Text = exp_info.Cv.ToString("0.000");
            labelV0TV.Text = exp_info.V0.ToString("0.0") + " м/с";
        }

        /// <summary>
        /// обновление графиков на основе заданных stat_speeds и stat_directions
        /// </summary>
        private void drawGraphs()
        {
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
            for (int i = 1; i <= 16; i++)
            {
                double r = stat_directions.Values[i] * 100;
                dlist.Add(r, 1);
                string txt = ((WindDirections)stat_directions.Keys[i]).Description();
                double x = r * Math.Sin(((i - 1) * 22.5d) * Math.PI / 180d);
                double y = r * Math.Cos(((i - 1) * 22.5d) * Math.PI / 180d);
                TextObj t = new TextObj(txt, x, y);
                dpane.GraphObjList.Add(t);
            }
            dpane.AddCurve("t(DD)", dlist, Color.Blue);
            zedGraphControlDirection.AxisChange();
            zedGraphControlDirection.Invalidate();

        }


    }
}
