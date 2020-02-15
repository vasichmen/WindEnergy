using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Operations
{
    /// <summary>
    /// Получение средних значений по часам для каждого месяца. 
    /// НА входе файл со столбцами: месяц, день, час, данные1[, данные2[, данные3]...]
    /// Заголовки столбцов обязательны!
    /// </summary>
    public static class Averager
    {
        private class Item
        {
            public DateTime Date { get; set; }
            public List<double> Values { get; set; }

            public Item() { Values = new List<double>(); }
        }

        private class Range : Dictionary<DateTime, Item>
        {
            public string FileName;
            public List<string> header = new List<string>();


            public static Range Import(string fname)
            {
                if (!File.Exists(fname))
                    return null;

                using (StreamReader sr = new StreamReader(fname, Encoding.Default, true))
                {

                    //сохраненеи заголовка
                    List<string> header = new List<string>();
                    string[] heads = sr.ReadLine().Split(';');
                    for (int i = 0; i < heads.Length - 3; i++)
                        header.Add(heads[3 + i]);


                    //чтение файла
                    Range res = new Range() { header = header };
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] arr = line.Split(';');

                        //получение даты
                        bool isdate =
                            int.TryParse(arr[0], out int month) &
                            int.TryParse(arr[1], out int day) &
                            int.TryParse(arr[2], out int hour);

                        if (!isdate) //выход, если не удалось получить дату
                            continue;

                        DateTime key = new DateTime(1, month, day, hour - 1, 0, 0);

                        //сохранение данных
                        List<double> vals = new List<double>();
                        for (int i = 3; i < arr.Length; i++) //первый три столбца это месяц, день, час. остальные -- данные
                        {
                            bool isval = double.TryParse(arr[i].Replace('.', Constants.DecimalSeparator), out double val);
                            vals.Add(isval ? val : double.NaN);
                        }

                        if (!res.ContainsKey(key))
                            res.Add(key, new Item() { Date = key, Values = vals });
                    }
                    res.FileName = Path.GetFileNameWithoutExtension(fname);
                    return res;
                }
            }

            /// <summary>
            /// Экспорт ряда в файл
            /// </summary>
            /// <param name="fname">имя файла экспорта</param>
            public void Export(string fname)
            {
                using (StreamWriter sw = new StreamWriter(fname, false, Encoding.UTF8))
                {
                    foreach (string line in header)
                        sw.WriteLine(line);

                    List<DateTime> keys = this.Keys.ToList();
                    keys.Sort(); //сортировка ряда
                    foreach (DateTime key in keys)
                    {

                        string line;
                        string values = "";
                        for (int i = 0; i < this[key].Values.Count; i++)
                            values += ";" + this[key].Values[i].ToString("0.000000000");
                        line = string.Format("{0};{1};{2}{3}",
                             this[key].Date.Month,
                             this[key].Date.Day,
                             this[key].Date.Hour,
                             values);

                        sw.WriteLine(line);
                    }
                }
            }
        }

        private static void ProcessRange(string fname)
        {
            if (!File.Exists(fname))
                return;

            Range range = Range.Import(fname);

            //обработка
            using (StreamWriter sw = new StreamWriter(Path.GetDirectoryName(fname) + "\\" + Path.GetFileNameWithoutExtension(fname) + "_averaged.csv", false, Encoding.UTF8))
            {
                for (int m = 1; m <= 12; m++)
                {
                    Dictionary<int, List<double>> graph = new Dictionary<int, List<double>>();
                    for (int h = 0; h <= 23; h++)
                    {
                        var items = from item in range.Values
                                    where item.Date.Month == m && item.Date.Hour == h
                                    select item;

                        List<double> averages = new List<double>();
                        for (int i = 0; i < range.header.Count; i++)
                        {
                            double average = items.Average((item) => { return item.Values[i]; });
                            averages.Add(average);
                        }
                        graph.Add(h, averages);
                    }

                    //запись в файл
                    sw.WriteLine(((Months)m).Description());
                    string line = "час;";
                    for (int h = 1; h <= 24; h++)
                        line += h + ";";
                    sw.WriteLine(line);
                    for (int i = 0; i < range.header.Count; i++)
                    {
                        line = range.header[i] + ";";
                        for (int h = 0; h <= 23; h++)
                        {
                            line += graph[h][i].ToString("0.0000000") + ";";
                        }
                        sw.WriteLine(line);
                    }
                    sw.WriteLine();
                }



                sw.Close();
            }

        }

        public static void ProcessRanges(List<string> files)
        {
            foreach (string file in files)
            {
                try
                {
                    ProcessRange(file);
                }
                catch (Exception)
                { continue; }
            }
        }
    }
}
