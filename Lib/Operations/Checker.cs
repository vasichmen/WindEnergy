using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Limits;
using WindEnergy.Lib.Operations.Structures;

namespace WindEnergy.Lib.Operations
{
    /// <summary>
    /// проверка и устранение ошибок в ряде
    /// </summary>
    public static class Checker
    {
        /// <summary>
        /// проверить ряд и устранить ошибки
        /// </summary>
        /// <param name="range">ряд</param>
        /// <param name="param">параметры обработки ошибок</param>
        /// <param name="info">результаты проверки ряда</param>
        /// <param name="action">действие, при изменении провенца выполнения</param>
        /// <returns></returns>
        public static RawRange ProcessRange(RawRange range, CheckerParameters param, out CheckerInfo info, Action<int> action = null)
        {
            ILimitsProvider provider;
            switch (param.LimitsProvider)
            {
                case LimitsProviders.Manual:
                    if ((param.Coordinates.IsEmpty || param.LimitsProvider == LimitsProviders.None) && param.CheckByPos)
                        throw new ArgumentException("не хватает данных для проверки ряда");
                    else
                        provider = new ManualLimits(param.DirectionInclude, param.SpeedInclude);
                    break;
                case LimitsProviders.StaticLimits:
                    provider = new StaticRegionLimits(Vars.Options.StaticRegionLimitsSourceFile);
                    break;
                default: throw new Exception("Этот провайдер не реализован");

            }

            RawRange res = new RawRange();
            res.Position = range.Position;
            res.BeginChange();
            List<DateTime> dates = new List<DateTime>();
            int lims = 0, repeats = 0, other = 0;
            double c = 0;
            foreach (RawItem item in range)
            {
                c++;
                if (Math.IEEERemainder(c, 100) == 0 && action != null)
                    action.Invoke((int)((c / range.Count) * 100));
                bool accept = provider.CheckItem(item, param.Coordinates); //проверка по диапазону
                if (!accept) lims++;

                if (accept) //если всё ещё подходит, то проверяем дату
                {
                    accept &= !dates.Contains(item.Date); //проверка повтора даты
                    if (!accept) repeats++;
                }
                if (accept)//если всё ещё подходит, то проверем значения скорости и направления (если не штиль, не переменное направление и не неопределённое и скорость равна 0 то не подходит)
                    if (item.DirectionRhumb != WindDirections.Undefined && 
                        item.DirectionRhumb != WindDirections.Variable && 
                        item.DirectionRhumb != WindDirections.Calm && 
                        item.Speed == 0)
                    {
                        accept = false;
                        other++;
                    }

                if (accept)
                {
                    res.Add(item);
                    dates.Add(item.Date);
                }
            }
            res.EndChange();
            info = new CheckerInfo() { DateRepeats = repeats, OtherErrors = other, OverLimits = lims, Remain = res.Count, Total = range.Count };
            return res;
        }


    }
}
