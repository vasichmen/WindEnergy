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
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindLib;

namespace WindEnergy.UI.Tools
{
    public partial class FormPowerCalculator : Form
    {
        private RawRange range;
        private EquipmentItemInfo selectedEquipment = null;


        public FormPowerCalculator(RawRange range)
        {
            InitializeComponent();
            this.range = range;

            comboBoxRegulator.Items.Clear();
            comboBoxRegulator.Items.AddRange(TurbineRegulations.None.GetItems().ToArray());

            this.loadTable();
            if (dataGridView1.Rows.Count > 0)
                dataGridView1.Rows[0].Selected = true;
        }

        private void loadTable()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Vars.EquipmentDatabase.List;
            dataGridView1.ClearSelection();
        }

        private void recalculateInterface()
        {
            if (selectedEquipment == null)
                return;

            //группа источников характеристики
            radioButtonCharacteristicCalculate.Checked = false;
            radioButtonCharacteristicFromDatabase.Checked = false;
            radioButtonCharacteristicManual.Checked = false;
            radioButtonCharacteristicFromDatabase.Enabled = selectedEquipment.HasCharacteristic;
            radioButtonCharacteristicCalculate.Enabled = selectedEquipment.EnoughDataToCalculate;
            if (selectedEquipment.HasCharacteristic && selectedEquipment.EnoughDataToCalculate)
                radioButtonCharacteristicFromDatabase.Checked = true;
            else if (selectedEquipment.EnoughDataToCalculate)
                radioButtonCharacteristicCalculate.Checked = true;
            else
                radioButtonCharacteristicManual.Checked = true;

            //основные характеристики
            comboBoxRegulator.SelectedItem = selectedEquipment.Regulator.Description();
        }


        /// <summary>
        /// изменение типа столбцов при добавлении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataGridViewColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e = e ?? throw new ArgumentNullException(nameof(e));
            switch (e.Column.Name.ToLower())
            {
                case "id":
                    e.Column.DefaultCellStyle.Format = "n0";
                    e.Column.Width = 40;
                    break;
                case "model":
                    e.Column.HeaderText = "Модель";
                    break;
                case "developer":
                    e.Column.HeaderText = "Производитель";
                    break;
                case "power":
                    e.Column.HeaderText = "Номинальная мощность, кВт";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "regulator":
                    e.Column.HeaderText = "Тип регулирования";
                    break;
                case "diameter":
                    e.Column.HeaderText = "Dрк, м";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "hascharacteristic":
                    e.Column.HeaderText = "Мощностная характеристика";
                    break;

                //удаляемые колонки пишем тут:
                case "performancecharacteristic":
                case "enoughdatatocalculate":
                case "towerheight":
                case "minwindspeed":
                case "nomwindspeed":
                    e.Column.DataGridView.Columns.Remove(e.Column);
                    break;
                default: throw new Exception("Для этой колонки нет названия");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            selectedEquipment = (EquipmentItemInfo)dataGridView1.SelectedRows[0].DataBoundItem;
            recalculateInterface();
        }
    }
}
