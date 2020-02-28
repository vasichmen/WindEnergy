namespace WindEnergy.UI.Tools
{
    partial class FormShowMeteostationsMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShowMeteostationsMap));
            this.gmapControlMap = new GMap.NET.WindowsForms.GMapControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStats = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.gmapControlMap.Location = new System.Drawing.Point(0, 0);
            this.gmapControlMap.Margin = new System.Windows.Forms.Padding(2);
            this.gmapControlMap.MarkersEnabled = true;
            this.gmapControlMap.MaxZoom = 10;
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
            this.gmapControlMap.Size = new System.Drawing.Size(842, 564);
            this.gmapControlMap.TabIndex = 0;
            this.gmapControlMap.Zoom = 0D;
            this.gmapControlMap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmapControlMap_OnMarkerClick);
            this.gmapControlMap.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.gmapControlMap_OnMapZoomChanged);
            this.gmapControlMap.SizeChanged += new System.EventHandler(this.gmapControlMap_SizeChanged);
            this.gmapControlMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gmapControlMap_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStats});
            this.statusStrip1.Location = new System.Drawing.Point(0, 542);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(842, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStats
            // 
            this.toolStripStatusLabelStats.Name = "toolStripStatusLabelStats";
            this.toolStripStatusLabelStats.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelStats.Text = "toolStripStatusLabel1";
            // 
            // FormShowMeteostationsMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 564);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gmapControlMap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormShowMeteostationsMap";
            this.Text = "Просмотр карты метеостанций";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormShowMeteostationsMap_FormClosing);
            this.Shown += new System.EventHandler(this.FormShowMeteostationsMap_Shown);
            this.Resize += new System.EventHandler(this.FormShowMeteostationsMap_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmapControlMap;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStats;
    }
}