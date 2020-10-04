using System;

namespace CommonLib.Classes
{
    /// <summary>
    /// Исключения для WindEnergy
    /// </summary>
    [Serializable]
    public class WindEnergyException : ApplicationException
    {
        /// <summary>
        /// всплывающая подсказка
        /// </summary>
        public string ToolTip { get; }

        public object Reason { get; set; }

        /// <summary>
        /// Простое исключение с сообщением
        /// </summary>
        /// <param name="message"></param>
        public WindEnergyException(string message) : base(message)
        {
        }

        /// <summary>
        /// сообщение и описание
        /// </summary>
        /// <param name="message">сообщение </param>
        /// <param name="tooltip">описание проблемы</param>
        public WindEnergyException(string message, string tooltip) : base(message)
        {
            this.ToolTip = tooltip;
        }

        /// <summary>
        /// сообщение и описание
        /// </summary>
        /// <param name="message">сообщение </param>
        /// <param name="reason">причина проблемы</param>
        public WindEnergyException(string message, object reason) : base(message)
        {
            this.Reason = reason;
        }
    }

    public class WrongFileFormatException : WindEnergyException
    {
        /// <summary>
        /// Простое исключение с сообщением
        /// </summary>
        /// <param name="message"></param>
        public WrongFileFormatException(string message) : base(message)
        {
        }
    }
}
