using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Operations.Structures
{
    /// <summary>
    /// параметры восстановления ряда
    /// </summary>
    public class RestorerParameters
    {
        /// <summary>
        /// создаёт значения по умолчанию
        /// </summary>
        public RestorerParameters()
        {
            Interval = StandartIntervals.H1;
            Method = InterpolateMathods.Stepwise;
        }

        /// <summary>
        /// интервал наблюдений в новом ряде
        /// </summary>
        public StandartIntervals Interval { get; set; }

        /// <summary>
        /// метод интерполяции
        /// </summary>
        public InterpolateMathods Method { get; set; }
    }

}
