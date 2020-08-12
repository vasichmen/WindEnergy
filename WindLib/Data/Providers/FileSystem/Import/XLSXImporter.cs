using CommonLib.Classes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Data.Providers.FileSystem.Import
{
    public class XLSXImporter : BaseImporter
    {
        private ExcelPackage excelPackage = null;

        public XLSXImporter(string fileName, BaseImporter baseImporter)
        {
            this.Initialize(fileName, baseImporter, FileFormats.XLSX);
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

            FileInfo fi = new FileInfo(FilePath);
            if (excelPackage == null || excelPackage.File.FullName != FilePath)
                excelPackage = new ExcelPackage(fi);

            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];

            var arr = worksheet.Cells;
            StringBuilder sb = new StringBuilder();
            for (int i = StartLine + 1; i <= worksheet.Dimension.Rows; i++)
            {
                if (i - StartLine == lineCount)
                    break;

                for (int j = 1; j <= worksheet.Dimension.Columns; j++)
                {
                    ExcelRange cell = arr[i, j];
                    object val = cell.Value;
                    if (val != null)
                    {
                        string format = cell.Style.Numberformat.Format;
                        if (format == ExcelFile.DateTimeFormat)
                            val = getDateTime(cell);
                        else
                        {
                            try
                            {
                                val = getDouble(cell);
                            }
                            catch (WindEnergyException)
                            {
                                val = cell.Value.ToString();
                            }
                        }
                        sb.Append(val);
                        sb.Append(";");
                    }
                }
                sb.Append("\r\n");

            }
            string res = sb.ToString();
            return res;
        }

        /// <summary>
        /// Преобразовать значение ячейки в double
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private double getDouble(ExcelRange cell)
        {
            if (cell == null || cell.Value == null)
                return double.NaN;

            if (cell.Value.GetType() == typeof(double))
                return (double)cell.Value;
            else
            {
                string val = cell.Value.ToString();
                try { return double.Parse(val); }
                catch (Exception)
                { throw new WindEnergyException($"Не удалось распознать значение \"{val}\" как double"); }
            }
        }

        /// <summary>
        /// преобразовать значение ячейки в DateTime
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private DateTime getDateTime(ExcelRange cell)
        {
            if (cell == null)
                throw new ArgumentNullException(nameof(cell));
            try
            {
                try
                {
                    double val = getDouble(cell);
                    return DateTime.FromOADate(val);
                }
                catch (WindEnergyException)
                {
                    return DateTime.Parse(cell.Value.ToString());
                }
            }
            catch (WindEnergyException)
            { throw new WindEnergyException($"Не удалось распознать значение \"{cell.Value.ToString()}\" как DateTime"); }
            catch (FormatException ex)
            { throw new WindEnergyException($"Не удалось распознать значение \"{cell.Value.ToString()}\" как DateTime\r\n({ex.Message})"); }
        }
    }
}
