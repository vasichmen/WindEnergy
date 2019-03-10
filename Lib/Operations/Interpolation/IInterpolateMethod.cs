using System.Collections.Generic;

namespace WindEnergy.Lib.Operations.Interpolation
{
    /// <summary>
    /// представляет основные методы интерполяции функций
    /// </summary>
    public interface IInterpolateMethod
    {
        /// <summary>
        /// получить значение функции по заданному парамтру. При невозможности возвращает double.NaN
        /// </summary>
        /// <param name="x">параметр функции</param>
        /// <returns></returns>
        double GetValue(double x);
        
    }
}