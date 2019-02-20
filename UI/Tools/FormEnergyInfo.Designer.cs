namespace WindEnergy.UI.Tools
{
    partial class FormEnergyInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEnergyInfo));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.zedGraphControlDirection = new ZedGraph.ZedGraphControl();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.labelPowerDensityTV = new System.Windows.Forms.Label();
            this.labelEnergyDensityTV = new System.Windows.Forms.Label();
            this.labelCvTV = new System.Windows.Forms.Label();
            this.labelV0TV = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.labelPowerDensity = new System.Windows.Forms.Label();
            this.labelEnergyDensity = new System.Windows.Forms.Label();
            this.labelCv = new System.Windows.Forms.Label();
            this.labelV0 = new System.Windows.Forms.Label();
            this.labelVmax = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.zedGraphControlSpeed = new ZedGraph.ZedGraphControl();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButtonSelectPeriod = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonSelectYearMonth = new System.Windows.Forms.RadioButton();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.comboBoxYearMonth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(8, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 523);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Характеристики ветра";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.zedGraphControlDirection, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.zedGraphControlSpeed, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(557, 504);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // zedGraphControlDirection
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.zedGraphControlDirection, 3);
            this.zedGraphControlDirection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControlDirection.Location = new System.Drawing.Point(3, 300);
            this.zedGraphControlDirection.Name = "zedGraphControlDirection";
            this.zedGraphControlDirection.ScrollGrace = 0D;
            this.zedGraphControlDirection.ScrollMaxX = 0D;
            this.zedGraphControlDirection.ScrollMaxY = 0D;
            this.zedGraphControlDirection.ScrollMaxY2 = 0D;
            this.zedGraphControlDirection.ScrollMinX = 0D;
            this.zedGraphControlDirection.ScrollMinY = 0D;
            this.zedGraphControlDirection.ScrollMinY2 = 0D;
            this.zedGraphControlDirection.Size = new System.Drawing.Size(551, 201);
            this.zedGraphControlDirection.TabIndex = 4;
            this.zedGraphControlDirection.UseExtendedPrintDialog = true;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label11);
            this.flowLayoutPanel3.Controls.Add(this.labelPowerDensityTV);
            this.flowLayoutPanel3.Controls.Add(this.labelEnergyDensityTV);
            this.flowLayoutPanel3.Controls.Add(this.labelCvTV);
            this.flowLayoutPanel3.Controls.Add(this.labelV0TV);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(356, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(198, 84);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "По повторяемости";
            // 
            // labelPowerDensityTV
            // 
            this.labelPowerDensityTV.AutoSize = true;
            this.labelPowerDensityTV.Location = new System.Drawing.Point(3, 13);
            this.labelPowerDensityTV.Name = "labelPowerDensityTV";
            this.labelPowerDensityTV.Size = new System.Drawing.Size(35, 13);
            this.labelPowerDensityTV.TabIndex = 5;
            this.labelPowerDensityTV.Text = "label6";
            // 
            // labelEnergyDensityTV
            // 
            this.labelEnergyDensityTV.AutoSize = true;
            this.labelEnergyDensityTV.Location = new System.Drawing.Point(3, 26);
            this.labelEnergyDensityTV.Name = "labelEnergyDensityTV";
            this.labelEnergyDensityTV.Size = new System.Drawing.Size(35, 13);
            this.labelEnergyDensityTV.TabIndex = 6;
            this.labelEnergyDensityTV.Text = "label6";
            // 
            // labelCvTV
            // 
            this.labelCvTV.AutoSize = true;
            this.labelCvTV.Location = new System.Drawing.Point(3, 39);
            this.labelCvTV.Name = "labelCvTV";
            this.labelCvTV.Size = new System.Drawing.Size(35, 13);
            this.labelCvTV.TabIndex = 7;
            this.labelCvTV.Text = "label6";
            // 
            // labelV0TV
            // 
            this.labelV0TV.AutoSize = true;
            this.labelV0TV.Location = new System.Drawing.Point(3, 52);
            this.labelV0TV.Name = "labelV0TV";
            this.labelV0TV.Size = new System.Drawing.Size(35, 13);
            this.labelV0TV.TabIndex = 8;
            this.labelV0TV.Text = "label6";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label10);
            this.flowLayoutPanel2.Controls.Add(this.labelPowerDensity);
            this.flowLayoutPanel2.Controls.Add(this.labelEnergyDensity);
            this.flowLayoutPanel2.Controls.Add(this.labelCv);
            this.flowLayoutPanel2.Controls.Add(this.labelV0);
            this.flowLayoutPanel2.Controls.Add(this.labelVmax);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(152, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(198, 84);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "По ряду наблюдений";
            // 
            // labelPowerDensity
            // 
            this.labelPowerDensity.AutoSize = true;
            this.labelPowerDensity.Location = new System.Drawing.Point(3, 13);
            this.labelPowerDensity.Name = "labelPowerDensity";
            this.labelPowerDensity.Size = new System.Drawing.Size(35, 13);
            this.labelPowerDensity.TabIndex = 0;
            this.labelPowerDensity.Text = "label6";
            // 
            // labelEnergyDensity
            // 
            this.labelEnergyDensity.AutoSize = true;
            this.labelEnergyDensity.Location = new System.Drawing.Point(3, 26);
            this.labelEnergyDensity.Name = "labelEnergyDensity";
            this.labelEnergyDensity.Size = new System.Drawing.Size(35, 13);
            this.labelEnergyDensity.TabIndex = 1;
            this.labelEnergyDensity.Text = "label6";
            // 
            // labelCv
            // 
            this.labelCv.AutoSize = true;
            this.labelCv.Location = new System.Drawing.Point(3, 39);
            this.labelCv.Name = "labelCv";
            this.labelCv.Size = new System.Drawing.Size(35, 13);
            this.labelCv.TabIndex = 2;
            this.labelCv.Text = "label6";
            // 
            // labelV0
            // 
            this.labelV0.AutoSize = true;
            this.labelV0.Location = new System.Drawing.Point(3, 52);
            this.labelV0.Name = "labelV0";
            this.labelV0.Size = new System.Drawing.Size(35, 13);
            this.labelV0.TabIndex = 3;
            this.labelV0.Text = "label6";
            // 
            // labelVmax
            // 
            this.labelVmax.AutoSize = true;
            this.labelVmax.Location = new System.Drawing.Point(3, 65);
            this.labelVmax.Name = "labelVmax";
            this.labelVmax.Size = new System.Drawing.Size(35, 13);
            this.labelVmax.TabIndex = 4;
            this.labelVmax.Text = "label6";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label9);
            this.flowLayoutPanel1.Controls.Add(this.label8);
            this.flowLayoutPanel1.Controls.Add(this.label7);
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 13, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(143, 84);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Удельная мощность";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Удельная энергия";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Коэффициент вариации";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Средняя скорость";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Максимальная скорость";
            // 
            // zedGraphControlSpeed
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.zedGraphControlSpeed, 3);
            this.zedGraphControlSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControlSpeed.Location = new System.Drawing.Point(3, 93);
            this.zedGraphControlSpeed.Name = "zedGraphControlSpeed";
            this.zedGraphControlSpeed.ScrollGrace = 0D;
            this.zedGraphControlSpeed.ScrollMaxX = 0D;
            this.zedGraphControlSpeed.ScrollMaxY = 0D;
            this.zedGraphControlSpeed.ScrollMaxY2 = 0D;
            this.zedGraphControlSpeed.ScrollMinX = 0D;
            this.zedGraphControlSpeed.ScrollMinY = 0D;
            this.zedGraphControlSpeed.ScrollMinY2 = 0D;
            this.zedGraphControlSpeed.Size = new System.Drawing.Size(551, 201);
            this.zedGraphControlSpeed.TabIndex = 3;
            this.zedGraphControlSpeed.UseExtendedPrintDialog = true;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(31, 41);
            this.dateTimePickerFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(151, 20);
            this.dateTimePickerFrom.TabIndex = 6;
            this.dateTimePickerFrom.ValueChanged += new System.EventHandler(this.comboBoxYear_SelectionChangeCommitted);
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(31, 65);
            this.dateTimePickerTo.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(151, 20);
            this.dateTimePickerTo.TabIndex = 7;
            this.dateTimePickerTo.ValueChanged += new System.EventHandler(this.comboBoxYear_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 71);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "По:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 43);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "С:";
            // 
            // radioButtonSelectPeriod
            // 
            this.radioButtonSelectPeriod.AutoSize = true;
            this.radioButtonSelectPeriod.Checked = true;
            this.radioButtonSelectPeriod.Location = new System.Drawing.Point(51, 16);
            this.radioButtonSelectPeriod.Name = "radioButtonSelectPeriod";
            this.radioButtonSelectPeriod.Size = new System.Drawing.Size(108, 17);
            this.radioButtonSelectPeriod.TabIndex = 10;
            this.radioButtonSelectPeriod.TabStop = true;
            this.radioButtonSelectPeriod.Text = "Выбрать период";
            this.radioButtonSelectPeriod.UseVisualStyleBackColor = true;
            this.radioButtonSelectPeriod.CheckedChanged += new System.EventHandler(this.comboBoxYear_SelectionChangeCommitted);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonSelectYearMonth);
            this.groupBox3.Controls.Add(this.comboBoxYear);
            this.groupBox3.Controls.Add(this.comboBoxYearMonth);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.radioButtonSelectPeriod);
            this.groupBox3.Controls.Add(this.dateTimePickerFrom);
            this.groupBox3.Controls.Add(this.dateTimePickerTo);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(7, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(456, 100);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // radioButtonSelectYearMonth
            // 
            this.radioButtonSelectYearMonth.AutoSize = true;
            this.radioButtonSelectYearMonth.Location = new System.Drawing.Point(266, 16);
            this.radioButtonSelectYearMonth.Name = "radioButtonSelectYearMonth";
            this.radioButtonSelectYearMonth.Size = new System.Drawing.Size(133, 17);
            this.radioButtonSelectYearMonth.TabIndex = 16;
            this.radioButtonSelectYearMonth.Text = "Выбрать год и месяц";
            this.radioButtonSelectYearMonth.UseVisualStyleBackColor = true;
            this.radioButtonSelectYearMonth.CheckedChanged += new System.EventHandler(this.comboBoxYear_SelectionChangeCommitted);
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.Enabled = false;
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(321, 36);
            this.comboBoxYear.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(132, 21);
            this.comboBoxYear.TabIndex = 12;
            this.comboBoxYear.SelectionChangeCommitted += new System.EventHandler(this.comboBoxYear_SelectionChangeCommitted);
            // 
            // comboBoxYearMonth
            // 
            this.comboBoxYearMonth.Enabled = false;
            this.comboBoxYearMonth.FormattingEnabled = true;
            this.comboBoxYearMonth.Location = new System.Drawing.Point(321, 63);
            this.comboBoxYearMonth.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxYearMonth.Name = "comboBoxYearMonth";
            this.comboBoxYearMonth.Size = new System.Drawing.Size(132, 21);
            this.comboBoxYearMonth.TabIndex = 13;
            this.comboBoxYearMonth.SelectionChangeCommitted += new System.EventHandler(this.comboBoxYear_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Выберите год:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Выберите месяц:";
            // 
            // FormEnergyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 641);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(487, 680);
            this.Name = "FormEnergyInfo";
            this.Text = "Основные энергетические характеристики ветра";
            this.Shown += new System.EventHandler(this.formEnergyInfo_Shown);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonSelectPeriod;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelVmax;
        private System.Windows.Forms.Label labelV0;
        private System.Windows.Forms.Label labelCv;
        private System.Windows.Forms.Label labelEnergyDensity;
        private System.Windows.Forms.Label labelPowerDensity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelV0TV;
        private System.Windows.Forms.Label labelCvTV;
        private System.Windows.Forms.Label labelEnergyDensityTV;
        private System.Windows.Forms.Label labelPowerDensityTV;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private ZedGraph.ZedGraphControl zedGraphControlDirection;
        private ZedGraph.ZedGraphControl zedGraphControlSpeed;
        private System.Windows.Forms.RadioButton radioButtonSelectYearMonth;
        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.ComboBox comboBoxYearMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}