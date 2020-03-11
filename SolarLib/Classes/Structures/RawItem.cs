using CommonLib.Classes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Classes.Structures
{
    public class RawItem : RawItemBase
    {
        private Dictionary<MeteorologyParameters, double> values;

        public RawItem(DateTime dt, Dictionary<MeteorologyParameters, double> values)
        {
            this.Date = dt;
            this.values = values;

            if (values[MeteorologyParameters.AllSkyInsolation] < values[MeteorologyParameters.ClearSkyInsolation]) ;
            { }

        }

        public RawItem(DateTime dt, double allSky, double clearSky)
        {
            Date = dt;
            this.values = new Dictionary<MeteorologyParameters, double>() {
                { MeteorologyParameters.AllSkyInsolation, allSky },
                {MeteorologyParameters.ClearSkyInsolation, clearSky }
            };
        }

        public double AllSkyInsolation { get { return values[MeteorologyParameters.AllSkyInsolation]; } set { values[MeteorologyParameters.AllSkyInsolation] = value; } }

        public double ClearSkyInsolation { get { return values[MeteorologyParameters.ClearSkyInsolation]; } set { values[MeteorologyParameters.ClearSkyInsolation] = value; } }

        public double DiffuseSkyInsolation { get { return AllSkyInsolation - ClearSkyInsolation; } }
    }
}
