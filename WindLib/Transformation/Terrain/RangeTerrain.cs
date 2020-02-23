using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using CommonLib;
using CommonLib.Classes;
using WindEnergy.WindLib.Statistic.Structures;
using WindEnergy.WindLib.Statistic.Calculations;
using WindEnergy.WindLib.Statistic.Collections;

namespace WindEnergy.WindLib.Transformation.Terrain
{
    /// <summary>
    /// преобразование рядов на основе информации о рельефе
    /// </summary>
    public static class RangeTerrain
    {
        /// <summary>
        /// поднять ряд на высоту с заданными настройками
        /// </summary>
        /// <param name="Range">поднимаемый ряд</param>
        /// <param name="param">настройки</param>
        /// <param name="actionPercent">действие при изменении процента выполнения</param>
        /// <param name="actionAfter">действие по окончании выполнения</param>
        public static void ProcessRange(RawRange Range, TerrainParameters param, Action<int> actionPercent, Action<RawRange, FlugerMeteostationInfo> actionAfter)
        {
            //поиск коэффициента k0
            Dictionary<WindDirections8, double> k0 = null;
            RawRange result = new RawRange();
            result.Position = param.PointCoordinates;
            result.Name = $"Ряд в точке {param.PointCoordinates.ToString(3)}";
            result.BeginChange();
            switch (param.TerrainType)
            {
                case TerrainType.First:
                    k0 = getFirstTypeK0(param.MSClasses, param.PointClasses); //получаем коэффициенты для плоского рельефа

                    //пересчет всех скоростей ряда

                    double c = 0;
                    foreach (var item in Range)
                    {
                        WindDirections8 wd8 = item.DirectionRhumb8;
                        double nspeed = item.Speed * k0[wd8]; //для направления в этой точке пересчитываем скорости
                        RawItem nitem = item.Clone();
                        nitem.Speed = nspeed;
                        result.Add(nitem);

                        c++;
                        if (Math.IEEERemainder(c, 100) == 0 && actionPercent != null)
                            actionPercent((int)((c / Range.Count) * 100));
                    }
                    break;
                case TerrainType.Second:

                    StatisticalRange<WindDirections8> expectancy = StatisticEngine.GetDirectionExpectancy(Range, GradationInfo<WindDirections8>.Rhumb8Gradations); //повторяемости скорости ветра по 8 румбам

                    //1. привести Ряд на МС к условиям открытой местности
                    //2. выбрать мезоклиматический коэффициент
                    //3. выбрать микроклиматический коэффициент
                    //4. Vа = kм * kмк * Vмс (только для преобладающего направления? >30%)
                    RawRange flattered = ToFlatTerrain(Range, expectancy, param.MSClasses, param.WaterType);

                    //мезоклиматический коэффициент
                    double Km = (param.MesoclimateCoefficient.Value.From + param.MesoclimateCoefficient.Value.To) / 2d; //среднее внутри диапазона

                    //микроклиматический коэффициент
                    double Kmk = param.MicroclimateCoefficient != null ? selectMicroclimateCoefficient(flattered, param.MicroclimateCoefficient, param.AtmosphereStratification) : 1;

                    //выбор преобладающих направлений ветра
                    List<WindDirections8> dominants = new List<WindDirections8>();
                    var maxes = expectancy.Values.OrderByDescending((d) => d).ToList();
                    dominants.Add((WindDirections8)expectancy.Keys[expectancy.Values.IndexOf(maxes[0])]); //первый всегда добавляем
                    if (maxes[0] > 0.3 && maxes[0] - maxes[1] < 0.1) //второй добавляем если первый больше 30% и у второго отрыв меньше 10%
                        dominants.Add((WindDirections8)expectancy.Keys[expectancy.Values.IndexOf(maxes[1])]); 

                    //пересчет ряда только для преобладающих направлений
                    foreach (RawItem item in flattered)
                    {
                        RawItem ni = item.Clone();
                        ni.Speed = dominants.Contains(ni.DirectionRhumb8) ? ni.Speed * Km * Kmk : ni.Speed;
                        result.Add(ni);
                    }

                    break;
                default: throw new Exception("Этот тип рельефа не реализован");
            }

            result.EndChange();
            actionAfter(result, param.FlugerMeteostation);
            return;
        }

        /// <summary>
        /// выбор микроклиматического коэффициента рельефа по средней скорости и стратификации атмосферы
        /// </summary>
        /// <param name="flattered"></param>
        /// <param name="microclimateCoefficient"></param>
        /// <param name="atmosphereStratification"></param>
        /// <returns></returns>
        private static double selectMicroclimateCoefficient(RawRange flattered, MicroclimateItemInfo microclimateCoefficient, AtmosphereStratification atmosphereStratification)
        {
            double aver = flattered.Average((i) => i.Speed);
            if (aver <= 5)
                return atmosphereStratification == AtmosphereStratification.Stable ? microclimateCoefficient.Stable_3_5 : microclimateCoefficient.Unstable_3_5;
            else
                return atmosphereStratification == AtmosphereStratification.Stable ? microclimateCoefficient.Stable_6_20 : microclimateCoefficient.Unstable_6_20;
        }


        /// <summary>
        /// расчет коэффициента k0 для первого типа рельефа
        /// </summary>
        /// <param name="msClasses"></param>
        /// <param name="pointClasses"></param>
        /// <returns></returns>
        private static Dictionary<WindDirections8, double> getFirstTypeK0(Dictionary<WindDirections8, double> msClasses, Dictionary<WindDirections8, double> pointClasses)
        {
            msClasses = msClasses ?? throw new ArgumentException(nameof(msClasses));
            pointClasses = pointClasses ?? throw new ArgumentException(nameof(pointClasses));
            if (msClasses.Count != 8 || pointClasses.Count != 8)
                throw new WindEnergyException("Классы открытости заданы не для всех направлений");

            Dictionary<WindDirections8, double> res = new Dictionary<WindDirections8, double>();
            foreach (WindDirections8 dir in WindDirections8.E.GetEnumItems())
                res[dir] = pointClasses[dir] / msClasses[dir];

            return res;
        }

        /// <summary>
        /// приведение ряда к условиям плоской местности
        /// </summary>
        /// <param name="ms_range">ряд</param>
        /// <param name="expectancy">повторяемости направлений ветра</param>
        /// <param name="ms_classes">классы открытости МС</param>
        /// <param name="water_type">расстояние до водной поверхности</param>
        /// <returns></returns>
        public static RawRange ToFlatTerrain(RawRange ms_range, StatisticalRange<WindDirections8> expectancy, Dictionary<WindDirections8, double> ms_classes, WaterDistanceType water_type)
        {
            //1. найти Кмс
            //2. пересчитать ряд

            RawRange result = new RawRange();
            double K0 = (int)water_type;

            //средневзвешенный коэффициент открытости метеостанции
            double Kms = 0;
            foreach (WindDirections8 dir in WindDirections8.N.GetEnumItems())
                if (dir != WindDirections8.Calm && dir != WindDirections8.Undefined && dir != WindDirections8.Variable)
                    Kms += expectancy[dir] * ms_classes[dir];

            double k0 = K0 / Kms;
            result.BeginChange();
            foreach (RawItem item in ms_range)
            {
                RawItem ni = item.Clone();
                ni.Speed = item.Speed * k0;
                result.Add(ni);
            }
            result.EndChange();
            return result;
        }
    }
}
