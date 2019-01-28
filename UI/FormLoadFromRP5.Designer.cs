namespace WindEnergy.UI
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
            this.comboBoxWMO = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dateTimePickerFromDate
            // 
            this.dateTimePickerFromDate.Location = new System.Drawing.Point(15, 67);
            this.dateTimePickerFromDate.Name = "dateTimePickerFromDate";
            this.dateTimePickerFromDate.Size = new System.Drawing.Size(146, 20);
            this.dateTimePickerFromDate.TabIndex = 0;
            // 
            // dateTimePickerToDate
            // 
            this.dateTimePickerToDate.Location = new System.Drawing.Point(218, 67);
            this.dateTimePickerToDate.Name = "dateTimePickerToDate";
            this.dateTimePickerToDate.Size = new System.Drawing.Size(150, 20);
            this.dateTimePickerToDate.TabIndex = 1;
            this.dateTimePickerToDate.Value = new System.DateTime(2019, 1, 26, 0, 26, 13, 0);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(156, 119);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(75, 23);
            this.buttonDownload.TabIndex = 2;
            this.buttonDownload.Text = "Загрузить";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // comboBoxWMO
            // 
            this.comboBoxWMO.FormattingEnabled = true;
            this.comboBoxWMO.Location = new System.Drawing.Point(12, 12);
            this.comboBoxWMO.Name = "comboBoxWMO";
            this.comboBoxWMO.Size = new System.Drawing.Size(356, 21);
            this.comboBoxWMO.TabIndex = 3;
            this.comboBoxWMO.SelectionChangeCommitted += new System.EventHandler(this.comboBoxWMO_SelectionChangeCommitted);
            this.comboBoxWMO.TextUpdate += new System.EventHandler(this.comboBoxWMO_TextUpdate);
            // 
            // FormLoadFromRP5
            // 
            this.AcceptButton = this.buttonDownload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 289);
            this.Controls.Add(this.comboBoxWMO);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.dateTimePickerToDate);
            this.Controls.Add(this.dateTimePickerFromDate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLoadFromRP5";
            this.Text = "Загрузка ряда с Расписания погоды";
            this.Shown += new System.EventHandler(this.formLoadFromRP5_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerFromDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerToDate;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.ComboBox comboBoxWMO;
    }
}