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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRepairRange));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonOpenFile = new System.Windows.Forms.RadioButton();
            this.radioButtonSelPoint = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxInterpolateMethod = new System.Windows.Forms.ComboBox();
            this.buttonRepairRange = new System.Windows.Forms.Button();
            this.comboBoxRepairInterval = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBoxInterpolateMethod);
            this.groupBox2.Controls.Add(this.buttonRepairRange);
            this.groupBox2.Controls.Add(this.comboBoxRepairInterval);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 248);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Восстановление ряда";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonOpenFile);
            this.groupBox3.Controls.Add(this.radioButtonSelPoint);
            this.groupBox3.Location = new System.Drawing.Point(9, 117);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(210, 82);
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
            this.radioButtonSelPoint.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Метод интерполяции";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Длительность интервала";
            // 
            // comboBoxInterpolateMethod
            // 
            this.comboBoxInterpolateMethod.FormattingEnabled = true;
            this.comboBoxInterpolateMethod.Location = new System.Drawing.Point(9, 90);
            this.comboBoxInterpolateMethod.Name = "comboBoxInterpolateMethod";
            this.comboBoxInterpolateMethod.Size = new System.Drawing.Size(210, 21);
            this.comboBoxInterpolateMethod.TabIndex = 2;
            this.comboBoxInterpolateMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxInterpolateMethod_SelectedIndexChanged);
            // 
            // buttonRepairRange
            // 
            this.buttonRepairRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRepairRange.Location = new System.Drawing.Point(50, 205);
            this.buttonRepairRange.Name = "buttonRepairRange";
            this.buttonRepairRange.Size = new System.Drawing.Size(133, 23);
            this.buttonRepairRange.TabIndex = 1;
            this.buttonRepairRange.Text = "Восстановить ряд";
            this.buttonRepairRange.UseVisualStyleBackColor = true;
            // 
            // comboBoxRepairInterval
            // 
            this.comboBoxRepairInterval.FormattingEnabled = true;
            this.comboBoxRepairInterval.Location = new System.Drawing.Point(9, 38);
            this.comboBoxRepairInterval.Name = "comboBoxRepairInterval";
            this.comboBoxRepairInterval.Size = new System.Drawing.Size(210, 21);
            this.comboBoxRepairInterval.TabIndex = 0;
            // 
            // FormRepairRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 450);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRepairRange";
            this.Text = "Восстановление ряда";
            this.Shown += new System.EventHandler(this.formRepairRange_Shown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonOpenFile;
        private System.Windows.Forms.RadioButton radioButtonSelPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxInterpolateMethod;
        private System.Windows.Forms.Button buttonRepairRange;
        private System.Windows.Forms.ComboBox comboBoxRepairInterval;
    }
}