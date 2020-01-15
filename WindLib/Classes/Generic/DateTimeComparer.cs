using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Classes.Generic
{
    /// <summary>
    /// компаратор DataItem для упорядочивания по возрастанию дат ряда
    /// </summary>
    public class DateTimeComparerRawItem : IComparer<RawItem>
    {
        /// <summary>
        /// сравнение дат в ряде
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(RawItem x, RawItem y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }

}
