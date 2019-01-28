using WindEnergy.Lib.Classes.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using WindEnergy.Lib.Data.Providers;

namespace WindEnergy
{
    public static class Vars
    {
        /// <summary>
        /// настройки программы
        /// </summary>
        public static Options Options { get; set; }

        /// <summary>
        /// объект взаимодействия с файловой системой
        /// </summary>
        public static LocalFileSystem LocalFileSystem { get; set; }

        /// <summary>
        /// разделитель десятичных разрядов при текущих настройках ОС
        /// </summary>
        private static char decimalSeparator = char.MinValue;

        /// <summary>
        /// разделитель десятичных разрядов в этой операционной системе
        /// </summary>
        public static char DecimalSeparator
        {
            get
            {
                if (decimalSeparator == char.MinValue)
                    decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                return decimalSeparator;
            }
        }
    }
}
