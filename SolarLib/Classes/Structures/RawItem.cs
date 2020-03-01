using CommonLib.Classes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Classes.Structures
{
   public class RawItem:RawItemBase
    {
        private DateTime dt;
        private Dictionary<MeteorologyParameters, double> values;

        public RawItem(DateTime dt, Dictionary<MeteorologyParameters, double> values)
        {
            this.dt = dt;
            this.values = values;
        }

        public double AllSkyInsolation { get; set; }

        public double ClearSkyInsolation { get; set; }
    }
}
