namespace WindEnergy.UI.Tools
{
    partial class FormRangeTerrain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRangeTerrain));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxTerrainSecond = new System.Windows.Forms.GroupBox();
            this.labelWaterDistanceType = new System.Windows.Forms.Label();
            this.comboBoxWaterDistance = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonUnstable = new System.Windows.Forms.RadioButton();
            this.radioButtonStable = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxMicroclimate = new System.Windows.Forms.ComboBox();
            this.labelMesoclimateStatus = new System.Windows.Forms.Label();
            this.comboBoxMesoclimate = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonTerrainSecond = new System.Windows.Forms.RadioButton();
            this.radioButtonTerrainFirst = new System.Windows.Forms.RadioButton();
            this.groupBoxTerrainFirst = new System.Windows.Forms.GroupBox();
            this.buttonSelectMSCoordinates = new System.Windows.Forms.Button();
            this.buttonSelectPointCoordinates = new System.Windows.Forms.Button();
            this.labelPointClassesStatus = new System.Windows.Forms.Label();
            this.buttonEnterPointClasses = new System.Windows.Forms.Button();
            this.labelMSClassesStatus = new System.Windows.Forms.Label();
            this.buttonEnterMSClasses = new System.Windows.Forms.Button();
            this.labelPointCoordinatesStatus = new System.Windows.Forms.Label();
            this.labelMSCoordinatesStatus = new System.Windows.Forms.Label();
            this.groupBoxTerrainSecond.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxTerrainFirst.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTerrainSecond
            // 
            this.groupBoxTerrainSecond.Controls.Add(this.labelWaterDistanceType);
            this.groupBoxTerrainSecond.Controls.Add(this.comboBoxWaterDistance);
            this.groupBoxTerrainSecond.Controls.Add(this.groupBox2);
            this.groupBoxTerrainSecond.Controls.Add(this.label2);
            this.groupBoxTerrainSecond.Controls.Add(this.comboBoxMicroclimate);
            this.groupBoxTerrainSecond.Controls.Add(this.labelMesoclimateStatus);
            this.groupBoxTerrainSecond.Controls.Add(this.comboBoxMesoclimate);
            this.groupBoxTerrainSecond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTerrainSecond.Location = new System.Drawing.Point(353, 53);
            this.groupBoxTerrainSecond.Name = "groupBoxTerrainSecond";
            this.groupBoxTerrainSecond.Size = new System.Drawing.Size(326, 203);
            this.groupBoxTerrainSecond.TabIndex = 8;
            this.groupBoxTerrainSecond.TabStop = false;
            // 
            // labelWaterDistanceType
            // 
            this.labelWaterDistanceType.AutoSize = true;
            this.labelWaterDistanceType.Location = new System.Drawing.Point(6, 155);
            this.labelWaterDistanceType.Name = "labelWaterDistanceType";
            this.labelWaterDistanceType.Size = new System.Drawing.Size(287, 13);
            this.labelWaterDistanceType.TabIndex = 7;
            this.labelWaterDistanceType.Text = "Расположение МС относительно водных поверхностей";
            // 
            // comboBoxWaterDistance
            // 
            this.comboBoxWaterDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWaterDistance.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxWaterDistance.FormattingEnabled = true;
            this.comboBoxWaterDistance.Location = new System.Drawing.Point(9, 171);
            this.comboBoxWaterDistance.Name = "comboBoxWaterDistance";
            this.comboBoxWaterDistance.Size = new System.Drawing.Size(308, 21);
            this.comboBoxWaterDistance.TabIndex = 6;
            this.comboBoxWaterDistance.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBoxWaterDistance.SelectionChangeCommitted += new System.EventHandler(this.comboBoxWaterDistance_SelectionChangeCommitted);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.radioButtonUnstable);
            this.groupBox2.Controls.Add(this.radioButtonStable);
            this.groupBox2.Location = new System.Drawing.Point(9, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(308, 51);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Стратификация атмосферы в точке ВЭС";
            // 
            // radioButtonUnstable
            // 
            this.radioButtonUnstable.AutoSize = true;
            this.radioButtonUnstable.Location = new System.Drawing.Point(159, 19);
            this.radioButtonUnstable.Name = "radioButtonUnstable";
            this.radioButtonUnstable.Size = new System.Drawing.Size(96, 17);
            this.radioButtonUnstable.TabIndex = 1;
            this.radioButtonUnstable.Text = "Неустойчивая";
            this.radioButtonUnstable.UseVisualStyleBackColor = true;
            this.radioButtonUnstable.CheckedChanged += new System.EventHandler(this.radioButtonStratification_CheckedChanged);
            // 
            // radioButtonStable
            // 
            this.radioButtonStable.AutoSize = true;
            this.radioButtonStable.Checked = true;
            this.radioButtonStable.Location = new System.Drawing.Point(6, 19);
            this.radioButtonStable.Name = "radioButtonStable";
            this.radioButtonStable.Size = new System.Drawing.Size(85, 17);
            this.radioButtonStable.TabIndex = 0;
            this.radioButtonStable.TabStop = true;
            this.radioButtonStable.Text = "Устойчивая";
            this.radioButtonStable.UseVisualStyleBackColor = true;
            this.radioButtonStable.CheckedChanged += new System.EventHandler(this.radioButtonStratification_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Тип микрорельефа точки ВЭС";
            // 
            // comboBoxMicroclimate
            // 
            this.comboBoxMicroclimate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMicroclimate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxMicroclimate.FormattingEnabled = true;
            this.comboBoxMicroclimate.Location = new System.Drawing.Point(9, 74);
            this.comboBoxMicroclimate.Name = "comboBoxMicroclimate";
            this.comboBoxMicroclimate.Size = new System.Drawing.Size(308, 21);
            this.comboBoxMicroclimate.TabIndex = 3;
            this.comboBoxMicroclimate.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBoxMicroclimate.SelectionChangeCommitted += new System.EventHandler(this.comboBoxMicroclimate_SelectionChangeCommitted);
            // 
            // labelMesoclimateStatus
            // 
            this.labelMesoclimateStatus.AutoSize = true;
            this.labelMesoclimateStatus.Location = new System.Drawing.Point(6, 16);
            this.labelMesoclimateStatus.Name = "labelMesoclimateStatus";
            this.labelMesoclimateStatus.Size = new System.Drawing.Size(154, 13);
            this.labelMesoclimateStatus.TabIndex = 2;
            this.labelMesoclimateStatus.Text = "Тип мезорельефа точки ВЭС";
            // 
            // comboBoxMesoclimate
            // 
            this.comboBoxMesoclimate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMesoclimate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxMesoclimate.FormattingEnabled = true;
            this.comboBoxMesoclimate.Location = new System.Drawing.Point(9, 32);
            this.comboBoxMesoclimate.Name = "comboBoxMesoclimate";
            this.comboBoxMesoclimate.Size = new System.Drawing.Size(308, 21);
            this.comboBoxMesoclimate.TabIndex = 0;
            this.comboBoxMesoclimate.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBoxMesoclimate.SelectionChangeCommitted += new System.EventHandler(this.comboBoxMesoclimate_SelectionChangeCommitted);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonCalculate, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxTerrainFirst, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxTerrainSecond, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(682, 324);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // progressBar1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar1, 2);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(3, 307);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(676, 14);
            this.progressBar1.TabIndex = 12;
            // 
            // buttonCalculate
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonCalculate, 2);
            this.buttonCalculate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCalculate.Location = new System.Drawing.Point(3, 262);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(676, 39);
            this.buttonCalculate.TabIndex = 9;
            this.buttonCalculate.Text = "Пересчитать ряд";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.radioButtonTerrainSecond);
            this.groupBox1.Controls.Add(this.radioButtonTerrainFirst);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(676, 44);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип рельефа";
            // 
            // radioButtonTerrainSecond
            // 
            this.radioButtonTerrainSecond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonTerrainSecond.AutoSize = true;
            this.radioButtonTerrainSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonTerrainSecond.Location = new System.Drawing.Point(549, 19);
            this.radioButtonTerrainSecond.Name = "radioButtonTerrainSecond";
            this.radioButtonTerrainSecond.Size = new System.Drawing.Size(121, 21);
            this.radioButtonTerrainSecond.TabIndex = 1;
            this.radioButtonTerrainSecond.Text = "II тип рельефа";
            this.radioButtonTerrainSecond.UseVisualStyleBackColor = true;
            this.radioButtonTerrainSecond.CheckedChanged += new System.EventHandler(this.radioButtonTerrain_CheckedChanged);
            // 
            // radioButtonTerrainFirst
            // 
            this.radioButtonTerrainFirst.AutoSize = true;
            this.radioButtonTerrainFirst.Checked = true;
            this.radioButtonTerrainFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonTerrainFirst.Location = new System.Drawing.Point(6, 19);
            this.radioButtonTerrainFirst.Name = "radioButtonTerrainFirst";
            this.radioButtonTerrainFirst.Size = new System.Drawing.Size(118, 21);
            this.radioButtonTerrainFirst.TabIndex = 0;
            this.radioButtonTerrainFirst.TabStop = true;
            this.radioButtonTerrainFirst.Text = "Ⅰ тип рельефа";
            this.radioButtonTerrainFirst.UseVisualStyleBackColor = true;
            this.radioButtonTerrainFirst.CheckedChanged += new System.EventHandler(this.radioButtonTerrain_CheckedChanged);
            // 
            // groupBoxTerrainFirst
            // 
            this.groupBoxTerrainFirst.Controls.Add(this.buttonSelectMSCoordinates);
            this.groupBoxTerrainFirst.Controls.Add(this.buttonSelectPointCoordinates);
            this.groupBoxTerrainFirst.Controls.Add(this.labelPointClassesStatus);
            this.groupBoxTerrainFirst.Controls.Add(this.buttonEnterPointClasses);
            this.groupBoxTerrainFirst.Controls.Add(this.labelMSClassesStatus);
            this.groupBoxTerrainFirst.Controls.Add(this.buttonEnterMSClasses);
            this.groupBoxTerrainFirst.Controls.Add(this.labelPointCoordinatesStatus);
            this.groupBoxTerrainFirst.Controls.Add(this.labelMSCoordinatesStatus);
            this.groupBoxTerrainFirst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTerrainFirst.Location = new System.Drawing.Point(3, 53);
            this.groupBoxTerrainFirst.Name = "groupBoxTerrainFirst";
            this.groupBoxTerrainFirst.Size = new System.Drawing.Size(344, 203);
            this.groupBoxTerrainFirst.TabIndex = 7;
            this.groupBoxTerrainFirst.TabStop = false;
            // 
            // buttonSelectMSCoordinates
            // 
            this.buttonSelectMSCoordinates.Location = new System.Drawing.Point(6, 19);
            this.buttonSelectMSCoordinates.Name = "buttonSelectMSCoordinates";
            this.buttonSelectMSCoordinates.Size = new System.Drawing.Size(218, 23);
            this.buttonSelectMSCoordinates.TabIndex = 4;
            this.buttonSelectMSCoordinates.Text = "Выбрать точку МС";
            this.buttonSelectMSCoordinates.UseVisualStyleBackColor = true;
            this.buttonSelectMSCoordinates.Click += new System.EventHandler(this.buttonSelectMSCoordinates_Click);
            // 
            // buttonSelectPointCoordinates
            // 
            this.buttonSelectPointCoordinates.Location = new System.Drawing.Point(6, 48);
            this.buttonSelectPointCoordinates.Name = "buttonSelectPointCoordinates";
            this.buttonSelectPointCoordinates.Size = new System.Drawing.Size(218, 23);
            this.buttonSelectPointCoordinates.TabIndex = 0;
            this.buttonSelectPointCoordinates.Text = "Выбрать точку ВЭС";
            this.buttonSelectPointCoordinates.UseVisualStyleBackColor = true;
            this.buttonSelectPointCoordinates.Click += new System.EventHandler(this.buttonSelectPointCoordinates_Click);
            // 
            // labelPointClassesStatus
            // 
            this.labelPointClassesStatus.AutoSize = true;
            this.labelPointClassesStatus.Location = new System.Drawing.Point(230, 111);
            this.labelPointClassesStatus.Name = "labelPointClassesStatus";
            this.labelPointClassesStatus.Size = new System.Drawing.Size(22, 13);
            this.labelPointClassesStatus.TabIndex = 5;
            this.labelPointClassesStatus.Text = "ОК";
            // 
            // buttonEnterPointClasses
            // 
            this.buttonEnterPointClasses.Location = new System.Drawing.Point(6, 106);
            this.buttonEnterPointClasses.Name = "buttonEnterPointClasses";
            this.buttonEnterPointClasses.Size = new System.Drawing.Size(218, 23);
            this.buttonEnterPointClasses.TabIndex = 1;
            this.buttonEnterPointClasses.Text = "Ввести классы открытости точки ВЭС";
            this.buttonEnterPointClasses.UseVisualStyleBackColor = true;
            this.buttonEnterPointClasses.Click += new System.EventHandler(this.buttonEnterPointClasses_Click);
            // 
            // labelMSClassesStatus
            // 
            this.labelMSClassesStatus.AutoSize = true;
            this.labelMSClassesStatus.Location = new System.Drawing.Point(230, 82);
            this.labelMSClassesStatus.Name = "labelMSClassesStatus";
            this.labelMSClassesStatus.Size = new System.Drawing.Size(22, 13);
            this.labelMSClassesStatus.TabIndex = 5;
            this.labelMSClassesStatus.Text = "ОК";
            // 
            // buttonEnterMSClasses
            // 
            this.buttonEnterMSClasses.Location = new System.Drawing.Point(6, 77);
            this.buttonEnterMSClasses.Name = "buttonEnterMSClasses";
            this.buttonEnterMSClasses.Size = new System.Drawing.Size(218, 23);
            this.buttonEnterMSClasses.TabIndex = 2;
            this.buttonEnterMSClasses.Text = "Ввести классы открытости МС";
            this.buttonEnterMSClasses.UseVisualStyleBackColor = true;
            this.buttonEnterMSClasses.Click += new System.EventHandler(this.buttonEnterMSClasses_Click);
            // 
            // labelPointCoordinatesStatus
            // 
            this.labelPointCoordinatesStatus.AutoSize = true;
            this.labelPointCoordinatesStatus.Location = new System.Drawing.Point(230, 53);
            this.labelPointCoordinatesStatus.Name = "labelPointCoordinatesStatus";
            this.labelPointCoordinatesStatus.Size = new System.Drawing.Size(22, 13);
            this.labelPointCoordinatesStatus.TabIndex = 5;
            this.labelPointCoordinatesStatus.Text = "ОК";
            // 
            // labelMSCoordinatesStatus
            // 
            this.labelMSCoordinatesStatus.AutoSize = true;
            this.labelMSCoordinatesStatus.Location = new System.Drawing.Point(230, 24);
            this.labelMSCoordinatesStatus.Name = "labelMSCoordinatesStatus";
            this.labelMSCoordinatesStatus.Size = new System.Drawing.Size(22, 13);
            this.labelMSCoordinatesStatus.TabIndex = 5;
            this.labelMSCoordinatesStatus.Text = "ОК";
            // 
            // FormRangeTerrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 324);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(698, 322);
            this.Name = "FormRangeTerrain";
            this.Text = "Пересчет скорости ветра в точку ВЭС";
            this.Load += new System.EventHandler(this.FormRangeTerrain_Load);
            this.groupBoxTerrainSecond.ResumeLayout(false);
            this.groupBoxTerrainSecond.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxTerrainFirst.ResumeLayout(false);
            this.groupBoxTerrainFirst.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBoxTerrainSecond;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonTerrainSecond;
        private System.Windows.Forms.RadioButton radioButtonTerrainFirst;
        private System.Windows.Forms.GroupBox groupBoxTerrainFirst;
        private System.Windows.Forms.Button buttonSelectMSCoordinates;
        private System.Windows.Forms.Button buttonSelectPointCoordinates;
        private System.Windows.Forms.Label labelPointClassesStatus;
        private System.Windows.Forms.Button buttonEnterPointClasses;
        private System.Windows.Forms.Label labelMSClassesStatus;
        private System.Windows.Forms.Button buttonEnterMSClasses;
        private System.Windows.Forms.Label labelPointCoordinatesStatus;
        private System.Windows.Forms.Label labelMSCoordinatesStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonUnstable;
        private System.Windows.Forms.RadioButton radioButtonStable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxMicroclimate;
        private System.Windows.Forms.Label labelMesoclimateStatus;
        private System.Windows.Forms.ComboBox comboBoxMesoclimate;
        private System.Windows.Forms.Label labelWaterDistanceType;
        private System.Windows.Forms.ComboBox comboBoxWaterDistance;
    }
}