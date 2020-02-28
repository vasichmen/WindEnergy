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
using WindEnergy.WindLib.Classes;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.UI.Ext;
using WindEnergy.UI.Properties;
using CommonLib.Classes;
using CommonLib;
using WindLib;

namespace WindEnergy.UI.Tools
{
    public partial class FormShowMeteostationsMap : Form
    {
        bool IsDialog;

        /// <summary>
        /// видимый слой карты
        /// </summary>
        private GMapOverlay lay;

        public RP5MeteostationInfo Result { get; private set; }

        public FormShowMeteostationsMap(bool isDialog=false)
        {
            IsDialog = isDialog;
            DialogResult = DialogResult.None;
            InitializeComponent();
            ConfigureGMapControl();
        }

        /// <summary>
        /// создание окна с выделенной метеостанцией на карте
        /// </summary>
        /// <param name="meteostation"></param>
        public FormShowMeteostationsMap(RP5MeteostationInfo meteostation) : this()
        {
            meteostation = meteostation ?? throw new ArgumentNullException(nameof(meteostation));
            gmapControlMap.Position = meteostation.Position;
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
            gmapControlMap.Position = PointLatLng.Empty;

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
            _ = Directory.CreateDirectory(Vars.Options.CacheFolder);
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
            if (gmapControlMap.Position.IsEmpty)
                gmapControlMap.Position = new PointLatLng(55.35, 37.45);
            showVisibleMeteostations();
            toolStripStatusLabelStats.Text = $"Аэропортов: {Vars.RP5Meteostations.AirportCount} шт., метеостанций: {Vars.RP5Meteostations.MeteostationsCount} шт., всего: {Vars.RP5Meteostations.TotalCount} шт.";
        }

        /// <summary>
        /// выводит на карту только видимые метеостанции
        /// </summary>
        private void showVisibleMeteostations()
        {
            //загрузка списка метеостанций
            if (lay != null)
            {
                //расчет областей экрана
                int h = 15; //кол-во по высоте
                int w = 15; //кол-во по ширине
                double hd = gmapControlMap.ViewArea.HeightLat; //высота в градусах
                double wd = gmapControlMap.ViewArea.WidthLng; //ширина в градусах
                double sh = hd / h; // шаг по высоте
                double sw = wd / w; //шаг по ширине

                RP5MeteostationInfo[][] mts_map = new RP5MeteostationInfo[h][];
                for (int i = 0; i < h; i++) mts_map[i] = new RP5MeteostationInfo[w];

                RectLatLng[][] rec_map = new RectLatLng[h][];
                for (int i = 0; i < h; i++) rec_map[i] = new RectLatLng[w];

                for (int i = 0; i < h; i++)
                    for (int j = 0; j < w; j++)
                    {
                        //левый верхний угол
                        double lat = gmapControlMap.ViewArea.Lat - (i) * sh;
                        double lng = gmapControlMap.ViewArea.Lng + (j) * sw;
                        rec_map[i][j] = new RectLatLng(lat, lng, sw, sh);
                    }

                //выборка точек из БД
                List<RP5MeteostationInfo> pts = new List<RP5MeteostationInfo>();
                foreach (var mts in Vars.RP5Meteostations.List)
                {
                    if (gmapControlMap.ViewArea.Contains(mts.Position))
                    {
                        pts.Add(mts); //добавление в общий список

                        //поиск по всем областям
                        bool exit = false;
                        for (int i = 0; i < h && !exit; i++)
                            for (int j = 0; j < w; j++)
                                // если попадает в маленькую область и эта область ещё не заполнена
                                if (rec_map[i][j].Contains(mts.Position) && mts_map[i][j] == null)
                                {//добавляем на карту и выходим
                                    mts_map[i][j] = mts;
                                    exit = true;
                                    break;
                                }

                    }
                }

                List<RP5MeteostationInfo> res;
                if (pts.Count < h * w)
                    res = pts;
                else
                {
                    res = new List<RP5MeteostationInfo>();
                    for (int i = 0; i < mts_map.Length; i++)
                        if (mts_map[i] != null)
                            foreach (var dd in mts_map[i])
                                if (dd != null)
                                    res.Add(dd);
                }

                //вывод на карту
                lay.Clear();
                showVisibleAMS();//вывод АМС без фильтрации, их мало
                foreach (var a in res)
                    showMarker(a.Position, a.Name, a);
            }
        }

        /// <summary>
        /// вывод на карту всех АМС
        /// </summary>
        private void showVisibleAMS()
        {
            //выборка точек из БД
            foreach (var ams in Vars.AMSMeteostations.List)
            {
                if (gmapControlMap.ViewArea.Contains(ams.Position))
                {
                    showMarker(ams.Position, "АМС " + ams.Name, ams);
                }
            }
        }

        /// <summary>
        /// вывод маркетра на карту
        /// </summary>
        /// <param name="cled">координаты нового маркера</param>
        private void showMarker(PointLatLng cled, string ttip, object tag)
        {
            Icon mark = null;
            if (tag.GetType() == typeof(RP5MeteostationInfo))
                mark = Resources.rp5_marker;
            else if (tag.GetType() == typeof(AMSMeteostationInfo))
                mark = Resources.ams_marker;

            Point offsets = new Point(0, -16);
            MapMarker mar = new MapMarker(cled, mark, offsets);
            mar.ToolTipText = ttip;
            mar.IsHitTestVisible = true;
            mar.Tag = tag;
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

        private void gmapControlMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            item = item ?? throw new ArgumentNullException(nameof(item));
            if (item.Tag.GetType() != typeof(RP5MeteostationInfo)) return;
            RP5MeteostationInfo mi = (RP5MeteostationInfo)item.Tag;

            if (IsDialog) {
                Result = mi;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                FormLoadFromRP5 frm = new FormLoadFromRP5(mi);
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    RawRange res = frm.Result;
                    TabPageExt tab = Program.winMain.mainTabControl.OpenNewTab(res, res.Name);
                    tab.HasNotSavedChanges = true;
                    _ = Program.winMain.Focus();
                }
            }
        }

        private void FormShowMeteostationsMap_Resize(object sender, EventArgs e)
        {
            showVisibleMeteostations();
        }

        private void OKToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            
        }

        private void FormShowMeteostationsMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
                DialogResult = DialogResult.Cancel;
        }
    }
}
