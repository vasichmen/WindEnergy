using CommonLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Transformation.Restore.Interpolation;

namespace WindEnergy.WindLib.Calculation.PowerGeneration
{
    public class PowerCalculator
    {
        public const double MIN_WIND_SPEED_STEP = 1e-10;

        /// <summary>
        /// расчет мощностной характеристики на основе основных параметров ВЭУ
        /// </summary>
        /// <param name="selectedEquipment"></param>
        /// <returns></returns>
        public static Dictionary<double, double> CalculatePerformanceCharacteristic(EquipmentItemInfo selectedEquipment)
        {
            if (!selectedEquipment.EnoughDataToCalculate)
                throw new WindEnergyException("Недостаточно данных для расчета мощностной характеристики");

            LinearInterpolateMethod interpolator = new LinearInterpolateMethod(new Dictionary<double, double>() {
                {0,0 },
                {selectedEquipment.MinWindSpeed,0 },
                {selectedEquipment.NomWindSpeed,selectedEquipment.Power },
                {selectedEquipment.MaxWindSpeed,selectedEquipment.Power },
                { selectedEquipment.MaxWindSpeed+MIN_WIND_SPEED_STEP,0 },
                {31,0 },
            });

            Dictionary<double, double> res = new Dictionary<double, double>();
            for (double speed = 1; speed <= 30; speed++)
                res.Add(speed, interpolator.GetValue(speed));
            return res;
        }
    }
}
