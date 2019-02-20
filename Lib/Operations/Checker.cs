﻿using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Limits;
using WindEnergy.Lib.Operations.Structures;

namespace WindEnergy.Lib.Operations
{
    /// <summary>
    /// проверка и устранение ошибок в ряде
    /// </summary>
    public static class Checker
    {
        /// <summary>
        /// проверить ряд и устранить ошибки
        /// </summary>
        /// <param name="range">ряд</param>
        /// <param name="param">параметры обработки ошибок</param>
        /// <returns></returns>
        public static RawRange ProcessRange(RawRange range, CheckerParameters param)
        {
            ILimitsProvider provider;


            switch (param.LimitsProvider)
            {
                case LimitsProviders.Manual:
                    if ((param.Coordinates.IsEmpty || param.LimitsProvider == LimitsProviders.None) && param.CheckByPos)
                        throw new ArgumentException("не хватает данных для проверки ряда");
                    else
                        provider = new ManualLimits(param.DirectionInclude, param.SpeedInclude);
                    break;
                case LimitsProviders.StaticLimits:
                    provider = new StaticRegionLimits(Vars.Options.StaticRegionLimitsSourceFile);
                    break;
                default: throw new Exception("Этот провайдер не реализован");

            }

            RawRange res = new RawRange();
            res.BeginChange();
            List<DateTime> dates = new List<DateTime>();
            foreach (RawItem item in range)
            {
                bool accept = provider.CheckItem(item, param.Coordinates); //проверка по диапазону

                if (accept) //если всё ещё подходит, то проверяем дату
                    accept &= !dates.Contains(item.Date); //проверка повтора даты

                if (accept)//если всё ещё подходит, то проверем значения скорости и направления (если не штиль, не переменное направление и не неопределённое и скорость равна 0 то не подходит)
                    if (item.DirectionRhumb != WindDirections.Undefined && item.DirectionRhumb != WindDirections.Variable && item.DirectionRhumb != WindDirections.Calm && item.Speed == 0)
                        accept = false;

                if (accept)
                {
                    res.Add(item);
                    dates.Add(item.Date);
                }
            }
            res.EndChange();
            return res;
        }


    }
}
