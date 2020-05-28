using CommonLib;
using CommonLib.Classes.Base;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Data.Providers.DB
{
    /// <summary>
    /// База данных Классы открытости МС
    /// </summary>
    public class FlugerMeteostationDatabase : BaseMeteostationDatabase<PointLatLng, FlugerMeteostationInfo>
    {
        /// <summary>
        /// создает объект для этого файла, не загружая данные
        /// </summary>
        /// <param name="FileName">адрес файла БД</param>
        public FlugerMeteostationDatabase(string FileName) : base(FileName) { }


        /// <summary>
        /// загрузка файла БД
        /// </summary>
        /// <returns></returns>
        public override Dictionary<PointLatLng, FlugerMeteostationInfo> LoadDatabaseFile()
        {
            Dictionary<PointLatLng, FlugerMeteostationInfo> items = new Dictionary<PointLatLng, FlugerMeteostationInfo>();
            StreamReader sr = new StreamReader(FileName);
            sr.ReadLine();//пропускаем первую строку-заголовок
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                
                string[] arr = line.Split(';');
                if (arr.Length < 12)
                    continue;

                int id = int.Parse(arr[0]);
                string name = arr[1];
                double lat = double.Parse(arr[2].Replace('.', Constants.DecimalSeparator));
                double lon = double.Parse(arr[3].Replace('.', Constants.DecimalSeparator));
                PointLatLng p = new PointLatLng(lat, lon);

                //классы открытости
                Dictionary<WindDirections8, double> km = new Dictionary<WindDirections8, double>();
                for (int i = 0; i <= 7; i++)
                {
                    double k = double.Parse(arr[4 + i].Replace('.', Constants.DecimalSeparator));
                    WindDirections8 dir = (WindDirections8)i;
                    km.Add(dir, k);
                }

                FlugerMeteostationInfo data = new FlugerMeteostationInfo()
                {
                    ID = id.ToString(),
                    Name = name,
                    Position = p,
                    KM = km
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
        public new FlugerMeteostationInfo GetNearestMS(PointLatLng coordinates, double maxRadius = double.MaxValue)
        {
            FlugerMeteostationInfo res = base.GetNearestMS(coordinates, maxRadius) as FlugerMeteostationInfo;
            return res;
        }

        /// <summary>
        /// найти все метеостанции из списка mts, которые находятся в радиусе radius от заданной точки coordinates
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="radius"></param>
        /// <param name="addOwn">Если истина, то если на coordinates есть МС, то она тоже будет добавлена</param>
        /// <returns></returns>
        public new List<FlugerMeteostationInfo> GetNearestMS(PointLatLng coordinates, double radius, bool addOwn = false)
        {
            List<FlugerMeteostationInfo> res = base.GetNearestMS(coordinates, radius, addOwn).Cast<FlugerMeteostationInfo>().ToList();
            return res;
        }
    }
}
