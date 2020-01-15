using CommonLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Statistic.Structures
{
    /// <summary>
    /// 
    /// Тип T - тип данных ряда (windDirections, GradationItem)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GradationInfo<T>
    {
        /// <summary>
        /// коллекция диапазонов-градаций
        /// </summary>
        public List<object> Items => items;

        private List<object> items;

        private GradationInfo()
        {
            items = new List<object>();
            if (typeof(T) == typeof(WindDirections))
            {
                var r = WindDirections.Calm.GetEnumItems();
                var ad = from x in r
                         where ((WindDirections)x) != WindDirections.Calm && ((WindDirections)x) != WindDirections.Undefined && ((WindDirections)x) != WindDirections.Variable
                         select x;
                items.AddRange(ad);
            }
        }

        /// <summary>
        /// новые градации в указанном диапазоне с указанным шагом
        /// </summary>
        /// <param name="from"></param>
        /// <param name="step"></param>
        /// <param name="to"></param>
        public GradationInfo(double from, double step, double to)
        {
            if (to == double.PositiveInfinity)
                throw new ArgumentOutOfRangeException("Диапазон градаций должен быть конечным числом");

            items = new List<object>();
            for (double i = from; i < to; i += step)
                items.Add(new GradationItem(i, i + step));
        }

        /// <summary>
        /// стандартные диапазоны скорости по ГГО им. А.И. Воейкова
        /// </summary>
        public static GradationInfo<GradationItem> VoeykowGradations
        {
            get
            {
                return new GradationInfo<GradationItem>()
                {
                    items = new List<object>() {
                        new GradationItem(0, 1.5),
                        new GradationItem(1.5, 3.5),
                        new GradationItem(3.5, 5.5),
                        new GradationItem(5.5, 7.5),
                        new GradationItem(7.5, 9.5),
                        new GradationItem(9.5, 11.5),
                        new GradationItem(11.5, 13.5),
                        new GradationItem(13.5, 15.5),
                        new GradationItem(15.5, 17.5),
                        new GradationItem(17.5, 20.5),
                        new GradationItem(20.5, 24.5),
                        new GradationItem(24.5, 28.5),
                        new GradationItem(28.5, 34.5),
                        new GradationItem(34.5, 36.75)
                    }
                };
            }
        }

        /// <summary>
        /// стандартные диапазоны скорости по шкале Бофорта
        /// </summary>
        public static GradationInfo<GradationItem> BofortGradations
        {
            get
            {
                return new GradationInfo<GradationItem>()
                {
                    items = new List<object>() {
                        new GradationItem(0, 0.2),
                        new GradationItem(0.2, 1.5),
                        new GradationItem(1.5, 3.3),
                        new GradationItem(3.3, 5.4),
                        new GradationItem(5.4, 7.9),
                        new GradationItem(7.9, 10.7),
                        new GradationItem(10.7, 13.8),
                        new GradationItem(13.8, 17.1),
                        new GradationItem(17.1, 20.7),
                        new GradationItem(20.7, 24.4),
                        new GradationItem(24.4, 28.4),
                        new GradationItem(28.4, 32.6)
                    }
                };
            }
        }

        /// <summary>
        /// стандартные диапазоны скорости по NASA
        /// </summary>
        public static GradationInfo<GradationItem> NASAGradations
        {
            get
            {
                return new GradationInfo<GradationItem>()
                {
                    items = new List<object>() {
                        new GradationItem(0, 2.5),
                        new GradationItem(2.5, 6.5),
                        new GradationItem(6.5, 10.5),
                        new GradationItem(10.5, 14.5),
                        new GradationItem(14.5, 18.5),
                        new GradationItem(18.5, 35),
                        new GradationItem(35, 60)
                    }
                };
            }
        }

        /// <summary>
        /// градации по 16 румбам
        /// </summary>
        public static GradationInfo<WindDirections> Rhumb16Gradations
        {
            get
            {
                return new GradationInfo<WindDirections>();
            }
        }

        /// <summary>
        /// получить соответствующую значению градацию типа Т
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        internal object GetItem(double val)
        {
            if (double.IsNaN(val))
                throw new ArgumentException("Заданное значение не является числом!");

            if (typeof(T) == typeof(GradationItem))
            {
                foreach (var v in items)
                    if (val >= (v as GradationItem).From && val <= (v as GradationItem).To)
                        return v;
                var max = double.MinValue;
                GradationItem mgi = null;
                foreach (GradationItem gi in items)
                    if (gi.To > max) { max = gi.To; mgi = gi; }
                if (mgi != null)
                    return mgi;
                else
                    return GradationItem.Empty;
            }
            else if (typeof(T) == typeof(WindDirections))
            { return new RawItem() { Direction = val }.DirectionRhumb; }
            else
                throw new Exception("Этот тип не реализован");


        }

        /// <summary>
        /// преобразование градаций этого типа в GradationItem
        /// </summary>
        /// <returns></returns>
        internal GradationInfo<GradationItem> ToGradationItem()
        {
            if (typeof(T) == typeof(GradationItem))
                return this as GradationInfo<GradationItem>;
            if (typeof(T) == typeof(WindDirections))
            {
                double d = 22.5d / 2d;
                GradationInfo<GradationItem> res = new GradationInfo<GradationItem>();
                foreach (WindDirections item in this.items)
                {
                    double dir = RawItem.GetDirection(item);
                    GradationItem gr = new GradationItem(dir - d, dir + d);
                    res.items.Add(gr);
                }
                return res;
            }
            throw new WindEnergyException("Невозможно преобразовать этот тип градаций в GradationItem");
        }
    }
}
