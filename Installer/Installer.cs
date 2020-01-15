using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;
using System.Diagnostics;
using WindEnergy;
using CommonLib;

namespace Installer
{
    class Installer
    {
        public static void Install(string from, string dest)
        {
            //скопировать файлы
            //сгенерировать ключ
            //создать ярлык на рабочем столе
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
            System.IO.File.Copy(from + "\\WindEnergy.exe.config", dest + "\\WindEnergy.exe.config", true);
            System.IO.File.Copy(from + "\\Updater.exe.config", dest + "\\Updater.exe.config", true);
            System.IO.File.Copy(from + "\\System.Data.SQLite.dll.config", dest + "\\System.Data.SQLite.dll.config", true);
            System.IO.File.Copy(from + "\\Lib.dll.config", dest + "\\Lib.dll.config", true);
            System.IO.File.Copy(from + "\\WindEnergy.exe", dest + "\\WindEnergy.exe", true);
            System.IO.File.Copy(from + "\\Updater.exe", dest + "\\Updater.exe", true);
            System.IO.File.Copy(from + "\\changelog.txt", dest + "\\changelog.txt", true);

            //ГЕНЕРАЦИЯ КЛЮЧА
            GenerateKey(dest + "\\id.key");

            //СОЗДАНИЕ ЯРЛЫКА
            WshShell shell = new WshShell();
            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\WindEnergy.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            //задаем свойства для ярлыка
            //описание ярлыка в всплывающей подсказке
            shortcut.Description = "Ветроэнергетический кадастр";
            //путь к самой программе
            shortcut.TargetPath = dest + "\\WindEnergy.exe";
            //Создаем ярлык
            shortcut.Save();

            System.Windows.Forms.MessageBox.Show("Установка завершена");
            Process.Start(dest + "\\WindEnergy.exe");
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
