﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Interpolation;

namespace WindEnergy.Lib.Operations
{
    /// <summary>
    /// восстановление ряда данных до указанного интервала
    /// </summary>
    public static class Restorer
    {
        /// <summary>
        /// параметры восстановления ряда
        /// </summary>
        public class RestoreParameters
        {
            /// <summary>
            /// создаёт значения по умолчанию
            /// </summary>
            public RestoreParameters()
            {
                Interval = InterpolateIntervals.H1;
                Method = InterpolateMathods.Stepwise;
            }

            /// <summary>
            /// интервал наблюдений в новом ряде
            /// </summary>
            public InterpolateIntervals Interval { get; set; }

            /// <summary>
            /// метод интерполяции
            /// </summary>
            public InterpolateMathods Method { get; set; }
        }

        /// <summary>
        /// восстановить ряд до нужного интревала наблюдений
        /// </summary>
        /// <param name="range"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static RawRange ProcessRange(RawRange range, RestoreParameters param)
        {
            Dictionary<double, double>
                speedFunc = new Dictionary<double, double>(), //функция скорости
                directsFunc = new Dictionary<double, double>(), //функция направления
                wetFunc = new Dictionary<double, double>(), //функция влажности
                tempFunc = new Dictionary<double, double>(); //функция температуры

            //заполнение известными значениями функции
            foreach (var item in range)
            {
                double timeStamp = item.DateArgument;
                speedFunc.Add(timeStamp, item.Speed);
                directsFunc.Add(timeStamp, item.Direction);
                wetFunc.Add(timeStamp, item.Wetness);
                tempFunc.Add(timeStamp, item.Temperature);
            }

            //новый интервал наблюдений в минутах
            double newInterval;
            switch (param.Interval)
            {
                case InterpolateIntervals.D1:
                    newInterval = 24 * 60; // 1 день
                    break;
                case InterpolateIntervals.H1:
                    newInterval = 60; // 1 час
                    break;
                case InterpolateIntervals.H3:
                    newInterval = 3 * 60; // 3 часа
                    break;
                case InterpolateIntervals.H6:
                    newInterval = 6 * 60; // 6 часов
                    break;
                case InterpolateIntervals.M10:
                    newInterval = 10; // 10 минут
                    break;
                case InterpolateIntervals.M30:
                    newInterval = 30; // 30 минут
                    break;
                default: throw new Exception("Этот интервал времени не реализован");
            }


            //метки времени для нового ряда
            List<double> newRangeX = new List<double>();
            for (double i = range[range.Count - 1].DateArgument; i <= range[0].DateArgument; i += newInterval)
                newRangeX.Add(i); //TODO: проверить количество новых элементов ряда

            //создание интерполяторов функций скорости, направления, температуры, влажности
            IInterpolateMethod methodSpeeds, methodDirects, methodWet, methodTemp;
            switch (param.Method)
            {
                case InterpolateMathods.Linear:
                    methodSpeeds = new LinearInterpolateMethod(speedFunc);
                    methodDirects = new LinearInterpolateMethod(directsFunc);
                    methodTemp = new LinearInterpolateMethod(tempFunc);
                    methodWet = new LinearInterpolateMethod(wetFunc);
                    break;
                case InterpolateMathods.Stepwise:
                    methodSpeeds = new StepwiseInterpolateMethod(speedFunc);
                    methodDirects = new StepwiseInterpolateMethod(directsFunc);
                    methodTemp = new StepwiseInterpolateMethod(tempFunc);
                    methodWet = new StepwiseInterpolateMethod(wetFunc);
                    break;
                default: throw new Exception("Этот метод не реализован");
            }

            //создание нового ряда
            RawRange res = new RawRange();
            foreach (var p in newRangeX)
            {
                double speed = methodSpeeds.GetValue(p);
                double direct = methodDirects.GetValue(p);
                double temp = methodTemp.GetValue(p);
                double wet = methodWet.GetValue(p);
                res.Add(new RawItem(p,speed,direct,temp,wet));
            }
            return res;
        }

    }
}
