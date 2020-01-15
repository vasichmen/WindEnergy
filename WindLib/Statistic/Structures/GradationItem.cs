using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Statistic.Structures
{
    /// <summary>
    /// диапазон одной градации
    /// </summary>
    public class GradationItem
    {
        private readonly double _from;
        private readonly double _to;
        private readonly bool notEmpty;

        public double From => _from;
        public double To => _to;
        public double Average => (From + To) / 2d;
        /// <summary>
        /// истина, если структура пустая
        /// </summary>
        public bool isEmpty { get { return !notEmpty; } }

        private GradationItem() { }

        public GradationItem(double from, double to)
        {
            _from = from;
            _to = to;
            notEmpty = true;
        }


        static GradationItem()
        { Empty = new GradationItem(); }

        /// <summary>
        /// пустое значение структуры
        /// </summary>
        public static readonly GradationItem Empty;
    }
}
