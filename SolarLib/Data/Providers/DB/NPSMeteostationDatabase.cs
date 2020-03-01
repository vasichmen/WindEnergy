using CommonLib.Classes.Base;
using GMap.NET;
using SolarEnergy.SolarLib.Classes.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Data.Providers.DB
{
    public class NPSMeteostationDatabase : BaseMeteostationDatabase<PointLatLng, NPSMeteostationInfo>
    {
        /// <summary>
        /// создает объект для этого файла, не загружая данные
        /// </summary>
        /// <param name="FileName">адрес файла БД</param>
        public NPSMeteostationDatabase(string FileName) : base(FileName) { }

        public override Dictionary<PointLatLng, NPSMeteostationInfo> LoadDatabaseFile()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// найти МС по wmo_id
        /// </summary>
        /// <param name="id">wmo_id</param>
        /// <returns></returns>
        internal NPSMeteostationInfo GetByID(string id)
        {
            var res = from t in List
                      where t.ID == id
                      select t;
            if (res.Count() > 0)
                return res.First();
            else
                return null;
        }
    }
}
