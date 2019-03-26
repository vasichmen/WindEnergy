﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Generic;
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
        public static RawRange ProcessRange(RawRange Range, RestorerParameters param)
        {
            if (param.Method == InterpolateMethods.NearestMeteostation && param.Coordinates.IsEmpty)
                throw new Exception("Для этого метода интерполяции необходимо указать расчетную точку на карте");

            //ПОДГОТОВКА ФУНКЦИЙ

            Dictionary<double, double>
                speedFunc = new Dictionary<double, double>(), //функция скорости
                directsFunc = new Dictionary<double, double>(), //функция направления
                wetFunc = new Dictionary<double, double>(), //функция влажности
                tempFunc = new Dictionary<double, double>(); //функция температуры

            Range.Sort(new DateTimeComparer());

            //заполнение известными значениями функции
            foreach (var item in Range)
            {
                double timeStamp = item.DateArgument;
                if (speedFunc.ContainsKey(timeStamp))
                    throw new ApplicationException("В ряде наблюдений есть повторяющиеся даты. Восстановление невозможно, попробуйте проверить ряд на ошибки.");
                speedFunc.Add(timeStamp, item.Speed);
                directsFunc.Add(timeStamp, item.Direction);
                wetFunc.Add(timeStamp, item.Wetness);
                tempFunc.Add(timeStamp, item.Temperature);
            }
                        
            //ПОДГОТОВКА ИНТЕРПОЛЯТОРОВ
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
                    RawRange baseRange;//ряд, на основе которого будет идти восстановление
                    if (param.Coordinates.IsEmpty)
                    {
                        //проверяем заданный ряд на возможность восстановления с помощью него
                        bool f = NearestMSInterpolateMethod.CheckNormalLaw(param.BaseRange, Vars.Options.NormalLawPirsonCoefficientDiapason);
                        if (f) baseRange = param.BaseRange;
                        else throw new WindEnergyException("Этот ряд не может быть использован так как он не подчиняется нормальному закону распределения");
                    }
                    else
                        //ищем подходящую МС из ближайших и получаем её ряд. Если подходит, то ошибки нет
                        baseRange = NearestMSInterpolateMethod.TryGetBaseRange(Range, param.Coordinates);

                    methodSpeeds = new NearestMSInterpolateMethod(speedFunc, baseRange, MeteorologyParameters.Speed);
                    methodDirects = new NearestMSInterpolateMethod(directsFunc, baseRange, MeteorologyParameters.Direction);
                    methodTemp = new LinearInterpolateMethod(tempFunc);
                    methodWet = new LinearInterpolateMethod(wetFunc);
                    break;
                default: throw new Exception("Этот метод не реализован");
            }

            //СОЗДАНИЕ НОВОГО РЯДА
            //новый интервал наблюдений в минутах
            int newInterval = (int)param.Interval;

            //метки времени для нового ряда
            List<double> newRangeX = new List<double>();
            for (double i = Range[0].DateArgument; i <= Range[Range.Count - 1].DateArgument; i += newInterval)
                newRangeX.Add(i);

            //расчет каждого значения
            RawRange res = new RawRange();
            res.Position = Range.Position;
            res.BeginChange();
            foreach (var p in newRangeX)
            {
                double speed = methodSpeeds.GetValue(p);
                double direct = methodDirects.GetValue(p);
                double temp = methodTemp.GetValue(p);
                double wet = methodWet.GetValue(p);
                if (double.IsNaN(speed) ||
                    double.IsNaN(direct) ||
                    double.IsNaN(temp) ||
                    double.IsNaN(wet))
                    continue;
                res.Add(new RawItem(p, speed, direct, temp, wet));
            }
            res.EndChange();
            return res;
        }

    }
}
