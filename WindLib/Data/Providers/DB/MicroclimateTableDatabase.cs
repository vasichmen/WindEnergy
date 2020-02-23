using CommonLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Data.Providers.DB
{
    /// <summary>
    /// таблица мезоклиматических коэффициентов рельефа
    /// </summary>
    public class MicroclimateTableDatabase : BaseFileDatabase<string, MicroclimateItemInfo>
    {
        /// <summary>
        /// создает объект для этого файла, не загружая данные
        /// </summary>
        /// <param name="FileName">адрес файла БД</param>
        public MicroclimateTableDatabase(string FileName) : base(FileName) { }

        /// <summary>
        /// загрузка файла БД
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, MicroclimateItemInfo> LoadDatabaseFile()
        {
            Dictionary<string, MicroclimateItemInfo> items = new Dictionary<string, MicroclimateItemInfo>();
            StreamReader sr = new StreamReader(FileName,Encoding.Default, true);
            sr.ReadLine();//пропускаем первую строку-заголовок
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                //Описание рельефа;Неустойчивая стратификация, 3-5м/с;Неустойчивая стратификация, 6-20м/с;Устойчивая стратификация, 3-5м/с;Устойчивая стратификация, 6-20м/с
                string[] arr = line.Split(';');
                if (arr.Length < 5)
                    continue;

                MicroclimateItemInfo data = new MicroclimateItemInfo()
                {
                    Name = arr[0],
                    Unstable_3_5 = double.Parse(arr[1].Replace(',', Constants.DecimalSeparator)),
                    Unstable_6_20 = double.Parse(arr[2].Replace(',', Constants.DecimalSeparator)),
                    Stable_3_5 = double.Parse(arr[3].Replace(',', Constants.DecimalSeparator)),
                    Stable_6_20 = double.Parse(arr[4].Replace(',', Constants.DecimalSeparator)),
                };

                if (!items.ContainsKey(arr[0]))
                    items.Add(arr[0], data);
            }
            sr.Close();
            return items;
        }
    }
}
