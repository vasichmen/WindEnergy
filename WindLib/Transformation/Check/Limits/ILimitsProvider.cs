using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Transformation.Check.Limits
{
    /// <summary>
    /// представляет основные методы проверки данных
    /// </summary>
    internal interface ILimitsProvider
    {
        /// <summary>
        /// возвращает истину, если значение допустимо в этой точке
        /// </summary>
        /// <param name="item">данные для провtрки</param>
        /// <param name="coordinates">координаты точки</param>
        /// <returns></returns>
        bool CheckItem(RawItem item);
    }
}