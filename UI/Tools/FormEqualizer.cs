using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Operations;

namespace WindEnergy.UI.Tools
{
    public partial class FormEqualizer : Form
    {
        List<string> files;
        string folder;

        public FormEqualizer()
        {
            InitializeComponent();
            labelFiles.Text = "Файлы не выбраны";
            labelFolder.Text = "Папка не выбрана";
        }

        private void buttonOpenFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog()
            {
                DefaultExt = ".csv",
                Filter = "*.csv|*.csv",
                Multiselect = true,
                InitialDirectory = Application.StartupPath
            };
            if (of.ShowDialog() == DialogResult.OK)
            {
                files = of.FileNames.ToList();
                labelFiles.Text = $"Выбрано файлов: {files.Count}";
            }
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Application.StartupPath;
            fbd.Description = "Выберите папку сохранения файлов";
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                folder = fbd.SelectedPath;
                labelFolder.Text = folder;
            }
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            if (files == null ||files.Count<2)
            {
                MessageBox.Show(this,"Преобразование файлов","Ошибка: необходимо выбрать 2 или более файлов");
            }

            if (string.IsNullOrWhiteSpace(folder))
            {
                MessageBox.Show(this, "Преобразование файлов", "Ошибка: файлы не выбраны");
            }
            Equalizer.ProcessRanges(files, folder, (int)numericUpDownStartLine.Value, checkBoxSeparateDate.Checked);
            Process.Start(folder);
        }
    }
}
