using CommonLib.Classes.Base;
using System.Collections.Generic;

namespace CommonLib.Classes.Collections.Generic
{
    /// <summary>
    /// компаратор DataItem для упорядочивания по возрастанию дат ряда
    /// </summary>
    public class DateTimeComparerRawItem : IComparer<RawItemBase>
    {
        /// <summary>
        /// сравнение дат в ряде
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(RawItemBase x, RawItemBase y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }

}
