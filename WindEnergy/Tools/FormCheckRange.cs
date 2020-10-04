using CommonLib.UITools;
using CommonLibLib.Data.Providers.InternetServices;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.UI.Dialogs;
using WindEnergy.UI.Properties;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Transformation.Check;
using WindLib;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// окно исправлений и восстановления ряда наблюдений
    /// </summary>
    public partial class FormCheckRange : Form
    {
        private RawRange range;

        /// <summary>
        /// точка на карте для которой происходит проверка ряда
        /// </summary>
        private PointLatLng checkPoint = PointLatLng.Empty;

        private List<Diapason<double>> speedDiapasons;
        private List<Diapason<double>> directionDiapasons;


        /// <summary>
        /// результат работы диалогового окна (новый ряд данных)
        /// </summary>
        public RawRange Result { get; private set; }

        /// <summary>
        /// открыть окно с заданным рядом
        /// </summary>
        /// <param name="range"></param>
        public FormCheckRange(RawRange range)
        {
            InitializeComponent();
            this.range = range;
        }

        #region проверка ряда

        /// <summary>
        /// кнопка проверить ряд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheckRange_Click(object sender, EventArgs e)
        {
            try
            {
                Action<double> pcAction = new Action<double>((percent) =>
                {
                    try
                    {
                        if (this.InvokeRequired)
                            _ = this.Invoke(new Action(() => { progressBar1.Value = (int)percent; }));
                        else
                            progressBar1.Value = (int)percent;
                    }
                    catch (Exception) { }
                });

                Cursor = Cursors.WaitCursor;
                new Task(() =>
                {
                    //если выбран способ с помощью выбранных истоников ограничений
                    if (radioButtonSelectLimitsProvider.Checked)
                    {
                        LimitsProviders provider = radioButtonSelectLimitsProvider.Checked ? LimitsProviders.StaticLimits : LimitsProviders.Manual;
                        int error = (int)this.Invoke(new Func<int>(() =>
                         {
                             int er = 0;
                             if (provider == LimitsProviders.None)
                             {
                                 _ = MessageBox.Show(this, "Необходимо выбрать истоник ограничений", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                 er = 1;
                             }
                             if (provider == LimitsProviders.Manual)
                             {
                                 _ = MessageBox.Show(this, "Для ручного ввода ограничений выберите соответствующий пункт", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                 er = 1;
                             }
                             if (checkPoint.IsEmpty)
                             {
                                 _ = MessageBox.Show(this, "Необходимо выбрать точку на карте", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                 er = 1;
                             }
                             if (er == 1)
                                 this.Cursor = Cursors.Arrow;
                             return er;
                         }));

                        if (error == 1) //если произошла ошибка, то выход
                            return;

                        range = new Checker().ProcessRange(range, new CheckerParameters(provider, checkPoint), out CheckerInfo stats, pcAction);
                        _ = this.Invoke(new Action(() =>
                           {
                               _ = MessageBox.Show(this, $"Ряд исправлен, результаты:\r\nНаблюдений в исходном ряде: {stats.Total}\r\nПовторов дат: {stats.DateRepeats}\r\nПревышений диапазонов: {stats.OverLimits}\r\nНулевая скорость с направлением: {stats.OtherErrors}\r\nОсталось наблюдений: {stats.Remain}", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               if (range == null)
                                   DialogResult = DialogResult.Cancel;
                               else
                               {
                                   DialogResult = DialogResult.OK;
                                   Result = range;
                               }
                               Cursor = Cursors.Arrow;
                               Close();
                           }));
                    }

                    //если выбран способ вручную вводить ограничения
                    if (radioButtonEnterLimits.Checked)
                    {
                        _ = this.Invoke(new Action(() =>
                          {
                              if (speedDiapasons == null)
                              {
                                  _ = MessageBox.Show(this, "Необходимо ввести ограничения для скоростей ветра", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                  return;
                              }
                              if (directionDiapasons == null)
                              {
                                  _ = MessageBox.Show(this, "Необходимо ввести ограничения для направлений ветра", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                  return;
                              }
                          }));
                        range = new Checker().ProcessRange(range, new CheckerParameters(speedDiapasons, directionDiapasons), out CheckerInfo stats, pcAction);
                        range.Name = "Исправленный ряд";
                        _ = this.Invoke(new Action(() =>
                          {
                              _ = MessageBox.Show(this, $"Ряд исправлен, результаты:\r\nНаблюдений в исходном ряде: {stats.Total}\r\nПовторов дат: {stats.DateRepeats}\r\nПревышений диапазонов: {stats.OverLimits}\r\nНулевая скорость с направлением: {stats.OtherErrors}\r\nОсталось наблюдений: {stats.Remain}", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Information);
                              if (range == null)
                                  DialogResult = DialogResult.Cancel;
                              else
                              {
                                  DialogResult = DialogResult.OK;
                                  Result = range;
                              }
                              Cursor = Cursors.Arrow;
                              Close();
                          }));
                    }


                }).Start();
            }
            catch (ApplicationException exc)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, exc.Message, "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// переключение выключателей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonLimits_CheckedChanged(object sender, EventArgs e)
        {
            buttonSelectPoint.Enabled = radioButtonSelectLimitsProvider.Checked;
            buttonEnterDirectionDiapason.Enabled = radioButtonEnterLimits.Checked;
            buttonEnterSpeedDiapason.Enabled = radioButtonEnterLimits.Checked;
        }

        /// <summary>
        /// выбор точки на карте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectPoint_Click(object sender, EventArgs e)
        {
            FormSelectMapPointDialog spt = new FormSelectMapPointDialog("Выберите точку на карте", PointLatLng.Empty, Vars.Options.CacheFolder, Resources.rp5_marker, Vars.Options.MapProvider);
            if (spt.ShowDialog(this) == DialogResult.OK)
            {
                labelPointCoordinates.Text = $"Широта: {spt.Result.Lat:0.000} Долгота: {spt.Result.Lng:0.000}";
                loadAddressAsync(spt.Result);
                toolTip1.SetToolTip(labelPointAddress, labelPointAddress.Text);
                checkPoint = spt.Result;
            }
        }

        /// <summary>
        /// ввод ограничений скоростей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEnterSpeedDiapason_Click(object sender, EventArgs e)
        {
            FormEditDiapasons fed = new FormEditDiapasons(speedDiapasons, "Редактирование диапазонов скоростей ветра");
            if (fed.ShowDialog(this) == DialogResult.OK)
            {
                this.speedDiapasons = fed.Result;
                labelspeedDiap.Text = "Выбрано " + speedDiapasons.Count + " диапазонов";
            }
        }

        /// <summary>
        /// ввод ограничений направлений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEnterDirectionDiapason_Click(object sender, EventArgs e)
        {
            FormEditDiapasons fed = new FormEditDiapasons(directionDiapasons, "Редактирование диапазонов направлений ветра");
            if (fed.ShowDialog(this) == DialogResult.OK)
            {
                this.directionDiapasons = fed.Result;
                labelspeedDiap.Text = "Выбрано " + directionDiapasons.Count + " диапазонов";
            }
        }

        #endregion

        /// <summary>
        /// открытие окна и заполнение элементов в combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formCheckRepairRange_Shown(object sender, EventArgs e)
        {
            checkPoint = range.Position;
            loadAddressAsync(checkPoint);
            labelPointCoordinates.Text = $"Широта: {checkPoint.Lat:0.000} Долгота: {checkPoint.Lng:0.000}";
            radioButtonSelectLimitsProvider.Checked = true;
        }

        /// <summary>
        /// Асинхронно загружает адрес точки ряда
        /// </summary>
        private async void loadAddressAsync(PointLatLng point)
        {
            labelPointAddress.Text = "Поиск адреса...";
            await Task.Run(() =>
            {
                string adr = "";
                try
                {
                    adr = new Arcgis(Vars.Options.CacheFolder + "\\arcgis").GetAddress(point);
                }
                catch (Exception)
                {
                    adr = "Не удалось найти адрес";
                }

                _ = this.Invoke(new Action(() =>
                  {
                      labelPointAddress.Text = adr;
                  }));

            }).ConfigureAwait(false);
        }

        private void labelPointCoordinates_TextChanged(object sender, EventArgs e)
        {
            string n = ((Label)sender).Text;
            new ToolTip().SetToolTip(sender as Label, n);
        }
    }
}
