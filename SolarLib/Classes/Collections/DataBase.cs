using GMap.NET;
using SolarEnergy.SolarLib.Classes.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Classes.Collections
{
    /// <summary>
    /// базовый класс для работы с табличными данными, привязанными к местности
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class DataBase<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public PointLatLng Position { get; set; }
        public string Name { get; set; }

        public string FilePath { get; set; }

        public NPSMeteostationInfo Meteostation { get; set; }

        public void Set(TKey key, TValue value)
        {
            this[key] = value;
        }

    }
}
