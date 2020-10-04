using GMap.NET;
using System.Collections.Generic;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Transformation.Check
{
    /// <summary>
    /// структура параметров обработки ошибок
    /// </summary>
    public class CheckerParameters
    {
        /// <summary>
        /// создаёт параметры для проверки в заданной точке
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="coordinates"></param>
        public CheckerParameters(LimitsProviders provider, PointLatLng coordinates)
        {
            LimitsProvider = provider;
            Coordinates = coordinates;
            CheckByPos = true;
            SpeedInclude = null;
            DirectionInclude = null;
        }

        /// <summary>
        /// создаёт параметры для проверки с указанными диапазонами
        /// </summary>
        /// <param name="speedInclude"></param>
        /// <param name="directionsInclude"></param>
        public CheckerParameters(List<Diapason<double>> speedInclude, List<Diapason<double>> directionsInclude)
        {
            SpeedInclude = speedInclude;
            DirectionInclude = directionsInclude;
            CheckByPos = false;
            LimitsProvider = LimitsProviders.Manual;
        }

        /// <summary>
        /// максимальная скорость для точки. (если указана точка и источник ограничений, то не используется)
        /// </summary>
        public List<Diapason<double>> SpeedInclude { get; set; }

        /// <summary>
        /// пределы  допустимых направлений ветра. (если указана точка и источник ограничений, то не используется)
        /// </summary>
        public List<Diapason<double>> DirectionInclude { get; set; }

        /// <summary>
        /// если истина, то данные будут проверять для той точки, которая указана
        /// </summary>
        public bool CheckByPos { get; }

        /// <summary>
        /// координаты точки, для которой будут браться допустимые пределы величин
        /// </summary>
        public PointLatLng Coordinates { get; set; }

        /// <summary>
        /// источник информации о допустимых значениях
        /// </summary>
        public LimitsProviders LimitsProvider { get; internal set; }
    }
}
