using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;

namespace SolarEnergy.SolarLib.Models.Interfaces
{
    public interface IMonthTransformerModel
    {
        DataRange GenerateRange(DataItem dataItem);
    }
}
