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

        /// <summary>
        /// возвращает значения вероятностей
        /// </summary>
        public List<double> Values { get ;  }
        public List<object> Keys { get; }

        public StatisticalRange(List<double> range, GradationInfo<T> gradation)
        {
            Gradation = gradation;
            Keys = new List<object>();
            Values = new List<double>();
            foreach (object g in gradation.Items)
            {
                Keys.Add(g);
                Values.Add(0);
            }
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
                Values[Keys.IndexOf(gi)]++;
            }

            foreach (object g in Keys)
            {
                Values[Keys.IndexOf(g)] /= range.Count;
            }
        }
    }
}
