using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;

namespace WindEnergy.Lib.Operations.Structures
{
    /// <summary>
    /// параметры восстановления ряда
    /// </summary>
    public class RestorerParameters
    {
        /// <summary>
        /// создаёт значения по умолчанию
        /// </summary>
        public RestorerParameters()
        {
            Interval = StandartIntervals.H1;
            Method = InterpolateMethods.Stepwise;
        }

        /// <summary>
        /// интервал наблюдений в новом ряде
        /// </summary>
        public StandartIntervals Interval { get; set; }

        /// <summary>
        /// метод интерполяции
        /// </summary>
        public InterpolateMethods Method { get; set; }

        /// <summary>
        /// координаты точки для которой происходит восстановление ряда
        /// </summary>
        public PointLatLng Coordinates { get; set; }

        /// <summary>
        /// базовый ряд, на основе которого будет происходить восстановление
        /// </summary>
        public RawRange BaseRange { get; set; }
    }

}
