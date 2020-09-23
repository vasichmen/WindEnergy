using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Statistic.Structures
{
    /// <summary>
    /// статистическая информация об одном периоде в ряде
    /// </summary>
    public class SinglePeriodInfo
    {
        /// <summary>
        /// год
        /// </summary>
        public int Year { get; internal set; }
        /// <summary>
        /// начальная дата периода
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// конечная дата периода
        /// </summary>
        public DateTime To { get; set; }

        /// <summary>
        /// полнота ряда в % !!!
        /// </summary>
        public double Completness { get; set; }

        /// <summary>
        /// интервал наблюдений
        /// </summary>
        public StandartIntervals Interval { get; set; }

        /// <summary>
        /// средняя скорость
        /// </summary>
        public double AverageSpeed { get; set; }

        /// <summary>
        /// отклонение средней скорости от среднемноголетней скорости
        /// </summary>
        public double SpeedDeviation { get; set; }

        /// <summary>
        /// отклонение средней скорости от среднемноголетней в процентах
        /// </summary>
        public double SpeedDeviationPercent { get;  set; }

        /// <summary>
        /// отклонение повторяемости скорости ветра в долях
        /// </summary>
        public double ExpectancyDeviation { get; set; }


        /// <summary>
        /// максимальная скорость за период
        /// </summary>
        public double Vmax { get; set; }

    }
}
