using GMap.NET;
using System;
using System.Collections.Generic;

namespace CommonLibLib.Data.Interfaces
{
    /// <summary>
    /// геокодер
    /// </summary>
    public interface IGeocoderProvider
    {

        /// <summary>
        /// получить список адресов по части адреса
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Dictionary<string, PointLatLng> GetAddresses(string query);

        /// <summary>
        /// получить адрес по заданным координатам
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        string GetAddress(PointLatLng coordinate);

        /// <summary>
        /// получить координаты по адресу
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        PointLatLng GetCoordinate(string address);

        /// <summary>
        /// узнать часовой пояс и временную зону по координатам
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        TimeZoneInfo GetTimeZone(PointLatLng coordinate);
    }
}