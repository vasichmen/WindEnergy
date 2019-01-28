using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Data;

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
        public RawRange Range { get; }

        /// <summary>
        /// создаёт новую вкладку с заданным рядом
        /// </summary>
        /// <param name="range"></param>
        /// <param name="text"></param>
        public TabPageExt(RawRange range, string text)
        {
            Range = range;
            this.ToolTipText = string.IsNullOrWhiteSpace(range.FilePath) ? "" : range.FilePath;
            this.Text = text;
            DataGridViewExt ndgv = new DataGridViewExt();
            ndgv.Dock = DockStyle.Fill;
            ndgv.DataSource = range;
            ndgv.Parent = this;
        }
    }
}
