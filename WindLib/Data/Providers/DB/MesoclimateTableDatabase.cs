using CommonLib.Classes.Base;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Data.Providers.DB
{
    /// <summary>
    /// таблица мезоклиматических коэффициентов рельефа
    /// </summary>
    public class MesoclimateTableDatabase : BaseFileDatabase<string, MesoclimateItemInfo>
    {
        /// <summary>
        /// создает объект для этого файла, не загружая данные
        /// </summary>
        /// <param name="FileName">адрес файла БД</param>
        public MesoclimateTableDatabase(string FileName) : base(FileName) { }

        public override void ExportDatabaseFile()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// загрузка файла БД
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, MesoclimateItemInfo> LoadDatabaseFile()
        {
            Dictionary<string, MesoclimateItemInfo> items = new Dictionary<string, MesoclimateItemInfo>();
            StreamReader sr = new StreamReader(FileName, Encoding.UTF8, true);
            sr.ReadLine();//пропускаем первую строку-заголовок
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                //Тип местоположения; Наименьший коэффициент; Наибольший коэффициент
                string[] arr = line.Split(';');
                if (arr.Length < 3)
                    continue;

                Diapason<double> val = new Diapason<double>()
                {
                    From = double.Parse(arr[1].Replace(',', Constants.DecimalSeparator)),
                    To = double.Parse(arr[2].Replace(',', Constants.DecimalSeparator))

                };

                MesoclimateItemInfo data = new MesoclimateItemInfo()
                {
                    Name = arr[0],
                    Value = val
                };

                if (!items.ContainsKey(arr[0]))
                    items.Add(arr[0], data);
            }
            sr.Close();
            return items;
        }
    }
}
