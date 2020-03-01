using CommonLib.Classes.Base;
using CommonLib.Geomodel;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Classes.Base
{

    /// <summary>
    /// базовые методы для работы с списками метеостанций
    /// </summary>
    public abstract class BaseMeteostationDatabase<TKey,TValue> : BaseFileDatabase<TKey, TValue>
    {

        /// <summary>
        /// расстояние в метрах при котором координаты считаются совпадающими
        /// </summary>
        private const double COORDINATES_OVERLAP = 10;



        /// <summary>
        /// создает объект для этого файла, не загружая данные
        /// </summary>
        /// <param name="FileName">адрес файла БД</param>
        protected BaseMeteostationDatabase(string FileName) : base(FileName) { }

        public override abstract Dictionary<TKey, TValue> LoadDatabaseFile();


        /// <summary>
        /// найти ближайшую МС для заданных координат и в заданном радиусе от точки 
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="maxRadius">радиус поиска в метрах</param>
        /// <returns></returns>
        protected virtual BaseMeteostationInfo GetNearestMS(PointLatLng coordinates, double maxRadius=double.MaxValue)
        {
            if (coordinates.IsEmpty)
                return null;
            BaseMeteostationInfo res = null;
            bool useMaxRadius = maxRadius != double.MaxValue;
            double min = double.MaxValue;
            foreach (var p in this.List.Cast<BaseMeteostationInfo>())
            {
                double f = EarthModel.CalculateDistance(p.Position, coordinates);
                if (f < COORDINATES_OVERLAP)
                {
                    // ближайшая метеостанция не должна быть той же самой 
                    continue;
                }
                if (useMaxRadius)
                {
                    if (f < min && f > COORDINATES_OVERLAP && f < maxRadius)
                    {
                        min = f;
                        res = p;
                    }
                }
                else
                {
                    if (f < min)
                    {
                        min = f;
                        res = p;
                    }
                }
            }
            if (min == double.MaxValue)
                return null;
            return res;
        }

        /// <summary>
        /// найти все метеостанции из списка mts, которые находятся в радиусе radius от заданной точки coordinates
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="radius"></param>
        /// <param name="addOwn">Если истина, то если точно в точке coordinates есть МС, то она тоже будет добавлена</param>
        /// <returns></returns>
        protected virtual List<BaseMeteostationInfo> GetNearestMS(PointLatLng coordinates, double radius, bool addOwn = false)
        {
            List<BaseMeteostationInfo> res = new List<BaseMeteostationInfo>();
            foreach (var ms in this.List.Cast<BaseMeteostationInfo>())
            {
                double dist = EarthModel.CalculateDistance(ms.Position, coordinates);
                if ((dist < radius && dist > COORDINATES_OVERLAP) || (dist < COORDINATES_OVERLAP && addOwn)) // если попадает в радиус и не совпадает или совпадает и надо добавлять 
                    res.Add(ms);
            }
            return res;
        }


    }
}
