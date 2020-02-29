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
            this.radioButtonUnstable = new System.Windows.Forms.RadioButton();
            this.radioButtonStable = new System.Windows.Forms.RadioButton();
            this.radioButtonTerrainMeso = new System.Windows.Forms.RadioButton();
            this.radioButtonTerrainMacro = new System.Windows.Forms.RadioButton();
            this.buttonSelectMSCoordinates = new System.Windows.Forms.Button();
            this.buttonSelectPointCoordinates = new System.Windows.Forms.Button();
            this.buttonEnterPointClasses = new System.Windows.Forms.Button();
            this.buttonEnterMSClasses = new System.Windows.Forms.Button();
            this.groupBoxTerrainSecond = new System.Windows.Forms.GroupBox();
            this.groupBoxStratification = new System.Windows.Forms.GroupBox();
            this.labelMicroclimateStatus = new System.Windows.Forms.Label();
            this.comboBoxMicroclimate = new System.Windows.Forms.ComboBox();
            this.labelMesoclimateStatus = new System.Windows.Forms.Label();
            this.comboBoxMesoclimate = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxTerrainFirst = new System.Windows.Forms.GroupBox();
            this.labelPointClassesStatus = new System.Windows.Forms.Label();
            this.labelMSClassesStatus = new System.Windows.Forms.Label();
            this.labelPointCoordinatesStatus = new System.Windows.Forms.Label();
            this.labelMSCoordinatesStatus = new System.Windows.Forms.Label();
            this.radioButtonTerrainMicro = new System.Windows.Forms.RadioButton();
            this.scintillaRecommendations = new ScintillaNET.Scintilla();
            this.groupBoxTerrainSecond.SuspendLayout();
            this.groupBoxStratification.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxTerrainFirst.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonUnstable
            // 
            this.radioButtonUnstable.AutoSize = true;
            this.radioButtonUnstable.Location = new System.Drawing.Point(159, 19);
            this.radioButtonUnstable.Name = "radioButtonUnstable";
            this.radioButtonUnstable.Size = new System.Drawing.Size(96, 17);
            this.radioButtonUnstable.TabIndex = 1;
            this.radioButtonUnstable.Text = "Неустойчивая";
            this.toolTip1.SetToolTip(this.radioButtonUnstable, resources.GetString("radioButtonUnstable.ToolTip"));
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
            this.toolTip1.SetToolTip(this.radioButtonStable, resources.GetString("radioButtonStable.ToolTip"));
            this.radioButtonStable.UseVisualStyleBackColor = true;
            this.radioButtonStable.CheckedChanged += new System.EventHandler(this.radioButtonStratification_CheckedChanged);
            // 
            // radioButtonTerrainMeso
            // 
            this.radioButtonTerrainMeso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonTerrainMeso.AutoSize = true;
            this.radioButtonTerrainMeso.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonTerrainMeso.Location = new System.Drawing.Point(301, 19);
            this.radioButtonTerrainMeso.Name = "radioButtonTerrainMeso";
            this.radioButtonTerrainMeso.Size = new System.Drawing.Size(110, 21);
            this.radioButtonTerrainMeso.TabIndex = 1;
            this.radioButtonTerrainMeso.Text = "Мезорельеф";
            this.toolTip1.SetToolTip(this.radioButtonTerrainMeso, "Неплоский, равнинно-холмистый и низкогорный рельеф с высотой до 750 м над уровнем" +
        " моря");
            this.radioButtonTerrainMeso.UseVisualStyleBackColor = true;
            this.radioButtonTerrainMeso.CheckedChanged += new System.EventHandler(this.radioButtonTerrain_CheckedChanged);
            // 
            // radioButtonTerrainMacro
            // 
            this.radioButtonTerrainMacro.AutoSize = true;
            this.radioButtonTerrainMacro.Checked = true;
            this.radioButtonTerrainMacro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonTerrainMacro.Location = new System.Drawing.Point(6, 19);
            this.radioButtonTerrainMacro.Name = "radioButtonTerrainMacro";
            this.radioButtonTerrainMacro.Size = new System.Drawing.Size(118, 21);
            this.radioButtonTerrainMacro.TabIndex = 0;
            this.radioButtonTerrainMacro.TabStop = true;
            this.radioButtonTerrainMacro.Text = "Макрорельеф";
            this.toolTip1.SetToolTip(this.radioButtonTerrainMacro, "Плоский рельеф с естественными и искуственными препятствиями высотой до 100 м (ра" +
        "внинная местность)");
            this.radioButtonTerrainMacro.UseVisualStyleBackColor = true;
            this.radioButtonTerrainMacro.CheckedChanged += new System.EventHandler(this.radioButtonTerrain_CheckedChanged);
            // 
            // buttonSelectMSCoordinates
            // 
            this.buttonSelectMSCoordinates.Location = new System.Drawing.Point(6, 19);
            this.buttonSelectMSCoordinates.Name = "buttonSelectMSCoordinates";
            this.buttonSelectMSCoordinates.Size = new System.Drawing.Size(218, 23);
            this.buttonSelectMSCoordinates.TabIndex = 4;
            this.buttonSelectMSCoordinates.Text = "Выбрать точку МС";
            this.toolTip1.SetToolTip(this.buttonSelectMSCoordinates, "Выбрать н акарте точку расположения ВЭС");
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
            this.toolTip1.SetToolTip(this.buttonSelectPointCoordinates, "Изменить точку расположения МС (если не задана ранее)");
            this.buttonSelectPointCoordinates.UseVisualStyleBackColor = true;
            this.buttonSelectPointCoordinates.Click += new System.EventHandler(this.buttonSelectPointCoordinates_Click);
            // 
            // buttonEnterPointClasses
            // 
            this.buttonEnterPointClasses.Location = new System.Drawing.Point(6, 106);
            this.buttonEnterPointClasses.Name = "buttonEnterPointClasses";
            this.buttonEnterPointClasses.Size = new System.Drawing.Size(218, 23);
            this.buttonEnterPointClasses.TabIndex = 1;
            this.buttonEnterPointClasses.Text = "Ввести классы открытости точки ВЭС";
            this.toolTip1.SetToolTip(this.buttonEnterPointClasses, "Указать классы открытости точки установки ВЭС");
            this.buttonEnterPointClasses.UseVisualStyleBackColor = true;
            this.buttonEnterPointClasses.Click += new System.EventHandler(this.buttonEnterPointClasses_Click);
            // 
            // buttonEnterMSClasses
            // 
            this.buttonEnterMSClasses.Location = new System.Drawing.Point(6, 77);
            this.buttonEnterMSClasses.Name = "buttonEnterMSClasses";
            this.buttonEnterMSClasses.Size = new System.Drawing.Size(218, 23);
            this.buttonEnterMSClasses.TabIndex = 2;
            this.buttonEnterMSClasses.Text = "Ввести классы открытости МС";
            this.toolTip1.SetToolTip(this.buttonEnterMSClasses, "Задать вручную классы открытости МС, если не удалось найти ближайшую МС из СБД Фл" +
        "югер");
            this.buttonEnterMSClasses.UseVisualStyleBackColor = true;
            this.buttonEnterMSClasses.Click += new System.EventHandler(this.buttonEnterMSClasses_Click);
            // 
            // groupBoxTerrainSecond
            // 
            this.groupBoxTerrainSecond.Controls.Add(this.groupBoxStratification);
            this.groupBoxTerrainSecond.Controls.Add(this.labelMicroclimateStatus);
            this.groupBoxTerrainSecond.Controls.Add(this.comboBoxMicroclimate);
            this.groupBoxTerrainSecond.Controls.Add(this.labelMesoclimateStatus);
            this.groupBoxTerrainSecond.Controls.Add(this.comboBoxMesoclimate);
            this.groupBoxTerrainSecond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTerrainSecond.Location = new System.Drawing.Point(353, 103);
            this.groupBoxTerrainSecond.Name = "groupBoxTerrainSecond";
            this.groupBoxTerrainSecond.Size = new System.Drawing.Size(331, 163);
            this.groupBoxTerrainSecond.TabIndex = 8;
            this.groupBoxTerrainSecond.TabStop = false;
            // 
            // groupBoxStratification
            // 
            this.groupBoxStratification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxStratification.Controls.Add(this.radioButtonUnstable);
            this.groupBoxStratification.Controls.Add(this.radioButtonStable);
            this.groupBoxStratification.Location = new System.Drawing.Point(9, 101);
            this.groupBoxStratification.Name = "groupBoxStratification";
            this.groupBoxStratification.Size = new System.Drawing.Size(313, 51);
            this.groupBoxStratification.TabIndex = 5;
            this.groupBoxStratification.TabStop = false;
            this.groupBoxStratification.Text = "Стратификация атмосферы в точке ВЭС";
            // 
            // labelMicroclimateStatus
            // 
            this.labelMicroclimateStatus.AutoSize = true;
            this.labelMicroclimateStatus.Location = new System.Drawing.Point(6, 58);
            this.labelMicroclimateStatus.Name = "labelMicroclimateStatus";
            this.labelMicroclimateStatus.Size = new System.Drawing.Size(160, 13);
            this.labelMicroclimateStatus.TabIndex = 4;
            this.labelMicroclimateStatus.Text = "Тип микрорельефа точки ВЭС";
            // 
            // comboBoxMicroclimate
            // 
            this.comboBoxMicroclimate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMicroclimate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxMicroclimate.FormattingEnabled = true;
            this.comboBoxMicroclimate.Location = new System.Drawing.Point(9, 74);
            this.comboBoxMicroclimate.Name = "comboBoxMicroclimate";
            this.comboBoxMicroclimate.Size = new System.Drawing.Size(313, 21);
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
            this.comboBoxMesoclimate.Size = new System.Drawing.Size(313, 21);
            this.comboBoxMesoclimate.TabIndex = 0;
            this.comboBoxMesoclimate.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBoxMesoclimate.SelectionChangeCommitted += new System.EventHandler(this.comboBoxMesoclimate_SelectionChangeCommitted);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.buttonCalculate, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxTerrainFirst, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxTerrainSecond, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.scintillaRecommendations, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(687, 334);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // progressBar1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar1, 2);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(3, 317);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(681, 14);
            this.progressBar1.TabIndex = 12;
            // 
            // buttonCalculate
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonCalculate, 2);
            this.buttonCalculate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCalculate.Location = new System.Drawing.Point(3, 272);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(681, 39);
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
            this.groupBox1.Controls.Add(this.radioButtonTerrainMicro);
            this.groupBox1.Controls.Add(this.radioButtonTerrainMeso);
            this.groupBox1.Controls.Add(this.radioButtonTerrainMacro);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(3, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 44);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите тип рельефа";
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
            this.groupBoxTerrainFirst.Location = new System.Drawing.Point(3, 103);
            this.groupBoxTerrainFirst.Name = "groupBoxTerrainFirst";
            this.groupBoxTerrainFirst.Size = new System.Drawing.Size(344, 163);
            this.groupBoxTerrainFirst.TabIndex = 7;
            this.groupBoxTerrainFirst.TabStop = false;
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
            // labelMSClassesStatus
            // 
            this.labelMSClassesStatus.AutoSize = true;
            this.labelMSClassesStatus.Location = new System.Drawing.Point(230, 82);
            this.labelMSClassesStatus.Name = "labelMSClassesStatus";
            this.labelMSClassesStatus.Size = new System.Drawing.Size(22, 13);
            this.labelMSClassesStatus.TabIndex = 5;
            this.labelMSClassesStatus.Text = "ОК";
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
            // radioButtonTerrainMicro
            // 
            this.radioButtonTerrainMicro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonTerrainMicro.AutoSize = true;
            this.radioButtonTerrainMicro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonTerrainMicro.Location = new System.Drawing.Point(554, 17);
            this.radioButtonTerrainMicro.Name = "radioButtonTerrainMicro";
            this.radioButtonTerrainMicro.Size = new System.Drawing.Size(118, 21);
            this.radioButtonTerrainMicro.TabIndex = 2;
            this.radioButtonTerrainMicro.Text = "Микрорельеф";
            this.toolTip1.SetToolTip(this.radioButtonTerrainMicro, "Неплоский, равнинно-холмистый и низкогорный рельеф с высотой до 750 м над уровнем" +
        " моря");
            this.radioButtonTerrainMicro.UseVisualStyleBackColor = true;
            this.radioButtonTerrainMicro.CheckedChanged += new System.EventHandler(this.radioButtonTerrain_CheckedChanged);
            // 
            // scintillaRecommendations
            // 
            this.scintillaRecommendations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.scintillaRecommendations, 2);
            this.scintillaRecommendations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaRecommendations.EdgeMode = ScintillaNET.EdgeMode.MultiLine;
            this.scintillaRecommendations.EolMode = ScintillaNET.Eol.Lf;
            this.scintillaRecommendations.HScrollBar = false;
            this.scintillaRecommendations.Location = new System.Drawing.Point(3, 3);
            this.scintillaRecommendations.Name = "scintillaRecommendations";
            this.scintillaRecommendations.Size = new System.Drawing.Size(681, 44);
            this.scintillaRecommendations.TabIndex = 13;
            // 
            // FormRangeTerrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 334);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(703, 373);
            this.Name = "FormRangeTerrain";
            this.Text = "Пересчет скорости ветра в точку ВЭС";
            this.Load += new System.EventHandler(this.FormRangeTerrain_Load);
            this.groupBoxTerrainSecond.ResumeLayout(false);
            this.groupBoxTerrainSecond.PerformLayout();
            this.groupBoxStratification.ResumeLayout(false);
            this.groupBoxStratification.PerformLayout();
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
        private System.Windows.Forms.RadioButton radioButtonTerrainMeso;
        private System.Windows.Forms.RadioButton radioButtonTerrainMacro;
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
        private System.Windows.Forms.GroupBox groupBoxStratification;
        private System.Windows.Forms.RadioButton radioButtonUnstable;
        private System.Windows.Forms.RadioButton radioButtonStable;
        private System.Windows.Forms.Label labelMicroclimateStatus;
        private System.Windows.Forms.ComboBox comboBoxMicroclimate;
        private System.Windows.Forms.Label labelMesoclimateStatus;
        private System.Windows.Forms.ComboBox comboBoxMesoclimate;
        private System.Windows.Forms.RadioButton radioButtonTerrainMicro;
        private ScintillaNET.Scintilla scintillaRecommendations;
    }
}