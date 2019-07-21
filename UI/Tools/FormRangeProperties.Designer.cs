namespace WindEnergy.UI.Tools
{
    partial class FormRangeProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRangeProperties));
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelCoordinates = new System.Windows.Forms.Label();
            this.labelAddress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelMSName = new System.Windows.Forms.Label();
            this.textBoxMSName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonIntervals = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxMSAddress = new System.Windows.Forms.TextBox();
            this.textBoxMSCoordinates = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRangeCount = new System.Windows.Forms.TextBox();
            this.textBoxMSID = new System.Windows.Forms.TextBox();
            this.labelMSID = new System.Windows.Forms.Label();
            this.labelMSType = new System.Windows.Forms.Label();
            this.textBoxMSType = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(180, 5);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(235, 20);
            this.textBoxName.TabIndex = 0;
            this.textBoxName.Text = "to";
            // 
            // labelCoordinates
            // 
            this.labelCoordinates.AutoSize = true;
            this.labelCoordinates.Location = new System.Drawing.Point(5, 107);
            this.labelCoordinates.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.labelCoordinates.Name = "labelCoordinates";
            this.labelCoordinates.Size = new System.Drawing.Size(144, 13);
            this.labelCoordinates.TabIndex = 2;
            this.labelCoordinates.Text = "Координаты метеостанции";
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(5, 132);
            this.labelAddress.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(113, 13);
            this.labelAddress.TabIndex = 3;
            this.labelAddress.Text = "Адрес метеостанции";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Название ряда";
            // 
            // labelMSName
            // 
            this.labelMSName.AutoSize = true;
            this.labelMSName.Location = new System.Drawing.Point(5, 82);
            this.labelMSName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.labelMSName.Name = "labelMSName";
            this.labelMSName.Size = new System.Drawing.Size(132, 13);
            this.labelMSName.TabIndex = 7;
            this.labelMSName.Text = "Название метеостанции";
            // 
            // textBoxMSName
            // 
            this.textBoxMSName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMSName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMSName.Location = new System.Drawing.Point(180, 82);
            this.textBoxMSName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.textBoxMSName.Name = "textBoxMSName";
            this.textBoxMSName.ReadOnly = true;
            this.textBoxMSName.Size = new System.Drawing.Size(235, 13);
            this.textBoxMSName.TabIndex = 8;
            this.textBoxMSName.TabStop = false;
            this.textBoxMSName.Text = "Информация недоступна";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.textBoxMSAddress, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxMSCoordinates, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelAddress, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelMSName, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelCoordinates, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxMSName, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelMSType, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxMSType, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxRangeCount, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxMSID, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelMSID, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(420, 208);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.buttonIntervals, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonCancel, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonSave, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 176);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(416, 30);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // buttonIntervals
            // 
            this.buttonIntervals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonIntervals.Location = new System.Drawing.Point(141, 3);
            this.buttonIntervals.Name = "buttonIntervals";
            this.buttonIntervals.Size = new System.Drawing.Size(132, 24);
            this.buttonIntervals.TabIndex = 12;
            this.buttonIntervals.Text = "Интервалы измерений";
            this.buttonIntervals.UseVisualStyleBackColor = true;
            this.buttonIntervals.Click += new System.EventHandler(this.ButtonIntervals_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(279, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(134, 24);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(3, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(132, 24);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // textBoxMSAddress
            // 
            this.textBoxMSAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMSAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMSAddress.Location = new System.Drawing.Point(180, 132);
            this.textBoxMSAddress.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.textBoxMSAddress.Name = "textBoxMSAddress";
            this.textBoxMSAddress.ReadOnly = true;
            this.textBoxMSAddress.Size = new System.Drawing.Size(235, 13);
            this.textBoxMSAddress.TabIndex = 10;
            this.textBoxMSAddress.TabStop = false;
            this.textBoxMSAddress.Text = "Информация недоступна";
            // 
            // textBoxMSCoordinates
            // 
            this.textBoxMSCoordinates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMSCoordinates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMSCoordinates.Location = new System.Drawing.Point(180, 107);
            this.textBoxMSCoordinates.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.textBoxMSCoordinates.Name = "textBoxMSCoordinates";
            this.textBoxMSCoordinates.ReadOnly = true;
            this.textBoxMSCoordinates.Size = new System.Drawing.Size(235, 13);
            this.textBoxMSCoordinates.TabIndex = 9;
            this.textBoxMSCoordinates.TabStop = false;
            this.textBoxMSCoordinates.Text = "Информация недоступна";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Количество наблюдений в ряде";
            // 
            // textBoxRangeCount
            // 
            this.textBoxRangeCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRangeCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxRangeCount.Location = new System.Drawing.Point(180, 32);
            this.textBoxRangeCount.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.textBoxRangeCount.Name = "textBoxRangeCount";
            this.textBoxRangeCount.ReadOnly = true;
            this.textBoxRangeCount.Size = new System.Drawing.Size(235, 13);
            this.textBoxRangeCount.TabIndex = 10;
            this.textBoxRangeCount.TabStop = false;
            this.textBoxRangeCount.Text = "Название МС";
            // 
            // textBoxMSID
            // 
            this.textBoxMSID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMSID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMSID.Location = new System.Drawing.Point(180, 157);
            this.textBoxMSID.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.textBoxMSID.Name = "textBoxMSID";
            this.textBoxMSID.ReadOnly = true;
            this.textBoxMSID.Size = new System.Drawing.Size(235, 13);
            this.textBoxMSID.TabIndex = 10;
            this.textBoxMSID.TabStop = false;
            this.textBoxMSID.Text = "Информация недоступна";
            // 
            // labelMSID
            // 
            this.labelMSID.AutoSize = true;
            this.labelMSID.Location = new System.Drawing.Point(5, 157);
            this.labelMSID.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.labelMSID.Name = "labelMSID";
            this.labelMSID.Size = new System.Drawing.Size(93, 13);
            this.labelMSID.TabIndex = 3;
            this.labelMSID.Text = "ID метеостанции";
            // 
            // labelMSType
            // 
            this.labelMSType.AutoSize = true;
            this.labelMSType.Location = new System.Drawing.Point(5, 57);
            this.labelMSType.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.labelMSType.Name = "labelMSType";
            this.labelMSType.Size = new System.Drawing.Size(101, 13);
            this.labelMSType.TabIndex = 7;
            this.labelMSType.Text = "Тип метеостанции";
            // 
            // textBoxMSType
            // 
            this.textBoxMSType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMSType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMSType.Location = new System.Drawing.Point(180, 57);
            this.textBoxMSType.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.textBoxMSType.Name = "textBoxMSType";
            this.textBoxMSType.ReadOnly = true;
            this.textBoxMSType.Size = new System.Drawing.Size(235, 13);
            this.textBoxMSType.TabIndex = 8;
            this.textBoxMSType.TabStop = false;
            this.textBoxMSType.Text = "Информация недоступна";
            // 
            // FormRangeProperties
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(420, 208);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(436, 247);
            this.Name = "FormRangeProperties";
            this.Text = "Свойства ряда";
            this.Shown += new System.EventHandler(this.FormRangeProperties_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelCoordinates;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelMSName;
        private System.Windows.Forms.TextBox textBoxMSName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxMSAddress;
        private System.Windows.Forms.TextBox textBoxMSCoordinates;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRangeCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonIntervals;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxMSID;
        private System.Windows.Forms.Label labelMSID;
        private System.Windows.Forms.Label labelMSType;
        private System.Windows.Forms.TextBox textBoxMSType;
    }
}