using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.WindLib;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data;
using WindEnergy.WindLib.Data.Providers;
using WindEnergy.WindLib.Data.Providers.InternetServices;
using WindEnergy.WindLib.Statistic.Structures;
using WindEnergy.UI.Dialogs;
using WindEnergy.UI.Ext;
using WindEnergy.UI.Helpers;
using WindEnergy.UI.Tools;
using WindLib;
using CommonLib.UITools;
using CommonLib;

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
        public MainHelper mainHelper { get; set; }

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
#if !DEBUG
            bool fullVersion = Driver.CheckFullKey();
            ToolStripMenuItemRangeElevator.Visible = fullVersion;
            ToolStripMenuItemRangeTerrain.Visible = fullVersion;
#endif

            saveToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            saveAsToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            checkRangeToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            modelRangeToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            repairRangeToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            rangePropertiesToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            calculateEnergyInfoToolStripMenuItem.Enabled = mainTabControl.SelectedTab != null;
            ToolStripMenuItemCalcYear.Enabled = mainTabControl.SelectedTab != null;
            ToolStripMenuItemRangeElevator.Enabled = mainTabControl.SelectedTab != null;
            ToolStripMenuItemRangeTerrain.Enabled = mainTabControl.SelectedTab != null;
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
            RawRange rang = this.mainHelper.OpenFile();
            if (rang == null)
                return;
            TabPageExt tab = this.mainTabControl.OpenNewTab(rang, rang.FileName);
            tab.HasNotSavedChanges = false;
        }

        /// <summary>
        /// импортировать текстовый файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFileImport fti = new FormFileImport();
            if (fti.ShowDialog(this) == DialogResult.OK)
            {
                RawRange rang = fti.Result;
                TabPageExt tab = mainTabControl.OpenNewTab(rang, rang.FileName);
                tab.HasNotSavedChanges = true;
            }
            fti.Dispose();
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
            frm.Dispose();
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
            frm.Dispose();
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
                _ = mainTabControl.OpenNewTab(frm.Result, frm.Result.Name);
            }
            frm.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repairRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            FormRepairRange frm = new FormRepairRange(rang, new List<InterpolateMethods>() { InterpolateMethods.NearestMeteostation }, "Восстановление ряда", "Восстановить ряд");
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _ = mainTabControl.OpenNewTab(frm.Result, frm.Result.Name);
            }
            frm.Dispose();
        }

        /// <summary>
        /// восстановить ряд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modelRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            FormRepairRange frm = new FormRepairRange(rang, new List<InterpolateMethods>() { InterpolateMethods.Linear, InterpolateMethods.Stepwise }, "Моделирование ряда", "Моделировать ряд");
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _ = mainTabControl.OpenNewTab(frm.Result, frm.Result.Name);
            }
            frm.Dispose();
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
            _ = frm.ShowDialog(this);
            frm.Dispose();
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
            var w = new FormEqualizer(Vars.Options.LastDirectory, this.Icon);
            w.Show(this);
            Vars.Options.LastDirectory = w.Directory;
        }

        /// <summary>
        /// среднесуточные графики
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dailyAverageGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var w = new FormDailyAverageGraphs(Vars.Options.LastDirectory, this.Icon);
            w.Show();
            Vars.Options.LastDirectory = w.Directory;
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
        /// пересчет ряда на высоту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemRangeElevator_Click(object sender, EventArgs e)
        {
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            FormRangeElevator frm = new FormRangeElevator(rang);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _ = mainTabControl.OpenNewTab(frm.Result, frm.Result.Name);
            }
            frm.Dispose();
        }

        /// <summary>
        /// пересчет скорости ветра в точку ВЭС
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemRangeTerrain_Click(object sender, EventArgs e)
        {
            RawRange rang = (mainTabControl.SelectedTab as TabPageExt).Range;
            FormRangeTerrain frm = new FormRangeTerrain(rang);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _ = mainTabControl.OpenNewTab(frm.Result, frm.Result.Name);
            }
            frm.Dispose();
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

        /// <summary>
        /// загрузка данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormLoadData().Show();
        }

        #endregion

        #region Помощь

        /// <summary>
        /// Кнопка О программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAbout().Show();
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
                _ = contextMenuStripRangeIntervals.Items.Add(intt.ToString());

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


        private void FormMain_Activated(object sender, EventArgs e)
        {
            Thread.Sleep(500);
            if (DateTime.Now - Vars.LastCheckEngine < TimeSpan.FromSeconds(30))
            {
                //TODO: добавить проверку ключа
            }
        }

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
            bool f = Driver.CheckFullKey();

        }
    }
}
