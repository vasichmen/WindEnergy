using GMap.NET;
namespace CommonLibLib.Data.Interfaces
{
    /// <summary>
    /// поставщик данных о высоте точек
    /// </summary>
    public interface IGeoInfoProvider
    {
        /// <summary>
        /// если истина, то это локальный источник данных
        /// </summary>
        bool isLocal { get; }

        /// <summary>
        /// возвращает высоту над уровнем моря в метрах
        /// </summary>
        /// <param name="coordinate">координаты точки</param>
        /// <returns></returns>
        double GetElevation(PointLatLng coordinate);
    }
}
