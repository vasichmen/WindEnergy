﻿namespace WindEnergy.UI.Tools
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
            this.components = new System.ComponentModel.Container();
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
            this.progressBarProgress = new System.Windows.Forms.ProgressBar();
            this.linkLabelSelectOnMap = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxClearRange = new System.Windows.Forms.CheckBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateTimePickerFromDate
            // 
            this.dateTimePickerFromDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimePickerFromDate.Enabled = false;
            this.dateTimePickerFromDate.Location = new System.Drawing.Point(40, 78);
            this.dateTimePickerFromDate.Name = "dateTimePickerFromDate";
            this.dateTimePickerFromDate.Size = new System.Drawing.Size(132, 20);
            this.dateTimePickerFromDate.TabIndex = 0;
            this.dateTimePickerFromDate.ValueChanged += new System.EventHandler(this.dateTimePickerDate_ValueChanged);
            // 
            // dateTimePickerToDate
            // 
            this.dateTimePickerToDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimePickerToDate.Enabled = false;
            this.dateTimePickerToDate.Location = new System.Drawing.Point(40, 104);
            this.dateTimePickerToDate.Name = "dateTimePickerToDate";
            this.dateTimePickerToDate.Size = new System.Drawing.Size(132, 20);
            this.dateTimePickerToDate.TabIndex = 1;
            this.dateTimePickerToDate.Value = new System.DateTime(2019, 1, 26, 0, 26, 13, 0);
            this.dateTimePickerToDate.ValueChanged += new System.EventHandler(this.dateTimePickerDate_ValueChanged);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonDownload.Enabled = false;
            this.buttonDownload.Location = new System.Drawing.Point(254, 74);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(132, 23);
            this.buttonDownload.TabIndex = 2;
            this.buttonDownload.Text = "Загрузить";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // comboBoxPoint
            // 
            this.comboBoxPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPoint.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxPoint.FormattingEnabled = true;
            this.comboBoxPoint.Location = new System.Drawing.Point(12, 25);
            this.comboBoxPoint.Name = "comboBoxPoint";
            this.comboBoxPoint.Size = new System.Drawing.Size(374, 21);
            this.comboBoxPoint.TabIndex = 3;
            this.comboBoxPoint.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxPoint_DrawItem);
            this.comboBoxPoint.SelectionChangeCommitted += new System.EventHandler(this.comboBoxPoint_SelectionChangeCommitted);
            this.comboBoxPoint.TextUpdate += new System.EventHandler(this.comboBoxPoint_TextUpdate);
            this.comboBoxPoint.DropDownClosed += new System.EventHandler(this.comboBoxPoint_DropDownClosed);
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
            this.labelDateRange.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDateRange.AutoSize = true;
            this.labelDateRange.Location = new System.Drawing.Point(9, 53);
            this.labelDateRange.Name = "labelDateRange";
            this.labelDateRange.Size = new System.Drawing.Size(81, 13);
            this.labelDateRange.TabIndex = 5;
            this.labelDateRange.Text = "Диапазон дат:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "ОТ:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "ДО:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(254, 110);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(132, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // progressBarProgress
            // 
            this.progressBarProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarProgress.Location = new System.Drawing.Point(12, 168);
            this.progressBarProgress.Name = "progressBarProgress";
            this.progressBarProgress.Size = new System.Drawing.Size(374, 23);
            this.progressBarProgress.TabIndex = 9;
            // 
            // linkLabelSelectOnMap
            // 
            this.linkLabelSelectOnMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelSelectOnMap.AutoSize = true;
            this.linkLabelSelectOnMap.Location = new System.Drawing.Point(283, 9);
            this.linkLabelSelectOnMap.Name = "linkLabelSelectOnMap";
            this.linkLabelSelectOnMap.Size = new System.Drawing.Size(98, 13);
            this.linkLabelSelectOnMap.TabIndex = 10;
            this.linkLabelSelectOnMap.TabStop = true;
            this.linkLabelSelectOnMap.Text = "Выбрать на карте";
            this.linkLabelSelectOnMap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelShowOnMap_LinkClicked);
            // 
            // checkBoxClearRange
            // 
            this.checkBoxClearRange.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxClearRange.AutoSize = true;
            this.checkBoxClearRange.Checked = true;
            this.checkBoxClearRange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxClearRange.Location = new System.Drawing.Point(11, 130);
            this.checkBoxClearRange.Name = "checkBoxClearRange";
            this.checkBoxClearRange.Size = new System.Drawing.Size(173, 17);
            this.checkBoxClearRange.TabIndex = 11;
            this.checkBoxClearRange.Text = "Проверить загруженный ряд";
            this.toolTip1.SetToolTip(this.checkBoxClearRange, "После загрузки очистить ряд от ошибок");
            this.checkBoxClearRange.UseVisualStyleBackColor = true;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 152);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(70, 13);
            this.labelStatus.TabIndex = 12;
            this.labelStatus.Text = "Состояние...";
            // 
            // FormLoadFromRP5
            // 
            this.AcceptButton = this.buttonDownload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(395, 203);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.checkBoxClearRange);
            this.Controls.Add(this.linkLabelSelectOnMap);
            this.Controls.Add(this.progressBarProgress);
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
            this.MinimumSize = new System.Drawing.Size(411, 242);
            this.Name = "FormLoadFromRP5";
            this.Text = "Загрузка ряда с Расписания погоды";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.formLoadFromRP5_FormClosed);
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
        private System.Windows.Forms.ProgressBar progressBarProgress;
        private System.Windows.Forms.LinkLabel linkLabelSelectOnMap;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxClearRange;
        private System.Windows.Forms.Label labelStatus;
    }
}