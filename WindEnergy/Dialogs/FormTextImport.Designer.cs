using WindEnergy.UI.Ext;

namespace WindEnergy.UI.Dialogs
{
    partial class FormTextImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTextImport));
            this.buttonSelect = new System.Windows.Forms.Button();
            this.labelPath = new System.Windows.Forms.Label();
            this.scintillaExample = new ScintillaNET.Scintilla();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTrimmers = new System.Windows.Forms.TextBox();
            this.checkBoxFindNearestMS = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxEncoding = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownStartLine = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDelimeter = new System.Windows.Forms.TextBox();
            this.groupBoxColumns = new System.Windows.Forms.GroupBox();
            this.checkBoxUseWetness = new System.Windows.Forms.CheckBox();
            this.checkBoxUsePressure = new System.Windows.Forms.CheckBox();
            this.checkBoxUseTemperature = new System.Windows.Forms.CheckBox();
            this.comboBoxWetUnit = new System.Windows.Forms.ComboBox();
            this.labelCoordinates = new System.Windows.Forms.Label();
            this.buttonSelectCoordinates = new System.Windows.Forms.Button();
            this.labelTemp = new System.Windows.Forms.Label();
            this.numericUpDownColTemper = new System.Windows.Forms.NumericUpDown();
            this.comboBoxDirectUnit = new System.Windows.Forms.ComboBox();
            this.comboBoxPressUnit = new System.Windows.Forms.ComboBox();
            this.labelDirect = new System.Windows.Forms.Label();
            this.numericUpDownColDirect = new System.Windows.Forms.NumericUpDown();
            this.labelWet = new System.Windows.Forms.Label();
            this.numericUpDownColWet = new System.Windows.Forms.NumericUpDown();
            this.labelPress = new System.Windows.Forms.Label();
            this.numericUpDownColPress = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownColSpeed = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownColDate = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelImportOptionsCorrect = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.установкиПоУмолчаниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultRP5wmoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultRP5metarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxUseDirection = new System.Windows.Forms.CheckBox();
            this.dataGridViewImported = new WindEnergy.UI.Ext.DataGridViewExt();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartLine)).BeginInit();
            this.groupBoxColumns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColTemper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColDirect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColWet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColPress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColDate)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImported)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(12, 27);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(75, 23);
            this.buttonSelect.TabIndex = 0;
            this.buttonSelect.Text = "Открыть";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(93, 32);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(86, 13);
            this.labelPath.TabIndex = 1;
            this.labelPath.Text = "Выберите файл";
            // 
            // scintillaExample
            // 
            this.scintillaExample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scintillaExample.Location = new System.Drawing.Point(12, 56);
            this.scintillaExample.Name = "scintillaExample";
            this.scintillaExample.ReadOnly = true;
            this.scintillaExample.Size = new System.Drawing.Size(857, 138);
            this.scintillaExample.TabIndex = 9;
            this.scintillaExample.TextChanged += new System.EventHandler(this.scintilla_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Удалить символы:";
            this.toolTip1.SetToolTip(this.label4, "Символы, удаляемые в начале и в конце ячейки перед импортом");
            // 
            // textBoxTrimmers
            // 
            this.textBoxTrimmers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxTrimmers.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxTrimmers.Location = new System.Drawing.Point(139, 98);
            this.textBoxTrimmers.Name = "textBoxTrimmers";
            this.textBoxTrimmers.Size = new System.Drawing.Size(63, 23);
            this.textBoxTrimmers.TabIndex = 19;
            this.textBoxTrimmers.Text = "\"";
            this.toolTip1.SetToolTip(this.textBoxTrimmers, "Символы, удаляемые в начале и в конце ячейки перед импортом");
            this.textBoxTrimmers.TextChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // checkBoxFindNearestMS
            // 
            this.checkBoxFindNearestMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxFindNearestMS.AutoSize = true;
            this.checkBoxFindNearestMS.Checked = true;
            this.checkBoxFindNearestMS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFindNearestMS.Location = new System.Drawing.Point(415, 102);
            this.checkBoxFindNearestMS.Name = "checkBoxFindNearestMS";
            this.checkBoxFindNearestMS.Size = new System.Drawing.Size(157, 17);
            this.checkBoxFindNearestMS.TabIndex = 17;
            this.checkBoxFindNearestMS.Text = "Связать с ближайшей МС";
            this.toolTip1.SetToolTip(this.checkBoxFindNearestMS, "Если выбрано, то к ряду будет привязана ближайшая МС из БД Метеостанции мира");
            this.checkBoxFindNearestMS.UseVisualStyleBackColor = true;
            this.checkBoxFindNearestMS.CheckedChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxTrimmers);
            this.groupBox1.Controls.Add(this.comboBoxEncoding);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownStartLine);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxDelimeter);
            this.groupBox1.Location = new System.Drawing.Point(12, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 127);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки импорта";
            // 
            // comboBoxEncoding
            // 
            this.comboBoxEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxEncoding.FormattingEnabled = true;
            this.comboBoxEncoding.Location = new System.Drawing.Point(139, 71);
            this.comboBoxEncoding.Name = "comboBoxEncoding";
            this.comboBoxEncoding.Size = new System.Drawing.Size(128, 21);
            this.comboBoxEncoding.TabIndex = 18;
            this.comboBoxEncoding.SelectedIndexChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Кодировка:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Начать со строки:";
            // 
            // numericUpDownStartLine
            // 
            this.numericUpDownStartLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownStartLine.Location = new System.Drawing.Point(139, 45);
            this.numericUpDownStartLine.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownStartLine.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStartLine.Name = "numericUpDownStartLine";
            this.numericUpDownStartLine.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownStartLine.TabIndex = 15;
            this.numericUpDownStartLine.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStartLine.ValueChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Разделитель столбцов:";
            // 
            // textBoxDelimeter
            // 
            this.textBoxDelimeter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDelimeter.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDelimeter.Location = new System.Drawing.Point(139, 16);
            this.textBoxDelimeter.Name = "textBoxDelimeter";
            this.textBoxDelimeter.Size = new System.Drawing.Size(63, 23);
            this.textBoxDelimeter.TabIndex = 13;
            this.textBoxDelimeter.Text = ";";
            this.textBoxDelimeter.TextChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // groupBoxColumns
            // 
            this.groupBoxColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxColumns.Controls.Add(this.checkBoxUseDirection);
            this.groupBoxColumns.Controls.Add(this.checkBoxUseWetness);
            this.groupBoxColumns.Controls.Add(this.checkBoxUsePressure);
            this.groupBoxColumns.Controls.Add(this.checkBoxUseTemperature);
            this.groupBoxColumns.Controls.Add(this.checkBoxFindNearestMS);
            this.groupBoxColumns.Controls.Add(this.comboBoxWetUnit);
            this.groupBoxColumns.Controls.Add(this.labelCoordinates);
            this.groupBoxColumns.Controls.Add(this.buttonSelectCoordinates);
            this.groupBoxColumns.Controls.Add(this.labelTemp);
            this.groupBoxColumns.Controls.Add(this.numericUpDownColTemper);
            this.groupBoxColumns.Controls.Add(this.comboBoxDirectUnit);
            this.groupBoxColumns.Controls.Add(this.comboBoxPressUnit);
            this.groupBoxColumns.Controls.Add(this.labelDirect);
            this.groupBoxColumns.Controls.Add(this.numericUpDownColDirect);
            this.groupBoxColumns.Controls.Add(this.labelWet);
            this.groupBoxColumns.Controls.Add(this.numericUpDownColWet);
            this.groupBoxColumns.Controls.Add(this.labelPress);
            this.groupBoxColumns.Controls.Add(this.numericUpDownColPress);
            this.groupBoxColumns.Controls.Add(this.label6);
            this.groupBoxColumns.Controls.Add(this.numericUpDownColSpeed);
            this.groupBoxColumns.Controls.Add(this.label5);
            this.groupBoxColumns.Controls.Add(this.numericUpDownColDate);
            this.groupBoxColumns.Location = new System.Drawing.Point(291, 200);
            this.groupBoxColumns.Name = "groupBoxColumns";
            this.groupBoxColumns.Size = new System.Drawing.Size(578, 127);
            this.groupBoxColumns.TabIndex = 14;
            this.groupBoxColumns.TabStop = false;
            this.groupBoxColumns.Text = "Настройки столбцов";
            // 
            // checkBoxUseWetness
            // 
            this.checkBoxUseWetness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUseWetness.AutoSize = true;
            this.checkBoxUseWetness.Location = new System.Drawing.Point(325, 71);
            this.checkBoxUseWetness.Name = "checkBoxUseWetness";
            this.checkBoxUseWetness.Size = new System.Drawing.Size(15, 14);
            this.checkBoxUseWetness.TabIndex = 20;
            this.checkBoxUseWetness.Tag = "";
            this.checkBoxUseWetness.UseVisualStyleBackColor = true;
            this.checkBoxUseWetness.CheckedChanged += new System.EventHandler(this.checkBoxUse_CheckedChanged);
            // 
            // checkBoxUsePressure
            // 
            this.checkBoxUsePressure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUsePressure.AutoSize = true;
            this.checkBoxUsePressure.Location = new System.Drawing.Point(325, 42);
            this.checkBoxUsePressure.Name = "checkBoxUsePressure";
            this.checkBoxUsePressure.Size = new System.Drawing.Size(15, 14);
            this.checkBoxUsePressure.TabIndex = 19;
            this.checkBoxUsePressure.Tag = "";
            this.checkBoxUsePressure.UseVisualStyleBackColor = true;
            this.checkBoxUsePressure.CheckedChanged += new System.EventHandler(this.checkBoxUse_CheckedChanged);
            // 
            // checkBoxUseTemperature
            // 
            this.checkBoxUseTemperature.AutoSize = true;
            this.checkBoxUseTemperature.Location = new System.Drawing.Point(142, 70);
            this.checkBoxUseTemperature.Name = "checkBoxUseTemperature";
            this.checkBoxUseTemperature.Size = new System.Drawing.Size(15, 14);
            this.checkBoxUseTemperature.TabIndex = 18;
            this.checkBoxUseTemperature.UseVisualStyleBackColor = true;
            this.checkBoxUseTemperature.CheckedChanged += new System.EventHandler(this.checkBoxUse_CheckedChanged);
            // 
            // comboBoxWetUnit
            // 
            this.comboBoxWetUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWetUnit.Enabled = false;
            this.comboBoxWetUnit.FormattingEnabled = true;
            this.comboBoxWetUnit.Location = new System.Drawing.Point(483, 65);
            this.comboBoxWetUnit.Name = "comboBoxWetUnit";
            this.comboBoxWetUnit.Size = new System.Drawing.Size(89, 21);
            this.comboBoxWetUnit.TabIndex = 16;
            this.comboBoxWetUnit.SelectedIndexChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // labelCoordinates
            // 
            this.labelCoordinates.AutoSize = true;
            this.labelCoordinates.Location = new System.Drawing.Point(142, 103);
            this.labelCoordinates.Name = "labelCoordinates";
            this.labelCoordinates.Size = new System.Drawing.Size(133, 13);
            this.labelCoordinates.TabIndex = 15;
            this.labelCoordinates.Text = "Координаты не выбраны";
            // 
            // buttonSelectCoordinates
            // 
            this.buttonSelectCoordinates.Location = new System.Drawing.Point(9, 98);
            this.buttonSelectCoordinates.Name = "buttonSelectCoordinates";
            this.buttonSelectCoordinates.Size = new System.Drawing.Size(127, 23);
            this.buttonSelectCoordinates.TabIndex = 14;
            this.buttonSelectCoordinates.Text = "Выбрать координаты";
            this.buttonSelectCoordinates.UseVisualStyleBackColor = true;
            this.buttonSelectCoordinates.Click += new System.EventHandler(this.ButtonSelectCoordinates_Click);
            // 
            // labelTemp
            // 
            this.labelTemp.AutoSize = true;
            this.labelTemp.Enabled = false;
            this.labelTemp.Location = new System.Drawing.Point(6, 72);
            this.labelTemp.Name = "labelTemp";
            this.labelTemp.Size = new System.Drawing.Size(74, 13);
            this.labelTemp.TabIndex = 13;
            this.labelTemp.Text = "Температура";
            // 
            // numericUpDownColTemper
            // 
            this.numericUpDownColTemper.Enabled = false;
            this.numericUpDownColTemper.Location = new System.Drawing.Point(86, 67);
            this.numericUpDownColTemper.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColTemper.Name = "numericUpDownColTemper";
            this.numericUpDownColTemper.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownColTemper.TabIndex = 12;
            this.numericUpDownColTemper.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColTemper.ValueChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // comboBoxDirectUnit
            // 
            this.comboBoxDirectUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDirectUnit.Enabled = false;
            this.comboBoxDirectUnit.FormattingEnabled = true;
            this.comboBoxDirectUnit.Location = new System.Drawing.Point(483, 12);
            this.comboBoxDirectUnit.Name = "comboBoxDirectUnit";
            this.comboBoxDirectUnit.Size = new System.Drawing.Size(89, 21);
            this.comboBoxDirectUnit.TabIndex = 11;
            this.comboBoxDirectUnit.SelectedIndexChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // comboBoxPressUnit
            // 
            this.comboBoxPressUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPressUnit.Enabled = false;
            this.comboBoxPressUnit.FormattingEnabled = true;
            this.comboBoxPressUnit.Location = new System.Drawing.Point(483, 39);
            this.comboBoxPressUnit.Name = "comboBoxPressUnit";
            this.comboBoxPressUnit.Size = new System.Drawing.Size(89, 21);
            this.comboBoxPressUnit.TabIndex = 10;
            this.comboBoxPressUnit.SelectedIndexChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // labelDirect
            // 
            this.labelDirect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDirect.AutoSize = true;
            this.labelDirect.Enabled = false;
            this.labelDirect.Location = new System.Drawing.Point(346, 15);
            this.labelDirect.Name = "labelDirect";
            this.labelDirect.Size = new System.Drawing.Size(75, 13);
            this.labelDirect.TabIndex = 9;
            this.labelDirect.Text = "Направление";
            // 
            // numericUpDownColDirect
            // 
            this.numericUpDownColDirect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownColDirect.Enabled = false;
            this.numericUpDownColDirect.Location = new System.Drawing.Point(427, 13);
            this.numericUpDownColDirect.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColDirect.Name = "numericUpDownColDirect";
            this.numericUpDownColDirect.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownColDirect.TabIndex = 8;
            this.numericUpDownColDirect.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColDirect.ValueChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // labelWet
            // 
            this.labelWet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWet.AutoSize = true;
            this.labelWet.Enabled = false;
            this.labelWet.Location = new System.Drawing.Point(347, 70);
            this.labelWet.Name = "labelWet";
            this.labelWet.Size = new System.Drawing.Size(63, 13);
            this.labelWet.TabIndex = 7;
            this.labelWet.Text = "Влажность";
            // 
            // numericUpDownColWet
            // 
            this.numericUpDownColWet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownColWet.Enabled = false;
            this.numericUpDownColWet.Location = new System.Drawing.Point(427, 65);
            this.numericUpDownColWet.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColWet.Name = "numericUpDownColWet";
            this.numericUpDownColWet.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownColWet.TabIndex = 6;
            this.numericUpDownColWet.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColWet.ValueChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // labelPress
            // 
            this.labelPress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPress.AutoSize = true;
            this.labelPress.Enabled = false;
            this.labelPress.Location = new System.Drawing.Point(346, 42);
            this.labelPress.Name = "labelPress";
            this.labelPress.Size = new System.Drawing.Size(58, 13);
            this.labelPress.TabIndex = 5;
            this.labelPress.Text = "Давление";
            // 
            // numericUpDownColPress
            // 
            this.numericUpDownColPress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownColPress.Enabled = false;
            this.numericUpDownColPress.Location = new System.Drawing.Point(427, 40);
            this.numericUpDownColPress.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColPress.Name = "numericUpDownColPress";
            this.numericUpDownColPress.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownColPress.TabIndex = 4;
            this.numericUpDownColPress.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColPress.ValueChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Скорость";
            // 
            // numericUpDownColSpeed
            // 
            this.numericUpDownColSpeed.Location = new System.Drawing.Point(86, 41);
            this.numericUpDownColSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColSpeed.Name = "numericUpDownColSpeed";
            this.numericUpDownColSpeed.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownColSpeed.TabIndex = 2;
            this.numericUpDownColSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColSpeed.ValueChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Дата";
            // 
            // numericUpDownColDate
            // 
            this.numericUpDownColDate.Location = new System.Drawing.Point(86, 18);
            this.numericUpDownColDate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColDate.Name = "numericUpDownColDate";
            this.numericUpDownColDate.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownColDate.TabIndex = 0;
            this.numericUpDownColDate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColDate.ValueChanged += new System.EventHandler(this.controlUpdate_Event);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelImportOptionsCorrect});
            this.statusStrip1.Location = new System.Drawing.Point(0, 484);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(881, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelImportOptionsCorrect
            // 
            this.toolStripStatusLabelImportOptionsCorrect.Name = "toolStripStatusLabelImportOptionsCorrect";
            this.toolStripStatusLabelImportOptionsCorrect.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelImportOptionsCorrect.Text = "toolStripStatusLabel1";
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImport.Location = new System.Drawing.Point(750, 392);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(119, 50);
            this.buttonImport.TabIndex = 16;
            this.buttonImport.Text = "Импортировать";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(750, 448);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(119, 33);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.установкиПоУмолчаниюToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(881, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // установкиПоУмолчаниюToolStripMenuItem
            // 
            this.установкиПоУмолчаниюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultRP5wmoToolStripMenuItem,
            this.defaultRP5metarToolStripMenuItem});
            this.установкиПоУмолчаниюToolStripMenuItem.Name = "установкиПоУмолчаниюToolStripMenuItem";
            this.установкиПоУмолчаниюToolStripMenuItem.Size = new System.Drawing.Size(217, 20);
            this.установкиПоУмолчаниюToolStripMenuItem.Text = "Установки столбцов по умолчанию";
            // 
            // defaultRP5wmoToolStripMenuItem
            // 
            this.defaultRP5wmoToolStripMenuItem.Name = "defaultRP5wmoToolStripMenuItem";
            this.defaultRP5wmoToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.defaultRP5wmoToolStripMenuItem.Text = "Расписание погоды (метеостанция)";
            this.defaultRP5wmoToolStripMenuItem.Click += new System.EventHandler(this.defaultRP5wmoToolStripMenuItem_Click);
            // 
            // defaultRP5metarToolStripMenuItem
            // 
            this.defaultRP5metarToolStripMenuItem.Name = "defaultRP5metarToolStripMenuItem";
            this.defaultRP5metarToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.defaultRP5metarToolStripMenuItem.Text = "Расписание погоды (аэропорт)";
            this.defaultRP5metarToolStripMenuItem.Click += new System.EventHandler(this.defaultRP5metarToolStripMenuItem_Click);
            // 
            // checkBoxUseDirection
            // 
            this.checkBoxUseDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUseDirection.AutoSize = true;
            this.checkBoxUseDirection.Location = new System.Drawing.Point(325, 15);
            this.checkBoxUseDirection.Name = "checkBoxUseDirection";
            this.checkBoxUseDirection.Size = new System.Drawing.Size(15, 14);
            this.checkBoxUseDirection.TabIndex = 21;
            this.checkBoxUseDirection.Tag = "";
            this.checkBoxUseDirection.UseVisualStyleBackColor = true;
            this.checkBoxUseDirection.CheckedChanged += new System.EventHandler(this.checkBoxUse_CheckedChanged);
            // 
            // dataGridViewImported
            // 
            this.dataGridViewImported.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewImported.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewImported.Location = new System.Drawing.Point(12, 333);
            this.dataGridViewImported.Name = "dataGridViewImported";
            this.dataGridViewImported.ReadOnly = true;
            this.dataGridViewImported.Size = new System.Drawing.Size(732, 148);
            this.dataGridViewImported.TabIndex = 10;
            this.dataGridViewImported.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DataGridView_ColumnAdded);
            // 
            // FormTextImport
            // 
            this.AcceptButton = this.buttonImport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(881, 506);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBoxColumns);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewImported);
            this.Controls.Add(this.scintillaExample);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.buttonSelect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(873, 440);
            this.Name = "FormTextImport";
            this.Text = "Импорт ряда их текстового файла";
            this.Load += new System.EventHandler(this.formTextImport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartLine)).EndInit();
            this.groupBoxColumns.ResumeLayout(false);
            this.groupBoxColumns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColTemper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColDirect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColWet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColPress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColDate)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImported)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Label labelPath;
        private ScintillaNET.Scintilla scintillaExample;
        private DataGridViewExt dataGridViewImported;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTrimmers;
        private System.Windows.Forms.ComboBox comboBoxEncoding;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownStartLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDelimeter;
        private System.Windows.Forms.GroupBox groupBoxColumns;
        private System.Windows.Forms.NumericUpDown numericUpDownColDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label labelWet;
        private System.Windows.Forms.NumericUpDown numericUpDownColWet;
        private System.Windows.Forms.Label labelPress;
        private System.Windows.Forms.NumericUpDown numericUpDownColPress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownColSpeed;
        private System.Windows.Forms.ComboBox comboBoxDirectUnit;
        private System.Windows.Forms.ComboBox comboBoxPressUnit;
        private System.Windows.Forms.Label labelDirect;
        private System.Windows.Forms.NumericUpDown numericUpDownColDirect;
        private System.Windows.Forms.Label labelTemp;
        private System.Windows.Forms.NumericUpDown numericUpDownColTemper;
        private System.Windows.Forms.Button buttonSelectCoordinates;
        private System.Windows.Forms.Label labelCoordinates;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImportOptionsCorrect;
        private System.Windows.Forms.ComboBox comboBoxWetUnit;
        private System.Windows.Forms.CheckBox checkBoxFindNearestMS;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem установкиПоУмолчаниюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultRP5wmoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultRP5metarToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxUseWetness;
        private System.Windows.Forms.CheckBox checkBoxUsePressure;
        private System.Windows.Forms.CheckBox checkBoxUseTemperature;
        private System.Windows.Forms.CheckBox checkBoxUseDirection;
    }
}