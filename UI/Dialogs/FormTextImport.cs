using GMap.NET;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes;
using WindEnergy.Lib.Classes.Collections;
using WindEnergy.Lib.Data.Providers.FileSystem;
using WindEnergy.UI.Ext;

namespace WindEnergy.UI.Dialogs
{
    /// <summary>
    /// окно импорта ряда наблюдений из текстового файла
    /// </summary>
    public partial class FormTextImport : Form
    {
        private int maxLineNumberCharLength;

        /// <summary>
        /// импортер данных из файла
        /// </summary>
        private TextImporter importer;

        /// <summary>
        /// результат импорта ряда наблюдений
        /// </summary>
        public RawRange Result { get; private set; }

        /// <summary>
        /// Создает новое окно импорта
        /// </summary>
        public FormTextImport()
        {
            InitializeComponent();
            importer = new TextImporter();
        }

        private void formTextImport_Load(object sender, EventArgs e)
        {
            //стиль индикатора
            Indicator indic = scintillaExample.Indicators[16];
            indic.Style = IndicatorStyle.FullBox;
            indic.ForeColor = Color.DarkRed;
            indic.Alpha = 100;
            scintillaExample.IndicatorCurrent = indic.Index;

            //заполнение кодировок
            comboBoxEncoding.Items.Clear();
            foreach (EncodingInfo enc in Encoding.GetEncodings())
                comboBoxEncoding.Items.Add(enc.GetEncoding().WebName);
            comboBoxEncoding.SelectedItem = "utf-8";

            //направления
            comboBoxDirectUnit.Items.Clear();
            foreach (string item in DirectionUnits.Degrees.GetItems())
                comboBoxDirectUnit.Items.Add(item);
            comboBoxDirectUnit.SelectedItem = DirectionUnits.TextRP5.Description();

            //давление
            comboBoxPressUnit.Items.Clear();
            foreach (string item in PressureUnits.KPa.GetItems())
                comboBoxPressUnit.Items.Add(item);
            comboBoxPressUnit.SelectedItem = PressureUnits.KPa.Description();

            //влажность
            comboBoxWetUnit.Items.Clear();
            foreach (string item in WetnessUnits.Parts.GetItems())
                comboBoxWetUnit.Items.Add(item);
            comboBoxWetUnit.SelectedItem = WetnessUnits.Parts.Description();

            //установка тегов для полей колонок
            numericUpDownColDate.Tag = ImportFields.Date;
            numericUpDownColSpeed.Tag = ImportFields.Speed;
            numericUpDownColTemper.Tag = ImportFields.Temperature;
            numericUpDownColWet.Tag = ImportFields.Wetness;
            numericUpDownColDirect.Tag = ImportFields.Direction;
            numericUpDownColPress.Tag = ImportFields.Pressure;

            writeImporter();
            update();
        }

        /// <summary>
        /// запись в настройки импорта данных из полей
        /// </summary>
        private void writeImporter()
        {
            foreach (Control contr in groupBoxColumns.Controls)
                if (contr.GetType() == typeof(NumericUpDown))
                    if (contr.Tag != null && contr.Tag.GetType() == typeof(ImportFields))
                        if (importer.Columns.ContainsKey((ImportFields)contr.Tag))
                            importer.Columns[(ImportFields)contr.Tag] = (int)((NumericUpDown)contr).Value;
                        else
                            importer.Columns.Add((ImportFields)contr.Tag, (int)((NumericUpDown)contr).Value);

            importer.Delimeter = textBoxDelimeter.Text;
            importer.Trimmers = textBoxTrimmers.Text.ToCharArray();
            importer.Encoding = Encoding.GetEncoding(comboBoxEncoding.SelectedItem as string);
            importer.DirectionUnit = (DirectionUnits)(new EnumTypeConverter<DirectionUnits>().ConvertFrom(comboBoxDirectUnit.SelectedItem));
            importer.PressureUnit = (PressureUnits)(new EnumTypeConverter<PressureUnits>().ConvertFrom(comboBoxPressUnit.SelectedItem));
            importer.WetnessUnit = (WetnessUnits)(new EnumTypeConverter<WetnessUnits>().ConvertFrom(comboBoxWetUnit.SelectedItem));
            importer.BindNearestMS = checkBoxFindNearestMS.Checked;
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.InitialDirectory = Vars.Options.LastDirectory;
            of.Multiselect = false;
            if (of.ShowDialog() == DialogResult.OK)
            {
                this.importer.FilePath = of.FileName;
                labelPath.Text = Path.GetFileName(this.importer.FilePath);
                new ToolTip().SetToolTip(labelPath, this.importer.FilePath);
                Vars.Options.LastDirectory = Path.GetDirectoryName(this.importer.FilePath);
                this.update();
            }
        }

