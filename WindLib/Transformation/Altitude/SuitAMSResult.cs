using System.Collections.Generic;

namespace WindEnergy.WindLib.Transformation.Altitude
{
    /// <summary>
    /// результат подбора АМС
    /// </summary>
    public class SuitAMSResult : List<SuitAMSResultItem>
    {
        /// <summary>
        /// если истина, то в исходнорм ряде представлены все месяцы
        /// </summary>
        public bool AllMonthInRange { get; set; }

        /// <summary>
        /// Относительные скорости по месяцам для этого ряда
        /// </summary>
        public Dictionary<Months, double> RangeRelativeSpeeds { get; set; }
    }
}
