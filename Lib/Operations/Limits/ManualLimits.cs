using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Structures;

namespace WindEnergy.Lib.Operations.Limits
{
    /// <summary>
    /// проверка значений по заданным вручную диапазонам
    /// </summary>
    public class ManualLimits : ILimitsProvider
    {
        /// <summary>
        /// координаты точки
        /// </summary>
        public PointLatLng Position { get; set; }

        /// <summary>
        /// название точки
        /// </summary>
        public string Name { get; set; }

        private readonly List<Diapason<double>> directionInclude;
        private readonly List<Diapason<double>> speedInclude;

        /// <summary>
        /// создает новый экземпляр с заданными допустимыми диапазонами
        /// </summary>
        /// <param name="directionInclude"></param>
        /// <param name="speedInclude"></param>
        public ManualLimits(List<Diapason<double>> directionInclude, List<Diapason<double>> speedInclude)
        {
            this.directionInclude = directionInclude;
            this.speedInclude = speedInclude;
        }

        public bool CheckItem(RawItem item, PointLatLng coordinates)
        {
            return check(speedInclude, item.Speed) && check(directionInclude, item.Direction);
        }

        private bool check(List<Diapason<double>> diapasons, double val)
        {
            if (diapasons.Count == 0) return true;
            foreach (Diapason<double> d in diapasons)
                if (d.From <= val && d.To >= val)
                    return true;
            return false;
        }
    }
}
