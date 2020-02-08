﻿using CommonLib;
using GMap.NET;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindEnergy.WindLib.Classes.Structures;
namespace WindEnergy.WindLib.Data.Providers.DB
{
    /// <summary>
    /// База данных коэффициентов пересчета на высоту по данным АМС
    /// </summary>
    public class AMSMeteostationDatabase: BaseMeteostationDatabase<PointLatLng,AMSMeteostationInfo>
    {
        /// <summary>
        /// создает объект для этого файла, не загружая данные
        /// </summary>
        /// <param name="FileName">адрес файла БД</param>
        public AMSMeteostationDatabase(string FileName) : base(FileName) { }

        /// <summary>
        /// загрузить список ограничений скоростей по точкам
        /// </summary>
        /// <returns></returns>
        public override Dictionary<PointLatLng, AMSMeteostationInfo>  LoadDatabaseFile()
        {
            Dictionary<PointLatLng, AMSMeteostationInfo> items = new Dictionary<PointLatLng, AMSMeteostationInfo>();
            StreamReader sr = new StreamReader(FileName);
            sr.ReadLine();//пропускаем первую строку-заголовок
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                //MSID; Название; Широта; Долгота; V10 по месяцам, январь; февраль; март; апрель; май; июнь; июль; август; сентябрь; октябрь; ноябрь; декабрь; 
                //m по месяцам,январь; февраль; март; апрель; май; июнь; июль; август; сентябрь; октябрь; ноябрь; декабрь; Среднее m;
                string[] arr = line.Split(';');
                if (arr.Length < 29)
                    continue;

                int id = int.Parse(arr[0]);
                string name = arr[1];
                double lat = double.Parse(arr[2].Replace(',', Constants.DecimalSeparator));
                double lon = double.Parse(arr[3].Replace(',', Constants.DecimalSeparator));
                PointLatLng p = new PointLatLng(lat, lon);
                double averM = double.Parse(arr[28].Replace(',', Constants.DecimalSeparator));

                //скорости на высоте 10м
                Dictionary<Months, double> speeds = new Dictionary<Months, double>();
                for (int i = 1; i <= 12; i++)
                {
                    double spd = double.Parse(arr[3 + i].Replace(',', Constants.DecimalSeparator));
                    Months month = (Months)i;
                    speeds.Add(month, spd);
                }

                //коэффициенты по месяцам
                Dictionary<Months, double> Ms = new Dictionary<Months, double>();
                for (int i = 1; i <= 12; i++)
                {
                    double m = double.Parse(arr[15 + i].Replace(',', Constants.DecimalSeparator));
                    Months month = (Months)i;
                    Ms.Add(month, m);
                }

                AMSMeteostationInfo data = new AMSMeteostationInfo()
                {
                    ID = id.ToString(),
                    Name = name,
                    Position = p,
                    AverageM = averM,
                    m = Ms,
                    V10 = speeds
                };

                if (!items.ContainsKey(p))
                    items.Add(p, data);
            }
            sr.Close();
            return items;
        }

        /// <summary>
        /// найти ближайшую МС для заданных координат и в заданном радиусе от точки 
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="maxRadius">радиус поиска в метрах</param>
        /// <returns></returns>
        public new AMSMeteostationInfo GetNearestMS(PointLatLng coordinates, double maxRadius = double.MaxValue)
        {
            AMSMeteostationInfo res = base.GetNearestMS(coordinates, maxRadius) as AMSMeteostationInfo;
            return res;
        }

        /// <summary>
        /// найти все метеостанции из списка mts, которые находятся в радиусе radius от заданной точки coordinates
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="radius"></param>
        /// <param name="addOwn">Если истина, то если на coordinates есть МС, то она тоже будет добавлена</param>
        /// <returns></returns>
        public new List<AMSMeteostationInfo> GetNearestMS(PointLatLng coordinates, double radius, bool addOwn = false)
        {
            List<AMSMeteostationInfo> res = base.GetNearestMS(coordinates, radius, addOwn).Cast<AMSMeteostationInfo>().ToList();
            return res;
        }

    }
}
