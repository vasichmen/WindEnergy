using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.WindLib.Calculation.PowerGeneration;
using WindEnergy.WindLib.Transformation.Restore.Interpolation;
using ZedGraph;

namespace WindEnergy.UI.Dialogs
{
    public partial class FormPerformanceCharacteristicDialog : Form
    {
        double selectedSpeed = double.NaN;
        internal Dictionary<double, double> Result;
        private readonly int maxSpeed;

        public FormPerformanceCharacteristicDialog(int maxSpeed)
        {
            InitializeComponent();

            DialogResult = DialogResult.Cancel;

            this.maxSpeed = maxSpeed;
            Result = new Dictionary<double, double>();

            refreshInterface();
        }

        private void refreshInterface()
        {
            List<double> keys = Result.Keys.ToList();
            keys.Sort();

            selectedSpeed = double.NaN;

            buttonAdd.Enabled = false;

            //список скоростей
            comboBoxSpeeds.Items.Clear();
            comboBoxSpeeds.SelectedItem = null;
            comboBoxSpeeds.Text = "";
            for (int i = 1; i <= maxSpeed; i++)
            {
                if (!Result.ContainsKey(i))
                    _ = comboBoxSpeeds.Items.Add(i);
            }

            //кнопки координат в списке
            flowLayoutPanelCoordinates.Controls.Clear();
            foreach (double key in keys)
            {
                Button button = new Button();
                button.Width = 60;
                button.Click += button_Click;
                button.Tag = key;
                button.Text = $"[{key};{Result[key]}]";
                button.Parent = flowLayoutPanelCoordinates;
                toolTip1.SetToolTip(button, "Чтобы удалить точку нажмите эту кнопку и удалите ее в панели слева от графика");
            }

            buttonDelete.Visible = false;

            //график
            GraphPane spane = zedGraphControl.GraphPane;
            spane.Title.Text = "P(V), кВт";
            spane.XAxis.Title.Text = "V, м/с";
            spane.YAxis.Title.Text = "P(V), кВт";
            spane.GraphObjList.Clear();
            spane.CurveList.Clear();
            PointPairList slist = new PointPairList();
            foreach (double key in keys)
                slist.Add(key, Result[key]);
            _ = spane.AddCurve("P(V)", slist, Color.Red);
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void button_Click(object sender, EventArgs e)
        {
            selectedSpeed = (double)(sender as Button).Tag;

            numericUpDownPower.Value = (decimal)Result[selectedSpeed];
            comboBoxSpeeds.Text = selectedSpeed.ToString();

            buttonDelete.Visible = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Result.Add(Convert.ToDouble(comboBoxSpeeds.SelectedItem), Convert.ToDouble(numericUpDownPower.Value));
            if (checkBoxIsMaxSpeed.Checked)
            {
                Result.Add(Convert.ToDouble(comboBoxSpeeds.SelectedItem) + PowerCalculator.MIN_WIND_SPEED_STEP, 0);
            }
            refreshInterface();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!double.IsNaN(selectedSpeed))
            {
                _ = Result.Remove(selectedSpeed);
                refreshInterface();
            }
        }

        private void comboBoxSpeeds_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBoxSpeeds.SelectedItem != null)
                buttonAdd.Enabled = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(Result.Count == 0)
            {
                _ = MessageBox.Show(this, "Для сохранения надо добавить хотя бы одну точку в характеристику", "Редактирование мощностной характеристики", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Result[0] = 0;
            Result[maxSpeed + 1] = 0;
            LinearInterpolateMethod interpolator = new LinearInterpolateMethod(Result);

            for (int i = 1; i <= maxSpeed; i++)
                Result[i] = interpolator.GetValue(i);

            _ = Result.Remove(0);
            _ = Result.Remove(maxSpeed + 1);

            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
