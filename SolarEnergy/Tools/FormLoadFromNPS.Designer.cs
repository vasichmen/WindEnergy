namespace SolarEnergy.UI.Tools
{
    partial class FormLoadFromNPS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoadFromNPS));
            this.comboBoxMonthTransformer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSelectPoint = new System.Windows.Forms.Button();
            this.labelPointCoordinates = new System.Windows.Forms.Label();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxMonthTransformer
            // 
            this.comboBoxMonthTransformer.FormattingEnabled = true;
            this.comboBoxMonthTransformer.Location = new System.Drawing.Point(15, 25);
            this.comboBoxMonthTransformer.Name = "comboBoxMonthTransformer";
            this.comboBoxMonthTransformer.Size = new System.Drawing.Size(234, 21);
            this.comboBoxMonthTransformer.TabIndex = 0;
            this.comboBoxMonthTransformer.SelectedValueChanged += new System.EventHandler(this.comboBoxMonthTransformer_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберите способ перехода между месяцами";
            // 
            // buttonSelectPoint
            // 
            this.buttonSelectPoint.Location = new System.Drawing.Point(15, 52);
            this.buttonSelectPoint.Name = "buttonSelectPoint";
            this.buttonSelectPoint.Size = new System.Drawing.Size(109, 23);
            this.buttonSelectPoint.TabIndex = 2;
            this.buttonSelectPoint.Text = "Выбрать точку";
            this.buttonSelectPoint.UseVisualStyleBackColor = true;
            this.buttonSelectPoint.Click += new System.EventHandler(this.buttonSelectPoint_Click);
            // 
            // labelPointCoordinates
            // 
            this.labelPointCoordinates.AutoSize = true;
            this.labelPointCoordinates.Location = new System.Drawing.Point(130, 57);
            this.labelPointCoordinates.Name = "labelPointCoordinates";
            this.labelPointCoordinates.Size = new System.Drawing.Size(68, 13);
            this.labelPointCoordinates.TabIndex = 3;
            this.labelPointCoordinates.Text = "Не выбрана";
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(15, 81);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(234, 23);
            this.buttonLoad.TabIndex = 4;
            this.buttonLoad.Text = "Получить данные";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // FormLoadFromNPS
            // 
            this.AcceptButton = this.buttonLoad;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 134);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.labelPointCoordinates);
            this.Controls.Add(this.buttonSelectPoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxMonthTransformer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLoadFromNPS";
            this.Text = "Загрузить данные из НПС";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMonthTransformer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSelectPoint;
        private System.Windows.Forms.Label labelPointCoordinates;
        private System.Windows.Forms.Button buttonLoad;
    }
}