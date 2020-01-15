using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Statistic.Structures
{
    /// <summary>
    /// информация об отклонениях относительно многолетних значений
    /// </summary>
    public class DeviationsInfo
    {
        /// <summary>
        /// среднеквадратичное отклонение скорости от многолетней
        /// </summary>
        public double SpeedDeviation { get; set; }

        /// <summary>
        /// среднеквадратичное отклонение повторяемости от многолетней
        /// </summary>
        public double ExpDeviation { get; set; }
    }
}
