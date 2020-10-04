using CommonLib.Classes.Base;

namespace SolarEnergy.SolarLib.Classes.Structures
{
    public class NPSMeteostationInfo : BaseMeteostationInfo
    {
        public DataItem Data { get; set; }

        public NPSMeteostationInfo()
        {
            Data = new DataItem();
        }
    }
}
