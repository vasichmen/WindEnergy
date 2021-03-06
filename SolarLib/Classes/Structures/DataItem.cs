﻿using SolarEnergy.SolarLib.Classes.Collections;

namespace SolarEnergy.SolarLib.Classes.Structures
{
    public class DataItem
    {
        public Dataset DatasetAllsky { get; set; }
        public Dataset DatasetClearSky { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }

        public DataItem()
        {
            DatasetAllsky = new Dataset();
            DatasetClearSky = new Dataset();
        }

        public DataItem(Dataset allsky, Dataset clearsky)
        {
            DatasetAllsky = allsky;
            DatasetClearSky = clearsky;
        }
    }
}
