using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Updater
{
    internal class Program
    {
        private static string TempFolder = Application.StartupPath + "\\update_tmp";
        private static string postinstallFile = Application.StartupPath + "\\postinstall.exe";
        private static string preinstallFile = Application.StartupPath + "\\preinstall.exe";
        private static string TmpFile;
        private static DateTime LastPrint;
        private static object locker = new object();

        private static void Main(string[] args)
        {
            if (args.Length == 0)
                return;

            //список файлов программы для обновления
            string[] files = {
                "*.exe.config",
                "*.dll.config",
                "*.exe",
                "changelog.txt",
            };

            Console.WriteLine("Подготовка к обновлению...");
            //завершить все экземпляры программы
            foreach (Process proc in Process.GetProcessesByName("WindEnergy"))
                proc.Kill();
            foreach (Process proc in Process.GetProcessesByName("SolarEnergy"))
                proc.Kill();

            //запуск скрипта после обновления
            if (File.Exists(preinstallFile))
                Process.Start(preinstallFile).WaitForExit(); //дожидаемся окончания работы процесса

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


            //заменить файлы в папке lib, Data и по списку файлов
            Console.WriteLine("Обновление...");
            string from = TempFolder + "\\Release", dest = Application.StartupPath;

            CopyDir(from + "\\Data", dest + "\\Data");
            CopyDir(from + "\\libs", dest + "\\libs");

            foreach (string file in files)
            {
                try
                {
                    foreach (string file_path in Directory.EnumerateFiles(from, file))
                        if (File.Exists(file_path) && Path.GetFileName(Application.ExecutablePath) != Path.GetFileName(file_path))
                            File.Copy(file_path, dest + "\\" + Path.GetFileName(file_path), true);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ОШИБКА!! " + e.Message);
                    Console.ReadLine();
                    return;
                }
            }

            //удаление временной папки
            Directory.Delete(TempFolder, true);

            //запуск скрипта после обновления
            if (File.Exists(postinstallFile))
                Process.Start(postinstallFile).WaitForExit(); //дожидаемся окончания работы процесса


            //запустить программу
            if (args.Length == 2 && File.Exists(args[1]))
                Process.Start(args[1]); //если есть аргумент, то запускаем нужный процесс
            else
                Process.Start(Application.StartupPath + "\\WindEnergy.exe"); //если нет, то по умолчанию

            Console.WriteLine("Обновление завершено!");
            Thread.Sleep(1000);
        }


        private static void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            lock (locker)
            {
                if (DateTime.Now - LastPrint > TimeSpan.FromSeconds(1))
                {
                    Console.SetCursorPosition(0, 1);
                    Console.Write($"Загрузка файла обновления: {e.ProgressPercentage}% {(double)e.BytesReceived / (1024 * 1024):0.000} MБ из {(double)e.TotalBytesToReceive / (1024 * 1024):0.000} MБ");
                    Thread.Sleep(50);
                    LastPrint = DateTime.Now;
                }
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
