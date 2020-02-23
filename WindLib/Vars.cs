using CommonLibLib.Data.Providers.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Structures.Options;
using WindEnergy.WindLib.Data.Interfaces;
using WindEnergy.WindLib.Data.Providers;
using WindEnergy.WindLib.Data.Providers.DB;
using WindEnergy.WindLib.Data.Providers.DB.ETOPO;
using WindEnergy.WindLib.Data.Providers.FileSystem;

namespace WindLib
{
    /// <summary>
    /// глобальные переменные
    /// </summary>
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
        /// дата последней проверки параметров компьютера
        /// </summary>
        public static DateTime LastCheckEngine = DateTime.MinValue;


        #region локальные БД

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
        /// подключение к базе данных ETOPO
        /// </summary>
        private static ETOPOProvider _ETOPOdatabase = null;

        /// <summary>
        /// Список метеостанций из БД Метеостанции мира
        /// </summary>
        public static RP5MeteostationDatabase RP5Meteostations
        {
            get
            {
                if (_RP5Meteostations == null)
                    _RP5Meteostations = new RP5MeteostationDatabase(Options.StaticMeteostationCoordinatesSourceFile);
                return _RP5Meteostations;
            }
            set { _RP5Meteostations = value; }
        }
        private static RP5MeteostationDatabase _RP5Meteostations = null;

        /// <summary>
        /// Список метеостанций из БД Метеостанции мира
        /// </summary>
        public static SpeedLimitsDatabase SpeedLimits
        {
            get
            {
                if (_SpeedLimits == null)
                    _SpeedLimits = new SpeedLimitsDatabase();
                return _SpeedLimits;
            }
            set { _SpeedLimits = value; }
        }
        private static SpeedLimitsDatabase _SpeedLimits = null;

        /// <summary>
        /// Список метеостанций из БД АМС
        /// </summary>
        public static AMSMeteostationDatabase AMSMeteostations
        {
            get
            {
                if (_AMSMeteostations == null)
                    _AMSMeteostations = new AMSMeteostationDatabase(Options.StaticAMSDatabaseSourceFile);
                return _AMSMeteostations;
            }
            set { _AMSMeteostations = value; }
        }
        private static AMSMeteostationDatabase _AMSMeteostations = null;

        /// <summary>
        /// Список метеостанций из БД АМС
        /// </summary>
        public static FlugerMeteostationDatabase FlugerMeteostations
        {
            get
            {
                if (_FlugerMeteostations == null)
                    _FlugerMeteostations = new FlugerMeteostationDatabase(Options.StaticFlugerDatabaseSourceFile);
                return _FlugerMeteostations;
            }
            set { _FlugerMeteostations = value; }
        }
        private static FlugerMeteostationDatabase _FlugerMeteostations = null;

        /// <summary>
        /// БД оборудования
        /// </summary>
        public static EquipmentDatabase EquipmentDatabase
        {
            get
            {
                if (_EquipmentDatabase == null)
                    _EquipmentDatabase = new EquipmentDatabase(Options.StaticEquipmentDatabaseSourceFile);
                return _EquipmentDatabase;
            }
            set { _EquipmentDatabase = value; }
        }
        private static EquipmentDatabase _EquipmentDatabase = null;

        /// <summary>
        /// локальная БД Расписание погоды
        /// </summary>
        public static RP5Database RP5Database
        {
            get
            {
                if (_RP5Database == null)
                    _RP5Database = new RP5Database(Options.StaticRP5DatabaseSourceDirectory);
                return _RP5Database;
            }
            set { _RP5Database = value; }
        }
        private static RP5Database _RP5Database = null;

        /// <summary>
        /// локальная БД мезоклиматических коэффициентов
        /// </summary>
        public static MesoclimateTableDatabase MesoclimateTableDatabase
        {
            get
            {
                if (_MesoclimateTableDatabase == null)
                    _MesoclimateTableDatabase = new MesoclimateTableDatabase(Options.StaticMesoclimateTableDatabaseSourceFile);
                return _MesoclimateTableDatabase;
            }
            set { _MesoclimateTableDatabase = value; }
        }
        private static MesoclimateTableDatabase _MesoclimateTableDatabase = null;

        /// <summary>
        /// локальная БД микроклиматических коэффициентов
        /// </summary>
        public static MicroclimateTableDatabase MicroclimateTableDatabase
        {
            get
            {
                if (_MicroclimateTableDatabase == null)
                    _MicroclimateTableDatabase = new MicroclimateTableDatabase(Options.StaticMicroclimateTableDatabaseSourceFile);
                return _MicroclimateTableDatabase;
            }
            set { _MicroclimateTableDatabase = value; }
        }
        private static MicroclimateTableDatabase _MicroclimateTableDatabase = null;

        #endregion

    }

}
