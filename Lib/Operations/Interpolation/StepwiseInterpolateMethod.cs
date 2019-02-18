using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Operations.Interpolation
{
    /// <summary>
    /// ступенчатая интерполяция
    /// </summary>
    public class StepwiseInterpolateMethod : IInterpolateMethod
    {
        private readonly Dictionary<double, double> values;
        private List<double> sortedX;

        /// <summary>
        /// сохдаёт новый экземпляр с заданной функцией
        /// </summary>
        /// <param name="funct"></param>
        public StepwiseInterpolateMethod(Dictionary<double, double> funct)
        {
            this.values = funct;
            sortedX = funct.Keys.ToList();
            sortedX.Sort();
        }

        /// <summary>
        /// получить значение функции по заданному аргументу
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double GetValue(double x)
        {
            if (values.ContainsKey(x))
                return values[x];

            if (x > sortedX[sortedX.Count - 1] || x < sortedX[0]) //если х выходит за границы диапазона функции, то ошибка
                throw new ArgumentOutOfRangeException("Значение х должно быть внутри диапазона функции");
            for (int i = 1; i < sortedX.Count; i++)
                if (sortedX[i] > x)
                    return values[sortedX[i - 1]];
            throw new Exception("ошибка при поиске аргумента");
        }


    }
}
