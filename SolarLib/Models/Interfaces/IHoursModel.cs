using SolarEnergy.SolarLib.Classes.Collections;

namespace SolarEnergy.SolarLib.Models.Interfaces
{
    public interface IHoursModel
    {
        DataHours<double> GetData(double dailySum, MeteorologyParameters allSkyInsolation);
    }
}
