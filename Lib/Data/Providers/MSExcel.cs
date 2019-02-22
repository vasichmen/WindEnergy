using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static void SaveEnergyInfoCSV(string fileName, EnergyInfo range_info, EnergyInfo ext_info, StatisticalRange<WindDirections> stat_directions, StatisticalRange<GradationItem> stat_speeds)
        {
            StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8);

            //по ряду наблюдений
            sw.WriteLine("Энергетические характеристики, расчитанные по ряду наблюдений");
            string nline = "Среднемноголетняя скорость, м/с;Среднее отклонение;Cv;Nуд.ср, Вт/м^2;Эуд, Вт*ч/м^2;Максимальная скорость, м/с";
            sw.WriteLine(nline);
            nline = string.Format("{0:f2};{1:f2};{2:f2};{3:f2};{4:f2};{5:f2}", range_info.V0, range_info.StandardDeviationSpeed, range_info.Cv, range_info.PowerDensity, range_info.EnergyDensity, range_info.Vmax);
            sw.WriteLine(nline);


            sw.WriteLine(";");
            //по повторяемости
            sw.WriteLine("Энергетические характеристики, расчитанные по повторяемости ветра");
            nline = "Среднемноголетняя скорость, м/с;Среднее отклонение;Cv;Nуд.ср, Вт/м^2;Эуд, Вт*ч/м^2";
            sw.WriteLine(nline);
            nline = string.Format("{0:f2};{1:f2};{2:f2};{3:f2};{4:f2}", ext_info.V0, ext_info.StandardDeviationSpeed, ext_info.Cv, ext_info.PowerDensity, ext_info.EnergyDensity);
            sw.WriteLine(nline);



            sw.WriteLine(";");
            //повторяемости скоростей ветра
            nline = "Повторяемость скоростей ветра";
            sw.WriteLine(nline);
            nline = "Градация;;Повторяемость, %";
            sw.WriteLine(nline);
            for (int j = 0; j < stat_speeds.Values.Count; j++)
            {
                nline = string.Format("{0};{1};{2:f2}", (stat_speeds.Keys[j] as GradationItem).From, (stat_speeds.Keys[j] as GradationItem).To, stat_speeds.Values[j] * 100);
                sw.WriteLine(nline);
            }


            sw.WriteLine(";");
            //повторяемости направлений ветра
            nline = "Повторяемость направлений ветра";
            sw.WriteLine(nline);
            nline = "Румб;Градусы;Повторяемость, %";
            sw.WriteLine(nline);
            List<Enum> rs = WindDirections.Calm.GetEnumItems().GetRange(1, 16);
            for (int j = 0; j < rs.Count; j++)
            {
                WindDirections rhumb = (WindDirections)rs[j];
                int index = stat_directions.Keys.IndexOf(rhumb);
                if (index == -1)
                    continue;
                nline = string.Format("{0};{1};{2:f2}", rhumb.Description(), j * 22.5d, stat_directions.Values[index] * 100);
                sw.WriteLine(nline);
            }
            sw.Close();           
        }
    }
}
