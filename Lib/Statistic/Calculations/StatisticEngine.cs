﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Statistic.Collections;
using WindEnergy.Lib.Statistic.Structures;

namespace WindEnergy.Lib.Statistic.Calculations
{
    /// <summary>
    /// статистическая обработка рядов
    /// </summary>
    public static class StatisticEngine
    {
        /// <summary>
        /// плотность воздуха кг/м3
        /// </summary>
        private const double AIR_DENSITY = 1.226;

        /// <summary>
        /// обработать ряд и получить характеристики по всему ряду
        /// </summary>
        /// <param name="tempr"></param>
        /// <returns></returns>
        public static EnergyInfo ProcessRange(RawRange tempr)
        {
            EnergyInfo res = new EnergyInfo();
            res.FromDate = tempr[0].Date;
            res.ToDate = tempr[tempr.Count - 1].Date;
            res.PowerDensity = getAveragePower(tempr);
            res.V0 = getAverageSpeed(tempr);
            res.StandardDeviation = getSigm(res.V0, tempr);
            res.Vmax = getMaxSpeed(tempr);
            res.EnergyDensity = res.PowerDensity * 8760d;
            res.Cv = res.StandardDeviation / res.V0;
            return res;
        }

        #region служебные



        /// <summary>
        /// максимальная скорость
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double getMaxSpeed(RawRange input)
        {
            double res = int.MinValue;
            foreach (var l in input)
                if (l.Speed > res)
                    res = l.Speed;
            return res;
        }

        /// <summary>
        /// удельная мощность
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double getAveragePower(RawRange input)
        {
            double sum = 0;
            foreach (var l in input)
            {
                sum += 0.5d * AIR_DENSITY * Math.Pow(l.Speed, 3);
            }
            return sum / (input.Count);
        }

        /// <summary>
        /// среднеквадратичное отклонение
        /// </summary>
        /// <param name="average"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double getSigm(double average, RawRange input)
        {
            double sum = 0;
            foreach (var l in input)
            {
                sum += Math.Pow(l.Speed - average, 2);
            }
            return Math.Sqrt(sum / (input.Count - 1));

        }

        /// <summary>
        /// средняя скорость в указанном ряде данных
        /// </summary>
        /// <param name="input"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private static double getAverageSpeed(RawRange input, int year = -1)
        {
            if (year == -1)
            {
                double sum = 0;
                foreach (var l in input)
                    sum += l.Speed;
                return sum / input.Count;
            }
            else
            {
                double sum = 0;
                int c = 0;
                foreach (var l in input)
                    if (l.Date.Year == year)
                    {
                        sum += l.Speed;
                        c++;
                    }
                return sum / c;
            }
        }

        #endregion

        /// <summary>
        /// получить характеристики по заданным градациям
        /// </summary>
        /// <param name="speeds"></param>
        /// <returns></returns>
        public static EnergyInfo ProcessRange(StatisticalRange<GradationItem> speeds)
        {
            List<GradationItem> grads = speeds.Gradation.Items.ConvertAll((r) => (GradationItem)r);
            List<double> expect = speeds.Values;
            if (grads.Count != expect.Count)
                throw new Exception("что-то опять не так");


            //средняя скорость:  сумма произведений скорости градации на вероятность
            IEnumerable<double> en = grads.Zip(expect, (g, e) => g.Average * e);
            double V0 = en.Aggregate(0d, (x, y) => x + (double.IsNaN(y) ? 0 : y));

            //удельная энергия
            double EDensity = (grads.Zip(expect, (g, e) => Math.Pow(g.Average, 3) * 0.5 * e * 8760 * AIR_DENSITY)).Aggregate((x, y) => x + (double.IsNaN(y) ? 0 : y));

            //удельная мощность
            double PDensity = EDensity / 8760d;

            //среднеквадратическое отклонение
            double sigm = Math.Sqrt((grads.Zip(expect, (g, e) => Math.Pow((g.Average - V0), 2) * e)).Aggregate((x, y) => x + (double.IsNaN(y) ? 0 : y)));

            //Cv
            double Cv = sigm/V0 ;

            return new EnergyInfo() { Cv = Cv, EnergyDensity = EDensity, PowerDensity = PDensity, StandardDeviation = sigm, V0 = V0 };
        }

        /// <summary>
        /// получить статистический ряд по заданным значениям и заданным градациям
        /// </summary>
        /// <param name="tempr"></param>
        /// <param name="voeykowGradations"></param>
        /// <returns></returns>
        public static StatisticalRange<GradationItem> GetSpeedExpectancy(RawRange tempr, GradationInfo<GradationItem> voeykowGradations)
        {
            List<double> spds = new List<double>(from t in tempr select t.Speed);
            StatisticalRange<GradationItem> r = new StatisticalRange<GradationItem>(spds, voeykowGradations);
            return r;
        }

        /// <summary>
        /// получить статистический ряд по заданным значениям и заданным градациям
        /// </summary>
        /// <param name="tempr"></param>
        /// <param name="rhumb16Gradations"></param>
        /// <returns></returns>
        public static StatisticalRange<WindDirections> GetDirectionExpectancy(RawRange tempr, GradationInfo<WindDirections> rhumb16Gradations)
        {
            List<double> spds = new List<double>(from t in tempr select t.Direction);
            StatisticalRange<WindDirections> r = new StatisticalRange<WindDirections>(spds, rhumb16Gradations);
            return r;
        }
    }
}
