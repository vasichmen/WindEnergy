using SolarEnergy.SolarLib.Classes.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Models.Interfaces
{
   public interface IHoursModel
    {
        DataHours<double> GetData(double dailySum, MeteorologyParameters allSkyInsolation);
    }
}
