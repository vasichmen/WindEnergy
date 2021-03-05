using CommonLib.Classes.Base;
using GMap.NET;
using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using SolarEnergy.SolarLib.Data.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SolarEnergy.SolarLib.Data.Providers.DB
{
    public class NPSMeteostationDatabase : BaseMeteostationDatabase<PointLatLng, NPSMeteostationInfo>, IDataItemtProvider
    {
        /// <summary>
        /// создает объект для этого файла, не загружая данные
        /// </summary>
        /// <param name="FileName">адрес файла БД</param>
        public NPSMeteostationDatabase(string FileName) : base(FileName) { }

        public override Dictionary<PointLatLng, NPSMeteostationInfo> LoadDatabaseFile()
        {
            Dictionary<PointLatLng, NPSMeteostationInfo> res = new Dictionary<PointLatLng, NPSMeteostationInfo>();


            using (StreamReader sr = new StreamReader(FileName, Encoding.UTF8, true))
            {
                string[] arr = sr.ReadToEnd().Split('\n');

                for (int i = 0; i < arr.Length - 1;)
                {
                    i++;//пропуск заголовка
                    string[] line = arr[i].Split(';');
                    string name = $"{line[2]} ({line[12]})";
                    string id = line[0];
                    PointLatLng pos = new PointLatLng(double.Parse(line[22].Replace(',', Constants.DecimalSeparator)), double.Parse(line[24].Replace(',', Constants.DecimalSeparator)));
                    Dataset dsAllsk = loadTable(arr, i + 3, 1);
                    Dataset dsClrsk = loadTable(arr, i + 3, 28);
                    NPSMeteostationInfo item = new NPSMeteostationInfo()
                    {
                        Data = new DataItem() { DatasetAllsky = dsAllsk, DatasetClearSky = dsClrsk },
                        ID = id,
                        Name = name,
                        Position = pos
                    };
                    if (!res.ContainsKey(pos))
                        res.Add(pos, item);
                    i += 16;
                }

                sr.Close();
            }

            return res;
        }

        /// <summary>
        /// загрузка таблицы, начиная с заданной ячейки
        /// </summary>
        /// <param name="array"></param>
        /// <param name="startLine"></param>
        /// <param name="startCol"></param>
        /// <returns></returns>
        private Dataset loadTable(string[] array, int startLine, int startCol)
        {
            Dataset res = new Dataset();
            for (int i = startLine; i < startLine + 12; i++)
            {
                DataHours<double> dh = new DataHours<double>();
                Months month = (Months)(i - startLine + 1);
                string[] line = array[i].Split(';');

                for (int j = startCol; j < startCol + 24; j++)
                {
                    int hour = j - startCol;
                    double val = double.Parse(line[j].Replace(',', Constants.DecimalSeparator));
                    dh[hour] = val;
                }
                res[month] = dh;
            }
            return res;
        }

        /// <summary>
        /// найти МС по wmo_id
        /// </summary>
        /// <param name="id">wmo_id</param>
        /// <returns></returns>
        internal NPSMeteostationInfo GetByID(string id)
        {
            var res = from t in List
                      where t.ID == id
                      select t;
            if (res.Count() > 0)
                return res.First();
            else
                return null;
        }

        public DataItem GetDataItem(PointLatLng point)
        {
            PointLatLng pt = this.GetNearestMS(point).Position;
            return this[pt].Data;
        }

        public override void ExportDatabaseFile()
        {
            throw new System.NotImplementedException();
        }
    }
}
