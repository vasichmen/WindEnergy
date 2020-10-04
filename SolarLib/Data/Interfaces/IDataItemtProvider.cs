using GMap.NET;
using SolarEnergy.SolarLib.Classes.Structures;

namespace SolarEnergy.SolarLib.Data.Interfaces
{
    internal interface IDataItemtProvider
    {
        DataItem GetDataItem(PointLatLng point);
    }
}
