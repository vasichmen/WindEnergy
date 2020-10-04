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
            if (!(sender as DataGridViewExt).CausesValidation)
                return;

            this.Rows[e.RowIndex].ErrorText = "";
            //проверка значений double на соответствие типу
            if (e.ColumnIndex == 0 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 5)
            {
                string val = e.FormattedValue as string;
                if (!double.TryParse(val.Replace('.', Constants.DecimalSeparator), out double d))
                {
                    this.Rows[e.RowIndex].ErrorText = $"Не удалось распознать число: {e.FormattedValue}";
                    e.Cancel = true;
                    return;
                }
            }
            //проверка направления на допустимый диапазон
            if (e.ColumnIndex == 1)
            {
                string val = e.FormattedValue as string;
                bool fl = double.TryParse(val.Replace('.', Constants.DecimalSeparator), out double dir);
                if (dir < 0 || dir >= 360)
                {
                    this.Rows[e.RowIndex].ErrorText = "Направление должно быть в диапазоне от 0 до 360";
                    e.Cancel = true;
                    return;
                }
            }
            //проверка влажности на допустимый диапазон
            if (e.ColumnIndex == 5)
            {
                string val = e.FormattedValue as string;
                bool fl = double.TryParse(val.Replace('.', Constants.DecimalSeparator), out double dir);
                if (dir < 0 || dir >= 100)
                {
                    this.Rows[e.RowIndex].ErrorText = "Влажность должна быть в диапазоне от 0 до 100%";
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
