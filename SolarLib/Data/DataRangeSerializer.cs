using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Data.Providers.FileSystem;

namespace SolarEnergy.SolarLib.Data
{
    public static class DataRangeSerializer
    {
        /// <summary>
        /// открыть файл 
        /// </summary>
        /// <param name="FileName">имя файла</param>
        /// <param name="onProgressChanged">действие при изменении состояния открытия (передаются проценты выполнения)</param>
        /// <returns></returns>
        public static DataRange DeserializeFile(string FileName, Action<double> onProgressChanged = null)
        {
            string ext = Path.GetExtension(FileName).ToLower();
            switch (ext)
            {
                case ".csv":
                    return new CSVFile().LoadDataRange(FileName);
                case ".xls":
                default: throw new Exception("Открытие этого типа файлов не реализовано");
            }
        }

        public static void SerializeFile(DataRange rang, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
