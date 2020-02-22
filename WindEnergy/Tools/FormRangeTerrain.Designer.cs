namespace WindEnergy.UI.Tools
{
    partial class FormRangeTerrain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRangeTerrain));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxTerrainSecond = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonTerrainSecond = new System.Windows.Forms.RadioButton();
            this.radioButtonTerrainFirst = new System.Windows.Forms.RadioButton();
            this.groupBoxTerrainFirst = new System.Windows.Forms.GroupBox();
            this.buttonSelectMSCoordinates = new System.Windows.Forms.Button();
            this.buttonSelectPointCoordinates = new System.Windows.Forms.Button();
            this.labelPointClassesStatus = new System.Windows.Forms.Label();
            this.buttonEnterPointClasses = new System.Windows.Forms.Button();
            this.labelMSClassesStatus = new System.Windows.Forms.Label();
            this.buttonEnterMSClasses = new System.Windows.Forms.Button();
            this.labelPointCoordinatesStatus = new System.Windows.Forms.Label();
            this.labelMSCoordinatesStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxTerrainFirst.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTerrainSecond
            // 
            this.groupBoxTerrainSecond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTerrainSecond.Location = new System.Drawing.Point(389, 53);
            this.groupBoxTerrainSecond.Name = "groupBoxTerrainSecond";
            this.groupBoxTerrainSecond.Size = new System.Drawing.Size(380, 152);
            this.groupBoxTerrainSecond.TabIndex = 8;
            this.groupBoxTerrainSecond.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.buttonCalculate, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxTerrainFirst, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxTerrainSecond, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(772, 253);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // buttonCalculate
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonCalculate, 2);
            this.buttonCalculate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCalculate.Location = new System.Drawing.Point(3, 211);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(766, 39);
            this.buttonCalculate.TabIndex = 9;
            this.buttonCalculate.Text = "Пересчитать ряд";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.radioButtonTerrainSecond);
            this.groupBox1.Controls.Add(this.radioButtonTerrainFirst);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 44);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип рельефа";
            // 
            // radioButtonTerrainSecond
            // 
            this.radioButtonTerrainSecond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonTerrainSecond.AutoSize = true;
            this.radioButtonTerrainSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonTerrainSecond.Location = new System.Drawing.Point(639, 19);
            this.radioButtonTerrainSecond.Name = "radioButtonTerrainSecond";
            this.radioButtonTerrainSecond.Size = new System.Drawing.Size(121, 21);
            this.radioButtonTerrainSecond.TabIndex = 1;
            this.radioButtonTerrainSecond.Text = "II тип рельефа";
            this.radioButtonTerrainSecond.UseVisualStyleBackColor = true;
            this.radioButtonTerrainSecond.CheckedChanged += new System.EventHandler(this.radioButtonTerrain_CheckedChanged);
            // 
            // radioButtonTerrainFirst
            // 
            this.radioButtonTerrainFirst.AutoSize = true;
            this.radioButtonTerrainFirst.Checked = true;
            this.radioButtonTerrainFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonTerrainFirst.Location = new System.Drawing.Point(6, 19);
            this.radioButtonTerrainFirst.Name = "radioButtonTerrainFirst";
            this.radioButtonTerrainFirst.Size = new System.Drawing.Size(118, 21);
            this.radioButtonTerrainFirst.TabIndex = 0;
            this.radioButtonTerrainFirst.TabStop = true;
            this.radioButtonTerrainFirst.Text = "Ⅰ тип рельефа";
            this.radioButtonTerrainFirst.UseVisualStyleBackColor = true;
            this.radioButtonTerrainFirst.CheckedChanged += new System.EventHandler(this.radioButtonTerrain_CheckedChanged);
            // 
            // groupBoxTerrainFirst
            // 
            this.groupBoxTerrainFirst.Controls.Add(this.buttonSelectMSCoordinates);
            this.groupBoxTerrainFirst.Controls.Add(this.buttonSelectPointCoordinates);
            this.groupBoxTerrainFirst.Controls.Add(this.labelPointClassesStatus);
            this.groupBoxTerrainFirst.Controls.Add(this.buttonEnterPointClasses);
            this.groupBoxTerrainFirst.Controls.Add(this.labelMSClassesStatus);
            this.groupBoxTerrainFirst.Controls.Add(this.buttonEnterMSClasses);
            this.groupBoxTerrainFirst.Controls.Add(this.labelPointCoordinatesStatus);
            this.groupBoxTerrainFirst.Controls.Add(this.labelMSCoordinatesStatus);
            this.groupBoxTerrainFirst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTerrainFirst.Location = new System.Drawing.Point(3, 53);
            this.groupBoxTerrainFirst.Name = "groupBoxTerrainFirst";
            this.groupBoxTerrainFirst.Size = new System.Drawing.Size(380, 152);
            this.groupBoxTerrainFirst.TabIndex = 7;
            this.groupBoxTerrainFirst.TabStop = false;
            // 
            // buttonSelectMSCoordinates
            // 
            this.buttonSelectMSCoordinates.Location = new System.Drawing.Point(6, 19);
            this.buttonSelectMSCoordinates.Name = "buttonSelectMSCoordinates";
            this.buttonSelectMSCoordinates.Size = new System.Drawing.Size(218, 23);
            this.buttonSelectMSCoordinates.TabIndex = 4;
            this.buttonSelectMSCoordinates.Text = "Выбрать точку МС";
            this.buttonSelectMSCoordinates.UseVisualStyleBackColor = true;
            this.buttonSelectMSCoordinates.Click += new System.EventHandler(this.buttonSelectMSCoordinates_Click);
            // 
            // buttonSelectPointCoordinates
            // 
            this.buttonSelectPointCoordinates.Location = new System.Drawing.Point(6, 48);
            this.buttonSelectPointCoordinates.Name = "buttonSelectPointCoordinates";
            this.buttonSelectPointCoordinates.Size = new System.Drawing.Size(218, 23);
            this.buttonSelectPointCoordinates.TabIndex = 0;
            this.buttonSelectPointCoordinates.Text = "Выбрать точку ВЭС";
            this.buttonSelectPointCoordinates.UseVisualStyleBackColor = true;
            this.buttonSelectPointCoordinates.Click += new System.EventHandler(this.buttonSelectPointCoordinates_Click);
            // 
            // labelPointClassesStatus
            // 
            this.labelPointClassesStatus.AutoSize = true;
            this.labelPointClassesStatus.Location = new System.Drawing.Point(230, 111);
            this.labelPointClassesStatus.Name = "labelPointClassesStatus";
            this.labelPointClassesStatus.Size = new System.Drawing.Size(22, 13);
            this.labelPointClassesStatus.TabIndex = 5;
            this.labelPointClassesStatus.Text = "ОК";
            // 
            // buttonEnterPointClasses
            // 
            this.buttonEnterPointClasses.Location = new System.Drawing.Point(6, 106);
            this.buttonEnterPointClasses.Name = "buttonEnterPointClasses";
            this.buttonEnterPointClasses.Size = new System.Drawing.Size(218, 23);
            this.buttonEnterPointClasses.TabIndex = 1;
            this.buttonEnterPointClasses.Text = "Ввести классы открытости точки ВЭС";
            this.buttonEnterPointClasses.UseVisualStyleBackColor = true;
            this.buttonEnterPointClasses.Click += new System.EventHandler(this.buttonEnterPointClasses_Click);
            // 
            // labelMSClassesStatus
            // 
            this.labelMSClassesStatus.AutoSize = true;
            this.labelMSClassesStatus.Location = new System.Drawing.Point(230, 82);
            this.labelMSClassesStatus.Name = "labelMSClassesStatus";
            this.labelMSClassesStatus.Size = new System.Drawing.Size(22, 13);
            this.labelMSClassesStatus.TabIndex = 5;
            this.labelMSClassesStatus.Text = "ОК";
            // 
            // buttonEnterMSClasses
            // 
            this.buttonEnterMSClasses.Location = new System.Drawing.Point(6, 77);
            this.buttonEnterMSClasses.Name = "buttonEnterMSClasses";
            this.buttonEnterMSClasses.Size = new System.Drawing.Size(218, 23);
            this.buttonEnterMSClasses.TabIndex = 2;
            this.buttonEnterMSClasses.Text = "Ввести классы открытости МС";
            this.buttonEnterMSClasses.UseVisualStyleBackColor = true;
            this.buttonEnterMSClasses.Click += new System.EventHandler(this.buttonEnterMSClasses_Click);
            // 
            // labelPointCoordinatesStatus
            // 
            this.labelPointCoordinatesStatus.AutoSize = true;
            this.labelPointCoordinatesStatus.Location = new System.Drawing.Point(230, 53);
            this.labelPointCoordinatesStatus.Name = "labelPointCoordinatesStatus";
            this.labelPointCoordinatesStatus.Size = new System.Drawing.Size(22, 13);
            this.labelPointCoordinatesStatus.TabIndex = 5;
            this.labelPointCoordinatesStatus.Text = "ОК";
            // 
            // labelMSCoordinatesStatus
            // 
            this.labelMSCoordinatesStatus.AutoSize = true;
            this.labelMSCoordinatesStatus.Location = new System.Drawing.Point(230, 24);
            this.labelMSCoordinatesStatus.Name = "labelMSCoordinatesStatus";
            this.labelMSCoordinatesStatus.Size = new System.Drawing.Size(22, 13);
            this.labelMSCoordinatesStatus.TabIndex = 5;
            this.labelMSCoordinatesStatus.Text = "ОК";
            // 
            // FormRangeTerrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 253);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(570, 292);
            this.Name = "FormRangeTerrain";
            this.Text = "Пересчет скорости ветра в точку ВЭС";
            this.Load += new System.EventHandler(this.FormRangeTerrain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxTerrainFirst.ResumeLayout(false);
            this.groupBoxTerrainFirst.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBoxTerrainSecond;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonTerrainSecond;
        private System.Windows.Forms.RadioButton radioButtonTerrainFirst;
        private System.Windows.Forms.GroupBox groupBoxTerrainFirst;
        private System.Windows.Forms.Button buttonSelectMSCoordinates;
        private System.Windows.Forms.Button buttonSelectPointCoordinates;
        private System.Windows.Forms.Label labelPointClassesStatus;
        private System.Windows.Forms.Button buttonEnterPointClasses;
        private System.Windows.Forms.Label labelMSClassesStatus;
        private System.Windows.Forms.Button buttonEnterMSClasses;
        private System.Windows.Forms.Label labelPointCoordinatesStatus;
        private System.Windows.Forms.Label labelMSCoordinatesStatus;
    }
}