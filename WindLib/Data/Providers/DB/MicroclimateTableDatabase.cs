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
    public class MicroclimateTableDatabase : BaseFileDatabase<string, MicroclimateItemInfo>
    {
        /// <summary>
        /// создает объект для этого файла, не загружая данные
        /// </summary>
        /// <param name="FileName">адрес файла БД</param>
        public MicroclimateTableDatabase(string FileName) : base(FileName) { }

        public override void ExportDatabaseFile()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// загрузка файла БД
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, MicroclimateItemInfo> LoadDatabaseFile()
        {
            Dictionary<string, MicroclimateItemInfo> items = new Dictionary<string, MicroclimateItemInfo>();
            StreamReader sr = new StreamReader(FileName, Encoding.UTF8, true);
            sr.ReadLine();//пропускаем первую строку-заголовок
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                //Описание рельефа;Неустойчивая стратификация, 3-5м/с;Неустойчивая стратификация, 6-20м/с;Устойчивая стратификация, 3-5м/с;Устойчивая стратификация, 6-20м/с
                string[] arr = line.Split(';');
                if (arr.Length < 5)
                    continue;

                Diapason<double> uns_3_5 = parseDiapason(arr[1]);
                Diapason<double> uns_6_20 = parseDiapason(arr[2]);
                Diapason<double> s_3_5 = parseDiapason(arr[3]);
                Diapason<double> s_6_20 = parseDiapason(arr[4]);

                MicroclimateItemInfo data = new MicroclimateItemInfo()
                {
                    Name = arr[0],
                    Unstable_3_5 = uns_3_5,
                    Unstable_6_20 = uns_6_20,
                    Stable_3_5 = s_3_5,
                    Stable_6_20 = s_6_20,
                };

                if (!items.ContainsKey(arr[0]))
                    items.Add(arr[0], data);
            }
            sr.Close();
            return items;
        }

        /// <summary>
        /// парсинг диапазона значений dobule
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private Diapason<double> parseDiapason(string v)
        {
            string[] arr = v.Split('-');
            Diapason<double> res = new Diapason<double>(double.Parse(arr[0].Replace('.', Constants.DecimalSeparator)), double.Parse(arr[1].Replace('.', Constants.DecimalSeparator)));
            return res;
        }

        protected override string GenerateNextKey()
        {
            throw new System.Exception("Вместо этого метода надо вызывать AddElement с параметром key");
        }
    }
}
