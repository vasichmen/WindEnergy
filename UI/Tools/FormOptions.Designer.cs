namespace WindEnergy.UI.Tools
{
    partial class FormOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelRegionLimitsFile = new System.Windows.Forms.Label();
            this.labelCoordinatesMSFile = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonSelectRegionLimitsFile = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonSelectCoordinatesMSFile = new System.Windows.Forms.Button();
            this.comboBoxMapProvider = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxCalculateAirDensity = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownNormalLawTo = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNormalLawFrom = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxMinCorrWet = new System.Windows.Forms.CheckBox();
            this.checkBoxMinCorrTemp = new System.Windows.Forms.CheckBox();
            this.checkBoxMinCorrDirection = new System.Windows.Forms.CheckBox();
            this.checkBoxMinCorrSpeed = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxMinimalCoeffCorrel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxNearestMSRadius = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDaysToNewInterval = new System.Windows.Forms.TextBox();
            this.textBoxSectionLength = new System.Windows.Forms.TextBox();
            this.textBoxMinimalSpeedDeviation = new System.Windows.Forms.TextBox();
            this.textBoxAirDensity = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.labelGradStep = new System.Windows.Forms.Label();
            this.labelGradTo = new System.Windows.Forms.Label();
            this.labelGradFrom = new System.Windows.Forms.Label();
            this.numericUpDownGradStep = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownGradTo = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownGradFrom = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxSpeedGradations = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNormalLawTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNormalLawFrom)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGradStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGradTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGradFrom)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(651, 452);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 2);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(645, 414);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.labelRegionLimitsFile);
            this.tabPage1.Controls.Add(this.labelCoordinatesMSFile);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.buttonSelectRegionLimitsFile);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.buttonSelectCoordinatesMSFile);
            this.tabPage1.Controls.Add(this.comboBoxMapProvider);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(637, 385);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основные";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelRegionLimitsFile
            // 
            this.labelRegionLimitsFile.AutoSize = true;
            this.labelRegionLimitsFile.Location = new System.Drawing.Point(117, 142);
            this.labelRegionLimitsFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRegionLimitsFile.Name = "labelRegionLimitsFile";
            this.labelRegionLimitsFile.Size = new System.Drawing.Size(118, 17);
            this.labelRegionLimitsFile.TabIndex = 7;
            this.labelRegionLimitsFile.Text = "Файл не выбран";
            // 
            // labelCoordinatesMSFile
            // 
            this.labelCoordinatesMSFile.AutoSize = true;
            this.labelCoordinatesMSFile.Location = new System.Drawing.Point(117, 90);
            this.labelCoordinatesMSFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCoordinatesMSFile.Name = "labelCoordinatesMSFile";
            this.labelCoordinatesMSFile.Size = new System.Drawing.Size(118, 17);
            this.labelCoordinatesMSFile.TabIndex = 6;
            this.labelCoordinatesMSFile.Text = "Файл на выбран";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 116);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(265, 17);
            this.label12.TabIndex = 5;
            this.label12.Text = "Выбрать файл ограничений скоростей";
            // 
            // buttonSelectRegionLimitsFile
            // 
            this.buttonSelectRegionLimitsFile.Location = new System.Drawing.Point(9, 135);
            this.buttonSelectRegionLimitsFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSelectRegionLimitsFile.Name = "buttonSelectRegionLimitsFile";
            this.buttonSelectRegionLimitsFile.Size = new System.Drawing.Size(100, 28);
            this.buttonSelectRegionLimitsFile.TabIndex = 4;
            this.buttonSelectRegionLimitsFile.Text = "Обзор";
            this.buttonSelectRegionLimitsFile.UseVisualStyleBackColor = true;
            this.buttonSelectRegionLimitsFile.Click += new System.EventHandler(this.buttonSelectRegionLimitsFile_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 64);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(276, 17);
            this.label11.TabIndex = 3;
            this.label11.Text = "Выбрать файл координат метеостанций";
            // 
            // buttonSelectCoordinatesMSFile
            // 
            this.buttonSelectCoordinatesMSFile.Location = new System.Drawing.Point(9, 84);
            this.buttonSelectCoordinatesMSFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSelectCoordinatesMSFile.Name = "buttonSelectCoordinatesMSFile";
            this.buttonSelectCoordinatesMSFile.Size = new System.Drawing.Size(100, 28);
            this.buttonSelectCoordinatesMSFile.TabIndex = 2;
            this.buttonSelectCoordinatesMSFile.Text = "Обзор";
            this.buttonSelectCoordinatesMSFile.UseVisualStyleBackColor = true;
            this.buttonSelectCoordinatesMSFile.Click += new System.EventHandler(this.buttonSelectCoordinatesMSFile_Click);
            // 
            // comboBoxMapProvider
            // 
            this.comboBoxMapProvider.FormattingEnabled = true;
            this.comboBoxMapProvider.Location = new System.Drawing.Point(9, 23);
            this.comboBoxMapProvider.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxMapProvider.Name = "comboBoxMapProvider";
            this.comboBoxMapProvider.Size = new System.Drawing.Size(177, 24);
            this.comboBoxMapProvider.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип карты:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxCalculateAirDensity);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.numericUpDownNormalLawTo);
            this.tabPage2.Controls.Add(this.numericUpDownNormalLawFrom);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.checkBoxMinCorrWet);
            this.tabPage2.Controls.Add(this.checkBoxMinCorrTemp);
            this.tabPage2.Controls.Add(this.checkBoxMinCorrDirection);
            this.tabPage2.Controls.Add(this.checkBoxMinCorrSpeed);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBoxMinimalCoeffCorrel);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.textBoxNearestMSRadius);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textBoxDaysToNewInterval);
            this.tabPage2.Controls.Add(this.textBoxSectionLength);
            this.tabPage2.Controls.Add(this.textBoxMinimalSpeedDeviation);
            this.tabPage2.Controls.Add(this.textBoxAirDensity);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(637, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Расчёты";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxCalculateAirDensity
            // 
            this.checkBoxCalculateAirDensity.AutoSize = true;
            this.checkBoxCalculateAirDensity.Location = new System.Drawing.Point(417, 21);
            this.checkBoxCalculateAirDensity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxCalculateAirDensity.Name = "checkBoxCalculateAirDensity";
            this.checkBoxCalculateAirDensity.Size = new System.Drawing.Size(107, 21);
            this.checkBoxCalculateAirDensity.TabIndex = 21;
            this.checkBoxCalculateAirDensity.Text = "Авторасчёт";
            this.checkBoxCalculateAirDensity.UseVisualStyleBackColor = true;
            this.checkBoxCalculateAirDensity.CheckedChanged += new System.EventHandler(this.checkBoxCalculateAirDensity_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(376, 178);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 17);
            this.label10.TabIndex = 20;
            this.label10.Text = "ДО:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(271, 178);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "ОТ:";
            // 
            // numericUpDownNormalLawTo
            // 
            this.numericUpDownNormalLawTo.DecimalPlaces = 2;
            this.numericUpDownNormalLawTo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownNormalLawTo.Location = new System.Drawing.Point(417, 176);
            this.numericUpDownNormalLawTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownNormalLawTo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownNormalLawTo.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownNormalLawTo.Name = "numericUpDownNormalLawTo";
            this.numericUpDownNormalLawTo.Size = new System.Drawing.Size(56, 22);
            this.numericUpDownNormalLawTo.TabIndex = 18;
            // 
            // numericUpDownNormalLawFrom
            // 
            this.numericUpDownNormalLawFrom.DecimalPlaces = 2;
            this.numericUpDownNormalLawFrom.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownNormalLawFrom.Location = new System.Drawing.Point(312, 176);
            this.numericUpDownNormalLawFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownNormalLawFrom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownNormalLawFrom.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownNormalLawFrom.Name = "numericUpDownNormalLawFrom";
            this.numericUpDownNormalLawFrom.Size = new System.Drawing.Size(56, 22);
            this.numericUpDownNormalLawFrom.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 178);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(201, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Диапазон критерия Пирсона";
            // 
            // checkBoxMinCorrWet
            // 
            this.checkBoxMinCorrWet.AutoSize = true;
            this.checkBoxMinCorrWet.Location = new System.Drawing.Point(383, 240);
            this.checkBoxMinCorrWet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxMinCorrWet.Name = "checkBoxMinCorrWet";
            this.checkBoxMinCorrWet.Size = new System.Drawing.Size(101, 21);
            this.checkBoxMinCorrWet.TabIndex = 15;
            this.checkBoxMinCorrWet.Text = "Влажность";
            this.toolTip1.SetToolTip(this.checkBoxMinCorrWet, "Использовать ограничение коэффициента корреляции для влажности");
            this.checkBoxMinCorrWet.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinCorrTemp
            // 
            this.checkBoxMinCorrTemp.AutoSize = true;
            this.checkBoxMinCorrTemp.Location = new System.Drawing.Point(251, 240);
            this.checkBoxMinCorrTemp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxMinCorrTemp.Name = "checkBoxMinCorrTemp";
            this.checkBoxMinCorrTemp.Size = new System.Drawing.Size(118, 21);
            this.checkBoxMinCorrTemp.TabIndex = 14;
            this.checkBoxMinCorrTemp.Text = "Температура";
            this.toolTip1.SetToolTip(this.checkBoxMinCorrTemp, "Использовать ограничение коэффициента корреляции для ткмпературы");
            this.checkBoxMinCorrTemp.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinCorrDirection
            // 
            this.checkBoxMinCorrDirection.AutoSize = true;
            this.checkBoxMinCorrDirection.Location = new System.Drawing.Point(127, 240);
            this.checkBoxMinCorrDirection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxMinCorrDirection.Name = "checkBoxMinCorrDirection";
            this.checkBoxMinCorrDirection.Size = new System.Drawing.Size(119, 21);
            this.checkBoxMinCorrDirection.TabIndex = 13;
            this.checkBoxMinCorrDirection.Text = "Направление";
            this.toolTip1.SetToolTip(this.checkBoxMinCorrDirection, "Использовать ограничение коэффициента корреляции для направления");
            this.checkBoxMinCorrDirection.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinCorrSpeed
            // 
            this.checkBoxMinCorrSpeed.AutoSize = true;
            this.checkBoxMinCorrSpeed.Location = new System.Drawing.Point(31, 240);
            this.checkBoxMinCorrSpeed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxMinCorrSpeed.Name = "checkBoxMinCorrSpeed";
            this.checkBoxMinCorrSpeed.Size = new System.Drawing.Size(91, 21);
            this.checkBoxMinCorrSpeed.TabIndex = 12;
            this.checkBoxMinCorrSpeed.Text = "Скорость";
            this.toolTip1.SetToolTip(this.checkBoxMinCorrSpeed, "Использовать ограничение коэффициента корреляции для скорости");
            this.checkBoxMinCorrSpeed.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 212);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(236, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Минимальный коэфф. корреляции";
            // 
            // textBoxMinimalCoeffCorrel
            // 
            this.textBoxMinimalCoeffCorrel.Location = new System.Drawing.Point(275, 208);
            this.textBoxMinimalCoeffCorrel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMinimalCoeffCorrel.Name = "textBoxMinimalCoeffCorrel";
            this.textBoxMinimalCoeffCorrel.Size = new System.Drawing.Size(132, 22);
            this.textBoxMinimalCoeffCorrel.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 150);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(247, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Максимальное расстояние до МС, м";
            // 
            // textBoxNearestMSRadius
            // 
            this.textBoxNearestMSRadius.Location = new System.Drawing.Point(275, 146);
            this.textBoxNearestMSRadius.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNearestMSRadius.Name = "textBoxNearestMSRadius";
            this.textBoxNearestMSRadius.Size = new System.Drawing.Size(132, 22);
            this.textBoxNearestMSRadius.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 118);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(225, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Минимальный диапазон Δt, дней";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 86);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Шаг дискретизации Δt, шт";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отклонение скорости, м/с";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Плотность воздуха, кг/м^3";
            // 
            // textBoxDaysToNewInterval
            // 
            this.textBoxDaysToNewInterval.Location = new System.Drawing.Point(275, 114);
            this.textBoxDaysToNewInterval.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDaysToNewInterval.Name = "textBoxDaysToNewInterval";
            this.textBoxDaysToNewInterval.Size = new System.Drawing.Size(132, 22);
            this.textBoxDaysToNewInterval.TabIndex = 3;
            // 
            // textBoxSectionLength
            // 
            this.textBoxSectionLength.Location = new System.Drawing.Point(275, 82);
            this.textBoxSectionLength.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSectionLength.Name = "textBoxSectionLength";
            this.textBoxSectionLength.Size = new System.Drawing.Size(132, 22);
            this.textBoxSectionLength.TabIndex = 2;
            // 
            // textBoxMinimalSpeedDeviation
            // 
            this.textBoxMinimalSpeedDeviation.Location = new System.Drawing.Point(275, 50);
            this.textBoxMinimalSpeedDeviation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMinimalSpeedDeviation.Name = "textBoxMinimalSpeedDeviation";
            this.textBoxMinimalSpeedDeviation.Size = new System.Drawing.Size(132, 22);
            this.textBoxMinimalSpeedDeviation.TabIndex = 1;
            // 
            // textBoxAirDensity
            // 
            this.textBoxAirDensity.Enabled = false;
            this.textBoxAirDensity.Location = new System.Drawing.Point(275, 18);
            this.textBoxAirDensity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxAirDensity.Name = "textBoxAirDensity";
            this.textBoxAirDensity.Size = new System.Drawing.Size(132, 22);
            this.textBoxAirDensity.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.labelGradStep);
            this.tabPage3.Controls.Add(this.labelGradTo);
            this.tabPage3.Controls.Add(this.labelGradFrom);
            this.tabPage3.Controls.Add(this.numericUpDownGradStep);
            this.tabPage3.Controls.Add(this.numericUpDownGradTo);
            this.tabPage3.Controls.Add(this.numericUpDownGradFrom);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.comboBoxSpeedGradations);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Size = new System.Drawing.Size(637, 383);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Градации";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // labelGradStep
            // 
            this.labelGradStep.AutoSize = true;
            this.labelGradStep.Location = new System.Drawing.Point(297, 47);
            this.labelGradStep.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGradStep.Name = "labelGradStep";
            this.labelGradStep.Size = new System.Drawing.Size(36, 17);
            this.labelGradStep.TabIndex = 8;
            this.labelGradStep.Text = "Шаг:";
            // 
            // labelGradTo
            // 
            this.labelGradTo.AutoSize = true;
            this.labelGradTo.Location = new System.Drawing.Point(148, 47);
            this.labelGradTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGradTo.Name = "labelGradTo";
            this.labelGradTo.Size = new System.Drawing.Size(34, 17);
            this.labelGradTo.TabIndex = 7;
            this.labelGradTo.Text = "ДО:";
            // 
            // labelGradFrom
            // 
            this.labelGradFrom.AutoSize = true;
            this.labelGradFrom.Location = new System.Drawing.Point(8, 47);
            this.labelGradFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGradFrom.Name = "labelGradFrom";
            this.labelGradFrom.Size = new System.Drawing.Size(32, 17);
            this.labelGradFrom.TabIndex = 6;
            this.labelGradFrom.Text = "ОТ:";
            // 
            // numericUpDownGradStep
            // 
            this.numericUpDownGradStep.DecimalPlaces = 2;
            this.numericUpDownGradStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownGradStep.Location = new System.Drawing.Point(345, 44);
            this.numericUpDownGradStep.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownGradStep.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownGradStep.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownGradStep.Name = "numericUpDownGradStep";
            this.numericUpDownGradStep.Size = new System.Drawing.Size(79, 22);
            this.numericUpDownGradStep.TabIndex = 5;
            // 
            // numericUpDownGradTo
            // 
            this.numericUpDownGradTo.DecimalPlaces = 2;
            this.numericUpDownGradTo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownGradTo.Location = new System.Drawing.Point(189, 44);
            this.numericUpDownGradTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownGradTo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownGradTo.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownGradTo.Name = "numericUpDownGradTo";
            this.numericUpDownGradTo.Size = new System.Drawing.Size(79, 22);
            this.numericUpDownGradTo.TabIndex = 5;
            // 
            // numericUpDownGradFrom
            // 
            this.numericUpDownGradFrom.DecimalPlaces = 2;
            this.numericUpDownGradFrom.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownGradFrom.Location = new System.Drawing.Point(49, 44);
            this.numericUpDownGradFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownGradFrom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownGradFrom.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownGradFrom.Name = "numericUpDownGradFrom";
            this.numericUpDownGradFrom.Size = new System.Drawing.Size(79, 22);
            this.numericUpDownGradFrom.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 15);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(186, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Градации скоростей ветра";
            // 
            // comboBoxSpeedGradations
            // 
            this.comboBoxSpeedGradations.FormattingEnabled = true;
            this.comboBoxSpeedGradations.Location = new System.Drawing.Point(203, 11);
            this.comboBoxSpeedGradations.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxSpeedGradations.Name = "comboBoxSpeedGradations";
            this.comboBoxSpeedGradations.Size = new System.Drawing.Size(160, 24);
            this.comboBoxSpeedGradations.TabIndex = 1;
            this.comboBoxSpeedGradations.SelectedValueChanged += new System.EventHandler(this.comboBoxSpeedGradations_SelectedValueChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.buttonCancel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonSave, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonAccept, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 420);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(645, 30);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(567, 0);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 30);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(3, 0);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(92, 30);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAccept.Location = new System.Drawing.Point(273, 0);
            this.buttonAccept.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(102, 30);
            this.buttonAccept.TabIndex = 2;
            this.buttonAccept.Text = "Применить";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(637, 385);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Источники данных";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поиск в Расписании Погоды";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 21);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(146, 21);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Использовать API";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 48);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(245, 21);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Использовать БД Метеостанций";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // FormOptions
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(651, 452);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.Shown += new System.EventHandler(this.formOptions_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNormalLawTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNormalLawFrom)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGradStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGradTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGradFrom)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxMapProvider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBoxSectionLength;
        private System.Windows.Forms.TextBox textBoxMinimalSpeedDeviation;
        private System.Windows.Forms.TextBox textBoxAirDensity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDaysToNewInterval;
        private System.Windows.Forms.CheckBox checkBoxMinCorrWet;
        private System.Windows.Forms.CheckBox checkBoxMinCorrTemp;
        private System.Windows.Forms.CheckBox checkBoxMinCorrDirection;
        private System.Windows.Forms.CheckBox checkBoxMinCorrSpeed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxMinimalCoeffCorrel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxNearestMSRadius;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownNormalLawTo;
        private System.Windows.Forms.NumericUpDown numericUpDownNormalLawFrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelRegionLimitsFile;
        private System.Windows.Forms.Label labelCoordinatesMSFile;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonSelectRegionLimitsFile;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonSelectCoordinatesMSFile;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label labelGradStep;
        private System.Windows.Forms.Label labelGradTo;
        private System.Windows.Forms.Label labelGradFrom;
        private System.Windows.Forms.NumericUpDown numericUpDownGradStep;
        private System.Windows.Forms.NumericUpDown numericUpDownGradTo;
        private System.Windows.Forms.NumericUpDown numericUpDownGradFrom;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxSpeedGradations;
        private System.Windows.Forms.CheckBox checkBoxCalculateAirDensity;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}