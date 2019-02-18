using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Statistic.Collections;

namespace WindEnergy.Lib.Statistic.Structures
{
    /// <summary>
    /// энергетические характеристики ветра
    /// </summary>
    public class EnergyInfo
    {
        /// <summary>
        /// период, за который получены эти данные
        /// </summary>
        public TimeSpan Period { get { return ToDate - FromDate; } }

        /// <summary>
        /// дата первого измерения в ряде
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// дата последнего измерения в ряде
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// средняя скорость за период времени м/с
        /// </summary>
        public double V0 { get; set; }

        /// <summary>
        /// коэффициент вариации 
        /// </summary>
        public double Cv { get; set; }

        /// <summary>
        /// удельная мощность, кВт/м2
        /// </summary>
        public double PowerDensity { get; set; }

        /// <summary>
        /// удельная энергия кВт*ч/м2
        /// </summary>
        public double EnergyDensity { get; set; }

        /// <summary>
        /// распределение по направлениям ветра
        /// </summary>
        public StatisticalRange<WindDirections> DirectionExpectancy { get; set; }

        /// <summary>
        /// распределение по скоростям
        /// </summary>
        public StatisticalRange<GradationItem> SpeedExpectancy { get; set; }
    }
}
