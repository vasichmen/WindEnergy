using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Collections.Generic;

namespace WindEnergy.Lib.Operations.Structures
{
    /// <summary>
    /// структура информации при выборе расчётного года
    /// </summary>
    public class CalculateYearInfo
    {
        public CalculateYearInfo() {
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
