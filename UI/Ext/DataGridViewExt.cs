using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindEnergy.UI.Ext
{
    /// <summary>
    /// расширенный вариант DataGridView
    /// </summary>
    public class DataGridViewExt : DataGridView
    {
        public DataGridViewExt()
        {
            this.CellValidating += dataGridView_CellValidating;
            this.CellEndEdit += dataGridView_CellEndEdit;
            this.UserDeletedRow += dataGridView_UserDeletedRow;
        }

        private void dataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            performUpdate();
        }

        /// <summary>
        /// установка флага несоранённых данных на вкладке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            performUpdate();
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
                catch (Exception)
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
        /// принудительное обновление документа
        /// </summary>
        private void performUpdate()
        {
            TabPageExt tab = this.Parent as TabPageExt;
            tab.HasNotSavedChanges = true;
            tab.Range.PerformRefreshQuality();
            Program.winMain.mainHelper.RefreshStatusBar();
        }
    }
}
