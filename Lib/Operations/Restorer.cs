using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Interpolation;
using WindEnergy.Lib.Operations.Structures;

namespace WindEnergy.Lib.Operations
{
    /// <summary>
    /// восстановление ряда данных до указанного интервала
    /// </summary>
    public static class Restorer
    {

        /// <summary>
        /// восстановить ряд до нужного интревала наблюдений
        /// </summary>
        /// <param name="range"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static RawRange ProcessRange(RawRange range, RestorerParameters param)
        {
            if (param.Method == InterpolateMethods.NearestMeteostation && param.Coordinates.IsEmpty)
                throw new Exception("Для этого метода интерполяции необходимо указать расчетную точку на карте");

            Dictionary<double, double>
                speedFunc = new Dictionary<double, double>(), //функция скорости
                directsFunc = new Dictionary<double, double>(), //функция направления
                wetFunc = new Dictionary<double, double>(), //функция влажности
                tempFunc = new Dictionary<double, double>(); //функция температуры

            //заполнение известными значениями функции
            foreach (var item in range)
            {
                double timeStamp = item.DateArgument;
                if (speedFunc.ContainsKey(timeStamp))
                    throw new ApplicationException("В ряде наблюдений есть повторяющиеся даты. Восстановление невозможно, попробуйте проверить ряд на ошибки.");
                speedFunc.Add(timeStamp, item.Speed);
                directsFunc.Add(timeStamp, item.Direction);
                wetFunc.Add(timeStamp, item.Wetness);
                tempFunc.Add(timeStamp, item.Temperature);
            }

            //новый интервал наблюдений в минутах
            int newInterval = (int)param.Interval;


            //метки времени для нового ряда
            List<double> newRangeX = new List<double>();
            for (double i = range[range.Count - 1].DateArgument; i <= range[0].DateArgument; i += newInterval)
                newRangeX.Add(i);

            //создание интерполяторов функций скорости, направления, температуры, влажности
            IInterpolateMethod methodSpeeds, methodDirects, methodWet, methodTemp;
            switch (param.Method)
            {
                case InterpolateMethods.Linear:
                    methodSpeeds = new LinearInterpolateMethod(speedFunc);
                    methodDirects = new LinearInterpolateMethod(directsFunc);
                    methodTemp = new LinearInterpolateMethod(tempFunc);
                    methodWet = new LinearInterpolateMethod(wetFunc);
                    break;
                case InterpolateMethods.Stepwise:
                    methodSpeeds = new StepwiseInterpolateMethod(speedFunc);
                    methodDirects = new StepwiseInterpolateMethod(directsFunc);
                    methodTemp = new StepwiseInterpolateMethod(tempFunc);
                    methodWet = new StepwiseInterpolateMethod(wetFunc);
                    break;
                case InterpolateMethods.NearestMeteostation:
                    if (param.Coordinates.IsEmpty)
                        methodSpeeds = new NearestMSInterpolateMethod(speedFunc, param.BaseRange, MeteorologyParameters.Speed);
                    else
                        methodSpeeds = new NearestMSInterpolateMethod(speedFunc, param.Coordinates, MeteorologyParameters.Speed);
                    methodDirects = new NearestMSInterpolateMethod(directsFunc, (NearestMSInterpolateMethod)methodSpeeds, MeteorologyParameters.Direction);
                    methodTemp = new NearestMSInterpolateMethod(tempFunc, (NearestMSInterpolateMethod)methodSpeeds, MeteorologyParameters.Temperature);
                    methodWet = new NearestMSInterpolateMethod(wetFunc, (NearestMSInterpolateMethod)methodSpeeds, MeteorologyParameters.Wetness);
                    break;
                default: throw new Exception("Этот метод не реализован");
            }

            //создание нового ряда
            RawRange res = new RawRange();
            res.BeginChange();
            foreach (var p in newRangeX)
            {
                double speed = methodSpeeds.GetValue(p);
                double direct = methodDirects.GetValue(p);
                double temp = methodTemp.GetValue(p);
                double wet = methodWet.GetValue(p);
                res.Add(new RawItem(p, speed, direct, temp, wet));
            }
            res.EndChange();
            return res;
        }

    }
}
