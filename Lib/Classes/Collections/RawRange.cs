using GMap.NET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes.Collections.Generic;
using WindEnergy.Lib.Classes.Generic;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Calculations;
using WindEnergy.Lib.Statistic.Structures;

namespace WindEnergy.Lib.Classes.Collections
{
    /// <summary>
    /// представление ряда для редактирования
    /// </summary>
    public class RawRange : SortableBindingList<RawItem>
    {
        private QualityInfo _quality;
        private bool _locked;

        /// <summary>
        /// путь к файлу
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// название файла
        /// </summary>
        public string FileName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FilePath))
                    return Path.GetFileName(FilePath);
                else
                    return null;
            }
        }


        /// <summary>
        /// статистика ряда (полнота, количество элементов, интервалы измерений)
        /// </summary>
        public QualityInfo Quality
        {
            get
            {
                if (_quality != null) return _quality;
                else { _quality = Qualifier.ProcessRange(this); return _quality; }
            }
        }

        /// <summary>
        /// название
        /// </summary>
        public string Name
        {
            get => _name; set
            {
                _name = value.Replace("\\", "").Replace("/", "").Replace("?", "").Replace(":", "").Replace("*", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "");
            }
        }
        private string _name;

        /// <summary>
        /// информация об метеостанции, откуда получены данные
        /// </summary>
        public RP5MeteostationInfo Meteostation { get; set; }

        /// <summary>
        /// координаты точки радa
        /// </summary>
        public PointLatLng Position
        {
            get
            {
                if (_position.IsEmpty)
                {
                    if (Meteostation != null)
                        return Meteostation.Position;
                    return PointLatLng.Empty;
                }
                else
                    return _position;
            }
            set { _position = value; }
        }
        private PointLatLng _position = PointLatLng.Empty;

        /// <summary>
        /// длина ряда
        /// </summary>
        public TimeSpan Length
        {
            get
            {
                if (length == TimeSpan.MinValue)
                    length = this.Max((r) => r.Date).Date - this.Min((r) => r.Date).Date;
                return length;
            }
        }
        private TimeSpan length = TimeSpan.MinValue;

        /// <summary>
        /// плотность воздуха в кг/м3, рассчитанная  в зависимости от настроек
        /// </summary>
        public double AirDensity
        {
            get
            {
                if (double.IsNaN(airDensity))
                    //получаем плотность воздуха в зависимости от настроек
                    if (Vars.Options.CalculateAirDensity)
                        airDensity = PowerFunctions.GetAirDensity(this);
                    else
                        airDensity = Vars.Options.AirDensity;
                return airDensity;
            }
        }
        private double airDensity = double.NaN;


        /// <summary>
        /// Созадает новый пустой ряд
        /// </summary>
        public RawRange()
        {
            FilePath = null;
            ListChanged += rawRange_ListChanged;
            _locked = false;
            Position = PointLatLng.Empty;
        }

        /// <summary>
        /// создаёт ряд с указанными элементами внутри
        /// </summary>
        /// <param name="list"></param>
        public RawRange(List<RawItem> list) : this()
        {
            BeginChange();
            foreach (var t in list)
                this.Add(t);
            EndChange();
        }

        /// <summary>
        /// создаёт ряд с указанными элементами внутри
        /// </summary>
        /// <param name="list"></param>
        public RawRange(IEnumerable<RawItem> list) : this(list.ToList()) { }


        /// <summary>
        /// добавление коллекции элементов в список
        /// </summary>
        /// <param name="r"></param>
        internal void Add(IEnumerable<RawItem> r)
        {
            this.BeginChange();
            foreach (var r1 in r)
                this.Add(r1);
            this.EndChange();
        }

        /// <summary>
        /// Начало добавления большого числа элементов. Приостанавливает пересчёт всех параметров ряда
        /// </summary>
        public void BeginChange()
        {
            _locked = true;
            this.ListChanged -= rawRange_ListChanged;
        }

        /// <summary>
        /// окончание добавления большого числа элементов
        /// </summary>
        public void EndChange()
        {
            _locked = false;
            this.ListChanged += rawRange_ListChanged;
            rawRange_ListChanged(this, null);
        }

        /// <summary>
        /// возвращает функцию заданного параметра от времени
        /// </summary>
        /// <param name="parameterType"></param>
        /// <returns></returns>
        public Dictionary<double, double> GetFunction(MeteorologyParameters parameterType)
        {
            Dictionary<double, double> res = new Dictionary<double, double>();
            foreach (var f in this)
                switch (parameterType)
                {
                    case MeteorologyParameters.Direction:
                        res.Add(f.DateArgument, f.Direction);
                        break;
                    case MeteorologyParameters.Speed:
                        res.Add(f.DateArgument, f.Speed);
                        break;
                    case MeteorologyParameters.Temperature:
                        res.Add(f.DateArgument, f.Temperature);
                        break;
                    case MeteorologyParameters.Wetness:
                        res.Add(f.DateArgument, f.Wetness);
                        break;
                    default: throw new Exception("Этот параметр не реализован");
                }
            return res;
        }


        /// <summary>
        /// принудительное обновление статистики ряда
        /// </summary>
        public void PerformRefreshQuality()
        {
            _quality = null;
            length = TimeSpan.MinValue;
        }

        /// <summary>
        /// выбор ряда из исходного по заданной фильтрации
        /// </summary>
        /// <param name="isPeriod">истина, если надо выбрать период от fromDate до toDate</param>
        /// <param name="isYearMonth">истина, если надо выбрать по месяцу и году</param>
        /// <param name="fromDate">начальная дата</param>
        /// <param name="toDate">конечная дата</param>
        /// <param name="Year">значение selectedItem в combobox года(СТРОКА)</param>
        /// <param name="Month">значение selectedItem в combobox месяца(СТРОКА)</param>
        /// <returns></returns>
        public RawRange GetRange(bool isPeriod, bool isYearMonth, DateTime fromDate, DateTime toDate, object Year, object Month)
        {
            if (isPeriod != !isYearMonth)
                return null;

            RawRange res = null;

            //для выбранного периода времени
            if (isPeriod)
            {
                res = new RawRange((from ttt in this
                                    where ttt.Date >= fromDate && ttt.Date <= toDate
                                    orderby ttt.Date
                                    select ttt).ToList());
            }
            else
            {
                //для выбранного года или месяцев
                if (isYearMonth)
                {
                    Months mt = (Months)(new EnumTypeConverter<Months>().ConvertFrom(Month));
                    int month = (int)mt;

                    if (month == 0) //любой месяц
                    {
                        if (Year.GetType() == typeof(string) && (string)Year == "Все") //любой год, любой месяц
                            res = new RawRange((from ttt in this
                                                orderby ttt.Date
                                                select ttt).ToList());
                        else //любой месяц, заданный год
                        {
                            int yr = (int)Year;
                            res = new RawRange((from ttt in this
                                                where ttt.Date.Year == yr
                                                orderby ttt.Date
                                                select ttt).ToList());
                        }
                    }
                    else //заданный месяц
                    {
                        if (Year.GetType() == typeof(string) && (string)Year == "Все") //любой год, заданный месяц
                            res = new RawRange((from ttt in this
                                                where ttt.Date.Month == month
                                                orderby ttt.Date
                                                select ttt).ToList());
                        else //заданный месяц, заданный год
                        {
                            int yr = (int)Year;
                            res = new RawRange((from ttt in this
                                                where ttt.Date.Year == yr && ttt.Date.Month == month
                                                orderby ttt.Date
                                                select ttt).ToList());
                        }
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// возвращает ряд без последнего дня
        /// </summary>
        /// <returns></returns>
        public RawRange Trim()
        {
            var res = from t in this
                           where t.Date < this.Last().Date
                           select t;
            return new RawRange(res);
        }


        /// <summary>
        /// объединение рядов (добавляются только уникальные даты)
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public RawRange  Concat(RawRange range)
        {
            this.BeginChange();
            foreach(RawItem ri in range)
            {
                var r = from t in this
                        where t.DateArgument == ri.DateArgument
                        select t;
                if (r.Count() == 0)
                    this.Add(ri);

            }
            this.EndChange();
            return this;
        }

        /// <summary>
        /// при изменении коллекции перерасчёт параметров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rawRange_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (!_locked)
                PerformRefreshQuality();
            airDensity = double.NaN;
            length = TimeSpan.MinValue;
        }

        /// <summary>
        /// преобразует в строковое представление
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"count: {this.Count}, {this.Position.ToString()}, {this.Meteostation.ToString()}";
        }
    }
}
