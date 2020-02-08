using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Transformation.Altitude
{
    /// <summary>
    /// настройки поднятия ряда на высоту
    /// </summary>
    public class ElevatorParameters
    {
        /// <summary>
        /// с какой высоты на у. з. поднимаем, м
        /// </summary>
        public double FromHeight { get; set; }

        /// <summary>
        /// на какую высоту поднимаем, м
        /// </summary>
        public double ToHeight { get; set; }

        /// <summary>
        /// координаты места измерения (для поиска АМС)
        /// </summary>
        public PointLatLng Coordinates { get; set; }

        /// <summary>
        /// расстояние, на котором ищется АМС (NaN - неограниченное)
        /// </summary>
        public double SearchRaduis { get;  set; }
    }
}
