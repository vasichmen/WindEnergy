namespace WindEnergy.UI.Tools
{
    partial class FormRangeStatistic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRangeStatistic));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewExt1 = new WindEnergy.UI.Ext.DataGridViewExt();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCompletness = new System.Windows.Forms.Label();
            this.labelMeasureAmount = new System.Windows.Forms.Label();
            this.labelExpectAmount = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.labelMaxEmpty = new System.Windows.Forms.Label();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExt1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewExt1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSaveFile, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(535, 406);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridViewExt1
            // 
            this.dataGridViewExt1.AllowUserToAddRows = false;
            this.dataGridViewExt1.AllowUserToDeleteRows = false;
            this.dataGridViewExt1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridViewExt1, 2);
            this.dataGridViewExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewExt1.Location = new System.Drawing.Point(2, 164);
            this.dataGridViewExt1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridViewExt1.Name = "dataGridViewExt1";
            this.dataGridViewExt1.ReadOnly = true;
            this.dataGridViewExt1.RowTemplate.Height = 55;
            this.dataGridViewExt1.Size = new System.Drawing.Size(531, 240);
            this.dataGridViewExt1.TabIndex = 0;
            this.dataGridViewExt1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridViewExt1_ColumnAdded);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.labelCompletness);
            this.flowLayoutPanel1.Controls.Add(this.labelMeasureAmount);
            this.flowLayoutPanel1.Controls.Add(this.labelExpectAmount);
            this.flowLayoutPanel1.Controls.Add(this.labelLength);
            this.flowLayoutPanel1.Controls.Add(this.labelMaxEmpty);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(391, 158);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Статистика наблюдений для ряда: ";
            // 
            // labelCompletness
            // 
            this.labelCompletness.AutoSize = true;
            this.labelCompletness.Location = new System.Drawing.Point(2, 17);
            this.labelCompletness.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCompletness.Name = "labelCompletness";
            this.labelCompletness.Size = new System.Drawing.Size(35, 13);
            this.labelCompletness.TabIndex = 1;
            this.labelCompletness.Text = "label2";
            // 
            // labelMeasureAmount
            // 
            this.labelMeasureAmount.AutoSize = true;
            this.labelMeasureAmount.Location = new System.Drawing.Point(2, 30);
            this.labelMeasureAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMeasureAmount.Name = "labelMeasureAmount";
            this.labelMeasureAmount.Size = new System.Drawing.Size(35, 13);
            this.labelMeasureAmount.TabIndex = 4;
            this.labelMeasureAmount.Text = "label2";
            // 
            // labelExpectAmount
            // 
            this.labelExpectAmount.AutoSize = true;
            this.labelExpectAmount.Location = new System.Drawing.Point(2, 43);
            this.labelExpectAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelExpectAmount.Name = "labelExpectAmount";
            this.labelExpectAmount.Size = new System.Drawing.Size(35, 13);
            this.labelExpectAmount.TabIndex = 5;
            this.labelExpectAmount.Text = "label2";
            // 
            // labelLength
            // 
            this.labelLength.AutoSize = true;
            this.labelLength.Location = new System.Drawing.Point(2, 56);
            this.labelLength.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLength.Name = "labelLength";
            this.labelLength.Size = new System.Drawing.Size(35, 13);
            this.labelLength.TabIndex = 2;
            this.labelLength.Text = "label2";
            // 
            // labelMaxEmpty
            // 
            this.labelMaxEmpty.AutoSize = true;
            this.labelMaxEmpty.Location = new System.Drawing.Point(2, 69);
            this.labelMaxEmpty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMaxEmpty.Name = "labelMaxEmpty";
            this.labelMaxEmpty.Size = new System.Drawing.Size(35, 13);
            this.labelMaxEmpty.TabIndex = 3;
            this.labelMaxEmpty.Text = "label2";
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Location = new System.Drawing.Point(398, 3);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(134, 42);
            this.buttonSaveFile.TabIndex = 2;
            this.buttonSaveFile.Text = "Сохранить результаты в файл";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // FormRangeStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 406);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(551, 445);
            this.Name = "FormRangeStatistic";
            this.Text = "FormRangeStatistic";
            this.Shown += new System.EventHandler(this.FormRangeStatistic_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExt1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Ext.DataGridViewExt dataGridViewExt1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCompletness;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.Label labelMaxEmpty;
        private System.Windows.Forms.Label labelMeasureAmount;
        private System.Windows.Forms.Label labelExpectAmount;
        private System.Windows.Forms.Button buttonSaveFile;
    }
}