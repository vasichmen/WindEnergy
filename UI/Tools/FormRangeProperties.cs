using GMap.NET;
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
using WindEnergy.Lib.Data.Providers;
using WindEnergy.UI.Dialogs;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// Окно редактирования свойств ряда
    /// </summary>
    public partial class FormRangeProperties : Form
    {
        private PointLatLng point;
        private readonly RawRange Range;

        public FormRangeProperties(RawRange range)
        {
            InitializeComponent();
            this.Range = range;
            point = range.Position;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Range.Name = textBoxName.Text;
            Range.Position = point;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonSelectCoordinates_Click(object sender, EventArgs e)
        {
            FormSelectMapPointDialog spt = new FormSelectMapPointDialog("Выберите точку на карте", point);
            if (spt.ShowDialog(this) == DialogResult.OK)
            {
                point = spt.Result;
                labelCoordinates.Text = $"Широта: {spt.Result.Lat.ToString("0.000")} Долгота: {spt.Result.Lng.ToString("0.000")}";
                labelAddress.Text = new Arcgis(Vars.Options.CacheFolder + "\\arcgis").GetAddress(spt.Result);
            }
        }

        private void FormRangeProperties_Shown(object sender, EventArgs e)
        {
            if (!point.IsEmpty)
            {
                labelCoordinates.Text = $"Широта: {point.Lat.ToString("0.000")} Долгота: {point.Lng.ToString("0.000")}";
                labelAddress.Text = new Arcgis(Vars.Options.CacheFolder + "\\arcgis").GetAddress(point);
            }
            textBoxName.Text = Range.Name;
        }

        private void labelCoordinates_TextChanged(object sender, EventArgs e)
        {
            string n = ((Label)sender).Text;
            new ToolTip().SetToolTip(sender as Label, n);
        }
    }
}
