using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Transformation.Altitude
{
    /// <summary>
    /// основной класс поднятия рядов на высоту
    /// </summary>
    public static class RangeElevator
    {
        /// <summary>
        /// поднять ряд на высоту с заданными настройками
        /// </summary>
        /// <param name="Range">поднимаемый ряд</param>
        /// <param name="param">настройки</param>
        /// <param name="actionPercent">действие при изменении процента выполнения</param>
        /// <param name="actionAfter">действие по окончании выполнения</param>
        public static void ProcessRange(RawRange Range, ElevatorParameters param, Action<int> actionPercent, Action<RawRange, SuitAMSResultItem> actionAfter)
        {
            //найти подходящую АМС
            //получить из неё параметры m по месяцам
            //поднять ряд Range на высоту


            //выбор варианта расчета m: через БД АМС или введенный вручную
            Dictionary<Months, double> coeffs = null;
            switch (param.HellmanCoefficientSource)
            {
                case HellmanCoefficientSource.AMSAnalog:
                    coeffs = getAMSAnalogCoefficients(param.SelectedAMS.AMS, Range);
                    break;
                case HellmanCoefficientSource.CustomMonths:
                    coeffs = param.CustomNCoefficientMonths;
                    break;
                case HellmanCoefficientSource.CustomOne:
                    coeffs = new Dictionary<Months, double>();
                    for (int i = 1; i <= 12; i++) coeffs.Add((Months)i, param.CustomMCoefficient);
                    break;
                default: throw new Exception("Этот источник данных не реализован");
            }

            coeffs = coeffs ?? throw new ArgumentException("Недопустимые настройки, коэффициенты пересчета на высоту не получены");

            //поднять ряд  с учетом m по месяцам
            RawRange res = new RawRange();
            res.Position = Range.Position;
            res.BeginChange();
            Task tsk = new Task(() =>
            {
                double c = 0;
                foreach (RawItem item in Range)
                {
                    c++;
                    if (Math.IEEERemainder(c, 100) == 0 && actionPercent != null)
                        actionPercent.Invoke((int)((c / Range.Count) * 100));
                    Months month = (Months)item.Date.Month;
                    double m = coeffs[month];
                    double oldV = item.Speed;
                    double newV = oldV * Math.Pow(param.ToHeight / param.FromHeight, m);
                    RawItem ni = item.Clone();
                    ni.Speed = newV;
                    res.Add(ni);
                }
                res.EndChange();
                actionAfter.Invoke(res, param.SelectedAMS);
            });
            tsk.Start();
        }

        /// <summary>
        /// возвращает коэффициенты m для каждого месяца по заданной модели АМС
        /// </summary>
        /// <param name="meteostation">модель АМС</param>
        /// <param name="range">ряд данных</param>
        /// <returns></returns>
        static Dictionary<Months, double> getAMSAnalogCoefficients(AMSMeteostationInfo meteostation, RawRange range)
        {
            //средняя скорость по месяцам
            Dictionary<Months, double> averSpeeds = new Dictionary<Months, double>();
            Dictionary<Months, double> result = new Dictionary<Months, double>();
            for (int m = 1; m <= 12; m++)
            {
                Months month = (Months)m;
                var r = from t in range
                        where t.Date.Month == m
                        select t.Speed;

                double mAverage = double.NaN;
                if (r.Count() == 0)
                    mAverage = 0;
                else mAverage = r.Average();

                //расчет коэффициентов m для каждого месяца по модели АМС: m=a*Vcp^(-b)
                //у нас уже приходят b<0, поэтому умножать на -1 не надо
                double mCoeff = meteostation.a * Math.Pow(mAverage, meteostation.b);

                //если скорость меньше 2мс и m получился больше 0.5, то m=0.5
                if (mAverage <= 2 && mCoeff > 0.5)
                    result.Add(month, 0.5);
                else
                    result.Add(month, mCoeff);
            }

            return result;
        }
    }
}
