﻿using System.Collections.Generic;
using System.Linq;

namespace WindEnergy.WindLib.Classes.Structures
{
    /// <summary>
    /// Запись в таблице БД оборудования
    /// </summary>
    public class EquipmentItemInfo
    {
        public EquipmentItemInfo()
        {
            this.PerformanceCharacteristic = new Dictionary<double, double>();
            this.TowerHeight = new List<double>();
        }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Модель
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// производитель
        /// </summary>
        public string Developer { get; set; }

        /// <summary>
        /// Номинальная мощность, кВт
        /// </summary>
        public double Power { get; set; }

        /// <summary>
        /// тип регулирования
        /// </summary>
        public TurbineRegulations Regulator { get; set; }

        /// <summary>
        /// Диаметр рабочего колеса, м
        /// </summary>
        public double Diameter { get; set; }

        /// <summary>
        /// максимальная скорость ветра м/с
        /// </summary>
        public double MaxWindSpeed { get; set; }

        /// <summary>
        /// Варианты высот башни, м
        /// </summary>
        public List<double> TowerHeight { get; set; }
        public string TowerHeightString
        {
            get
            {
                string res = "";
                foreach (double a in TowerHeight)
                    res += a.ToString() + "/";

                return res.Trim(new char[] { '/' });
            }
        }

        /// <summary>
        /// Минимальная скорость ветра, м/с
        /// </summary>
        public double MinWindSpeed { get; set; }

        /// <summary>
        /// Расчетная скорость ветра, м/с
        /// </summary>
        public double NomWindSpeed { get; set; }

        /// <summary>
        /// истина, если заполнена мощностная характеристика
        /// </summary>
        public bool HasCharacteristic { get { return this.PerformanceCharacteristic.Count > 0; } }

        /// <summary>
        /// истина, если у этой записи достаточно данных для расчета
        /// </summary>
        public bool EnoughDataToCalculate
        {
            get
            {
                return !double.IsNaN(MinWindSpeed) && !double.IsNaN(NomWindSpeed) && Regulator != TurbineRegulations.None;
            }
        }



        /// <summary>
        /// Рабочая характеристика, N(V), кВт, м/с
        /// </summary>
        public Dictionary<double, double> PerformanceCharacteristic { get; set; }
    }
}
