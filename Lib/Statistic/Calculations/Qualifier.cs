﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Generic;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Structures;

namespace WindEnergy.Lib.Statistic.Calculations
{
    /// <summary>
    /// определение качества исходного ряда
    /// </summary>
    public class Qualifier
    {
        /// <summary>
        /// обработать ряд и получить сведения о полноте, интервалах, количестве ошибок
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static QualityInfo ProcessRange(RawRange Range)
        {
            if (Range.Count < Vars.Options.QualifierSectionLength * 1.5)
                return null;

            //поиск всех интервалов измерений
            //весь ряд делится на отрезки по 10 измерений.
            //на каждом отрезке ищется минимальный интервал, он принимается основным для этого отрезка
            //если в ряде встречаются только такие интервалы, то весь ряд заносится в результат с одним интервалом
            //иначе границей раздела интервалов берётся гранича разделов отрезков с разными интервалами

            List<RawItem> range = new List<RawItem>(Range);
            range.Sort(new DateTimeComparer());

            //делим ряд по 10 измерений и для каждого отрезка находим минимальный интервал
            List<Diapason<int>> diapasons = new List<Diapason<int>>();
            List<StandartIntervals> intervals = new List<StandartIntervals>();
            //int k = 0;
            for (int i = 0; i < range.Count - Vars.Options.QualifierSectionLength; i += Vars.Options.QualifierSectionLength)
            {
                Diapason<int> d = new Diapason<int>(i, i + Vars.Options.QualifierSectionLength);
                TimeSpan minSpan = TimeSpan.MaxValue;
                for (int j = i; j < i + Vars.Options.QualifierSectionLength; j++)
                {
                    // k++;
                    TimeSpan nsp = range[j].Date > range[j + 1].Date ? range[j].Date - range[j + 1].Date : range[j + 1].Date - range[j].Date;
                    if (nsp.TotalMinutes == 0)
                        continue;
                    if (nsp < minSpan && Enum.IsDefined(typeof(StandartIntervals), (int)nsp.TotalMinutes))
                        minSpan = nsp;
                }
                int minutes = (int)minSpan.TotalMinutes;
                StandartIntervals interval = (StandartIntervals)minutes;
                diapasons.Add(d);
                intervals.Add(interval);
            }

            //исправление ошибок - удаление всех диапазонов, которые меньше заданного DAYS_TO_NEW_INTERVAL



            //поиск изменений в интервалах
            List<RangeInterval> rangeIntervals = new List<RangeInterval>();
            int s1 = 0, s2 = 0;
            for (int i = 0; i < intervals.Count - 1; i++)
                if (intervals[i] != intervals[i + 1])
                {
                    if ((int)intervals[i] * (s2 - s1) > Vars.Options.QualifierDaysToNewInterval * 24 * 60) //если второй интервал больше минимального
                    { //добавление первого интервала (от s1 до s2)
                        DateTime sd = range[diapasons[s1].From].Date;
                        DateTime ed = range[diapasons[s2].To].Date;
                        bool f = intervals[s1] == intervals[s2];
                        rangeIntervals.Add(new RangeInterval() { Diapason = new Diapason<DateTime>(sd, ed), Interval = intervals[s1] });
                        s1 = s2;
                    }
                    else //если второй интервал меньше минимального, то переставляем начало второго интервала
                        s2 = i + 1;
                }

            //добавление последнего
            DateTime end = range[range.Count - 1].Date; //конец диапазона
            DateTime start;
            if (rangeIntervals.Count > 0)
                start = rangeIntervals.Last().Diapason.To;
            else
                start = range[0].Date;
            rangeIntervals.Add(new RangeInterval() { Diapason = new Diapason<DateTime>(start, end), Interval = intervals[intervals.Count - 1] }); // добавление диапазона

            QualityInfo res = new QualityInfo(rangeIntervals, range.Count);
            return res;
        }
    }
}
