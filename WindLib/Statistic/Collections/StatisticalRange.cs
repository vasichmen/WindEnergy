﻿using System.Collections.Generic;
using WindEnergy.WindLib.Statistic.Structures;

namespace WindEnergy.WindLib.Statistic.Collections
{
    /// <summary>
    /// представление статистического ряда. Тип T - тип градаций ряда (windDirections, GradationItem)
    /// </summary>
    public class StatisticalRange<T>
    {

        /// <summary>
        /// информация о градациях статистического ряда
        /// </summary>
        public GradationInfo<T> Gradation { get; }

        /// <summary>
        /// возвращает значения вероятностей от 0 до 1
        /// </summary>
        public List<double> Values { get; }

        /// <summary>
        /// ключи - интервалы (GradationItem, WindDirections)
        /// </summary>
        public List<object> Keys { get; }

        /// <summary>
        /// длина исходного ряда
        /// </summary>
        public int RangeCount { get; }

        /// <summary>
        /// получает элемент из массива Values с заданным ключом grad
        /// </summary>
        /// <param name="grad">ключ (интервал) для которого надо получить вероятность</param>
        /// <returns></returns>
        public double this[object grad] { get { return Values[Keys.IndexOf(grad)]; } }

        /// <summary>
        /// создаёт статистический ряд из ряда наблюдений по заданным интервалам (градациям)
        /// </summary>
        /// <param name="range"></param>
        /// <param name="gradation"></param>
        public StatisticalRange(List<double> range, GradationInfo<T> gradation)
        {
            Gradation = gradation;
            Keys = new List<object>();
            Values = new List<double>();
            RangeCount = range.Count;
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
                if (double.IsNaN(val)) continue;

                var gi = Gradation.GetItem(val);
                var index = Keys.IndexOf(gi);
                if (index < 0) continue;
                Values[index]++;
            }

            foreach (object g in Keys)
            {
                Values[Keys.IndexOf(g)] /= range.Count;
            }
        }
    }
}
