using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackConverter.UI.Common.Dialogs;
using WindEnergy.Lib.Classes;
using WindEnergy.Lib.Classes.Structures.Options;
using WindEnergy.Lib.Data.Providers;

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
#if (!DEBUG)
            try
            {
#endif

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Vars.Options = Options.Load(Application.StartupPath + "\\options.xml");
                Vars.LocalFileSystem = new LocalFileSystem();

                winMain = new FormMain();


                //обработчик выхода из приложения
                Application.ApplicationExit += application_ApplicationExit;


            #region запись статистики, проверка версии

            new Task(new Action(() =>
            {
                Velomapa site = new Velomapa(); //связь с сайтом
                site.SendStatisticAsync(); //статистика

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
            Vars.Options.Save(Application.StartupPath + "\\options.xml");
        }
    }
}
