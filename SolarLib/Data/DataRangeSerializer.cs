using SolarEnergy.SolarLib.Classes.Collections;
using System;
using System.IO;
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
            string ext = Path.GetExtension(fileName).ToLower();
            switch (ext)
            {
                case ".csv":
                    new CSVFile().SaveDataRange(rang, fileName);
                    break;
                case ".xls":
                default: throw new Exception("Сохранение этого типа файлов не реализовано");
            }
        }
    }
}
