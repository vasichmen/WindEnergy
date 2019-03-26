using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Geomodel;

namespace WindEnergy.Lib.Operations.Limits
{
    /// <summary>
    /// статические ограничения скоростей по регионам
    /// </summary>
    public class StaticRegionLimits : ILimitsProvider
    {
        private Dictionary<PointLatLng, ManualLimits> limits;

        /// <summary>
        /// загружает ограничения из заданного файла
        /// </summary>
        /// <param name="sourceFile"></param>
        public StaticRegionLimits(string sourceFile)
        {
           limits= Vars.LocalFileSystem.StaticSpeedLimits;
        }

      

        /// <summary>
        /// возвращает истину, если значение допустимо в этой точке
        /// </summary>
        /// <param name="item">данные для провtрки</param>
        /// <param name="coordinates">координаты точки</param>
        /// <returns></returns>
        public bool CheckItem(RawItem item, PointLatLng coordinates)
        {
            PointLatLng pt = getNearest(coordinates);
            if (limits.ContainsKey(pt))
                return limits[pt].CheckItem(item, pt);
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
            foreach (var p in limits.Keys)
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
