using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Classes.Structures.Options
{
    /// <summary>
    /// структура , формата импорта из текстовых файлов
    /// </summary>
    public class TextImporterState
    {

        /// <summary>
        /// установка значений по умолчанию
        /// </summary>
        public TextImporterState()
        {
            DateColumn = 1;
            PressColumn = 3;
            SpeedColumn = 8;
            TemperatureColumn = 2;
            DirectionColumn = 7;
            WetnessColumnm = 6;
            DirectionUnits = DirectionUnits.TextRP5;
            PressureUnits = PressureUnits.mmHgArt;
            WetnessUnits = WetnessUnits.Percents;
            Trimmers = "\"";
            Delimeter = ";";
        }

        /// <summary>
        /// номер столбца даты
        /// </summary>
        public int DateColumn { get; set; }

        /// <summary>
        /// номер столбца направления
        /// </summary>
        public int DirectionColumn { get; set; }

        /// <summary>
        /// столбец давления
        /// </summary>
        public int PressColumn { get; set; }

        /// <summary>
        /// столбец скорости
        /// </summary>
        public int SpeedColumn { get; set; }

        /// <summary>
        /// столбец давления
        /// </summary>
        public int TemperatureColumn { get; set; }

        /// <summary>
        /// столбец влажности
        /// </summary>
        public int WetnessColumnm { get; set; }

        /// <summary>
        /// единицы измерения направления
        /// </summary>
        public DirectionUnits DirectionUnits { get; set; }

        /// <summary>
        /// единицы измерения давления
        /// </summary>
        public PressureUnits PressureUnits { get; set; }

        /// <summary>
        /// единицы измерения владности
        /// </summary>
        public WetnessUnits WetnessUnits { get; set; }

        /// <summary>
        /// удаляемые символы
        /// </summary>
        public string Trimmers { get; set; }

        /// <summary>
        /// разделитель
        /// </summary>
        public string Delimeter { get; set; }

    }
}
