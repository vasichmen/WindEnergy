﻿using GMap.NET;
using WindEnergy.Lib.Classes.Structures;

namespace WindEnergy.Lib.Operations.Limits
{
    /// <summary>
    /// представляет основные методы проверки данных
    /// </summary>
    interface ILimitsProvider
    {
        /// <summary>
        /// возвращает истину, если значение допустимо в этой точке
        /// </summary>
        /// <param name="item">данные для провtрки</param>
        /// <param name="coordinates">координаты точки</param>
        /// <returns></returns>
        bool CheckItem(RawItem item, PointLatLng coordinates);
    }
}