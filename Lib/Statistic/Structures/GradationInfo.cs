using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Structures;

namespace WindEnergy.Lib.Statistic.Structures
{
    /// <summary>
    /// 
    /// Тип T - тип данных ряда (windDirections, GradationItem)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GradationInfo<T>
    {
        private List<GradationItem> items;

        private GradationInfo() { }

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

            items = new List<GradationItem>();
            for (double i = from; i < to - step; i += step)
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
                    items = new List<GradationItem>() {
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
                        new GradationItem(34.5, 36.75),
                        new GradationItem(36.75, double.PositiveInfinity)
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
            switch (typeof(T).Name)
            {
                case "GradationItem":
                    foreach (var v in items)
                        if (val >= v.From && val < v.To)
                            return v;
                    return GradationItem.Empty;
                case "WindDirections":
                    return new RawItem() { Direction = val }.DirectionRhumb;

                default: throw new Exception("Этот тип перечисления не реализован");
            }

        }
    }
}
