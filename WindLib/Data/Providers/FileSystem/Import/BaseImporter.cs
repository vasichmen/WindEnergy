﻿using CommonLib;
using CommonLib.Classes;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data.Providers.InternetServices;
using WindLib;

namespace WindEnergy.WindLib.Data.Providers.FileSystem.Import
{
    /// <summary>
    ///  настраиваемый импорт из текстового файла
    /// </summary>
    public abstract class BaseImporter
    {
        /// <summary>
        /// Выбранные координаты для привязки с ряду
        /// </summary>
        public PointLatLng Coordinates;

        /// <summary>
        /// Столбцы импортируемых параметров (с единицы!)
        /// </summary>
        public Dictionary<ImportFields, int> Columns { get; set; }

        /// <summary>
        /// разделитель столбцов
        /// </summary>
        public string Delimeter { get; set; }

        /// <summary>
        /// удаляемые символы с концов значения
        /// </summary>
        public char[] Trimmers { get; set; }

        /// <summary>
        /// индекс строки с нуля, с которой начнется импорт
        /// </summary>
        public int StartLine { get; set; }

        /// <summary>
        /// Адрес файла
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Единицы измерения направлений
        /// </summary>
        public DirectionUnits DirectionUnit { get; set; }

        /// <summary>
        /// Единицы  измерения давления
        /// </summary>
        public PressureUnits PressureUnit { get; set; }

        /// <summary>
        /// Кодировка файла
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Единицы измерения влажности
        /// </summary>
        public WetnessUnits WetnessUnit { get; set; }

        /// <summary>
        /// Связать с ближайшей метеостанцией
        /// </summary>
        public bool? BindNearestMS { get; set; }

        /// <summary>
        /// Необязательные поля
        /// </summary>
        public List<ImportFields> NonRequireFields { get; set; }

        /// <summary>
        /// формат импортируемого файла
        /// </summary>
        public FileFormats FileFormat { get; set; }

        /// <summary>
        /// создает пустой объект импортера
        /// </summary>
        protected void Initialize(string filePath, BaseImporter baseImporter = null, FileFormats fileFormat = FileFormats.None)
        {
            FilePath = filePath;
            FileFormat = fileFormat;

            if (baseImporter == null)
            {
                Columns = new Dictionary<ImportFields, int>();
                Delimeter = string.Empty;
                Trimmers = null;
                StartLine = 0;
                DirectionUnit = DirectionUnits.None;
                PressureUnit = PressureUnits.None;
                Encoding = null;
                Coordinates = PointLatLng.Empty;
                BindNearestMS = null;
                WetnessUnit = WetnessUnits.None;
                NonRequireFields = new List<ImportFields>() { ImportFields.Pressure, ImportFields.Temperature, ImportFields.Wetness, ImportFields.Direction };
            }
            else
            {
                Columns = baseImporter.Columns;
                Delimeter = baseImporter.Delimeter;
                Trimmers = baseImporter.Trimmers;
                StartLine = baseImporter.StartLine;
                DirectionUnit = baseImporter.DirectionUnit;
                PressureUnit = baseImporter.PressureUnit;
                Encoding = baseImporter.Encoding;
                Coordinates = baseImporter.Coordinates;
                BindNearestMS = baseImporter.BindNearestMS;
                WetnessUnit = baseImporter.WetnessUnit;
                NonRequireFields = baseImporter.NonRequireFields;
            }
        }


