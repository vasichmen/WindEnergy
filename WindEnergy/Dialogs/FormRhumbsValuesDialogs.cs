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
    public partial class FormRhumbsValuesDialogs : Form
    {
        Dictionary<WindDirections8, double> Values;

        /// <summary>
        /// результат работы диалогового окна
        /// </summary>
        public Dictionary<WindDirections8, double> Result { get; private set; }


        Dictionary<WindDirections8, Label> Labels = new Dictionary<WindDirections8, Label>();
        Dictionary<WindDirections8, TextBox> Textboxes = new Dictionary<WindDirections8, TextBox>();

        public FormRhumbsValuesDialogs(Dictionary<WindDirections8, double> values, string caption)
        {
            InitializeComponent();
            Text = caption;
            if (values != null)
                Values = values;
            else
                Values = new Dictionary<WindDirections8, double>();

            for (int i =0; i <= 7; i++)
            {
                WindDirections8 rhumb = (WindDirections8)i;
                Label label = new Label() { Text = rhumb.Description(), Height = 20, Margin = new Padding(0, 0, 0, 2), Font = new Font(FontFamily.GenericSansSerif, 10) };
                TextBox textbox = new TextBox() { Height = 30, Margin = new Padding(0, 0, 0, 2), Text = Values.ContainsKey(rhumb) ? Values[rhumb].ToString() : "",Tag = rhumb };
                Labels.Add(rhumb, label);
                Textboxes.Add(rhumb, textbox);
                flowLayoutPanelRhumb.Controls.Add(label);
                flowLayoutPanelValue.Controls.Add(textbox);
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            foreach(TextBox tb in Textboxes.Values)
            {
                WindDirections8 rhumb = (WindDirections8)tb.Tag;
                if(string.IsNullOrWhiteSpace(tb.Text))
                { _ = MessageBox.Show(this, $"Не удалось распознать значение румба: {rhumb.Description()}", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (!double.TryParse(tb.Text.Trim().Replace('.', Constants.DecimalSeparator), out double value))
                { _ = MessageBox.Show(this, $"Не удалось распознать \"{tb.Text}\" как число", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                Values[rhumb] = value;
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
