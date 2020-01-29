using CommonLib;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Classes.Structures
{
    /// <summary>
    /// Информация о метеостанции из БД АМС
    /// </summary>
    public class AMSMeteostationInfo: BaseMeteostationInfo
    {
        /// <summary>
        /// коэффициенты m по месяцам 
        /// </summary>
        public Dictionary<Months, double> m { get; set; }

        /// <summary>
        /// Средние скорости на высоте 10м по месяцам
        /// </summary>
        public Dictionary<Months, double> V10 { get; set; }

        /// <summary>
        /// среднее значение коэффициента m за год
        /// </summary>
        public double AverageM { get; set; }
    }
}
