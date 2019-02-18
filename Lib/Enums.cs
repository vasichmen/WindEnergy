using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy
{
    /// <summary>
    /// направления ветра по 16 румбам
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<WindDirections>))]
    public enum WindDirections
    {

        /// <summary>
        /// нет данных
        /// </summary>
        [Description("Нет данных")]
        Undefined,

        /// <summary>
        /// ветер с севера
        /// </summary>
        [Description("С")]
        N,

        /// <summary>
        /// ветер с северо северо востока
        /// </summary>
        [Description("ССВ")]
        NNE,

        /// <summary>
        /// ветер с северо  востока
        /// </summary>
        [Description("СВ")]
        NE,

        /// <summary>
        /// ветер с северо востока востока
        /// </summary>
        [Description("СВВ")]
        NEE,

        /// <summary>
        /// ветер с востока
        /// </summary>
        [Description("В")]
        E,

        /// <summary>
        /// ветер с юго востока востока
        /// </summary>
        [Description("ЮВВ")]
        SEE,

        /// <summary>
        /// ветер с юго востока 
        /// </summary>
        [Description("ЮВ")]
        SE,

        /// <summary>
        /// ветер с  юго юго востока
        /// </summary>
        /// 
        [Description("ЮЮВ")]
        SSE,

        /// <summary>
        /// ветер с юга
        /// </summary>
        [Description("Ю")]
        S,

        /// <summary>
        /// ветер с юго юго запада
        /// </summary>
        [Description("ЮЮЗ")]
        SSW,

        /// <summary>
        /// ветер с юго  запада
        /// </summary>
        [Description("ЮЗ")]
        SW,

        /// <summary>
        /// ветер с  юго запада запада
        /// </summary>
        [Description("ЮЗЗ")]
        SWW,

        /// <summary>
        /// ветер с запада
        /// </summary>
        [Description("З")]
        W,

        /// <summary>
        /// ветер с северо запада запада
        /// </summary>
        [Description("СЗЗ")]
        NWW,

        /// <summary>
        /// ветер с северо запада 
        /// </summary>
        [Description("СЗ")]
        NW,

        /// <summary>
        /// ветер с северо северо запада
        /// </summary>
        [Description("ССЗ")]
        NNW,

        /// <summary>
        /// Переменное направление
        /// </summary>
        [Description("Переменное направление")]
        Variable
    }

    /// <summary>
    /// тип источника информации о погоде
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<MeteoSourceType>))]
    public enum MeteoSourceType
    {
        /// <summary>
        /// метеостанция
        /// </summary>
        [Description("Метеостанция")]
        Meteostation,

        /// <summary>
        /// аэропорт
        /// </summary>
        [Description("Аэропорт")]
        Airport,

        /// <summary>
        /// неофициальная метеостанция
        /// </summary>
        [Description("Неофициальная метеостанция")]
        UnofficialMeteostation
    }

    /// <summary>
    /// все поддерживаемые форматы файлов
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<FileFormats>))]
    public enum FileFormats
    {
        /// <summary>
        /// формат файлов, полученных из архива rp5 для аэропорта
        /// </summary>
        RP5MetarCSV,

        /// <summary>
        /// формат файлов, полученных из архива rp5 для метеостанции
        /// </summary>
        RP5WmoCSV,

        /// <summary>
        /// другой формат
        /// </summary>
        None
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

    /// <summary>
    /// методы интерполяции
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<InterpolateMathods>))]
    public enum InterpolateMathods
    {
        /// <summary>
        /// линейная интерполяция
        /// </summary>
        [Description("Линейная интерполяция")]
        Linear,

        /// <summary>
        /// ступенчатая
        /// </summary>
        [Description("Ступенчатое восстановление")]
        Stepwise
    }

    /// <summary>
    /// Интервалы восстановления ряда
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<InterpolateIntervals>))]
    public enum InterpolateIntervals
    {
        /// <summary>
        /// 10 минут
        /// </summary>
        [Description("10 минут")]
        M10,

        /// <summary>
        /// 30 минут
        /// </summary>
        [Description("30 минут")]
        M30,

        /// <summary>
        /// 1 час
        /// </summary>
        [Description("1 час")]
        H1,

        /// <summary>
        /// 3 часа
        /// </summary>
        [Description("3 часа")]
        H3,

        /// <summary>
        /// 6 часов
        /// </summary>
        [Description("6 часов")]
        H6,

        /// <summary>
        /// 1 день
        /// </summary>
        [Description("1 день")]
        D1
    }

    /// <summary>
    /// источники ограничений для параметров
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<LimitsProviders>))]
    public enum LimitsProviders
    {
        /// <summary>
        /// источник не выбран
        /// </summary>
        [Description("Источник не выбран")]
        None
    }
}