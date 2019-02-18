using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes;
using WindEnergy.UI.Properties;

namespace WindEnergy.UI.Dialogs
{
    /// <summary>
    /// выбор точки на карте
    /// </summary>
    public partial class FormSelectMapPointDialog : Form
    {
        public PointLatLng Result { get; set; }
        private PointLatLng cPoint;
        private GMapOverlay lay;

        public FormSelectMapPointDialog(string caption, PointLatLng initialPoint)
        {
            InitializeComponent();
            Text = caption;
            ConfigureGMapControl();
            if (!initialPoint.IsEmpty)
                gmapControlMap.Position = initialPoint;
            else
                gmapControlMap.Position = new PointLatLng(55, 37);
            DialogResult = DialogResult.None;
        }

        /// <summary>
        /// настройки браузера карты
        /// </summary>
        public void ConfigureGMapControl()
        {

            #region системные настройки

            gmapControlMap.DragButton = MouseButtons.Left;

            //порядок получения данных 
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            GMaps.Instance.CacheOnIdleRead = true;
            GMaps.Instance.BoostCacheEngine = true;
            GMaps.Instance.MemoryCache.Capacity = 200; //максимальный размер кэша в МБ

            //zoom
            gmapControlMap.Zoom = 7;

            //информация о масштабе карты
            gmapControlMap.MapScaleInfoEnabled = false;

            //включение кэша карт, маршрутов итд
            gmapControlMap.Manager.UseDirectionsCache = true;
            gmapControlMap.Manager.UseGeocoderCache = true;
            gmapControlMap.Manager.UseMemoryCache = true;
            gmapControlMap.Manager.UsePlacemarkCache = true;
            gmapControlMap.Manager.UseRouteCache = true;
            gmapControlMap.Manager.UseUrlCache = true;

            //отключение черно-белого режима
            gmapControlMap.GrayScaleMode = false;

            //заполнение отсутствующих тайлов из меньшего масштаба
            gmapControlMap.FillEmptyTiles = true;

            //язык карты
            GMapProvider.Language = LanguageType.Russian;


            //поставщик карты
            switch (Vars.Options.MapProvider) {
                case MapProviders.GoogleSatellite:
                    gmapControlMap.MapProvider = GMapProviders.GoogleSatelliteMap;
                    break;
                case MapProviders.OpenStreetMap:
                    gmapControlMap.MapProvider = GMapProviders.OpenStreetMap;
                    break;
                case MapProviders.YandexMap:
                    gmapControlMap.MapProvider = GMapProviders.YandexMap;
                    break;
                default:
                    throw new Exception("Этот тип карты не реализован");
            }


            //Если вы используете интернет через прокси сервер,
            //указываем свои учетные данные.
            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;


            //вид пустых тайлов
            gmapControlMap.EmptyMapBackground = Color.White;
            gmapControlMap.EmptyTileColor = Color.White;
            gmapControlMap.EmptyTileText = "Не удалось загрузить изображение \r\n Возможно, проблема с интернет-соединением или попробуйте уменьшить масштаб.";
            gmapControlMap.MapScaleInfoEnabled = true;

            //папка с кэшем
            Directory.CreateDirectory(Vars.Options.CacheFolder);
            gmapControlMap.CacheLocation = Vars.Options.CacheFolder;

            #endregion


            lay = new GMapOverlay();
            gmapControlMap.Overlays.Add(lay);

        }

        private void gmapControlMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (gmapControlMap.IsDragging)
                return;
            lay.Clear();
            PointLatLng cled = gmapControlMap.FromLocalToLatLng(e.X, e.Y);
            cPoint = cled;
            Point offsets = new Point(0, -16);
            MapMarker mar = new MapMarker(cled, Resources.marker, offsets);

            mar.IsHitTestVisible = true;

            lay.Markers.Add(mar);
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Result = PointLatLng.Empty;
            Close();
        }

        private void OKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cPoint.IsEmpty)
            {
                return;
            }
            Result = cPoint;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FormSelectMapPointDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.None)
                DialogResult = DialogResult.Cancel;
        }
    }
}
