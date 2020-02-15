using CommonLib;
using CommonLib.Geomodel;
using CommonLibLib.Data.Providers.InternetServices;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data.Providers.InternetServices;
using WindLib;

namespace WindEnergy.WindLib.Data.Providers.DB
{
    /// <summary>
    /// База данных метеостанций
    /// </summary>
    public class RP5MeteostationDatabase : BaseMeteostationDatabase<string, RP5MeteostationInfo>
    {
        /// <summary>
        /// создает объект для этого файла, не загружая данные
        /// </summary>
        /// <param name="FileName">адрес файла БД</param>
        public RP5MeteostationDatabase(string FileName) : base(FileName) { }

        /// <summary>
        /// количество аэропортов в БД
        /// </summary>
        public int AirportCount
        {
            get
            {
                if (_airportCount == -1)
                {
                    _airportCount = 0;
                    foreach (var m in List)
                        if (m.MeteoSourceType == MeteoSourceType.Airport)
                            _airportCount++;
                }
                return _airportCount;
            }
        }
        private int _airportCount = -1;

        /// <summary>
        /// количество метеостанций в БД
        /// </summary>
        public int MeteostationsCount
        {
            get
            {
                if (_meteostationsCount == -1)
                {
                    _meteostationsCount = 0;
                    foreach (var m in List)
                        if (m.MeteoSourceType == MeteoSourceType.Meteostation)
                            _meteostationsCount++;
                }
                return _meteostationsCount;
            }
        }
        private int _meteostationsCount = -1;

        /// <summary>
        /// общее количество записей
        /// </summary>
        public int TotalCount
        {
            get
            {
                return List.Count;
            }
        }


        /// <summary>
        /// загрузка списка метеостанций и координат
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, RP5MeteostationInfo> LoadDatabaseFile()
        {
            StreamReader sr = new StreamReader(FileName);
            sr.ReadLine(); //пропуск заголовка

            Dictionary<string, RP5MeteostationInfo> res = new Dictionary<string, RP5MeteostationInfo>();
            while (!sr.EndOfStream)
            {
                string[] arr = sr.ReadLine().Split(';');
                if (arr.Length < 3)
                    continue;
                string wmo = arr[0];
                string cc_code = arr[1];
                string name = arr[2];
                string address = arr[3];
                double lat = double.Parse(arr[4].Replace('.', Constants.DecimalSeparator).Replace(',', Constants.DecimalSeparator));
                double lon = double.Parse(arr[5].Replace('.', Constants.DecimalSeparator).Replace(',', Constants.DecimalSeparator));

                double alt = double.NaN;
                DateTime mfrom = DateTime.MinValue;
                if (arr.Length > 6)
                {
                    alt = double.Parse(arr[6].Replace('.', Constants.DecimalSeparator).Replace(',', Constants.DecimalSeparator));
                    mfrom = DateTime.Parse(arr[7]);
                }
                PointLatLng position = new PointLatLng(lat, lon);
                res.Add(wmo, new RP5MeteostationInfo()
                {
                    ID = wmo,
                    Position = position,
                    Name = name,
                    Altitude = alt,
                    MonitoringFrom = mfrom,
                    CC_Code = cc_code,
                    Address = address,
                    MeteoSourceType = string.IsNullOrWhiteSpace(cc_code) ? MeteoSourceType.Meteostation : MeteoSourceType.Airport
                });
            }

            sr.Close();
            return res;
        }

