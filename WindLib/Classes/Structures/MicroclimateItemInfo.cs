using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Classes.Structures
{
    public class MicroclimateItemInfo
    {
        /// <summary>
        /// название, описание рельефа
        /// </summary>
        public string Name { get; set; }

        public double Stable_3_5 { get; set; }
        public double Stable_6_20 { get; set; }
        public double Unstable_3_5 { get; set; }
        public double Unstable_6_20 { get; set; }
    }
}
