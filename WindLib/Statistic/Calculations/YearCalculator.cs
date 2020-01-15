using CommonLib.Classes;
using CommonLib.Classes.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Operations.Structures;
using WindEnergy.WindLib.Statistic.Collections;
using WindEnergy.WindLib.Statistic.Structures;

namespace WindEnergy.WindLib.Statistic.Calculations
{
    /// <summary>
    /// класс поиска расчётного года
    /// </summary>
    public static class YearCalculator
    {
        /// <summary>
        /// обработка ряда и получение расчётного года
        /// </summary>
        /// <param name="Range"></param>
        /// <returns></returns>
        public static CalculateYearInfo ProcessRange(RawRange Range)
        {
            if (Range == null || Range.Count < 3)
                throw new ArgumentException("Ряд не может быть null или длиной меньше трёх наблюдений");
            List<RawItem> range = new List<RawItem>(Range);
            range.Sort(new DateTimeComparerRawItem());
            if (range[range.Count - 1].Date - range[0].Date < TimeSpan.FromDays(366))
                throw new ArgumentException("Ряд должен быть длиной больше одного года");
            

            //среднемноголетняя скорость
            double averSpeed = range.Average((i) => i.Speed);

            if (double.IsNaN(averSpeed))
                throw new WindEnergyException("Ряд содержит недопустимые для расчета значения скорости");

            //разделяем исходный ряд по годам
            var years = range.GroupBy((i) => i.Date.Year).ToList().ConvertAll((gr) => new RawRange(gr.ToList())); 

            //ОБРАБОТКА ВСЕХ РЯДОВ И ПОЛУЧЕНИЕ ИНФОРМАЦИИ
            CalculateYearInfo res = new CalculateYearInfo();
            res.AverageSpeed = averSpeed;
            res.Range = Range;
            foreach (RawRange r in years)
            {
                QualityInfo qinfo = Qualifier.ProcessRange(r);
                
                //если null, то ряд очень маленький или не удалось расчитать параметры
                if (r == null || r[r.Count - 1].Date - r[0].Date < TimeSpan.FromDays(364))
                    continue;

                StatisticalRange<GradationItem> exp = StatisticEngine.GetExpectancy(range, Vars.Options.CurrentSpeedGradation);
                DeviationsInfo dinfo = StatisticEngine.ProcessRangeDeviations(r, averSpeed, exp);

                double aver = r.Average((t) => t.Speed);
                SinglePeriodInfo spinf = new SinglePeriodInfo()
                {
                    Interval = qinfo.Intervals.Count == 1 ? qinfo.Intervals[0].Interval : StandartIntervals.Variable,
                    Completness = qinfo.Completeness * 100,
                    Vmax = r.Max((t) => t.Speed),
                    Year = r[0].Date.Year,
                    From = r[0].Date,
                    To = r[r.Count - 1].Date,
                    SpeedDeviation = dinfo.SpeedDeviation,
                    ExpectancyDeviation = dinfo.ExpDeviation,
                    AverageSpeed = aver,
                    SpeedDeviationPercent = (dinfo.SpeedDeviation / averSpeed) * 100d
                };
                res.Years.Add(spinf);
            }

            //ВЫБОР РАСЧЕТНОГО ГОДА
            //получаем года с одинаковым интервалом и полнотой больше 95%
            List<SinglePeriodInfo> accepts = (from t in res.Years where t.Interval != StandartIntervals.Variable && t.Interval != StandartIntervals.Missing && t.Completness > 95 select t).ToList();

            //если даже таких годов не нашлось, то выходим
            if (accepts.Count == 0)
                return res;

            //если только один год остался, то его и оставляем
            if (accepts.Count == 1)
            {
                res.RecomendedYear = accepts[0];
                return res;
            }

            //проверка по отклонению скорости
            var ac = accepts.OrderBy((t) => t.SpeedDeviation).ToList(); //в начале ряда остаются минимальные отклонения скорости
            double startsDev = ac[0].SpeedDeviation; //запоминаем сааамое мальнкое отклонение (с ним сравниваем остальные, чтоб найти похожие)
            var ac2 = ac.TakeWhile(e => e.SpeedDeviation - startsDev < Vars.Options.MinimalSpeedDeviation).ToList(); //остаются только ряды с очень похожим отклонением скоростей


            if (ac2.Count == 1) //если остался только один, то его оставляем
            {
                res.RecomendedYear = ac2[0];
                return res;
            }

            //проверка по отклонению повторяемости
            var ac3 = ac2.OrderBy((t) => t.ExpectancyDeviation); //выбираем минимальное отклонение по повторяемости
            res.RecomendedYear = ac3.ToList()[0];

            return res;
        }
    }
}
