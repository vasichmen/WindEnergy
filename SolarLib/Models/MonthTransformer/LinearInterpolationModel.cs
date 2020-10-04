using CommonLib;
using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using SolarEnergy.SolarLib.Models.Interfaces;
using System;

namespace SolarEnergy.SolarLib.Models.MonthTransformer
{
    /// <summary>
    /// линейная интерполяция для значений между месяцами
    /// </summary>
    public class LinearInterpolationModel : IMonthTransformerModel
    {
        private DataHours<double> allSky;
        private DataHours<double> clearSky;



        public DataRange GenerateRange(DataItem dataItem)
        {
            allSky = prepareData(dataItem.DatasetAllsky);
            clearSky = prepareData(dataItem.DatasetClearSky);


            DataRange res = new DataRange();

            for (int i = 0; i < 8760; i++)
            {
                long ticks = (long)(i * 60 * 60 * 10e6);
                DateTime date = new DateTime(ticks);
                Months month = (Months)date.Month;
                int hour = date.Hour;

                double clsk = clearSky[i];
                double alsk = allSky[i];

                RawItem ni = new RawItem(date, alsk, clsk);
                res.Add(ni);
            }

            return res;

        }

        /// <summary>
        /// интерполяция каждого часа отдельно для всех месяцев
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DataHours<double> prepareData(Dataset data)
        {
            DataHours<double> res = new DataHours<double>();
            double[,] arr = new double[24, 365];

            //заполнение недостающих значений
            for (int h = 0; h < 24; h++)
                for (int d = 0; d < 365;)
                    foreach (Months month in Months.All.GetEnumItems())
                    {
                        if (month == Months.All)
                            continue;
                        arr[h, d] = data[month][h];
                        int daysInMonth = DateTime.DaysInMonth(1999, (int)month); // количество дней в месяце обычного года

                        if (month != Months.December) //если это не последний месяц, то заполняем промежуток
                            fillSpace(ref arr, h, d + 1, daysInMonth - 1, arr[h, d], data[month + 1][h]);
                        d += daysInMonth; //следующее значение через месяц
                    }

            //заполнение результата
            for (int h = 0; h < 24; h++)
                for (int d = 0; d < 365; d++)
                {
                    int hour = d * 24 + h;
                    res[hour] = arr[h, d];
                }
            return res;
        }

        /// <summary>
        /// Заполнение строки массива интерполированными данными
        /// </summary>
        /// <param name="arr">массив</param>
        /// <param name="h">час (строка массива)</param>
        /// <param name="startDay">начальный день, с которого надо начать заполнять</param>
        /// <param name="length">сколько дней заполнять</param>
        /// <param name="fromValue">начальное значение</param>
        /// <param name="toValue">конечное значение</param>
        private void fillSpace(ref double[,] arr, int h, int startDay, int length, double fromValue, double toValue)
        {
            int endDay = startDay + length;
            for (int i = startDay; i <= endDay; i++)
                arr[h, i] = LinearInterpolation(startDay - 1, endDay + 1, fromValue, toValue, i);
        }


        /// <summary>
        /// линейная интерполяция между двумя заданными точками
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double LinearInterpolation(double x1, double x2, double y1, double y2, double x)
        {
            return y2 + ((y1 - y2) / (x1 - x2)) * (x - x2);
        }
    }
}
