using CommonLib.Classes.Base;
using System.Collections.Generic;

namespace WindEnergy.WindLib.Classes.Structures
{
    /// <summary>
    /// структура записи в БД Классы открытости МС
    /// </summary>
    public class FlugerMeteostationInfo : BaseMeteostationInfo
    {
        /// <summary>
        /// коэффициенты открытости по 8 румбам kм
        /// </summary>
        public Dictionary<WindDirections8, double> KM { get; set; }
    }
}
