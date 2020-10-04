using SolarEnergy.SolarLib.Models.Interfaces;
using System;
using System.Linq;

namespace SolarEnergy.SolarLib.Classes.Collections
{
    /// <summary>
    /// таблица информации о точке
    /// </summary>
    public class Dataset : DataMonthsHour<double>
    {
        public Dataset() : base() { }

        /// <summary>
        /// преобразует ряд наблюдений в статистику
        /// </summary>
        /// <param name="range"></param>
        public Dataset(RawRange range, MeteorologyParameters param, IHoursModel model) : this()
        {
            for (int i = 1; i <= 12; i++) //цикл по месяцам
            {
                switch (param)
                {
                    case MeteorologyParameters.AllSkyInsolation:

                        double aver = (from t in range
                                       where t.Date.Month == i
                                       select t.AllSkyInsolation).Average();
                        DataHours<double> hours = model.GetData(aver, param);
                        Months month = (Months)i;
                        this[month] = hours;
                        break;
                    case MeteorologyParameters.ClearSkyInsolation:

                        double aver2 = (from t in range
                                        where t.Date.Month == i
                                        select t.ClearSkyInsolation).Average();
                        DataHours<double> hours2 = model.GetData(aver2, param);
                        Months month2 = (Months)i;
                        this[month2] = hours2;
                        break;
                    default: throw new Exception("Этот параметр не реализован");
                }
            }
        }
    }
}
