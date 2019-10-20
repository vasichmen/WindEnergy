using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Installer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonInstall_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Application.ExecutablePath;
            fbd.Description = "Выберите папку для установки";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Installer.Install(Application.StartupPath, fbd.SelectedPath);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Application.ExecutablePath;
            fbd.Description = "Выберите папку для сохранения ключа";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Installer.GenerateKey(fbd.SelectedPath+"\\id.key");
            }
        }
    }
}
