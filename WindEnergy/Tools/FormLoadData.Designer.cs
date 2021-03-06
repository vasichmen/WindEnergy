﻿namespace WindEnergy.UI.Tools
{
    partial class FormLoadData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoadData));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBarStatusMS = new System.Windows.Forms.ProgressBar();
            this.buttonStopMS = new System.Windows.Forms.Button();
            this.labelStatusMS = new System.Windows.Forms.Label();
            this.buttonStartMS = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBarStatusMaxSpeed = new System.Windows.Forms.ProgressBar();
            this.buttonStopMaxSpeed = new System.Windows.Forms.Button();
            this.labelStatusMaxSpeed = new System.Windows.Forms.Label();
            this.buttonStartMaxSpeed = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonUpdateRP5 = new System.Windows.Forms.Button();
            this.checkBoxSkipErrors = new System.Windows.Forms.CheckBox();
            this.progressBarStatusRP5 = new System.Windows.Forms.ProgressBar();
            this.buttonStopRP5 = new System.Windows.Forms.Button();
            this.labelStatusRP5 = new System.Windows.Forms.Label();
            this.buttonStartRP5 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.progressBarStatusNASA = new System.Windows.Forms.ProgressBar();
            this.buttonStopNASA = new System.Windows.Forms.Button();
            this.labelStatusNASA = new System.Windows.Forms.Label();
            this.buttonStartNASA = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.progressBarStatusMS);
            this.groupBox1.Controls.Add(this.buttonStopMS);
            this.groupBox1.Controls.Add(this.labelStatusMS);
            this.groupBox1.Controls.Add(this.buttonStartMS);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(757, 90);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Обновление списка метеостанций (www.rp5.ru)";
            // 
            // progressBarStatusMS
            // 
            this.progressBarStatusMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarStatusMS.Location = new System.Drawing.Point(90, 48);
            this.progressBarStatusMS.Name = "progressBarStatusMS";
            this.progressBarStatusMS.Size = new System.Drawing.Size(661, 23);
            this.progressBarStatusMS.TabIndex = 3;
            // 
            // buttonStopMS
            // 
            this.buttonStopMS.Enabled = false;
            this.buttonStopMS.Location = new System.Drawing.Point(6, 48);
            this.buttonStopMS.Name = "buttonStopMS";
            this.buttonStopMS.Size = new System.Drawing.Size(75, 23);
            this.buttonStopMS.TabIndex = 2;
            this.buttonStopMS.Text = "Остановить";
            this.buttonStopMS.UseVisualStyleBackColor = true;
            this.buttonStopMS.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // labelStatusMS
            // 
            this.labelStatusMS.AutoSize = true;
            this.labelStatusMS.Location = new System.Drawing.Point(87, 24);
            this.labelStatusMS.Name = "labelStatusMS";
            this.labelStatusMS.Size = new System.Drawing.Size(70, 13);
            this.labelStatusMS.TabIndex = 1;
            this.labelStatusMS.Text = "Состояние...";
            // 
            // buttonStartMS
            // 
            this.buttonStartMS.Location = new System.Drawing.Point(6, 19);
            this.buttonStartMS.Name = "buttonStartMS";
            this.buttonStartMS.Size = new System.Drawing.Size(75, 23);
            this.buttonStartMS.TabIndex = 0;
            this.buttonStartMS.Text = "Начать";
            this.buttonStartMS.UseVisualStyleBackColor = true;
            this.buttonStartMS.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.progressBarStatusMaxSpeed);
            this.groupBox2.Controls.Add(this.buttonStopMaxSpeed);
            this.groupBox2.Controls.Add(this.labelStatusMaxSpeed);
            this.groupBox2.Controls.Add(this.buttonStartMaxSpeed);
            this.groupBox2.Location = new System.Drawing.Point(12, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(757, 90);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Обновление максимальных скоростей ветра (www.energywind.ru)";
            // 
            // progressBarStatusMaxSpeed
            // 
            this.progressBarStatusMaxSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarStatusMaxSpeed.Location = new System.Drawing.Point(90, 48);
            this.progressBarStatusMaxSpeed.Name = "progressBarStatusMaxSpeed";
            this.progressBarStatusMaxSpeed.Size = new System.Drawing.Size(661, 23);
            this.progressBarStatusMaxSpeed.TabIndex = 3;
            // 
            // buttonStopMaxSpeed
            // 
            this.buttonStopMaxSpeed.Enabled = false;
            this.buttonStopMaxSpeed.Location = new System.Drawing.Point(6, 48);
            this.buttonStopMaxSpeed.Name = "buttonStopMaxSpeed";
            this.buttonStopMaxSpeed.Size = new System.Drawing.Size(75, 23);
            this.buttonStopMaxSpeed.TabIndex = 2;
            this.buttonStopMaxSpeed.Text = "Остановить";
            this.buttonStopMaxSpeed.UseVisualStyleBackColor = true;
            this.buttonStopMaxSpeed.Click += new System.EventHandler(this.buttonStopMaxSpeed_Click);
            // 
            // labelStatusMaxSpeed
            // 
            this.labelStatusMaxSpeed.AutoSize = true;
            this.labelStatusMaxSpeed.Location = new System.Drawing.Point(87, 24);
            this.labelStatusMaxSpeed.Name = "labelStatusMaxSpeed";
            this.labelStatusMaxSpeed.Size = new System.Drawing.Size(70, 13);
            this.labelStatusMaxSpeed.TabIndex = 1;
            this.labelStatusMaxSpeed.Text = "Состояние...";
            // 
            // buttonStartMaxSpeed
            // 
            this.buttonStartMaxSpeed.Location = new System.Drawing.Point(6, 19);
            this.buttonStartMaxSpeed.Name = "buttonStartMaxSpeed";
            this.buttonStartMaxSpeed.Size = new System.Drawing.Size(75, 23);
            this.buttonStartMaxSpeed.TabIndex = 0;
            this.buttonStartMaxSpeed.Text = "Начать";
            this.buttonStartMaxSpeed.UseVisualStyleBackColor = true;
            this.buttonStartMaxSpeed.Click += new System.EventHandler(this.buttonStartMaxSpeed_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonUpdateRP5);
            this.groupBox3.Controls.Add(this.checkBoxSkipErrors);
            this.groupBox3.Controls.Add(this.progressBarStatusRP5);
            this.groupBox3.Controls.Add(this.buttonStopRP5);
            this.groupBox3.Controls.Add(this.labelStatusRP5);
            this.groupBox3.Controls.Add(this.buttonStartRP5);
            this.groupBox3.Location = new System.Drawing.Point(12, 204);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(757, 96);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Загрузка БД Расписание погоды";
            // 
            // buttonUpdateRP5
            // 
            this.buttonUpdateRP5.Location = new System.Drawing.Point(90, 19);
            this.buttonUpdateRP5.Name = "buttonUpdateRP5";
            this.buttonUpdateRP5.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateRP5.TabIndex = 5;
            this.buttonUpdateRP5.Text = "Обновить";
            this.buttonUpdateRP5.UseVisualStyleBackColor = true;
            this.buttonUpdateRP5.Click += new System.EventHandler(this.buttonUpdateRP5_Click);
            // 
            // checkBoxSkipErrors
            // 
            this.checkBoxSkipErrors.AutoSize = true;
            this.checkBoxSkipErrors.Checked = true;
            this.checkBoxSkipErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSkipErrors.Location = new System.Drawing.Point(6, 73);
            this.checkBoxSkipErrors.Name = "checkBoxSkipErrors";
            this.checkBoxSkipErrors.Size = new System.Drawing.Size(110, 17);
            this.checkBoxSkipErrors.TabIndex = 4;
            this.checkBoxSkipErrors.Text = "Пропуск ошибок";
            this.checkBoxSkipErrors.UseVisualStyleBackColor = true;
            // 
            // progressBarStatusRP5
            // 
            this.progressBarStatusRP5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarStatusRP5.Location = new System.Drawing.Point(90, 48);
            this.progressBarStatusRP5.Name = "progressBarStatusRP5";
            this.progressBarStatusRP5.Size = new System.Drawing.Size(661, 23);
            this.progressBarStatusRP5.TabIndex = 3;
            // 
            // buttonStopRP5
            // 
            this.buttonStopRP5.Enabled = false;
            this.buttonStopRP5.Location = new System.Drawing.Point(6, 48);
            this.buttonStopRP5.Name = "buttonStopRP5";
            this.buttonStopRP5.Size = new System.Drawing.Size(75, 23);
            this.buttonStopRP5.TabIndex = 2;
            this.buttonStopRP5.Text = "Остановить";
            this.buttonStopRP5.UseVisualStyleBackColor = true;
            this.buttonStopRP5.Click += new System.EventHandler(this.buttonStopRP5_Click);
            // 
            // labelStatusRP5
            // 
            this.labelStatusRP5.AutoSize = true;
            this.labelStatusRP5.Location = new System.Drawing.Point(171, 24);
            this.labelStatusRP5.Name = "labelStatusRP5";
            this.labelStatusRP5.Size = new System.Drawing.Size(70, 13);
            this.labelStatusRP5.TabIndex = 1;
            this.labelStatusRP5.Text = "Состояние...";
            // 
            // buttonStartRP5
            // 
            this.buttonStartRP5.Location = new System.Drawing.Point(6, 19);
            this.buttonStartRP5.Name = "buttonStartRP5";
            this.buttonStartRP5.Size = new System.Drawing.Size(75, 23);
            this.buttonStartRP5.TabIndex = 0;
            this.buttonStartRP5.Text = "Начать";
            this.buttonStartRP5.UseVisualStyleBackColor = true;
            this.buttonStartRP5.Click += new System.EventHandler(this.buttonStartRP5_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.progressBarStatusNASA);
            this.groupBox4.Controls.Add(this.buttonStopNASA);
            this.groupBox4.Controls.Add(this.labelStatusNASA);
            this.groupBox4.Controls.Add(this.buttonStartNASA);
            this.groupBox4.Location = new System.Drawing.Point(12, 306);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(757, 96);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Загрузка БД NASA";
            // 
            // progressBarStatusNASA
            // 
            this.progressBarStatusNASA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarStatusNASA.Location = new System.Drawing.Point(90, 48);
            this.progressBarStatusNASA.Name = "progressBarStatusNASA";
            this.progressBarStatusNASA.Size = new System.Drawing.Size(661, 23);
            this.progressBarStatusNASA.TabIndex = 3;
            // 
            // buttonStopNASA
            // 
            this.buttonStopNASA.Enabled = false;
            this.buttonStopNASA.Location = new System.Drawing.Point(6, 48);
            this.buttonStopNASA.Name = "buttonStopNASA";
            this.buttonStopNASA.Size = new System.Drawing.Size(75, 23);
            this.buttonStopNASA.TabIndex = 2;
            this.buttonStopNASA.Text = "Остановить";
            this.buttonStopNASA.UseVisualStyleBackColor = true;
            this.buttonStopNASA.Click += new System.EventHandler(this.buttonStopNASA_Click);
            // 
            // labelStatusNASA
            // 
            this.labelStatusNASA.AutoSize = true;
            this.labelStatusNASA.Location = new System.Drawing.Point(87, 24);
            this.labelStatusNASA.Name = "labelStatusNASA";
            this.labelStatusNASA.Size = new System.Drawing.Size(70, 13);
            this.labelStatusNASA.TabIndex = 1;
            this.labelStatusNASA.Text = "Состояние...";
            // 
            // buttonStartNASA
            // 
            this.buttonStartNASA.Location = new System.Drawing.Point(6, 19);
            this.buttonStartNASA.Name = "buttonStartNASA";
            this.buttonStartNASA.Size = new System.Drawing.Size(75, 23);
            this.buttonStartNASA.TabIndex = 0;
            this.buttonStartNASA.Text = "Начать";
            this.buttonStartNASA.UseVisualStyleBackColor = true;
            this.buttonStartNASA.Click += new System.EventHandler(this.buttonStartNASA_Click);
            // 
            // FormLoadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 409);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(660, 152);
            this.Name = "FormLoadData";
            this.Text = "Загрузка данных";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar progressBarStatusMS;
        private System.Windows.Forms.Button buttonStopMS;
        private System.Windows.Forms.Label labelStatusMS;
        private System.Windows.Forms.Button buttonStartMS;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBarStatusMaxSpeed;
        private System.Windows.Forms.Button buttonStopMaxSpeed;
        private System.Windows.Forms.Label labelStatusMaxSpeed;
        private System.Windows.Forms.Button buttonStartMaxSpeed;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar progressBarStatusRP5;
        private System.Windows.Forms.Button buttonStopRP5;
        private System.Windows.Forms.Label labelStatusRP5;
        private System.Windows.Forms.Button buttonStartRP5;
        private System.Windows.Forms.CheckBox checkBoxSkipErrors;
        private System.Windows.Forms.Button buttonUpdateRP5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ProgressBar progressBarStatusNASA;
        private System.Windows.Forms.Button buttonStopNASA;
        private System.Windows.Forms.Label labelStatusNASA;
        private System.Windows.Forms.Button buttonStartNASA;
    }
}