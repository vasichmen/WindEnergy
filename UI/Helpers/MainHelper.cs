using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Data;
using WindEnergy.UI.Ext;

namespace WindEnergy.UI.Helpers
{
    /// <summary>
    /// вспомогательные методы для интерфейса основного окна
    /// </summary>
    public class MainHelper
    {
        private readonly FormMain f = null;

        public MainHelper(FormMain mainf)
        {
            f = mainf;
        }

        /// <summary>
        /// сохранить как отдельный файл
        /// </summary>
        /// <param name="rang"></param>
        internal string SaveAsFile(RawRange rang)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.InitialDirectory = Vars.Options.LastDirectory;
            sf.AddExtension = true;
            sf.FileName = rang.Name;

            sf.Filter = "Файл rp5.ru METAR (*.csv)|*.csv";
            sf.Filter += "|Файл rp5.ru WMO (*.csv)|*.csv";

            if (sf.ShowDialog(f) == DialogResult.OK)
            {
                Vars.Options.LastDirectory = Path.GetDirectoryName(sf.FileName);
                FileFormats format;
                switch (sf.FilterIndex)
                {
                    case 1:
                        format = FileFormats.RP5MetarCSV;
                        break;
                    case 2:
                        format = FileFormats.RP5WmoCSV;
                        break;
                    default: throw new Exception("Этот тип не реализован");
                }
                RawRangeSerializer.SerializeFile(rang, sf.FileName, format);
                rang.FilePath = sf.FileName;
                return sf.FileName;
            }
            return null;
        }

        /// <summary>
        /// сохранить документ на заданной вкладке
        /// </summary>
        /// <param name="tab">вкладка, документ которой надо сохранить</param>
        internal void Save(TabPage tab)
        {
            if (tab == null)
                return;
            RawRange rang = (tab as TabPageExt).Range;
            if (!string.IsNullOrWhiteSpace(rang.FilePath)) // если есть путь для сохранения
                RawRangeSerializer.SerializeFile(rang, rang.FilePath, rang.FileFormat);
            else
            {
                string name = SaveAsFile(rang);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    tab.Text = Path.GetFileName(name);
                    tab.ToolTipText = name;
                }
            }
        }

        /// <summary>
        /// обновление информации в статусной строке в соответствии с выбранной вкладкой
        /// </summary>
        internal void RefreshStatusBar()
        {
            if (f.mainTabControl.SelectedTab != null)
            {
                RawRange rang = (f.mainTabControl.SelectedTab as TabPageExt).Range;
                if (rang.Quality != null)
                {
                    f.toolStripStatusLabelRangeCount.Text = "Количество измерений: " + rang.Count;
                    f.toolStripStatusLabelCompletness.Text = "Полнота ряда: " + (rang.Quality.Completeness * 100).ToString("0.00") + "%";
                    f.toolStripStatusLabelInterval.Text = "Интервал: " + (rang.Quality.Intervals.Count == 1 ? rang.Quality.Intervals[0].Interval.Description() : "Неоднородный ряд");
                }
                else
                {
                    f.toolStripStatusLabelRangeCount.Text = "";
                    f.toolStripStatusLabelCompletness.Text = "";
                    f.toolStripStatusLabelInterval.Text = "";
                }
            }
            else
            {
                f.toolStripStatusLabelRangeCount.Text = "";
                f.toolStripStatusLabelCompletness.Text = "";
                f.toolStripStatusLabelInterval.Text = "";
            }
        }
    }
}
