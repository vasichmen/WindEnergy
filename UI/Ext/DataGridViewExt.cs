using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindEnergy.UI.Ext
{
    public class DataGridViewExt : DataGridView
    {
        public DataGridViewExt()
        {
            this.ColumnAdded += dataGridView_ColumnAdded;
            this.CellValidating += dataGridView_CellValidating;
            this.CellEndEdit += dataGridView_CellEndEdit;

        }

        /// <summary>
        /// установка флага несоранённых данных на вкладке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            TabPageExt tab = this.Parent as TabPageExt;
            tab.HasNotSavedChanges = true;
        }


        /// <summary>
        /// проверка значений в ячейке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //проверка значений double на соответствие типу
            if (e.ColumnIndex == 1 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                try
                {
                    double.Parse(e.FormattedValue as string);
                }
                catch (Exception exx)
                {
                    e.Cancel = true;
                    return;
                }
            }
            //проверка направления на допустимый диапазон
            if (e.ColumnIndex == 1)
            {
                double dir = double.Parse(e.FormattedValue as string);
                if (dir < 0 || dir >= 360)
                {
                    e.Cancel = true;
                    return;
                }
            }
            //проверка влажности на допустимый диапазон
            if (e.ColumnIndex == 5)
            {
                double dir = double.Parse(e.FormattedValue as string);
                if (dir < 0 || dir > 100)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        /// <summary>
        /// изменение типа столбцов при добавлении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            switch (e.Column.Name.ToLower())
            {
                case "date":
                    e.Column.HeaderText = "Дата наблюдения";
                    e.Column.CellTemplate = new DataGridViewCalendarCell();
                    break;
                case "direction":
                    e.Column.HeaderText = "Направление, °";
                    break;
                case "directionrhumb":
                    e.Column.HeaderText = "Румб";
                    e.Column.CellTemplate = new DataGridViewComboboxCell<WindDirections>();
                    break;
                case "speed":
                    e.Column.HeaderText = "Скорость, м/с";
                    break;
                case "temperature":
                    e.Column.HeaderText = "Температура, °С";
                    break;
                case "wetness":
                    e.Column.HeaderText = "Влажность, %";
                    break;
                default: throw new Exception("Для этой колонки нет названия");
            }
        }
    }
}
