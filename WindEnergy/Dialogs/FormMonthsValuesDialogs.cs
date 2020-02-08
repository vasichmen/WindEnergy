using CommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindEnergy.UI.Dialogs
{
    /// <summary>
    /// диалог заполнения значений для каждого месяца
    /// </summary>
    public partial class FormMonthsValuesDialogs : Form
    {
        Dictionary<Months, double> Values;


        /// <summary>
        /// результат работы диалогового окна
        /// </summary>
        public Dictionary<Months, double> Result { get; set; }


        Dictionary<Months, Label> Labels = new Dictionary<Months, Label>();
        Dictionary<Months, TextBox> Textboxes = new Dictionary<Months, TextBox>();

        public FormMonthsValuesDialogs(Dictionary<Months, double> values, string caption)
        {
            InitializeComponent();
            Text = caption;
            if (values != null)
                Values = values;
            else
                Values = new Dictionary<Months, double>();

            for (int i = 1; i <= 12; i++)
            {
                Months month = (Months)i;
                Label label = new Label() { Text = month.Description(), Height = 20, Margin = new Padding(0, 0, 0, 2), Font = new Font(FontFamily.GenericSansSerif, 10) };
                TextBox textbox = new TextBox() { Height = 30, Margin = new Padding(0, 0, 0, 2), Text = Values.ContainsKey(month) ? Values[month].ToString() : "",Tag = month };
                Labels.Add(month, label);
                Textboxes.Add(month, textbox);
                flowLayoutPanelMonth.Controls.Add(label);
                flowLayoutPanelValue.Controls.Add(textbox);
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            foreach(TextBox tb in Textboxes.Values)
            {
                Months month = (Months)tb.Tag;
                if(string.IsNullOrWhiteSpace(tb.Text))
                { _ = MessageBox.Show(this, $"Не удалось распознать значение месяца: {month.Description()}", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (!double.TryParse(tb.Text.Trim().Replace('.', Constants.DecimalSeparator), out double value))
                { _ = MessageBox.Show(this, $"Не удалось распознать \"{tb.Text}\" как число", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                Values[month] = value;
            }

            Result = Values;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
