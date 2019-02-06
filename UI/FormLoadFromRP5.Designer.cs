﻿namespace WindEnergy.UI
{
    partial class FormLoadFromRP5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoadFromRP5));
            this.dateTimePickerFromDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerToDate = new System.Windows.Forms.DateTimePicker();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.comboBoxPoint = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDateRange = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePickerFromDate
            // 
            this.dateTimePickerFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFromDate.Enabled = false;
            this.dateTimePickerFromDate.Location = new System.Drawing.Point(40, 86);
            this.dateTimePickerFromDate.Name = "dateTimePickerFromDate";
            this.dateTimePickerFromDate.Size = new System.Drawing.Size(132, 20);
            this.dateTimePickerFromDate.TabIndex = 0;
            // 
            // dateTimePickerToDate
            // 
            this.dateTimePickerToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerToDate.Enabled = false;
            this.dateTimePickerToDate.Location = new System.Drawing.Point(254, 86);
            this.dateTimePickerToDate.Name = "dateTimePickerToDate";
            this.dateTimePickerToDate.Size = new System.Drawing.Size(132, 20);
            this.dateTimePickerToDate.TabIndex = 1;
            this.dateTimePickerToDate.Value = new System.DateTime(2019, 1, 26, 0, 26, 13, 0);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDownload.Enabled = false;
            this.buttonDownload.Location = new System.Drawing.Point(12, 119);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(160, 23);
            this.buttonDownload.TabIndex = 2;
            this.buttonDownload.Text = "Загрузить";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // comboBoxPoint
            // 
            this.comboBoxPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPoint.FormattingEnabled = true;
            this.comboBoxPoint.Location = new System.Drawing.Point(12, 25);
            this.comboBoxPoint.Name = "comboBoxPoint";
            this.comboBoxPoint.Size = new System.Drawing.Size(374, 21);
            this.comboBoxPoint.TabIndex = 3;
            this.comboBoxPoint.SelectionChangeCommitted += new System.EventHandler(this.comboBoxWMO_SelectionChangeCommitted);
            this.comboBoxPoint.TextUpdate += new System.EventHandler(this.comboBoxWMO_TextUpdate);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Найдите нужную точку:";
            // 
            // labelDateRange
            // 
            this.labelDateRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDateRange.AutoSize = true;
            this.labelDateRange.Location = new System.Drawing.Point(9, 61);
            this.labelDateRange.Name = "labelDateRange";
            this.labelDateRange.Size = new System.Drawing.Size(81, 13);
            this.labelDateRange.TabIndex = 5;
            this.labelDateRange.Text = "Диапазон дат:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "ОТ:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "ДО:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(311, 119);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormLoadFromRP5
            // 
            this.AcceptButton = this.buttonDownload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(395, 154);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelDateRange);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxPoint);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.dateTimePickerToDate);
            this.Controls.Add(this.dateTimePickerFromDate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(411, 193);
            this.Name = "FormLoadFromRP5";
            this.Text = "Загрузка ряда с Расписания погоды";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormLoadFromRP5_FormClosed);
            this.Shown += new System.EventHandler(this.formLoadFromRP5_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerFromDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerToDate;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.ComboBox comboBoxPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelDateRange;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonCancel;
    }
}