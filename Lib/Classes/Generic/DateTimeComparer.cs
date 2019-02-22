using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Structures;

namespace WindEnergy.Lib.Classes.Generic
{
    /// <summary>
    /// компаратор DataItem для упорядочивания по возрастанию дат ряда
    /// </summary>
    public class DateTimeComparer : IComparer<RawItem>
    {
        public int Compare(RawItem x, RawItem y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }
}
