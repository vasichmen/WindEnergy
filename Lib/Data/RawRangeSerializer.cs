using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Data.Providers;

namespace WindEnergy.Lib.Data
{
    /// <summary>
    /// открытие и сохранение файлов разных форматов
    /// </summary>
    public static class RawRangeSerializer
    {
        /// <summary>
        /// открыть файл 
        /// </summary>
        /// <param name="FileName">имя файла</param>
        /// <param name="onProgressChanged">действие при изменении состояния открытия (передаются проценты выполнения)</param>
        /// <returns></returns>
        public static RawRange DeserializeFile(string FileName, Action<double> onProgressChanged = null)
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

        /// <summary>
        /// сохранение файла с заданным форматом. Если формат не задан, то будет использован формат из ряда
        /// </summary>
        /// <param name="rang"></param>
        /// <param name="fileFormat"></param>
        public static void SerializeFile(RawRange rang, string filename, FileFormats fileFormat = FileFormats.None)
        {
            if (fileFormat == FileFormats.None)
                fileFormat = rang.FileFormat;
            
            switch (fileFormat)
            {
                case FileFormats.RP5MetarCSV:
                    RP5ru.ExportCSV(rang,filename, FileFormats.RP5MetarCSV);
                    break;

                case FileFormats.RP5WmoCSV:
                    RP5ru.ExportCSV(rang,filename, FileFormats.RP5WmoCSV);
                    break;
                default: throw new Exception("Этот формат не реализован");
            }
        }
    }
}
