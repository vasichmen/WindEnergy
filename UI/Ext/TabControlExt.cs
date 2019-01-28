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
        private List<RawRange> ranges;


        public TabControlExt()
        {
            ranges = new List<RawRange>();
        }

        /// <summary>
        /// открыть новую вкладку с заданным рядом
        /// </summary>
        /// <param name="res"></param>
        internal void OpenNewTab(RawRange res, string text = "Новый документ")
        {
            //TODO: проверить открытие вкладки
            TabPage ntab = new TabPage(text);
            DataGridViewExt ndgv = new DataGridViewExt();
            ndgv.Dock = DockStyle.Fill;
            ndgv.DataSource = res;
            ndgv.Parent = ntab;
            this.TabPages.Add(ntab);
        }
    }
}
