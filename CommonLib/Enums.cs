using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{

    /// <summary>
    /// месяцы
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<Months>))]
    public enum Months
    {
        [Description("Все месяцы")]
        All = 0,
        [Description("Январь")]
        January = 1,
        [Description("Февраль")]
        February = 2,
        [Description("Март")]
        March = 3,
        [Description("Апрель")]
        April = 4,
        [Description("Май")]
        May = 5,
        [Description("Июнь")]
        June = 6,
        [Description("Июль")]
        July = 7,
        [Description("Август")]
        August = 8,
        [Description("Сентябрь")]
        September = 9,
        [Description("Октябрь")]
        October = 10,
        [Description("Ноябрь")]
        November = 11,
        [Description("Декабрь")]
        December = 12,
    }



    /// <summary>
    /// тип базы данных ETOPO
    /// </summary>
    public enum ETOPODBType
    {
        /// <summary>
        /// Float 
        /// </summary>
        Float,

        /// <summary>
        /// Int16
        /// </summary>
        Int16,

        /// <summary>
        /// SQLite
        /// </summary>
        SQLite
    }

    /// <summary>
    /// диалог обновления программы
    /// </summary>
    public enum UpdateDialogAnswer
    {
        /// <summary>
        /// всегда спрашивать
        /// </summary>
        AlwaysAsk,

        /// <summary>
        /// всегда игнорировать обновления
        /// </summary>
        AlwaysIgnore,

        /// <summary>
        /// всегда принимать обновления
        /// </summary>
        AlwaysAccept
    }

    /// <summary>
    /// все поддерживаемые форматы файлов
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<FileFormats>))]
    public enum FileFormats
    {
        /// <summary>
        /// формат файлов, полученных из архива rp5
        /// </summary>
        CSV,


        /// <summary>
        /// другой формат
        /// </summary>
        None,

        /// <summary>
        /// формат XLSX
        /// </summary>
        XLSX
    }

    /// <summary>
    /// Поставщики карты
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<MapProviders>))]
    public enum MapProviders
    {
        /// <summary>
        /// схема яндекса
        /// </summary>
        [Description("Схема Яндекс")]
        YandexMap,

        /// <summary>
        /// карта OSM
        /// </summary>
        [Description("Карта OSM")]
        OpenStreetMap,

        /// <summary>
        /// спуник Google
        /// </summary>
        [Description("Google спутник")]
        GoogleSatellite
    }
}
