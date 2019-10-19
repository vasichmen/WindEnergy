using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Classes.Structures
{
    /// <summary>
    /// структура записи в БД Флюгер
    /// </summary>
    public class FlugerMeteostationInfo: BaseMeteostationInfo
    {
      

        /// <summary>
        /// коэффициенты к
        /// </summary>
        public Dictionary<WindDirections, double> KM { get; set; } 
    }
}
