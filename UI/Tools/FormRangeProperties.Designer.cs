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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRangeProperties));
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonSelectCoordinates = new System.Windows.Forms.Button();
            this.labelCoordinates = new System.Windows.Forms.Label();
            this.labelAddress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(72, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(164, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // buttonSelectCoordinates
            // 
            this.buttonSelectCoordinates.Location = new System.Drawing.Point(12, 47);
            this.buttonSelectCoordinates.Name = "buttonSelectCoordinates";
            this.buttonSelectCoordinates.Size = new System.Drawing.Size(95, 38);
            this.buttonSelectCoordinates.TabIndex = 1;
            this.buttonSelectCoordinates.Text = "Выбрать точку на карте";
            this.buttonSelectCoordinates.UseVisualStyleBackColor = true;
            this.buttonSelectCoordinates.Click += new System.EventHandler(this.buttonSelectCoordinates_Click);
            // 
            // labelCoordinates
            // 
            this.labelCoordinates.AutoSize = true;
            this.labelCoordinates.Location = new System.Drawing.Point(113, 47);
            this.labelCoordinates.Name = "labelCoordinates";
            this.labelCoordinates.Size = new System.Drawing.Size(99, 13);
            this.labelCoordinates.TabIndex = 2;
            this.labelCoordinates.Text = "Точка не выбрана";
            this.labelCoordinates.TextChanged += new System.EventHandler(this.labelCoordinates_TextChanged);
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(113, 72);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(99, 13);
            this.labelAddress.TabIndex = 3;
            this.labelAddress.Text = "Точка не выбрана";
            this.labelAddress.TextChanged += new System.EventHandler(this.labelCoordinates_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Название";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(3, 104);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(226, 104);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormRangeProperties
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(313, 141);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelAddress);
            this.Controls.Add(this.labelCoordinates);
            this.Controls.Add(this.buttonSelectCoordinates);
            this.Controls.Add(this.textBoxName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRangeProperties";
            this.Text = "Свойства ряда";
            this.Shown += new System.EventHandler(this.FormRangeProperties_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonSelectCoordinates;
        private System.Windows.Forms.Label labelCoordinates;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}