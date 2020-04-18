using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Transformation.Altitude
{
  public  class SuitAMSResult
    {
        public AMSMeteostationInfo AMS { get; internal set; }
        public bool AllMonthInRange { get; internal set; }
    }
}
