using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Classes.Structures
{

    /// <summary>
    /// информация о метеостанции с архивом погоды
    /// </summary>
    public class MeteostationInfo
    {
        /// <summary>
        /// wmo_id или metar_id метеостанции для загрузки архива
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// символьный код аэропорта
        /// </summary>
        public string CC_Code { get; set; }

        /// <summary>
        /// является ли родительска точка этой метеостанцией
        /// </summary>
        public bool IsOwner { get { return OwnerDistance == 0; } }

        /// <summary>
        /// расстояние от метеостанции до точки в км
        /// </summary>
        public double OwnerDistance { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// строка ссылки
        /// </summary>
        public string altName { get; set; }

        /// <summary>
        /// дата начала наблюдений
        /// </summary>
        public DateTime MonitoringFrom { get; set; }

        /// <summary>
        /// высота над у. м. в метрах
        /// </summary>
        public double Altitude { get; set; }

        /// <summary>
        /// источник данных архива
        /// </summary>
        public MeteoSourceType MeteoSourceType { get; set; }

        /// <summary>
        /// ссылка на страницу
        /// </summary>
        public string Link
        {
            get
            {
                if (string.IsNullOrEmpty(altName))
                {
                    if (MeteoSourceType == MeteoSourceType.Meteostation)
                        return @"http://rp5.ru/archive.php?wmo_id=" + ID;
                    else
                        return null;
                }
                else
                    return @"https://rp5.ru/" + altName;
            }

            set
            {
                altName = value.Replace(@"https://rp5.ru/", "").Replace(@"http://rp5.ru/", "");
            }
        }

        /// <summary>
        /// координаты точки расположения метеостанции
        /// </summary>
        public PointLatLng Coordinates { get; set; }
    }
}
