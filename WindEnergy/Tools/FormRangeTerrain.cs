﻿using CommonLib;
using CommonLib.Classes;
using CommonLib.UITools;
using GMap.NET;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using WindEnergy.UI.Dialogs;
using WindEnergy.UI.Properties;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Transformation.Terrain;
using WindLib;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// окно пересчета скорости ветра в точку ВЭС
    /// </summary>
    public partial class FormRangeTerrain : Form
    {
        private RawRange range;
        private FlugerMeteostationInfo flugerMeteostation = null;
        private Dictionary<WindDirections8, double> msClasses = null;
        private Dictionary<WindDirections8, double> pointClasses = null;
        private PointLatLng pointCoordinates = PointLatLng.Empty;
        private TerrainType terrainType = TerrainType.Macro;
        private AtmosphereStratification stratification;
        private MesoclimateItemInfo mesoclimateCoeff = null;
        private MicroclimateItemInfo microclimateCoeff = null;

        /// <summary>
        /// результат работы диалогового окна (новый ряд данных)
        /// </summary>
        public RawRange Result { get; private set; }


        public FormRangeTerrain(RawRange range)
        {
            InitializeComponent();
            this.range = range;
            scintillaRecommendations.StyleResetDefault();
            scintillaRecommendations.Styles[Style.Default].Size = 11;
            scintillaRecommendations.StyleClearAll();


        }

        private void FormRangeTerrain_Load(object sender, EventArgs e)
        {
            //заполнение списков типов рельефа
            foreach (MesoclimateItemInfo meso in Vars.MesoclimateTableDatabase.List)
                _ = comboBoxMesoclimate.Items.Add(meso.Name);
            foreach (MicroclimateItemInfo micro in Vars.MicroclimateTableDatabase.List)
                _ = comboBoxMicroclimate.Items.Add(micro.Name);

            //установка тегов для radio типов стратификации
            radioButtonStable.Tag = AtmosphereStratification.Stable;
            radioButtonUnstable.Tag = AtmosphereStratification.Unstable;

            //попытка найти классы открытости для точки ряда
            tryGetMSClasses();
            radioButtonTerrain_CheckedChanged(null, null); //вызов setStatuses();  внутри
        }

        #region Первый тип рельефа

        private void buttonSelectMSCoordinates_Click(object sender, EventArgs e)
        {
            FormSelectMapPointDialog fsp = new FormSelectMapPointDialog("Выберите точку метеостанции", range.Position, Vars.Options.CacheFolder, Resources.rp5_marker, Vars.Options.MapProvider);
            if (fsp.ShowDialog(this) == DialogResult.OK)
                range.Position = fsp.Result;
            tryGetMSClasses(); //при изменении точки МС заново ищем классы открытости из Классы открытости МС
            setStatuses();
        }

        private void buttonSelectPointCoordinates_Click(object sender, EventArgs e)
        {
            FormSelectMapPointDialog fsp = new FormSelectMapPointDialog("Выберите точку ВЭС", pointCoordinates, Vars.Options.CacheFolder, Resources.rp5_marker, Vars.Options.MapProvider);
            if (fsp.ShowDialog(this) == DialogResult.OK)
                pointCoordinates = fsp.Result;
            setStatuses();
        }

        private void buttonEnterPointClasses_Click(object sender, EventArgs e)
        {
            FormRhumbsValuesDialogs frv = new FormRhumbsValuesDialogs(pointClasses, "Введите классы открытости точки ВЭС");
            if (frv.ShowDialog(this) == DialogResult.OK)
                pointClasses = frv.Result;
            setStatuses();
        }

        private void buttonEnterMSClasses_Click(object sender, EventArgs e)
        {
            FormRhumbsValuesDialogs frv = new FormRhumbsValuesDialogs(msClasses, "Введите классы открытости точки метеостанции");
            if (frv.ShowDialog(this) == DialogResult.OK)
                msClasses = frv.Result;
            setStatuses();
        }


        /// <summary>
        /// попробовать получить коэффициенты kм для координат метеостанции (из range.Position)
        /// </summary>
        private void tryGetMSClasses()
        {
            FlugerMeteostationInfo info = Vars.FlugerMeteostations.GetNearestMS(range.Position, 100 * 1000);
            if (info != null)
            {
                flugerMeteostation = info;
                msClasses = info.KM;
            }
        }


        #endregion

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            Action<int> action = new Action<int>((percent) =>
            {
                try
                {
                    if (this.InvokeRequired)
                        _ = Invoke(new Action(() => { progressBar1.Value = percent; }));
                    else
                        progressBar1.Value = percent;
                }
                catch (Exception) { }
            });

            Action<RawRange, FlugerMeteostationInfo> actionAfter = new Action<RawRange, FlugerMeteostationInfo>((rawRange, fluger_MS) =>
            {
                _ = this.Invoke(new Action(() =>
                {
                    string text = fluger_MS != null ? $"\r\nпо данным МС {fluger_MS.Name} ({fluger_MS.Position}) с учетом изменения классов открытости площадок" : "";
                    _ = MessageBox.Show(this, $"Скорости ветра пересчитаны в точку ВЭС ({pointCoordinates.ToString(3)}){((!string.IsNullOrWhiteSpace(text)) ? text : "")}", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    rawRange.Name = $"Ряд в точке {pointCoordinates.ToString(3)}";
                    if (rawRange == null)
                        DialogResult = DialogResult.Cancel;
                    else
                    {
                        DialogResult = DialogResult.OK;
                        Result = rawRange;
                    }
                    Cursor = Cursors.Arrow;
                    Result = rawRange;
                    Close();
                }));
            });

            try
            {
                if (range.Position.IsEmpty)
                {
                    FormSelectMapPointDialog fsp = new FormSelectMapPointDialog("Выберите координаты ряда " + range.Name, PointLatLng.Empty, Vars.Options.CacheFolder, Resources.rp5_marker, Vars.Options.MapProvider);
                    if (fsp.ShowDialog(this) == DialogResult.OK)
                        range.Position = fsp.Result;
                    else
                        return;
                }

                RangeTerrain.ProcessRange(range, new TerrainParameters()
                {
                    MSClasses = msClasses,
                    PointClasses = pointClasses,
                    TerrainType = terrainType,
                    PointCoordinates = pointCoordinates,
                    FlugerMeteostation = flugerMeteostation,
                    AtmosphereStratification = stratification,
                    MicroclimateCoefficient = microclimateCoeff,
                    MesoclimateCoefficient = mesoclimateCoeff,
                }, action, actionAfter); ;

            }
            catch (WebException exc)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
            catch (WindEnergyException wex)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, wex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
            catch (ApplicationException exc)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, exc.Message + "\r\nПопробуйте уменьшить длину ряда", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                _ = MessageBox.Show(this, "Произошла ошибка:\r\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonTerrain_CheckedChanged(object sender, EventArgs e)
        {
            terrainType = radioButtonTerrainMacro.Checked ? TerrainType.Macro : (radioButtonTerrainMeso.Checked ? TerrainType.Meso : TerrainType.Micro);

            //эти относятся только к макрорельефу
            labelPointClassesStatus.Enabled = terrainType == TerrainType.Macro;
            buttonEnterPointClasses.Enabled = terrainType == TerrainType.Macro;
            buttonEnterMSClasses.Enabled = terrainType == TerrainType.Macro;

            //эти относятся только с мезорельефу
            labelMesoclimateStatus.Enabled = terrainType == TerrainType.Meso;
            comboBoxMesoclimate.Enabled = terrainType == TerrainType.Meso;


            //эти относятся только к микрорельефу
            labelMicroclimateStatus.Enabled = terrainType == TerrainType.Micro;
            comboBoxMicroclimate.Enabled = terrainType == TerrainType.Micro;
            foreach (Control c in groupBoxStratification.Controls)
                c.Enabled = terrainType == TerrainType.Micro;


            //перерасчет статусов
            setStatuses();
        }


        /// <summary>
        /// установка текста и цвета статусов около кнопок
        /// </summary>
        private void setStatuses()
        {
            setRecommendation();

            //общая часть
            labelMSClassesStatus.Text = msClasses == null || msClasses.Count == 0 ? "Не выбраны" : (flugerMeteostation != null ? flugerMeteostation.Name + $" ({flugerMeteostation.Position.ToString(3)})" : "Заданы вручную");
            labelPointClassesStatus.Text = pointClasses == null || pointClasses.Count == 0 ? "Не выбрано" : "Заданы вручную";
            labelMSCoordinatesStatus.Text = range.Position.IsEmpty ? "Не выбрана" : range.Position.ToString(2);
            labelPointCoordinatesStatus.Text = pointCoordinates.IsEmpty ? "Не выбрана" : pointCoordinates.ToString(2);


            //установка цветов и активности кнопки
            buttonCalculate.Enabled = true;
            labelMSCoordinatesStatus.ForeColor = range.Position.IsEmpty ? Color.Red : Color.Green;
            labelPointCoordinatesStatus.ForeColor = pointCoordinates.IsEmpty ? Color.Red : Color.Green;

            buttonCalculate.Enabled &= labelMSCoordinatesStatus.ForeColor == Color.Green && labelPointCoordinatesStatus.ForeColor == Color.Green;

            switch (terrainType)
            {
                case TerrainType.Macro:
                    labelMSClassesStatus.ForeColor = msClasses == null || msClasses.Count == 0 ? Color.Red : Color.Green;
                    labelPointClassesStatus.ForeColor = pointClasses == null || pointClasses.Count == 0 ? Color.Red : Color.Green;
                    buttonCalculate.Enabled &= labelMSClassesStatus.ForeColor == Color.Green && labelPointClassesStatus.ForeColor == Color.Green;
                    break;
                case TerrainType.Meso:
                    labelMesoclimateStatus.ForeColor = mesoclimateCoeff == null ? Color.Red : Color.Green;
                    buttonCalculate.Enabled &= labelMesoclimateStatus.ForeColor == Color.Green;
                    break;
                case TerrainType.Micro:
                    labelMicroclimateStatus.ForeColor = microclimateCoeff == null ? Color.Red : Color.Green;
                    buttonCalculate.Enabled &= labelMicroclimateStatus.ForeColor == Color.Green;
                    break;
            }
        }

        /// <summary>
        /// установка текста рекомендации
        /// </summary>
        private void setRecommendation()
        {
            try
            {
                scintillaRecommendations.ReadOnly = false;
                scintillaRecommendations.Text = "";
                Dictionary<string, Color> textCollection = RangeTerrain.GetRecommendation(range, pointCoordinates, Vars.ETOPOdatabase);
                int i = 9, start = 0;
                foreach (string text in textCollection.Keys)
                {
                    scintillaRecommendations.Text += text + "\n";

                    Indicator indic = scintillaRecommendations.Indicators[i++];
                    indic.Style = IndicatorStyle.TextFore;
                    indic.ForeColor = textCollection[text];
                    indic.Alpha = 100;
                    scintillaRecommendations.IndicatorCurrent = indic.Index;
                    scintillaRecommendations.IndicatorFillRange(start, text.Length);
                    start += text.Length + 1;
                }
            }
            catch (WindEnergyException ex)
            {
                Indicator indic = scintillaRecommendations.Indicators[16];
                indic.Style = IndicatorStyle.TextFore;
                indic.ForeColor = Color.Red;
                indic.Alpha = 100;
                scintillaRecommendations.IndicatorCurrent = indic.Index;
                scintillaRecommendations.Text = ex.Message;
                scintillaRecommendations.IndicatorFillRange(0, ex.Message.Length);
            }
            finally
            {
                scintillaRecommendations.ReadOnly = true;
            }
        }

        private void radioButtonStratification_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
                stratification = (AtmosphereStratification)radio.Tag;
            setStatuses();
        }

        /// <summary>
        /// отрисовка элемента списка и вывод подсказки, если он выбран
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1) return;
            ComboBox combobox = sender as ComboBox;

            string text = combobox.GetItemText(combobox.Items[e.Index]);
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(text, e.Font, br, e.Bounds);
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                if (text.Length > Math.Round(Convert.ToDecimal(combobox.Size.Width / 8)))
                    this.toolTip1.Show(text, combobox, e.Bounds.Right, e.Bounds.Bottom);
            }
            else
                this.toolTip1.Hide(combobox);
            e.DrawFocusRectangle();
        }

        private void comboBoxMesoclimate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string key = comboBoxMesoclimate.SelectedItem as string;
            mesoclimateCoeff = Vars.MesoclimateTableDatabase[key];
            setStatuses();
        }

        private void comboBoxMicroclimate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string key = comboBoxMicroclimate.SelectedItem as string;
            microclimateCoeff = Vars.MicroclimateTableDatabase[key];
            setStatuses();
        }
    }
}
