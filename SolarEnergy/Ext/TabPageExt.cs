using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.UI.Ext;
using System;
using System.Windows.Forms;

namespace WindEnergy.UI.Ext
{
    /// <summary>
    /// вкладка-документ
    /// </summary>
    public class TabPageExt : TabPage
    {
        /// <summary>
        /// ряд данных, для этой вкладки
        /// </summary>
        public DataRange DataRange { get; }

        /// <summary>
        /// создаёт новую вкладку с заданным рядом
        /// </summary>
        /// <param name="range"></param>
        /// <param name="text"></param>
        public TabPageExt(DataRange range, string text)
        {
            range = range ?? throw new ArgumentNullException(nameof(range));
            TextChanged += tabPageExt_TextChanged;
            DataRange = range;
            this.ToolTipText = string.IsNullOrWhiteSpace(range.FilePath) ? "" : range.FilePath;
            this.Text = text;

            //расстановка контролов
            //FlowLayoutPanel flp = new FlowLayoutPanel();
            //flp.FlowDirection = FlowDirection.TopDown;
            //flp.Dock = DockStyle.Fill;
            //flp.Parent = this;

            ZedGraphControlExt zgc = new ZedGraphControlExt(range);
            zgc.Name = "zgc";
            zgc.Parent = this;
            zgc.Dock = DockStyle.Fill;

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
            (this.Parent as TabControlExt).TabPages.Remove(this); //закрываем вкладку
            return false;
        }

    }
}
