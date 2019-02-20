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
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Calculations;

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


        public RawRange()
        {
            FilePath = null;
            ListChanged += rawRange_ListChanged;
            _locked = false;
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
    }
}