        /// <summary>
        /// обновление полей после изменения параметров импорта
        /// </summary>
        private void update()
        {
            scintillaExample.ReadOnly = false;
            //заполнение первых 10 строк файла в окно
            try
            {
                scintillaExample.Text = importer.GetText(10); //получаем текст

                //выделяем разделители
                MatchCollection collect = new Regex("w*" + textBoxDelimeter.Text + "w*").Matches(scintillaExample.Text);
                foreach (Match match in collect)
                    scintillaExample.IndicatorFillRange(match.Index, match.Length);

                //количество столбцов в первой строке
                string firstLine = scintillaExample.Lines[0].Text;
                string[] cols = new Regex("w*" + textBoxDelimeter.Text + "w*").Split(firstLine);
                int count = cols.Length;
                groupBoxColumns.Text = $"Настройки столбцов. В первой строке доступно {count} шт.";
                if (count > 0)
                {
                    foreach (var c in groupBoxColumns.Controls)
                        if (c.GetType() == typeof(NumericUpDown))
                            (c as NumericUpDown).Maximum = count;
                }

                //Проверка параметров
                try
                {
                    string success = importer.CheckParameters();
                    toolStripStatusLabelImportOptionsCorrect.ForeColor = Color.Green;
                    toolStripStatusLabelImportOptionsCorrect.Text = success;
                }
                catch (ArgumentException aex)
                {
                    toolStripStatusLabelImportOptionsCorrect.Text = aex.Message;
                    toolStripStatusLabelImportOptionsCorrect.ForeColor = Color.Red;
                }

                //заполнение примера импорта
                try
                {
                    RawRange test =importer.Import(10);
                    dataGridViewImported.DataSource = test;
                }
                catch (WindEnergyException wex)
                {
                    toolStripStatusLabelImportOptionsCorrect.Text = "Ошибка импорта: " + wex.Message;
                    toolStripStatusLabelImportOptionsCorrect.ToolTipText = wex.ToolTip;
                    toolStripStatusLabelImportOptionsCorrect.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabelImportOptionsCorrect.Text = ex.Message;
                toolStripStatusLabelImportOptionsCorrect.ForeColor = Color.Red;
            }
            scintillaExample.ReadOnly = true;
        }

        /// <summary>
        /// обновление зависимых контролов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlUpdate_Event(object sender, EventArgs e)
        {
            //установка изменившегося параметра
            string name = (sender as Control).Name;
            switch (name)
            {
                case "textBoxDelimeter":
                    importer.Delimeter = textBoxDelimeter.Text;
                    break;
                case "numericUpDownStartLine":
                    importer.StartLine = (int)numericUpDownStartLine.Value;
                    break;
                case "comboBoxEncoding":
                    importer.Encoding = Encoding.GetEncoding(comboBoxEncoding.SelectedItem as string);
                    break;
                case "textBoxTrimmers":
                    importer.Trimmers = textBoxTrimmers.Text.ToCharArray();
                    break;
                case "numericUpDownColDate":
                case "numericUpDownColSpeed":
                case "numericUpDownColTemper":
                case "numericUpDownColWet":
                case "numericUpDownColDirect":
                case "numericUpDownColPress":
                    var contr = sender as NumericUpDown;
                    ImportFields fld = (ImportFields)contr.Tag;
                    if (importer.Columns.ContainsKey(fld))
                        importer.Columns[fld] = (int)contr.Value;
                    else
                        importer.Columns.Add(fld, (int)contr.Value);
                    break;
                case "comboBoxDirectUnit":
                    importer.DirectionUnit = (DirectionUnits)(new EnumTypeConverter<DirectionUnits>().ConvertFrom(comboBoxDirectUnit.SelectedItem));
                    break;
                case "comboBoxPressUnit":
                    importer.PressureUnit = (PressureUnits)(new EnumTypeConverter<PressureUnits>().ConvertFrom(comboBoxPressUnit.SelectedItem));
                    break;
                case "comboBoxWetUnit":
                    importer.WetnessUnit = (WetnessUnits)(new EnumTypeConverter<WetnessUnits>().ConvertFrom(comboBoxWetUnit.SelectedItem));
                    break;
                case "checkBoxFindNearestMS":
                    importer.BindNearestMS = checkBoxFindNearestMS.Checked;
                    break;
                default: throw new Exception("Этот контрол не реализован");
            }

            update(); //пересобираем окно
        }

        /// <summary>
        /// Установка номеров строк
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scintilla_TextChanged(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintillaExample.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int PADDING = 2;
            scintillaExample.Margins[0].Width = scintillaExample.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + PADDING;
            this.maxLineNumberCharLength = maxLineNumberCharLength;
        }

        /// <summary>
        /// выбор координат ряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSelectCoordinates_Click(object sender, EventArgs e)
        {
            FormSelectMapPointDialog spt = new FormSelectMapPointDialog("Выберите точку на карте", PointLatLng.Empty);
            if (spt.ShowDialog(this) == DialogResult.OK)
            {
                labelCoordinates.Text = $"Широта: {spt.Result.Lat.ToString("0.000")} Долгота: {spt.Result.Lng.ToString("0.000")}";
                toolTip1.SetToolTip(labelCoordinates, labelCoordinates.Text);
                importer.Coordinates = spt.Result;
                update();
            }
        }

        /// <summary>
        /// импортирование данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonImport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Result = importer.Import();
                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Импорт из файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// изменение типа столбцов при добавлении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            switch (e.Column.Name.ToLower())
            {
                case "date":
                    e.Column.HeaderText = "Дата наблюдения";
                    e.Column.Width = 130;
                    e.Column.CellTemplate = new DataGridViewCalendarCell();
                    break;
                case "direction":
                    e.Column.HeaderText = "Направление, °";
                    e.Column.DefaultCellStyle.Format = "n2";
                    break;
                case "directionrhumb":
                    e.Column.HeaderText = "Румб";
                    e.Column.Width = 55;
                    e.Column.CellTemplate = new DataGridViewComboboxCell<WindDirections>();
                    break;
                case "speed":
                    e.Column.HeaderText = "Скорость, м/с";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "temperature":
                    e.Column.HeaderText = "Температура, °С";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "wetness":
                    e.Column.HeaderText = "Влажность, %";
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;
                case "pressure":
                    e.Column.HeaderText = "Давление, мм рт. ст.";
                    e.Column.Width = 130;
                    e.Column.DefaultCellStyle.Format = "n1";
                    break;

                //удаляемые колонки пишем тут:
                case "dateargument":
                    e.Column.DataGridView.Columns.Remove(e.Column);
                    break;
                default: throw new Exception("Для этой колонки нет названия");
            }
        }

