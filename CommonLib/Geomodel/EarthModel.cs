using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Geomodel
{
    /// <summary>
    /// географические вычисления
    /// </summary>
    public static class EarthModel
    {

        /// <summary>
        /// полярное сжатие
        /// </summary>
        public static double Compression
        {
            get
            {
                return 1 / 298.257223563d;
            }
        }

        /// <summary>
        /// Малый радиус (к полюсу), м
        /// </summary>
        public static double MinAxis { get { return MaxAxis - Compression * MaxAxis; } }

        /// <summary>
        /// большая полуось,м
        /// </summary>
        public static double MaxAxis
        {
            get
            {
                return 6378137d;
            }
        }

        /// <summary>
        /// Средний радиус, м
        /// </summary>
        public static double AverageRadius { get { return Math.Sqrt(MinAxis * MaxAxis); } }

        /// <summary>
        /// расчет расстояния между двумя точками в метрах
        /// </summary>
        /// <param name="p1">точка 1</param>
        /// <param name="p2">точка 2</param>
        /// <returns></returns>
        public static double CalculateDistance(PointLatLng p1, PointLatLng p2)
        {
            return CalculateDistance(p1.Lat, p1.Lng, p2.Lat, p2.Lng);
        }

        /// <summary>
        /// расчет расстояния между двумя точками в метрах
        /// </summary>
        /// <param name="lat1">широта 1 точки</param>
        /// <param name="lon1">долгота 1 точки</param>
        /// <param name="lat2">широта 2 точки</param>
        /// <param name="lon2">долгота 2 точки</param>
        /// <returns></returns>
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double d = SphereCalculations.CalculateDistance(lat1, lon1, lat2, lon2);
            double D = d * AverageRadius;
            return D;
        }
    }
}
