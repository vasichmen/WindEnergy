namespace WindEnergy.UI.Tools
{
    partial class FormRepairRange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRepairRange));
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.labelInterval = new System.Windows.Forms.Label();
            this.labelRangeLength = new System.Windows.Forms.Label();
            this.labelMaxEmptySpace = new System.Windows.Forms.Label();
            this.labelCompletness = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonOpenFile = new System.Windows.Forms.RadioButton();
            this.radioButtonSelPoint = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxInterpolateMethod = new System.Windows.Forms.ComboBox();
            this.buttonRepairRange = new System.Windows.Forms.Button();
            this.comboBoxRepairInterval = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxMain.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.labelInterval);
            this.groupBoxMain.Controls.Add(this.labelRangeLength);
            this.groupBoxMain.Controls.Add(this.labelMaxEmptySpace);
            this.groupBoxMain.Controls.Add(this.labelCompletness);
            this.groupBoxMain.Controls.Add(this.groupBox3);
            this.groupBoxMain.Controls.Add(this.label2);
            this.groupBoxMain.Controls.Add(this.label1);
            this.groupBoxMain.Controls.Add(this.comboBoxInterpolateMethod);
            this.groupBoxMain.Controls.Add(this.buttonRepairRange);
            this.groupBoxMain.Controls.Add(this.comboBoxRepairInterval);
            this.groupBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMain.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(308, 321);
            this.groupBoxMain.TabIndex = 4;
            this.groupBoxMain.TabStop = false;
            this.groupBoxMain.Text = "Восстановление ряда";
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(6, 55);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(35, 13);
            this.labelInterval.TabIndex = 9;
            this.labelInterval.Text = "label3";
            // 
            // labelRangeLength
            // 
            this.labelRangeLength.AutoSize = true;
            this.labelRangeLength.Location = new System.Drawing.Point(6, 42);
            this.labelRangeLength.Name = "labelRangeLength";
            this.labelRangeLength.Size = new System.Drawing.Size(35, 13);
            this.labelRangeLength.TabIndex = 8;
            this.labelRangeLength.Text = "label3";
            // 
            // labelMaxEmptySpace
            // 
            this.labelMaxEmptySpace.AutoSize = true;
            this.labelMaxEmptySpace.Location = new System.Drawing.Point(6, 29);
            this.labelMaxEmptySpace.Name = "labelMaxEmptySpace";
            this.labelMaxEmptySpace.Size = new System.Drawing.Size(35, 13);
            this.labelMaxEmptySpace.TabIndex = 7;
            this.labelMaxEmptySpace.Text = "label3";
            // 
            // labelCompletness
            // 
            this.labelCompletness.AutoSize = true;
            this.labelCompletness.Location = new System.Drawing.Point(6, 16);
            this.labelCompletness.Name = "labelCompletness";
            this.labelCompletness.Size = new System.Drawing.Size(35, 13);
            this.labelCompletness.TabIndex = 6;
            this.labelCompletness.Text = "label3";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.radioButtonOpenFile);
            this.groupBox3.Controls.Add(this.radioButtonSelPoint);
            this.groupBox3.Location = new System.Drawing.Point(9, 183);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(293, 82);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Выбор источника ряда";
            // 
            // radioButtonOpenFile
            // 
            this.radioButtonOpenFile.AutoSize = true;
            this.radioButtonOpenFile.Location = new System.Drawing.Point(6, 39);
            this.radioButtonOpenFile.Name = "radioButtonOpenFile";
            this.radioButtonOpenFile.Size = new System.Drawing.Size(125, 17);
            this.radioButtonOpenFile.TabIndex = 1;
            this.radioButtonOpenFile.Text = "Выбрать файл ряда";
            this.toolTip1.SetToolTip(this.radioButtonOpenFile, "Выбрать файл, содержаий ряд, на основе которого будет восстанавливаться исходный " +
        "ряд");
            this.radioButtonOpenFile.UseVisualStyleBackColor = true;
            // 
            // radioButtonSelPoint
            // 
            this.radioButtonSelPoint.AutoSize = true;
            this.radioButtonSelPoint.Checked = true;
            this.radioButtonSelPoint.Location = new System.Drawing.Point(6, 19);
            this.radioButtonSelPoint.Name = "radioButtonSelPoint";
            this.radioButtonSelPoint.Size = new System.Drawing.Size(117, 17);
            this.radioButtonSelPoint.TabIndex = 0;
            this.radioButtonSelPoint.TabStop = true;
            this.radioButtonSelPoint.Text = "По точке на карте";
            this.toolTip1.SetToolTip(this.radioButtonSelPoint, "Выбрать точку на карте, для которой будет найдена ближайшая МС с доступными данны" +
        "ми");
            this.radioButtonSelPoint.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Метод интерполяции";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Длительность интервала";
            // 
            // comboBoxInterpolateMethod
            // 
            this.comboBoxInterpolateMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxInterpolateMethod.FormattingEnabled = true;
            this.comboBoxInterpolateMethod.Location = new System.Drawing.Point(9, 156);
            this.comboBoxInterpolateMethod.Name = "comboBoxInterpolateMethod";
            this.comboBoxInterpolateMethod.Size = new System.Drawing.Size(293, 21);
            this.comboBoxInterpolateMethod.TabIndex = 2;
            this.comboBoxInterpolateMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxInterpolateMethod_SelectedIndexChanged);
            // 
            // buttonRepairRange
            // 
            this.buttonRepairRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRepairRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRepairRange.Location = new System.Drawing.Point(77, 286);
            this.buttonRepairRange.Name = "buttonRepairRange";
            this.buttonRepairRange.Size = new System.Drawing.Size(139, 23);
            this.buttonRepairRange.TabIndex = 1;
            this.buttonRepairRange.Text = "Восстановить ряд";
            this.buttonRepairRange.UseVisualStyleBackColor = true;
            this.buttonRepairRange.Click += new System.EventHandler(this.buttonRepairRange_Click);
            // 
            // comboBoxRepairInterval
            // 
            this.comboBoxRepairInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRepairInterval.FormattingEnabled = true;
            this.comboBoxRepairInterval.Location = new System.Drawing.Point(9, 104);
            this.comboBoxRepairInterval.Name = "comboBoxRepairInterval";
            this.comboBoxRepairInterval.Size = new System.Drawing.Size(293, 21);
            this.comboBoxRepairInterval.TabIndex = 0;
            // 
            // FormRepairRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 321);
            this.Controls.Add(this.groupBoxMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(10000, 360);
            this.MinimumSize = new System.Drawing.Size(324, 360);
            this.Name = "FormRepairRange";
            this.Text = "Восстановление ряда";
            this.Shown += new System.EventHandler(this.formRepairRange_Shown);
            this.groupBoxMain.ResumeLayout(false);
            this.groupBoxMain.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonOpenFile;
        private System.Windows.Forms.RadioButton radioButtonSelPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxInterpolateMethod;
        private System.Windows.Forms.Button buttonRepairRange;
        private System.Windows.Forms.ComboBox comboBoxRepairInterval;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.Label labelRangeLength;
        private System.Windows.Forms.Label labelMaxEmptySpace;
        private System.Windows.Forms.Label labelCompletness;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}