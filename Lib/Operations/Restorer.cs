using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Collections.Generic;
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
        public static void ProcessRange(RawRange Range, RestorerParameters param, Action<int> actionPercent, Action<RawRange> actionAfter)
        {
            if (param.Method == InterpolateMethods.NearestMeteostation && param.Coordinates.IsEmpty)
                throw new Exception("Для этого метода интерполяции необходимо указать расчетную точку на карте");

            //ПОДГОТОВКА ФУНКЦИЙ

            Dictionary<double, double>
                speedFunc = new Dictionary<double, double>(), //функция скорости
                directsFunc = new Dictionary<double, double>(), //функция направления
                wetFunc = new Dictionary<double, double>(), //функция влажности
                tempFunc = new Dictionary<double, double>(), //функция температуры
                pressFunc = new Dictionary<double, double>(); //функция давления

            Range = new RawRange(Range.OrderBy(x => x.Date).ToList());

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
                    tempFunc.Add(timeStamp, item.Pressure);
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
                    methodPress = new LinearInterpolateMethod(pressFunc);
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
                    {
                        //ищем подходящую МС из ближайших и получаем её ряд. Если подходит, то ошибки нет
                        baseRange = NearestMSInterpolateMethod.TryGetBaseRange(Range, param.Coordinates);
                    }

                    methodSpeeds = new NearestMSInterpolateMethod(speedFunc, baseRange, MeteorologyParameters.Speed);
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
                directsFunc.Keys.Min(),
                tempFunc.Keys.Min(),
                wetFunc.Keys.Min(),
                pressFunc.Keys.Min()
            };
            foreach (double st in starts)
                if (st > start)
                    start = st;
            for (double i = start; i <= Range[Range.Count - 1].DateArgument; i += newInterval)
                newRangeX.Add(i);

            //расчет каждого значения
            RawRange res = new RawRange();
            res.Position = Range.Position;
            res.BeginChange();
            Task tsk = new Task(() =>
            {
                double c = 0;
                foreach (var p in newRangeX)
                {
                    c++;
                    if (Math.IEEERemainder(c, 100) == 0 && actionPercent != null)
                        actionPercent.Invoke((int)((c / newRangeX.Count) * 100));
                    double speed = methodSpeeds.GetValue(p);
                    double direct = methodDirects.GetValue(p);
                    double temp = methodTemp.GetValue(p);
                    double wet = methodWet.GetValue(p);
                    double press = methodPress.GetValue(p);
                    if (double.IsNaN(speed) ||
                        double.IsNaN(direct) ||
                        double.IsNaN(temp) ||
                        double.IsNaN(press) ||
                        double.IsNaN(wet))
                        continue;
                    res.Add(new RawItem(p, speed, direct, temp, wet,press));
                }
                res.EndChange();
                actionAfter.Invoke(res);
            });
            tsk.Start();
            return;
        }

    }
}
