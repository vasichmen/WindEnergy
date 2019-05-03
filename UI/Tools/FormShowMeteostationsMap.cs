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

namespace WindEnergy.UI.Tools
{
    public partial class FormShowMeteostationsMap : Form
    {
        /// <summary>
        /// видимый слой карты
        /// </summary>
        private GMapOverlay lay;

        public FormShowMeteostationsMap()
        {
            InitializeComponent();
            ConfigureGMapControl();
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
            gmapControlMap.Zoom = 9;

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
            switch (Vars.Options.MapProvider)
            {
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

        /// <summary>
        /// загрузка и вывод на карту списка метеостанций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormShowMeteostationsMap_Shown(object sender, EventArgs e)
        {
            gmapControlMap.Position = new PointLatLng(55.35, 37.45);
            showVisibleMeteostations();
        }

        /// <summary>
        /// выводит на карту только видимые метеостанции
        /// </summary>
        private void showVisibleMeteostations()
        {
            //загрузка списка метеостанций
            if (lay != null)
            {
                lay.Clear();
                foreach (var mts in Vars.LocalFileSystem.MeteostationList)
                {
                    if (gmapControlMap.ViewArea.Contains(mts.Coordinates))
                        ShowMarker(mts.Coordinates, mts.Name);
                }
            }
        }

        /// <summary>
        /// вывод маркетра на карту
        /// </summary>
        /// <param name="cled">координаты нового маркера</param>
        private void ShowMarker(PointLatLng cled, string ttip)
        {
            Point offsets = new Point(0, -16);
            MapMarker mar = new MapMarker(cled, Resources.marker, offsets);
            mar.ToolTipText = ttip;
            mar.IsHitTestVisible = true;
            lay.Markers.Add(mar);
        }

        private void gmapControlMap_OnMapZoomChanged()
        {
            showVisibleMeteostations();
        }
        
        private void gmapControlMap_SizeChanged(object sender, EventArgs e)
        {
            showVisibleMeteostations();
        }

        private void gmapControlMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == gmapControlMap.DragButton)
                showVisibleMeteostations();
        }
    }
}
