﻿using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Transformation.Check.Limits;
using WindLib;

namespace WindEnergy.WindLib.Data.Providers.DB
{
    /// <summary>
    /// База данных ограничений скоростей ветра по регионам
    /// </summary>
    public class SpeedLimitsDatabase
    {
        private Dictionary<PointLatLng, ManualLimits> _staticSpeedLimits = null;

        /// <summary>
        /// список ограничений по регионам
        /// </summary>
        public Dictionary<PointLatLng, ManualLimits> List
        {
            get
            {
                if (_staticSpeedLimits == null || _staticSpeedLimits.Count == 0)
                    _staticSpeedLimits = loadStaticSpeedLimits(Vars.Options.StaticRegionLimitsSourceFile);
                return _staticSpeedLimits;
            }
        }

        /// <summary>
        /// пробует загрузить файл ограничений и возвращает true, если  загрузка удалась
        /// </summary>
        /// <param name="fileName">адрес файла</param>
        /// <returns></returns>
        public bool CheckRegionLimitsFile(string fileName)
        {
            try
            {
                loadStaticSpeedLimits(fileName);
                return true;
            }
            catch (Exception)
            { return false; }
        }

        /// <summary>
        /// загрузить список ограничений скоростей по точкам
        /// </summary>
        /// <param name="filename">адрес файла ограничения скоростей</param>
        /// <returns></returns>
        private Dictionary<PointLatLng, ManualLimits> loadStaticSpeedLimits(string filename)
        {
            Dictionary<PointLatLng, ManualLimits> limits = new Dictionary<PointLatLng, ManualLimits>();
            StreamReader sr = new StreamReader(filename);
            sr.ReadLine();//пропускаем первую строку-заголовок
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] arr = line.Split(';'); //название;широта;долгота;минимальная скорость;максимальная скорость
                if (arr.Length < 5)
                    continue;
                Diapason<double> d = new Diapason<double>(double.Parse(arr[3].Replace('.', Constants.DecimalSeparator)), double.Parse(arr[4].Replace('.', Constants.DecimalSeparator)));
                PointLatLng p = new PointLatLng(double.Parse(arr[1].Replace('.', Constants.DecimalSeparator)), double.Parse(arr[2].Replace('.', Constants.DecimalSeparator)));
                ManualLimits ml = new ManualLimits(new List<Diapason<double>>(), new List<Diapason<double>>() { d }) { Position = p, Name = arr[0] };
                if (!limits.ContainsKey(p))
                    limits.Add(p, ml);
            }
            sr.Close();
            return limits;
        }

    }
}
