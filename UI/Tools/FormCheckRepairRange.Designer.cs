namespace WindEnergy.UI.Tools
{
    partial class FormCheckRepairRange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCheckRepairRange));
            this.comboBoxRepairInterval = new System.Windows.Forms.ComboBox();
            this.buttonCheckRange = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labeldirectDiap = new System.Windows.Forms.Label();
            this.labelspeedDiap = new System.Windows.Forms.Label();
            this.buttonEnterDirectionDiapason = new System.Windows.Forms.Button();
            this.buttonEnterSpeedDiapason = new System.Windows.Forms.Button();
            this.labelPointCoordinates = new System.Windows.Forms.Label();
            this.buttonSelectPoint = new System.Windows.Forms.Button();
            this.radioButtonEnterLimits = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectLimitsProvider = new System.Windows.Forms.RadioButton();
            this.comboBoxLimitsProvider = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonRepairRange = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxInterpolateMethod = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxRepairInterval
            // 
            this.comboBoxRepairInterval.FormattingEnabled = true;
            this.comboBoxRepairInterval.Location = new System.Drawing.Point(50, 38);
            this.comboBoxRepairInterval.Name = "comboBoxRepairInterval";
            this.comboBoxRepairInterval.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRepairInterval.TabIndex = 0;
            // 
            // buttonCheckRange
            // 
            this.buttonCheckRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheckRange.Location = new System.Drawing.Point(84, 205);
            this.buttonCheckRange.Name = "buttonCheckRange";
            this.buttonCheckRange.Size = new System.Drawing.Size(135, 23);
            this.buttonCheckRange.TabIndex = 1;
            this.buttonCheckRange.Text = "Проверить ряд";
            this.toolTip1.SetToolTip(this.buttonCheckRange, "Проверить ряд и исправить ошибки");
            this.buttonCheckRange.UseVisualStyleBackColor = true;
            this.buttonCheckRange.Click += new System.EventHandler(this.buttonCheckRange_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labeldirectDiap);
            this.groupBox1.Controls.Add(this.labelspeedDiap);
            this.groupBox1.Controls.Add(this.buttonEnterDirectionDiapason);
            this.groupBox1.Controls.Add(this.buttonEnterSpeedDiapason);
            this.groupBox1.Controls.Add(this.labelPointCoordinates);
            this.groupBox1.Controls.Add(this.buttonSelectPoint);
            this.groupBox1.Controls.Add(this.radioButtonEnterLimits);
            this.groupBox1.Controls.Add(this.radioButtonSelectLimitsProvider);
            this.groupBox1.Controls.Add(this.comboBoxLimitsProvider);
            this.groupBox1.Controls.Add(this.buttonCheckRange);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 248);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Проверка ряда";
            // 
            // labeldirectDiap
            // 
            this.labeldirectDiap.AutoSize = true;
            this.labeldirectDiap.Location = new System.Drawing.Point(159, 176);
            this.labeldirectDiap.Name = "labeldirectDiap";
            this.labeldirectDiap.Size = new System.Drawing.Size(130, 13);
            this.labeldirectDiap.TabIndex = 12;
            this.labeldirectDiap.Text = "Диапазоны не выбраны";
            // 
            // labelspeedDiap
            // 
            this.labelspeedDiap.AutoSize = true;
            this.labelspeedDiap.Location = new System.Drawing.Point(6, 176);
            this.labelspeedDiap.Name = "labelspeedDiap";
            this.labelspeedDiap.Size = new System.Drawing.Size(130, 13);
            this.labelspeedDiap.TabIndex = 11;
            this.labelspeedDiap.Text = "Диапазоны не выбраны";
            // 
            // buttonEnterDirectionDiapason
            // 
            this.buttonEnterDirectionDiapason.Enabled = false;
            this.buttonEnterDirectionDiapason.Location = new System.Drawing.Point(162, 137);
            this.buttonEnterDirectionDiapason.Name = "buttonEnterDirectionDiapason";
            this.buttonEnterDirectionDiapason.Size = new System.Drawing.Size(137, 36);
            this.buttonEnterDirectionDiapason.TabIndex = 10;
            this.buttonEnterDirectionDiapason.Text = "Ввести ограничения направлений";
            this.buttonEnterDirectionDiapason.UseVisualStyleBackColor = true;
            this.buttonEnterDirectionDiapason.Click += new System.EventHandler(this.buttonEnterDirectionDiapason_Click);
            // 
            // buttonEnterSpeedDiapason
            // 
            this.buttonEnterSpeedDiapason.Enabled = false;
            this.buttonEnterSpeedDiapason.Location = new System.Drawing.Point(9, 137);
            this.buttonEnterSpeedDiapason.Name = "buttonEnterSpeedDiapason";
            this.buttonEnterSpeedDiapason.Size = new System.Drawing.Size(137, 36);
            this.buttonEnterSpeedDiapason.TabIndex = 9;
            this.buttonEnterSpeedDiapason.Text = "Ввести ограничения скоростей";
            this.buttonEnterSpeedDiapason.UseVisualStyleBackColor = true;
            this.buttonEnterSpeedDiapason.Click += new System.EventHandler(this.buttonEnterSpeedDiapason_Click);
            // 
            // labelPointCoordinates
            // 
            this.labelPointCoordinates.AutoSize = true;
            this.labelPointCoordinates.Location = new System.Drawing.Point(111, 75);
            this.labelPointCoordinates.Name = "labelPointCoordinates";
            this.labelPointCoordinates.Size = new System.Drawing.Size(99, 13);
            this.labelPointCoordinates.TabIndex = 8;
            this.labelPointCoordinates.Text = "Точка не выбрана";
            // 
            // buttonSelectPoint
            // 
            this.buttonSelectPoint.Location = new System.Drawing.Point(9, 70);
            this.buttonSelectPoint.Name = "buttonSelectPoint";
            this.buttonSelectPoint.Size = new System.Drawing.Size(96, 23);
            this.buttonSelectPoint.TabIndex = 7;
            this.buttonSelectPoint.Text = "Выбрать точку";
            this.buttonSelectPoint.UseVisualStyleBackColor = true;
            this.buttonSelectPoint.Click += new System.EventHandler(this.buttonSelectPoint_Click);
            // 
            // radioButtonEnterLimits
            // 
            this.radioButtonEnterLimits.AutoSize = true;
            this.radioButtonEnterLimits.Location = new System.Drawing.Point(9, 114);
            this.radioButtonEnterLimits.Name = "radioButtonEnterLimits";
            this.radioButtonEnterLimits.Size = new System.Drawing.Size(172, 17);
            this.radioButtonEnterLimits.TabIndex = 6;
            this.radioButtonEnterLimits.Text = "Ввести ограничения вручную";
            this.radioButtonEnterLimits.UseVisualStyleBackColor = true;
            this.radioButtonEnterLimits.CheckedChanged += new System.EventHandler(this.radioButtonLimits_CheckedChanged);
            // 
            // radioButtonSelectLimitsProvider
            // 
            this.radioButtonSelectLimitsProvider.AutoSize = true;
            this.radioButtonSelectLimitsProvider.Checked = true;
            this.radioButtonSelectLimitsProvider.Location = new System.Drawing.Point(9, 20);
            this.radioButtonSelectLimitsProvider.Name = "radioButtonSelectLimitsProvider";
            this.radioButtonSelectLimitsProvider.Size = new System.Drawing.Size(185, 17);
            this.radioButtonSelectLimitsProvider.TabIndex = 5;
            this.radioButtonSelectLimitsProvider.TabStop = true;
            this.radioButtonSelectLimitsProvider.Text = "Выбрать источник ограничений";
            this.radioButtonSelectLimitsProvider.UseVisualStyleBackColor = true;
            this.radioButtonSelectLimitsProvider.CheckedChanged += new System.EventHandler(this.radioButtonLimits_CheckedChanged);
            // 
            // comboBoxLimitsProvider
            // 
            this.comboBoxLimitsProvider.FormattingEnabled = true;
            this.comboBoxLimitsProvider.Location = new System.Drawing.Point(9, 43);
            this.comboBoxLimitsProvider.Name = "comboBoxLimitsProvider";
            this.comboBoxLimitsProvider.Size = new System.Drawing.Size(210, 21);
            this.comboBoxLimitsProvider.TabIndex = 2;
            // 
            // buttonRepairRange
            // 
            this.buttonRepairRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRepairRange.Location = new System.Drawing.Point(50, 205);
            this.buttonRepairRange.Name = "buttonRepairRange";
            this.buttonRepairRange.Size = new System.Drawing.Size(133, 23);
            this.buttonRepairRange.TabIndex = 1;
            this.buttonRepairRange.Text = "Восстановить ряд";
            this.toolTip1.SetToolTip(this.buttonRepairRange, "Дополнение ряда до указанного интервала измерений");
            this.buttonRepairRange.UseVisualStyleBackColor = true;
            this.buttonRepairRange.Click += new System.EventHandler(this.buttonRepairRange_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBoxInterpolateMethod);
            this.groupBox2.Controls.Add(this.buttonRepairRange);
            this.groupBox2.Controls.Add(this.comboBoxRepairInterval);
            this.groupBox2.Location = new System.Drawing.Point(332, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 248);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Восстановление ряда";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Метод интерполяции";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Длительность интервала";
            // 
            // comboBoxInterpolateMethod
            // 
            this.comboBoxInterpolateMethod.FormattingEnabled = true;
            this.comboBoxInterpolateMethod.Location = new System.Drawing.Point(50, 90);
            this.comboBoxInterpolateMethod.Name = "comboBoxInterpolateMethod";
            this.comboBoxInterpolateMethod.Size = new System.Drawing.Size(121, 21);
            this.comboBoxInterpolateMethod.TabIndex = 2;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(382, 266);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(133, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(103, 266);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(128, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormCheckRepairRange
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(566, 296);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCheckRepairRange";
            this.Text = "Проверить и восстановить ряд";
            this.Shown += new System.EventHandler(this.formCheckRepairRange_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxRepairInterval;
        private System.Windows.Forms.Button buttonCheckRange;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxInterpolateMethod;
        private System.Windows.Forms.Button buttonRepairRange;
        private System.Windows.Forms.ComboBox comboBoxLimitsProvider;
        private System.Windows.Forms.Label labelPointCoordinates;
        private System.Windows.Forms.Button buttonSelectPoint;
        private System.Windows.Forms.RadioButton radioButtonEnterLimits;
        private System.Windows.Forms.RadioButton radioButtonSelectLimitsProvider;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labeldirectDiap;
        private System.Windows.Forms.Label labelspeedDiap;
        private System.Windows.Forms.Button buttonEnterDirectionDiapason;
        private System.Windows.Forms.Button buttonEnterSpeedDiapason;
    }
}