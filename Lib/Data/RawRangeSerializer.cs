using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Data.Providers;

namespace WindEnergy.Lib.Data
{
    public static class RawRangeSerializer
    {
        public static RawRange OpenFile(string FileName, Action<double> onProgressChanged = null)
        {
            string ext = Path.GetExtension(FileName).ToLower();
            switch (ext)
            {
                case ".csv":
                    RawRange rang = RP5ru.LoadCSV(FileName);
                    return rang;
                default: throw new Exception("Открытие этого типа файлов не реализовано");
            }
        }


    }
}
