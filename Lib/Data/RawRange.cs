using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Data
{
    /// <summary>
    /// представление ряда для редактирования
    /// </summary>
    public class RawRange : BindingList<RawItem>
    {
        /// <summary>
        /// путь к файлу
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// название файла
        /// </summary>
        public string FileName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FilePath))
                    return Path.GetFileName(FilePath);
                else
                    return null;
            }
        }

        /// <summary>
        /// формат файла
        /// </summary>
        public FileFormats FileFormat { get; set; }

        public RawRange()
        {
            FilePath = null;
        }

    }
}
