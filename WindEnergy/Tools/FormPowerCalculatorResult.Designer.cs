
namespace WindEnergy.UI.Tools
{
    partial class FormPowerCalculatorResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPowerCalculatorResult));
            this.comboBoxTowerHeights = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxTowerHeights
            // 
            this.comboBoxTowerHeights.FormattingEnabled = true;
            this.comboBoxTowerHeights.Location = new System.Drawing.Point(15, 29);
            this.comboBoxTowerHeights.Name = "comboBoxTowerHeights";
            this.comboBoxTowerHeights.Size = new System.Drawing.Size(168, 24);
            this.comboBoxTowerHeights.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберите высоту башни";
            // 
            // FormPowerCalculatorResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 542);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxTowerHeights);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPowerCalculatorResult";
            this.Text = "Расчет ВЭУ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTowerHeights;
        private System.Windows.Forms.Label label1;
    }
}