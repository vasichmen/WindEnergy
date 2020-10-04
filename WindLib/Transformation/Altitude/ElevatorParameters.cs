using GMap.NET;
using System.Collections.Generic;

namespace WindEnergy.WindLib.Transformation.Altitude
{
    /// <summary>
    /// настройки поднятия ряда на высоту
    /// </summary>
    public class ElevatorParameters
    {
        /// <summary>
        /// с какой высоты на у. з. поднимаем, м
        /// </summary>
        public double FromHeight { get; set; }

        /// <summary>
        /// на какую высоту поднимаем, м
        /// </summary>
        public double ToHeight { get; set; }

        /// <summary>
        /// координаты места измерения (для поиска АМС)
        /// </summary>
        public PointLatLng Coordinates { get; set; }

        /// <summary>
        /// расстояние в метрах, на котором ищется АМС (NaN - неограниченное)
        /// </summary>
        public double SearchRaduis { get; set; }

        /// <summary>
        /// коэффициент m, заданный вручную
        /// </summary>
        public double CustomMCoefficient { get; set; }

        /// <summary>
        /// среднемноголетний показатель степени по месяцам
        /// </summary>
        public Dictionary<Months, double> CustomNCoefficientMonths { get; set; }

        /// <summary>
        /// источник среднемноголетний показатель степени
        /// </summary>
        public HellmanCoefficientSource HellmanCoefficientSource { get; set; }

        /// <summary>
        /// максимальное отклонение относительной скорости при поиске подходящей АМС
        /// </summary>
        public double MaximalRelativeSpeedDeviation { get; set; }

        /// <summary>
        /// Выбранная АМС для расчетов
        /// </summary>
        public SuitAMSResultItem SelectedAMS { get; set; }
    }
}
