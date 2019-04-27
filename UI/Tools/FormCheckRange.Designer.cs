namespace WindEnergy.UI.Tools
{
    partial class FormCheckRange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCheckRange));
            this.buttonCheckRange = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labeldirectDiap = new System.Windows.Forms.Label();
            this.labelspeedDiap = new System.Windows.Forms.Label();
            this.buttonEnterDirectionDiapason = new System.Windows.Forms.Button();
            this.buttonEnterSpeedDiapason = new System.Windows.Forms.Button();
            this.labelPointAddress = new System.Windows.Forms.Label();
            this.labelPointCoordinates = new System.Windows.Forms.Label();
            this.buttonSelectPoint = new System.Windows.Forms.Button();
            this.radioButtonEnterLimits = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectLimitsProvider = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCheckRange
            // 
            this.buttonCheckRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCheckRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheckRange.Location = new System.Drawing.Point(83, 189);
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
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.labeldirectDiap);
            this.groupBox1.Controls.Add(this.labelspeedDiap);
            this.groupBox1.Controls.Add(this.buttonEnterDirectionDiapason);
            this.groupBox1.Controls.Add(this.buttonEnterSpeedDiapason);
            this.groupBox1.Controls.Add(this.labelPointAddress);
            this.groupBox1.Controls.Add(this.labelPointCoordinates);
            this.groupBox1.Controls.Add(this.buttonSelectPoint);
            this.groupBox1.Controls.Add(this.radioButtonEnterLimits);
            this.groupBox1.Controls.Add(this.radioButtonSelectLimitsProvider);
            this.groupBox1.Controls.Add(this.buttonCheckRange);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 248);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Проверка ряда";
            // 
            // labeldirectDiap
            // 
            this.labeldirectDiap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labeldirectDiap.AutoSize = true;
            this.labeldirectDiap.Location = new System.Drawing.Point(159, 158);
            this.labeldirectDiap.Name = "labeldirectDiap";
            this.labeldirectDiap.Size = new System.Drawing.Size(130, 13);
            this.labeldirectDiap.TabIndex = 12;
            this.labeldirectDiap.Text = "Диапазоны не выбраны";
            // 
            // labelspeedDiap
            // 
            this.labelspeedDiap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelspeedDiap.AutoSize = true;
            this.labelspeedDiap.Location = new System.Drawing.Point(6, 158);
            this.labelspeedDiap.Name = "labelspeedDiap";
            this.labelspeedDiap.Size = new System.Drawing.Size(130, 13);
            this.labelspeedDiap.TabIndex = 11;
            this.labelspeedDiap.Text = "Диапазоны не выбраны";
            // 
            // buttonEnterDirectionDiapason
            // 
            this.buttonEnterDirectionDiapason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEnterDirectionDiapason.Enabled = false;
            this.buttonEnterDirectionDiapason.Location = new System.Drawing.Point(162, 119);
            this.buttonEnterDirectionDiapason.Name = "buttonEnterDirectionDiapason";
            this.buttonEnterDirectionDiapason.Size = new System.Drawing.Size(137, 36);
            this.buttonEnterDirectionDiapason.TabIndex = 10;
            this.buttonEnterDirectionDiapason.Text = "Ввести ограничения направлений";
            this.buttonEnterDirectionDiapason.UseVisualStyleBackColor = true;
            this.buttonEnterDirectionDiapason.Click += new System.EventHandler(this.buttonEnterDirectionDiapason_Click);
            // 
            // buttonEnterSpeedDiapason
            // 
            this.buttonEnterSpeedDiapason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEnterSpeedDiapason.Enabled = false;
            this.buttonEnterSpeedDiapason.Location = new System.Drawing.Point(9, 119);
            this.buttonEnterSpeedDiapason.Name = "buttonEnterSpeedDiapason";
            this.buttonEnterSpeedDiapason.Size = new System.Drawing.Size(137, 36);
            this.buttonEnterSpeedDiapason.TabIndex = 9;
            this.buttonEnterSpeedDiapason.Text = "Ввести ограничения скоростей";
            this.buttonEnterSpeedDiapason.UseVisualStyleBackColor = true;
            this.buttonEnterSpeedDiapason.Click += new System.EventHandler(this.buttonEnterSpeedDiapason_Click);
            // 
            // labelPointAddress
            // 
            this.labelPointAddress.AutoSize = true;
            this.labelPointAddress.Location = new System.Drawing.Point(111, 63);
            this.labelPointAddress.Name = "labelPointAddress";
            this.labelPointAddress.Size = new System.Drawing.Size(99, 13);
            this.labelPointAddress.TabIndex = 8;
            this.labelPointAddress.Text = "Точка не выбрана";
            this.labelPointAddress.TextChanged += new System.EventHandler(this.labelPointCoordinates_TextChanged);
            // 
            // labelPointCoordinates
            // 
            this.labelPointCoordinates.AutoSize = true;
            this.labelPointCoordinates.Location = new System.Drawing.Point(111, 43);
            this.labelPointCoordinates.Name = "labelPointCoordinates";
            this.labelPointCoordinates.Size = new System.Drawing.Size(99, 13);
            this.labelPointCoordinates.TabIndex = 8;
            this.labelPointCoordinates.Text = "Точка не выбрана";
            this.labelPointCoordinates.TextChanged += new System.EventHandler(this.labelPointCoordinates_TextChanged);
            // 
            // buttonSelectPoint
            // 
            this.buttonSelectPoint.Location = new System.Drawing.Point(9, 43);
            this.buttonSelectPoint.Name = "buttonSelectPoint";
            this.buttonSelectPoint.Size = new System.Drawing.Size(96, 33);
            this.buttonSelectPoint.TabIndex = 7;
            this.buttonSelectPoint.Text = "Выбрать точку";
            this.buttonSelectPoint.UseVisualStyleBackColor = true;
            this.buttonSelectPoint.Click += new System.EventHandler(this.buttonSelectPoint_Click);
            // 
            // radioButtonEnterLimits
            // 
            this.radioButtonEnterLimits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonEnterLimits.AutoSize = true;
            this.radioButtonEnterLimits.Location = new System.Drawing.Point(9, 96);
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
            this.radioButtonSelectLimitsProvider.Size = new System.Drawing.Size(209, 17);
            this.radioButtonSelectLimitsProvider.TabIndex = 5;
            this.radioButtonSelectLimitsProvider.TabStop = true;
            this.radioButtonSelectLimitsProvider.Text = "Автоматический поиск ограничений";
            this.radioButtonSelectLimitsProvider.UseVisualStyleBackColor = true;
            this.radioButtonSelectLimitsProvider.CheckedChanged += new System.EventHandler(this.radioButtonLimits_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(9, 218);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(290, 23);
            this.progressBar1.TabIndex = 13;
            // 
            // FormCheckRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 248);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(322, 287);
            this.Name = "FormCheckRange";
            this.Text = "Проверить  ряд";
            this.Shown += new System.EventHandler(this.formCheckRepairRange_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCheckRange;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelPointCoordinates;
        private System.Windows.Forms.Button buttonSelectPoint;
        private System.Windows.Forms.RadioButton radioButtonEnterLimits;
        private System.Windows.Forms.RadioButton radioButtonSelectLimitsProvider;
        private System.Windows.Forms.Label labeldirectDiap;
        private System.Windows.Forms.Label labelspeedDiap;
        private System.Windows.Forms.Button buttonEnterDirectionDiapason;
        private System.Windows.Forms.Button buttonEnterSpeedDiapason;
        private System.Windows.Forms.Label labelPointAddress;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}