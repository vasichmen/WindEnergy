using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Structures;

namespace WindEnergy.Lib.Data.Providers.DB
{
    /// <summary>
    /// База данных Флюгер
    /// </summary>
    public class FlugerMeteostationDatabase : BaseFileDatabase<PointLatLng, FlugerMeteostationInfo>
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
                //MSID; Название; Широта; Долгота; V10 по месяцам, январь; февраль; март; апрель; май; июнь; июль; август; сентябрь; октябрь; ноябрь; декабрь; 
                //m по месяцам,январь; февраль; март; апрель; май; июнь; июль; август; сентябрь; октябрь; ноябрь; декабрь; Среднее m;
                string[] arr = line.Split(';');
                if (arr.Length < 12)
                    continue;

                int id = int.Parse(arr[0]);
                string name = arr[1];
                double lat = double.Parse(arr[2].Replace(',', Vars.DecimalSeparator));
                double lon = double.Parse(arr[3].Replace(',', Vars.DecimalSeparator));
                PointLatLng p = new PointLatLng(lat, lon);

                //скорости на высоте 10м
                Dictionary<WindDirections, double> km = new Dictionary<WindDirections, double>();
                for (int i = 1; i <= 8; i++)
                {
                    double k = double.Parse(arr[3+i].Replace(',', Vars.DecimalSeparator));
                    WindDirections dir;
                    switch (i)
                    {
                        case 1:
                            dir = WindDirections.N;
                            break;
                        case 2:
                            dir = WindDirections.NE;
                            break;
                        case 3:
                            dir = WindDirections.E;
                            break;
                        case 4:
                            dir = WindDirections.SE;
                            break;
                        case 5:
                            dir = WindDirections.S;
                            break;
                        case 6:
                            dir = WindDirections.SW;
                            break;
                        case 7:
                            dir = WindDirections.W;
                            break;
                        case 8:
                            dir = WindDirections.NW;
                            break;
                        default: throw new Exception("Что-то пошло не так");
                    }
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
    }
}
