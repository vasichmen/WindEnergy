using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.UI.Dialogs
{
    /// <summary>
    /// окно редактирования диапазонов
    /// </summary>
    public partial class FormEditDiapasons : Form
    {
        /// <summary>
        /// данные для связи со списком
        /// </summary>
        private BindingList<Diapason<double>> diapasons;

        /// <summary>
        /// результат - список диапазонов
        /// </summary>
        public List<Diapason<double>> Result { get; private set; }

        /// <summary>
        /// создаёт новое окно и заполняет указанными диапазонами
        /// </summary>
        /// <param name="diapasons"></param>
        /// <param name="caption"></param>
        public FormEditDiapasons(List<Diapason<double>> diapasons, string caption)
        {
            InitializeComponent();
            Text = caption;
            if (diapasons == null)
                diapasons = new List<Diapason<double>>();
            this.diapasons = new BindingList<Diapason<double>>(diapasons);
        }

        /// <summary>
        /// добавление нового диапазона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Diapason<double> nd = new Diapason<double>();
            nd.From = (double)numericUpDownFrom.Value;
            nd.To = (double)numericUpDownTo.Value;
            if (nd.From >= nd.To)
            {
                _ = MessageBox.Show(this, "Начало диапазона должно быть меньше конца", "Добавление диапазона", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            diapasons.Add(nd);
        }

        /// <summary>
        /// удаление выделенного диапазона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxDiapasons.SelectedIndex != -1)
                diapasons.RemoveAt(listBoxDiapasons.SelectedIndex);
        }

        /// <summary>
        /// сохранить и закрыть окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Result = diapasons.ToList();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// отмена изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// заполнение списка диапазонамил
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormEditDiapasons_Shown(object sender, EventArgs e)
        {
            listBoxDiapasons.DataSource = diapasons;
        }
    }
}
