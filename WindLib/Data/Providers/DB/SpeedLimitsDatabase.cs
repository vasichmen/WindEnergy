using CommonLib.Classes.Base;
using GMap.NET;
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
    public class SpeedLimitsDatabase : BaseFileDatabase<PointLatLng, ManualLimits>
    {
        public SpeedLimitsDatabase(string fileName) : base(fileName) { }

        public override void ExportDatabaseFile()
        {
            throw new NotImplementedException();
        }

        public override Dictionary<PointLatLng, ManualLimits> LoadDatabaseFile()
        {
            Dictionary<PointLatLng, ManualLimits> limits = new Dictionary<PointLatLng, ManualLimits>();
            StreamReader sr = new StreamReader(FileName);
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

        protected override PointLatLng GenerateNextKey()
        {
            throw new System.Exception("Вместо этого метода надо вызывать AddElement с параметром key");
        }

    }
}
