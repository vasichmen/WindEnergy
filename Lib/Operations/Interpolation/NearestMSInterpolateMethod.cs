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
        private readonly Func<double, double> getRes = null;

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

            double a = 0, b = 0, r = 0; //коэффициенты прямой  и коэффициент корреляции
            Dictionary<double, double> baseFunc = baseRange.GetFunction(parameterType); //функция базового ряда
            List<double>[] tableCoeff = calcTableCoeff(func, baseFunc); //таблица для расчёта коэффициентов a, b, r

            bool f = tableCoeff[0].Count == tableCoeff[1].Count && tableCoeff[1].Count == tableCoeff[2].Count && tableCoeff[2].Count == tableCoeff[3].Count &&
                tableCoeff[3].Count == tableCoeff[4].Count && tableCoeff[4].Count == tableCoeff[5].Count && tableCoeff[5].Count == tableCoeff[6].Count &&
                tableCoeff[6].Count == tableCoeff[7].Count && tableCoeff[7].Count == tableCoeff[8].Count;
            if (!f) { throw new Exception("расчет таблицы не удался"); }

            //коэффициенты прямой
            double s_v1 = tableCoeff[0][tableCoeff[0].Count - 1];
            double s_v2 = tableCoeff[1][tableCoeff[1].Count - 1];
            double s_v1v2 = tableCoeff[2][tableCoeff[2].Count - 1];
            double s_v1v1 = tableCoeff[3][tableCoeff[3].Count - 1];
            int n = tableCoeff[0].Count - 1; //-1 тк последний элемент - сумма
            a = (n * s_v1v2 - s_v1 * s_v2) / (n * s_v1v1 - Math.Pow(s_v1, 2));
            b = (s_v2 - a * s_v1) / (n);

            //коэффициент корреляции
            double s_dv1dv2 = tableCoeff[6][tableCoeff[6].Count - 1];
            double s_dv1dv1 = tableCoeff[7][tableCoeff[7].Count - 1];
            double s_dv2dv2 = tableCoeff[8][tableCoeff[8].Count - 1];
            r = s_dv1dv2 / (Math.Sqrt(s_dv1dv1 * s_dv2dv2));

            if (r < Vars.Options.MinimalCorrelationCoeff && Vars.Options.MinimalCorrelationControlParametres.Contains(parameterType))
                throw new Exception("Недостаточная точность");

            getRes = new Func<double, double>((x) => { return a * x + b; });
        }

        private List<double>[] calcTableCoeff(Dictionary<double, double> func, Dictionary<double, double> baseFunc)
        {
            ///V1 - заданная функция
            ///V2 - функция на ближайшей МС
            ///V1*V2
            ///V1^2
            ///dV1=V1-averV1
            ///dV2=V2-averV2
            ///dV1*dV2
            ///dV1^2
            ///dV2^2
            List<double>[] res = new List<double>[9];
            for (int i = 0; i < res.Length; i++) res[i] = new List<double>();

            double averV1 = func.Values.Average();
            double averV2 = baseFunc.Values.Average();

            //ищем совпадающие значения и расчитываем строки
            foreach (var k in func.Keys)
            {
                if (baseFunc.ContainsKey(k))
                {
                    //для коэфф a,b
                    double v1 = func[k];
                    double v2 = baseFunc[k];
                    res[0].Add(v1);
                    res[1].Add(v2);
                    res[2].Add(v1 * v2);
                    res[3].Add(v1 * v1);

                    //для коэфф корреляции
                    double dv1 = v1 - averV1;
                    double dv2 = v2 - averV2;
                    res[4].Add(dv1);
                    res[5].Add(dv2);
                    res[6].Add(dv1 * dv2);
                    res[7].Add(dv1 * dv1);
                    res[8].Add(dv2 * dv2);
                }
            }

            //суммы в конце рядов
            for (int i = 0; i < res.Length; i++)
            {
                double s = res[i].Sum();
                res[i].Add(s);
            }

            return res;
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
                if (f < 10)
                {//TODO: ближайшая метеостанция не должна быть той же самой 
                }

                if (f < min && f > 0 && f < Vars.Options.NearestMSRadius)
                {
                    min = f;
                    res = p;
                }
            }
            if (min == double.MaxValue)
                return null;
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
            if (nearestMS == null)
                throw new Exception("Не удалось найти ближайшую метеостанцию в заданном радиусе");

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
