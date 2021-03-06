﻿using System;

namespace CommonLib
{
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// значение атрибута Description у перечисления
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <param name="useNewLine">если истина, то результат будет разбит на несколько строк</param>
        /// <returns></returns>
        public static string ToText(this TimeSpan timeSpan, bool useNewLine = false)
        {
            string newline = useNewLine ? "\r\n" : " ";

            int years = (int)(timeSpan.TotalDays / 365);
            int months = (int)((timeSpan.TotalDays - years * 365) / 30);
            int days = (int)(timeSpan.TotalDays - years * 365 - months * 30);

            string yearsT = (years > 0) ? "лет: " + years.ToString() : "";
            string monthsT = (months > 0) ? "месяцев: " + months.ToString() : "";
            string daysT = (days < 0 && (years == 0 && months == 0)) ? "меньше одного дня" : (days > 0) ? "дней: " + days.ToString() : "";
            return yearsT + newline + monthsT + newline + daysT;
        }
    }
}
