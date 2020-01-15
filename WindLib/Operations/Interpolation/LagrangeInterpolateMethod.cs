using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Operations.Interpolation
{
    /// <summary>
    /// интерполяция полиномом лагранжа
    /// </summary>
    public class LagrangeInterpolateMethod : IInterpolateMethod
    {
        public LagrangeInterpolateMethod()
        { }

        public double GetValue(double x)
        {
            throw new NotImplementedException();
        }


        private static double lagrange(double x, double[] xd, double[] yd)
        {
            if (xd.Length != yd.Length)
            {
                throw new ArgumentException("Arrays must be of equal length."); //$NON-NLS-1$
            }
            double sum = 0;
            for (int i = 0, n = xd.Length; i < n; i++)
            {
                if (x - xd[i] == 0)
                {
                    return yd[i];
                }
                double product = yd[i];
                for (int j = 0; j < n; j++)
                {
                    if ((i == j) || (xd[i] - xd[j] == 0))
                    {
                        continue;
                    }
                    product *= (x - xd[i]) / (xd[i] - xd[j]);
                }
                sum += product;
            }
            return sum;
        }
    }
}
