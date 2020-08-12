using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Data.Providers.FileSystem.Import
{
    public class CSVImporter : BaseImporter
    {
        public CSVImporter (string fileName, BaseImporter baseImporter)
        {
            this.Initialize(fileName, baseImporter, FileFormats.CSV);
        }

        /// <summary>
        /// получить заданное число строк из файла, начиная с указанной строки StartLine. 
        /// </summary>
        /// <param name="lineCount">long.MaxValue, если надо считать файл до конца</param>
        /// <returns></returns>
        public override string GetText(long lineCount)
        {
            if (!File.Exists(FilePath))
                throw new ArgumentException("Файл не выбран");
            if (Encoding == null)
                throw new ArgumentException("Не задана кодировка");

            using (StreamReader sr = new StreamReader(FilePath, Encoding, true))
            {
                for (int i = 0; i < StartLine - 1; i++) //пропуск строк
                    sr.ReadLine();

                if (lineCount == long.MaxValue)
                    return sr.ReadToEnd();
                else
                {
                    string res = "";
                    for (long i = 0; i < lineCount + this.StartLine; i++)
                        if (!sr.EndOfStream)
                            res += sr.ReadLine() + "\r\n";
                    return res;
                }
            }
        }
    }
}
