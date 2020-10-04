using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using System;

namespace SolarEnergy.SolarLib.Data.Interfaces
{
    internal interface IRangeProvider
    {

        RawRange GetRange(DateTime fromDate, DateTime toDate, NPSMeteostationInfo point_info, Action<double> onPercentChange = null, Func<bool> checkStop = null);

    }
}
