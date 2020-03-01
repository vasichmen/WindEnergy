using CommonLib.Classes.Base;
using SolarEnergy.SolarLib.Classes.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Classes.Structures
{
   public class NPSMeteostationInfo:BaseMeteostationInfo
    {
        public Dataset DatasetAllsky { get; set; }
        public Dataset DatasetClearSky { get; set; }
    }
}
