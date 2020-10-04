using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonLib.Operations
{
    /// <summary>
    /// приведение двух рядов к одному интервалу
    /// </summary>
    public static class Equalizer
    {
        private class Item
        {
            public DateTime Date { get; set; }
            public string Data { get; set; }
        }

        private class Range : Dictionary<DateTime, Item>
        {
            private List<string> header = new List<string>();
            private bool separateDate;
            public string FileName;

            /// <summary>
            /// Импортирует фал данных
            /// </summary>
            /// <param name="fname">адрес фала csv</param>
            /// <param name="startLine">номер строки (с нуля) с которой начинаются данные</param>
            /// <param name="separateDate">Если истина, то дата и время в 1 и 2 столбце соответственно</param>
            /// <returns></returns>
            public static Range Import(string fname, int startLine, bool separateDate = false)
            {
                using (StreamReader sr = new StreamReader(fname, Encoding.Default, true))
                {

                    //сохраненеи заголовка
                    List<string> header = new List<string>();
                    for (int i = 0; i < startLine; i++)
                        header.Add(sr.ReadLine());


                    //чтение файла
                    Range res = new Range() { separateDate = separateDate, header = header };
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] arr = line.Split(';');
                        DateTime key;
                        string data = "";
                        int startData;

                        //получение даты
                        if (separateDate) //дата и время в разных столбцах
                        {
                            if (arr.Length < 3 || string.IsNullOrWhiteSpace(arr[0])) continue;
                            key = DateTime.Parse(arr[0]) + TimeSpan.Parse(arr[1]);
                            startData = 2; ;
                        }
                        else //дата и время в одном столбце
                        {
                            if (arr.Length < 2 || string.IsNullOrWhiteSpace(arr[0])) continue;
                            key = DateTime.Parse(arr[0]);
                            startData = 1;
                        }

                        //сохранение данных
                        for (int i = startData; i < arr.Length; i++)
                            data += ";" + arr[i];

                        if (!res.ContainsKey(key))
                            res.Add(key, new Item() { Date = key, Data = data });
                    }
                    res.FileName = Path.GetFileNameWithoutExtension(fname);
                    return res;
                }
            }

            /// <summary>
            /// Экспорт ряда в файл
            /// </summary>
            /// <param name="fname">имя файла экспорта</param>
            /// <param name="dates">список дат, которые надо экспортировать</param>
            public void Export(string fname, List<DateTime> dates)
            {
                using (StreamWriter sw = new StreamWriter(fname, false, Encoding.UTF8))
                {
                    foreach (string line in header)
                        sw.WriteLine(line);

                    List<DateTime> keys = this.Keys.ToList();
                    keys.Sort(); //сортировка ряда
                    foreach (DateTime key in keys)
                    {
                        if (!dates.Contains(key)) continue; //если этой даты нет в списке на экспорт, то пропускаем

                        string line;
                        string data = this[key].Data;
                        if (!separateDate) //дата и время в одном столбце
                        {
                            string datetime = key.ToString();
                            line = $"{datetime}{data}";
                        }
                        else //дата и время в разных столбцах
                        {
                            string date = key.ToString("dd.MM.yyyy");
                            string time = key.ToString("hh:mm:ss");
                            line = $"{date};{time}{data}";
                        }
                        sw.WriteLine(line);
                    }
                }
            }
        }



        /// <summary>
        /// привести ряды к одному интервалу (минимальному) и удалить несовпадающие данные. 
        /// </summary>
        /// <param name="fnames"></param>
        /// <param name="resultDir">папка сохранения результатов</param>
        /// <param name="startLine"></param>            
        /// <param name="separateDate">Если истина, то дата и время в 1 и 2 столбце соответственно</param>
        public static void ProcessRanges(List<string> fnames, string resultDir, int startLine, bool separateDate = false)
        {
            //загрузка файлов
            List<Range> ranges = new List<Range>();
            foreach (string file in fnames)
                ranges.Add(Range.Import(file, startLine, separateDate));

            //отсев значений
            if (ranges.Count < 2) return;
            List<DateTime> exports = new List<DateTime>();
            List<DateTime> keys = ranges[0].Keys.ToList();
            foreach (var key in keys)
            {
                bool contains = true;
                foreach (Range rang in ranges)
                    if (!rang.ContainsKey(key))
                    {
                        contains = false;
                        break;
                    }
                if (contains)
                    exports.Add(key);
            }

            //экспорт файлов
            foreach (Range rang in ranges)
                rang.Export(resultDir + "\\" + rang.FileName + "__equalized.csv", exports);
        }

    }
}
