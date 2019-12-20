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
        /// <param name="onPercentChange">метод, вызываемый при изменении процесса прогресса выполнения</param>
        /// <param name="checkStop">функция проверки прерывания процесаа. Возвращает true, если надо прервать загрузку</param>
        /// <returns></returns>
        RawRange GetRange(DateTime fromDate, DateTime toDate, RP5MeteostationInfo point_info, Action<double> onPercentChange = null, Func<bool> checkStop = null);
    }
}