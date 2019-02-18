using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;

namespace WindEnergy.Lib.Operations
{
    /// <summary>
    /// проверка и устранение ошибок в ряде
    /// </summary>
    public static class Checker
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
            public CheckerParameters(List<Diapason> speedInclude, List<Diapason> directionsInclude)
            {
                SpeedInclude = speedInclude;
                DirectionInclude = directionsInclude;
                CheckByPos = false;
                LimitsProvider = LimitsProviders.None;
            }

            /// <summary>
            /// максимальная скорость для точки. (если указана точка и источник ограничений, то не используется)
            /// </summary>
            public List<Diapason> SpeedInclude { get; set; }

            /// <summary>
            /// пределы  допустимых направлений ветра. (если указана точка и источник ограничений, то не используется)
            /// </summary>
            public List<Diapason> DirectionInclude { get; set; }

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

        /// <summary>
        /// проверить ряд и устранить ошибки
        /// </summary>
        /// <param name="range">ряд</param>
        /// <param name="param">параметры обработки ошибок</param>
        /// <returns></returns>
        public static RawRange ProcessRange(RawRange range, CheckerParameters param)
        {
            return range;

            if ((param.Coordinates.IsEmpty || param.LimitsProvider == LimitsProviders.None) && param.CheckByPos)
                throw new ArgumentException("не хватает данных дл проверки ряда");

            //TODO:проверка ряда в зависимости от заданных параметров
            foreach (RawItem item in range)
            {
                //проверка и удаление ошибок(скорость или направление не соответствует заданной точке)

            }
        }


    }
}
