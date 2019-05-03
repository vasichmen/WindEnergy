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
            this.gmapControlMap.Size = new System.Drawing.Size(1122, 694);
            this.gmapControlMap.TabIndex = 0;
            this.gmapControlMap.Zoom = 0D;
            this.gmapControlMap.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.gmapControlMap_OnMapZoomChanged);
            this.gmapControlMap.SizeChanged += new System.EventHandler(this.gmapControlMap_SizeChanged);
            this.gmapControlMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gmapControlMap_MouseUp);
            // 
            // FormShowMeteostationsMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 694);
            this.Controls.Add(this.gmapControlMap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormShowMeteostationsMap";
            this.Text = "Просмотр карты метеостанций";
            this.Shown += new System.EventHandler(this.FormShowMeteostationsMap_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmapControlMap;
    }
}