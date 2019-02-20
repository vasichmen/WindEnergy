using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
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
        /// компаратор DataItem для упорядочивания по возрастанию дат ряда
        /// </summary>
        private class DTComparer : IComparer<RawItem>
        {
            public int Compare(RawItem x, RawItem y)
            {
                return x.Date.CompareTo(y.Date);
            }
        }

        /// <summary>
        /// длина отрезка при разбиении на промежутки для поиска разделов интервалов
        /// </summary>
        private const int SECTION_LENGTH = 10;

        /// <summary>
        /// минимальное количество дней измерений с одинаковым интервалом, чтоб это можно было считать новым дипазоном измерений
        /// </summary>
        private const int DAYS_TO_NEW_INTERVAL = 30;

        /// <summary>
        /// обработать ряд и получить сведения о полноте, интервалах, количестве ошибок
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static QualityInfo ProcessRange(RawRange Range)
        {
            if (Range.Count < SECTION_LENGTH + SECTION_LENGTH * 0.5)
                return null;

            //поиск всех интервалов измерений
            //весь ряд делится на отрезки по 10 измерений.
            //на каждом отрезке ищется минимальный интервал, он принимается основным для этого отрезка
            //если в ряде встречаются только такие интервалы, то весь ряд заносится в результат с одним интервалом
            //иначе границей раздела интервалов берётся гранича разделов отрезков с разными интервалами

            List<RawItem> range = new List<RawItem>(Range);
            range.Sort(new DTComparer());

            //делим ряд по 10 измерений и для каждого отрезка находим минимальный интервал
            List<Diapason<int>> diapasons = new List<Diapason<int>>();
            List<StandartIntervals> intervals = new List<StandartIntervals>();
            for (int i = 0; i < range.Count - SECTION_LENGTH; i += SECTION_LENGTH)
            {
                Diapason<int> d = new Diapason<int>(i, i + SECTION_LENGTH);
                TimeSpan minSpan = TimeSpan.MaxValue;
                for (int j = i; j < i + SECTION_LENGTH - 1; j++)
                {
                    TimeSpan nsp = range[j].Date > range[j + 1].Date ? range[j].Date - range[j + 1].Date : range[j + 1].Date - range[j].Date;
                    if (nsp.TotalMinutes == 0)
                        continue;
                    if (nsp < minSpan)
                        minSpan = nsp;
                }
                int minutes = (int)minSpan.TotalMinutes;
                StandartIntervals interval = (StandartIntervals)minutes;
                diapasons.Add(d);
                intervals.Add(interval);
            }

            //исправление ошибок
            for (int i = 1; i < intervals.Count - 1; i++)
            {
                //если у диапазона соседи с одинаковыми, но другими интервалами, то это ошибка, исправляем
                if (intervals[i - 1] != intervals[i] && intervals[i + 1] != intervals[i] && intervals[i + 1] == intervals[i - 1])
                    intervals[i] = intervals[i - 1];
            }

            //поиск изменений в интервалах
            List<RangeInterval> rangeIntervals = new List<RangeInterval>();
            DateTime start = range[0].Date;
            DateTime end;
            for (int i = 0; i < intervals.Count - 1; i++)
                if (intervals[i] != intervals[i + 1])
                {
                    end = range[diapasons[i].To].Date; //конец диапазона
                    if (end - start > TimeSpan.FromDays(DAYS_TO_NEW_INTERVAL)) //если диапазон получается больше месяца, то добавляем
                    {
                        rangeIntervals.Add(new RangeInterval() { Diapason = new Diapason<DateTime>(start, end), Interval = intervals[i] }); // добавление диапазона
                        start = range[diapasons[i + 1].To].Date;
                    }
                }
            end = range[diapasons[diapasons.Count - 1].To].Date; //конец диапазона
            rangeIntervals.Add(new RangeInterval() { Diapason = new Diapason<DateTime>(start, end), Interval = intervals[intervals.Count - 1] }); // добавление диапазона
            QualityInfo res = new QualityInfo(rangeIntervals, range.Count);
            return res;
        }
    }
}

