using System;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;

namespace WindEnergy.Lib.Data.Interfaces
{
    /// <summary>
    /// источник рядов данных о погоде за определенный промежуток времени
    /// </summary>
    public interface IRangeProvider
    {
        /// <summary>
        /// получить ряд данных за указанный промежуток времени в заданной точке
        /// </summary>
        /// <param name="fromDate">начало</param>
        /// <param name="toDate">конец</param>
        /// <param name="point_info">информация о точке (объект класса PointInfo конкретного класса)</param>
        /// <returns></returns>
        RawRange GetRange(DateTime fromDate, DateTime toDate, MeteostationInfo point_info);
    }
}