        /// <summary>
        /// Пытается импортировать файл на основе заданных настроек. При ошибке выбрасывает исключение WindEnergyException с информацией или ArgumentException при недопустимых настройках
        /// </summary>
        /// <returns></returns>
        /// <param name="count">количество считываемых строк, начиная со StartLine</param>
        public RawRange Import(long count = long.MaxValue)
        {
            CheckParameters(); //проверка параметров

            //Импорт данных
            string data = GetText(count);

            RawRange res = new RawRange();
            res.BeginChange();

            string[] lines = data.Split('\r');
            int line_i = 0;
            foreach (string line in lines)
            {
                line_i++;
                string[] arr = new Regex("w*" + Delimeter + "w*").Split(line.Replace("\n", ""));
                if (arr.Length <= 1)
                    continue;

                List<ImportFields> fields = this.Columns.Keys.ToList();
                RawItem item = new RawItem();
                foreach (ImportFields field in fields)
                {

                    int index = Columns[field] - 1;
                    string value = arr[index];
                    if (Trimmers != null && Trimmers.Length > 0)
                        value = value.Trim(Trimmers);
                    try
                    {
                        switch (field)
                        {
                            case ImportFields.Date:
                                item.Date = DateTime.Parse(value);
                                break;
                            case ImportFields.Direction:
                                switch (DirectionUnit)
                                {
                                    case DirectionUnits.Degrees:
                                        item.Direction = double.Parse(value.Replace('.', Constants.DecimalSeparator));
                                        break;
                                    case DirectionUnits.TextRP5:
                                        item.DirectionRhumb = RP5ru.GetWindDirectionFromString(value);
                                        break;
                                    case DirectionUnits.None:
                                        throw new ArgumentException("Не заданы единицы измерения для направления");
                                    default:
                                        throw new WindEnergyException("Эта единица измерений не реализована");
                                }
                                break;
                            case ImportFields.Pressure:
                                switch (PressureUnit)
                                {
                                    case PressureUnits.KPa:
                                        item.Pressure = double.Parse(value.Replace('.', Constants.DecimalSeparator)) * Constants.MMHGART_IN_1KPA; //перевод в мм рт. ст.
                                        break;
                                    case PressureUnits.mmHgArt:
                                        item.Pressure = double.Parse(value.Replace('.', Constants.DecimalSeparator));
                                        break;
                                    case PressureUnits.None:
                                        throw new ArgumentException("Не заданы единицы измерения для давления");
                                    default:
                                        throw new WindEnergyException("Эта единица измерений не реализована");
                                }
                                break;
                            case ImportFields.Wetness:
                                switch (WetnessUnit)
                                {
                                    case WetnessUnits.Percents:
                                        item.Wetness = double.Parse(value.Replace('.', Constants.DecimalSeparator));
                                        break;
                                    case WetnessUnits.Parts:
                                        item.Wetness = double.Parse(value.Replace('.', Constants.DecimalSeparator)) * 100d;//перевод в %
                                        break;
                                    case WetnessUnits.None:
                                        throw new ArgumentException("Не заданы единицы измерения для влажности");
                                    default:
                                        throw new WindEnergyException("Эта единица измерений не реализована");
                                }
                                break;
                            case ImportFields.Temperature:
                                item.Temperature = double.Parse(value.Replace('.', Constants.DecimalSeparator));
                                break;
                            case ImportFields.Speed:
                                item.Speed = double.Parse(value.Replace('.', Constants.DecimalSeparator));
                                break;
                            default: throw new WindEnergyException("Этот параметр не реализован");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new WindEnergyException($"Не удалось распознать строку \"{value}\" как значение поля {field.Description()} в строке {line_i}", ex.Message);
                    }
                }
                res.Add(item);

            }

            if ((bool)BindNearestMS)
                res.Meteostation = Vars.RP5Meteostations.GetNearestMS(this.Coordinates);
            res.Position = this.Coordinates;
            res.Name = Path.GetFileNameWithoutExtension(this.FilePath);
            res.EndChange();
            res.PerformRefreshQuality();
            return res;
        }

        /// <summary>
        /// Проверка параметров. При неудаче выбрасывает ArgumentException
        /// </summary>
        public string CheckParameters()
        {
            if (PressureUnit == PressureUnits.None && !NonRequireFields.Contains(ImportFields.Pressure))
                throw new ArgumentException("Не заданы едиицы измерения давления");
            if (WetnessUnit == WetnessUnits.None && !NonRequireFields.Contains(ImportFields.Wetness))
                throw new ArgumentException("Не заданы едиицы измерения влажности");
            if (BindNearestMS == null)
                throw new ArgumentException("Не установлена связка с ближайшей МС");
            if (string.IsNullOrEmpty(Delimeter))
                throw new ArgumentException("Не задан разделитель столбцов");
            foreach (ImportFields fld in ImportFields.Date.GetEnumItems())
                if (!Columns.ContainsKey(fld) && !NonRequireFields.Contains(fld))
                    throw new ArgumentException($"Не указан столбец для параметра {fld.Description()}");
            if (!File.Exists(FilePath))
                throw new ArgumentException("Не задан путь к файлу или файл не существует");
            if (Coordinates.IsEmpty)
                throw new ArgumentException("Не выбраны координаты");
            return "Настройки верны";
        }

        /// <summary>
        /// получить заданное число строк из файла, начиная с указанной строки StartLine. 
        /// </summary>
        /// <param name="lineCount">long.MaxValue, если надо считать файл до конца</param>
        /// <returns></returns>
        public abstract string GetText(long lineCount);
    }
}
