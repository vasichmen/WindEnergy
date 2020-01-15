using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Classes.Structures
{
    /// <summary>
    /// диапазон измерений с одинаковым интервалом
    /// </summary>
    public class RangeInterval
    {
        /// <summary>
        /// начальная и конечная дата измерений
        /// </summary>
        public Diapason<DateTime> Diapason { get; set; }

        /// <summary>
        /// интервал измерений
        /// </summary>
        public StandartIntervals Interval { get; set; }

        /// <summary>
        /// длина диапазона
        /// </summary>
        public TimeSpan Length { get { return this.Diapason.To - this.Diapason.From; } }

        /// <summary>
        /// длительность диапазона для вывода в статистике
        /// </summary>
        public string LengthString { get {
                return Length.ToText(true);
            } }

        /// <summary>
        /// длина диапазона в минутах
        /// </summary>
        public int LengthMinutes { get {
                return (int)Length.TotalMinutes;
            } }

        public override string ToString()
        {
            return Diapason.ToString() + "  " + Interval.Description();
        }
    }
}
