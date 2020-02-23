using CommonLib;
using CommonLib.Classes;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.UI.Dialogs;
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
        RawRange range;
        FlugerMeteostationInfo flugerMeteostation = null;
        Dictionary<WindDirections8, double> msClasses = null;
        Dictionary<WindDirections8, double> pointClasses = null;
        PointLatLng pointCoordinates = PointLatLng.Empty;
        TerrainType terrainType = TerrainType.First;
        AtmosphereStratification stratification;
        MesoclimateItemInfo mesoclimateCoeff = null;
        MicroclimateItemInfo microclimateCoeff = null;
        WaterDistanceType waterDistance = WaterDistanceType.Undefined;

        /// <summary>
        /// результат работы диалогового окна (новый ряд данных)
        /// </summary>
        public RawRange Result { get; private set; }


        public FormRangeTerrain(RawRange range)
        {
            InitializeComponent();
            this.range = range;
        }

        private void FormRangeTerrain_Load(object sender, EventArgs e)
        {
            //заполнение списков типов рельефа
            foreach (MesoclimateItemInfo meso in Vars.MesoclimateTableDatabase.List)
                comboBoxMesoclimate.Items.Add(meso.Name);
            foreach (MicroclimateItemInfo micro in Vars.MicroclimateTableDatabase.List)
                comboBoxMicroclimate.Items.Add(micro.Name);

            comboBoxWaterDistance.Items.AddRange(WaterDistanceType.FarFromWater.GetItems().ToArray());
            comboBoxWaterDistance.SelectedItem = WaterDistanceType.Undefined.Description();

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
            FormSelectMapPointDialog fsp = new FormSelectMapPointDialog("Выберите точку метеостанции", range.Position);
            if (fsp.ShowDialog(this) == DialogResult.OK)
                range.Position = fsp.Result;
            tryGetMSClasses(); //при изменении точки МС заново ищем классы открытости из флюгера
            setStatuses();
        }

        private void buttonSelectPointCoordinates_Click(object sender, EventArgs e)
        {
            FormSelectMapPointDialog fsp = new FormSelectMapPointDialog("Выберите точку ВЭС", pointCoordinates);
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
                    string text = fluger_MS != null ? $"На основе данных МС {fluger_MS.Name} ({fluger_MS.Position.ToString()})\r\nИз СБД Флюгер" : "";
                    rawRange.Name = $"Ряд в точке {pointCoordinates.ToString(3)}";
                    _ = MessageBox.Show(this, $"Скорости ветра пересчитаны в точку {pointCoordinates.ToString(3)} м\r\n{((!string.IsNullOrWhiteSpace(text)) ? text : "")}", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    FormSelectMapPointDialog fsp = new FormSelectMapPointDialog("Выберите координаты ряда " + range.Name, PointLatLng.Empty);
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
                    WaterType = waterDistance
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
            terrainType = radioButtonTerrainFirst.Checked ? TerrainType.First : TerrainType.Second;

            //эти относятся только к первому типу рельефа
            labelPointClassesStatus.Enabled = terrainType == TerrainType.First;
            buttonEnterPointClasses.Enabled = terrainType == TerrainType.First;

            //эти относятся только ко второму типу рельефа
            foreach (Control control in groupBoxTerrainSecond.Controls)
                control.Enabled = terrainType == TerrainType.Second;

            //перерасчет статусов
            setStatuses();
        }


        /// <summary>
        /// установка текста и цвета статусов около кнопок
        /// </summary>
        private void setStatuses()
        {
            //цвет
            labelMSClassesStatus.ForeColor = msClasses == null || msClasses.Count == 0 ? Color.Red : Color.Green;

            labelMSCoordinatesStatus.ForeColor = range.Position.IsEmpty ? Color.Red : Color.Green;
            labelPointCoordinatesStatus.ForeColor = pointCoordinates.IsEmpty ? Color.Red : Color.Green;

            if (terrainType == TerrainType.Second) //только для второго типа рельефа
            {
                labelMesoclimateStatus.ForeColor = mesoclimateCoeff == null ? Color.Red : Color.Green;
                labelWaterDistanceType.ForeColor = waterDistance == WaterDistanceType.Undefined?Color.Red : Color.Green;
            }

            if (terrainType == TerrainType.First) //только для первого типа рельефа
                labelPointClassesStatus.ForeColor = pointClasses == null || pointClasses.Count == 0 ? Color.Red : Color.Green;

            //текст
            labelMSClassesStatus.Text = msClasses == null || msClasses.Count == 0 ? "Не выбраны" : (flugerMeteostation != null ? flugerMeteostation.Name + $" ({flugerMeteostation.Position.ToString(3)})" : "Заданы вручную");
            labelPointClassesStatus.Text = pointClasses == null || pointClasses.Count == 0 ? "Не выбрано" : "Заданы вручную";
            labelMSCoordinatesStatus.Text = range.Position.IsEmpty ? "Не выбрана" : range.Position.ToString(2);
            labelPointCoordinatesStatus.Text = pointCoordinates.IsEmpty ? "Не выбрана" : pointCoordinates.ToString(2);

            //кнопка расчета
            buttonCalculate.Enabled = labelMSClassesStatus.ForeColor == Color.Green &&
                                      labelMSCoordinatesStatus.ForeColor == Color.Green &&
                                      labelPointCoordinatesStatus.ForeColor == Color.Green;


            if (terrainType == TerrainType.Second)//только для второго типа рельефа
            { 
                buttonCalculate.Enabled &= labelMesoclimateStatus.ForeColor == Color.Green;
                buttonCalculate.Enabled &= labelWaterDistanceType.ForeColor == Color.Green;
            }
            if(terrainType == TerrainType.First) //только для первого рельефа
                buttonCalculate.Enabled &= labelPointClassesStatus.ForeColor == Color.Green;

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
        void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
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

        private void comboBoxWaterDistance_SelectionChangeCommitted(object sender, EventArgs e)
        {
            waterDistance = (WaterDistanceType)(new EnumTypeConverter<WaterDistanceType>().ConvertFrom(comboBoxWaterDistance.SelectedItem));
            setStatuses();
        }
    }
}
