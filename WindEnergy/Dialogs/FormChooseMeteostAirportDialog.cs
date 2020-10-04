using System;
using System.Windows.Forms;

namespace WindEnergy.UI.Dialogs
{
    /// <summary>
    /// диалог выбора одного из двух вариантов ответа с комментарием
    /// </summary>
    public partial class FormChooseMeteostAirportDialog : Form
    {
        /// <summary>
        /// резульат работы - какая кнопка была нажата: первая или вторая
        /// </summary>
        public int Result { get; set; }

        public FormChooseMeteostAirportDialog(string caption, string text, string button1Text, string button2Text)
        {
            InitializeComponent();
            this.Text = caption;
            textBoxText.Text = text;
            button1.Text = button1Text;
            button2.Text = button2Text;

            new ToolTip().SetToolTip(button1, button1.Text);
            new ToolTip().SetToolTip(button2, button2.Text);

            DialogResult = DialogResult.None;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result = 1;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result = 2;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FormChooseMeteostAirportDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.None)
                DialogResult = DialogResult.Cancel;
        }
    }
}
