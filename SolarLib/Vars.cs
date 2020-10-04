using CommonLibLib.Data.Providers.FileSystem;
using SolarEnergy.SolarLib.Data.Providers.DB;
using SolarLib.Classes.Structures.Options;

namespace SolarLib
{
    public static class Vars
    {
        public static Options Options { get; set; }
        public static LocalFileSystem LocalFileSystem { get; set; }

        /// <summary>
        /// локальная БД мезоклиматических коэффициентов
        /// </summary>
        public static NPSMeteostationDatabase NPSMeteostationDatabase
        {
            get
            {
                if (_NPSMeteostationDatabase == null)
                    _NPSMeteostationDatabase = new NPSMeteostationDatabase(Options.StaticNPSMeteostationDatabaseSourceFile);
                return _NPSMeteostationDatabase;
            }
            set { _NPSMeteostationDatabase = value; }
        }
        private static NPSMeteostationDatabase _NPSMeteostationDatabase = null;
    }
}
