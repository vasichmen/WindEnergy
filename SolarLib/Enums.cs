using CommonLib;
using System.ComponentModel;

namespace SolarEnergy.SolarLib
{
    /// <summary>
    /// метеорологические параметры, хранящиеся в рядах наблюдений
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<MeteorologyParameters>))]
    public enum MeteorologyParameters
    {
        /// <summary>
        /// Направление
        /// </summary>
        [Description("Полная радиация")]
        AllSkyInsolation,

        /// <summary>
        /// Скорость
        /// </summary>
        [Description("Прямая радиация")]
        ClearSkyInsolation,

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
    /// типы переходов от значения одного месяца к другому
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<MonthTransformationModels>))]
    public enum MonthTransformationModels
    {
        [Description("Не выбрано")]
        None,

        [Description("Ступенчатый переход")]
        Constant,

        [Description("Линейная интерполяция")]
        LinearInterpolation,


    }

    /// <summary>
    /// типы исходных данных для NASA
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<NasaSourceTypes>))]
    public enum NasaSourceTypes
    {
        [Description("Не выбрано")]
        None,

        [Description("Только выбранный год")]
        SelectedYear,

        [Description("Средние значения для всего периода")]
        AllPeriod,

        [Description("Минимальные значения для каждого дня")]
        Minimal,

        [Description("Максимальные значения для каждого дня")]
        Maximal
    }

    /// <summary>
    /// Модели распркделения радиации по часам суток
    /// </summary>
    [TypeConverter(typeof(EnumTypeConverter<HourModels>))]
    public enum HourModels
    {
        [Description("Не выбрано")]
        None,

        [Description("Равномерное распределение")]
        Uniform
    }
}
