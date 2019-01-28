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
    /// контроллер вкладок документов
    /// </summary>
    public class TabControlExt : TabControl
    {
        public TabControlExt()
        {
            ShowToolTips = true;
        }

        /// <summary>
        /// открыть новую вкладку с заданным рядом
        /// </summary>
        /// <param name="range">ряд данных для отображения</param>
        /// <param name="text">заголовок вкладки</param>
        internal void OpenNewTab(RawRange range, string text = "Новый документ")
        {
            TabPageExt ntab = new TabPageExt(range,text);
            this.TabPages.Add(ntab);
            this.SelectedTab = ntab;
        }
    }
}
