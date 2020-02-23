using CommonLib;
using CommonLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Statistic.Collections;
using WindEnergy.WindLib.Statistic.Structures;
using WindLib;

namespace WindEnergy.WindLib.Statistic.Calculations
{
    /// <summary>
    /// статистическая обработка рядов
    /// </summary>
    public static class StatisticEngine
    {

        /// <summary>
        /// обработать ряд и получить характеристики по всему ряду
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static EnergyInfo ProcessRange(RawRange range)
        {
            double density = range.AirDensity;

            if (range.Count == 0)
                return null;
            EnergyInfo res = new EnergyInfo();
            res.FromDate = range[0].Date;
            res.ToDate = range[range.Count - 1].Date;
            res.PowerDensity = getAveragePower(range,density);
            res.V0 = getAverageSpeed(range);
            res.StandardDeviationSpeed = getSigmV(res.V0, range);
            res.Vmax = getMaxSpeed(range);
            res.Vmin = getMinSpeed(range);
            res.EnergyDensity = res.PowerDensity * 8760d;
            res.Cv = res.StandardDeviationSpeed / res.V0;
            res.VeybullGamma = getVeybullGamma(res.Cv);
            res.VeybullBeta = getVeybullBeta(res.V0, res.VeybullGamma);
            return res;
        }


        #region служебные

        /// <summary>
        /// получиь параметр распределения вейбулла бета
        /// </summary>
        /// <returns></returns>
        private static double getVeybullBeta(double V_average, double gamma)
        {
            double arg = 1d + 1d / gamma;
            double G = SpecialFunction.gamma(arg);
            return V_average / G;
        }

        /// <summary>
        /// получиь параметр распределения вейбулла гамма
        /// </summary>
        /// <param name="cv"></param>
        /// <returns></returns>
        private static double getVeybullGamma(double cv)
        {
            return Math.Pow(cv, -1.069);
        }


        /// <summary>
        /// максимальная скорость
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double getMaxSpeed(RawRange input)
        {
            double res = double.MinValue;
            foreach (var l in input)
                if (l.Speed > res && !double.IsNaN(l.Speed))
                    res = l.Speed;
            return res == double.MinValue ? double.NaN : res;
        }

        /// <summary>
        /// получить минимальную скорость
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double getMinSpeed(RawRange input)
        {
            double res = double.MaxValue;
            foreach (var l in input)
                if (l.Speed < res && !double.IsNaN(l.Speed))
                    res = l.Speed;
            return res == double.MaxValue ? double.NaN : res;
        }

        /// <summary>
        /// удельная мощность
        /// </summary>
        /// <param name="input"></param>
        /// <param name="density">плотность воздуха</param>
        /// <returns></returns>
        private static double getAveragePower(RawRange input, double density)
        {
            double sum = 0;
            int c = 0;
            foreach (var l in input)
            {
                if (!double.IsNaN(l.Speed))
                {
                    sum += 0.5d * density * Math.Pow(l.Speed, 3);
                    c++;
                }
            }
            double res = c==0?double.NaN: sum / c;
            return res;
        }


        /// <summary>
        /// среднеквадратичное отклонение
        /// </summary>
        /// <param name="average"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double getSigmV(double average, RawRange input)
        {
            double sum = 0;
            int c = 0;
            foreach (var l in input)
            {
                if (!double.IsNaN(l.Speed))
                {
                    sum += Math.Pow(l.Speed - average, 2);
                    c++;
                }
            }
            double res = c-1<=0?double.NaN:  Math.Sqrt(sum / (c - 1));
            return res;

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
                int c = 0;
                foreach (var l in input)
                    if(!double.IsNaN(l.Speed))
                {
                    sum += l.Speed;
                    c++;
                }
                if (c == 0) return double.NaN;
                return sum / c;
            }
            else
            {
                double sum = 0;
                int c = 0;
                foreach (var l in input)
                    if (l.Date.Year == year && !double.IsNaN(l.Speed))
                    {
                        sum += l.Speed;
                        c++;
                    }
                if (c == 0) return double.NaN;
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
            double EDensity = (grads.Zip(expect, (g, e) => Math.Pow(g.Average, 3) * 0.5 * e * 8760 * Vars.Options.AirDensity)).Aggregate((x, y) => x + (double.IsNaN(y) ? 0 : y));

            //удельная мощность
            double PDensity = EDensity / 8760d;

            //среднеквадратическое отклонение
            double sigm = Math.Sqrt((grads.Zip(expect, (g, e) => Math.Pow((g.Average - V0), 2) * e)).Aggregate((x, y) => x + (double.IsNaN(y) ? 0 : y)));

            //Cv
            double Cv = sigm / V0;

            double VeyGamma= getVeybullGamma(Cv);
            double VeyBeta = getVeybullBeta(V0, VeyGamma);

            return new EnergyInfo() { Cv = Cv, EnergyDensity = EDensity, PowerDensity = PDensity, StandardDeviationSpeed = sigm, V0 = V0, VeybullBeta=VeyBeta, VeybullGamma=VeyGamma };
        }

