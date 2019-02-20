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
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.UI.Dialogs;
using WindEnergy.UI.Ext;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// окно исправлений и восстановления ряда наблюдений
    /// </summary>
    public partial class FormCheckRepairRange : Form
    {
        private RawRange range = null;

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
        public FormCheckRepairRange(RawRange range)
        {
            InitializeComponent();
            comboBoxInterpolateMethod.Items.Clear();
            comboBoxInterpolateMethod.Items.AddRange(InterpolateMathods.Linear.GetItems().ToArray());
            comboBoxRepairInterval.Items.Clear();
            comboBoxRepairInterval.Items.AddRange(StandartIntervals.H1.GetItems().ToArray());
            comboBoxLimitsProvider.Items.Clear();
            comboBoxLimitsProvider.Items.AddRange(LimitsProviders.None.GetItems().ToArray());
            this.range = range;
        }

        #region восстановление ряда

        /// <summary>
        /// кнопка восстановить ряд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRepairRange_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                InterpolateMathods method = (InterpolateMathods)(new EnumTypeConverter<InterpolateMathods>().ConvertFrom(comboBoxInterpolateMethod.SelectedItem));
                StandartIntervals interval = (StandartIntervals)(new EnumTypeConverter<StandartIntervals>().ConvertFrom(comboBoxRepairInterval.SelectedItem));
                range = Restorer.ProcessRange(range, new RestorerParameters() { Interval = interval, Method = method });
                range.Name = "Восстановленный ряд до " + interval.Description();
            }
            catch (ApplicationException exc)
            {
                MessageBox.Show(this, exc.Message, "Восстановление ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        #endregion

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
                Cursor = Cursors.WaitCursor;
                //если выбран способ с помощью выбранных истоников ограничений
                if (radioButtonSelectLimitsProvider.Checked)
                {
                    LimitsProviders provider = (LimitsProviders)(new EnumTypeConverter<LimitsProviders>().ConvertFrom(comboBoxLimitsProvider.SelectedItem));
                    if (provider == LimitsProviders.None)
                    {
                        MessageBox.Show(this, "Необходимо выбрать истоник ограничений", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (provider == LimitsProviders.Manual)
                    {
                        MessageBox.Show(this, "Для ручного ввода ограничений выберите соответствующий пункт", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (checkPoint.IsEmpty)
                    {
                        MessageBox.Show(this, "Необходимо выбрать точку на карте", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    range = Checker.ProcessRange(range, new CheckerParameters(provider, checkPoint));
                    range.Name = "Исправленный ряд";
                    return;
                }

                //если выбран способ вручную вводить ограничения
                if (radioButtonEnterLimits.Checked)
                {
                    if (speedDiapasons == null)
                    {
                        MessageBox.Show(this, "Необходимо ввести ограничения для скоростей ветра", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (directionDiapasons == null)
                    {
                        MessageBox.Show(this, "Необходимо ввести ограничения для направлений ветра", "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    range = Checker.ProcessRange(range, new CheckerParameters(speedDiapasons, directionDiapasons));
                    range.Name = "Исправленный ряд";
                    return;
                }
            }
            catch (ApplicationException exc)
            {
                MessageBox.Show(this, exc.Message, "Проверка ряда", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// переключение выключателей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonLimits_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEnterLimits.Checked != radioButtonSelectLimitsProvider.Checked)
                radioButtonEnterLimits.Checked = !radioButtonSelectLimitsProvider.Checked;

            buttonSelectPoint.Enabled = radioButtonSelectLimitsProvider.Checked;
            comboBoxLimitsProvider.Enabled = radioButtonSelectLimitsProvider.Checked;
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
            FormSelectMapPointDialog spt = new FormSelectMapPointDialog("Выберите точку на карте", PointLatLng.Empty);
            if (spt.ShowDialog(this) == DialogResult.OK)
            {
                labelPointCoordinates.Text = spt.Result.ToString();
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
            comboBoxInterpolateMethod.SelectedItem = InterpolateMathods.Linear.Description();
            comboBoxRepairInterval.SelectedItem = StandartIntervals.H1.Description();
            comboBoxLimitsProvider.SelectedItem = LimitsProviders.StaticLimits.Description();

            radioButtonSelectLimitsProvider.Checked = true;
        }

        /// <summary>
        /// сохранение изменений и закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (range == null)
                DialogResult = DialogResult.Cancel;
            else
            {
                DialogResult = DialogResult.OK;
                Result = range;
            }
            Close();
        }

        /// <summary>
        /// отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}
