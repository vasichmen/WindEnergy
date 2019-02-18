﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Statistic.Structures
{
    /// <summary>
    /// диапазон одной градации
    /// </summary>
    public struct GradationItem
    {
        private readonly double _from;
        private readonly double _to;
        private bool notEmpty;

        public double From => _from;
        public double To => _to;

        /// <summary>
        /// истина, если структура пустая
        /// </summary>
        public bool isEmpty { get { return !notEmpty; } }

        public GradationItem(double from, double to)
        {
            _from = from;
            _to = to;
            notEmpty = true;
        }


        static GradationItem()
        { Empty = new GradationItem(); }

        /// <summary>
        /// пустое значение структуры
        /// </summary>
        public static readonly GradationItem Empty;
    }
}