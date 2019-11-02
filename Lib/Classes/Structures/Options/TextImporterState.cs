using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Classes.Structures.Options
{
    /// <summary>
    /// структура , формата импорта из текстовых файлов
    /// </summary>
   public class TextImporterState
    {
        public TextImporterState() {
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

        public int DateColumn { get; set; }
        public int DirectionColumn { get; set; }
        public int PressColumn { get; set; }
        public int SpeedColumn { get; set; }
        public int TemperatureColumn { get; set; }
        public int WetnessColumnm { get; set; }
        public DirectionUnits DirectionUnits { get; set; }
        public PressureUnits PressureUnits { get; set; }
        public WetnessUnits WetnessUnits { get; set; }
        public string Trimmers { get; set; }
        public string Delimeter { get; set; }
        
    }
}
