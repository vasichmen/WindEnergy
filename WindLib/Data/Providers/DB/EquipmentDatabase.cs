using CommonLib;
using CommonLib.Classes.Base;
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
    /// БД оборудования
    /// </summary>
    public class EquipmentDatabase : BaseFileDatabase<int, EquipmentItemInfo>
    {
        /// <summary>
        /// Создает БД с указанным файлом, не загружая файл.
        /// </summary>
        /// <param name="FileName"></param>
        public EquipmentDatabase(string FileName) : base(FileName) { }

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

                    //максимальная скорость
                    string[] arrMaxSpeed = arr[12].Split('/');
                    List<double> maxSpeed = new List<double>();
                    foreach (string v in arrMaxSpeed)
                    {
                        double h = double.NaN;
                        if (double.TryParse(v.Trim().Replace('.', Constants.DecimalSeparator), out h))
                            maxSpeed.Add(h);
                    }

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
                        MaxWindSpeed = maxSpeed,
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
