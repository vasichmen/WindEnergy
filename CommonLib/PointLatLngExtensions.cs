using GMap.NET;
using System;

namespace CommonLib
{
    public static class PointLatLngExtensions
    {
        /// <summary>
        /// форматирование координат с заданным колическтвом знаков после запятой
        /// </summary>
        /// <param name="pointElement"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static string ToString(this PointLatLng pointElement, int decimals)
        {
            return $"{Math.Round(pointElement.Lat, decimals)} {Math.Round(pointElement.Lng, decimals)}";
        }
    }
}
