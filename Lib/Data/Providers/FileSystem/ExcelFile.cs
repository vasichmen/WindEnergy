﻿using GMap.NET;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Calculations;
using WindEnergy.Lib.Statistic.Collections;
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

        /// <summary>
        /// Сохренение информации о выбранном расчетном годе
        /// </summary>
        /// <param name="fileName">имя файла</param>
        /// <param name="years">информация о выборе</param>
        public override void SaveCalcYearInfo(string fileName, CalculateYearInfo years)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "Wind Energy";
                excelPackage.Workbook.Properties.Title = "Статистика наблюдений";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                //Create the WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Лист 1");
                if (years.RecomendedYear != null)
                    worksheet.Cells[1, 1].Value = "Рекомендуется принять в качестве расчетного " + years.RecomendedYear.Year + " год";
                else
                    worksheet.Cells[1, 1].Value = "Не удалось выбрать расчетный год";
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                //заголовок таблицы
                string[] header = new string[] { "Год", "Полнота ряда, %", "Интервал", "Отклонение от среднемноголетней скорости, м/с", "Отклонение от среднемноголетней скорости, %", "Отклонение повторяемости скорости", "Средняя скорость, м/с" };
                for (int j = 1; j <= header.Length; j++)
                    worksheet.Cells[2, j].Value = header[j - 1];
                worksheet.Cells[2, 1, 2, header.Length].Style.Font.Bold = true;
                worksheet.Cells[2, 1, 2, header.Length].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[2, 1, 2, header.Length].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                int i = 3;
                foreach (SinglePeriodInfo spi in years.Years)
                {
                    worksheet.Cells[i, 1].Value = spi.Year.ToString();
                    worksheet.Cells[i, 2].Value = spi.Completness.ToString("0.0");
                    worksheet.Cells[i, 3].Value = spi.Interval.Description();
                    worksheet.Cells[i, 4].Value = spi.SpeedDeviation.ToString("0.00");
                    worksheet.Cells[i, 5].Value = spi.SpeedDeviationPercent.ToString("0.00");
                    worksheet.Cells[i, 6].Value = spi.ExpectancyDeviation.ToString("0.00");
                    worksheet.Cells[i, 7].Value = spi.AverageSpeed.ToString("0.00");
                    i++;
                }
                //Save your file
                FileInfo fi = new FileInfo(fileName);
                excelPackage.SaveAs(fi);
            }
        }

        /// <summary>
        /// сохранение ВЭК
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="range"></param>
        public override void SaveEnergyInfo(string filename, RawRange range)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "Wind Energy";
                excelPackage.Workbook.Properties.Title = range.Name;
                excelPackage.Workbook.Properties.Created = DateTime.Now;
                //Create the WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Лист 1");

                List<object> years = new List<object>();
                foreach (RawItem item in range)
                    if (!years.Contains(item.Date.Year))
                        years.Add(item.Date.Year);

                //формирование заголовка
                List<string> cap = new List<string>() { "Год", "Месяц", "Кол-во измерений" };
                foreach (GradationItem grad in Vars.Options.CurrentSpeedGradation.Items)
                    cap.Add(grad.Average.ToString("0.00"));
                cap.AddRange(new string[] { "Vmin, м/с", "Vmax, м/с", "Vср, м/с", "Cv(V)", "γ", "β", "Nвал уд., Вт/м^2", "Эвал уд., Вт*ч/м^2" });
                foreach (WindDirections wd in WindDirections.Calm.GetEnumItems().GetRange(0, 17))
                    cap.Add(wd.Description());


                //ЗАПИСЬ ФАЙЛА
                //запись заголовка
                for (int j = 1; j <= cap.Count; j++)
                    worksheet.Cells[1, j].Value = cap[j - 1];
                worksheet.Cells[1, 1, 1, cap.Count].Style.Font.Bold = true;
                worksheet.Cells[1, 1, 1, cap.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 1, 1, cap.Count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                //запись данных обо всём периоде
                EnergyInfo ri1 = StatisticEngine.ProcessRange(range);
                StatisticalRange<WindDirections> sd1 = StatisticEngine.GetDirectionExpectancy(range, GradationInfo<WindDirections>.Rhumb16Gradations);
                StatisticalRange<GradationItem> ss1 = StatisticEngine.GetExpectancy(range, Vars.Options.CurrentSpeedGradation);
                EnergyInfo ei1 = StatisticEngine.ProcessRange(ss1);
                int cells = saveEnergyInfoLine(worksheet, 2, ri1, ei1, sd1, ss1, "Все года", "Все месяцы", range.Count);
                worksheet.Cells[2, 1, 2, cells].Style.Font.Bold = true;
                worksheet.Cells[2, 1, 2, cells].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[2, 1, 2, cells].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                //запись данных для каждого года
                int line = 3;
                foreach (int year in years) //цикл по годам
                {
                    //для каждого месяца в году
                    for (int mt = 0; mt <= 12; mt++)//по месяцам, начиная со всех
                    {
                        Months month = (Months)mt;
                        RawRange rn = range.GetRange(false, true, DateTime.Now, DateTime.Now, year, month.Description());
                        if (rn == null || rn.Count == 0)
                            continue;
                        EnergyInfo ri = StatisticEngine.ProcessRange(rn);
                        StatisticalRange<WindDirections> sd = StatisticEngine.GetDirectionExpectancy(rn, GradationInfo<WindDirections>.Rhumb16Gradations);
                        StatisticalRange<GradationItem> ss = StatisticEngine.GetExpectancy(rn, Vars.Options.CurrentSpeedGradation);
                        EnergyInfo ei = StatisticEngine.ProcessRange(ss);
                        cells = saveEnergyInfoLine(worksheet, line, ri, ei, sd, ss, year.ToString(), month.Description(), rn.Count);
                        if (Math.IEEERemainder(mt + 1, 2) == 0 || mt == 0) //на нечетных строках добавляем серый фон
                        {
                            worksheet.Cells[line, 1, line, cells].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[line, 1, line, cells].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                        if (month == Months.All) //для суммарной информации по году выделение жирным
                        {
                            worksheet.Cells[line, 1, line, cells].Style.Font.Bold = true;
                        }
                        line++;
                    }
                }
                //Save your file
                FileInfo fi = new FileInfo(filename);
                excelPackage.SaveAs(fi);
            }
        }

        /// <summary>
        /// запись одной строки в лист ВЭК. Возвращет число элементов в этой строке
        /// </summary>
        /// <param name="worksheet">страница для записи</param>
        /// <param name="line">строка</param>
        /// <param name="range_info">характеристики по ряду наблюдений</param>
        /// <param name="ext_info">характеристики по градациям</param>
        /// <param name="stat_directions">повторяемость направлений</param>
        /// <param name="stat_speeds">повторяемость скоростей</param>
        /// <param name="year">год</param>
        /// <param name="month">месяц</param>
        /// <param name="amount">число измерений</param>
        /// <returns>количество добавленных ячеек</returns>
        private int saveEnergyInfoLine(ExcelWorksheet worksheet,
            int line,
            EnergyInfo range_info,
            EnergyInfo ext_info,
            StatisticalRange<WindDirections> stat_directions,
            StatisticalRange<GradationItem> stat_speeds,
            string year,
            string month,
            int amount)
        {
            if (range_info == null || ext_info == null || stat_directions == null || stat_speeds == null)
            {
                return 0;
            }

            //  "Год;Месяц;кол-во изм;0.75;2.5;4.5;6.5;8.5;10.5;12.5;14.5;16.5;19;22.5;26.5;31.5;37.5;43.5;Vmin;Vmax;Vср.год;Cv(V);Nвал уд.;Эвал уд.;С;СВ;В;ЮВ;Ю;ЮЗ;З;СЗ;штиль";
            List<string> values = new List<string>() { year, month, amount.ToString() };

            //повторяемости скоростей ветра
            for (int j = 0; j < stat_speeds.Keys.Count; j++)
                values.Add((stat_speeds.Values[j] * 100).ToString("0.00"));

            //по ряду наблюдений
            values.AddRange(new List<string>() {
                range_info.Vmin.ToString("0.00"),
                range_info.Vmax.ToString("0.00"),
                range_info.V0.ToString("0.00"),
                range_info.Cv.ToString("0.00"),
                range_info.VeybullGamma.ToString("0.00"),
                range_info.VeybullBeta.ToString("0.00"),
                range_info.PowerDensity.ToString("0.00"),
                range_info.EnergyDensity.ToString("0.00")
            });

            //повторяемости направлений ветра
            List<Enum> rs = WindDirections.Calm.GetEnumItems().GetRange(0, 17);
            for (int j = 0; j < rs.Count; j++)
            {
                WindDirections rhumb = (WindDirections)rs[j];
                int index = stat_directions.Keys.IndexOf(rhumb);
                if (index == -1)
                    continue;
                values.Add((stat_directions.Values[index] * 100).ToString("0.00"));
            }

            //запись всех значений
            for (int j = 1; j <= values.Count; j++)
                worksheet.Cells[line, j].Value = values[j - 1];

            return values.Count;
        }

        /// <summary>
        /// экспорт информации о периодах наблюдения
        /// </summary>
        /// <param name="fileName">имя файла</param>
        /// <param name="qualityInfo">информация для сохранения</param>
        /// <param name="rangeLength">длительность ряда</param>
        public override void SaveRangeQualityInfo(string fileName, QualityInfo qualityInfo, TimeSpan rangeLength)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "Wind Energy";
                excelPackage.Workbook.Properties.Title = "Статистика наблюдений";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                //Create the WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Лист 1");

                worksheet.Cells[1, 1].Value = "Полнота ряда, %;" + qualityInfo.Completeness * 100;
                worksheet.Cells[2, 1].Value = "Общее количество наблюдений;" + qualityInfo.MeasureAmount;
                worksheet.Cells[3, 1].Value = "Ожидаемое число измерений;" + qualityInfo.ExpectAmount;
                worksheet.Cells[4, 1].Value = "Длительность ряда наблюдений; " + rangeLength.ToText();
                worksheet.Cells[5, 1].Value = "Максимальная длительность пропуска данных;" + qualityInfo.MaxEmptySpace.ToText();

                //заголовок
                List<string> caption = new List<string>() { "Диапазон наблюдений", "Интервал", "Длительность диапазона" };
                for (int j = 1; j <= caption.Count; j++)
                    worksheet.Cells[7, j].Value = caption[j - 1];
                worksheet.Cells[7, 1, 7, caption.Count].Style.Font.Bold = true;
                worksheet.Cells[7, 1, 7, caption.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[7, 1, 7, caption.Count].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                int i = 8;
                foreach (var spi in qualityInfo.Intervals)
                {
                    worksheet.Cells[i, 1].Value = spi.Diapason;
                    worksheet.Cells[i, 2].Value = spi.Interval.Description();
                    worksheet.Cells[i, 3].Value = spi.Length.ToText();
                }
                //Save your file
                FileInfo fi = new FileInfo(fileName);
                excelPackage.SaveAs(fi);
            }
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
                for (int j = 1; j <= caption.Length; j++)
                    worksheet.Cells[4, j].Value = caption[j - 1];
                worksheet.Cells[4, 1, 4, caption.Length].Style.Font.Bold = true;
                worksheet.Cells[4, 1, 4, caption.Length].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[4, 1, 4, caption.Length].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

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