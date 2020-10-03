using GMap.NET;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Transformation.Check.Limits;
using WindLib;

namespace WindEnergy.WindLib.Transformation.Check
{
    /// <summary>
    /// проверка и устранение ошибок в ряде
    /// </summary>
    public class Checker
    {
        object lockerIncrement = new object();
        object lockerLims = new object();
        object lockerRepeats = new object();
        object lockerOther = new object();

        int lims = 0, repeats = 0, other = 0, totalCount = 0;
        double c = 0;
        ConcurrentBag<RawItem> resultCollection = new ConcurrentBag<RawItem>();
        HashSet<double> dates = new HashSet<double>();
        Action<double> action = null;

        ILimitsProvider provider;
        PointLatLng coordinates = PointLatLng.Empty;

        /// <summary>
        /// проверить ряд и устранить ошибки
        /// </summary>
        /// <param name="range">ряд</param>
        /// <param name="param">параметры обработки ошибок</param>
        /// <param name="info">результаты проверки ряда</param>
        /// <param name="action">действие, при изменении провенца выполнения</param>
        /// <returns></returns>
        public RawRange ProcessRange(RawRange range, CheckerParameters param, out CheckerInfo info, Action<double> action = null)
        {
            this.action = action;
            this.totalCount = range.Count;

            switch (param.LimitsProvider)
            {
                case LimitsProviders.Manual:
                    if (param.Coordinates.IsEmpty && param.CheckByPos)
                        throw new ArgumentException("Ряд не содержит информацию о координатах");
                    else
                        provider = new ManualLimits(param.DirectionInclude, param.SpeedInclude);
                    break;
                case LimitsProviders.StaticLimits:
                    provider = new StaticRegionLimits(Vars.SpeedLimits.List, param.Coordinates);
                    break;
                default: throw new Exception("Этот провайдер не реализован");

            }

            Parallel.ForEach(range, (item) =>
            {
                incrementCount();
                bool accepted = checkItem(item);
                if (accepted)
                    resultCollection.Add(item);
            });

            RawRange res = new RawRange(resultCollection.OrderBy(new Func<RawItem, double>((item) => { return item.DateArgument; })))
            {
                Position = range.Position,
                Meteostation = range.Meteostation,
                FilePath = range.FilePath,
                Name = "Исправленный ряд. " + range.Name
            };

            info = new CheckerInfo()
            {
                DateRepeats = repeats,
                OtherErrors = other,
                OverLimits = lims,
                Remain = res.Count,
                Total = range.Count
            };
            return res;
        }

        private bool checkItem(RawItem item)
        {
            bool accept = provider.CheckItem(item); //проверка по диапазону
            if (!accept) incrementLims();

            if (accept) //если всё ещё подходит, то проверяем дату
            {
                accept &= !dates.Contains(item.DateArgument); //проверка повтора даты
                if (!accept) incrementRepeats();
            }
            if (accept)//если всё ещё подходит, то проверем значения скорости и направления (если не штиль, не переменное направление и не неопределённое и скорость равна 0 то не подходит)
                if (item.DirectionRhumb != WindDirections16.Undefined &&
                    item.DirectionRhumb != WindDirections16.Variable &&
                    item.DirectionRhumb != WindDirections16.Calm &&
                    item.Speed == 0)
                {
                    accept = false;
                    incrementOther();
                }

            if (accept)
                dates.Add(item.DateArgument);

            return accept;
        }

        private void incrementCount()
        {
            c++;
            lock (lockerIncrement)
            {
                if (Math.IEEERemainder(c, 100) == 0 && action != null)
                    action.Invoke((c / totalCount) * 100);
            }
        }

        private void incrementOther()
        {
            other++;
        }

        private void incrementLims()
        {
            lims++;
        }

        private void incrementRepeats()
        {
            repeats++;
        }
    }
}
