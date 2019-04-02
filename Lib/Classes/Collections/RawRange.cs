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
        public QualityInfo Quality { get { if (_quality != null) return _quality; else { _quality = Qualifier.ProcessRange(this); return _quality; } } }

        /// <summary>
        /// название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// формат файла
        /// </summary>
        public FileFormats FileFormat { get; set; }

        /// <summary>
        /// координаты точки радa
        /// </summary>
        public PointLatLng Position { get;  set; }

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
        public RawRange(List<RawItem> list):this()
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
        }

        /// <summary>
        /// окончание добавления большого числа элементов
        /// </summary>
        public void EndChange()
        {
            _locked = false;
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
            foreach(var f in this)
                switch(parameterType)
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
            _quality = Qualifier.ProcessRange(this);
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
        }

        /// <summary>
        /// сортировка списка 
        /// </summary>
        /// <param name="dateTimeComparer"></param>
        internal void Sort(DateTimeComparer dateTimeComparer)
        {
            List<RawItem> tmp = this.ToList();
            this.Clear();
            tmp.Sort(dateTimeComparer);
            this.Add(tmp);
        }
    }
}
