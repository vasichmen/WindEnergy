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
            this.comboBoxMapProvider = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownNormalLawFrom = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNormalLawTo = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonSelectCoordinatesMSFile = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonSelectRegionLimitsFile = new System.Windows.Forms.Button();
            this.labelCoordinatesMSFile = new System.Windows.Forms.Label();
            this.labelRegionLimitsFile = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNormalLawFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNormalLawTo)).BeginInit();
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(488, 367);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(484, 335);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(476, 309);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основные";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBoxMapProvider
            // 
            this.comboBoxMapProvider.FormattingEnabled = true;
            this.comboBoxMapProvider.Location = new System.Drawing.Point(7, 19);
            this.comboBoxMapProvider.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxMapProvider.Name = "comboBoxMapProvider";
            this.comboBoxMapProvider.Size = new System.Drawing.Size(134, 21);
            this.comboBoxMapProvider.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип карты:";
            // 
            // tabPage2
            // 
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
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(476, 309);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Расчёты";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinCorrWet
            // 
            this.checkBoxMinCorrWet.AutoSize = true;
            this.checkBoxMinCorrWet.Location = new System.Drawing.Point(287, 195);
            this.checkBoxMinCorrWet.Name = "checkBoxMinCorrWet";
            this.checkBoxMinCorrWet.Size = new System.Drawing.Size(82, 17);
            this.checkBoxMinCorrWet.TabIndex = 15;
            this.checkBoxMinCorrWet.Text = "Влажность";
            this.toolTip1.SetToolTip(this.checkBoxMinCorrWet, "Использовать ограничение коэффициента корреляции для влажности");
            this.checkBoxMinCorrWet.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinCorrTemp
            // 
            this.checkBoxMinCorrTemp.AutoSize = true;
            this.checkBoxMinCorrTemp.Location = new System.Drawing.Point(188, 195);
            this.checkBoxMinCorrTemp.Name = "checkBoxMinCorrTemp";
            this.checkBoxMinCorrTemp.Size = new System.Drawing.Size(93, 17);
            this.checkBoxMinCorrTemp.TabIndex = 14;
            this.checkBoxMinCorrTemp.Text = "Температура";
            this.toolTip1.SetToolTip(this.checkBoxMinCorrTemp, "Использовать ограничение коэффициента корреляции для ткмпературы");
            this.checkBoxMinCorrTemp.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinCorrDirection
            // 
            this.checkBoxMinCorrDirection.AutoSize = true;
            this.checkBoxMinCorrDirection.Location = new System.Drawing.Point(95, 195);
            this.checkBoxMinCorrDirection.Name = "checkBoxMinCorrDirection";
            this.checkBoxMinCorrDirection.Size = new System.Drawing.Size(94, 17);
            this.checkBoxMinCorrDirection.TabIndex = 13;
            this.checkBoxMinCorrDirection.Text = "Направление";
            this.toolTip1.SetToolTip(this.checkBoxMinCorrDirection, "Использовать ограничение коэффициента корреляции для направления");
            this.checkBoxMinCorrDirection.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinCorrSpeed
            // 
            this.checkBoxMinCorrSpeed.AutoSize = true;
            this.checkBoxMinCorrSpeed.Location = new System.Drawing.Point(23, 195);
            this.checkBoxMinCorrSpeed.Name = "checkBoxMinCorrSpeed";
            this.checkBoxMinCorrSpeed.Size = new System.Drawing.Size(74, 17);
            this.checkBoxMinCorrSpeed.TabIndex = 12;
            this.checkBoxMinCorrSpeed.Text = "Скорость";
            this.toolTip1.SetToolTip(this.checkBoxMinCorrSpeed, "Использовать ограничение коэффициента корреляции для скорости");
            this.checkBoxMinCorrSpeed.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Минимальный коэфф. корреляции";
            // 
            // textBoxMinimalCoeffCorrel
            // 
            this.textBoxMinimalCoeffCorrel.Location = new System.Drawing.Point(206, 169);
            this.textBoxMinimalCoeffCorrel.Name = "textBoxMinimalCoeffCorrel";
            this.textBoxMinimalCoeffCorrel.Size = new System.Drawing.Size(100, 20);
            this.textBoxMinimalCoeffCorrel.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Максимальное расстояние до МС, м";
            // 
            // textBoxNearestMSRadius
            // 
            this.textBoxNearestMSRadius.Location = new System.Drawing.Point(206, 119);
            this.textBoxNearestMSRadius.Name = "textBoxNearestMSRadius";
            this.textBoxNearestMSRadius.Size = new System.Drawing.Size(100, 20);
            this.textBoxNearestMSRadius.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Минимальный диапазон Δt, дней";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Шаг дискретизации Δt, шт";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отклонение скорости, м/с";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Плотность воздуха, кг/м^3";
            // 
            // textBoxDaysToNewInterval
            // 
            this.textBoxDaysToNewInterval.Location = new System.Drawing.Point(206, 93);
            this.textBoxDaysToNewInterval.Name = "textBoxDaysToNewInterval";
            this.textBoxDaysToNewInterval.Size = new System.Drawing.Size(100, 20);
            this.textBoxDaysToNewInterval.TabIndex = 3;
            // 
            // textBoxSectionLength
            // 
            this.textBoxSectionLength.Location = new System.Drawing.Point(206, 67);
            this.textBoxSectionLength.Name = "textBoxSectionLength";
            this.textBoxSectionLength.Size = new System.Drawing.Size(100, 20);
            this.textBoxSectionLength.TabIndex = 2;
            // 
            // textBoxMinimalSpeedDeviation
            // 
            this.textBoxMinimalSpeedDeviation.Location = new System.Drawing.Point(206, 41);
            this.textBoxMinimalSpeedDeviation.Name = "textBoxMinimalSpeedDeviation";
            this.textBoxMinimalSpeedDeviation.Size = new System.Drawing.Size(100, 20);
            this.textBoxMinimalSpeedDeviation.TabIndex = 1;
            // 
            // textBoxAirDensity
            // 
            this.textBoxAirDensity.Location = new System.Drawing.Point(206, 15);
            this.textBoxAirDensity.Name = "textBoxAirDensity";
            this.textBoxAirDensity.Size = new System.Drawing.Size(100, 20);
            this.textBoxAirDensity.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.buttonCancel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonSave, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 341);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(484, 24);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(426, 0);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(56, 24);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(2, 0);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(69, 24);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Диапазон критерия Пирсона";
            // 
            // numericUpDownNormalLawFrom
            // 
            this.numericUpDownNormalLawFrom.DecimalPlaces = 2;
            this.numericUpDownNormalLawFrom.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownNormalLawFrom.Location = new System.Drawing.Point(234, 143);
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
            this.numericUpDownNormalLawFrom.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownNormalLawFrom.TabIndex = 17;
            // 
            // numericUpDownNormalLawTo
            // 
            this.numericUpDownNormalLawTo.DecimalPlaces = 2;
            this.numericUpDownNormalLawTo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownNormalLawTo.Location = new System.Drawing.Point(313, 143);
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
            this.numericUpDownNormalLawTo.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownNormalLawTo.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(203, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "ОТ:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(282, 145);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "ДО:";
            // 
            // buttonSelectCoordinatesMSFile
            // 
            this.buttonSelectCoordinatesMSFile.Location = new System.Drawing.Point(7, 68);
            this.buttonSelectCoordinatesMSFile.Name = "buttonSelectCoordinatesMSFile";
            this.buttonSelectCoordinatesMSFile.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectCoordinatesMSFile.TabIndex = 2;
            this.buttonSelectCoordinatesMSFile.Text = "Обзор";
            this.buttonSelectCoordinatesMSFile.UseVisualStyleBackColor = true;
            this.buttonSelectCoordinatesMSFile.Click += new System.EventHandler(this.buttonSelectCoordinatesMSFile_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(211, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Выбрать файл координат метеостанций";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(203, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Выбрать файл ограничений скоростей";
            // 
            // buttonSelectRegionLimitsFile
            // 
            this.buttonSelectRegionLimitsFile.Location = new System.Drawing.Point(7, 110);
            this.buttonSelectRegionLimitsFile.Name = "buttonSelectRegionLimitsFile";
            this.buttonSelectRegionLimitsFile.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectRegionLimitsFile.TabIndex = 4;
            this.buttonSelectRegionLimitsFile.Text = "Обзор";
            this.buttonSelectRegionLimitsFile.UseVisualStyleBackColor = true;
            this.buttonSelectRegionLimitsFile.Click += new System.EventHandler(this.buttonSelectRegionLimitsFile_Click);
            // 
            // labelCoordinatesMSFile
            // 
            this.labelCoordinatesMSFile.AutoSize = true;
            this.labelCoordinatesMSFile.Location = new System.Drawing.Point(88, 73);
            this.labelCoordinatesMSFile.Name = "labelCoordinatesMSFile";
            this.labelCoordinatesMSFile.Size = new System.Drawing.Size(92, 13);
            this.labelCoordinatesMSFile.TabIndex = 6;
            this.labelCoordinatesMSFile.Text = "Файл на выбран";
            // 
            // labelRegionLimitsFile
            // 
            this.labelRegionLimitsFile.AutoSize = true;
            this.labelRegionLimitsFile.Location = new System.Drawing.Point(88, 115);
            this.labelRegionLimitsFile.Name = "labelRegionLimitsFile";
            this.labelRegionLimitsFile.Size = new System.Drawing.Size(92, 13);
            this.labelRegionLimitsFile.TabIndex = 7;
            this.labelRegionLimitsFile.Text = "Файл не выбран";
            // 
            // FormOptions
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(488, 367);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
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
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNormalLawFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNormalLawTo)).EndInit();
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
    }
}