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
            this.directionInclude = directionInclude!=null?directionInclude:new List<Diapason<double>>();
            this.speedInclude = speedInclude!=null ? speedInclude : new List<Diapason<double>>();
        }

        public bool CheckItem(RawItem item, PointLatLng coordinates)
        {
            return check(speedInclude, item.Speed) && check(directionInclude, item.Direction);
        }

        /// <summary>
        /// если коллекция пустая, то возвращает trur
        /// </summary>
        /// <param name="diapasons">коллекция диапазонов</param>
        /// <param name="val">проверяемое значение</param>
        /// <returns></returns>
        private bool check(List<Diapason<double>> diapasons, double val)
        {
            if (diapasons.Count == 0) return true;
            foreach (Diapason<double> d in diapasons)
                if (d.From <= val && d.To >= val)
                    return true;
            return false;
        }

        /// <summary>
        /// возвращает минимальное ограничение для заданого параметра
        /// </summary>
        /// <param name="paramter">тип параметра</param>
        /// <returns></returns>
        internal object GetMinimal(MeteorologyParameters paramter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// возвращает максимальное ограничение для заданого параметра
        /// </summary>
        /// <param name="paramter">тип параметра</param>
        /// <returns></returns>
        internal object GetMaximal(MeteorologyParameters paramter)
        {
            throw new NotImplementedException();
        }
    }
}
