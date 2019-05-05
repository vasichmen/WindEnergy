using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.Lib.Operations;
using WindEnergy.Lib.Operations.Interpolation;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Structures;
using WindEnergy.UI.Ext;
using WindEnergy.UI.Helpers;
using WindEnergy.UI.Tools;

namespace WindEnergy.UI
{
    /// <summary>
    /// главное окно программы
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// вспомогательные методы интерфейса
        /// </summary>
        public MainHelper mainHelper;


        public FormMain()
        {
            InitializeComponent();
            mainHelper = new MainHelper(this);
#if DEBUG
            button1.Visible = true;
#endif

        }

        #region Главное меню

        /// <summary>
        /// обновление состояний кнопок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
            saveToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            saveAsToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            checkRangeToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            modelRangeToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            repairRangeToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            rangePropertiesToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            calculateEnergyInfoToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            ToolStripMenuItemCalcYear.Enabled = mainTabControl.SelectedTab != null;
        }

        #region Файл

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// сохранить изменения в документе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab == null)
                return;
            mainHelper.Save(mainTabControl.SelectedTab);
            (mainTabControl.SelectedTab as TabPageExt).HasNotSavedChanges = false;
        }

        /// <summary>
        /// сохранить как отдельный файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab == null)
                return;
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            string name = mainHelper.SaveAsFile(rang);
            if (!string.IsNullOrWhiteSpace(name))
            {
                mainTabControl.SelectedTab.Text = Path.GetFileName(name);
                mainTabControl.SelectedTab.ToolTipText = name;
            }
            (mainTabControl.SelectedTab as TabPageExt).HasNotSavedChanges = false;
        }

        /// <summary>
        /// создать новый документ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawRange r = new RawRange();
            r.Name = "Новый документ";
            TabPageExt tab = mainTabControl.OpenNewTab(r);
            tab.HasNotSavedChanges = true;
        }

        /// <summary>
        /// открыть файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.InitialDirectory = Vars.Options.LastDirectory;
            of.Multiselect = true;

            if (of.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string file in of.FileNames)
                {
                    try
                    {
                        string ext = Path.GetExtension(file).ToLower();
                        RawRange rang = RawRangeSerializer.DeserializeFile(file, null);
                        rang.FilePath = file;
                        rang.Name = Path.GetFileNameWithoutExtension(file);
                        TabPageExt tab = mainTabControl.OpenNewTab(rang, rang.FileName);
                        tab.HasNotSavedChanges = false;
                        Vars.Options.LastDirectory = Path.GetDirectoryName(file);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, "Открытие файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }


        /// <summary>
        /// загрузить с сайта rp5.ru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloadRP5ruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLoadFromRP5 frm = new FormLoadFromRP5();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                RawRange res = frm.Result;
                TabPageExt tab = mainTabControl.OpenNewTab(res, res.Name);
                tab.HasNotSavedChanges = true;
            }
        }

        /// <summary>
        /// загрузить из БД NASA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloadNASAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLoadFromNASA frm = new FormLoadFromNASA();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                RawRange res = frm.Result;
                TabPageExt tab = mainTabControl.OpenNewTab(res, res.Name);
                tab.HasNotSavedChanges = true;
            }
        }

        /// <summary>
        /// посмотреть доступные метеостанции на карте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showMeteostationsMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormShowMeteostationsMap().Show();
        }

        #endregion

        #region Правка

        /// <summary>
        /// проверить 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkRepairRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            FormCheckRange frm = new FormCheckRange(rang);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                mainTabControl.OpenNewTab(frm.Result, frm.Result.Name);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repairRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            FormRepairRange frm = new FormRepairRange(rang, new List<InterpolateMethods>() { InterpolateMethods.NearestMeteostation }, "Восстановление ряда");
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                mainTabControl.OpenNewTab(frm.Result, frm.Result.Name);
            }
        }

        /// <summary>
        /// восстановить ряд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modelRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            FormRepairRange frm = new FormRepairRange(rang, new List<InterpolateMethods>() { InterpolateMethods.Linear, InterpolateMethods.Stepwise }, "Моделирование ряда");
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                mainTabControl.OpenNewTab(frm.Result, frm.Result.Name);
            }
        }

        /// <summary>
        /// просмотр и редатирование свойств ряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rangePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            FormRangeProperties frm = new FormRangeProperties(rang);
            frm.ShowDialog(this);
        }

        #endregion

        #region Операции

        /// <summary>
        /// привести ряды с разными интервалами наблюдений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void equalizeRangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofMax = new OpenFileDialog()
            {
                DefaultExt = ".csv",
                Filter = "*.csv|*.csv",
                Multiselect = false,
                InitialDirectory = Application.StartupPath
            };
            MessageBox.Show("Выберите ряд с большим интервалом");
            if (ofMax.ShowDialog() == DialogResult.OK)
            {
                OpenFileDialog ofMin = new OpenFileDialog()
                {
                    DefaultExt = ".csv",
                    Filter = "*.csv|*.csv",
                    Multiselect = false,
                    InitialDirectory = Application.StartupPath
                };
                MessageBox.Show("Выберите ряд с меньшим интервалом");
                if (ofMin.ShowDialog() == DialogResult.OK)
                {
                    Equalizer.ProcessRange(ofMax.FileName, ofMin.FileName);
                }
            }
        }

        /// <summary>
        /// расчитать основные энергетические характеристики
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculateEnergyInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            FormEnergyInfo fei = new FormEnergyInfo(rang);
            fei.Show(this);
        }

        /// <summary>
        /// выбор расчетного года
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCalcYear_Click(object sender, EventArgs e)
        {
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            FormCalcYear fei = new FormCalcYear(rang);
            fei.Show(this);
        }

        /// <summary>
        /// настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormOptions().Show(this);
        }

        #endregion


        #endregion

        #region меню управления

        /// <summary>
        /// сохранить все документы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAlltoolStripButton_Click(object sender, EventArgs e)
        {
            foreach (TabPageExt tab in mainTabControl.TabPages)
                mainHelper.Save(tab);
        }

        #endregion

        #region строка состояния внизу экрана

        /// <summary>
        /// открытие списка интерваолв измерений в статусной строке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStatusLabelInterval_MouseEnter(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab == null)
                return;
            contextMenuStripRangeIntervals.Items.Clear();
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            foreach (RangeInterval intt in rang.Quality.Intervals)
                contextMenuStripRangeIntervals.Items.Add(intt.ToString());

            contextMenuStripRangeIntervals.Show(MousePosition);
        }

        /// <summary>
        /// закрытие списка интервалов в статусной строке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStatusLabelInterval_MouseLeave(object sender, EventArgs e)
        {
            contextMenuStripRangeIntervals.Close();
        }

        /// <summary>
        /// нажатие на ссылку статистики ряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStatusLabelInterval_Click(object sender, EventArgs e)
        {
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            new FormRangeStatistic(rang).Show(this);
        }

        #endregion


        /// <summary>
        /// подтверждение закрытия окна приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.MdiFormClosing: //если окно закрывается пользователем, то надо проверить все вкладки и сохранить
                case CloseReason.FormOwnerClosing:
                case CloseReason.UserClosing:
                    foreach (TabPageExt tab in mainTabControl.TabPages)
                    {
                        bool cancelClosing = tab.ClosePage();
                        if (cancelClosing)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                    break;
            }
        }

        private void formMain_Shown(object sender, EventArgs e)
        {
            mainHelper.RefreshStatusBar();
        }


        /// <summary>
        /// изменение типа столбцов при добавлении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            switch (e.Column.Name.ToLower())
            {
                case "date":
                    e.Column.HeaderText = "Дата наблюдения";
                    e.Column.Width = 130;
                    e.Column.CellTemplate = new DataGridViewCalendarCell();
                    break;
                case "direction":
                    e.Column.HeaderText = "Направление, °";
                    e.Column.DefaultCellStyle.Format = "n2";
                    break;
                case "directionrhumb":
                    e.Column.HeaderText = "Румб";
                    e.Column.Width = 55;
                    e.Column.CellTemplate = new DataGridViewComboboxCell<WindDirections>();
                    break;
                case "speed":
                    e.Column.HeaderText = "Скорость, м/с";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "temperature":
                    e.Column.HeaderText = "Температура, °С";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "wetness":
                    e.Column.HeaderText = "Влажность, %";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;

                //удаляемые колонки пишем тут:
                case "dateargument":
                    e.Column.DataGridView.Columns.Remove(e.Column);
                    break;
                default: throw new Exception("Для этой колонки нет названия");
            }
        }

        /// <summary>
        /// обновление информации в статусной строке при изменении выбранной вкладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainTabControl_Selected(object sender, TabControlEventArgs e)
        {
            mainHelper.RefreshStatusBar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // (mainTabControl.SelectedTab as TabPageExt).Range.PerformRefreshQuality();
            Scripts.DownloadMeteostationExtInfo(Vars.LocalFileSystem.MeteostationList, Vars.Options.StaticMeteostationCoordinatesSourceFile, Vars.ETOPOdatabase);

        }

    }
}
