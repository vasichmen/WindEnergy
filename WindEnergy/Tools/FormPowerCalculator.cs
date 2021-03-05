﻿using CommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.UI.Dialogs;
using WindEnergy.WindLib.Calculation.PowerGeneration;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindLib;
using ZedGraph;

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
            radioButtonCharacteristicCalculate.Enabled = selectedEquipment.EnoughDataToCalculateCharacteristic;
            if (selectedEquipment.HasCharacteristic && selectedEquipment.EnoughDataToCalculateCharacteristic)
                radioButtonCharacteristicFromDatabase.Checked = true;
            else if (selectedEquipment.EnoughDataToCalculateCharacteristic)
                radioButtonCharacteristicCalculate.Checked = true;
            else
                radioButtonCharacteristicManual.Checked = true;

            //основные характеристики
            comboBoxRegulator.SelectedItem = selectedEquipment.Regulator.Description();
            textBoxPower.Text = selectedEquipment.Power.ToString();
            textBoxMaxWindSpeed.Text = selectedEquipment.MaxWindSpeed.ToString();
            textBoxMinWindSpeed.Text = selectedEquipment.MinWindSpeed.ToString();
            textBoxNomWindSpeed.Text = selectedEquipment.NomWindSpeed.ToString();
            textBoxDeveloper.Text = selectedEquipment.Developer;
            textBoxDiameter.Text = selectedEquipment.Diameter.ToString();
            textBoxModel.Text = selectedEquipment.Model;
            textBoxID.Text = selectedEquipment.ID.ToString();
            textBoxTowerHeight.Text = selectedEquipment.TowerHeightString;
            buttonCalculate.Enabled = selectedEquipment.EnoughDataToCalculateCharacteristic;

            //отрисовка графика характеристики
            zedGraphControl.GraphPane.CurveList.Clear();
            if (selectedEquipment.HasCharacteristic)
            {
                List<double> keys = selectedEquipment.PerformanceCharacteristic.Keys.ToList();
                keys.Sort();
                GraphPane spane = zedGraphControl.GraphPane;
                spane.Title.Text = "P(V), кВт";
                spane.XAxis.Title.Text = "V, м/с";
                spane.YAxis.Title.Text = "P(V), кВт";
                spane.GraphObjList.Clear();
                spane.CurveList.Clear();
                PointPairList slist = new PointPairList();
                foreach (double key in keys)
                    slist.Add(key, selectedEquipment.PerformanceCharacteristic[key]);
                _ = spane.AddCurve("P(V)", slist, Color.Red);
            }
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
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
                case "maxwindspeed":
                    e.Column.HeaderText = "Максимальная скорость ветра, м/с";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "towerheightstring":
                    e.Column.HeaderText = "Варианты высот башни, м";
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

            selectedEquipment =( (EquipmentItemInfo)dataGridView1.SelectedRows[0].DataBoundItem).Clone();
            recalculateInterface();
        }

        /// <summary>
        /// перезапись данных в выбранном элементе при изменении в контроле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            switch (control.Tag)
            {
                case "developer":
                    selectedEquipment.Developer = textBoxDeveloper.Text;
                    break;
                case "model":
                    selectedEquipment.Model = textBoxModel.Text;
                    break;
                case "power":
                    if (double.TryParse(textBoxPower.Text.Replace('.', Constants.DecimalSeparator), out double power))
                        selectedEquipment.Power = power;
                    break;
                case "nomWindSpeed":
                    if (double.TryParse(textBoxNomWindSpeed.Text.Replace('.', Constants.DecimalSeparator), out double nom))
                        selectedEquipment.NomWindSpeed = nom;
                    break;
                case "minWindSpeed":
                    if (double.TryParse(textBoxMinWindSpeed.Text.Replace('.', Constants.DecimalSeparator), out double min))
                        selectedEquipment.MinWindSpeed = min;
                    break;
                case "maxWindSpeed":
                    if (double.TryParse(textBoxMaxWindSpeed.Text.Replace('.', Constants.DecimalSeparator), out double max))
                        selectedEquipment.MaxWindSpeed = max;
                    break;
                case "diameter":
                    if (double.TryParse(textBoxDiameter.Text.Replace('.', Constants.DecimalSeparator), out double diameter))
                        selectedEquipment.Diameter = diameter;
                    break;
                case "regulator":
                    selectedEquipment.Regulator = (TurbineRegulations)(new EnumTypeConverter<TurbineRegulations>().ConvertFrom(comboBoxRegulator.SelectedItem));
                    break;
                case "towerHeight":
                    string[] arr = textBoxTowerHeight.Text.Trim(new char[] { ',', '/', '.' }).Replace(" ", "").Split('/');
                    List<double> vals = new List<double>();
                    foreach (string elem in arr)
                        if (double.TryParse(elem.Replace('.', Constants.DecimalSeparator), out double val))
                            vals.Add(val);
                    selectedEquipment.TowerHeight = vals;
                    break;
                case "id":
                    break;
                default: throw new Exception("Поле не определено");
            }
            recalculateInterface();
        }

        /// <summary>
        /// заполнение выбранного элемента перед сохранением в БД или расчетом
        /// </summary>
        private bool prepareEquipment()
        {
            if (radioButtonCharacteristicCalculate.Checked)
                selectedEquipment.PerformanceCharacteristic = PowerCalculator.CalculatePerformanceCharacteristic(selectedEquipment);
            else if (radioButtonCharacteristicManual.Checked)
            {
                FormPerformanceCharacteristicDialog fpcd = new FormPerformanceCharacteristicDialog(30);
                if (fpcd.ShowDialog(this) == DialogResult.OK)
                {
                    selectedEquipment.PerformanceCharacteristic = fpcd.Result;
                }
                else return false;
            }
            return true;
        }

        private void buttonSaveEquipmentInDb_Click(object sender, EventArgs e)
        {
            if (prepareEquipment())
            {
                selectedEquipment.ID = Vars.EquipmentDatabase.GenerateNextKey();
                Vars.EquipmentDatabase.AddElement(selectedEquipment.ID, selectedEquipment);
                loadTable();
                _ = MessageBox.Show("Новая запись сохранена в БД Энергетическое оборудование");
            }
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (prepareEquipment())
                new FormPowerCalculatorResult(selectedEquipment).Show(this);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(MessageBox.Show(this,"Вы действиетльно хотите удалить эту ВЭУ?", "Удаление ВЭУ",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Vars.EquipmentDatabase.DeleteElement(selectedEquipment.ID);
                    loadTable();
                }
            }
        }
    }
}