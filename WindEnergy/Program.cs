using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.UI.Common.Dialogs;
using WindEnergy.WindLib.Classes;
using WindEnergy.WindLib.Classes.Structures.Options;
using WindEnergy.WindLib.Data.Providers;
using WindEnergy.WindLib.Data.Providers.FileSystem;
using WindEnergy.WindLib.Data.Providers.InternetServices;
using WindEnergy.WindLib;
using CommonLib;
using CommonLib.Classes;
using CommonLibLib.Data.Providers.FileSystem;
using CommonLib.Data.Providers.InternetServices;
using WindLib;

namespace WindEnergy.UI
{
    internal static class Program
    {
        public static FormMain winMain { get; set; }



        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {

#if(!DEBUG)
            bool is_accept = File.Exists(Application.StartupPath + "\\id.key");
            if (is_accept)
            {
                byte[] cur_key = Driver.GetID();
                byte[] file_key = Driver.LoadID(Application.StartupPath + "\\id.key");
                if (cur_key.Length == file_key.Length)
                    for (int i = cur_key.Length - 1; i >= 0; i--)
                        is_accept &= cur_key[i] == file_key[i];
                else
                    is_accept = false;
            }
            if (!is_accept)
            {
                MessageBox.Show("Ошибка при проверке файла ключа, программа будет закрыта\r\n");
                return;
            }
#endif


#if (!DEBUG)
            try
            {
#endif

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Vars.Options = Options.Load(Application.StartupPath + "\\windOptions.xml");
                if (Vars.Options == null)
                {
                    throw new Exception("Файлы программы повреждены, запуск невозможен");
                }
                Vars.LocalFileSystem = new LocalFileSystem(Vars.Options.TempFolder);

                winMain = new FormMain();


                //обработчик выхода из приложения
                Application.ApplicationExit += application_ApplicationExit;


                #region запись статистики, проверка версии

                new Task(new Action(() =>
                {
                    Velomapa site = new Velomapa(); //связь с сайтом
                site.SendStatisticAsync(Vars.Options.ApplicationGuid); //статистика

                //действие при проверке версии
                Action<VersionInfo> action = new Action<VersionInfo>((vi) =>
                    {
                        float curVer = Vars.Options.VersionInt;
                        if (vi.VersionInt > curVer)
                        {
                            FormUpdateDialog fud = new FormUpdateDialog(vi);
                            if (Vars.Options.UpdateMode != UpdateDialogAnswer.AlwaysIgnore)
                                winMain.Invoke(new Action(() => fud.ShowDialog()));
                        }
                    });
                    site.GetVersionAsync(action); //проверка версии
            })
                ).Start();

                #endregion

                Application.Run(winMain);
#if (!DEBUG)
            }
            catch (Exception e)
            {
                StreamWriter sw = new StreamWriter("exceptions.log", true, Encoding.UTF8);
                sw.WriteLine("{0}\r\n{1}", e.Message, e.StackTrace);
                sw.Close();
            }
#endif
        }

        /// <summary>
        /// выход и приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void application_ApplicationExit(object sender, EventArgs e)
        {
            //очистка времнной папки
            try
            {
                if (Directory.Exists(Vars.Options.TempFolder))
                    Directory.Delete(Vars.Options.TempFolder, true);
            }
            catch (Exception exxx) { Debug.Print(exxx.Message); }
            finally { Debug.Print("Temp directory removed"); }

            //сохранение настроек
            Vars.Options.Save(Application.StartupPath + "\\windOptions.xml");
        }
    }
}
