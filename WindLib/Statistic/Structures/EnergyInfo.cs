using System;

namespace WindEnergy.WindLib.Statistic.Structures
{
    /// <summary>
    /// энергетические характеристики ветра
    /// </summary>
    public class EnergyInfo
    {
        /// <summary>
        /// период, за который получены эти данные
        /// </summary>
        public TimeSpan Period { get { return ToDate - FromDate; } }

        /// <summary>
        /// дата первого измерения в ряде
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// дата последнего измерения в ряде
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// средняя скорость за период времени м/с
        /// </summary>
        public double V0 { get; set; }

        /// <summary>
        /// коэффициент вариации 
        /// </summary>
        public double Cv { get; set; }

        /// <summary>
        /// удельная мощность, Вт/м2
        /// </summary>
        public double PowerDensity { get; set; }

        /// <summary>
        /// удельная энергия Вт*ч/м2
        /// </summary>
        public double EnergyDensity { get; set; }

        /// <summary>
        /// среднеквадратическое отклонение скорости, м/с
        /// </summary>
        public double StandardDeviationSpeed { get; set; }

        /// <summary>
        /// максимальная скорость за период времени, м/м
        /// </summary>
        public double Vmax { get; set; }

        /// <summary>
        /// минимальная скорость, м/с
        /// </summary>
        public double Vmin { get; set; }

        /// <summary>
        /// параметр распределения вейбулла гамма
        /// </summary>
        public double VeybullGamma { get; set; }

        /// <summary>
        /// параметр распределения вейбулла бета, м/с
        /// </summary>
        public double VeybullBeta { get; set; }

        /// <summary>
        /// Экстремальна скорость ветра за 50 лет, м/с
        /// </summary>
        public double ExtremalSpeed { get; set; }

        /// <summary>
        /// отклонение повторяемости скорости ветра от многолетней в долях
        /// </summary>
        public double ExpectancyDeviation { get; set; }
    }
}
