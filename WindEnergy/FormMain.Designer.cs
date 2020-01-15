﻿namespace WindEnergy.UI
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadRP5ruToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadNASAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showMeteostationsMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repairRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rangePropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.операцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.equalizeRangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateEnergyInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCalcYear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.createNewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveAlltoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.contextMenuStripRangeIntervals = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripStatusLabelRangeCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCompletness = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelInterval = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mainTabControl = new WindEnergy.UI.Ext.TabControlExt();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.правкаToolStripMenuItem,
            this.операцииToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1157, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MenuActivate += new System.EventHandler(this.menuStrip1_MenuActivate);
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.showMeteostationsMapToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(50, 21);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // createNewToolStripMenuItem
            // 
            this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
            this.createNewToolStripMenuItem.Size = new System.Drawing.Size(289, 22);
            this.createNewToolStripMenuItem.Text = "Создать";
            this.createNewToolStripMenuItem.Click += new System.EventHandler(this.createNewToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadRP5ruToolStripMenuItem,
            this.downloadNASAToolStripMenuItem,
            this.importTextToolStripMenuItem,
            this.openFileToolStripMenuItem1});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(289, 22);
            this.openToolStripMenuItem.Text = "Открыть";
            // 
            // downloadRP5ruToolStripMenuItem
            // 
            this.downloadRP5ruToolStripMenuItem.Name = "downloadRP5ruToolStripMenuItem";
            this.downloadRP5ruToolStripMenuItem.Size = new System.Drawing.Size(290, 22);
            this.downloadRP5ruToolStripMenuItem.Text = "Загрузить ряд с Расписания погоды";
            this.downloadRP5ruToolStripMenuItem.Click += new System.EventHandler(this.downloadRP5ruToolStripMenuItem_Click);
            // 
            // downloadNASAToolStripMenuItem
            // 
            this.downloadNASAToolStripMenuItem.Name = "downloadNASAToolStripMenuItem";
            this.downloadNASAToolStripMenuItem.Size = new System.Drawing.Size(290, 22);
            this.downloadNASAToolStripMenuItem.Text = "Загрузить ряд из СБД NASA";
            this.downloadNASAToolStripMenuItem.Click += new System.EventHandler(this.downloadNASAToolStripMenuItem_Click);
            // 
            // importTextToolStripMenuItem
            // 
            this.importTextToolStripMenuItem.Name = "importTextToolStripMenuItem";
            this.importTextToolStripMenuItem.Size = new System.Drawing.Size(290, 22);
            this.importTextToolStripMenuItem.Text = "Импортировать текстовый файл";
            this.importTextToolStripMenuItem.Click += new System.EventHandler(this.importTextToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem1
            // 
            this.openFileToolStripMenuItem1.Name = "openFileToolStripMenuItem1";
            this.openFileToolStripMenuItem1.Size = new System.Drawing.Size(290, 22);
            this.openFileToolStripMenuItem1.Text = "Файл";
            this.openFileToolStripMenuItem1.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(289, 22);
            this.saveToolStripMenuItem.Text = "Сохранить";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(289, 22);
            this.saveAsToolStripMenuItem.Text = "Сохранить как";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(286, 6);
            // 
            // showMeteostationsMapToolStripMenuItem
            // 
            this.showMeteostationsMapToolStripMenuItem.Name = "showMeteostationsMapToolStripMenuItem";
            this.showMeteostationsMapToolStripMenuItem.Size = new System.Drawing.Size(289, 22);
            this.showMeteostationsMapToolStripMenuItem.Text = "Посмотреть метеостанции на карте";
            this.showMeteostationsMapToolStripMenuItem.Click += new System.EventHandler(this.showMeteostationsMapToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(286, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(289, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkRangeToolStripMenuItem,
            this.repairRangeToolStripMenuItem,
            this.modelRangeToolStripMenuItem,
            this.rangePropertiesToolStripMenuItem});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // checkRangeToolStripMenuItem
            // 
            this.checkRangeToolStripMenuItem.Name = "checkRangeToolStripMenuItem";
            this.checkRangeToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.checkRangeToolStripMenuItem.Text = "Проверить ряд";
            this.checkRangeToolStripMenuItem.ToolTipText = "Проверка ряда на наличие пропусков и некорректных данных";
            this.checkRangeToolStripMenuItem.Click += new System.EventHandler(this.checkRepairRangeToolStripMenuItem_Click);
            // 
            // repairRangeToolStripMenuItem
            // 
            this.repairRangeToolStripMenuItem.Name = "repairRangeToolStripMenuItem";
            this.repairRangeToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.repairRangeToolStripMenuItem.Text = "Восстановить ряд";
            this.repairRangeToolStripMenuItem.Click += new System.EventHandler(this.repairRangeToolStripMenuItem_Click);
            // 
            // modelRangeToolStripMenuItem
            // 
            this.modelRangeToolStripMenuItem.Name = "modelRangeToolStripMenuItem";
            this.modelRangeToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.modelRangeToolStripMenuItem.Text = "Моделировать ряд";
            this.modelRangeToolStripMenuItem.ToolTipText = "Восстановление ряда до заданного интервала наблюдений";
            this.modelRangeToolStripMenuItem.Click += new System.EventHandler(this.modelRangeToolStripMenuItem_Click);
            // 
            // rangePropertiesToolStripMenuItem
            // 
            this.rangePropertiesToolStripMenuItem.Name = "rangePropertiesToolStripMenuItem";
            this.rangePropertiesToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.rangePropertiesToolStripMenuItem.Text = "Свойства ряда";
            this.rangePropertiesToolStripMenuItem.Click += new System.EventHandler(this.rangePropertiesToolStripMenuItem_Click);
            // 
            // операцииToolStripMenuItem
            // 
            this.операцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.equalizeRangesToolStripMenuItem,
            this.calculateEnergyInfoToolStripMenuItem,
            this.ToolStripMenuItemCalcYear,
            this.toolStripSeparator1,
            this.optionsToolStripMenuItem,
            this.loadDataToolStripMenuItem});
            this.операцииToolStripMenuItem.Name = "операцииToolStripMenuItem";
            this.операцииToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.операцииToolStripMenuItem.Text = "Операции";
            // 
            // equalizeRangesToolStripMenuItem
            // 
            this.equalizeRangesToolStripMenuItem.Name = "equalizeRangesToolStripMenuItem";
            this.equalizeRangesToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.equalizeRangesToolStripMenuItem.Text = "Привести ряды";
            this.equalizeRangesToolStripMenuItem.ToolTipText = "Для каждого наблюдения из ряда с большим интервалом подобрать наблюдение из ряда " +
    "с меньшим интервалом и сохранить";
            this.equalizeRangesToolStripMenuItem.Click += new System.EventHandler(this.equalizeRangesToolStripMenuItem_Click);
            // 
            // calculateEnergyInfoToolStripMenuItem
            // 
            this.calculateEnergyInfoToolStripMenuItem.Name = "calculateEnergyInfoToolStripMenuItem";
            this.calculateEnergyInfoToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.calculateEnergyInfoToolStripMenuItem.Text = "Энергетические характеристики";
            this.calculateEnergyInfoToolStripMenuItem.ToolTipText = "Рассчитать основные энергетические характеристике ветра";
            this.calculateEnergyInfoToolStripMenuItem.Click += new System.EventHandler(this.calculateEnergyInfoToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemCalcYear
            // 
            this.ToolStripMenuItemCalcYear.Name = "ToolStripMenuItemCalcYear";
            this.ToolStripMenuItemCalcYear.Size = new System.Drawing.Size(266, 22);
            this.ToolStripMenuItemCalcYear.Text = "Выбор расчётного года";
            this.ToolStripMenuItemCalcYear.Click += new System.EventHandler(this.toolStripMenuItemCalcYear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(263, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.optionsToolStripMenuItem.Text = "Настройки";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // loadDataToolStripMenuItem
            // 
            this.loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.loadDataToolStripMenuItem.Text = "Загрузка данных";
            this.loadDataToolStripMenuItem.Click += new System.EventHandler(this.loadDataToolStripMenuItem_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewToolStripButton,
            this.openFileToolStripButton,
            this.saveToolStripButton,
            this.saveAlltoolStripButton,
            this.toolStripSeparator});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1157, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // createNewToolStripButton
            // 
            this.createNewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.createNewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("createNewToolStripButton.Image")));
            this.createNewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createNewToolStripButton.Name = "createNewToolStripButton";
            this.createNewToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.createNewToolStripButton.Text = "&Создать";
            this.createNewToolStripButton.Click += new System.EventHandler(this.createNewToolStripMenuItem_Click);
            // 
            // openFileToolStripButton
            // 
            this.openFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openFileToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openFileToolStripButton.Image")));
            this.openFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFileToolStripButton.Name = "openFileToolStripButton";
            this.openFileToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.openFileToolStripButton.Text = "&Открыть";
            this.openFileToolStripButton.ToolTipText = "Открыть файл";
            this.openFileToolStripButton.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.saveToolStripButton.Text = "&Сохранить";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAlltoolStripButton
            // 
            this.saveAlltoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveAlltoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveAlltoolStripButton.Image")));
            this.saveAlltoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAlltoolStripButton.Name = "saveAlltoolStripButton";
            this.saveAlltoolStripButton.Size = new System.Drawing.Size(24, 24);
            this.saveAlltoolStripButton.Text = "toolStripButton1";
            this.saveAlltoolStripButton.ToolTipText = "Сохранить все";
            this.saveAlltoolStripButton.Click += new System.EventHandler(this.saveAlltoolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ContextMenuStrip = this.contextMenuStripRangeIntervals;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelRangeCount,
            this.toolStripStatusLabelCompletness,
            this.toolStripStatusLabelInterval});
            this.statusStrip1.Location = new System.Drawing.Point(0, 553);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1157, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // contextMenuStripRangeIntervals
            // 
            this.contextMenuStripRangeIntervals.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripRangeIntervals.Name = "contextMenuStripRangeIntervals";
            this.contextMenuStripRangeIntervals.Size = new System.Drawing.Size(61, 4);
            // 
            // toolStripStatusLabelRangeCount
            // 
            this.toolStripStatusLabelRangeCount.Name = "toolStripStatusLabelRangeCount";
            this.toolStripStatusLabelRangeCount.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelRangeCount.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabelCompletness
            // 
            this.toolStripStatusLabelCompletness.Name = "toolStripStatusLabelCompletness";
            this.toolStripStatusLabelCompletness.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelCompletness.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabelInterval
            // 
            this.toolStripStatusLabelInterval.IsLink = true;
            this.toolStripStatusLabelInterval.Name = "toolStripStatusLabelInterval";
            this.toolStripStatusLabelInterval.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelInterval.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabelInterval.Click += new System.EventHandler(this.toolStripStatusLabelInterval_Click);
            this.toolStripStatusLabelInterval.MouseEnter += new System.EventHandler(this.toolStripStatusLabelInterval_MouseEnter);
            this.toolStripStatusLabelInterval.MouseLeave += new System.EventHandler(this.toolStripStatusLabelInterval_MouseLeave);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.mainTabControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 52);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1157, 501);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.HotTrack = true;
            this.mainTabControl.ItemSize = new System.Drawing.Size(70, 20);
            this.mainTabControl.Location = new System.Drawing.Point(3, 3);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.ShowToolTips = true;
            this.mainTabControl.Size = new System.Drawing.Size(1151, 495);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.mainTabControl_Selected);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(251, 26);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 575);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Wind Energy";
            this.Activated += new System.EventHandler(this.FormMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.Shown += new System.EventHandler(this.formMain_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem операцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        public Ext.TabControlExt mainTabControl;
        private System.Windows.Forms.ToolStripMenuItem equalizeRangesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadRP5ruToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton createNewToolStripButton;
        private System.Windows.Forms.ToolStripButton openFileToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem createNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton saveAlltoolStripButton;
        private System.Windows.Forms.ToolStripMenuItem downloadNASAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateEnergyInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRangeCount;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCompletness;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelInterval;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripRangeIntervals;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCalcYear;
        private System.Windows.Forms.ToolStripMenuItem modelRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rangePropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repairRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem showMeteostationsMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTextToolStripMenuItem;
    }
}
