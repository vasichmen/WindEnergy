using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Classes
{
    [Serializable]
    public class WindEnergyException : ApplicationException
    {
        public WindEnergyException(string message) : base(message)
        {
        }
    }
}
