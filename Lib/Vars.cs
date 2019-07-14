using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Structures.Options;
using WindEnergy.Lib.Data.Interfaces;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.Lib.Data.Providers.DB;
using WindEnergy.Lib.Data.Providers.DB.ETOPO;
using WindEnergy.Lib.Data.Providers.FileSystem;

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

        /// <summary>
        /// подключение к базе данных ETOPO
        /// </summary>
        private static ETOPOProvider _ETOPOdatabase = null;

        /// <summary>
        /// подключение к базе данных ETOPO
        /// </summary>
        public static ETOPOProvider ETOPOdatabase
        {
            get
            {
                if (_ETOPOdatabase == null)
                    _ETOPOdatabase = new ETOPOProvider(Options.ETOPO2Folder);
                return _ETOPOdatabase;
            }
        }

        /// <summary>
        /// Список метеостанций из БД Метеостанции мира
        /// </summary>
        public static MeteostationDatabase Meteostations
        {
            get
            {
                if (_Meteostations == null)
                    _Meteostations = new MeteostationDatabase();
                return _Meteostations;
            }
            set { _Meteostations = value; }
        }
        private static MeteostationDatabase _Meteostations = null;
    }
}
