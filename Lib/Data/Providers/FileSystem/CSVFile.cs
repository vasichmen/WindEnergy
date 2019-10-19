using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data.Providers.InternetServices;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Calculations;
using WindEnergy.Lib.Statistic.Collections;
using WindEnergy.Lib.Statistic.Structures;

namespace WindEnergy.Lib.Data.Providers.FileSystem
{
    /// <summary>
    /// работа с файлами формата csv
    /// </summary>
    public class CSVFile : FileProvider
    {
        /// <summary>
        /// сохранить энергетические характеристики в файл csv 
        /// </summary>
        /// <param name="fileName">имя файла</param>
        /// <param name="range_info">характеристики по ряду наблюдений</param>
        /// <param name="ext_info">характеристики по градациям</param>
        /// <param name="stat_directions">повторяемость направлений</param>
        /// <param name="stat_speeds">повторяемость скоростей</param>
        /// <param name="append">если истина, то файл будет дописан</param>
        /// <param name="caption">заголовок, который будет записан в начале</param>
        /// <param name="month">значение графы месяц</param>
        /// <param name="year">значение графы год</param>
        /// <param name="amount">количество измерений в ряде</param>
        private void saveEnergyInfoLine(
            string fileName,
            EnergyInfo range_info,
            EnergyInfo ext_info,
            StatisticalRange<WindDirections> stat_directions,
            StatisticalRange<GradationItem> stat_speeds,
            string caption,
            string year,
            string month,
            int amount,
            bool append
            )
        {
            StreamWriter sw = new StreamWriter(fileName, append, Encoding.UTF8);

            //запись заголовка
            if (!string.IsNullOrEmpty(caption))
                sw.WriteLine(caption);

            if (range_info == null || ext_info == null || stat_directions == null || stat_speeds == null)
            {
                sw.Close();
                return;
            }

            //  "Год;Месяц;кол-во изм;0.75;2.5;4.5;6.5;8.5;10.5;12.5;14.5;16.5;19;22.5;26.5;31.5;37.5;43.5;Vmin;Vmax;Vср.год;Cv(V);Nвал уд.;Эвал уд.;С;СВ;В;ЮВ;Ю;ЮЗ;З;СЗ;штиль";
            string line = $"{year};{month};{amount}";

            //повторяемости скоростей ветра
            for (int j = 0; j < stat_speeds.Keys.Count; j++)
                line += ";" + (stat_speeds.Values[j] * 100).ToString("0.00");

            //по ряду наблюдений
            line += string.Format(";{0:f2};{1:f2};{2:f2};{3:f2};{4:f2};{5:f2};{6:f2};{7:f2}", range_info.Vmin, range_info.Vmax, range_info.V0, range_info.Cv, range_info.VeybullGamma, range_info.VeybullBeta, range_info.PowerDensity, range_info.EnergyDensity);

            //повторяемости направлений ветра
            List<Enum> rs = WindDirections.Calm.GetEnumItems().GetRange(0, 17);
            for (int j = 0; j < rs.Count; j++)
            {
                WindDirections rhumb = (WindDirections)rs[j];
                int index = stat_directions.Keys.IndexOf(rhumb);
                if (index == -1)
                    continue;
                line += ";" + (stat_directions.Values[index] * 100).ToString("0.00");
            }
            sw.WriteLine(line);
            sw.Close();
        }

        /// <summary>
        /// загрузка файла CSV поддерживаемых форматов
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public override RawRange LoadRange(string fileName)
        {
            StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true);

            //определение формата файла csv
            string title = null, title2 = null, title3 = null;
            title = sr.ReadLine();
            if (!sr.EndOfStream)
                title2 = sr.ReadLine();
            if (!sr.EndOfStream)
                title3 = sr.ReadLine();
            sr.Close();

            if (title == null)
                throw new WindEnergyException("Файл пуст!");

            string regex = @"^\d+[\.\,].\d*\s+\d+[\.\,].\d*$";

            //этот файл - один из типов файлов rp5.ru (в заголовке есть тип источника, во второй строке указана кодировка, в третьей есть ссылка на источник)
            if ((title.Contains("WMO_ID") || title.Contains("METAR"))
                && title2 != null && title2.Contains("UTF-8")
                && title3 != null && title3.Contains("rp5.ru"))
                return RP5ru.LoadCSV(fileName);

            //этот файл - просто файл CSV (в заголовке есть тип источника, во второй строке есть координаты точки)
            else if ((title.Contains("ID"))
                     && title2 != null && new Regex(regex).IsMatch(title2))
                return loadCSVFile(fileName);

            //этот файл другого формата
            else throw new Exception("Файл повреждён или имеет неподдерживаемый формат");
        }

