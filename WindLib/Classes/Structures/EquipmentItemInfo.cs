using System;
using System.Collections.Generic;
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
        /// истина, если у этой записи достаточно данных для расчета мощностной характеристики
        /// </summary>
        public bool EnoughDataToCalculateCharacteristic
        {
            get
            {
                return
                    Regulator == TurbineRegulations.Pitch && //расчет характеристики возожен только для турбинс pitch регулированием
                    !double.IsNaN(MinWindSpeed) &&
                    !double.IsNaN(NomWindSpeed) &&
                    !double.IsNaN(MaxWindSpeed) &&
                    !double.IsNaN(Power);
            }
        }

        /// <summary>
        /// истина, если у этой записи достаточно данных для выполнения основного расчета  ВЭУ
        /// </summary>
        public bool EnoughDataToCalculatePower
        {
            get
            {
                return
                    EnoughDataToCalculateCharacteristic &&
                    TowerHeight.Count > 0;
            }
        }



        /// <summary>
        /// Рабочая характеристика, N(V), кВт, м/с
        /// </summary>
        public Dictionary<double, double> PerformanceCharacteristic { get; set; }

        /// <summary>
        /// Возвращает копию объекта
        /// </summary>
        /// <returns></returns>
        public EquipmentItemInfo Clone()
        {
            return new EquipmentItemInfo()
            {
                Developer = this.Developer,
                ID = this.ID,
                Diameter = this.Diameter,
                MaxWindSpeed = this.MaxWindSpeed,
                MinWindSpeed = this.MinWindSpeed,
                Model = this.Model,
                TowerHeight = this.TowerHeight.ToList(),
                NomWindSpeed = this.NomWindSpeed,
                PerformanceCharacteristic = new Dictionary<double, double>(this.PerformanceCharacteristic),
                Regulator = this.Regulator,
                Power = this.Power
            };
        }
    }
}
