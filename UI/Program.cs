using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Vars.Options = Options.Load(Application.StartupPath);
            Vars.LocalFileSystem = new LocalFileSystem();

            winMain = new FormMain();


            //обработчик выхода из приложения
            Application.ApplicationExit += application_ApplicationExit;

            Application.Run(winMain);
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
                if (Directory.Exists( Vars.Options.TempFolder))
                    Directory.Delete( Vars.Options.TempFolder, true);
            }
            catch (Exception exxx) { Debug.Print(exxx.Message); }
            finally { Debug.Print("Temp directory removed"); }
        }
    }
}
