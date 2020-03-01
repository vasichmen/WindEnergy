using SolarEnergy.SolarLib.Classes.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Data.Providers.FileSystem
{
    /// <summary>
    /// лющий класс взаимодействия с файлами
    /// </summary>
    public abstract class FileProvider
    {
        /// <summary>
        /// загрузка файла CSV поддерживаемых форматов
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public abstract Dataset LoadDataset(string fileName);

        /// <summary>
        /// экспорт ряда наблюдений в файл
        /// </summary>
        /// <param name="rang"></param>
        /// <param name="filename"></param>
        internal abstract void SaveDataset(Dataset dataset, string filename);
    }
}
