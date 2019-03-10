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
            TextChanged += tabPageExt_TextChanged;
            Range = range;
            this.ToolTipText = string.IsNullOrWhiteSpace(range.FilePath) ? "" : range.FilePath;
            this.Text = text;
            DataGridViewExt ndgv = new DataGridViewExt();
            ndgv.ColumnAdded += Program.winMain.DataGridView_ColumnAdded;
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
    }
}
