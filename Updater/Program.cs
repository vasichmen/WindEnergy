using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Updater
{
    class Program
    {
        static string TempFolder = Application.StartupPath + "\\update_tmp";
        static string TmpFile;
        static DateTime LastPrint;

        static void Main(string[] args)
        {
            if (args.Length == 0)
                return;

            //завершить все экземпляры программы
            foreach (Process proc in Process.GetProcessesByName("WindEnergy"))
                proc.Kill();

            //скачать архив с сайта   
            TmpFile = GetTempFileName();
            LastPrint = DateTime.Now;
            WebClient webcl = new WebClient();
            webcl.DownloadFileAsync(new Uri(args[0]), TmpFile);
            webcl.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);

            while (webcl.IsBusy)
                Thread.Sleep(200);
            webcl.Dispose();
            Console.Clear();


            //распаковать в временную папку
            Console.WriteLine("Распаковка...");
            if (Directory.Exists(TempFolder + "\\Release"))
                Directory.Delete(TempFolder + "\\Release", true);
            ZipFile.ExtractToDirectory(TmpFile, TempFolder);


            //заменить файлы в папке lib, windEnergy.exe
            Console.WriteLine("Обновление...");
            string from = TempFolder + "\\Release", dest = Application.StartupPath;
            File.Copy(from + "\\WindEnergy.exe", dest + "\\WindEnergy.exe", true);
            File.Copy(from + "\\WindEnergy.exe.config", dest + "\\WindEnergy.exe.config", true);
            File.Copy(from + "\\System.Data.SQLite.dll.config", dest + "\\System.Data.SQLite.dll.config", true);
            File.Copy(from + "\\Lib.dll.config", dest + "\\Lib.dll.config", true);
            File.Copy(from + "\\WindEnergy.exe", dest + "\\WindEnergy.exe", true);
            File.Copy(from + "\\changelog.txt", dest + "\\changelog.txt", true);
            CopyDir(from + "\\libs", dest + "\\libs");

            //удаление временной папки
            Directory.Delete(TempFolder, true);

            //запустить программу
            Process.Start(Application.StartupPath + "\\WindEnergy.exe");

            Console.WriteLine("Обновление завершено!");
            Thread.Sleep(1000);

        }


        private static void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //Console.Clear();
            if (DateTime.Now - LastPrint > TimeSpan.FromSeconds(1))
            {
                Console.SetCursorPosition(0, 0);
                Console.Write($"Загрузка файла обновления: {e.ProgressPercentage}% {e.BytesReceived / (1024 * 1024)}MБ из {e.TotalBytesToReceive / (1024 * 1024)}MБ");
                LastPrint = DateTime.Now;
            }
        }

        /// <summary>
        /// создание временного файла
        /// </summary>
        /// <returns></returns>
        public static string GetTempFileName()
        {

            if (!Directory.Exists(TempFolder))
                Directory.CreateDirectory(TempFolder);
            string res = TempFolder + "\\" + Guid.NewGuid().ToString() + ".tmp";
            while (File.Exists(res))
                res = TempFolder + "\\" + Guid.NewGuid().ToString() + ".tmp";
            return res;
        }


        private static void CopyDir(string FromDir, string ToDir)
        {
            Directory.CreateDirectory(ToDir);
            foreach (string s1 in Directory.GetFiles(FromDir))
            {
                string s2 = ToDir + "\\" + Path.GetFileName(s1);
                File.Copy(s1, s2, true);
            }
            foreach (string s in Directory.GetDirectories(FromDir))
            {
                CopyDir(s, ToDir + "\\" + Path.GetFileName(s));
            }
        }



    }
}
