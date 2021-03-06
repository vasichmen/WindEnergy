﻿namespace WindEnergy.UI.Tools
{
    partial class FormCalcYear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCalcYear));
            this.labelRecomendedYear = new System.Windows.Forms.Label();
            this.labelSpeedDeviation = new System.Windows.Forms.Label();
            this.labelExpectDeviation = new System.Windows.Forms.Label();
            this.labelAverageCalcYearSpeed = new System.Windows.Forms.Label();
            this.labelMaxSpeed = new System.Windows.Forms.Label();
            this.labelInterval = new System.Windows.Forms.Label();
            this.labelCompletness = new System.Windows.Forms.Label();
            this.buttonSaveResults = new System.Windows.Forms.Button();
            this.labelAverageYearsSpeed = new System.Windows.Forms.Label();
            this.buttonOpenCalcYear = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewExt1 = new WindEnergy.UI.Ext.DataGridViewExt();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExt1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelRecomendedYear
            // 
            this.labelRecomendedYear.AutoSize = true;
            this.labelRecomendedYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRecomendedYear.Location = new System.Drawing.Point(9, 22);
            this.labelRecomendedYear.Name = "labelRecomendedYear";
            this.labelRecomendedYear.Size = new System.Drawing.Size(328, 13);
            this.labelRecomendedYear.TabIndex = 1;
            this.labelRecomendedYear.Text = "Рекомендуется в качестве расчетного принять * год:";
            // 
            // labelSpeedDeviation
            // 
            this.labelSpeedDeviation.AutoSize = true;
            this.labelSpeedDeviation.Location = new System.Drawing.Point(9, 36);
            this.labelSpeedDeviation.Name = "labelSpeedDeviation";
            this.labelSpeedDeviation.Size = new System.Drawing.Size(25, 13);
            this.labelSpeedDeviation.TabIndex = 2;
            this.labelSpeedDeviation.Text = "      ";
            // 
            // labelExpectDeviation
            // 
            this.labelExpectDeviation.AutoSize = true;
            this.labelExpectDeviation.Location = new System.Drawing.Point(9, 48);
            this.labelExpectDeviation.Name = "labelExpectDeviation";
            this.labelExpectDeviation.Size = new System.Drawing.Size(25, 13);
            this.labelExpectDeviation.TabIndex = 3;
            this.labelExpectDeviation.Text = "      ";
            // 
            // labelAverageCalcYearSpeed
            // 
            this.labelAverageCalcYearSpeed.AutoSize = true;
            this.labelAverageCalcYearSpeed.Location = new System.Drawing.Point(9, 63);
            this.labelAverageCalcYearSpeed.Name = "labelAverageCalcYearSpeed";
            this.labelAverageCalcYearSpeed.Size = new System.Drawing.Size(25, 13);
            this.labelAverageCalcYearSpeed.TabIndex = 4;
            this.labelAverageCalcYearSpeed.Text = "      ";
            // 
            // labelMaxSpeed
            // 
            this.labelMaxSpeed.AutoSize = true;
            this.labelMaxSpeed.Location = new System.Drawing.Point(9, 74);
            this.labelMaxSpeed.Name = "labelMaxSpeed";
            this.labelMaxSpeed.Size = new System.Drawing.Size(25, 13);
            this.labelMaxSpeed.TabIndex = 5;
            this.labelMaxSpeed.Text = "      ";
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(9, 87);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(25, 13);
            this.labelInterval.TabIndex = 6;
            this.labelInterval.Text = "      ";
            // 
            // labelCompletness
            // 
            this.labelCompletness.AutoSize = true;
            this.labelCompletness.Location = new System.Drawing.Point(9, 100);
            this.labelCompletness.Name = "labelCompletness";
            this.labelCompletness.Size = new System.Drawing.Size(25, 13);
            this.labelCompletness.TabIndex = 7;
            this.labelCompletness.Text = "      ";
            // 
            // buttonSaveResults
            // 
            this.buttonSaveResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveResults.Location = new System.Drawing.Point(605, 9);
            this.buttonSaveResults.Name = "buttonSaveResults";
            this.buttonSaveResults.Size = new System.Drawing.Size(121, 36);
            this.buttonSaveResults.TabIndex = 8;
            this.buttonSaveResults.Text = "Сохранить результаты в файл";
            this.buttonSaveResults.UseVisualStyleBackColor = true;
            this.buttonSaveResults.Click += new System.EventHandler(this.buttonSaveResults_Click);
            // 
            // labelAverageYearsSpeed
            // 
            this.labelAverageYearsSpeed.AutoSize = true;
            this.labelAverageYearsSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAverageYearsSpeed.Location = new System.Drawing.Point(9, 9);
            this.labelAverageYearsSpeed.Name = "labelAverageYearsSpeed";
            this.labelAverageYearsSpeed.Size = new System.Drawing.Size(27, 13);
            this.labelAverageYearsSpeed.TabIndex = 9;
            this.labelAverageYearsSpeed.Text = "     ";
            // 
            // buttonOpenCalcYear
            // 
            this.buttonOpenCalcYear.Location = new System.Drawing.Point(605, 51);
            this.buttonOpenCalcYear.Name = "buttonOpenCalcYear";
            this.buttonOpenCalcYear.Size = new System.Drawing.Size(121, 36);
            this.buttonOpenCalcYear.TabIndex = 10;
            this.buttonOpenCalcYear.Text = "Открыть расчетный год";
            this.toolTip1.SetToolTip(this.buttonOpenCalcYear, "Открыть найденный расчетный год в новой вкладке");
            this.buttonOpenCalcYear.UseVisualStyleBackColor = true;
            this.buttonOpenCalcYear.Visible = false;
            this.buttonOpenCalcYear.Click += new System.EventHandler(this.buttonOpenCalcYear_Click);
            // 
            // dataGridViewExt1
            // 
            this.dataGridViewExt1.AllowUserToAddRows = false;
            this.dataGridViewExt1.AllowUserToDeleteRows = false;
            this.dataGridViewExt1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewExt1.CausesValidation = false;
            this.dataGridViewExt1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExt1.Location = new System.Drawing.Point(12, 116);
            this.dataGridViewExt1.Name = "dataGridViewExt1";
            this.dataGridViewExt1.ReadOnly = true;
            this.dataGridViewExt1.Size = new System.Drawing.Size(714, 304);
            this.dataGridViewExt1.TabIndex = 0;
            this.dataGridViewExt1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridViewExt1_ColumnAdded);
            // 
            // FormCalcYear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 432);
            this.Controls.Add(this.buttonOpenCalcYear);
            this.Controls.Add(this.labelAverageYearsSpeed);
            this.Controls.Add(this.buttonSaveResults);
            this.Controls.Add(this.labelCompletness);
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.labelMaxSpeed);
            this.Controls.Add(this.labelAverageCalcYearSpeed);
            this.Controls.Add(this.labelExpectDeviation);
            this.Controls.Add(this.labelSpeedDeviation);
            this.Controls.Add(this.labelRecomendedYear);
            this.Controls.Add(this.dataGridViewExt1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(489, 428);
            this.Name = "FormCalcYear";
            this.Text = "Выбор расчётного года";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExt1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ext.DataGridViewExt dataGridViewExt1;
        private System.Windows.Forms.Label labelRecomendedYear;
        private System.Windows.Forms.Label labelSpeedDeviation;
        private System.Windows.Forms.Label labelExpectDeviation;
        private System.Windows.Forms.Label labelAverageCalcYearSpeed;
        private System.Windows.Forms.Label labelMaxSpeed;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.Label labelCompletness;
        private System.Windows.Forms.Button buttonSaveResults;
        private System.Windows.Forms.Label labelAverageYearsSpeed;
        private System.Windows.Forms.Button buttonOpenCalcYear;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}