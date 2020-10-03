using CommonLib.Geomodel;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Structures;
using WindLib;

namespace WindEnergy.WindLib.Transformation.Check.Limits
{
    /// <summary>
    /// БД Максимальные скорости ветра
    /// </summary>
    public class StaticRegionLimits : ILimitsProvider
    {
        private readonly Dictionary<PointLatLng, ManualLimits> limits;
        PointLatLng nearestPoint = PointLatLng.Empty;

        /// <summary>
        /// загружает ограничения из заданного файла
        /// </summary>
        public StaticRegionLimits(Dictionary<PointLatLng, ManualLimits> limits, PointLatLng coordinates)
        {
            this.limits = limits;
            nearestPoint = getNearest(coordinates);
        }

        /// <summary>
        /// возвращает истину, если значение допустимо в этой точке
        /// </summary>
        /// <param name="item">данные для проверки</param>
        /// <returns></returns>
        public bool CheckItem(RawItem item)
        {
            if (limits.ContainsKey(nearestPoint))
                return limits[nearestPoint].CheckItem(item);
            else
                throw new Exception("Коллекция ключей пуста!");

        }

        /// <summary>
        /// найти ближайшую точку в словаре к заданной
        /// </summary>
        /// <param name="coordinates">заданная точка</param>
        /// <returns></returns>
        private PointLatLng getNearest(PointLatLng coordinates)
        {
            double min = double.MaxValue;
            PointLatLng res = PointLatLng.Empty;
            foreach (var p in this.limits.Keys)
            {
                double f = EarthModel.CalculateDistance(p, coordinates);
                if (f < min)
                {
                    min = f;
                    res = p;
                }
            }
            return res;
        }
    }
}
