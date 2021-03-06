﻿using GMap.NET;

namespace CommonLib.Classes.Base
{
    /// <summary>
    /// Универсальная информация о МС
    /// </summary>
    public abstract class BaseMeteostationInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Название МС
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Координаты
        /// </summary>
        public PointLatLng Position { get; set; }


    }
}
