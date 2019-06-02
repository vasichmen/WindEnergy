using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Classes
{
    [Serializable]
    public class WindEnergyException : ApplicationException
    {
        public WindEnergyException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class GetBaseRangeException : WindEnergyException
    {
        /// <summary>
        /// максимальный коэфф корреляции, найденный среди метеостанций в заданной области
        /// </summary>
        public double MaxCorrelationCoefficient { get; }

        /// <summary>
        /// минимальный допустимый коэфф корреляции из настроек
        /// </summary>
        public double MinCorellationCoefficient { get; }
        /// <summary>
        /// расстояние до ближайшей метеостанции, м
        /// </summary>
        public double NearestMSDistance { get; }

        /// <summary>
        /// общее количество найденных метеостанций в заданной области
        /// </summary>
        public int TotalMeteostationsInArea { get; }

        /// <summary>
        /// максимальный радиус поиска МС из настроек
        /// </summary>
        public double NearestMsRadius { get; }

        /// <summary>
        /// координаты центральной точки поиска
        /// </summary>
        public PointLatLng CenterPoint { get; }

        /// <summary>
        /// Создает новый экземпляр класса 
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="maxCorrelKoeff">Максимальный коэффициент корреляции, найденный в списке МС</param>
        /// <param name="nearestMS">расстояние в м до ближайшей существующей МС</param>
        /// <param name="nearestMSRadius">максимальный радиус поиска метеостанций из настроек</param>
        /// <param name="totalMSs">общее количество МС в заданной области</param>
        /// <param name="centerPoint">координаты центральной точки поиска</param>
        /// <param name="minCorrlCoeff">минимальный допустимый коэфф корреляции из настроек</param>
        public GetBaseRangeException(double maxCorrelKoeff, double minCorrlCoeff, double nearestMS, int totalMSs, double nearestMSRadius, PointLatLng centerPoint)
            : base($"Не удалось найти ряд соседней метеостанции для восстановления. Возможно, неправильно заданы настройки:\r\n \r\n" +
                 $"Найдено метеостанций в заданной области: {totalMSs} \r\n" +
                 $"Ближайшая метеостанция находится в {(nearestMS / 1000).ToString("0")} км от заданной точки ({centerPoint.Lat.ToString("0.000")} {centerPoint.Lng.ToString("0.000")}),\r\n" +
                 $"Максимальный радиус поиска соседних метеостанций {(nearestMSRadius / 1000).ToString("0")} км.\r\n" + 
                 (maxCorrelKoeff != double.MinValue ? $"Максимальный коэффициент корреляции, среди наденных метеостанций: {maxCorrelKoeff.ToString("0.00")}, (минимальный допустимый {minCorrlCoeff.ToString("0.00")})" : ""))
        {
            MaxCorrelationCoefficient = maxCorrelKoeff;
            NearestMSDistance = nearestMS;
            NearestMsRadius = nearestMSRadius;
            TotalMeteostationsInArea = totalMSs;
            CenterPoint = centerPoint;
            MinCorellationCoefficient = minCorrlCoeff;
        }
    }
}
