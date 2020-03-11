using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Classes.Structures
{
   public class DataRangeConverterParams
    {
        public StandartIntervals Interval { get; set; }

        public HourModels HourModel { get; set; }

        public NasaSourceTypes SourceType { get; set; }

        public int Year { get; set; }
    }
}
