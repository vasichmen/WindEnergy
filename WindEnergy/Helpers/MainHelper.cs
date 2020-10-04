using CommonLib;
using System;
using System.IO;
using System.Windows.Forms;
using WindEnergy.UI.Ext;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Data;
using WindLib;

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
        /// Открыть файл 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        internal RawRange OpenFile(Form form = null)
        {
            if (form == null)
                form = f;
            OpenFileDialog of = new OpenFileDialog();
            of.InitialDirectory = Vars.Options.LastDirectory;
            of.Multiselect = true;
            of.Filter = "Все файлы|*.*";
            of.Filter += "|Файл Excel (*.xlsx)|*.xlsx";
            of.Filter += "|Файл csv (*.csv)|*.csv";
            if (of.ShowDialog(form) == DialogResult.OK)
            {
                foreach (string file in of.FileNames)
                {
                    try
                    {
                        f.Cursor = Cursors.WaitCursor;
                        RawRange rang = RawRangeSerializer.DeserializeFile(file, null);
                        rang.FilePath = file;
                        rang.Name = Path.GetFileNameWithoutExtension(file);
                        Vars.Options.LastDirectory = Path.GetDirectoryName(file);
                        return rang;
                    }
                    catch (Exception ex)
                    {
                        _ = MessageBox.Show(form, ex.Message, "Открытие файла " + file, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        f.Cursor = Cursors.Default;
                    }
                }
            }
            of.Dispose();
            return null;
        }

        /// <summary>
        /// сохранить как отдельный файл
        /// </summary>
        /// <param name="rang"></param>
        internal string SaveAsFile(RawRange rang, string fileName = null)
        {
            try
            {
                f.Cursor = Cursors.WaitCursor;
                if (fileName == null)
                {
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.InitialDirectory = Vars.Options.LastDirectory;
                    sf.AddExtension = true;
                    sf.FileName = rang.Name;
                    sf.Filter = "Файл Excel (*.xlsx)|*.xlsx";
                    sf.Filter += "|Файл csv (*.csv)|*.csv";

                    if (sf.ShowDialog(f) == DialogResult.OK)
                    {
                        Vars.Options.LastDirectory = Path.GetDirectoryName(sf.FileName);
                        RawRangeSerializer.SerializeFile(rang, sf.FileName);
                        rang.FilePath = sf.FileName;
                        return sf.FileName;
                    }
                    sf.Dispose();
                    return null;
                }
                else
                {
                    RawRangeSerializer.SerializeFile(rang, fileName);
                    rang.FilePath = fileName;
                    return fileName;
                }
            }
            catch (Exception e)
            {
                string msg = e.InnerException.InnerException != null ? e.InnerException.InnerException.Message : e.Message;
                _ = MessageBox.Show(this.f, $"Не удалось сохранить файл, причина:\r\n{msg}", "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                f.Cursor = Cursors.Default;
            }
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
                _ = SaveAsFile(rang, rang.FilePath);
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
