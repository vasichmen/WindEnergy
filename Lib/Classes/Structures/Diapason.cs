using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Classes.Structures
{
    /// <summary>
    /// 
    /// </summary>
    public struct Diapason<T>
    {
        public Diapason(T from, T to) : this()
        {
            this.From = from;
            this.To = to;
        }

        /// <summary>
        /// начало
        /// </summary>
        public T From { get; set; }

        /// <summary>
        /// конец
        /// </summary>
        public T To { get; set; }


        public override string ToString()
        {
            return "От: " + From.ToString() + " до: " + To.ToString();
        }
    }
}
