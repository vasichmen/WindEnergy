using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Data;
using WindEnergy.Lib.Statistic.Structures;

namespace WindEnergy.Lib.Statistic.Collections
{
    /// <summary>
    /// представление статистического ряда. Тип T - тип градаций ряда (windDirections, GradationItem)
    /// </summary>
   public class StatisticalRange<T>
    {
        /// <summary>
        /// информация о градациях градации
        /// </summary>
        public GradationInfo<T> Gradation { get; set; }

        private Dictionary<object, double> dict;

        public StatisticalRange(List<double> range, GradationInfo<T> gradation)
        {
            Gradation = gradation;
            dict = new Dictionary<object, double>();
            loadRange(range);
        }

        /// <summary>
        /// загружает ряд в словарь по заданным градациям
        /// </summary>
        /// <param name="range"></param>
        private void loadRange(List<double> range)
        {
            foreach (double val in range)
            {
                var gi = Gradation.GetItem(val);
                dict[gi]++;
            }
            foreach (T g in dict.Keys)
                dict[g] /= range.Count;
        }
    }
}
