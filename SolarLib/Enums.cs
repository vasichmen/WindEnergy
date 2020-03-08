using CommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
