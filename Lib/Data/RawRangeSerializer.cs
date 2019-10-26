using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.Lib.Data.Providers.FileSystem;
using WindEnergy.Lib.Data.Providers.InternetServices;

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
                    return  new CSVFile().LoadRange(FileName);
                case ".xls":
                case ".xlsx":
                    return  new ExcelFile().LoadRange(FileName);
                default: throw new Exception("Открытие этого типа файлов не реализовано");
            }
        }

        /// <summary>
        /// сохранение файла с заданным форматом. Если формат не задан, то будет использован формат из ряда
        /// </summary>
        /// <param name="rang"></param>
        /// <param name="filename"></param>
        public static void SerializeFile(RawRange rang, string filename)
        {
            FileProvider provider;
            string ext = Path.GetExtension(filename);
            switch (ext)
            {
                case ".csv":
                    provider = new CSVFile();
                    break;
                case ".xlsx":
                case ".xls":
                    provider = new ExcelFile();
                    break;
                default: throw new Exception("Этот тип не реализован");
            }
            provider.SaveRange(rang, filename);
        }
    }
}
