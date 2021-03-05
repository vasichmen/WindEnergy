using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.WindLib.Classes.Structures;

namespace WindEnergy.UI.Tools
{
    public partial class FormPowerCalculatorResult : Form
    {
        private EquipmentItemInfo equipment;

        public FormPowerCalculatorResult(EquipmentItemInfo selectedEquipment)
        {
            InitializeComponent();
            equipment = selectedEquipment;
        }
    }
}
