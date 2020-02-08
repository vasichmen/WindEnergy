namespace WindEnergy.UI.Tools
{
    partial class FormRangeElevator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRangeElevator));
            this.textBoxFromHeight = new System.Windows.Forms.TextBox();
            this.textBoxToHeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonElevate = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // textBoxFromHeight
            // 
            this.textBoxFromHeight.Enabled = false;
            this.textBoxFromHeight.Location = new System.Drawing.Point(12, 12);
            this.textBoxFromHeight.Name = "textBoxFromHeight";
            this.textBoxFromHeight.Size = new System.Drawing.Size(100, 20);
            this.textBoxFromHeight.TabIndex = 0;
            this.textBoxFromHeight.Text = "10";
            // 
            // textBoxToHeight
            // 
            this.textBoxToHeight.Location = new System.Drawing.Point(12, 38);
            this.textBoxToHeight.Name = "textBoxToHeight";
            this.textBoxToHeight.Size = new System.Drawing.Size(100, 20);
            this.textBoxToHeight.TabIndex = 1;
            this.textBoxToHeight.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Высота наблюдений, м";
            this.toolTip1.SetToolTip(this.label1, "Высота наблюдений выбранного ряда от уровня земли в метрах");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Высота башни ВЭУ, м";
            this.toolTip1.SetToolTip(this.label2, "Высота, на которую надо поднять ряд наблюдений от уровня земли в метрах");
            // 
            // buttonElevate
            // 
            this.buttonElevate.Location = new System.Drawing.Point(55, 100);
            this.buttonElevate.Name = "buttonElevate";
            this.buttonElevate.Size = new System.Drawing.Size(127, 23);
            this.buttonElevate.TabIndex = 4;
            this.buttonElevate.Text = "Пересчитать ряд";
            this.buttonElevate.UseVisualStyleBackColor = true;
            this.buttonElevate.Click += new System.EventHandler(this.buttonElevate_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 129);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(315, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // FormRangeElevator
            // 
            this.AcceptButton = this.buttonElevate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 189);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonElevate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxToHeight);
            this.Controls.Add(this.textBoxFromHeight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRangeElevator";
            this.Text = "Поднятие наблюдений на высоту башни";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFromHeight;
        private System.Windows.Forms.TextBox textBoxToHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonElevate;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}