using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Operations.Structures;
using WindEnergy.WindLib.Statistic.Calculations;
using WindEnergy.WindLib.Statistic.Collections;
using WindEnergy.WindLib.Statistic.Structures;

namespace WindEnergy.WindLib.Data.Providers.FileSystem
{
    /// <summary>
    /// лющий класс взаимодействия с файлами
    /// </summary>
    public abstract class FileProvider
    {
        /// <summary>
        /// Сохранение ВЭКв файл по годам
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="range"></param>
        public abstract void SaveEnergyInfo(string filename, RawRange range);

        /// <summary>
        /// загрузка файла CSV поддерживаемых форматов
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public abstract RawRange LoadRange(string fileName);

        /// <summary>
        /// сохранение статистики наблюдений в файл
        /// </summary>
        /// <param name="fileName">имя файла</param>
        /// <param name="qualityInfo">информация о полноте ряда</param>
        /// <param name="rangeLength">длительность наблюдений</param>
        public abstract void SaveRangeQualityInfo(string fileName, QualityInfo qualityInfo, TimeSpan rangeLength);

        /// <summary>
        /// сохранение информации о расчетном годе в файл
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="years"></param>
        public abstract void SaveCalcYearInfo(string fileName, CalculateYearInfo years);

        /// <summary>
        /// экспорт ряда наблюдений в файл
        /// </summary>
        /// <param name="rang"></param>
        /// <param name="filename"></param>
        internal abstract void SaveRange(RawRange rang, string filename);
    }
}
