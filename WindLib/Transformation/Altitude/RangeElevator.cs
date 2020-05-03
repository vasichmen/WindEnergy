using CommonLib;
using CommonLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindLib;

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
        public static void ProcessRange(RawRange Range, ElevatorParameters param, Action<int> actionPercent, Action<RawRange, SuitAMSResult> actionAfter)
        {
            //найти подходящую АМС
            //получить из неё параметры m по месяцам
            //поднять ряд Range на высоту


            //выбор варианта расчета m: через БД АМС или введенный вручную
            Dictionary<Months, double> coeffs = null;
            AMSMeteostationInfo AMS = null;
            SuitAMSResult AMSanswer = null;
            switch (param.HellmanCoefficientSource)
            {
                case HellmanCoefficientSource.AMSAnalog:
                    AMSanswer = AMSSupport.GetSuitAMS(Range, param.Coordinates, Vars.AMSMeteostations, param.SearchRaduis);
                    AMS = AMSanswer.AMS;
                    if (AMS == null)
                        throw new WindEnergyException("Не удалось найти подходящую АМС в заданном радиусе или длина исходного ряда слишком мала.\r\nПопробуйте увеличить радиус поиска или возьмите более длинный ряд.");
                    coeffs = AMS.m; ;
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
                actionAfter.Invoke(res, AMSanswer);
            });
            tsk.Start();
            return;
        }
    }
}
