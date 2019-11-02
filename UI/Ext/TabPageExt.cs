using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Data;

namespace WindEnergy.UI.Ext
{
    /// <summary>
    /// вкладка-документ
    /// </summary>
    public class TabPageExt : TabPage
    {
        public DataGridViewExt DataGrid { get; }

        /// <summary>
        /// ряд данных, для этой вкладки
        /// </summary>
        public RawRange Range { get; }

        /// <summary>
        /// если итина, то на этой вкладке есть несохраненные изменения
        /// </summary>
        public bool HasNotSavedChanges { get; internal set; }

        /// <summary>
        /// создаёт новую вкладку с заданным рядом
        /// </summary>
        /// <param name="range"></param>
        /// <param name="text"></param>
        public TabPageExt(RawRange range, string text)
        {
            range = range ?? throw new ArgumentNullException(nameof(range));
            TextChanged += tabPageExt_TextChanged;
            Range = range;
            this.ToolTipText = string.IsNullOrWhiteSpace(range.FilePath) ? "" : range.FilePath;
            this.Text = text;
            DataGridViewExt ndgv = new DataGridViewExt();
            ndgv.ColumnAdded += DataGridView_ColumnAdded;
            ndgv.Dock = DockStyle.Fill;
            ndgv.Parent = this;
            ndgv.DataSource = range;
            HasNotSavedChanges = false;
            DataGrid = ndgv;
        }


        /// <summary>
        /// добавление пустого места после названия вкладки, чтоб поместилась кнопка закрытия вкладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPageExt_TextChanged(object sender, EventArgs e)
        {
            //пустое место, чтоб поместилась кнопка закрытия вкладки
            if (!this.Text.EndsWith("     "))
                this.Text += "     ";
        }

        /// <summary>
        /// закрыть эту вкладку. Если есть несохранённые изменения, то будет выдан подтверждающий диалог. Возвращает true, если надо отменить закрытие
        /// </summary>
        internal bool ClosePage()
        {
            if (this.HasNotSavedChanges)
            {
                DialogResult result = MessageBox.Show(Parent.Parent, "Сохранить изменения в файле " + this.Range.Name + " ?", "Закрытие файла", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                switch (result)
                {
                    case DialogResult.No:
                        this.CausesValidation = false;
                        this.DataGrid.CausesValidation = false;
                        this.DataGrid.RefreshEdit();
                        this.DataGrid.EndEdit();
                        (this.Parent as TabControlExt).TabPages.Remove(this); //закрываем вкладку без сохранения
                        return false;
                    case DialogResult.Yes:
                        (Parent.Parent.Parent as FormMain).mainHelper.Save(this);
                        (this.Parent as TabControlExt).TabPages.Remove(this); //закрываем вкладку после сохранения
                        return false;
                    case DialogResult.Cancel:
                        return true;
                    default: throw new Exception();
                }
            }
            else
            {
                (this.Parent as TabControlExt).TabPages.Remove(this); //закрываем вкладку
                return false;
            }

        }

        /// <summary>
        /// изменение типа столбцов при добавлении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e = e ?? throw new ArgumentNullException(nameof(e));
            switch (e.Column.Name.ToLower())
            {
                case "date":
                    e.Column.HeaderText = "Дата наблюдения";
                    e.Column.Width = 130;
                    e.Column.CellTemplate = new DataGridViewCalendarCell();
                    break;
                case "direction":
                    e.Column.HeaderText = "Направление, °";
                    e.Column.DefaultCellStyle.Format = "n2";
                    break;
                case "directionrhumb":
                    e.Column.HeaderText = "Румб";
                    e.Column.Width = 55;
                    e.Column.CellTemplate = new DataGridViewComboboxCell<WindDirections>();
                    break;
                case "speed":
                    e.Column.HeaderText = "Скорость, м/с";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "temperature":
                    e.Column.HeaderText = "Температура, °С";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "wetness":
                    e.Column.HeaderText = "Влажность, %";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "pressure":
                    e.Column.HeaderText = "Давление, мм рт. ст.";
                    e.Column.Width = 130;
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;

                //удаляемые колонки пишем тут:
                case "dateargument":
                    e.Column.DataGridView.Columns.Remove(e.Column);
                    break;
                default: throw new Exception("Для этой колонки нет названия");
            }
        }
    }
}
