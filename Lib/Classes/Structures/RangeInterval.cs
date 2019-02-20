using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Classes.Structures
{
    /// <summary>
    /// диапазон измерений с одинаковым интервалом
    /// </summary>
   public  class RangeInterval
    {
        /// <summary>
        /// начальная и конечная дата измерений
        /// </summary>
        public Diapason<DateTime> Diapason { get; set; }

        /// <summary>
        /// интервал измерений
        /// </summary>
        public StandartIntervals Interval { get; set; }
    }
}
