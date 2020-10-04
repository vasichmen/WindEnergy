using CommonLib.Classes;
using System;
using System.IO;
using System.Linq;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data.Interfaces;
using WindEnergy.WindLib.Data.Providers.FileSystem;

namespace WindEnergy.WindLib.Data.Providers.DB
{
    /// <summary>
    /// Класс взаимодействия с локальной БД Расписание погоды
    /// </summary>
    public class RP5Database : IRangeProvider
    {
        /// <summary>
        /// префикс файла наблюдений на МС
        /// </summary>
        public const string PREFIX = "file_";

        /// <summary>
        /// адрес папки с БД 
        /// </summary>
        private readonly string folder;

        public RP5Database(string folder)
        {
            this.folder = folder;
        }

        /// <summary>
        /// проверка коректности структуры папки РП5
        /// </summary>
        /// <returns></returns>
        public bool CheckDatabaseFolder()
        {
            return Directory.Exists(folder);
        }

        /// <summary>
        /// загрузка ряда из локальной БД
        /// </summary>
        /// <param name="fromDate">начальная дата</param>
        /// <param name="toDate">конечная дата</param>
        /// <param name="point_info">информация о МС</param>
        /// <param name="onPercentChange">не используется</param>
        /// <param name="checkStop">не используется</param>
        /// <returns></returns>
        public RawRange GetRange(DateTime fromDate, DateTime toDate, RP5MeteostationInfo point_info, Action<double> onPercentChange = null, Func<bool> checkStop = null)
        {
            //проверка параметров
            if (toDate < fromDate)
                throw new WindEnergyException("Даты указаны неверно");
            if (string.IsNullOrWhiteSpace(point_info.ID))
                throw new ArgumentException("Поле ID не заполнено", nameof(point_info));
            if (!Directory.Exists(folder))
                throw new IOException("Папка БД Расписание Погоды не существует:\r\n" + folder);
            string filename = folder + "\\" + PREFIX + point_info.ID + ".xlsx";
            if (!File.Exists(filename))
                throw new FileNotFoundException("Файл не найден: " + filename);

            //загрузка файла
            RawRange range = new ExcelFile().LoadRange(filename);
            if (range.First().Date > fromDate)
                throw new WindEnergyException($"Начальной даты нет в локальной БД (доступны данные с {range.First().Date})");
            if (toDate > range.Last().Date)
                throw new WindEnergyException($"Конечной даты нет в локальной БД (доступны данные до {range.Last().Date})");
            var res = range.Where(new Func<RawItem, bool>((item) => { return (item.Date >= fromDate) && (item.Date <= toDate); }));

            RawRange result = new RawRange(res);
            if (result.Meteostation == null)
                result.Meteostation = point_info;
            return result;
        }
    }
}
