using CommonLibLib.Data.Providers.FileSystem;
using SolarEnergy.SolarLib.Data.Providers.DB;
using SolarLib.Classes.Structures.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLib
{
    public static class Vars
    {
        public static Options Options {get;set;}
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
