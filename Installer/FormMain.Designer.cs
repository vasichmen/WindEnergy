namespace Installer
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonInstall = new System.Windows.Forms.Button();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonGenerateFullKey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonInstall
            // 
            this.buttonInstall.Location = new System.Drawing.Point(16, 15);
            this.buttonInstall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(227, 103);
            this.buttonInstall.TabIndex = 0;
            this.buttonInstall.Text = "Установить";
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(251, 15);
            this.buttonGenerate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(227, 103);
            this.buttonGenerate.TabIndex = 1;
            this.buttonGenerate.Text = "Сгенерировать ключ";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonGenerateFullKey
            // 
            this.buttonGenerateFullKey.Location = new System.Drawing.Point(486, 15);
            this.buttonGenerateFullKey.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGenerateFullKey.Name = "buttonGenerateFullKey";
            this.buttonGenerateFullKey.Size = new System.Drawing.Size(227, 103);
            this.buttonGenerateFullKey.TabIndex = 2;
            this.buttonGenerateFullKey.Text = "Активировать полную версию";
            this.buttonGenerateFullKey.UseVisualStyleBackColor = true;
            this.buttonGenerateFullKey.Click += new System.EventHandler(this.buttonGenerateFullKey_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 130);
            this.Controls.Add(this.buttonGenerateFullKey);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonInstall);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormMain";
            this.Text = "Установка WindEnergy";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonGenerateFullKey;
    }
}

