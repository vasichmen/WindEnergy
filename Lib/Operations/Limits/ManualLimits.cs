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
        internal double GetMinimal(MeteorologyParameters paramter)
        {
            switch (paramter)
            {
                case MeteorologyParameters.Direction:
                    double min = directionInclude.Min(new Func<Diapason<double>, double>((diapason)=>{
                        return diapason.From;
                    }));
                    return min;
                case MeteorologyParameters.Speed:
                    double mins = speedInclude.Min(new Func<Diapason<double>, double>((diapason) => {
                        return diapason.From;
                    }));
                    return mins;
                default: throw new Exception("Этот параметр не реализован");
            }
        }

        /// <summary>
        /// возвращает максимальное ограничение для заданого параметра
        /// </summary>
        /// <param name="paramter">тип параметра</param>
        /// <returns></returns>
        internal double GetMaximal(MeteorologyParameters paramter)
        {
            switch (paramter)
            {
                case MeteorologyParameters.Direction:
                    double max = directionInclude.Max(new Func<Diapason<double>, double>((diapason) => {
                        return diapason.To;
                    }));
                    return max;
                case MeteorologyParameters.Speed:
                    double maxs = speedInclude.Max(new Func<Diapason<double>, double>((diapason) => {
                        return diapason.To;
                    }));
                    return maxs;
                default: throw new Exception("Этот параметр не реализован");
            }
        }
    }
}
