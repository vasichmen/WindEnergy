using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.Lib.Geomodel;

namespace WindEnergy.Lib.Operations.Interpolation
{
    /// <summary>
    /// интерполирование функции на основе данных ближайшей метеостанции
    /// </summary>
    public class NearestMSInterpolateMethod : IInterpolateMethod
    {
        private readonly Dictionary<double, double> func;
        private readonly MeteorologyParameters parameterType;
        private RawRange nearestRange;
        private readonly Func<double, double> getRes=null;

        /// <summary>
        /// создаёт новый интерполятор для заданной точки с заданной функций и типом расчетного параметра
        /// </summary>
        /// <param name="func">известные значения функции в заданной точке</param>
        /// <param name="coordinates">координаты точки, для которой известны значения func</param>
        /// <param name="parameterType">тип мтеорологического параметра</param>
        public NearestMSInterpolateMethod(Dictionary<double, double> func, PointLatLng coordinates, MeteorologyParameters parameterType) : this(func, getNearestRange(func, coordinates), parameterType) { }


        /// <summary>
        /// создание нового интерполятора с заданными значениями на основе существующего интерполятора (в том случае, если для этой точки уже был создан интерполятор)
        /// </summary>
        /// <param name="func">известные значения функции</param>
        /// <param name="baseInterpolator"></param>
        /// <param name="direction">тип мтеорологического параметра</param>
        public NearestMSInterpolateMethod(Dictionary<double, double> func, NearestMSInterpolateMethod baseInterpolator, MeteorologyParameters parameterType) : this(func, baseInterpolator.nearestRange, parameterType) { }

        /// <summary>
        /// создание нового интерполятора с заданными значениями на основе существующего базового ряда наблюдения
        /// </summary>
        /// <param name="func">известные значения функции в заданной точке</param>
        /// <param name="baseRange">базовый ряд (с ближайшей МС), на основе которого будет происходить восстановление</param>
        /// <param name="parameterType">тип мтеорологического параметра</param>
        public NearestMSInterpolateMethod(Dictionary<double, double> func, RawRange baseRange, MeteorologyParameters parameterType)
        {
            this.parameterType = parameterType;
            this.func = func;
            this.nearestRange = baseRange;
            //TODO: подготовка исходной функции и базового ряда, расчет коэффициента корреляции, сохдание функции получения результата
        }

        /// <summary>
        /// получить значение функции по заданному значению
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double GetValue(double x)
        {
            if (getRes != null)
                return getRes(x);
            else
                throw new Exception("Функция получения результата не задана");
        }

        #region вспомогательные методы

        /// <summary>
        /// найти ближайшую МС для заданных координат
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        private static MeteostationInfo getNearestMS(PointLatLng coordinates, List<MeteostationInfo> mts)
        {
            MeteostationInfo res = null;
            double min = double.MaxValue;
            foreach (var p in mts)
            {
                double f = EarthModel.CalculateDistance(p.Coordinates, coordinates);
                if (f < min && f > 0)
                {
                    min = f;
                    res = p;
                }
            }
            res.OwnerDistance = min;
            return res;
        }


        /// <summary>
        /// загружает ряд наблюдений с ближайшей МС, максимально подходящий под заданную функцию
        /// </summary>
        /// <param name="func">функция исходного ряда (для определения начала и конца ряда)</param>
        /// <param name="coordinates">координаты исходного ряда</param>
        /// <returns></returns>
        private static RawRange getNearestRange(Dictionary<double, double> func, PointLatLng coordinates)
        {
            RawRange res = null;
            List<MeteostationInfo> meteostations;
            meteostations = Vars.LocalFileSystem.LoadMeteostationList(Vars.Options.StaticMeteostationCoordinatesSourceFile);
            MeteostationInfo nearestMS = getNearestMS(coordinates, meteostations);

            RP5ru provider = new RP5ru(Vars.Options.CacheFolder + "\\rp5.ru");
            provider.GetMeteostationExtInfo(ref nearestMS);

            DateTime fr, to;
            fr = DateTime.MinValue + TimeSpan.FromMinutes(func.Keys.ToList().Min());
            to = DateTime.MinValue + TimeSpan.FromMinutes(func.Keys.ToList().Max());

            if (fr < nearestMS.MonitoringFrom) //если исходный ряд начинается
                fr = nearestMS.MonitoringFrom;
            if (fr > to)
                throw new Exception("Ряды не пересекаются: один из рядов заканчивается раньше, чем начинается другой");
            res = provider.GetRange(fr, to, nearestMS);
            return res;
        }

        

        #endregion
    }
}
