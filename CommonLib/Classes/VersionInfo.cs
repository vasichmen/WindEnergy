using System;

namespace CommonLib.Classes
{
    /// <summary>
    /// информация о версии программы
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// список изменений
        /// </summary>
        public string Changes { get; set; }

        /// <summary>
        /// дата публикации
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// номер версии
        /// </summary>
        public int VersionInt { get; set; }

        /// <summary>
        /// текстовое представление весрии
        /// </summary>
        public string VersionText { get; set; }

        /// <summary>
        /// ссылка для скачаивания
        /// </summary>
        public string DownloadLink { get; set; }
    }
}
