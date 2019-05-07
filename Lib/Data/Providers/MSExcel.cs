using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Collections;
using WindEnergy.Lib.Statistic.Structures;

namespace WindEnergy.Lib.Data.Providers
{
    /// <summary>
    /// работа с файлами формата MS Excel
    /// </summary>
    public static class MSExcel
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
        public static void SaveEnergyInfoCSV(
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
        public static RawRange LoadCSV(string fileName)
        {
            StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true);

            //определение формата файла csv
            string title = sr.ReadLine();
            sr.Close();
            if (title.Contains("WMO_ID") || title.Contains("METAR"))
                return RP5ru.LoadCSV(fileName);
            else throw new Exception("Файл повреждён или имеет неподдерживаемый формат");
        }

        /// <summary>
        /// сохранение статистики наблюдений в файл
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="qualityInfo"></param>
        public static void SaveRangeQualityInfoCSV(string fileName, QualityInfo qualityInfo, TimeSpan rangeLength)
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
        public static void SaveCalcYearInfoCSV(string fileName, CalculateYearInfo years)
        {
            StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8);
            if (years.RecomendedYear != null)
                sw.WriteLine("Рекомендуется принять в качестве расчетного " + years.RecomendedYear.Year + " год");
            string nline = "Год;Полнота ряда, %;Интервал;Отклонение от среднемноголетней скорости, м/с;Отклонение повторяемости скорости;Средняя скорость, м/с;Максимальная скорость, м/с";
            sw.WriteLine(nline);
            foreach (SinglePeriodInfo spi in years.Years)
            {
                nline = string.Format("{0};{1:f2};{2};{3:f2};{4:f2};{5:f2};{6:f2};{7:f1}", spi.Year, spi.Completness, spi.Interval.Description(), spi.SpeedDeviation, spi.SpeedDeviationPercent, spi.AverageSpeed, spi.ExpectancyDeviation, spi.Vmax);
                sw.WriteLine(nline);
            }
            sw.Close();
        }


    }
}
