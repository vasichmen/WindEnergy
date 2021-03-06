﻿namespace WindEnergy.UI.Tools
{
    partial class FormRangeElevator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRangeElevator));
            this.textBoxFromHeight = new System.Windows.Forms.TextBox();
            this.textBoxToHeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxRadius = new System.Windows.Forms.TextBox();
            this.checkBoxUseRadius = new System.Windows.Forms.CheckBox();
            this.textBoxCoeffM = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonMonths = new System.Windows.Forms.Button();
            this.buttonElevate = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkBoxCustomCoeffM = new System.Windows.Forms.CheckBox();
            this.labelCurrentOptions = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxFromHeight
            // 
            this.textBoxFromHeight.Enabled = false;
            this.textBoxFromHeight.Location = new System.Drawing.Point(12, 12);
            this.textBoxFromHeight.Name = "textBoxFromHeight";
            this.textBoxFromHeight.Size = new System.Drawing.Size(100, 20);
            this.textBoxFromHeight.TabIndex = 0;
            this.textBoxFromHeight.Text = "10";
            this.toolTip1.SetToolTip(this.textBoxFromHeight, "Высота наблюдений выбранного ряда от уровня земли в метрах. Значение можно измени" +
        "ть только если используется среднемноголетний показатель степени, заданный вручн" +
        "ую");
            // 
            // textBoxToHeight
            // 
            this.textBoxToHeight.Location = new System.Drawing.Point(12, 38);
            this.textBoxToHeight.Name = "textBoxToHeight";
            this.textBoxToHeight.Size = new System.Drawing.Size(100, 20);
            this.textBoxToHeight.TabIndex = 1;
            this.textBoxToHeight.Text = "100";
            this.toolTip1.SetToolTip(this.textBoxToHeight, "Высота, на которую надо поднять ряд наблюдений от уровня земли в метрах");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Высота наблюдений, м";
            this.toolTip1.SetToolTip(this.label1, "Высота наблюдений выбранного ряда от уровня земли в метрах. Значение можно измени" +
        "ть только если используется среднемноголетний показатель степени, заданный вручн" +
        "ую");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Высота башни ВЭУ, м";
            this.toolTip1.SetToolTip(this.label2, "Высота, на которую надо поднять ряд наблюдений от уровня земли в метрах");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Радиус поиска АМС, км";
            this.toolTip1.SetToolTip(this.label3, "Область поиска АМС для получения коэффициентов пересчета скорости ветра на высоту" +
        "");
            // 
            // textBoxRadius
            // 
            this.textBoxRadius.Enabled = false;
            this.textBoxRadius.Location = new System.Drawing.Point(12, 87);
            this.textBoxRadius.Name = "textBoxRadius";
            this.textBoxRadius.Size = new System.Drawing.Size(100, 20);
            this.textBoxRadius.TabIndex = 12;
            this.textBoxRadius.Text = "1000";
            this.toolTip1.SetToolTip(this.textBoxRadius, "Область поиска АМС для получения коэффициентов пересчета скорости ветра на высоту" +
        "");
            this.textBoxRadius.TextChanged += new System.EventHandler(this.textBoxRadius_TextChanged);
            // 
            // checkBoxUseRadius
            // 
            this.checkBoxUseRadius.AutoSize = true;
            this.checkBoxUseRadius.Location = new System.Drawing.Point(12, 64);
            this.checkBoxUseRadius.Name = "checkBoxUseRadius";
            this.checkBoxUseRadius.Size = new System.Drawing.Size(188, 17);
            this.checkBoxUseRadius.TabIndex = 14;
            this.checkBoxUseRadius.Text = "Ограничить радиус поиска АМС";
            this.toolTip1.SetToolTip(this.checkBoxUseRadius, "Если выбрано, то при поиске АМС-аналога поиск будет производиться только в заданн" +
        "ой области");
            this.checkBoxUseRadius.UseVisualStyleBackColor = true;
            this.checkBoxUseRadius.CheckedChanged += new System.EventHandler(this.checkBoxUseRadius_CheckedChanged);
            // 
            // textBoxCoeffM
            // 
            this.textBoxCoeffM.Enabled = false;
            this.textBoxCoeffM.Location = new System.Drawing.Point(12, 136);
            this.textBoxCoeffM.Name = "textBoxCoeffM";
            this.textBoxCoeffM.Size = new System.Drawing.Size(100, 20);
            this.textBoxCoeffM.TabIndex = 12;
            this.textBoxCoeffM.Text = "0.18";
            this.toolTip1.SetToolTip(this.textBoxCoeffM, "Значение среднемноголетнего показателя степени для степенного преобразования скор" +
        "ости ветра на заданную высоту");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(288, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Значение среднемноголетнего показателя степени (m)";
            this.toolTip1.SetToolTip(this.label4, "Значение среднемноголетнего показателя степени для степенного преобразования скор" +
        "ости ветра на заданную высоту");
            // 
            // buttonMonths
            // 
            this.buttonMonths.Location = new System.Drawing.Point(12, 159);
            this.buttonMonths.Name = "buttonMonths";
            this.buttonMonths.Size = new System.Drawing.Size(100, 23);
            this.buttonMonths.TabIndex = 16;
            this.buttonMonths.Text = "По месяцам";
            this.toolTip1.SetToolTip(this.buttonMonths, "Задать среднемноголетний показатель степени для каждого месяца отдельно");
            this.buttonMonths.UseVisualStyleBackColor = true;
            this.buttonMonths.Click += new System.EventHandler(this.buttonMonths_Click);
            // 
            // buttonElevate
            // 
            this.buttonElevate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonElevate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonElevate.Location = new System.Drawing.Point(12, 223);
            this.buttonElevate.Name = "buttonElevate";
            this.buttonElevate.Size = new System.Drawing.Size(392, 23);
            this.buttonElevate.TabIndex = 4;
            this.buttonElevate.Text = "Пересчитать ряд";
            this.buttonElevate.UseVisualStyleBackColor = true;
            this.buttonElevate.Click += new System.EventHandler(this.buttonElevate_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 252);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(392, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // checkBoxCustomCoeffM
            // 
            this.checkBoxCustomCoeffM.AutoSize = true;
            this.checkBoxCustomCoeffM.Location = new System.Drawing.Point(12, 113);
            this.checkBoxCustomCoeffM.Name = "checkBoxCustomCoeffM";
            this.checkBoxCustomCoeffM.Size = new System.Drawing.Size(317, 17);
            this.checkBoxCustomCoeffM.TabIndex = 15;
            this.checkBoxCustomCoeffM.Text = "Задать среднемноголетний показатель степени вручную";
            this.checkBoxCustomCoeffM.UseVisualStyleBackColor = true;
            this.checkBoxCustomCoeffM.CheckedChanged += new System.EventHandler(this.checkBoxCustomCoeffM_CheckedChanged);
            // 
            // labelCurrentOptions
            // 
            this.labelCurrentOptions.AutoSize = true;
            this.labelCurrentOptions.Location = new System.Drawing.Point(12, 198);
            this.labelCurrentOptions.Name = "labelCurrentOptions";
            this.labelCurrentOptions.Size = new System.Drawing.Size(111, 13);
            this.labelCurrentOptions.TabIndex = 17;
            this.labelCurrentOptions.Text = "Текущие настройки:";
            // 
            // FormRangeElevator
            // 
            this.AcceptButton = this.buttonElevate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 286);
            this.Controls.Add(this.labelCurrentOptions);
            this.Controls.Add(this.buttonMonths);
            this.Controls.Add(this.checkBoxCustomCoeffM);
            this.Controls.Add(this.checkBoxUseRadius);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxCoeffM);
            this.Controls.Add(this.textBoxRadius);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonElevate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxToHeight);
            this.Controls.Add(this.textBoxFromHeight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(432, 325);
            this.Name = "FormRangeElevator";
            this.Text = "Расчет скорости ветра на высоте башни ВЭУ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFromHeight;
        private System.Windows.Forms.TextBox textBoxToHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonElevate;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxRadius;
        private System.Windows.Forms.CheckBox checkBoxUseRadius;
        private System.Windows.Forms.CheckBox checkBoxCustomCoeffM;
        private System.Windows.Forms.TextBox textBoxCoeffM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonMonths;
        private System.Windows.Forms.Label labelCurrentOptions;
    }
}