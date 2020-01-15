using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Data.Interfaces;
using WindEnergy.WindLib.Data.Providers;

namespace WindEnergy
{
    /// <summary>
    /// дополнительные функции энергетических расчётов
    /// </summary>
  public static  class PowerFunctions
    {
        /// <summary>
        /// рассчитать плотность воздуха для этого ряда в кг/м3. В ряду должны быть координаты точки и данные о температуре
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static double GetAirDensity(RawRange range)
        {
            if (range.Position.IsEmpty)
                throw new WindEnergyException("Для вычисления плотности воздуха необходимы координаты точки");

            IGeoInfoProvider provider = Vars.ETOPOdatabase;           
            double alt = provider.GetElevation(range.Position);
            double pressure = 101.29 - 0.011837 * alt + 4.793e-7 * Math.Pow(alt, 2);

            double temp_aver = 0;
            int c = 0;
            foreach (var i in range)
                if (!double.IsNaN(i.Temperature))
                {
                    c++;
                    temp_aver += i.Temperature;
                }
            temp_aver /= c;
            temp_aver += 273; //градусы Кельвина
            double dens = 3.4837 * (pressure / temp_aver);
            return dens;
        }
    }
}
