using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Generic;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Structures;

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
        /// <returns></returns>
        public static QualityInfo ProcessRange(RawRange Range)
        {
            if (Range.Count < Vars.Options.QualifierSectionLength * 1.5)
                return null;

            TimeSpan maxemptyspace = TimeSpan.MinValue;
            TimeSpan tmp;
            //поиск наибольшего пустого пропуска
            for (int i = 1; i < Range.Count; i++)
            {
                tmp = Range[i].Date - Range[i - 1].Date;
                if (tmp > maxemptyspace)
                    maxemptyspace = tmp;
            }

            //поиск всех интервалов измерений
            //весь ряд делится на отрезки по 10 измерений.
            //на каждом отрезке ищется минимальный интервал, он принимается основным для этого отрезка
            //если в ряде встречаются только такие интервалы, то весь ряд заносится в результат с одним интервалом
            //иначе границей раздела интервалов берётся гранича разделов отрезков с разными интервалами

            List<RawItem> range = new List<RawItem>(Range);
            range.Sort(new DateTimeComparerRawItem());

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
                    if (nsp.TotalDays > Vars.Options.QualifierDaysToBeginMissing)
                    {
                        minSpan = TimeSpan.MaxValue;
                        break;
                    }
                }
                StandartIntervals interval;
                if (minSpan == TimeSpan.MaxValue)
                    interval = StandartIntervals.Missing;
                else
                {
                    int minutes = (int)minSpan.TotalMinutes;
                    interval = (StandartIntervals)minutes;
                }
                diapasons.Add(d);
                intervals.Add(interval);
            }

            //поиск изменений в интервалах и добавление в общий список
            List<RangeInterval> rangeIntervals = new List<RangeInterval>();
            int s1 = 0;
            for (int i = 0; i < intervals.Count - 1; i++)
                if (intervals[i] != intervals[i + 1])
                {
                    DateTime sd = range[diapasons[s1].From].Date;
                    DateTime ed = range[diapasons[i].To].Date;
                    StandartIntervals inter = intervals[s1];
                    rangeIntervals.Add(new RangeInterval() { Diapason = new Diapason<DateTime>(sd, ed), Interval = inter });
                    s1 = i + 1;

                }

            //добавление последнего
            DateTime end = range.Last().Date; //конец диапазона
            DateTime start;
            if (rangeIntervals.Count > 0)
                start = rangeIntervals.Last().Diapason.To;
            else
                start = range[0].Date;
            rangeIntervals.Add(new RangeInterval() { Diapason = new Diapason<DateTime>(start, end), Interval = intervals.Last() }); // добавление диапазона


            //обход всех найденных диапазонов и объединение маленьких или одинаковых до тех пор, пока будет совершено хоть одно объединение
            int minimal = Vars.Options.QualifierDaysToNewInterval * 24 * 60;
            bool hasCompl = true;
            while (hasCompl)
            {
                //если диапазонов нет или один, то выходим
                if (rangeIntervals.Count == 0 || rangeIntervals.Count == 1)
                {
                    break;
                }

                //если диапазонов два, то ищем маленький диапазон в конце и в начале
                if (rangeIntervals.Count == 2)
                {
                    if (rangeIntervals[0].LengthMinutes < minimal || rangeIntervals[1].LengthMinutes < minimal) //объединяем диапазоны и выходим
                    {
                        RangeInterval ri = combineIntervals(rangeIntervals[0], rangeIntervals[1]);
                        rangeIntervals.Clear();
                        rangeIntervals.Add(ri);
                        hasCompl = true;
                        continue;
                    }
                    break;
                }

                //если больше двух диапазонов, то пробуем объединить все маленькие диапазоны
                hasCompl = false;
                for (int i = 1; i < rangeIntervals.Count - 1; i++)
                {
                    RangeInterval r1 = rangeIntervals[i - 1];
                    RangeInterval ri = rangeIntervals[i];
                    RangeInterval r2 = rangeIntervals[i + 1];

                    int i1 = (int)r1.Interval;
                    int ii = (int)ri.Interval;
                    int i2 = (int)r2.Interval;

                    int l1 = r1.LengthMinutes;
                    int li = ri.LengthMinutes;
                    int l2 = r2.LengthMinutes;

                    #region объединение диапазонов с одинаковым интервалом

                    if (i1 == ii) //если у левого и среднего диапазона интервалы одинаковые
                    {
                        RangeInterval rr = combineIntervals(r1, ri);
                        rangeIntervals.Remove(ri);
                        rangeIntervals.Remove(r1);
                        rangeIntervals.Insert(i - 1, rr);
                        hasCompl = true;
                        break;
                    }

                    if (ii == i2) //если у правого и среднего диапазона интервалы одинаковые
                    {
                        RangeInterval rr = combineIntervals(ri, r2);
                        rangeIntervals.Remove(ri);
                        rangeIntervals.Remove(r2);
                        rangeIntervals.Insert(i - 1, rr);
                        hasCompl = true;
                        break;
                    }

                    #endregion

                    #region объединение диапазонов, меньших, чем минимальный

                    if (l1 < minimal) //если левый меньше минимального, то объединяем с средним
                    {
                        RangeInterval rr = combineIntervals(r1, ri);
                        rangeIntervals.Remove(ri);
                        rangeIntervals.Remove(r1);
                        rangeIntervals.Insert(i - 1, rr);
                        hasCompl = true;
                        break;
                    }

                    if (li < minimal) //если средний меньше минимального, то объединяем с тем, у кого меньше интервал
                    {
                        if (i1 < i2) //если левый интервал меньше правого
                        {
                            RangeInterval rr = combineIntervals(r1, ri);
                            rangeIntervals.Remove(ri);
                            rangeIntervals.Remove(r1);
                            rangeIntervals.Insert(i - 1, rr);
                            hasCompl = true;
                            break;
                        }
                        if (i1 > i2) //если правый интервал меньше правого
                        {
                            RangeInterval rr = combineIntervals(ri, r2);
                            rangeIntervals.Remove(ri);
                            rangeIntervals.Remove(r2);
                            rangeIntervals.Insert(i - 1, rr);
                            hasCompl = true;
                            break;
                        }
                        if (i1 == i2)//если интервалы равны, то объединяем всех троих
                        {
                            RangeInterval rr = combineIntervals(r1, r2);
                            rangeIntervals.Remove(r1);
                            rangeIntervals.Remove(ri);
                            rangeIntervals.Remove(r2);
                            rangeIntervals.Insert(i - 1, rr);
                            hasCompl = true;
                            break;
                        }
                    }

                    if (l2 < minimal && i == rangeIntervals.Count - 1) //если правый меньше минимального и это последний элемент, то объединяем с средним
                    {
                        RangeInterval rr = combineIntervals(ri, r2);
                        rangeIntervals.Remove(ri);
                        rangeIntervals.Remove(r2);
                        rangeIntervals.Insert(i - 1, rr);
                        hasCompl = true;
                        break;
                    }

                    #endregion

                }
            }



            QualityInfo res = new QualityInfo(rangeIntervals, maxemptyspace, range.Count);
            return res;
        }

        /// <summary>
        /// объединяет два диапазона. Началом становится начало диапазона 1, а концом конец диапазона 2. За интервал принимается минимальный из двух
        /// </summary>
        /// <param name="rangeInterval1"></param>
        /// <param name="rangeInterval2"></param>
        /// <returns></returns>
        private static RangeInterval combineIntervals(RangeInterval rangeInterval1, RangeInterval rangeInterval2)
        {
            int i1 = (int)rangeInterval1.Interval;
            int i2 = (int)rangeInterval2.Interval;
            StandartIntervals inter = (StandartIntervals)Math.Min(i1, i2);
            return new RangeInterval() { Diapason = new Diapason<DateTime>(rangeInterval1.Diapason.From, rangeInterval2.Diapason.To), Interval = inter };
        }


        /// <summary>
        /// обработать ряд и получить сведения о полноте, интервалах, количестве ошибок
        /// </summary>
        /// <returns></returns>
        public static QualityInfo ProcessRange2(RawRange Range)
        {
            if (Range.Count < Vars.Options.QualifierSectionLength * 1.5)
                return null;

            TimeSpan maxemptyspace = TimeSpan.MinValue;
            TimeSpan tmp;
            //поиск наибольшего пустого пропуска
            for (int i = 1; i < Range.Count; i++)
            {
                tmp = Range[i].Date - Range[i - 1].Date;
                if (tmp > maxemptyspace)
                    maxemptyspace = tmp;
            }

            //поиск всех интервалов измерений
            //весь ряд делится на отрезки по 10 измерений.
            //на каждом отрезке ищется минимальный интервал, он принимается основным для этого отрезка
            //если в ряде встречаются только такие интервалы, то весь ряд заносится в результат с одним интервалом
            //иначе границей раздела интервалов берётся гранича разделов отрезков с разными интервалами

            List<RawItem> range = new List<RawItem>(Range);
            range.Sort(new DateTimeComparerRawItem());

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

            //поиск изменений в интервалах
            //все диапазоны дат, которые меньше Vars.Options.QualifierDaysToNewInterval присоединяются к левой части ряда
            List<RangeInterval> rangeIntervals = new List<RangeInterval>();
            int s1 = 0, s2 = 0; //указатели на элементы в массиве intervals[] (начала первого и второго отрезка)
            for (int i = 0; i < intervals.Count - 1; i++)
                if (intervals[i] != intervals[i + 1])
                {
                    int s2_min = (int)range[diapasons[s2].From].DateArgument;
                    int s1_min = (int)range[diapasons[s1].To].DateArgument;
                    int a1 = s1_min - s2_min; //длина первого отрезк в минутах

                    int si_min = (int)range[diapasons[i].To].DateArgument;
                    int a2 = si_min - s2_min; //длина второго отрезк в минутах

                    int minimal = Vars.Options.QualifierDaysToNewInterval * 24 * 60; // длина минимального отрезка в минутах

                    if (s1 == s2)
                    {
                        s2 = i + 1;
                        continue;
                    }

                    if (a2 < minimal)
                    {
                        s2 = i + 1;
                        continue;
                    }



                    if (a2 > minimal && a1 > minimal) //если первый и второй отрезок больше минимального
                    {
                        //добавление первого отрезка (от s1 до s2)
                        DateTime sd = range[diapasons[s1].From].Date;
                        DateTime ed = range[diapasons[s2].To].Date;
                        StandartIntervals inter = intervals[s1];
                        //bool f = intervals[s1] == intervals[s2];
                        rangeIntervals.Add(new RangeInterval() { Diapason = new Diapason<DateTime>(sd, ed), Interval = inter });
                        s1 = s2;
                        s2 = i + 1;
                    }
                    else //если второй отрезок меньше минимального, то переставляем начало второго отрезка (присоединяем второй отрезок к первому)
                        s2 = i + 1;
                }

            //добавление последнего
            DateTime end = range.Last().Date; //конец диапазона
            DateTime start;
            if (rangeIntervals.Count > 0)
            {
                start = rangeIntervals.Last().Diapason.To;

                // если последний диапазон с таким же интервалом, как предпоследний, то обновляем последний добавленный интервал
                if (intervals.Last() == rangeIntervals.Last().Interval)
                {
                    rangeIntervals.Last().Diapason = new Diapason<DateTime>(start, end);
                }
                else
                    rangeIntervals.Add(new RangeInterval() { Diapason = new Diapason<DateTime>(start, end), Interval = intervals.Last() }); // добавление диапазона
            }
            else
            {
                start = range[0].Date;
                rangeIntervals.Add(new RangeInterval() { Diapason = new Diapason<DateTime>(start, end), Interval = intervals.Last() }); // добавление диапазона
            }

            QualityInfo res = new QualityInfo(rangeIntervals, maxemptyspace, range.Count);
            return res;
        }
    }
}