        /// <summary>
        /// расчёт отклонений от заданных многолетних значений
        /// </summary>
        /// <param name="r">ряд за небольшой промежуток (год)</param>
        /// <param name="averSpeed">среднемноголетняя скорость</param>
        /// <param name="exp">многолетняя повторяемость скорости ветра</param>
        /// <returns></returns>
        internal static DeviationsInfo ProcessRangeDeviations(RawRange r, double averSpeed, StatisticalRange<GradationItem> exp)
        {
            DeviationsInfo res = new DeviationsInfo();

            res.SpeedDeviation = Math.Abs(r.Average((t) => t.Speed) - averSpeed);

            StatisticalRange<GradationItem> th = GetExpectancy(r, Vars.Options.CurrentSpeedGradation);
            res.ExpDeviation = Math.Sqrt(exp.Values.Zip(th.Values, (a, b) => Math.Pow(a - b, 2)).Aggregate((x, y) => x + y)); //корень из суммы квадратов разностей повторяемостей многолетней и этого промежутка
            return res;
        }

        /// <summary>
        /// получить статистический ряд по заданным значениям и заданным градациям
        /// </summary>
        /// <param name="range"></param>
        /// <param name="gradations"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static StatisticalRange<GradationItem> GetExpectancy(IList<RawItem> range, GradationInfo<GradationItem> gradations, MeteorologyParameters parameter = MeteorologyParameters.Speed)
        {
            List<double> rang;
            switch (parameter)
            {
                case MeteorologyParameters.Speed:
                    rang = new List<double>(from t in range select t.Speed);
                    break;
                case MeteorologyParameters.Direction:
                    rang = new List<double>(from t in range select t.Direction);
                    break;
                case MeteorologyParameters.Temperature:
                    rang = new List<double>(from t in range select t.Temperature);
                    break;
                case MeteorologyParameters.Wetness:
                    rang = new List<double>(from t in range select t.Wetness);
                    break;
                default: throw new WindEnergyException("Этот параметр не реализован");
            }
            StatisticalRange<GradationItem> r = new StatisticalRange<GradationItem>(rang, gradations);
            return r;
        }

        /// <summary>
        /// получить статистический ряд по заданным значениям и заданным градациям
        /// </summary>
        /// <param name="tempr"></param>
        /// <param name="rhumb16Gradations"></param>
        /// <returns></returns>
        public static StatisticalRange<WindDirections16> GetDirectionExpectancy(RawRange tempr, GradationInfo<WindDirections16> rhumb16Gradations)
        {
            List<double> spds = new List<double>(from t in tempr select t.Direction);
            StatisticalRange<WindDirections16> r = new StatisticalRange<WindDirections16>(spds, rhumb16Gradations);
            return r;
        }

        /// <summary>
        /// получить статистический ряд по заданным значениям и заданным градациям
        /// </summary>
        /// <param name="tempr"></param>
        /// <param name="rhumb8Gradations"></param>
        /// <returns></returns>
        public static StatisticalRange<WindDirections8> GetDirectionExpectancy(RawRange tempr, GradationInfo<WindDirections8> rhumb8Gradations)
        {
            List<double> spds = new List<double>(from t in tempr select t.Direction);
            StatisticalRange<WindDirections8> r = new StatisticalRange<WindDirections8>(spds, rhumb8Gradations);
            return r;
        }
    }
}
