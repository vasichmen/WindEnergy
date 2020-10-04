using CommonLib.Classes.Base;
using System;
using System.Windows.Forms;

namespace SolarLib.Classes.Structures.Options
{
    public class Options : OptionsBase
    {

        public Options()
        {
            LastDirectory = Application.StartupPath;
            StaticNPSMeteostationDatabaseSourceFile = Application.StartupPath + "\\Data\\NPS.database.txt";
        }

        public string StaticNPSMeteostationDatabaseSourceFile { get; set; }


        /// <summary>
        /// Загрузка файла настроек
        /// </summary>
        /// <param name="filename">адрес файла</param>
        /// <returns></returns>
        public static Options Load(string filename)
        {
            try
            {
                Options res = xmlDeserialize<Options>(filename);
                return res ?? new Options();
            }
            catch (Exception) { return new Options(); }
        }

    }
}
