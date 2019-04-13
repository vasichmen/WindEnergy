using GMap.NET;
using System;

namespace WindEnergy.Lib.Data.Providers.ETOPO
{
    interface IDatabase
    {
        double this[PointLatLng coordinate] { get; }
        double this[int i, int j] { get; }
        double CellSize { get; }
        int Columns { get; }
        string DataFile { get; }
        string Folder { get; }
        string HeaderFile { get; }
        int Rows { get; }
        ETOPODBType Type { get; }
        void ExportToSQL(string FileName, Action<string> callback=null);
    }
}