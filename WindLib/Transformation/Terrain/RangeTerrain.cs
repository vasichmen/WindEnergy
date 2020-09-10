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
using GMap.NET;
using CommonLibLib.Data.Interfaces;
using CommonLib.Geomodel;
using System.Drawing;
using WindEnergy.WindLib.Operations.Interpolation;

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
            RawRange result = new RawRange();
            result.Position = param.PointCoordinates;
            result.Name = $"Ряд в точке {param.PointCoordinates.ToString(3)}";
            result.BeginChange();
            switch (param.TerrainType)
            {
                case TerrainType.Macro:
                   Dictionary<WindDirections8, double> k0 = getTerrainMacroK0(param.MSClasses, param.PointClasses); //получаем коэффициенты для плоского рельефа

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
                case TerrainType.Meso:
                    //мезоклиматический коэффициент
                    double Km = (param.MesoclimateCoefficient.Value.From + param.MesoclimateCoefficient.Value.To) / 2d; //среднее внутри диапазона

                    //выбор преобладающих направлений ветра 
                    List<WindDirections8> dominants = GetRhumbsDominants(Range);

                    //пересчет ряда только для преобладающих направлений
                    foreach (RawItem item in Range)
                    {
                        RawItem ni = item.Clone();
                        ni.Speed = dominants.Contains(ni.DirectionRhumb8) ? ni.Speed * Km : ni.Speed;
                        result.Add(ni);
                    }
                    break;
                case TerrainType.Micro:
                    //выбор преобладающих направлений ветра 
                    List<WindDirections8> directs = GetRhumbsDominants(Range);

                    //пересчет ряда только для преобладающих направлений
                    foreach (RawItem item in Range)
                    {
                        double Kmk = selectMicroclimateCoefficient(item.Speed, param.MicroclimateCoefficient, param.AtmosphereStratification);
                        RawItem ni = item.Clone();
                        ni.Speed = directs.Contains(ni.DirectionRhumb8) ? ni.Speed * Kmk : ni.Speed;
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
        /// <param name="speed">скорость ветра </param>
        /// <param name="microclimateCoefficient"></param>
        /// <param name="atmosphereStratification"></param>
        /// <returns></returns>
        private static double selectMicroclimateCoefficient(double speed, MicroclimateItemInfo microclimateCoefficient, AtmosphereStratification atmosphereStratification)
        {
            Diapason<double> diap;
            double res;
            if (speed <= 5)
            {
                diap = atmosphereStratification == AtmosphereStratification.Stable ? microclimateCoefficient.Stable_3_5 : microclimateCoefficient.Unstable_3_5;
                res = LinearInterpolateMethod.LinearInterpolation(3, 5, diap.From, diap.To, speed);
            }
            else
            {
                diap = atmosphereStratification == AtmosphereStratification.Stable ? microclimateCoefficient.Stable_6_20 : microclimateCoefficient.Unstable_6_20;
                res = LinearInterpolateMethod.LinearInterpolation(6, 20, diap.From, diap.To, speed);
            }

            if (res < 0)
                throw new Exception("Отрицательный коэффициент после интерполяции");
            return res;
        }


        /// <summary>
        /// расчет коэффициента k0 для макрорельефа
        /// </summary>
        /// <param name="msClasses"></param>
        /// <param name="pointClasses"></param>
        /// <returns></returns>
        private static Dictionary<WindDirections8, double> getTerrainMacroK0(Dictionary<WindDirections8, double> msClasses, Dictionary<WindDirections8, double> pointClasses)
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

        /// <summary>
        /// получить преобладающие направления ветра 
        /// </summary>
        /// <param name="ms_range"></param>
        /// <param name="expectancy"></param>
        /// <returns></returns>
        public static List<WindDirections8> GetRhumbsDominants(RawRange ms_range, StatisticalRange<WindDirections8> expectancy = null)
        {
            List<WindDirections8> res = new List<WindDirections8>();
            expectancy = expectancy ?? StatisticEngine.GetDirectionExpectancy(ms_range, GradationInfo<WindDirections8>.Rhumb8Gradations); //повторяемости скорости ветра по 8 румбам
            var maxes = expectancy.Values.OrderByDescending((d) => d).ToList();
            res.Add((WindDirections8)expectancy.Keys[expectancy.Values.IndexOf(maxes[0])]); //первый всегда добавляем
            if (maxes[0] > 0.3 && maxes[0] - maxes[1] < 0.1) //второй добавляем если первый больше 30% и у второго отрыв меньше 10%
                res.Add((WindDirections8)expectancy.Keys[expectancy.Values.IndexOf(maxes[1])]);
            return res;
        }

        /// <summary>
        /// выбор текста для рекомендаций, какой тип рельефа лучше использовать
        /// </summary>
        /// <param name="range"></param>
        /// <param name="pointCoordinates"></param>
        /// <param name="geoinfo"></param>
        /// <returns></returns>
        public static Dictionary<string, Color> GetRecommendation(RawRange range, PointLatLng pointCoordinates, IGeoInfoProvider geoinfo)
        {
            if (range.Position.IsEmpty)
                throw new WindEnergyException("Не заданы координаты метеостанции");
            if (pointCoordinates.IsEmpty)
                throw new WindEnergyException("Не заданы координаты точки ВЭС");
            if (geoinfo == null)
                throw new ArgumentNullException(nameof(geoinfo));

            Dictionary<string, Color> result = new Dictionary<string, Color>();
            double L = EarthModel.CalculateDistance(range.Position, pointCoordinates) / 1000; //расстояние вкилометрах между МС и точкой ВЭС
            double Hms = geoinfo.GetElevation(range.Position); //высота точки МС
            double Hpoint = geoinfo.GetElevation(pointCoordinates); //высота точки ВЭС
            double Habs = Math.Max(Hms, Hpoint); //максимальная высота на у.м.
            double dH = Math.Abs(Hpoint - Habs); //перепад высот между МС и ВЭС
            StatisticalRange<WindDirections8> expectancy = StatisticEngine.GetDirectionExpectancy(range, GradationInfo<WindDirections8>.Rhumb8Gradations); //повторяемости скорости ветра по 8 румбам
            var maxes = expectancy.Values.OrderByDescending((d) => d).ToList();

            if (maxes[0] < 0.2) //если все румбы меньше 20%, то нет преобладающего направления
                result.Add("Нет преобладающего направления ветра", Color.Black);
            else//выбор преобладающих направлений ветра
            {
                var dirs = GetRhumbsDominants(range, expectancy);
                string ln = dirs.Aggregate("", (str, dir) => str + $"{dir.Description()} ({Math.Round(expectancy[dir] * 100, 2)}%), ").Trim(new char[] { ' ', ',' });
                result.Add("Преобладающие направления ветра: " + ln, Color.Black);
            }

            if (Hms > 750)
                result.Add($"Высота точки МС больше 750 м над у. м. ({Hms:0.0} м), не рекомендуеся выполнять преобразование", Color.Red);
            else if (Hpoint > 750)
                result.Add($"Высота точки ВЭС больше 750 м над у. м. ({Hpoint:0.0} м), не рекомендуеся выполнять преобразование", Color.Red);
            else
            {
                if (L > 50 && dH <= 750 && Habs <= 750) //макрорельеф
                    result.Add($"Рекомендуется выбрать макрорельеф (L={L:0.0} км, Δh={dH:0.0} м)", Color.Green);
                if (L > 3 && L <= 50 && dH <= 750 && Habs <= 750) //мезорельеф
                    result.Add($"Рекомендуется выбрать мезорельеф (L={L:0.0} км, Δh={dH:0.0} м)", Color.Green);
                if (L <= 3 && dH <= 80 && Habs <= 750) //микрорельеф
                    result.Add($"Рекомендуется выбрать микрорельеф (L={L:0.0} км, Δh={dH:0.0} м)", Color.Green);
            }

            return result;
        }
    }
}
