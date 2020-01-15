namespace WindEnergy.UI.Tools
{
    partial class FormEqualizer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEqualizer));
            this.buttonOpenFiles = new System.Windows.Forms.Button();
            this.labelFiles = new System.Windows.Forms.Label();
            this.buttonSelectFolder = new System.Windows.Forms.Button();
            this.labelFolder = new System.Windows.Forms.Label();
            this.checkBoxSeparateDate = new System.Windows.Forms.CheckBox();
            this.numericUpDownStartLine = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartLine)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenFiles
            // 
            this.buttonOpenFiles.Location = new System.Drawing.Point(12, 63);
            this.buttonOpenFiles.Name = "buttonOpenFiles";
            this.buttonOpenFiles.Size = new System.Drawing.Size(116, 23);
            this.buttonOpenFiles.TabIndex = 0;
            this.buttonOpenFiles.Text = "Выбрать файлы";
            this.buttonOpenFiles.UseVisualStyleBackColor = true;
            this.buttonOpenFiles.Click += new System.EventHandler(this.buttonOpenFiles_Click);
            // 
            // labelFiles
            // 
            this.labelFiles.AutoSize = true;
            this.labelFiles.Location = new System.Drawing.Point(12, 89);
            this.labelFiles.Name = "labelFiles";
            this.labelFiles.Size = new System.Drawing.Size(35, 13);
            this.labelFiles.TabIndex = 1;
            this.labelFiles.Text = "label1";
            // 
            // buttonSelectFolder
            // 
            this.buttonSelectFolder.Location = new System.Drawing.Point(134, 63);
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.Size = new System.Drawing.Size(116, 23);
            this.buttonSelectFolder.TabIndex = 2;
            this.buttonSelectFolder.Text = "Выбрать папку";
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // labelFolder
            // 
            this.labelFolder.AutoSize = true;
            this.labelFolder.Location = new System.Drawing.Point(135, 89);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(35, 13);
            this.labelFolder.TabIndex = 3;
            this.labelFolder.Text = "label1";
            // 
            // checkBoxSeparateDate
            // 
            this.checkBoxSeparateDate.AutoSize = true;
            this.checkBoxSeparateDate.Location = new System.Drawing.Point(12, 40);
            this.checkBoxSeparateDate.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.checkBoxSeparateDate.Name = "checkBoxSeparateDate";
            this.checkBoxSeparateDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBoxSeparateDate.Size = new System.Drawing.Size(194, 17);
            this.checkBoxSeparateDate.TabIndex = 4;
            this.checkBoxSeparateDate.Text = "Дата и время в разных столбцах";
            this.toolTip1.SetToolTip(this.checkBoxSeparateDate, "Если выбрано, то дата и время в первом и втором столбце соответственно");
            this.checkBoxSeparateDate.UseVisualStyleBackColor = true;
            // 
            // numericUpDownStartLine
            // 
            this.numericUpDownStartLine.Location = new System.Drawing.Point(114, 14);
            this.numericUpDownStartLine.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownStartLine.Name = "numericUpDownStartLine";
            this.numericUpDownStartLine.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownStartLine.TabIndex = 5;
            this.toolTip1.SetToolTip(this.numericUpDownStartLine, "Количество пропускаемый строк перед импортом");
            this.numericUpDownStartLine.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Строк в заголовке";
            this.toolTip1.SetToolTip(this.label1, "Количество пропускаемый строк перед импортом");
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 105);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(238, 23);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "Преобразовать файлы";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonStart);
            this.groupBox1.Controls.Add(this.buttonOpenFiles);
            this.groupBox1.Controls.Add(this.labelFiles);
            this.groupBox1.Controls.Add(this.numericUpDownStartLine);
            this.groupBox1.Controls.Add(this.buttonSelectFolder);
            this.groupBox1.Controls.Add(this.checkBoxSeparateDate);
            this.groupBox1.Controls.Add(this.labelFolder);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 141);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки преобразования";
            // 
            // FormEqualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 162);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEqualizer";
            this.Text = "Приведение рядов к однородным";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartLine)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenFiles;
        private System.Windows.Forms.Label labelFiles;
        private System.Windows.Forms.Button buttonSelectFolder;
        private System.Windows.Forms.Label labelFolder;
        private System.Windows.Forms.CheckBox checkBoxSeparateDate;
        private System.Windows.Forms.NumericUpDown numericUpDownStartLine;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}