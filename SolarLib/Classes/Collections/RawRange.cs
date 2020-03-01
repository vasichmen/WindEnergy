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
    /// ряд наблюдений за параметрами
    /// </summary>
    public class RawRange : List<RawItem>
    {
        public PointLatLng Position { get; set; }

        public string FilePath { get; set; }

        public string Name { get; set; }
    }
}
