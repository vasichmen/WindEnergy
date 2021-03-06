﻿using CommonLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Transformation.Restore.Interpolation;
using WindLib;

namespace WindEnergy.WindLib.Transformation.Restore
{
    /// <summary>
    /// восстановление ряда данных до указанного интервала
    /// </summary>
    public static class Restorer
    {
        /// <summary>
        /// восстановить ряд до нужного интревала наблюдений
        /// </summary>
        /// <param name="range">Ряд для восстановления</param>
        /// <param name="param">параметры восстановления</param>
        /// <param name="actionPercent">изменение процента выполнения</param>
        /// <param name="actionAfter">действие после обработки</param>
        /// <returns></returns>
        public static void ProcessRange(RawRange range, RestorerParameters param, Action<int, string> actionPercent, Action<RawRange, RawRange, double> actionAfter)
        {
            RawRange baseRange = null;//ряд, на основе которого будет идти восстановление
            double r = double.NaN; //коэффициент корреляции


            if (param.Method == InterpolateMethods.NearestMeteostation && param.Coordinates.IsEmpty)
                throw new Exception("Для этого метода интерполяции необходимо указать расчетную точку на карте");

            //ПОДГОТОВКА ФУНКЦИЙ

            Dictionary<double, double>
                speedFunc = new Dictionary<double, double>(), //функция скорости
                directsFunc = new Dictionary<double, double>(), //функция направления
                wetFunc = new Dictionary<double, double>(), //функция влажности
                tempFunc = new Dictionary<double, double>(), //функция температуры
                pressFunc = new Dictionary<double, double>(); //функция давления


            RawRange Range = new RawRange(range.OrderBy(x => x.Date).ToList());

            //заполнение известными значениями функции
            foreach (var item in Range)
            {
                double timeStamp = item.DateArgument;
                if (speedFunc.ContainsKey(timeStamp))
                    throw new ApplicationException("В ряде наблюдений есть повторяющиеся даты. Восстановление невозможно, попробуйте проверить ряд на ошибки.");
                if (!double.IsNaN(item.Speed))
                    speedFunc.Add(timeStamp, item.Speed);
                if (!double.IsNaN(item.Direction))
                    directsFunc.Add(timeStamp, item.Direction);
                if (!double.IsNaN(item.Wetness))
                    wetFunc.Add(timeStamp, item.Wetness);
                if (!double.IsNaN(item.Temperature))
                    tempFunc.Add(timeStamp, item.Temperature);
                if (!double.IsNaN(item.Pressure))
                    pressFunc.Add(timeStamp, item.Pressure);
            }

            //ПОДГОТОВКА ИНТЕРПОЛЯТОРОВ
            //создание интерполяторов функций скорости, направления, температуры, влажности
            IInterpolateMethod
                methodSpeeds,
                methodDirects,
                methodWet,
                methodTemp,
                methodPress;
            switch (param.Method)
            {
                case InterpolateMethods.Linear:
                    methodSpeeds = new LinearInterpolateMethod(speedFunc);
                    methodDirects = new LinearInterpolateMethod(directsFunc);
                    methodTemp = new LinearInterpolateMethod(tempFunc);
                    methodWet = new LinearInterpolateMethod(wetFunc);
                    methodPress = new LinearInterpolateMethod(pressFunc);
                    break;
                case InterpolateMethods.Stepwise:
                    methodSpeeds = new StepwiseInterpolateMethod(speedFunc);
                    methodDirects = new StepwiseInterpolateMethod(directsFunc);
                    methodTemp = new StepwiseInterpolateMethod(tempFunc);
                    methodWet = new StepwiseInterpolateMethod(wetFunc);
                    methodPress = new StepwiseInterpolateMethod(pressFunc);
                    break;
                case InterpolateMethods.NearestMeteostation:
                    if (param.BaseRange != null)
                    {
                        //проверяем заданный ряд на возможность восстановления с помощью него
                        bool f = NearestMSInterpolateMethod.CheckNormalLaw(param.BaseRange, Vars.Options.NormalLawPirsonCoefficientDiapason);
                        if (f) baseRange = param.BaseRange;
                        else throw new WindEnergyException("Этот ряд не может быть использован так как он не подчиняется нормальному закону распределения");

                    }
                    else
                    {
                        //ищем подходящую МС из ближайших и получаем её ряд. Если подходит, то ошибки нет
                        baseRange = NearestMSInterpolateMethod.TryGetBaseRange(Range, param.Coordinates, out r, actionPercent);
                    }

                    methodSpeeds = new NearestMSInterpolateMethod(speedFunc, baseRange, MeteorologyParameters.Speed, param.ReplaceExistMeasurements);
                    methodDirects = new LinearInterpolateMethod(directsFunc);
                    methodTemp = new LinearInterpolateMethod(tempFunc);
                    methodWet = new LinearInterpolateMethod(wetFunc);
                    methodPress = new LinearInterpolateMethod(pressFunc);
                    break;
                default: throw new Exception("Этот метод не реализован");
            }

            //СОЗДАНИЕ НОВОГО РЯДА
            //новый интервал наблюдений в минутах
            int newInterval = (int)param.Interval;

            //метки времени для нового ряда
            List<double> newRangeX = new List<double>();
            double start = double.MinValue;
            double[] starts = new double[] {
                speedFunc.Keys.Min(),
                directsFunc.Keys.Count>0?directsFunc.Keys.Min():speedFunc.Keys.Min(),
                tempFunc.Keys.Count>0?tempFunc.Keys.Min():speedFunc.Keys.Min(),
                wetFunc.Keys.Count>0?wetFunc.Keys.Min():speedFunc.Keys.Min(),
                pressFunc.Keys.Count>0?pressFunc.Keys.Min():speedFunc.Keys.Min()
            };
            foreach (double st in starts)
                if (st > start)
                    start = st;
            for (double i = start; i <= Range[Range.Count - 1].DateArgument; i += newInterval)
                newRangeX.Add(i);

            //расчет каждого значения
            RawRange res = new RawRange();
            res.Position = range.Position;
            res.Meteostation = range.Meteostation;
            res.BeginChange();
            Task tsk = new Task(() =>
            {
                double c = 0;
                foreach (var p in newRangeX)
                {
                    c++;
                    if (Math.IEEERemainder(c, 100) == 0 && actionPercent != null)
                        actionPercent.Invoke((int)((c / newRangeX.Count) * 100), "Изменение интервала наблюдений...");
                    double speed = methodSpeeds.GetValue(p);
                    double direct = methodDirects.GetValue(p);
                    double temp = methodTemp.GetValue(p);
                    double wet = methodWet.GetValue(p);
                    double press = methodPress.GetValue(p);
                    if (double.IsNaN(speed))
                        continue;
                    if (speed < 0)
                        speed = 0;
                    res.Add(new RawItem(p, speed, direct, temp, wet, press));
                }
                res.EndChange();
                actionAfter.Invoke(res, baseRange, r);
            });
            tsk.Start();
            return;
        }

    }
}
