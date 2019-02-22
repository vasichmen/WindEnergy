using System.Collections.Generic;

namespace WindEnergy.Lib.Operations.Interpolation
{
    public interface IInterpolateMethod
    {
        double GetValue(double x);
        
    }
}