using CommonLib;
using CommonLib.Classes;
using GMap.NET;
using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using SolarLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Data.Providers.FileSystem
{
    /// <summary>
    /// работа с файлами формата csv
    /// </summary>
    public class CSVFile : FileProvider
    {
        /// <summary>
        /// загрузка файла CSV поддерживаемых форматов
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public override DataRange LoadDataRange(string fileName)
        {
            StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true);

            //определение формата файла csv
            string title = null;
            title = sr.ReadLine();

            DataRange res = new DataRange();
            while(!sr.EndOfStream)
            {
                string[] arr = sr.ReadLine().Split(';');
                DateTime dt = DateTime.Parse(arr[0]);
                double allsk = double.Parse(arr[1].Replace('.', Constants.DecimalSeparator));
                double clrsk = double.Parse(arr[2].Replace('.', Constants.DecimalSeparator));
                res.Add(new RawItem(dt, allsk, clrsk));
            }
            res.Name = Path.GetFileNameWithoutExtension(fileName);
            res.FilePath = fileName;
            sr.Close();
            return res;
        }

        /// <summary>
        /// сохранение файла в формате CSV
        /// </summary>
        /// <param name="rang"></param>
        /// <param name="filename"></param>
        internal override void SaveDataRange(DataRange rang, string filename)
        {
            StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
            string caption = "Местное время;Эполн, кВт*ч/м2;Эпр, кВт*ч/м2;Эдифф, кВт*ч/м2";
            sw.WriteLine(caption);

            foreach (var item in rang)
                sw.WriteLine("{0};{1};{2};{3}",
                    item.Date,
                    item.AllSkyInsolation,
                    item.ClearSkyInsolation,
                    item.DiffuseSkyInsolation);

            sw.Close();
        }
    }
}
