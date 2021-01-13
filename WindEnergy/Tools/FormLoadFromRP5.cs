using CommonLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.UI.Dialogs;
using WindEnergy.UI.Ext;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data.Providers.InternetServices;
using WindEnergy.WindLib.Transformation.Check;
using WindLib;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// окно загрузки ряда с сайта rp5.ru
    /// </summary>
    public partial class FormLoadFromRP5 : Form
    {
        /// <summary>
        /// список ближайших метеостанций к выбранной точке погоды
        /// </summary>
        private RP5MeteostationInfo selectedMeteostation = null;
        private RP5ru engine;

        public FormLoadFromRP5()
        {
            InitializeComponent();
            engine = new RP5ru(Vars.Options.CacheFolder + "\\rp5.ru");
            DialogResult = DialogResult.None;
        }

        /// <summary>
        /// открытие формы с уже выбранной МС
        /// </summary>
        public FormLoadFromRP5(RP5MeteostationInfo meteostaion) : this()
        {
            selectedMeteostation = meteostaion;
        }

        /// <summary>
        /// результат работы окна 
        /// </summary>
        public RawRange Result { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formLoadFromRP5_Shown(object sender, EventArgs e)
        {
            dateTimePickerToDate.Value = DateTime.Now.AddDays(-5);
            dateTimePickerFromDate.Value = DateTime.Now.AddDays(-20);

            if (selectedMeteostation != null)
            {
                comboBoxPoint.Text = selectedMeteostation.Name;

                //установка времени начала и конца наблюдений
                dateTimePickerFromDate.MinDate = selectedMeteostation.MonitoringFrom;
                dateTimePickerToDate.MaxDate = DateTime.Now;
                labelDateRange.Text = "Выберите диапазон дат: (дата начала наблюдений: " + selectedMeteostation.MonitoringFrom.ToString() + ")";

                //разблокировка элементов
                dateTimePickerFromDate.Enabled = true;
                dateTimePickerToDate.Enabled = true;
                buttonDownload.Enabled = true;
            }
        }

        /// <summary>
        /// загрузка данных с сайта и закрытие диалогового окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (selectedMeteostation == null)
            {
                _ = MessageBox.Show(this, "Не выбрана метеостанция или координаты метеостанции недоступны", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Action<double> pcChange = new Action<double>((pc) =>
            {
                if (this.InvokeRequired)
                {
                    _ = Invoke(new Action(() =>
                      {
                          progressBarProgress.Value = (int)pc;
                          progressBarProgress.Refresh();
                      }));
                }
                else
                {
                    progressBarProgress.Value = (int)pc;
                    progressBarProgress.Refresh();
                }
            });
            Action<RawRange> success = new Action<RawRange>((res) =>
            {
                Result = res;
                DialogResult = DialogResult.OK;
                Close();
            });
            Action<Exception> error1 = new Action<Exception>((ex) =>
            {
                buttonDownload.Enabled = true;
                _ = MessageBox.Show(this, ex.Message + "\r\n" + (ex.InnerException != null ? ex.InnerException.Message : ""), "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            });
            Action<Exception> error2 = new Action<Exception>((ae) =>
            {
                buttonDownload.Enabled = true;
                _ = MessageBox.Show(this, ae.Message + "\r\n" + (ae.InnerException != null ? ae.InnerException.Message : "\r\n") + "\r\nПопробуйте выбрать меньший интервал времени", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            });
            Action<string> setStatus = new Action<string>((text) => { labelStatus.Text = text; });

            buttonDownload.Enabled = false;

            new Task(() =>
            {
                try
                {
                    this.Invoke(setStatus, "Загрузка ряда...");
                    RawRange res = engine.GetRange(dateTimePickerFromDate.Value, dateTimePickerToDate.Value, selectedMeteostation, pcChange);
                    if (checkBoxClearRange.Checked)
                    {
                        pcChange.Invoke(0);
                        this.Invoke(setStatus, "Очистка ряда...");
                        res = new Checker().ProcessRange(res, new CheckerParameters(LimitsProviders.StaticLimits, res.Position), out CheckerInfo stats, pcChange);
                    }

                    if (this.InvokeRequired)
                        _ = Invoke(success, res);
                    else
                        success.Invoke(res);
                }
                catch (WebException ex)
                {
                    if (this.InvokeRequired)
                        _ = Invoke(error1, ex);
                    else
                        error1.Invoke(ex);
                }
                catch (ApplicationException ae)
                {

                    if (this.InvokeRequired)
                        _ = Invoke(error2, ae);
                    else
                        error2.Invoke(ae);
                }
                catch (Exception ee)
                {
                    if (this.InvokeRequired)
                        _ = Invoke(error1, ee);
                    else
                        error1.Invoke(ee);
                }
            }).Start();

        }

        /// <summary>
        /// фильтрация по началу города
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxPoint_TextUpdate(object sender, EventArgs e)
        {
            buttonDownload.Enabled = false;
            selectedMeteostation = null;
            dateTimePickerFromDate.Enabled = false;
            dateTimePickerToDate.Enabled = false;
            string curTextBox = comboBoxPoint.Text.Trim();
            switch (Vars.Options.RP5SearchEngine)
            {
                case RP5SearchEngine.DBSearch:
                    List<RP5MeteostationInfo> results = Vars.RP5Meteostations.Search(curTextBox);
                    comboBoxPoint.Items.Clear();
                    comboBoxPoint.Items.AddRange(results.ToArray());
                    comboBoxPoint.SelectionStart = comboBoxPoint.Text.Length;
                    break;
                case RP5SearchEngine.OnlineAPI:
                    updateWMOListAsync(curTextBox);
                    break;
            }
        }

        /// <summary>
        /// отрисовка элемента списка и вывод подсказки, если он выбран
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxPoint_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1) return;
            string text = this.comboBoxPoint.GetItemText(comboBoxPoint.Items[e.Index]);
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(text, e.Font, br, e.Bounds);
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                if (text.Length > Math.Round(Convert.ToDecimal(this.comboBoxPoint.Size.Width / 8)))
                    this.toolTip1.Show(text, comboBoxPoint, e.Bounds.Right, e.Bounds.Bottom);
            }
            else
                this.toolTip1.Hide(comboBoxPoint);
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// асинхронная загрузка списка адресов метеостанций 
        /// </summary>
        /// <param name="query">запрос</param>
        private async void updateWMOListAsync(string query)
        {
            if (query.Length < 2)
                return;

            //действие обновления списка подсказок
            Action<List<RP5ru.WmoInfo>> updList = new Action<List<RP5ru.WmoInfo>>((list) =>
            {
                comboBoxPoint.Items.Clear();
                comboBoxPoint.Items.AddRange(list.ToArray());
                comboBoxPoint.SelectionStart = comboBoxPoint.Text.Length;
            });
            try
            {
                List<RP5ru.WmoInfo> results;
                await Task.Run(() =>
                {
                    Thread.Sleep(1000); //ждем 1 с

                    //получаем новый текст 
                    string curTextBox = "";
                    if (InvokeRequired)
                        _ = Invoke(new Action(() => { curTextBox = comboBoxPoint.Text.Trim(); }));
                    else
                        curTextBox = comboBoxPoint.Text.Trim();

                    if (query != curTextBox) //если этот текст  изменился, то выходим
                        return;

                    selectedMeteostation = null;
                    results = engine.Search(query);
                    //обновление списка
                    if (InvokeRequired)
                        _ = Invoke(updList, results);
                    else
                        updList.Invoke(results);

                }).ConfigureAwait(false);
            }
            catch (WebException)
            {
                _ = MessageBox.Show(this, "Ошибка подключения, проверьте соединение с Интернет", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ApplicationException exc)
            {
                _ = MessageBox.Show(this, exc.Message, "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Debug.WriteLine("updateList end");
        }

        /// <summary>
        /// проверка после выбора метеостанции 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxPoint_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //после того, как из поиска выбрана точка, надо проверить, есть ли на ней архив погоды. и вывести предупреждение, если ближайший архив далеко

            try
            {
                if (comboBoxPoint.SelectedItem == null)
                    return;

                //выбор метеостанции
                if (comboBoxPoint.SelectedItem.GetType() == typeof(RP5ru.WmoInfo))
                {
                    List<RP5MeteostationInfo> meteost = engine.GetMeteostationsAtPoint(comboBoxPoint.SelectedItem as RP5ru.WmoInfo);
                    RP5MeteostationInfo meteostation;
                    if (meteost.Count == 0)
                        return;
                    if (meteost.Count == 1)
                        meteostation = meteost[0];
                    else
                    {
                        string text = "Ближайшие метеостанции к выбранной точке:\r\n\r\n";
                        text += meteost[0].Name + ", (" + meteost[0].OwnerDistance + " км)\r\n\r\n";
                        text += meteost[1].Name + ", (" + meteost[1].OwnerDistance + " км)\r\n\r\n";
                        FormChooseMeteostAirportDialog dlg = new FormChooseMeteostAirportDialog("Загрузка ряда с rp5.ru", text, meteost[0].Name, meteost[1].Name);
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                            meteostation = meteost[dlg.Result - 1];
                        else return;
                    }
                    this.selectedMeteostation = meteostation;
                }
                else
                    this.selectedMeteostation = comboBoxPoint.SelectedItem as RP5MeteostationInfo;

                //установка времени начала и конца наблюдений
                dateTimePickerFromDate.MinDate = selectedMeteostation.MonitoringFrom;
                dateTimePickerToDate.MaxDate = DateTime.Now;
                labelDateRange.Text = "Выберите диапазон дат: (дата начала наблюдений: " + selectedMeteostation.MonitoringFrom.ToString() + ")";

                //разблокировка элементов
                dateTimePickerFromDate.Enabled = true;
                dateTimePickerToDate.Enabled = true;
                buttonDownload.Enabled = true;
                linkLabelOpenNasa.Enabled = true;

                //запись координат
                labelCoordinates.Text = $"Координаты метеостанции: {selectedMeteostation.Position.ToString(4)}";
            }
            catch (WebException)
            {
                _ = MessageBox.Show(this, "Ошибка подключения, проверьте соединение с Интернет", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

        private void comboBoxPoint_DropDownClosed(object sender, EventArgs e)
        {
            this.toolTip1.Hide(comboBoxPoint);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void formLoadFromRP5_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.None)
                DialogResult = DialogResult.Cancel;
        }

        private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerFromDate.MaxDate = dateTimePickerToDate.Value;
            dateTimePickerToDate.MinDate = dateTimePickerFromDate.Value;
        }

        private void linkLabelShowOnMap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormShowMeteostationsMap fsmon = new FormShowMeteostationsMap(true);
            if (fsmon.ShowDialog(this) == DialogResult.OK)
            {
                this.selectedMeteostation = fsmon.Result;

                //установка времени начала и конца наблюдений
                dateTimePickerFromDate.MinDate = selectedMeteostation.MonitoringFrom;
                dateTimePickerToDate.MaxDate = DateTime.Now;
                labelDateRange.Text = "Выберите диапазон дат: (дата начала наблюдений: " + selectedMeteostation.MonitoringFrom.ToString() + ")";

                //разблокировка элементов
                dateTimePickerFromDate.Enabled = true;
                dateTimePickerToDate.Enabled = true;
                buttonDownload.Enabled = true;
                linkLabelOpenNasa.Enabled = true;

                comboBoxPoint.Items.Clear();
                comboBoxPoint.Items.Add(selectedMeteostation);
                comboBoxPoint.SelectedItem = selectedMeteostation;

                //запись координат
                labelCoordinates.Text = $"Координаты метеостанции: {selectedMeteostation.Position.ToString(4)}";
            }
        }

        private void linkLabelOpenNasa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormLoadFromNASA frm = new FormLoadFromNASA(selectedMeteostation.Position);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                RawRange res = frm.Result;
                TabPageExt tab = Program.winMain.mainTabControl.OpenNewTab(res, res.Name);
                tab.HasNotSavedChanges = true;
            }
            frm.Dispose();
        }
    }
}
