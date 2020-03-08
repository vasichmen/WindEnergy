using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using SolarEnergy.SolarLib.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Models.MonthTransformer
{
    /// <summary>
    /// ступенчатый переход между месяцами
    /// </summary>
    public class ConstantModel : IMonthTransformerModel
    {
        public DataRange GenerateRange(DataItem dataItem)
        {
            DataRange res = new DataRange();

            for (int i = 0; i < 8760; i++)
            {
                long ticks = (long)( i * 60 * 60 * 10e6 );
                DateTime date = new DateTime(ticks);
                Months month = (Months)date.Month;
                int hour = date.Hour;

                double clsk = dataItem.DatasetClearSky[month][hour];
                double alsk = dataItem.DatasetAllsky[month][hour];

                RawItem ni = new RawItem(date, alsk,clsk);
                res.Add(ni);
            }

            return res;
        }
    }
}
