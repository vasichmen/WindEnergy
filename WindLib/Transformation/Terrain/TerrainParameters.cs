﻿using GMap.NET;
using System.Collections.Generic;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Transformation.Terrain
{
    /// <summary>
    /// настройки пересчета скорости ветра в точку
    /// </summary>
    public class TerrainParameters
    {
        public Dictionary<WindDirections8, double> MSClasses { get; set; }
        public Dictionary<WindDirections8, double> PointClasses { get; set; }
        public TerrainType TerrainType { get; set; }
        public PointLatLng PointCoordinates { get; set; }
        public FlugerMeteostationInfo FlugerMeteostation { get; set; }
        public MesoclimateItemInfo MesoclimateCoefficient { get; set; }
        public MicroclimateItemInfo MicroclimateCoefficient { get; set; }
        public AtmosphereStratification AtmosphereStratification { get; set; }

    }
}
