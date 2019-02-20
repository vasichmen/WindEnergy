using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Operations;
using WindEnergy.Lib.Operations.Structures;
using WindEnergy.Lib.Statistic.Calculations;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// окно основных энергетических характеристик
    /// </summary>
    public partial class FormEnergyInfo : Form
    {
        private RawRange range;
        
        /// <summary>
        /// создаёт новое окно с заданным рядом
        /// </summary>
        /// <param name="rang">ряд наблюдений, для которого расчитываются характеристики</param>
        public FormEnergyInfo(RawRange rang)
        {
            InitializeComponent();
            this.range = rang;
        }

        /// <summary>
        /// расчет характеристик ряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formEnergyInfo_Shown(object sender, EventArgs e)
        {
            //TODO: расчёт параметров и заполнение полей 
            QualityInfo info = Qualifier.ProcessRange(range);
        }
    }
}
