using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Geomodel;

namespace WindEnergy.Lib.Operations.Limits
{
    /// <summary>
    /// статические ограничения скоростей по регионам
    /// </summary>
    public class StaticRegionLimits : ILimitsProvider
    {
        private Dictionary<PointLatLng, ManualLimits> limits;

        /// <summary>
        /// загружает ограничения из заданного файла
        /// </summary>
        /// <param name="sourceFile"></param>
        public StaticRegionLimits(string sourceFile)
        {
            loadDictionary(sourceFile);
        }

        /// <summary>
        /// загрузка словаря огрничений из файла
        /// </summary>
        /// <param name="sourceFile">адрес файла</param>
        /// <returns></returns>
        private void loadDictionary(string sourceFile)
        {
            limits = new Dictionary<PointLatLng, ManualLimits>();
            StreamReader sr = new StreamReader(sourceFile);
            sr.ReadLine();//пропускаем первую строку-заголовок
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] arr = line.Split(';'); //название;широта;долгота;минимальная скорость;максимальная скорость
                if (arr.Length < 5)
                    continue;
                Diapason<double> d = new Diapason<double>(double.Parse(arr[3].Replace('.', Vars.DecimalSeparator)), double.Parse(arr[4].Replace('.', Vars.DecimalSeparator)));
                PointLatLng p = new PointLatLng(double.Parse(arr[1].Replace('.', Vars.DecimalSeparator)), double.Parse(arr[2].Replace('.', Vars.DecimalSeparator)));
                ManualLimits ml = new ManualLimits(new List<Diapason<double>>(), new List<Diapason<double>>() { d }) { Position = p, Name = arr[0] };
                limits.Add(p, ml);
            }
            sr.Close();
        }

        /// <summary>
        /// возвращает истину, если значение допустимо в этой точке
        /// </summary>
        /// <param name="item">данные для провtрки</param>
        /// <param name="coordinates">координаты точки</param>
        /// <returns></returns>
        public bool CheckItem(RawItem item, PointLatLng coordinates)
        {
            PointLatLng pt = getNearest(coordinates);
            if (limits.ContainsKey(pt))
                return limits[pt].CheckItem(item, pt);
            else
                throw new Exception("Коллекция ключей пуста!");

        }

        /// <summary>
        /// найти ближайшую точку в словаре к заданной
        /// </summary>
        /// <param name="coordinates">заданная точка</param>
        /// <returns></returns>
        private PointLatLng getNearest(PointLatLng coordinates)
        {
            double min = double.MaxValue;
            PointLatLng res = PointLatLng.Empty;
            foreach (var p in limits.Keys)
            {
                double f = EarthModel.CalculateDistance(p, coordinates);
                if (f < min)
                {
                    min = f;
                    res = p;
                }
            }
            return res;
        }
    }
}
