using CommonLib;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.UI.Dialogs;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
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
            //попытка найти классы открытости для точки ряда
            tryGetMSClasses();
            setStatuses();
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

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonTerrain_CheckedChanged(object sender, EventArgs e)
        {
            terrainType = radioButtonTerrainFirst.Checked ? TerrainType.First : TerrainType.Second;

            foreach (Control control in groupBoxTerrainFirst.Controls)
                control.Enabled = terrainType == TerrainType.First;
            foreach (Control control in groupBoxTerrainSecond.Controls)
                control.Enabled = terrainType == TerrainType.Second;
            setStatuses();
        }


        /// <summary>
        /// установка текста и цвета статусов около кнопок
        /// </summary>
        private void setStatuses()
        {
            switch (terrainType)
            {
                case TerrainType.First:
                    //цвет
                    labelMSClassesStatus.ForeColor = msClasses == null || msClasses.Count == 0 ? Color.Red : Color.Green;
                    labelPointClassesStatus.ForeColor = pointClasses == null || pointClasses.Count == 0 ? Color.Red : Color.Green;
                    labelMSCoordinatesStatus.ForeColor = range.Position.IsEmpty ? Color.Red : Color.Green;
                    labelPointCoordinatesStatus.ForeColor = pointCoordinates.IsEmpty ? Color.Red : Color.Green;

                    //текст
                    labelMSClassesStatus.Text = msClasses == null || msClasses.Count == 0 ? "Не выбраны" : (flugerMeteostation != null ? flugerMeteostation.Name + $" ({flugerMeteostation.Position.ToString(3)})" : "Заданы вручную");
                    labelPointClassesStatus.Text = pointClasses == null || pointClasses.Count == 0 ? "Не выбрано" : "Заданы вручную";
                    labelMSCoordinatesStatus.Text = range.Position.IsEmpty ? "Не выбрана" : range.Position.ToString(2);
                    labelPointCoordinatesStatus.Text = pointCoordinates.IsEmpty ? "Не выбрана" : pointCoordinates.ToString(2);

                    //кнопка расчета
                    buttonCalculate.Enabled = labelMSClassesStatus.ForeColor == Color.Green &&
                                              labelPointClassesStatus.ForeColor == Color.Green &&
                                              labelMSCoordinatesStatus.ForeColor == Color.Green &&
                                              labelPointCoordinatesStatus.ForeColor == Color.Green;
                    break;
                case TerrainType.Second:
                    break;
                default: throw new Exception("Этот тип не реализован");
            }

        }

    }
}
