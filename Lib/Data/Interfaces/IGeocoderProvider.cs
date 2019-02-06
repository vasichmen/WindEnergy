using System;
using GMap.NET;

namespace WindEnergy.Lib.Data.Interfaces
{
    public interface IGeocoderProvider
    {
        string GetAddress(PointLatLng coordinate);
        PointLatLng GetCoordinate(string address);
        TimeZoneInfo GetTimeZone(PointLatLng coordinate);
    }
}