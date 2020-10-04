using CommonLib;
using System.ComponentModel;


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

/// <summary>
/// Интервалы наблюдений. При приведении к int - время в минутах
/// </summary>
[TypeConverter(typeof(EnumTypeConverter<StandartIntervals>))]
public enum StandartIntervals
{
    /// <summary>
    /// пропущенные данные
    /// </summary>
    [Description("Пропуск данных")]
    Missing = int.MaxValue,

    /// <summary>
    /// переменный интервал (ряд не однородный)
    /// </summary>
    [Description("Неоднородный ряд")]
    Variable = -1,

    /// <summary>
    /// 10 минут
    /// </summary>
    [Description("10 минут")]
    M10 = 10,

    /// <summary>
    /// 30 минут
    /// </summary>
    [Description("30 минут")]
    M30 = 30,

    /// <summary>
    /// 1 час
    /// </summary>
    [Description("1 час")]
    H1 = 60,

    /// <summary>
    /// 3 часа
    /// </summary>
    [Description("3 часа")]
    H3 = 180,

    /// <summary>
    /// 6 часов
    /// </summary>
    [Description("6 часов")]
    H6 = 360,

    /// <summary>
    /// 8 часов
    /// </summary>
    [Description("8 часов")]
    H8 = 480,

    /// <summary>
    /// 8 часов
    /// </summary>
    [Description("12 часов")]
    H12 = 720,

    /// <summary>
    /// 1 день
    /// </summary>
    [Description("1 день")]
    D1 = 24 * 60,
}

/// <summary>
/// типы ошибок при загрузке данных
/// </summary>
[TypeConverter(typeof(EnumTypeConverter<ErrorReason>))]
public enum ErrorReason
{
    #region Ошибки РП5

    /// <summary>
    /// Для этого id нет архива погоды
    /// </summary>
    FS004,

    /// <summary>
    /// Ошибка в исходных данных
    /// </summary>
    FS002,

    /// <summary>
    /// Ошибка авторизации
    /// </summary>
    FS000,

    /// <summary>
    /// Неправильный метод запроса. Ожидается POST
    /// </summary>
    FS001,

    /// <summary>
    /// Время жизни статистики истекло для этой сессии
    /// </summary>
    FM000,

    /// <summary>
    /// Превышено количество запросов
    /// </summary>
    FM004,

    #endregion

}

