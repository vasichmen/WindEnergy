using CommonLib;
using System.ComponentModel;

namespace WindEnergy
{

    /// <summary>
    /// Способ поиска метеостанций (онлайн или из БД)
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<RP5SearchEngine>))]
    public enum RP5SearchEngine
    {
        /// <summary>
        /// Онлайн поиск по API
        /// </summary>
        [Description("Онлайн поиск")]
        OnlineAPI,

        /// <summary>
        /// поиск по локальной БД
        /// </summary>
        [Description("БД Метеостанции мира")]
        DBSearch
    }

    /// <summary>
    /// типы источников среднемноголетних показателей степени m
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<HellmanCoefficientSource>))]
    public enum HellmanCoefficientSource
    {
        /// <summary>
        /// Поиск АМС-аналога
        /// </summary>
        [Description("Поиск АМС-аналога")]
        AMSAnalog,

        /// <summary>
        /// заданный вручную, один
        /// </summary>
        [Description("Коэффициент m задан вручную")]
        CustomOne,

        /// <summary>
        /// заданные вручную по месяцам
        /// </summary>
        [Description("Коэффициенты m заданы вручную по месяцам")]
        CustomMonths,

        /// <summary>
        /// не задано
        /// </summary>
        [Description("Настройки не определены")]
        None,
    }

    /// <summary>
    /// Способ поиска метеостанций (онлайн или из БД)
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<RP5SourceType>))]
    public enum RP5SourceType
    {
        /// <summary>
        /// Онлайн поиск по API
        /// </summary>
        [Description("Онлайн поиск")]
        OnlineAPI,

        /// <summary>
        /// поиск по локальной БД
        /// </summary>
        [Description("Локальная БД Расписание Погоды")]
        LocalDBSearch
    }

    /// <summary>
    /// направления ветра по 16 румбам
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<WindDirections16>))]
    public enum WindDirections16
    {

        /// <summary>
        /// нет данных
        /// </summary>
        [Description("Нет данных")]
        Undefined = -1,

        /// <summary>
        /// ветер с севера
        /// </summary>
        [Description("С")]
        N = 0,

        /// <summary>
        /// ветер с северо северо востока
        /// </summary>
        [Description("ССВ")]
        NNE = 1,

        /// <summary>
        /// ветер с северо  востока
        /// </summary>
        [Description("СВ")]
        NE = 2,

        /// <summary>
        /// ветер с северо востока востока
        /// </summary>
        [Description("СВВ")]
        NEE = 3,

        /// <summary>
        /// ветер с востока
        /// </summary>
        [Description("В")]
        E = 4,

        /// <summary>
        /// ветер с юго востока востока
        /// </summary>
        [Description("ЮВВ")]
        SEE = 5,

        /// <summary>
        /// ветер с юго востока 
        /// </summary>
        [Description("ЮВ")]
        SE = 6,

        /// <summary>
        /// ветер с  юго юго востока
        /// </summary>
        /// 
        [Description("ЮЮВ")]
        SSE = 7,

        /// <summary>
        /// ветер с юга
        /// </summary>
        [Description("Ю")]
        S = 8,

        /// <summary>
        /// ветер с юго юго запада
        /// </summary>
        [Description("ЮЮЗ")]
        SSW = 9,

        /// <summary>
        /// ветер с юго  запада
        /// </summary>
        [Description("ЮЗ")]
        SW = 10,

        /// <summary>
        /// ветер с  юго запада запада
        /// </summary>
        [Description("ЮЗЗ")]
        SWW = 11,

        /// <summary>
        /// ветер с запада
        /// </summary>
        [Description("З")]
        W = 12,

        /// <summary>
        /// ветер с северо запада запада
        /// </summary>
        [Description("СЗЗ")]
        NWW = 13,

        /// <summary>
        /// ветер с северо запада 
        /// </summary>
        [Description("СЗ")]
        NW = 14,

        /// <summary>
        /// ветер с северо северо запада
        /// </summary>
        [Description("ССЗ")]
        NNW = 15,

        /// <summary>
        /// Переменное направление
        /// </summary>
        [Description("Переменное направление")]
        Variable = -2,

        /// <summary>
        /// штиль
        /// </summary>
        [Description("Штиль")]
        Calm = -3
    }

    /// <summary>
    /// направления ветра по 8 румбам
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<WindDirections8>))]
    public enum WindDirections8
    {

        /// <summary>
        /// нет данных
        /// </summary>
        [Description("Нет данных")]
        Undefined = -1,

        /// <summary>
        /// ветер с севера
        /// </summary>
        [Description("С")]
        N = 0,

        /// <summary>
        /// ветер с северо  востока
        /// </summary>
        [Description("СВ")]
        NE = 1,

        /// <summary>
        /// ветер с востока
        /// </summary>
        [Description("В")]
        E = 2,

        /// <summary>
        /// ветер с юго востока 
        /// </summary>
        [Description("ЮВ")]
        SE = 3,

        /// <summary>
        /// ветер с юга
        /// </summary>
        [Description("Ю")]
        S = 4,

        /// <summary>
        /// ветер с юго  запада
        /// </summary>
        [Description("ЮЗ")]
        SW = 5,

        /// <summary>
        /// ветер с запада
        /// </summary>
        [Description("З")]
        W = 6,

        /// <summary>
        /// ветер с северо запада 
        /// </summary>
        [Description("СЗ")]
        NW = 7,

        /// <summary>
        /// Переменное направление
        /// </summary>
        [Description("Переменное направление")]
        Variable = -2,

        /// <summary>
        /// штиль
        /// </summary>
        [Description("Штиль")]
        Calm = -3
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
        UnofficialMeteostation,

        /// <summary>
        /// неизвестный
        /// </summary>
        [Description("Неизвестный")]
        None
    }

    /// <summary>
    /// методы интерполяции
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<InterpolateMethods>))]
    public enum InterpolateMethods
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
        Stepwise,

        /// <summary>
        /// линейная корреляция с ближайшей метеостанцией
        /// </summary>
        [Description("Линейная корреляция с ближайшей метеостанцией")]
        NearestMeteostation
    }

    /// <summary>
    /// источники ограничений для параметров
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<LimitsProviders>))]
    public enum LimitsProviders
    {
        /// <summary>
        /// стандартные ограничения по регионам
        /// </summary>
        [Description("Стандартные ограничения по регионам")]
        StaticLimits,

        /// <summary>
        /// источник не выбран
        /// </summary>
        [Description("Источник не выбран")]
        None,

        /// <summary>
        /// ручной режим
        /// </summary>
        [Description("Пользовательские ограничения")]
        Manual
    }

    /// <summary>
    /// метеорологические параметры, хранящиеся в рядах наблюдений
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<MeteorologyParameters>))]
    public enum MeteorologyParameters
    {
        /// <summary>
        /// Направление
        /// </summary>
        [Description("Направление")]
        Direction,

        /// <summary>
        /// Скорость
        /// </summary>
        [Description("Скорость")]
        Speed,

        /// <summary>
        /// Температура
        /// </summary>
        [Description("Температура")]
        Temperature,

        /// <summary>
        /// Влажность
        /// </summary>
        [Description("Влажность")]
        Wetness,

        /// <summary>
        /// Давление
        /// </summary>
        [Description("Давление")]
        Pressure
    }

    /// <summary>
    /// типы градаций для повторяемости скорости ветра
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<GradationTypes>))]
    public enum GradationTypes
    {
        [Description("NASA")]
        NASA,

        [Description("ГГО им. А.И. Воейкова")]
        Voeykow,

        [Description("Шкала Бофорта")]
        Bofort,

        [Description("Пользовательский шаг градации")]
        User

    }

    #region Импорт из текста, единицы измерения

    /// <summary>
    /// Единицы измерений направления
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<DirectionUnits>))]
    public enum DirectionUnits
    {
        /// <summary>
        /// текст расписания погоды
        /// </summary>
        [Description("Текст РП5")]
        TextRP5,

        /// <summary>
        /// градусы
        /// </summary>
        [Description("Градусы")]
        Degrees,

        [Description("Не выбраны")]
        None
    }

    /// <summary>
    /// единицы измерения давления
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<PressureUnits>))]
    public enum PressureUnits
    {
        /// <summary>
        /// КПа
        /// </summary>
        [Description("КПа")]
        KPa,

        /// <summary>
        /// мм рт. ст.
        /// </summary>
        [Description("мм рт. ст.")]
        mmHgArt,

        [Description("Не выбраны")]
        None
    }

    /// <summary>
    /// единицы измерения давления
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<WetnessUnits>))]
    public enum WetnessUnits
    {
        /// <summary>
        /// Проценты
        /// </summary>
        [Description("Проценты")]
        Percents,

        /// <summary>
        /// Доли
        /// </summary>
        [Description("Доли")]
        Parts,

        [Description("Не выбраны")]
        None
    }

    /// <summary>
    /// Поля для импорта
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<ImportFields>))]
    public enum ImportFields
    {
        /// <summary>
        /// Дата
        /// </summary>
        [Description("Дата")]
        Date,

        /// <summary>
        /// Направление
        /// </summary>
        [Description("Направление")]
        Direction,

        /// <summary>
        /// Скорость
        /// </summary>
        [Description("Скорость")]
        Speed,

        /// <summary>
        /// Температура
        /// </summary>
        [Description("Температура")]
        Temperature,

        /// <summary>
        /// Давление
        /// </summary>
        [Description("Давление")]
        Pressure,

        /// <summary>
        /// Влажность
        /// </summary>
        [Description("Влажность")]
        Wetness,

    }
    #endregion

    /// <summary>
    /// Типы регулирования турбин
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<TurbineRegulations>))]
    public enum TurbineRegulations
    {
        [Description("Pitch")]
        Pitch,

        [Description("Stall")]
        Stall,

        [Description("Fixed")]
        Fixed,

        [Description("Неизвестно")]
        None,
    }

    /// <summary>
    /// Два типа рельефа
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<TerrainType>))]
    public enum TerrainType
    {

        Macro,

        Micro,

        Meso
    }

    /// <summary>
    /// расстояния до водной поверхности
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<WaterDistanceType>))]
    public enum WaterDistanceType
    {
        [Description("Не выбрано")]
        Undefined = -1,

        [Description("Вдали от водных поверхностей")]
        FarFromWater = 7,

        [Description("Берег большой реки")]
        River = 8,

        [Description("Берег большого озера или залива")]
        Lake = 9,

        [Description("Побережье внутреннего моря")]
        SeaCoast = 10,

        [Description("Побережье океана или внешнего моря")]
        OceanCoast = 11
    }

    /// <summary>
    /// типы стратификации атмосферы
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<WaterDistanceType>))]
    public enum AtmosphereStratification
    {
        /// <summary>
        /// устойчивая
        /// </summary>
        [Description("Устойчивая")]
        Stable,

        /// <summary>
        /// неустойчивая
        /// </summary>
        [Description("Неустойчивая")]
        Unstable
    }

    /// <summary>
    /// варианты скорости для NASA
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<NasaWindSpeedHeight>))]
    public enum NasaWindSpeedHeight
    {
        [Description("Средняя скорость на высоте 50м")]
        WS50M,

        [Description("Средняя скорость на высоте 10м")]
        WS10M,

        [Description("Минимальная скорость на высоте 50м")]
        WS50M_MIN,

        [Description("Минимальная скорость на высоте 10м")]
        WS10M_MIN,

        [Description("Максимальная скорость на высоте 50м")]
        WS50M_MAX,

        [Description("Максимальная скорость на высоте 10м")]
        WS10M_MAX
    }
}