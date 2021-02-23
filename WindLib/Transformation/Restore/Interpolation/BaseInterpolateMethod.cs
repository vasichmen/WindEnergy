using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Transformation.Restore.Interpolation
{
   public abstract class BaseInterpolateMethod: IInterpolateMethod
    {
        protected StandartIntervals FromInterval;
        protected StandartIntervals ToInterval;

        protected  BaseInterpolateMethod(StandartIntervals from, StandartIntervals to)
        {
            FromInterval = from;
            ToInterval = to;
        }

        public abstract double GetValue(double x);
    }
}
