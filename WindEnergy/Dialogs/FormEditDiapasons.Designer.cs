namespace WindEnergy.UI.Dialogs
{
    partial class FormEditDiapasons
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditDiapasons));
            this.listBoxDiapasons = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.numericUpDownFrom = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTo = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTo)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxDiapasons
            // 
            this.listBoxDiapasons.FormattingEnabled = true;
            this.listBoxDiapasons.Location = new System.Drawing.Point(12, 12);
            this.listBoxDiapasons.Name = "listBoxDiapasons";
            this.listBoxDiapasons.Size = new System.Drawing.Size(170, 264);
            this.listBoxDiapasons.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(188, 93);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(130, 23);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Добавить диапазон";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(188, 253);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(130, 23);
            this.buttonRemove.TabIndex = 4;
            this.buttonRemove.Text = "Удалить диапазон";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(12, 282);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(243, 282);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // numericUpDownFrom
            // 
            this.numericUpDownFrom.DecimalPlaces = 1;
            this.numericUpDownFrom.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownFrom.Location = new System.Drawing.Point(188, 28);
            this.numericUpDownFrom.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDownFrom.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.numericUpDownFrom.Name = "numericUpDownFrom";
            this.numericUpDownFrom.Size = new System.Drawing.Size(130, 20);
            this.numericUpDownFrom.TabIndex = 7;
            // 
            // numericUpDownTo
            // 
            this.numericUpDownTo.DecimalPlaces = 1;
            this.numericUpDownTo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTo.Location = new System.Drawing.Point(188, 67);
            this.numericUpDownTo.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDownTo.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.numericUpDownTo.Name = "numericUpDownTo";
            this.numericUpDownTo.Size = new System.Drawing.Size(130, 20);
            this.numericUpDownTo.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Начало диапазона";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Конец диапазона";
            // 
            // FormEditDiapasons
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(340, 313);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownTo);
            this.Controls.Add(this.numericUpDownFrom);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.listBoxDiapasons);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEditDiapasons";
            this.Text = "Редактирование диапазонов";
            this.Shown += new System.EventHandler(this.FormEditDiapasons_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxDiapasons;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.NumericUpDown numericUpDownFrom;
        private System.Windows.Forms.NumericUpDown numericUpDownTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}