namespace WindEnergy.WindLib.Statistic.Structures
{
    /// <summary>
    /// информация об отклонениях относительно многолетних значений
    /// </summary>
    public class DeviationsInfo
    {
        /// <summary>
        /// среднеквадратичное отклонение скорости от многолетней
        /// </summary>
        public double SpeedDeviation { get; set; }

        /// <summary>
        /// среднеквадратичное отклонение повторяемости от многолетней
        /// </summary>
        public double ExpDeviation { get; set; }
    }
}
