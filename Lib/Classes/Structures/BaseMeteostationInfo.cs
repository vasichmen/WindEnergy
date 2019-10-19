using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Classes.Structures
{
   public abstract class BaseMeteostationInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Название МС
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Координаты
        /// </summary>
        public PointLatLng Position { get; set; }


    }
}
