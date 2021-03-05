using CommonLib.Classes.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.WindLib.Data.Providers.DB
{
    /// <summary>
    /// БД оборудования
    /// </summary>
    public class EquipmentDatabase : BaseFileDatabase<int, EquipmentItemInfo>
    {
        /// <summary>
        /// Создает БД с указанным файлом, не загружая файл.
        /// </summary>
        /// <param name="FileName"></param>
        public EquipmentDatabase(string FileName) : base(FileName) { }

        public override void ExportDatabaseFile()
        {
            using (StreamWriter sw = new StreamWriter(this.FileName, false, Encoding.UTF8))
            {
                //              0            1      2               3        4         5              6              7                   8          9               10                 11              12               13                                                                            42    43             44           45               46                            47        48               49            50       51     52      53   54    55      56            57                    58         
                sw.WriteLine("Производитель;Модель;Мощность, кВт;Диаметр,м;Регулир.;Мультипликатор;Оффшорисполн.;Макс.скорость, м/с;Производство;Высота башни, м;Мин.скорость, м/с;Ном.скорость, м/с;Макс.скорость, м/с;1;2;3;4;5;6;7;8;9;10;11;12;13;14;15;16;17;18;19;20;21;22;23;24;25;26;27;28;29;30;Число лопастей;Угол лопасти;Высота втулки, м;Вращ. ВК, об/мин;перед. Число;Тип ген-ра;Напряж.ген., кВ;Вращ.рот, об/мин;Лопасть;Ротор;Гондола;Башня;Общий;Страна;Шум, дБ(А);Систмы регулирования;Тормозная система;;;Сайт;;;;;;;;Видео");

                foreach (EquipmentItemInfo item in List)
                {
                    string regulator;
                    switch (item.Regulator)
                    {
                        case TurbineRegulations.Pitch: regulator = "pitch"; break;
                        case TurbineRegulations.Stall: regulator = "stall"; break;
                        case TurbineRegulations.Fixed: regulator = "fixed"; break;
                        case TurbineRegulations.None: regulator = "nd"; break;
                        default: throw new Exception("Этот тип регулирования не реализован");
                    }


                    string characteristic = "";
                    for (double i = 1; i <= 30; i++)
                        characteristic += (item.PerformanceCharacteristic.ContainsKey(i) ? item.PerformanceCharacteristic[i].ToString() : "") + ";";

                    //                                      7    9   10 11  12
                    string format = "{0};{1};{2};{3};{4};;;{5};;{6};{7};{8};{9};{10}";
                    string line = string.Format(format,
                        item.Developer, //0
                        item.Model, //1
                        item.Power, //2
                        item.Diameter,//3
                        regulator,//4
                        item.MaxWindSpeed,//5
                        item.TowerHeightString,//6
                        item.MinWindSpeed,//7
                        item.NomWindSpeed,//8
                        item.MaxWindSpeed,//9
                        characteristic
                        );
                    sw.WriteLine(line);
                }

                sw.Close();
            }
        }

        protected override int GenerateNextKey()
        {
            return Dictionary.Keys.Max() + 1;
        }

        /// <summary>
        /// загрузка файла БД
        /// </summary>
        /// <returns></returns>
        public override Dictionary<int, EquipmentItemInfo> LoadDatabaseFile()
        {
            using (StreamReader sr = new StreamReader(this.FileName, Encoding.UTF8))
            {
                Dictionary<int, EquipmentItemInfo> res = new Dictionary<int, EquipmentItemInfo>();
                sr.ReadLine();
                int id = 0;
                while (!sr.EndOfStream)
                {
                    string[] arr = sr.ReadLine().Split(';');

                    //рабочая характеристика
                    Dictionary<double, double> perf = new Dictionary<double, double>();
                    int st = 13, length = 30;
                    for (int i = st; i < st + length; i++)
                    {
                        bool exist = double.TryParse(arr[i].Trim().Replace('.', Constants.DecimalSeparator), out double val);
                        if (exist)
                            perf.Add(i - st + 1, val);
                    }

                    //диаметр
                    double d = double.NaN;
                    double.TryParse(arr[3].Trim().Replace('.', Constants.DecimalSeparator), out d);

                    //мощность
                    double power = double.NaN;
                    double.TryParse(arr[2].Trim().Replace('.', Constants.DecimalSeparator), out power);

                    //максимальная скорость (выбирается минимальная из списка)
                    string[] arrMaxSpeed = arr[12].Split('/');
                    List<double> maxSpeed = new List<double>();
                    foreach (string v in arrMaxSpeed)
                    {
                        double h = double.NaN;
                        if (double.TryParse(v.Trim().Replace('.', Constants.DecimalSeparator), out h))
                            maxSpeed.Add(h);
                    }
                    double maxSpeedResult = maxSpeed.Count > 0 ? maxSpeed.Min() : double.NaN;

                    //минимальная скорость
                    double minSpeed = double.NaN;
                    double.TryParse(arr[10].Trim().Replace('.', Constants.DecimalSeparator), out minSpeed);

                    //номинальная скорость
                    double nomSpeed = double.NaN;
                    double.TryParse(arr[11].Trim().Replace('.', Constants.DecimalSeparator), out nomSpeed);

                    //высота башни 
                    string[] arrH = arr[9].Trim().Split('/');
                    List<double> height = new List<double>();
                    foreach (string v in arrH)
                    {
                        double h = double.NaN;
                        if (double.TryParse(v.Replace('.', Constants.DecimalSeparator), out h))
                            height.Add(h);
                    }

                    //тип регулирования
                    TurbineRegulations regulator;
                    switch (arr[4].ToLower().Trim())
                    {
                        //мусорные типы
                        case "100":
                        case "":
                            continue;

                        //типы регулирования
                        case "pitch":
                            regulator = TurbineRegulations.Pitch;
                            break;
                        case "stall":
                            regulator = TurbineRegulations.Stall;
                            break;
                        case "fixed":
                            regulator = TurbineRegulations.Fixed;
                            break;
                        case "nd":
                            regulator = TurbineRegulations.None;
                            break;
                        default: throw new Exception("Тип регулирования не реализован");
                    }

                    res.Add(id++, new EquipmentItemInfo()
                    {
                        ID = id,
                        PerformanceCharacteristic = perf,
                        Developer = arr[0].Trim(),
                        Model = arr[1].Trim(),
                        Diameter = d,
                        MaxWindSpeed = maxSpeedResult,
                        MinWindSpeed = minSpeed,
                        NomWindSpeed = nomSpeed,
                        Power = power,
                        Regulator = regulator,
                        TowerHeight = height
                    });
                }
                sr.Close();
                return res;
            }
        }
    }
}
