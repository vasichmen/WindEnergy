using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    }
}
