namespace WindEnergy.UI.Tools
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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox1.Size = new System.Drawing.Size(620, 90);
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
            this.progressBarStatusMS.Size = new System.Drawing.Size(524, 23);
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
            this.groupBox2.Size = new System.Drawing.Size(620, 90);
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
            this.progressBarStatusMaxSpeed.Size = new System.Drawing.Size(524, 23);
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
            // FormLoadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 267);
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
    }
}