using CommonLib.Geomodel;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data.Providers.DB;

namespace WindEnergy.WindLib.Transformation.Altitude
{
    /// <summary>
    /// расчет и получение подходящих значений коэффициентов поднятия на высоту
    /// </summary>
    public static class AMSSupport
    {
        /// <summary>
        /// на основе ряда наблюдений на высоте 10м подобрать АМС и выбрать ряд коэффициентов m для поднятия скорости на высоту
        /// </summary>
        /// <param name="range">ряд наблюдений на МС</param>
        /// <param name="coordinates">координаты ряда</param>
        /// <param name="MSMeteostations">БД АМС</param>
        /// <param name="searchRadius">расстояние для фильтрации АМС в метрах. Если задано NaN, то фильтрация по расстоянию проводить не будет</param>
        /// <param name="maximalRelativeSpeedDeviation">максимальное среднеквадратичное отклонение относительной скорости. NaN, если не надо учитывать</param>
        /// <returns></returns>
        public static SuitAMSResult GetSuitAMS(RawRange range, PointLatLng coordinates, AMSMeteostationDatabase MSMeteostations, double searchRadius, double maximalRelativeSpeedDeviation)
        {
            //посчитать среднемесячные относительные скорости для МС
            //выбрать АМС из заданного радиуса
            //посчитать относительные скорости на всех АМС
            //найти наиболее подходящую АМС по наименьшему среднеквадратичному отклонению относительных скоростей

            //относительные среднемесячные скорости на МС
            Dictionary<Months, double> msRelatives = getRelativeAverageMonthSpeeds(range, out bool allMonth);

            //выбор АМС в заданном радиусе
            List<AMSMeteostationInfo> amss = double.IsNaN(searchRadius) ? MSMeteostations.List : MSMeteostations.GetNearestMS(coordinates, searchRadius, true); //выбираем все АМС в радиусе 

            if (amss == null)
                return null;

            //поиск АМС с минимальным среднеквадр отклонением относительных скоростей
            SuitAMSResult res = new SuitAMSResult();
            foreach (AMSMeteostationInfo ams in amss)
            {
                SuitAMSResultItem item = new SuitAMSResultItem();
                item.Deviation = Math.Sqrt(msRelatives.Average((kv) => { return Math.Pow(kv.Value - ams.RelativeSpeeds[kv.Key], 2); })); //корень(среднее ((KjМС - KjАМС)^2)), j - номер месяца
                item.AMS = ams;
                item.AllMonthInRange = allMonth;
                item.IsDeviationFailed = !double.IsNaN(maximalRelativeSpeedDeviation) && item.Deviation > maximalRelativeSpeedDeviation;
                item.Distance = EarthModel.CalculateDistance(coordinates, ams.Position);
                res.Add(item);
            }

            res.AllMonthInRange = allMonth;
            res.RangeRelativeSpeeds = msRelatives;
            return res;
        }

        /// <summary>
        /// возвращает относительные среднемесячные скорости по месяцам для многолетнего ряда
        /// </summary>
        /// <param name="range"></param>
        /// <param name="allMonth">истина, если в ряде представлены не все месяцы года</param>
        /// <returns></returns>
        private static Dictionary<Months, double> getRelativeAverageMonthSpeeds(RawRange range, out bool allMonth)
        {
            double rAverage = range.Average((item) => { return item.Speed; }); //средняя скорость во всем ряде на МС
            Dictionary<Months, double> msRelatives = new Dictionary<Months, double>(); //относительные скорости по месяцам на МС
            allMonth = true;
            for (int m = 1; m <= 12; m++)
            {
                Months month = (Months)m;
                double mAverage = double.NaN;
                var r = from t in range
                        where t.Date.Month == m
                        select t.Speed;
                if (r.Count() == 0)
                {
                    allMonth = false;
                    mAverage = 0;
                }
                else mAverage = r.Average();
                msRelatives.Add(month, mAverage / rAverage);//относительная скорость = среднемесячная скорость / средняя скорость ряда
            }
            return msRelatives;
        }
    }
}
