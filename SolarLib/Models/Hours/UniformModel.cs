using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Models.Interfaces;

namespace SolarEnergy.SolarLib.Models.Hours
{
    /// <summary>
    /// модель равномерного распределения радиации внутри суток
    /// </summary>
    public class UniformModel : IHoursModel
    {
        public DataHours<double> GetData(double dailySum, MeteorologyParameters allSkyInsolation)
        {
            DataHours<double> res = new DataHours<double>();
            for (int i = 0; i < 24; i++)
                res.Add(i, dailySum / 24d);
            return res;
        }
    }
}
