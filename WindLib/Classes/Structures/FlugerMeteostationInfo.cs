using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Classes.Structures
{
    /// <summary>
    /// структура записи в БД Флюгер
    /// </summary>
    public class FlugerMeteostationInfo: BaseMeteostationInfo
    {
        /// <summary>
        /// коэффициенты открытости по 8 румбам kм
        /// </summary>
        public Dictionary<WindDirections8, double> KM { get; set; } 
    }
}
