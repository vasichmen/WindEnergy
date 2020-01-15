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
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Data.Providers;
using WindEnergy.WindLib.Data.Providers.InternetServices;
using WindEnergy.UI.Dialogs;

namespace WindEnergy.UI.Tools
{
    /// <summary>
    /// Окно редактирования свойств ряда
    /// </summary>
    public partial class FormRangeProperties : Form
    {
        private readonly RawRange Range;

        public FormRangeProperties(RawRange range)
        {
            InitializeComponent();
            this.Range = range;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Range.Name = textBoxName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormRangeProperties_Shown(object sender, EventArgs e)
        {
            if (Range.Meteostation != null)
            {
                textBoxMSName.Text = Range.Meteostation.Name;
                new ToolTip().SetToolTip(textBoxMSName, textBoxMSName.Text);

                textBoxMSCoordinates.Text = $"Широта: {Range.Meteostation.Position.Lat.ToString("0.000")} Долгота: {Range.Meteostation.Position.Lng.ToString("0.000")}";
                new ToolTip().SetToolTip(textBoxMSCoordinates, textBoxMSCoordinates.Text);

                textBoxMSAddress.Text = Range.Meteostation.Address;
                new ToolTip().SetToolTip(textBoxMSAddress, textBoxMSAddress.Text);

                switch (Range.Meteostation.MeteoSourceType) {
                    case MeteoSourceType.Meteostation:
                        textBoxMSType.Text = "Метеостанция";
                        labelMSID.Text = "WMO ID метеостанции";
                        textBoxMSID.Text = Range.Meteostation.ID;
                        new ToolTip().SetToolTip(textBoxMSAddress, textBoxMSAddress.Text);
                        break;
                    case MeteoSourceType.Airport:
                        textBoxMSType.Text = "Аэропорт";
                        labelMSID.Text = "CC код аэропорта";
                        textBoxMSID.Text = Range.Meteostation.CC_Code;
                        new ToolTip().SetToolTip(textBoxMSAddress, textBoxMSAddress.Text);
                        break;
                    default: throw new Exception("Этот тип МС не реализован");
                }
            }


            textBoxRangeCount.Text = Range.Count.ToString() +" штук";
            new ToolTip().SetToolTip(textBoxRangeCount, textBoxRangeCount.Text);

            textBoxName.Text = Range.Name;
            new ToolTip().SetToolTip(textBoxName, Range.Name);
        }

        private void labelCoordinates_TextChanged(object sender, EventArgs e)
        {
            string n = ((Label)sender).Text;
            new ToolTip().SetToolTip(sender as Label, n);
        }

        /// <summary>
        /// окно интервалов наблюдений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonIntervals_Click(object sender, EventArgs e)
        {
            new FormRangeStatistic(Range).Show(this);
        }

        /// <summary>
        /// переход к метеостанции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxMSName_Click(object sender, EventArgs e)
        {
            if (Range.Meteostation != null)
                new FormShowMeteostationsMap(Range.Meteostation).Show(this);
        }
    }
}
