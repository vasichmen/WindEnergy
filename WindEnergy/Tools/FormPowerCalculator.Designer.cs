
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
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonCharacteristicManual = new System.Windows.Forms.RadioButton();
            this.radioButtonCharacteristicCalculate = new System.Windows.Forms.RadioButton();
            this.radioButtonCharacteristicFromDatabase = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxRegulator = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDiameter = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxMinWindSpeed = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMaxWindSpeed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.buttonSaveEquipmentInDb = new System.Windows.Forms.Button();
            this.textBoxTowerHeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxNomWindSpeed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxDeveloper = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 246F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1528, 562);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1528, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.zedGraphControl, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 250);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1520, 283);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 4);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(752, 275);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DataGridViewColumnAdded);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // zedGraphControl
            // 
            this.zedGraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl.Location = new System.Drawing.Point(765, 5);
            this.zedGraphControl.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.ScrollGrace = 0D;
            this.zedGraphControl.ScrollMaxX = 0D;
            this.zedGraphControl.ScrollMaxY = 0D;
            this.zedGraphControl.ScrollMaxY2 = 0D;
            this.zedGraphControl.ScrollMinX = 0D;
            this.zedGraphControl.ScrollMinY = 0D;
            this.zedGraphControl.ScrollMinY2 = 0D;
            this.zedGraphControl.Size = new System.Drawing.Size(750, 273);
            this.zedGraphControl.TabIndex = 1;
            this.zedGraphControl.UseExtendedPrintDialog = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1520, 238);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonCharacteristicManual);
            this.groupBox1.Controls.Add(this.radioButtonCharacteristicCalculate);
            this.groupBox1.Controls.Add(this.radioButtonCharacteristicFromDatabase);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(764, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(752, 230);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Мощностная характеристика";
            // 
            // radioButtonCharacteristicManual
            // 
            this.radioButtonCharacteristicManual.AutoSize = true;
            this.radioButtonCharacteristicManual.Location = new System.Drawing.Point(8, 80);
            this.radioButtonCharacteristicManual.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonCharacteristicManual.Name = "radioButtonCharacteristicManual";
            this.radioButtonCharacteristicManual.Size = new System.Drawing.Size(329, 21);
            this.radioButtonCharacteristicManual.TabIndex = 2;
            this.radioButtonCharacteristicManual.TabStop = true;
            this.radioButtonCharacteristicManual.Text = "Ввести мощностную характеристику вручную";
            this.radioButtonCharacteristicManual.UseVisualStyleBackColor = true;
            // 
            // radioButtonCharacteristicCalculate
            // 
            this.radioButtonCharacteristicCalculate.AutoSize = true;
            this.radioButtonCharacteristicCalculate.Location = new System.Drawing.Point(8, 52);
            this.radioButtonCharacteristicCalculate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonCharacteristicCalculate.Name = "radioButtonCharacteristicCalculate";
            this.radioButtonCharacteristicCalculate.Size = new System.Drawing.Size(344, 21);
            this.radioButtonCharacteristicCalculate.TabIndex = 1;
            this.radioButtonCharacteristicCalculate.TabStop = true;
            this.radioButtonCharacteristicCalculate.Text = "Расчитать характеристику на основе Vmin и Vр";
            this.radioButtonCharacteristicCalculate.UseVisualStyleBackColor = true;
            // 
            // radioButtonCharacteristicFromDatabase
            // 
            this.radioButtonCharacteristicFromDatabase.AutoSize = true;
            this.radioButtonCharacteristicFromDatabase.Location = new System.Drawing.Point(8, 23);
            this.radioButtonCharacteristicFromDatabase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonCharacteristicFromDatabase.Name = "radioButtonCharacteristicFromDatabase";
            this.radioButtonCharacteristicFromDatabase.Size = new System.Drawing.Size(480, 21);
            this.radioButtonCharacteristicFromDatabase.TabIndex = 0;
            this.radioButtonCharacteristicFromDatabase.TabStop = true;
            this.radioButtonCharacteristicFromDatabase.Text = "Использовать характеристику из БД энергетическое обородование";
            this.radioButtonCharacteristicFromDatabase.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxRegulator);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxDiameter);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxID);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBoxMinWindSpeed);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxMaxWindSpeed);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.buttonCalculate);
            this.groupBox2.Controls.Add(this.buttonSaveEquipmentInDb);
            this.groupBox2.Controls.Add(this.textBoxTowerHeight);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBoxNomWindSpeed);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxModel);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxDeveloper);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxPower);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(752, 230);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Основные параметры ВЭУ";
            // 
            // comboBoxRegulator
            // 
            this.comboBoxRegulator.FormattingEnabled = true;
            this.comboBoxRegulator.Location = new System.Drawing.Point(200, 148);
            this.comboBoxRegulator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxRegulator.Name = "comboBoxRegulator";
            this.comboBoxRegulator.Size = new System.Drawing.Size(132, 24);
            this.comboBoxRegulator.TabIndex = 19;
            this.comboBoxRegulator.Tag = "regulator";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 153);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Тип регулирования";
            // 
            // textBoxDiameter
            // 
            this.textBoxDiameter.Location = new System.Drawing.Point(257, 116);
            this.textBoxDiameter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDiameter.Name = "textBoxDiameter";
            this.textBoxDiameter.Size = new System.Drawing.Size(75, 22);
            this.textBoxDiameter.TabIndex = 16;
            this.textBoxDiameter.Tag = "diameter";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 122);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(199, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "Диаметр рабочего колеса, м";
            // 
            // textBoxID
            // 
            this.textBoxID.Enabled = false;
            this.textBoxID.Location = new System.Drawing.Point(257, 20);
            this.textBoxID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(75, 22);
            this.textBoxID.TabIndex = 17;
            this.textBoxID.Tag = "id";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 23);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 17);
            this.label10.TabIndex = 15;
            this.label10.Text = "Идентификатор";
            // 
            // textBoxMinWindSpeed
            // 
            this.textBoxMinWindSpeed.Location = new System.Drawing.Point(257, 52);
            this.textBoxMinWindSpeed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMinWindSpeed.Name = "textBoxMinWindSpeed";
            this.textBoxMinWindSpeed.Size = new System.Drawing.Size(75, 22);
            this.textBoxMinWindSpeed.TabIndex = 17;
            this.textBoxMinWindSpeed.Tag = "minWindSpeed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Минимальная скорость ветра, м/с";
            // 
            // textBoxMaxWindSpeed
            // 
            this.textBoxMaxWindSpeed.Location = new System.Drawing.Point(257, 84);
            this.textBoxMaxWindSpeed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMaxWindSpeed.Name = "textBoxMaxWindSpeed";
            this.textBoxMaxWindSpeed.Size = new System.Drawing.Size(75, 22);
            this.textBoxMaxWindSpeed.TabIndex = 13;
            this.textBoxMaxWindSpeed.Tag = "maxWindSpeed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 87);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Максимальная скорость ветра, м/с";
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(611, 191);
            this.buttonCalculate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(133, 28);
            this.buttonCalculate.TabIndex = 11;
            this.buttonCalculate.Text = "Расчитать ";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // buttonSaveEquipmentInDb
            // 
            this.buttonSaveEquipmentInDb.Location = new System.Drawing.Point(361, 191);
            this.buttonSaveEquipmentInDb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSaveEquipmentInDb.Name = "buttonSaveEquipmentInDb";
            this.buttonSaveEquipmentInDb.Size = new System.Drawing.Size(140, 28);
            this.buttonSaveEquipmentInDb.TabIndex = 10;
            this.buttonSaveEquipmentInDb.Text = "Сохранить ВЭУ";
            this.buttonSaveEquipmentInDb.UseVisualStyleBackColor = true;
            this.buttonSaveEquipmentInDb.Click += new System.EventHandler(this.buttonSaveEquipmentInDb_Click);
            // 
            // textBoxTowerHeight
            // 
            this.textBoxTowerHeight.Location = new System.Drawing.Point(611, 149);
            this.textBoxTowerHeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxTowerHeight.Name = "textBoxTowerHeight";
            this.textBoxTowerHeight.Size = new System.Drawing.Size(132, 22);
            this.textBoxTowerHeight.TabIndex = 7;
            this.textBoxTowerHeight.Tag = "towerHeight";
            this.textBoxTowerHeight.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(357, 154);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(181, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "Варианты высот башни, м";
            // 
            // textBoxNomWindSpeed
            // 
            this.textBoxNomWindSpeed.Location = new System.Drawing.Point(611, 117);
            this.textBoxNomWindSpeed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNomWindSpeed.Name = "textBoxNomWindSpeed";
            this.textBoxNomWindSpeed.Size = new System.Drawing.Size(132, 22);
            this.textBoxNomWindSpeed.TabIndex = 7;
            this.textBoxNomWindSpeed.Tag = "nomWindSpeed";
            this.textBoxNomWindSpeed.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(357, 122);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(231, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Номинальная скорость ветра, м/с";
            // 
            // textBoxModel
            // 
            this.textBoxModel.Location = new System.Drawing.Point(537, 52);
            this.textBoxModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(205, 22);
            this.textBoxModel.TabIndex = 3;
            this.textBoxModel.Tag = "model";
            this.textBoxModel.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(357, 55);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Модель";
            // 
            // textBoxDeveloper
            // 
            this.textBoxDeveloper.Location = new System.Drawing.Point(537, 20);
            this.textBoxDeveloper.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDeveloper.Name = "textBoxDeveloper";
            this.textBoxDeveloper.Size = new System.Drawing.Size(205, 22);
            this.textBoxDeveloper.TabIndex = 3;
            this.textBoxDeveloper.Tag = "developer";
            this.textBoxDeveloper.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(357, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Производитель";
            // 
            // textBoxPower
            // 
            this.textBoxPower.Location = new System.Drawing.Point(611, 84);
            this.textBoxPower.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPower.Name = "textBoxPower";
            this.textBoxPower.Size = new System.Drawing.Size(132, 22);
            this.textBoxPower.TabIndex = 1;
            this.textBoxPower.Tag = "power";
            this.textBoxPower.TextChanged += new System.EventHandler(this.controlChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(357, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номинальная мощность, кВт";
            // 
            // FormPowerCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1528, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private ZedGraph.ZedGraphControl zedGraphControl;
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
        private System.Windows.Forms.TextBox textBoxDeveloper;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Button buttonSaveEquipmentInDb;
        private System.Windows.Forms.TextBox textBoxTowerHeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxRegulator;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDiameter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxMinWindSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMaxWindSpeed;
        private System.Windows.Forms.Label label2;
    }
}