using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Classes.Collections
{
    /// <summary>
    /// таблица, зависящая от месяца и часа суток
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class DataMonthsHour<T>:DataMonths<DataHours<T>>
    {
    }
}
