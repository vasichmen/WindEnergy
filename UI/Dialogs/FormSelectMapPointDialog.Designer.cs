namespace WindEnergy.UI.Dialogs
{
    partial class FormSelectMapPointDialog
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
                if(lay!=null)
                lay.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectMapPointDialog));
            this.gmapControlMap = new GMap.NET.WindowsForms.GMapControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.OKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxSearch = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxLat = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxLon = new System.Windows.Forms.ToolStripTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCoordinates = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gmapControlMap
            // 
            this.gmapControlMap.Bearing = 0F;
            this.gmapControlMap.CanDragMap = true;
            this.gmapControlMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gmapControlMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmapControlMap.GrayScaleMode = false;
            this.gmapControlMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmapControlMap.LevelsKeepInMemmory = 5;
            this.gmapControlMap.Location = new System.Drawing.Point(0, 29);
            this.gmapControlMap.MarkersEnabled = true;
            this.gmapControlMap.MaxZoom = 17;
            this.gmapControlMap.MinZoom = 2;
            this.gmapControlMap.MouseWheelZoomEnabled = true;
            this.gmapControlMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmapControlMap.Name = "gmapControlMap";
            this.gmapControlMap.NegativeMode = false;
            this.gmapControlMap.PolygonsEnabled = true;
            this.gmapControlMap.RetryLoadTile = 0;
            this.gmapControlMap.RoutesEnabled = true;
            this.gmapControlMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmapControlMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmapControlMap.ShowTileGridLines = false;
            this.gmapControlMap.Size = new System.Drawing.Size(806, 443);
            this.gmapControlMap.TabIndex = 0;
            this.gmapControlMap.Zoom = 0D;
            this.gmapControlMap.OnPositionChanged += new GMap.NET.PositionChanged(this.gmapControlMap_OnPositionChanged);
            this.gmapControlMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gmapControlMap_MouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OKToolStripMenuItem,
            this.cancelToolStripMenuItem,
            this.toolStripComboBoxSearch,
            this.toolStripMenuItem1,
            this.toolStripTextBoxLat,
            this.toolStripMenuItem2,
            this.toolStripTextBoxLon});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(806, 29);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // OKToolStripMenuItem
            // 
            this.OKToolStripMenuItem.AutoToolTip = true;
            this.OKToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.OKToolStripMenuItem.Name = "OKToolStripMenuItem";
            this.OKToolStripMenuItem.Size = new System.Drawing.Size(44, 25);
            this.OKToolStripMenuItem.Text = "ОК";
            this.OKToolStripMenuItem.Click += new System.EventHandler(this.OKToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(81, 25);
            this.cancelToolStripMenuItem.Text = "Отмена";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // toolStripComboBoxSearch
            // 
            this.toolStripComboBoxSearch.Name = "toolStripComboBoxSearch";
            this.toolStripComboBoxSearch.Size = new System.Drawing.Size(300, 25);
            this.toolStripComboBoxSearch.ToolTipText = "Начните вводить адрес...";
            this.toolStripComboBoxSearch.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearch_SelectionChangeCommitted);
            this.toolStripComboBoxSearch.TextUpdate += new System.EventHandler(this.comboBoxSearch_TextUpdate);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(65, 25);
            this.toolStripMenuItem1.Text = "Широта:";
            // 
            // toolStripTextBoxLat
            // 
            this.toolStripTextBoxLat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxLat.Name = "toolStripTextBoxLat";
            this.toolStripTextBoxLat.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxLat.TextChanged += new System.EventHandler(this.toolStripTextBoxLat_TextChanged);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(67, 25);
            this.toolStripMenuItem2.Text = "Долгота:";
            // 
            // toolStripTextBoxLon
            // 
            this.toolStripTextBoxLon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxLon.Name = "toolStripTextBoxLon";
            this.toolStripTextBoxLon.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxLon.TextChanged += new System.EventHandler(this.toolStripTextBoxLat_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCoordinates});
            this.statusStrip1.Location = new System.Drawing.Point(0, 450);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(806, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCoordinates
            // 
            this.toolStripStatusLabelCoordinates.Name = "toolStripStatusLabelCoordinates";
            this.toolStripStatusLabelCoordinates.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelCoordinates.Text = "toolStripStatusLabel1";
            // 
            // FormSelectMapPointDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 472);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gmapControlMap);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(822, 511);
            this.Name = "FormSelectMapPointDialog";
            this.ShowInTaskbar = false;
            this.Text = "FormSelectMapPointDialog";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSelectMapPointDialog_FormClosed);
            this.Shown += new System.EventHandler(this.FormSelectMapPointDialog_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmapControlMap;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem OKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSearch;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxLat;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxLon;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCoordinates;
    }
}