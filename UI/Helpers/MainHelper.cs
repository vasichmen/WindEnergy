using WindEnergy.Lib.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.UI.Ext;

namespace WindEnergy.UI.Helpers
{
    public class MainHelper
    {
        private readonly FormMain f = null;

        public MainHelper(FormMain mainf)
        {
            f = mainf;
        }


        internal void OpenFile(string file)
        {
            RawRange rang = RawRangeSerializer.OpenFile(file, null);
            DataGridViewExt dgv = new DataGridViewExt();
            TabPage pg = new TabPage(rang.FileName);
            dgv.Parent = pg;
            dgv.Dock = DockStyle.Fill;
            dgv.DataSource = rang;
            dgv.Refresh();
            f.mainTabControl.TabPages.Add(pg);
        }
    }
}
