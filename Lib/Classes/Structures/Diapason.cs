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
    public struct Diapason
    {
        /// <summary>
        /// начало
        /// </summary>
        public double From { get; set; }

        /// <summary>
        /// конец
        /// </summary>
        public double To { get; set; }

        public override string ToString()
        {
            return "От: " + From.ToString() + " до: " + To.ToString();
        }
    }
}
