using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Operations.Interpolation
{
    public class LinearInterpolateMethod : IInterpolateMethod
    {
        private readonly Dictionary<double, double> values;
        private List<double> sortedX;
        public readonly bool Empty;

        /// <summary>
        /// сохдаёт новый экземпляр с заданной функцией
        /// </summary>
        /// <param name="funct"></param>
        public LinearInterpolateMethod(Dictionary<double, double> funct)
        {
            if (funct.Keys.Count == 0)
            { Empty = true; return; }
            Empty = false;
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
            if (Empty)
                return double.NaN;

            if (values.ContainsKey(x))
                return values[x];

            double res;
            if (x > sortedX[sortedX.Count - 1] || x < sortedX[0]) //если х выходит за границы диапазона функции, то ошибка
                throw new ArgumentOutOfRangeException("Значение х должно быть внутри диапазона функции");
            int left = getLeftBound(x);
            res = linInterpolate(sortedX[left], sortedX[left + 1], x);
            return res;
        }

        /// <summary>
        /// поиск индекса левого края диапазона аргументов, в который попадает заданное значение аргумента х
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int getLeftBound(double x)
        {
            //поиск методом бисекций
            int from_i = 0;
            int to_i = sortedX.Count - 1;

            while (to_i - from_i > 1)
            {
                double from_x = sortedX[from_i];
                double to_x = sortedX[to_i];
                int c_i = (to_i + from_i) / 2;
                double c_x = sortedX[c_i];
                if (x > c_x) // если искомое значение справа
                    from_i = c_i;
                else //если искомое значение слева
                    to_i = c_i;
            }
            return from_i;
        }

        /// <summary>
        /// интерполяция между заданными точками на функции
        /// </summary>
        /// <param name="x1">левый известнтый х</param>
        /// <param name="x2">правый известный х</param>
        /// <param name="x">искомый агрумент </param>
        /// <returns></returns>
        private double linInterpolate(double x1, double x2, double x)
        {
            double y1 = values[x1];
            double y2 = values[x2];
            double y = LinearInterpolation(x1, x2, y1, y2, x);
            return y;
        }

        /// <summary>
        /// линейная интерполяция между двумя заданными точками
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double LinearInterpolation(double x1, double x2, double y1, double y2, double x)
        {
            return y2 + ((y1 - y2) / (x1 - x2)) * (x - x2);
        }
    }
}