        #region Установки по умолчанию

        private void defaultRP5wmoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                numericUpDownColDate.Value = 1;
                numericUpDownColDirect.Value = 7;
                numericUpDownColPress.Value = 3;
                numericUpDownColSpeed.Value = 8;
                numericUpDownColTemper.Value = 2;
                numericUpDownColWet.Value = 6;
                comboBoxDirectUnit.SelectedItem = DirectionUnits.TextRP5.Description();
                comboBoxPressUnit.SelectedItem = PressureUnits.mmHgArt.Description();
                comboBoxWetUnit.SelectedItem = WetnessUnits.Percents.Description();
                textBoxTrimmers.Text = "\"";
                textBoxDelimeter.Text = ";";
                writeImporter();
                update();
            }
            catch (Exception)
            { }
        }

        private void defaultRP5metarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                numericUpDownColDate.Value = 1;
                numericUpDownColDirect.Value = 6;
                numericUpDownColPress.Value = 3;
                numericUpDownColSpeed.Value = 7;
                numericUpDownColTemper.Value = 2;
                numericUpDownColWet.Value = 5;
                comboBoxDirectUnit.SelectedItem = DirectionUnits.TextRP5.Description();
                comboBoxPressUnit.SelectedItem = PressureUnits.mmHgArt.Description();
                comboBoxWetUnit.SelectedItem = WetnessUnits.Percents.Description();
                textBoxTrimmers.Text = "\"";
                textBoxDelimeter.Text = ";";
                writeImporter();
                update();
            }
            catch (Exception)
            { }
        }

        #endregion
    }
}
