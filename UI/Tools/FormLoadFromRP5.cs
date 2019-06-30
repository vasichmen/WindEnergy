using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.UI.Dialogs;

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
        private MeteostationInfo selectedMeteostation = null;
        private RP5ru engine;

        public FormLoadFromRP5()
        {
            InitializeComponent();
            engine = new RP5ru(Vars.Options.CacheFolder + "\\rp5.ru");
            DialogResult = DialogResult.None;
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
            dateTimePickerFromDate.Value = DateTime.Now.AddDays(-2);
            dateTimePickerToDate.Value = DateTime.Now;
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
                MessageBox.Show(this, "Не выбрана метеостанция или координаты метеостанции недоступны", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Action<double> pcChange = new Action<double>((pc) =>
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
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

                buttonDownload.Enabled = false;
                new Task(() =>
                {
                    RawRange res = engine.GetRange(dateTimePickerFromDate.Value, dateTimePickerToDate.Value, selectedMeteostation, pcChange);

                    if (this.InvokeRequired)
                        this.Invoke(new Action(() =>
                        {
                            Result = res;
                            DialogResult = DialogResult.OK;
                            Close();

                        }));
                    else
                    {
                        Result = res;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }).Start();
            }
            catch (WebException ex)
            {
                buttonDownload.Enabled = true;
                MessageBox.Show(this, ex.Message + "\r\n" + ex.InnerException != null ? ex.InnerException.Message : "", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ApplicationException ae)
            {
                buttonDownload.Enabled = true;
                MessageBox.Show(this, ae.Message + "\r\n" + (ae.InnerException != null ? ae.InnerException.Message : "\r\n") + "\r\nПопробуйте выбрать меньший интервал времени", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        /// <summary>
        /// фильтрация по началу города
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxWMO_TextUpdate(object sender, EventArgs e)
        {
            string curTextBox = comboBoxPoint.Text.Trim();
            updateWMOListAsync(curTextBox);
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
                    Thread.Sleep(2000); //ждем 2 с

                    //получаем новый текст 
                    string curTextBox = "";
                    if (this.InvokeRequired)
                        this.Invoke(new Action(() => { curTextBox = comboBoxPoint.Text.Trim(); }));
                    else
                        curTextBox = comboBoxPoint.Text.Trim();

                    if (query != curTextBox) //если этот текст  изменился, то выходим
                        return;

                    selectedMeteostation = null;
                    results = engine.Search(query);
                    //обновление списка
                    if (this.InvokeRequired)
                        this.Invoke(updList, results);
                    else
                        updList.Invoke(results);

                });
            }
            catch (WebException)
            {
                MessageBox.Show(this, "Ошибка подключения, проверьте соединение с Интернет", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }catch (ApplicationException exc)
            {
                MessageBox.Show(this, exc.Message, "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Debug.WriteLine("updateList end");
        }

        /// <summary>
        /// проверка после выбора метеостанции 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxWMO_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //после того, как из поиска выбрана точка, надо проверить, есть ли на ней архив погоды. и вывести предупреждение, если ближайший архив далеко

            try
            {
                if (comboBoxPoint.SelectedItem == null)
                    return;
                List<MeteostationInfo> meteost = engine.GetMeteostationsAtPoint(comboBoxPoint.SelectedItem as RP5ru.WmoInfo);
                //выбор метеостанции
                MeteostationInfo meteostation;
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

                //установка времени начала и конца наблюдений
                dateTimePickerFromDate.MinDate = selectedMeteostation.MonitoringFrom;
                dateTimePickerToDate.MaxDate = DateTime.Now;
                labelDateRange.Text = "Выберите диапазон дат: (дата начала наблюдений: " + selectedMeteostation.MonitoringFrom.ToString() + ")";

                //разблокировка элементов
                dateTimePickerFromDate.Enabled = true;
                dateTimePickerToDate.Enabled = true;
                buttonDownload.Enabled = true;

            }
            catch (WebException)
            {
                MessageBox.Show(this, "Ошибка подключения, проверьте соединение с Интернет", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


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
    }
}
