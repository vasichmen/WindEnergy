using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Data.Providers.DB
{
    public class FlugerMeteostationDatabase : BaseFileDatabase<PointLatLng, FlugerMeteostationDatabase>
    {
        /// <summary>
        /// создает объект для этого файла, не загружая данные
        /// </summary>
        /// <param name="FileName">адрес файла БД</param>
        public FlugerMeteostationDatabase(string FileName) : base(FileName) { }

        public override Dictionary<PointLatLng, FlugerMeteostationDatabase> LoadDatabaseFile()
        {
            throw new NotImplementedException();
        }
    }
}
