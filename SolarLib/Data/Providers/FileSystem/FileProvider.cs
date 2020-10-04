using SolarEnergy.SolarLib.Classes.Collections;

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
        public abstract DataRange LoadDataRange(string fileName);

        /// <summary>
        /// экспорт ряда наблюдений в файл
        /// </summary>
        /// <param name="rang"></param>
        /// <param name="filename"></param>
        internal abstract void SaveDataRange(DataRange dataset, string filename);
    }
}
