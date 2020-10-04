using CommonLib.Operations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CommonLib.UITools
{
    public partial class FormEqualizer : Form
    {
        private List<string> files;
        public string Directory;
        private string folder;

        public FormEqualizer(string directory, Icon icon)
        {
            InitializeComponent();
            labelFiles.Text = "Файлы не выбраны";
            labelFolder.Text = "Папка не выбрана";
            Directory = directory;
            this.Icon = icon;
        }

        private void buttonOpenFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog()
            {
                DefaultExt = ".csv",
                Filter = "*.csv|*.csv",
                Multiselect = true,
                InitialDirectory = Directory
            };
            if (of.ShowDialog() == DialogResult.OK)
            {
                files = of.FileNames.ToList();
                Directory = Path.GetDirectoryName(of.FileNames[0]);
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
            if (files == null || files.Count < 2)
            {
                _ = MessageBox.Show(this, "Преобразование файлов", "Ошибка: необходимо выбрать 2 или более файлов");
            }

            if (string.IsNullOrWhiteSpace(folder))
            {
                _ = MessageBox.Show(this, "Преобразование файлов", "Ошибка: не выбрана папка сохранения файлов");
            }
            Equalizer.ProcessRanges(files, folder, (int)numericUpDownStartLine.Value, checkBoxSeparateDate.Checked);
            _ = Process.Start(folder);
        }
    }
}
