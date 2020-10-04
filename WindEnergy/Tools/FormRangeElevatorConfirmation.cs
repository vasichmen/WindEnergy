using CommonLib;
using System;
using System.Drawing;
using System.Windows.Forms;
using WindEnergy.WindLib.Transformation.Altitude;
using ZedGraph;

namespace WindEnergy.UI.Tools
{
    public partial class FormRangeElevatorConfirmation : Form
    {
        private SuitAMSResult AMSList;
        private SuitAMSResultItem selectedAMS;

        public SuitAMSResultItem Result { get; private set; }

        public FormRangeElevatorConfirmation(SuitAMSResult suitAMSList)
        {
            suitAMSList = suitAMSList ?? throw new ArgumentNullException(nameof(suitAMSList));
            InitializeComponent();
            if (suitAMSList.Count == 0)
                return;
            AMSList = suitAMSList;
            AMSList.Sort(new Comparison<SuitAMSResultItem>((a1, a2) => { return a1.Deviation.CompareTo(a2.Deviation); }));
            DialogResult = DialogResult.Cancel;
            initializeList(AMSList);
            _ = listViewAMS.SelectedIndices.Add(0);
        }

        private void initializeList(SuitAMSResult list)
        {
            listViewAMS.Items.Clear();
            foreach (SuitAMSResultItem item in list)
            {
                ListViewItem val = new ListViewItem(item.AMS.Name);
                _ = val.SubItems.Add((item.Distance / 1000).ToString("0.000"));
                _ = val.SubItems.Add(item.Deviation.ToString("0.0000"));
                val.Tag = item;
                _ = listViewAMS.Items.Add(val);
            }
        }

        private void initializeGraph(SuitAMSResultItem item)
        {
            GraphPane spane = zedGraphControlAMS.GraphPane;
            spane.Title.Text = "Сравнение среднемесячных относительных\r\nскоростей на МС и выбранной АМС";
            spane.XAxis.Title.IsVisible = false;
            spane.YAxis.Title.Text = "Относительная скорость";
            spane.GraphObjList.Clear();
            spane.CurveList.Clear();
            spane.XAxis.ScaleFormatEvent += xAxis_ScaleFormatEvent;
            spane.XAxis.Scale.MajorStep = 1;
            spane.XAxis.Scale.FontSpec.Size = 8;
            spane.XAxis.Scale.FontSpec.Angle = 45;
            spane.XAxis.Scale.Min = 1;
            spane.XAxis.Scale.Max = 12;
            PointPairList msList = new PointPairList();
            PointPairList amsList = new PointPairList();

            for (int i = 1; i <= 12; i++)
            {
                Months month = (Months)i;
                msList.Add(i, AMSList.RangeRelativeSpeeds[month]);
                amsList.Add(i, item.AMS.RelativeSpeeds[month]);
            }

            _ = spane.AddCurve("Ряд данных", msList, Color.Black);
            _ = spane.AddCurve(item.AMS.Name, amsList, Color.Red);


            zedGraphControlAMS.AxisChange();
            zedGraphControlAMS.Invalidate();
        }

        private string xAxis_ScaleFormatEvent(GraphPane pane, Axis axis, double val, int index)
        {
            Months month = (Months)val;
            return month.Description();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Result = selectedAMS;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listViewAMS_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                selectedAMS = e.Item.Tag as SuitAMSResultItem;
                initializeGraph(selectedAMS);
            }
        }

        private void FormRangeElevatorConfirmation_Shown(object sender, EventArgs e)
        {
            if (!AMSList.AllMonthInRange)
                _ = MessageBox.Show("В исходном ряде представлены не все месяцы, расчет может быть неточным");
        }
    }
}
