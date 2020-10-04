namespace WindEnergy.WindLib.Classes.Structures.Options
{
    /// <summary>
    /// настройки пользовательской градации
    /// </summary>
    public class UserGradation
    {
        public UserGradation()
        {
            From = 0;
            To = 100;
            Step = 10;
        }

        /// <summary>
        /// начальное значение
        /// </summary>
        public double From { get; set; }

        /// <summary>
        /// конечное значение
        /// </summary>
        public double To { get; set; }

        /// <summary>
        /// шаг
        /// </summary>
        public double Step { get; set; }
    }
}
