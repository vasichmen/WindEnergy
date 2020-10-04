using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Transformation.Altitude
{
    public class SuitAMSResultItem
    {
        /// <summary>
        /// выбранная метеостанция или null, если ошибка
        /// </summary>
        public AMSMeteostationInfo AMS { get; set; }

        /// <summary>
        /// истина, если в ряде представлены все месяцы
        /// </summary>
        public bool AllMonthInRange { get; set; }

        /// <summary>
        /// ошибка из-за отклонения
        /// </summary>
        public bool IsDeviationFailed { get; set; }

        /// <summary>
        /// среднеквадратическое отклонение по всем месяцам от скоростей  в ряде
        /// </summary>
        public double Deviation { get; set; }

        /// <summary>
        /// расстояние в метрах до метеостанции
        /// </summary>
        public double Distance { get; set; }

        public SuitAMSResultItem()
        {
            AMS = null;
            AllMonthInRange = false;
            IsDeviationFailed = false;
        }
    }
}
