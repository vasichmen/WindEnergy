using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Operations.Interpolation
{
    public class LinearInterpolateMethod : IInterpolateMethod
    {
        private readonly Dictionary<double, double> values;
        private List<double> sortedX;

        /// <summary>
        /// сохдаёт новый экземпляр с заданной функцией
        /// </summary>
        /// <param name="funct"></param>
        public LinearInterpolateMethod(Dictionary<double, double> funct)
        {
            this.values = new Dictionary<double, double>();
            foreach (var kv in funct)
                if (!double.IsNaN(kv.Value))
                    values.Add(kv.Key, kv.Value);
            
            sortedX = values.Keys.ToList();
            sortedX.Sort();
        }

        /// <summary>
        /// получить значение функции по заданному аргументу
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double GetValue(double x)
        {
            double res = double.NaN;
            if (values.ContainsKey(x))
                res= values[x];

            if (x > sortedX[sortedX.Count - 1] || x < sortedX[0]) //если х выходит за границы диапазона функции, то ошибка
                throw new ArgumentOutOfRangeException("Значение х должно быть внутри диапазона функции");
            for (int i = 1; i < sortedX.Count; i++)
                if (sortedX[i] > x)
                {
                    res = linInterpolate(sortedX[i - 1], sortedX[i], x);
                    break;
                }
            return res;
        }

        /// <summary>
        /// интерполяция между заданными точками
        /// </summary>
        /// <param name="x1">левый известнтый х</param>
        /// <param name="x3">правый известный х</param>
        /// <param name="x2">искомый агрумент </param>
        /// <returns></returns>
        private double linInterpolate(double x1, double x2, double x)
        {
            double y1 = values[x1];
            double y2 = values[x2];
            double y = y2 + ((y1 - y2) / (x1 - x2)) * (x - x2);
            return y;
        }
    }
}
