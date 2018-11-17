using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Data.Formats
{
    public static class RP5ru
    {
        public static WindDirections GetWindDirectionFromString(string direction)
        {
            switch (direction.ToLower())
            {
                case "штиль, безветрие":
                case "ветер, дующий с севера":
                    return WindDirections.N;
                case "ветер, дующий с северо-северо-востока":
                    return WindDirections.NNE;
                case "ветер, дующий с северо-востока":
                    return WindDirections.NE;
                case "ветер, дующий с востоко-северо-востока":
                    return WindDirections.NEE;
                case "ветер, дующий с востока":
                    return WindDirections.E;
                case "ветер, дующий с востоко-юго-востока":
                    return WindDirections.SEE;
                case "ветер, дующий с юго-востока":
                    return WindDirections.SE;
                case "ветер, дующий с юго-юго-востока":
                    return WindDirections.SSE;
                case "ветер, дующий с юга":
                    return WindDirections.S;
                case "ветер, дующий с юго-юго-запада":
                    return WindDirections.SSW;
                case "ветер, дующий с юго-запада":
                    return WindDirections.SW;
                case "ветер, дующий с западо-юго-запада":
                    return WindDirections.SWW;
                case "ветер, дующий с запада":
                    return WindDirections.W;
                case "ветер, дующий с западо-северо-запада":
                    return WindDirections.NWW;
                case "ветер, дующий с северо-запада":
                    return WindDirections.NW;
                case "ветер, дующий с северо-северо-запада":
                    return WindDirections.NNW;
                default: throw new Exception("Это направление ветра не реализовано");
            }
        }

        public static string GetStringFromWindDirection(WindDirections direction)
        {
            throw new NotImplementedException();
        }
    }
}
