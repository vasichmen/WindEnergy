using CommonLib;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data.Providers.DB;

namespace WindEnergy.WindLib.Transformation.Altitude
{
    /// <summary>
    /// расчет и получение подходящих значений коэффициентов поднятия на высоту
    /// </summary>
    class AMSSupport
    {
        /// <summary>
        /// на основе ряда наблюдений на высоте 10м подобрать АМС и выбрать ряд коэффициентов m для поднятия скорости на высоту
        /// </summary>
        /// <param name="range">ряд наблюдений на МС</param>
        /// <param name="coordinates">координаты ряда</param>
        /// <param name="MSMeteostations">БД АМС</param>
        /// <param name="searchRadius">расстояние для фильтрации АМС в метрах. Если задано NaN, то фильтрация по расстоянию проводить не будет</param>
        /// <returns></returns>
        internal static AMSMeteostationInfo GetSuitAMS(RawRange range, PointLatLng coordinates, AMSMeteostationDatabase MSMeteostations,double searchRadius)
        {
            //посчитать среднемесячные скорости на МС
            //посчитать относительные скорости для МС
            //выбрать АМС из заданного радиуса
            //посчитать относительные скорости на всех АМС
            //найти наиболее подходящую АМС по наименьшему среднеквадратичному отклонению относительных скоростей


            //относительные скорости на МС
            double rAverage = range.Average((item) => { return item.Speed; }); //средняя скорость во всем ряде на МС
            Dictionary<Months, double> msRelatives = new Dictionary<Months, double>(); //относительные скорости по месяцам на МС
            for (int m = 1; m <= 12; m++)
            {
                Months month = (Months)m;
                double mAverage = (from t in range
                                   where t.Date.Month == m
                                   select t.Speed).Average();
                msRelatives.Add(month, mAverage / rAverage);//относительная скорость = среднемесячная скорость / средняя скорость ряда
            }

            //выбор АМС в заданном радиусе
            List<AMSMeteostationInfo> amss = double.IsNaN(searchRadius)? MSMeteostations.List: MSMeteostations.GetNearestMS(coordinates, searchRadius, true); //выбираем все АМС в радиусе 1000 км

            //поиск АМС с минимальным среднеквадр отклонением относительных скоростей
            double min = double.MaxValue;
            AMSMeteostationInfo min_ams = null;
            foreach (AMSMeteostationInfo ams in amss)
            {
                Dictionary<Months, double> amsRelatives = ams.RelativeSpeeds;
                double delta = Math.Sqrt(msRelatives.Average((kv) => { return Math.Pow(kv.Value - amsRelatives[kv.Key], 2); })); //корень(среднее ((KjМС - KjАМС)^2)), j - номер месяца

                if (delta < min)
                {
                    min = delta;
                    min_ams = ams;
                }
            }

            return min_ams;
        }
    }
}
