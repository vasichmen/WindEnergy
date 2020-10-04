using CommonLib.Classes.Collections.Generic;
using WindEnergy.WindLib.Classes.Collections;

namespace WindEnergy.WindLib.Statistic.Structures
{
    /// <summary>
    /// структура информации при выборе расчётного года
    /// </summary>
    public class CalculateYearInfo
    {
        public CalculateYearInfo()
        {
            Years = new SortableBindingList<SinglePeriodInfo>();
        }

        /// <summary>
        /// рекомендуемый расчётный год
        /// </summary>
        public SinglePeriodInfo RecomendedYear { get; set; }

        /// <summary>
        /// исходный ряд, для которого ищем года
        /// </summary>
        public RawRange Range { get; set; }

        /// <summary>
        /// список годов и информации о них
        /// </summary>
        public SortableBindingList<SinglePeriodInfo> Years { get; set; }

        /// <summary>
        /// среднемноголетняя скорость
        /// </summary>
        public double AverageSpeed { get; set; }
    }
}
