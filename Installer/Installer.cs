using CommonLib;
using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Installer
{
    internal class Installer
    {
        public static void Install(string from, string dest)
        {
            //завершить все экземпляры программы
            foreach (Process proc in Process.GetProcessesByName("WindEnergy"))
                proc.Kill();
            foreach (Process proc in Process.GetProcessesByName("SolarEnergy"))
                proc.Kill();

            if (!dest.EndsWith("WindEnergy") && !dest.EndsWith("WindEnergy\\"))
            {
                if (dest.EndsWith("\\"))
                    dest += "WindEnergy";
                else
                    dest += "\\WindEnergy";
            }
            if (dest.EndsWith("\\"))
                dest = dest.TrimEnd('\\');


            //КОПИРОВАНИЕ ФАЙЛОВ
            CopyDir(from + "\\Data", dest + "\\Data");
            CopyDir(from + "\\libs", dest + "\\libs");
            string[] files = {
                "WindEnergy.exe.config",
                "SolarEnergy.exe.config",
                "Updater.exe.config",
                "System.Data.SQLite.dll.config",
                "CommonLib.dll.config",
                "WindLib.dll.config",
                "SolarLib.dll.config",
                "postinstall.exe.config",
                "postinstall.exe",
                "preinstall.exe.config",
                "preinstall.exe",
                "WindEnergy.exe",
                "SolarEnergy.exe",
                "Updater.exe",
                "changelog.txt",
            };
            foreach (string file in files)
            {
                try
                {
                    System.IO.File.Copy(from + "\\" + file, dest + "\\" + file, true);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Копирование файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            //ГЕНЕРАЦИЯ КЛЮЧА
            GenerateKey(dest + "\\id.key");

            //СОЗДАНИЕ ЯРЛЫКОВ
            createShortcut(dest + "\\WindEnergy.exe", "Ветроэнергетический кадастр");
            createShortcut(dest + "\\SolarEnergy.exe", "");


            MessageBox.Show("Установка завершена");
            Process.Start(dest + "\\WindEnergy.exe");
        }

        private static void createShortcut(string targetFile, string description)
        {
            //СОЗДАНИЕ ЯРЛЫКА
            WshShell shell = new WshShell();
            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + Path.GetFileNameWithoutExtension(targetFile) + @".lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            //задаем свойства для ярлыка
            //описание ярлыка в всплывающей подсказке
            shortcut.Description = description;
            //путь к самой программе
            shortcut.TargetPath = targetFile;
            //Создаем ярлык
            shortcut.Save();
        }

        internal static void GenerateKey(string selectedPath)
        {
            byte[] key = Driver.GetID();
            WriteToFileArray(key, selectedPath);
        }

        private static void WriteToFileArray(byte[] array, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8))
            {
                foreach (byte b in array)
                    sw.WriteLine(b);
                sw.Close();
            }
        }

        private static void CopyDir(string FromDir, string ToDir)
        {
            Directory.CreateDirectory(ToDir);
            foreach (string s1 in Directory.GetFiles(FromDir))
            {
                string s2 = ToDir + "\\" + Path.GetFileName(s1);
                System.IO.File.Copy(s1, s2, true);
            }
            foreach (string s in Directory.GetDirectories(FromDir))
            {
                CopyDir(s, ToDir + "\\" + Path.GetFileName(s));
            }
        }
    }
}
