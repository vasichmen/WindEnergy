using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Structures;

namespace WindEnergy.Lib.Data.Providers.FileSystem
{
    /// <summary>
    /// класс взаимодействия с файлами xlsx
	/// http://zeeshanumardotnet.blogspot.com/2011/06/creating-reports-in-excel-2007-using.html
    /// </summary>
    public class ExcelFile : FileProvider
    {
        public override RawRange LoadRange(string fileName)
        {
            throw new NotImplementedException();
        }

        public override void SaveCalcYearInfo(string fileName, CalculateYearInfo years)
        {
            throw new NotImplementedException();
        }

        public override void SaveEnergyInfo(string filename, RawRange range)
        {
            throw new NotImplementedException();
        }

        public override void SaveRangeQualityInfo(string fileName, QualityInfo qualityInfo, TimeSpan rangeLength)
        {
            throw new NotImplementedException();
        }

        internal override void SaveRange(RawRange rang, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
