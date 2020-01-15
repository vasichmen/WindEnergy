﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy
{

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
    [TypeConverter(typeof(EnumTypeConverter<WindDirections>))]
    public enum WindDirections
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
}