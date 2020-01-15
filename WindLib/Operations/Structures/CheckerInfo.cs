using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Operations.Structures
{
    /// <summary>
    /// статистика исправлений в ряде
    /// </summary>
  public  class CheckerInfo
    {
        /// <summary>
        /// всего исходных измерений
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// превышений диапазонов ограничений
        /// </summary>
        public int OverLimits { get; set; }

        /// <summary>
        /// осталось измерений
        /// </summary>
        public int Remain { get; set; }

        /// <summary>
        /// повторов даты
        /// </summary>
        public int DateRepeats { get; set; }

        /// <summary>
        /// других ошибок
        /// </summary>
        public int OtherErrors { get; set; }
    }
}
