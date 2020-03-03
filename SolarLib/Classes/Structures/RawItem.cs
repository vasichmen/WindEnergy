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
        private DateTime dt;
        private Dictionary<MeteorologyParameters, double> values;

        public RawItem(DateTime dt, Dictionary<MeteorologyParameters, double> values)
        {
            this.dt = dt;
            this.values = values;
        }

        public double AllSkyInsolation { get { return values[MeteorologyParameters.AllSkyInsolation]; } set { values[MeteorologyParameters.AllSkyInsolation] = value; } }

        public double ClearSkyInsolation { get { return values[MeteorologyParameters.ClearSkyInsolation]; } set { values[MeteorologyParameters.ClearSkyInsolation] = value; } }
    }
}
