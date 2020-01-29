using CommonLib.Classes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolarLib.Classes.Options
{
    public class Options : OptionsBase
    {

        public Options()
        {
            LastDirectory = Application.StartupPath;
        }


        /// <summary>
        /// Загрузка файла настроек
        /// </summary>
        /// <param name="filename">адрес файла</param>
        /// <returns></returns>
        public static Options Load(string filename)
        {
            try
            {
                OptionsBase res = xmlDeserialize(filename);
                return res == null ? new Options() : (Options)res;
            }
            catch (Exception) { return new Options(); }
        }

    }
}
