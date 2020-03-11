using CommonLib.Classes;
using SolarEnergy.SolarLib.Classes.Structures;
using SolarEnergy.SolarLib.Models.Hours;
using SolarEnergy.SolarLib.Models.Interfaces;
using SolarEnergy.SolarLib.Models.MonthTransformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Classes.Collections
{
    public class DataRange : RawRange
    {
        /// <summary>
        /// создает новый ряд данных на основе статистики
        /// </summary>
        /// <param name="item"></param>
        /// <param name="modelKind"></param>
        public DataRange(DataItem item, MonthTransformationModels modelKind) : base()
        {
            if (item == null || modelKind == MonthTransformationModels.None)
                return;

            IMonthTransformerModel model;
            switch (modelKind)
            {
                case MonthTransformationModels.Constant:
                    model = new ConstantModel();
                    break;
                case MonthTransformationModels.LinearInterpolation:
                    model = new LinearInterpolationModel();
                    break;
                default: throw new Exception("Эта модель не реализована");
            }

            this.AddRange(model.GenerateRange(item));
        }

        /// <summary>
        /// создает новый ряд данных
        /// </summary>
        public DataRange() : base()
        { }

        /// <summary>
        /// создает новый ряд даннях на основе ряда наблюдений
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataRangeConverterParams"></param>
        public DataRange(RawRange data, DataRangeConverterParams param) : base()
        {
            data = data ?? throw new ArgumentNullException(nameof(data));
            param = param ?? throw new ArgumentNullException(nameof(param));

            DataRange range = null;
            switch (param.Interval)
            {
                case StandartIntervals.D1:
                    range = getFromDaily(data, param);
                    break;
                default: throw new Exception("Этот период не реализован");
            }

            this.AddRange(range);
        }

        /// <summary>
        /// получить ряд и суточных наблюдений
        /// </summary>
        /// <param name="data"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private DataRange getFromDaily(RawRange data, DataRangeConverterParams param)
        {
            //выбоор данных
            RawRange selectedRange = new RawRange();
            switch (param.SourceType)
            {
                case NasaSourceTypes.AllPeriod:
                    for (int i = 0; i < 365; i++)
                    {
                        var thisDay = from t in data
                                      where t.Date.DayOfYear == i + 1
                                      select t;
                        double averAllsk = 0, allskCount = 0;
                        double averClearsk = 0, clskCount = 0;
                        foreach (RawItem ri in thisDay)
                        {
                            averAllsk += !double.IsNaN(ri.AllSkyInsolation) ? ri.AllSkyInsolation : 0;
                            averClearsk += !double.IsNaN(ri.ClearSkyInsolation) ? ri.ClearSkyInsolation : 0;

                            clskCount += !double.IsNaN(ri.AllSkyInsolation) ? 1 : 0;
                            allskCount += !double.IsNaN(ri.ClearSkyInsolation) ? 1 : 0;
                        }
                        averClearsk /= clskCount;
                        averAllsk /= allskCount;
                        DateTime dt = new DateTime(0) + TimeSpan.FromDays(i);
                        selectedRange.Add(new RawItem(dt, averAllsk, averClearsk));
                    }

                    break;
                case NasaSourceTypes.Maximal:

                    for (int i = 0; i < 365; i++)
                    {
                        var thisDay = from t in data
                                      where t.Date.DayOfYear == i + 1
                                      select t;
                        double maxD = double.MinValue;
                        RawItem max = null;
                        foreach (RawItem ri in thisDay)
                            if (maxD < ri.AllSkyInsolation && !double.IsNaN(ri.AllSkyInsolation) && !double.IsNaN(ri.ClearSkyInsolation))
                            {
                                maxD = ri.AllSkyInsolation;
                                max = ri;
                            }
                        DateTime dt = new DateTime(0) + TimeSpan.FromDays(i);
                        selectedRange.Add(new RawItem(dt, max.AllSkyInsolation, max.ClearSkyInsolation));
                    }


                    break;
                case NasaSourceTypes.Minimal:
                    for (int i = 0; i < 365; i++)
                    {
                        var thisDay = from t in data
                                      where t.Date.DayOfYear == i + 1
                                      select t;
                        double minD = double.MaxValue;
                        RawItem min = null;
                        foreach (RawItem ri in thisDay)
                            if (minD > ri.AllSkyInsolation && !double.IsNaN(ri.AllSkyInsolation) && !double.IsNaN(ri.ClearSkyInsolation))
                            {
                                minD = ri.AllSkyInsolation;
                                min = ri;
                            }
                        DateTime dt = new DateTime(0) + TimeSpan.FromDays(i);
                        selectedRange.Add(new RawItem(dt, min.AllSkyInsolation, min.ClearSkyInsolation));
                    }
                    break;
                case NasaSourceTypes.SelectedYear:
                    var collection = from t in data
                                     where t.Date.Year == param.Year
                                     select t;
                    selectedRange.AddRange(collection);
                    break;
                default: throw new Exception("Этот тип источников данных не реализован");
            }

            if (selectedRange.Count != 365)
                throw new Exception("Не все данные получены");

            //выбор модели
            IHoursModel model;
            switch (param.HourModel)
            {
                case HourModels.Uniform:
                    model = new UniformModel();
                    break;
                default: throw new Exception("Эта модель не реализована");
            }

            //преобразование к часовому ряду
            DataRange res = new DataRange();
            foreach (RawItem item in selectedRange)
            {
                DataHours<double> hoursAll = model.GetData(item.AllSkyInsolation, MeteorologyParameters.AllSkyInsolation);
                DataHours<double> hoursClear = model.GetData(item.ClearSkyInsolation, MeteorologyParameters.ClearSkyInsolation);

                DataRange day = new DataRange();
                for (int h = 0; h < 24; h++)
                {
                    DateTime dt = new DateTime(01999, item.Date.Month, item.Date.Day, h, 0, 0);
                    double alsk = hoursAll[h];
                    double clsk = hoursClear[h];
                    RawItem ri = new RawItem(dt, alsk, clsk);
                    day.Add(ri);
                }
                res.AddRange(day);
            }

            if (res.Count != 8760)
                throw new WindEnergyException("Ошибка при преобразованииряда наблюдений");
            return res;
        }
    }
}
