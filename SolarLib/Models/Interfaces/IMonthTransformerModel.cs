using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Models.Interfaces
{
    public interface IMonthTransformerModel
    {
        DataRange GenerateRange(DataItem dataItem);
    }
}
