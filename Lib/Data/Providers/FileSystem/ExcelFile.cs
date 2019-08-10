using GMap.NET;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Structures;

namespace WindEnergy.Lib.Data.Providers.FileSystem
{
    /// <summary>
    /// класс взаимодействия с файлами xlsx
	/// http://zeeshanumardotnet.blogspot.com/2011/06/creating-reports-in-excel-2007-using.html
    /// https://riptutorial.com/epplus
    /// </summary>
    public class ExcelFile : FileProvider
    {
        /// <summary>
        /// Загрузка ряда из файла xlsx
        /// </summary>
        /// <param name="fileName">адрес файла</param>
        /// <returns></returns>
        public override RawRange LoadRange(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];

                string title = worksheet.Cells[1, 1].Value.ToString();
                string coordinates = worksheet.Cells[2, 1].Value.ToString();
                string name = worksheet.Cells[3, 1].Value.ToString();

                RawRange res = new RawRange() { Name = name };

                //чтение координат файла
                string regex = @"^\d+[\.\,].\d*\s+\d+[\.\,].\d*$";
                bool isMatch = new Regex(regex).IsMatch(coordinates);
                PointLatLng coord;
                if (isMatch)
                {
                    string[] s = coordinates.Split(' ');
                    double lat = double.Parse(s[0].Trim().Replace('.', Vars.DecimalSeparator));
                    double lon = double.Parse(s[1].Trim().Replace('.', Vars.DecimalSeparator));
                    coord = new PointLatLng(lat, lon);
                }
                else
                    coord = PointLatLng.Empty;
                res.Position = coord;
                res.BeginChange();

                for (int i = 5; i <= worksheet.Dimension.Rows; i++)
                {
                    List<string> elems = new List<string>();
                    for (int j = 1; j <= worksheet.Dimension.Columns; j++)
                        if (worksheet.Cells[i, j] != null && worksheet.Cells[i, j].Value != null)
                            elems.Add(worksheet.Cells[i, j].Value.ToString());

                    if (elems.Count < 6)
                        continue;
                    if (elems[3] == "")
                        continue;
                    if (elems[4] == "")
                        continue;

                    double temp = elems[1] == "" ? double.NaN : double.Parse(elems[1].Replace('.', Vars.DecimalSeparator));
                    DateTime dt = DateTime.Parse(elems[0]);
                    double spd = double.Parse(elems[4].Replace('.', Vars.DecimalSeparator));
                    double press = double.Parse(elems[5].Replace('.', Vars.DecimalSeparator));
                    double wet = elems[2] == "" ? double.NaN : double.Parse(elems[2].Replace('.', Vars.DecimalSeparator));
                    double dirs = double.Parse(elems[3].Replace('.', Vars.DecimalSeparator));
                    try
                    { res.Add(new RawItem() { Date = dt, Direction = dirs, Speed = spd, Temperature = temp, Wetness = wet, Pressure = press }); }
                    catch (Exception)
                    { continue; }
                }


                //поиск информации о МС
                MeteostationInfo meteostation = null;
                int start = title.IndexOf("ID=") + "ID=".Length;
                string id_s = title.Substring(start);
                meteostation = Vars.Meteostations.GetByID(int.Parse(id_s));
                res.Meteostation = meteostation;
                res.EndChange();
                return res;
            }
        }

        public override void SaveCalcYearInfo(string fileName, CalculateYearInfo years)
        {
            throw new NotImplementedException();
        }

        public override void SaveEnergyInfo(string filename, RawRange range)
        {
            throw new NotImplementedException();
        }

        public override void SaveRangeQualityInfo(string fileName, QualityInfo qualityInfo, TimeSpan rangeLength)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// сохранение в файл xlsx
        /// </summary>
        /// <param name="rang"></param>
        /// <param name="filename"></param>
        internal override void SaveRange(RawRange rang, string filename)
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
                for (int j = 1; j <= 6; j++)
                    worksheet.Cells[4, j].Value = caption[j - 1];
                int i = 5;
                foreach (RawItem item in rang)
                {
                    if (double.IsNaN(item.Direction) || double.IsNaN(item.Speed) || item.DirectionRhumb == WindDirections.Undefined)
                        continue;
                    worksheet.Cells[i, 1].Value = item.Date.ToString("dd.MM.yyyy HH:mm");
                    worksheet.Cells[i, 2].Value = item.Temperature;
                    worksheet.Cells[i, 3].Value = item.Wetness;
                    worksheet.Cells[i, 4].Value = item.Direction;
                    worksheet.Cells[i, 5].Value = item.Speed;
                    worksheet.Cells[i, 6].Value = item.Pressure;
                    i++;
                }


                //Save your file
                FileInfo fi = new FileInfo(filename);
                excelPackage.SaveAs(fi);
            }



        }
    }
}