        /// <summary>
        /// загрузка ряда из формата WindEnergy
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private RawRange loadCSVFile(string filename)
        {
            StreamReader sr = new StreamReader(filename, Encoding.UTF8);
            string title = sr.ReadLine();
            string coordinates = sr.ReadLine();
            string name = sr.ReadLine();
            sr.ReadLine();//пропуск заголовка таблицы
            string data = sr.ReadToEnd();
            sr.Close();
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


            string[] lines = data.Split('\n');
            foreach (string line in lines)
            {
                string[] elems = line.Split(';');
                if (elems.Length < 6)
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
            RP5MeteostationInfo meteostation = null;
            int start = title.IndexOf("ID=") + "ID=".Length;
            string id_s = title.Substring(start);
            meteostation = Vars.RP5Meteostations.GetByID(int.Parse(id_s));
            res.Meteostation = meteostation;


            res.EndChange();
            return res;
        }

        /// <summary>
        /// сохранение статистики наблюдений в файл
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="qualityInfo"></param>
        public override void SaveRangeQualityInfo(string fileName, QualityInfo qualityInfo, TimeSpan rangeLength)
        {
            StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8);
            sw.WriteLine("Полнота ряда, %;" + qualityInfo.Completeness * 100);
            sw.WriteLine("Общее количество наблюдений;" + qualityInfo.MeasureAmount);
            sw.WriteLine("Ожидаемое число измерений;" + qualityInfo.ExpectAmount);
            sw.WriteLine("Длительность ряда наблюдений;" + rangeLength.ToText());
            sw.WriteLine("Максимальная длительность пропуска данных;" + qualityInfo.MaxEmptySpace.ToText());
            sw.WriteLine();
            string nline = "Диапазон наблюдений;Интервал;Длительность диапазона";
            sw.WriteLine(nline);
            foreach (var spi in qualityInfo.Intervals)
            {
                nline = string.Format("{0};{1};{2}", spi.Diapason, spi.Interval.Description(), spi.Length.ToText());
                sw.WriteLine(nline);
            }
            sw.Close();
        }

        /// <summary>
        /// сохранение информации о расчетном годе в файл
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="years"></param>
        public override void SaveCalcYearInfo(string fileName, CalculateYearInfo years)
        {
            StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8);
            if (years.RecomendedYear != null)
                sw.WriteLine("Рекомендуется принять в качестве расчетного " + years.RecomendedYear.Year + " год");
            string nline = "Год;Полнота ряда, %;Интервал;Отклонение от среднемноголетней скорости, м/с;Отклонение от среднемноголетней скорости, %;Отклонение повторяемости скорости;Средняя скорость, м/с";
            sw.WriteLine(nline);
            foreach (SinglePeriodInfo spi in years.Years)
            {
                nline = string.Format("{0};{1:f2};{2};{3:f2};{4:f2};{5:f2};{6:f2}", spi.Year, spi.Completness, spi.Interval.Description(), spi.SpeedDeviation, spi.SpeedDeviationPercent, spi.ExpectancyDeviation, spi.AverageSpeed);
                sw.WriteLine(nline);
            }
            sw.Close();
        }

        /// <summary>
        /// Сохранение ВЭК в формате CSV
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="range"></param>
        public override void SaveEnergyInfo(string filename, RawRange range)
        {

            List<object> years = new List<object>();
            foreach (RawItem item in range)
            {
                if (!years.Contains(item.Date.Year))
                    years.Add(item.Date.Year);
            }

            //формирование заголовка
            string cap = "Год;Месяц;кол-во изм";
            foreach (GradationItem grad in Vars.Options.CurrentSpeedGradation.Items)
                cap += ";" + grad.Average.ToString("0.00");
            cap += ";Vmin, м/с;Vmax, м/с;Vср, м/с;Cv(V);параметр γ;параметр β;Nвал уд., Вт/м^2;Эвал уд., Вт*ч/м^2";
            foreach (WindDirections wd in WindDirections.Calm.GetEnumItems().GetRange(0, 17))
                cap += ";" + wd.Description();

            //запись в файл
            saveEnergyInfoLine(filename, null, null, null, null, cap, "", "", 0, false); //запись заголовка

            //запись данных обо всём периоде
            EnergyInfo ri1 = StatisticEngine.ProcessRange(range);
            StatisticalRange<WindDirections> sd1 = StatisticEngine.GetDirectionExpectancy(range, GradationInfo<WindDirections>.Rhumb16Gradations);
            StatisticalRange<GradationItem> ss1 = StatisticEngine.GetExpectancy(range, Vars.Options.CurrentSpeedGradation);
            EnergyInfo ei1 = StatisticEngine.ProcessRange(ss1);
            saveEnergyInfoLine(filename, ri1, ei1, sd1, ss1, null, "Все года", "Все месяцы", range.Count, true);

            //запись данных для каждого года
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
                    saveEnergyInfoLine(filename, ri, ei, sd, ss, null, year.ToString(), month.Description(), rn.Count, true);
                }
            }
        }

        /// <summary>
        /// сохранение файла в формате CSV
        /// </summary>
        /// <param name="rang"></param>
        /// <param name="filename"></param>
        internal override void SaveRange(RawRange rang, string filename)
        {
            StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
            string coordinates = rang.Position.Lat.ToString("0.000000") + " " + rang.Position.Lng.ToString("0.000000");
            string caption = "Местное время;T;U;DD;ff;P0";
            string title;
            if (rang.Meteostation == null)
                title = "ID=undefined";
            else
                title = $"ID={rang.Meteostation.ID}";
            sw.WriteLine(title);
            sw.WriteLine(coordinates);
            sw.WriteLine(rang.Name);
            sw.WriteLine(caption);
            foreach (RawItem item in rang)
            {
                if (double.IsNaN(item.Direction) || double.IsNaN(item.Speed) || item.DirectionRhumb == WindDirections.Undefined)
                    continue;
                sw.WriteLine($"{item.Date.ToString("dd.MM.yyyy HH:mm")};{item.Temperature};{item.Wetness};{item.Direction};{item.Speed};{item.Pressure}");
            }

            sw.Close();
        }
    }
}
