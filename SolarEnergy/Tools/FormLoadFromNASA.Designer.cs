namespace SolarEnergy.UI.Tools
{
    partial class FormLoadFromNASA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoadFromNASA));
            this.buttonSelectPoint = new System.Windows.Forms.Button();
            this.comboBoxSourceType = new System.Windows.Forms.ComboBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPointStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDailyModel = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonSelectPoint
            // 
            this.buttonSelectPoint.Location = new System.Drawing.Point(12, 104);
            this.buttonSelectPoint.Name = "buttonSelectPoint";
            this.buttonSelectPoint.Size = new System.Drawing.Size(117, 23);
            this.buttonSelectPoint.TabIndex = 0;
            this.buttonSelectPoint.Text = "Выбрать точку";
            this.buttonSelectPoint.UseVisualStyleBackColor = true;
            this.buttonSelectPoint.Click += new System.EventHandler(this.buttonSelectPoint_Click);
            // 
            // comboBoxSourceType
            // 
            this.comboBoxSourceType.FormattingEnabled = true;
            this.comboBoxSourceType.Location = new System.Drawing.Point(15, 25);
            this.comboBoxSourceType.Name = "comboBoxSourceType";
            this.comboBoxSourceType.Size = new System.Drawing.Size(191, 21);
            this.comboBoxSourceType.TabIndex = 1;
            this.comboBoxSourceType.SelectedValueChanged += new System.EventHandler(this.comboBoxSourceType_SelectedValueChanged);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(12, 133);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(117, 23);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "Загрузить данные";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выберите тип исходных данных";
            // 
            // labelPointStatus
            // 
            this.labelPointStatus.AutoSize = true;
            this.labelPointStatus.Location = new System.Drawing.Point(135, 109);
            this.labelPointStatus.Name = "labelPointStatus";
            this.labelPointStatus.Size = new System.Drawing.Size(68, 13);
            this.labelPointStatus.TabIndex = 4;
            this.labelPointStatus.Text = "Не выбрана";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Выберите модель распределения";
            // 
            // comboBoxDailyModel
            // 
            this.comboBoxDailyModel.FormattingEnabled = true;
            this.comboBoxDailyModel.Location = new System.Drawing.Point(15, 77);
            this.comboBoxDailyModel.Name = "comboBoxDailyModel";
            this.comboBoxDailyModel.Size = new System.Drawing.Size(191, 21);
            this.comboBoxDailyModel.TabIndex = 5;
            this.comboBoxDailyModel.SelectedValueChanged += new System.EventHandler(this.comboBoxDailyModel_SelectedValueChanged);
            // 
            // FormLoadFromNASA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxDailyModel);
            this.Controls.Add(this.labelPointStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.comboBoxSourceType);
            this.Controls.Add(this.buttonSelectPoint);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLoadFromNASA";
            this.Text = "Загрузить данные с NASA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectPoint;
        private System.Windows.Forms.ComboBox comboBoxSourceType;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPointStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDailyModel;
    }
}