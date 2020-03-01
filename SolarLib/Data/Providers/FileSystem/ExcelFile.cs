using CommonLib;
using CommonLib.Classes;
using GMap.NET;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using SolarLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Data.Providers.FileSystem
{
    /// <summary>
    /// класс взаимодействия с файлами xlsx
	/// http://zeeshanumardotnet.blogspot.com/2011/06/creating-reports-in-excel-2007-using.html
    /// https://riptutorial.com/epplus
    /// </summary>
    public class ExcelFile : FileProvider
    {
        private static string DateTimeFormat = DateTimeFormatInfo.CurrentInfo.ShortDatePattern + " " + DateTimeFormatInfo.CurrentInfo.LongTimePattern;

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

        /// <summary>
        /// Загрузка ряда из файла xlsx
        /// </summary>
        /// <param name="fileName">адрес файла</param>
        /// <returns></returns>
        public override Dataset LoadDataset(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
                if( worksheet.Cells[3, 1].Value == null)
                    return null;
                string title = worksheet.Cells[1, 1].Value.ToString();
                string coordinates = worksheet.Cells[2, 1].Value.ToString();
                string name = worksheet.Cells[3, 1].Value.ToString();

                Dataset res = new Dataset() { Name = name };

                //чтение координат файла
                string regex = @"^\d+[\.\,].\d*\s+\d+[\.\,].\d*$";
                bool isMatch = new Regex(regex).IsMatch(coordinates);
                PointLatLng coord;
                if (isMatch)
                {
                    string[] s = coordinates.Split(' ');
                    double lat = double.Parse(s[0].Trim().Replace('.', Constants.DecimalSeparator));
                    double lon = double.Parse(s[1].Trim().Replace('.', Constants.DecimalSeparator));
                    coord = new PointLatLng(lat, lon);
                }
                else
                    coord = PointLatLng.Empty;
                res.Position = coord;
                
                //TODO: загрузка из xls

                //поиск информации о МС
                NPSMeteostationInfo meteostation = null;
                int start = title.IndexOf("ID=") + "ID=".Length;
                string id_s = title.Substring(start);
                if (id_s.ToLower() != "nasa" && id_s.ToLower() != "undefined")
                    meteostation = Vars.NPSMeteostationDatabase.GetByID(id_s);

                res.Meteostation = meteostation;
                return res;
            }
        }


        /// <summary>
        /// сохранение в файл xlsx
        /// </summary>
        /// <param name="rang"></param>
        /// <param name="filename"></param>
        internal override void SaveDataset(Dataset rang, string filename)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "Wind Energy";
                excelPackage.Workbook.Properties.Title = rang.Name;
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                //Create the WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Лист 1");

                string coordinates = rang.Position.Lat.ToString("0.000000") + " " + rang.Position.Lng.ToString("0.000000");
                string[] caption = new string[] { "Местное время", "T, ℃", "U, %", "DD, °", "ff, м/с", "P0, мм рт. ст." };
                string title;
                if (rang.Meteostation == null)
                    title = "ID=undefined";
                else
                    title = $"ID={rang.Meteostation.ID}";
                worksheet.Cells[1, 1].Value = title;
                worksheet.Cells[2, 1].Value = coordinates;
                worksheet.Cells[3, 1].Value = rang.Name;
                for (int j = 1; j <= caption.Length; j++)
                    worksheet.Cells[4, j].Value = caption[j - 1];
                worksheet.Cells[4, 1, 4, caption.Length].Style.Font.Bold = true;
                worksheet.Cells[4, 1, 4, caption.Length].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[4, 1, 4, caption.Length].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.View.FreezePanes(5, 1);
                worksheet.Column(1).Width = 20;

                int i = 5;
   
                //TODO: сохранение xls

                //Save your file
                FileInfo fi = new FileInfo(filename);
                excelPackage.SaveAs(fi);
                excelPackage.Dispose();
            }
        }
    }
}
