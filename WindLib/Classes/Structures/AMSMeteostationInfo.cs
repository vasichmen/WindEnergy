using CommonLib.Classes.Base;
using System.Collections.Generic;
using System.Linq;

namespace WindEnergy.WindLib.Classes.Structures
{
    /// <summary>
    /// Информация о метеостанции из БД АМС
    /// </summary>
    public class AMSMeteostationInfo : BaseMeteostationInfo
    {
        /// <summary>
        /// коэффициенты m по месяцам 
        /// </summary>
        public Dictionary<Months, double> m { get; set; }

        /// <summary>
        /// коэффциент модели а
        /// </summary>
        public double b { get; internal set; }

        /// <summary>
        /// коэффициент модели b
        /// </summary>
        public double a { get; internal set; }

        /// <summary>
        /// коэффициент модели R
        /// </summary>
        public double R { get; internal set; }

        /// <summary>
        /// Средние скорости на высоте 10м по месяцам
        /// </summary>
        public Dictionary<Months, double> V10 { get; set; }

        /// <summary>
        /// Средние скорости на высоте 100м по месяцам
        /// </summary>
        public Dictionary<Months, double> V100 { get; set; }

        /// <summary>
        /// Средние скорости на высоте 200м по месяцам
        /// </summary>
        public Dictionary<Months, double> V200 { get; set; }

        /// <summary>
        /// среднее значение коэффициента m за год
        /// </summary>
        public double AverageM { get; set; }

        /// <summary>
        /// относительные скорости на высоте 10м
        /// </summary>
        public Dictionary<Months, double> RelativeSpeeds
        {
            get
            {
                if (_relativeSpeeds == null)
                    _relativeSpeeds = calcRelatives();
                return _relativeSpeeds;
            }
        }
        private Dictionary<Months, double> _relativeSpeeds = null;


        /// <summary>
        /// расчет относительных скоростей
        /// </summary>
        /// <returns></returns>
        private Dictionary<Months, double> calcRelatives()
        {
            double aver = V10.Values.Average();
            Dictionary<Months, double> res = new Dictionary<Months, double>();
            foreach (Months month in V10.Keys)
                res.Add(month, V10[month] / aver);
            return res;
        }
    }
}
