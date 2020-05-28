using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Transformation.Altitude
{
  public  class SuitAMSResult
    {
        /// <summary>
        /// выбранная метеостанция или null, если ошибка
        /// </summary>
        public AMSMeteostationInfo AMS { get; internal set; }

        /// <summary>
        /// истина, если в ряде представлены все месяцы
        /// </summary>
        public bool AllMonthInRange { get; internal set; }

        /// <summary>
        /// ошибка из-за отклонения
        /// </summary>
        public bool IsDeviationFailed { get; set; }
        public object Deviation { get; internal set; }

        public SuitAMSResult()
        {
            AMS = null;
            AllMonthInRange = false;
            IsDeviationFailed = false;
        }
    }
}
