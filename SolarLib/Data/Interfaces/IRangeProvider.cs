using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Data.Interfaces
{
    interface IRangeProvider
    {

        RawRange GetRange(DateTime fromDate, DateTime toDate, NPSMeteostationInfo point_info, Action<double> onPercentChange = null, Func<bool> checkStop = null);

    }
}
