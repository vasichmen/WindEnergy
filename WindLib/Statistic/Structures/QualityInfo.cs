﻿using System;
using System.Collections.Generic;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Statistic.Structures
{
    /// <summary>
    /// информация о качестве ряда
    /// </summary>
    public class QualityInfo
    {
        /// <summary>
        /// количество пропусков
        /// </summary>
        public int PassAmount { get; }

        /// <summary>
        /// количество измерений
        /// </summary>
        public int MeasureAmount { get; }

        /// <summary>
        /// ожидаемое количество измерений
        /// </summary>
        public int ExpectAmount { get; }

        /// <summary>
        /// полнота ряда в долях
        /// </summary>
        public double Completeness { get; }

        /// <summary>
        /// список интервалов измерений 
        /// </summary>
        public List<RangeInterval> Intervals { get; }

        /// <summary>
        /// максимальный перерыв в измерениях
        /// </summary>
        public TimeSpan MaxEmptySpace { get; }

        /// <summary>
        /// создает новую структуру информации о качестве ряда с указанными значениями
        /// </summary>
        /// <param name="intervals">диапазоны наблюдений с разными значениями интервалов</param>
        /// <param name="maxEmptySpace"></param>
        /// <param name="measures">общее количество измерений в ряде</param>
        public QualityInfo(List<RangeInterval> intervals, TimeSpan maxEmptySpace, int measures)
        {
            if (intervals == null || intervals.Count == 0)
                throw new ArgumentNullException("должны быть заданы интервалы наблюдений");
            Intervals = intervals;

            //складываем все диапазоны и вычисляем ожидаемое число измерений
            int expectAm = 0;
            foreach (RangeInterval ri in intervals)
            {
                DateTime fromi = ri.Diapason.From;
                DateTime toi = ri.Diapason.To;
                TimeSpan span = toi - fromi;
                int intervalMinutes = (int)ri.Interval;
                expectAm += (int)(span.TotalMinutes / intervalMinutes);
            }
            expectAm++;

            MaxEmptySpace = maxEmptySpace;
            ExpectAmount = expectAm; //ожидаемое число измерений
            PassAmount = expectAm - measures; //количество пропусков в ряде
            Completeness = (double)(measures / (double)expectAm); //полнота ряда
            if (Completeness > 1)
                Completeness = 1;
            MeasureAmount = measures; //фактическое количество измерений
        }
    }
}
