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

        public Dictionary<Months, double> RelativeSpeeds { get {
                if (_relativeSpeeds == null)
                    _relativeSpeeds = CalcRelatives();
                return _relativeSpeeds;
            } }

        private Dictionary<Months, double> _relativeSpeeds = null;

        /// <summary>
        /// расчет относительных скоростей
        /// </summary>
        /// <returns></returns>
        private Dictionary<Months, double> CalcRelatives()
        {
            double aver = V10.Values.Average();
            Dictionary<Months, double> res = new Dictionary<Months, double>();
            foreach (Months month in V10.Keys)
                res.Add(month, V10[month] / aver);
            return res;
        }
    }
}
