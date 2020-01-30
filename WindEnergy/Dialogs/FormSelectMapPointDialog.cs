using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.WindLib.Classes;
using WindEnergy.WindLib.Data.Providers.InternetServices;
using WindEnergy.UI.Properties;
using CommonLib.Classes;
using CommonLib;
using CommonLibLib.Data.Providers.InternetServices;
using WindLib;

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
        private PointLatLng initialPoint;
        private Arcgis searcher;
        private Dictionary<string, PointLatLng> adressess;

        public FormSelectMapPointDialog(string caption, PointLatLng initialPoint)
        {
            InitializeComponent();
            Text = caption;
            ConfigureGMapControl();
            this.initialPoint = initialPoint;
            if (!initialPoint.IsEmpty)
                gmapControlMap.Position = initialPoint;
            else
                gmapControlMap.Position = new PointLatLng(55.75, 37.62);

            gmapControlMap_OnPositionChanged(gmapControlMap.Position);
            toolStripTextBoxLat.Text = gmapControlMap.Position.Lat.ToString();
            toolStripTextBoxLon.Text = gmapControlMap.Position.Lng.ToString();
            DialogResult = DialogResult.None;
            searcher = new Arcgis(Vars.Options.CacheFolder + "\\arcgis");
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

            gmapControlMap.DisableFocusOnMouseEnter = true;

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
            _ = Directory.CreateDirectory(Vars.Options.CacheFolder);
            gmapControlMap.CacheLocation = Vars.Options.CacheFolder;

            #endregion


            lay = new GMapOverlay();
            gmapControlMap.Overlays.Add(lay);

        }

        private void gmapControlMap_MouseClick(object sender, MouseEventArgs e)
        {
            PointLatLng cled = gmapControlMap.FromLocalToLatLng(e.X, e.Y);
            toolStripTextBoxLat.Text = cled.Lat.ToString();
            toolStripTextBoxLon.Text = cled.Lng.ToString();
            ShowMarker(cled);
        }

        private void ShowMarker(PointLatLng cled)
        {
            if (gmapControlMap.IsDragging)
                return;
            lay.Clear();
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
                _ = MessageBox.Show("Выберите точку на карте!");
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

        private void FormSelectMapPointDialog_Shown(object sender, EventArgs e)
        {
            if (!initialPoint.IsEmpty)
                ShowMarker(initialPoint);
        }

        /// <summary>
        /// фильтрация по началу города
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSearch_TextUpdate(object sender, EventArgs e)
        {
            string curTextBox = toolStripComboBoxSearch.Text.Trim();
            updateSearchListAsync(curTextBox);
        }

        /// <summary>
        /// асинхронная загрузка списка адресов метеостанций 
        /// </summary>
        /// <param name="query">запрос</param>
        private async void updateSearchListAsync(string query)
        {
            if (query.Length < 2)
                return;

            //действие обновления списка подсказок
            Action<Dictionary<string, PointLatLng>> updList = new Action<Dictionary<string, PointLatLng>>((list) =>
            {
                toolStripComboBoxSearch.Items.Clear();
                toolStripComboBoxSearch.Items.AddRange(list.Keys.ToArray());
                this.adressess = list;
                toolStripComboBoxSearch.SelectionStart = toolStripComboBoxSearch.Text.Length;
            });
            try
            {
                Dictionary<string, PointLatLng> results;
                await Task.Run(() =>
                {
                    Thread.Sleep(1000); //ждем 1 с

                    //получаем новый текст 
                    string curTextBox = "";
                    if (InvokeRequired)
                        _ = Invoke(new Action(() => { curTextBox = toolStripComboBoxSearch.Text.Trim(); }));
                    else
                        curTextBox = toolStripComboBoxSearch.Text.Trim();

                    if (query != curTextBox) //если этот текст  изменился, то выходим
                        return;

                    results = searcher.GetAddresses(query);
                    //обновление списка
                    if (InvokeRequired)
                        _ = Invoke(updList, results);
                    else
                        updList.Invoke(results);

                }).ConfigureAwait(false);
            }
            catch (WebException)
            {
                _ = MessageBox.Show(this, "Ошибка подключения, проверьте соединение с Интернет", "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ApplicationException exc)
            {
                _ = MessageBox.Show(this, exc.Message, "Загрузка ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Debug.WriteLine("updateList end");
        }

        /// <summary>
        /// проверка после выбора метеостанции 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string addr = toolStripComboBoxSearch.SelectedItem as string;
            if (adressess.ContainsKey(addr))
            {
                PointLatLng point = adressess[addr];
                gmapControlMap.Position = point;
            }
        }

        private void toolStripTextBoxLat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double lat = double.Parse(toolStripTextBoxLat.Text.Replace('.', Constants.DecimalSeparator).Replace('.', Constants.DecimalSeparator));
                double lon = double.Parse(toolStripTextBoxLon.Text.Replace('.', Constants.DecimalSeparator).Replace('.', Constants.DecimalSeparator));
                gmapControlMap.Position = new PointLatLng(lat, lon);
            }
            catch (Exception)
            { }
        }


        /// <summary>
        /// изменение подписей при движении карты
        /// </summary>
        /// <param name="point"></param>
        private void gmapControlMap_OnPositionChanged(PointLatLng point)
        {
            toolStripStatusLabelCoordinates.Text = $"Широта: {point.Lat.ToString("0.00000")}, долгота: {point.Lng.ToString("0.00000")}";
        }
    }
}
