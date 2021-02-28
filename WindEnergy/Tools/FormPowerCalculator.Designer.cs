
namespace WindEnergy.UI.Tools
{
    partial class FormPowerCalculator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPowerCalculator));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonCharacteristicManual = new System.Windows.Forms.RadioButton();
            this.radioButtonCharacteristicCalculate = new System.Windows.Forms.RadioButton();
            this.radioButtonCharacteristicFromDatabase = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.buttonSaveEquipmentInDb = new System.Windows.Forms.Button();
            this.comboBoxRegulator = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxTowerHeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxNomWindSpeed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDiameter = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxMinWindSpeed = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxDeveloper = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxMaxWindSpeed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPower = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1146, 457);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 437);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1146, 20);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.zedGraphControl1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 203);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1140, 231);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(564, 225);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DataGridViewColumnAdded);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.Location = new System.Drawing.Point(573, 3);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(564, 225);
            this.zedGraphControl1.TabIndex = 1;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1140, 194);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonCharacteristicManual);
            this.groupBox1.Controls.Add(this.radioButtonCharacteristicCalculate);
            this.groupBox1.Controls.Add(this.radioButtonCharacteristicFromDatabase);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(573, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Мощностная характеристика";
            // 
            // radioButtonCharacteristicManual
            // 
            this.radioButtonCharacteristicManual.AutoSize = true;
            this.radioButtonCharacteristicManual.Location = new System.Drawing.Point(6, 65);
            this.radioButtonCharacteristicManual.Name = "radioButtonCharacteristicManual";
            this.radioButtonCharacteristicManual.Size = new System.Drawing.Size(256, 17);
            this.radioButtonCharacteristicManual.TabIndex = 2;
            this.radioButtonCharacteristicManual.TabStop = true;
            this.radioButtonCharacteristicManual.Text = "Ввести мощностную характеристику вручную";
            this.radioButtonCharacteristicManual.UseVisualStyleBackColor = true;
            // 
            // radioButtonCharacteristicCalculate
            // 
            this.radioButtonCharacteristicCalculate.AutoSize = true;
            this.radioButtonCharacteristicCalculate.Location = new System.Drawing.Point(6, 42);
            this.radioButtonCharacteristicCalculate.Name = "radioButtonCharacteristicCalculate";
            this.radioButtonCharacteristicCalculate.Size = new System.Drawing.Size(265, 17);
            this.radioButtonCharacteristicCalculate.TabIndex = 1;
            this.radioButtonCharacteristicCalculate.TabStop = true;
            this.radioButtonCharacteristicCalculate.Text = "Расчитать характеристику на основе Vmin и Vр";
            this.radioButtonCharacteristicCalculate.UseVisualStyleBackColor = true;
            // 
            // radioButtonCharacteristicFromDatabase
            // 
            this.radioButtonCharacteristicFromDatabase.AutoSize = true;
            this.radioButtonCharacteristicFromDatabase.Location = new System.Drawing.Point(6, 19);
            this.radioButtonCharacteristicFromDatabase.Name = "radioButtonCharacteristicFromDatabase";
            this.radioButtonCharacteristicFromDatabase.Size = new System.Drawing.Size(374, 17);
            this.radioButtonCharacteristicFromDatabase.TabIndex = 0;
            this.radioButtonCharacteristicFromDatabase.TabStop = true;
            this.radioButtonCharacteristicFromDatabase.Text = "Использовать характеристику из БД энергетическое обородование";
            this.radioButtonCharacteristicFromDatabase.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonCalculate);
            this.groupBox2.Controls.Add(this.buttonSaveEquipmentInDb);
            this.groupBox2.Controls.Add(this.comboBoxRegulator);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxTowerHeight);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBoxNomWindSpeed);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxDiameter);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxMinWindSpeed);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxModel);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxDeveloper);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxMaxWindSpeed);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxPower);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(564, 188);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Основные параметры ВЭУ";
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(196, 159);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(100, 23);
            this.buttonCalculate.TabIndex = 11;
            this.buttonCalculate.Text = "Расчитать ";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            // 
            // buttonSaveEquipmentInDb
            // 
            this.buttonSaveEquipmentInDb.Location = new System.Drawing.Point(9, 159);
            this.buttonSaveEquipmentInDb.Name = "buttonSaveEquipmentInDb";
            this.buttonSaveEquipmentInDb.Size = new System.Drawing.Size(105, 23);
            this.buttonSaveEquipmentInDb.TabIndex = 10;
            this.buttonSaveEquipmentInDb.Text = "Сохранить ВЭУ";
            this.buttonSaveEquipmentInDb.UseVisualStyleBackColor = true;
            this.buttonSaveEquipmentInDb.Click += new System.EventHandler(this.buttonSaveEquipmentInDb_Click);
            // 
            // comboBoxRegulator
            // 
            this.comboBoxRegulator.FormattingEnabled = true;
            this.comboBoxRegulator.Location = new System.Drawing.Point(458, 98);
            this.comboBoxRegulator.Name = "comboBoxRegulator";
            this.comboBoxRegulator.Size = new System.Drawing.Size(100, 21);
            this.comboBoxRegulator.TabIndex = 9;
            this.comboBoxRegulator.Tag = "regulator";
            this.comboBoxRegulator.SelectedValueChanged += new System.EventHandler(this.controlChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(311, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Тип регулирования";
            // 
            // textBoxTowerHeight
            // 
            this.textBoxTowerHeight.Location = new System.Drawing.Point(196, 125);
            this.textBoxTowerHeight.Name = "textBoxTowerHeight";
            this.textBoxTowerHeight.Size = new System.Drawing.Size(100, 20);
            this.textBoxTowerHeight.TabIndex = 7;
            this.textBoxTowerHeight.Tag = "towerHeight";
            this.textBoxTowerHeight.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Варианты высот башни, м";
            // 
            // textBoxNomWindSpeed
            // 
            this.textBoxNomWindSpeed.Location = new System.Drawing.Point(196, 99);
            this.textBoxNomWindSpeed.Name = "textBoxNomWindSpeed";
            this.textBoxNomWindSpeed.Size = new System.Drawing.Size(100, 20);
            this.textBoxNomWindSpeed.TabIndex = 7;
            this.textBoxNomWindSpeed.Tag = "nomWindSpeed";
            this.textBoxNomWindSpeed.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Номинальная скорость ветра, м/с";
            // 
            // textBoxDiameter
            // 
            this.textBoxDiameter.Location = new System.Drawing.Point(501, 72);
            this.textBoxDiameter.Name = "textBoxDiameter";
            this.textBoxDiameter.Size = new System.Drawing.Size(57, 20);
            this.textBoxDiameter.TabIndex = 5;
            this.textBoxDiameter.Tag = "diameter";
            this.textBoxDiameter.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(311, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Диаметр рабочего колеса, м";
            // 
            // textBoxMinWindSpeed
            // 
            this.textBoxMinWindSpeed.Location = new System.Drawing.Point(501, 20);
            this.textBoxMinWindSpeed.Name = "textBoxMinWindSpeed";
            this.textBoxMinWindSpeed.Size = new System.Drawing.Size(57, 20);
            this.textBoxMinWindSpeed.TabIndex = 5;
            this.textBoxMinWindSpeed.Tag = "minWindSpeed";
            this.textBoxMinWindSpeed.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Минимальная скорость ветра, м/с";
            // 
            // textBoxModel
            // 
            this.textBoxModel.Location = new System.Drawing.Point(141, 46);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(155, 20);
            this.textBoxModel.TabIndex = 3;
            this.textBoxModel.Tag = "model";
            this.textBoxModel.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Модель";
            // 
            // textBoxDeveloper
            // 
            this.textBoxDeveloper.Location = new System.Drawing.Point(141, 20);
            this.textBoxDeveloper.Name = "textBoxDeveloper";
            this.textBoxDeveloper.Size = new System.Drawing.Size(155, 20);
            this.textBoxDeveloper.TabIndex = 3;
            this.textBoxDeveloper.Tag = "developer";
            this.textBoxDeveloper.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Производитель";
            // 
            // textBoxMaxWindSpeed
            // 
            this.textBoxMaxWindSpeed.Location = new System.Drawing.Point(501, 46);
            this.textBoxMaxWindSpeed.Name = "textBoxMaxWindSpeed";
            this.textBoxMaxWindSpeed.Size = new System.Drawing.Size(57, 20);
            this.textBoxMaxWindSpeed.TabIndex = 3;
            this.textBoxMaxWindSpeed.Tag = "maxWindSpeed";
            this.textBoxMaxWindSpeed.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Максимальная скорость ветра, м/с";
            // 
            // textBoxPower
            // 
            this.textBoxPower.Location = new System.Drawing.Point(196, 72);
            this.textBoxPower.Name = "textBoxPower";
            this.textBoxPower.Size = new System.Drawing.Size(100, 20);
            this.textBoxPower.TabIndex = 1;
            this.textBoxPower.Tag = "power";
            this.textBoxPower.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номинальная мощность, кВт";
            // 
            // FormPowerCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 457);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormPowerCalculator";
            this.Text = "Расчет выработки ВЭУ";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonCharacteristicManual;
        private System.Windows.Forms.RadioButton radioButtonCharacteristicCalculate;
        private System.Windows.Forms.RadioButton radioButtonCharacteristicFromDatabase;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxPower;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNomWindSpeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMinWindSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMaxWindSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxRegulator;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDeveloper;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDiameter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Button buttonSaveEquipmentInDb;
        private System.Windows.Forms.TextBox textBoxTowerHeight;
        private System.Windows.Forms.Label label9;
    }
}