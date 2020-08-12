using CommonLib.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Data.Providers.FileSystem.Import
{
    public class ImportFactory
    {
        public static BaseImporter CreateImporter(string fileName, BaseImporter baseImporter = null)
        {
            switch (Path.GetExtension(fileName))
            {
                case ".csv": return new CSVImporter(fileName, baseImporter);
                case ".xls":
                case ".xlsx":
                    return new XLSXImporter(fileName, baseImporter);
                default: throw new WrongFileFormatException("Неподдерживаемый формат файла");
            }
        }
    }
}
