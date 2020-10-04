using System;
using System.Windows.Forms;

namespace Installer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
#if BUILD_FULL
            this.buttonGenerateFullKey.Visible = true;
#else
            this.buttonGenerateFullKey.Visible = false;
#endif
        }

        private void buttonInstall_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Application.ExecutablePath;
            fbd.Description = "Выберите папку для установки";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
#if BUILD_FULL
                Driver.GenerateFullKey();
#endif
                Installer.Install(Application.StartupPath, fbd.SelectedPath);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Application.StartupPath;
            fbd.Description = "Выберите папку для сохранения ключа";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Installer.GenerateKey(fbd.SelectedPath + "\\id.key");
            }
        }

        private void buttonGenerateFullKey_Click(object sender, EventArgs e)
        {
#if BUILD_FULL
            Driver.GenerateFullKey();
            MessageBox.Show("Ключ создан");
#endif
        }
    }
}
