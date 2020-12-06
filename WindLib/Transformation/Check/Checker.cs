using GMap.NET;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
        private object lockerIncrement = new object();
        private object lockerLims = new object();
        private object lockerRepeats = new object();
        private object lockerOther = new object();
        private int lims = 0, repeats = 0, other = 0, totalCount = 0;
        private double c = 0;
        private ConcurrentBag<RawItem> resultCollection = new ConcurrentBag<RawItem>();
        private ConcurrentDictionary<double, byte> dates = new ConcurrentDictionary<double, byte>();
        private Action<double> action = null;
        private ILimitsProvider provider;
        private PointLatLng coordinates = PointLatLng.Empty;

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

            Parallel.ForEach(range, new ParallelOptions(), (item) =>
                {
                    incrementCount();
                    bool accepted = checkItem(item);
                    item = restoreItem(item);
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

        private RawItem restoreItem(RawItem item)
        {
            //если скорость ноль, но направление Штиль
            if (item.Speed == 0)
                item.DirectionRhumb = WindDirections16.Calm;
            return item;
        }

        private bool checkItem(RawItem item)
        {
            bool accept = provider.CheckItem(item); //проверка по диапазону
            if (!accept) incrementLims();

            if (accept) //если всё ещё подходит, то проверяем дату
            {
                accept &= !dates.ContainsKey(item.DateArgument); //проверка повтора даты
                if (!accept) incrementRepeats();
            }
            if (item.DirectionRhumb == WindDirections16.Undefined ||
               item.DirectionRhumb == WindDirections16.Variable)
            {
                incrementOther();
                accept = false;
            }


            if (accept)
                dates.TryAdd(item.DateArgument, 0);

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
