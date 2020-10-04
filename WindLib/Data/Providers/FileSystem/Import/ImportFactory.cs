using CommonLib.Classes;
using System.IO;

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
