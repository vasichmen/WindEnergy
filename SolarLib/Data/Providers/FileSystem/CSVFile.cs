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
        public override Dataset LoadDataset(string fileName)
        {
            StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true);

            //определение формата файла csv
            string title = null, title2 = null, title3 = null;
            title = sr.ReadLine();
            if (!sr.EndOfStream)
                title2 = sr.ReadLine();
            if (!sr.EndOfStream)
                title3 = sr.ReadLine();
            sr.Close();

            if (title == null)
                throw new WindEnergyException("Файл пуст!");

            string regex = @"^\d+[\.\,].\d*\s+\d+[\.\,].\d*$";

            Dataset res = new Dataset();
            //TODO: Загрузка из csv

            return res;
        }

        /// <summary>
        /// загрузка ряда из формата WindEnergy
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private Dataset loadCSVFile(string filename)
        {
            StreamReader sr = new StreamReader(filename, Encoding.UTF8);
            string title = sr.ReadLine();
            string coordinates = sr.ReadLine();
            string name = sr.ReadLine();
            sr.ReadLine();//пропуск заголовка таблицы
            string data = sr.ReadToEnd();
            sr.Close();
            Dataset res = new Dataset() { Name = name };

            //чтение координат файла
            string regex = @"^\d+[\.\,].\d*\s+\d+[\.\,].\d*$";
            bool isMatch = new Regex(regex).IsMatch(coordinates);
            PointLatLng coord;
            if (isMatch)
            {
                string[] s = coordinates.Split(' ');
                double lat = double.Parse(s[0].Trim().Replace('.', Constants.DecimalSeparator));
                double lon = double.Parse(s[1].Trim().Replace('.', Constants.DecimalSeparator));
                coord = new PointLatLng(lat, lon);
            }
            else
                coord = PointLatLng.Empty;
            res.Position = coord;

            //TODO: Загрузка из csv

            //поиск информации о МС
            NPSMeteostationInfo meteostation = null;
            int start = title.IndexOf("ID=") + "ID=".Length;
            string id_s = title.Substring(start);
            meteostation = Vars.NPSMeteostationDatabase.GetByID(id_s);
            res.Meteostation = meteostation;


            return res;
        }

        /// <summary>
        /// сохранение файла в формате CSV
        /// </summary>
        /// <param name="rang"></param>
        /// <param name="filename"></param>
        internal override void SaveDataset(Dataset rang, string filename)
        {
            StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
            string coordinates = rang.Position.Lat.ToString("0.000000") + " " + rang.Position.Lng.ToString("0.000000");
            string caption = "Местное время;T;U;DD;ff;P0";
            string title;
            if (rang.Meteostation == null)
                title = "ID=undefined";
            else
                title = $"ID={rang.Meteostation.ID}";
            sw.WriteLine(title);
            sw.WriteLine(coordinates);
            sw.WriteLine(rang.Name);
            sw.WriteLine(caption);
            
            //TODO: сохранение csv

            sw.Close();
        }
    }
}
