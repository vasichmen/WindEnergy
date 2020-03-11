﻿using CommonLib;
using CommonLib.UITools;
using GMap.NET;
using SolarEnergy.SolarLib;
using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using SolarEnergy.SolarLib.Data.Providers.InternetServices;
using SolarEnergy.UI.Properties;
using SolarLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolarEnergy.UI.Tools
{
    public partial class FormLoadFromNASA : Form
    {
        private PointLatLng point = new PointLatLng(55,37);
        private NasaSourceTypes sourceType = NasaSourceTypes.None;
        private HourModels hourModel = HourModels.None;
        private int year = int.MinValue;

        public DataRange Result { get; private set; }

        public FormLoadFromNASA()
        {
            InitializeComponent();

            comboBoxSourceType.Items.AddRange(NasaSourceTypes.None.GetItems().ToArray());
            comboBoxSourceType.SelectedItem = NasaSourceTypes.AllPeriod.Description();
            comboBoxDailyModel.Items.AddRange(HourModels.None.GetItems().ToArray());
            comboBoxDailyModel.SelectedItem = HourModels.None.Description();
            DialogResult = DialogResult.Cancel;

            updateUI();
        }

        private void buttonSelectPoint_Click(object sender, EventArgs e)
        {
            FormSelectMapPointDialog fsmp = new FormSelectMapPointDialog("Выберите точку на карте", PointLatLng.Empty, Vars.Options.CacheFolder, Resources.rp5_marker, Vars.Options.MapProvider);
            if (fsmp.ShowDialog(this) == DialogResult.OK)
            {
                point = fsmp.Result;
                updateUI();
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            RawRange data = new NASA(Vars.Options.CacheFolder + "\\nasa").GetRange(DateTime.Now - TimeSpan.FromDays(30 * 365), DateTime.Now, new NPSMeteostationInfo() { Position = point });
            DataRange range = new DataRange(data, new DataRangeConverterParams()
            {
                HourModel = hourModel,
                Interval = StandartIntervals.D1,
                SourceType = sourceType,
                Year = year
                
            });
            Result = range;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void comboBoxSourceType_SelectedValueChanged(object sender, EventArgs e)
        {
            sourceType = (NasaSourceTypes)(new EnumTypeConverter<NasaSourceTypes>().ConvertFrom(comboBoxSourceType.SelectedItem));
            updateUI();
        }

        /// <summary>
        /// обновление контролов 
        /// </summary>
        private void updateUI()
        {
            labelPointStatus.ForeColor = point.IsEmpty ? Color.Red : Color.Green;
            labelPointStatus.Text = point.IsEmpty ? "Не выбрана" : point.ToString(2);
            buttonLoad.Enabled = labelPointStatus.ForeColor == Color.Green && sourceType != NasaSourceTypes.None && hourModel != HourModels.None;
        }

        private void comboBoxDailyModel_SelectedValueChanged(object sender, EventArgs e)
        {
            hourModel = (HourModels)(new EnumTypeConverter<HourModels>().ConvertFrom(comboBoxDailyModel.SelectedItem));
            updateUI();
        }
    }
}