        /// <summary>
        /// экспортирует список метеостанций в указанный файл 
        /// </summary>
        /// <param name="list">список метеостанций</param>
        /// <param name="filename">адрес файла, куда сохраняется список</param>
        public static void ExportMeteostationList(List<RP5MeteostationInfo> list, string filename)
        {
            //удаление повторов
            List<RP5MeteostationInfo> list_unic = new List<RP5MeteostationInfo>();
            foreach (var m in list)
            {
                //проверка существования ID в массиве
                bool contains = false;
                foreach (var cc in list_unic)
                    if (cc.ID == m.ID)
                    {
                        contains = true;
                        break;
                    }
                //если не существует, то  добавляем
                if (!contains)
                    list_unic.Add(m);
            }
            //запись в файл
            StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
            sw.WriteLine("ВМО ID;CC_Code;Название архива;Адрес;Широта;Долгота;Высота над у. м., м;Дата начала наблюдений");
            foreach (var ms in list_unic)
                sw.WriteLine(
                    ms.ID + ";" +
                    ms.CC_Code + ";" +
                    ms.Name + ";" +
                    ms.Address + ";" +
                    ms.Position.Lat.ToString().Replace(Constants.DecimalSeparator, ',') + ";" +
                    ms.Position.Lng.ToString().Replace(Constants.DecimalSeparator, ',') + ";" +
                    ms.Altitude.ToString("0.00").Replace(Constants.DecimalSeparator, ',') + ";" +
                    ms.MonitoringFrom.ToString());
            sw.Close();
        }

        /// <summary>
        /// ищет в БД метеостанции по запросу
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<RP5MeteostationInfo> Search(string query)
        {
            List<RP5MeteostationInfo> res = new List<RP5MeteostationInfo>();
            var sel = from item in List
                      where item.Name.ToLower().Contains(query.ToLower()) || item.Address.ToLower().Contains(query.ToLower())
                      select item;
            return sel.ToList();
        }

        /// <summary>
        /// найти ближайшую МС для заданных координат и в заданном радиусе от точки 
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="maxRadius">радиус поиска в метрах</param>
        /// <returns></returns>
        public new RP5MeteostationInfo GetNearestMS(PointLatLng coordinates, double maxRadius = double.MaxValue)
        {
            RP5MeteostationInfo res = base.GetNearestMS(coordinates, maxRadius) as RP5MeteostationInfo;
            return res;
        }

        /// <summary>
        /// найти все метеостанции из списка mts, которые находятся в радиусе radius от заданной точки coordinates
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="radius"></param>
        /// <param name="addOwn">Если истина, то если на coordinates есть МС, то она тоже будет добавлена</param>
        /// <returns></returns>
        public new List<RP5MeteostationInfo> GetNearestMS(PointLatLng coordinates,  double radius, bool addOwn = false)
        {
            List<RP5MeteostationInfo> res = base.GetNearestMS(coordinates, radius, addOwn).Cast<RP5MeteostationInfo>().ToList();
            return res;
        }

        /// <summary>
        /// найти МС по wmo_id
        /// </summary>
        /// <param name="id">wmo_id</param>
        /// <returns></returns>
        internal RP5MeteostationInfo GetByID(string id)
        {
            var res = from t in List
                      where t.ID == id
                      select t;
            if (res.Count() > 0)
                return res.First();
            else
                return null;
        }

        /// <summary>
        /// найти аэропорт по METAR
        /// </summary>
        /// <param name="CC_code">METAR</param>
        /// <returns></returns>
        internal RP5MeteostationInfo GetByCC_code(string CC_code)
        {
            var res = from t in List
                      where t.CC_Code == CC_code
                      select t;
            if (res.Count() > 0)
                return res.First();
            else
                return null;
        }

        /// <summary>
        /// попытка добавить метеостанцию с БД. Если такая уже есть, то возвращает false. При успешном добавлении перезаписывает файл БД и возвращает true
        /// </summary>
        /// <param name="info">информация о МС для добавления в БД</param>
        internal bool TryAddMeteostation(RP5MeteostationInfo info)
        {
            if (this.Contains(info))
                return false;

            if (double.IsNaN(info.Altitude))
                info.Altitude = Vars.ETOPOdatabase.GetElevation(info.Position);
            if (string.IsNullOrWhiteSpace(info.Address))
                info.Address = new Arcgis(Vars.Options.CacheFolder + "\\arcgis").GetAddress(info.Position);

            _dictionary.Add(info.ID,info);
            ExportMeteostationList(this.List, Vars.Options.StaticMeteostationCoordinatesSourceFile);
            return true;
        }

        /// <summary>
        /// проверка существования метеостанции в БД
        /// </summary>
        /// <param name="meteostation"></param>
        /// <returns></returns>
        public bool Contains(RP5MeteostationInfo meteostation)
        {
            foreach (var v in this.List)
                if (v.ID == meteostation.ID)
                    return true;
            return false;
        }
    }
}
