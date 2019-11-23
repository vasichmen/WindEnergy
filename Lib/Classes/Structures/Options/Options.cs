using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WindEnergy.Lib.Statistic.Structures;

namespace WindEnergy.Lib.Classes.Structures.Options
{
    /// <summary>
    /// Настройки программы
    /// </summary>
    [Serializable]
    public class Options
    {
        /// <summary>
        /// создает новый экземпляр
        /// </summary>
        public Options()
        {
            TempFolder = Application.StartupPath + "\\tmp";
            LastDirectory = Application.StartupPath;
            CacheFolder = Application.StartupPath + "\\cache";
            MapProvider = MapProviders.YandexMap;
            StaticRegionLimitsSourceFile = Application.StartupPath + "\\Data\\staticRegionLimits.txt";
            StaticMeteostationCoordinatesSourceFile = Application.StartupPath + "\\Data\\staticMeteostationCoordinates.txt";
            StaticAMSDatabaseSourceFile = Application.StartupPath + "\\Data\\AMS.database.txt";
            StaticFlugerDatabaseSourceFile = Application.StartupPath + "\\Data\\Fluger.database.txt";
            StaticEquipmentDatabaseSourceFile = Application.StartupPath + "\\Data\\Equipment.database.txt";
            QualifierSectionLength = 20; //20 измерений
            QualifierDaysToNewInterval = 30; //1 месяц
            QualifierDaysToBeginMissing = 30; //1 месяца
            AirDensity = 1.226;
            MinimalSpeedDeviation = 0.1d;
            MinimalCorrelationCoeff = 0.7;
            MinimalCorrelationControlParametres = new List<MeteorologyParameters>() { MeteorologyParameters.Speed };
            NearestMSRadius = 50e3;//50 km
            NormalLawPirsonCoefficientDiapason = new Diapason<double>(0, 5);
            CurrentSpeedGradationType = GradationTypes.Voeykow;
            UserSpeedGradation = new UserGradation();
            TextImportState = new TextImporterState();
            CalculateAirDensity = false;
            ETOPO2Folder = Application.StartupPath + "\\Data\\ETOPO";
            RP5SearchEngine = RP5SearchEngine.DBSearch;
        }

        /// <summary>
        /// источник поиска по метеостанциям РП5
        /// </summary>
        public RP5SearchEngine RP5SearchEngine { get; set; }

        /// <summary>
        /// адрес файла настроек
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// временная папка 
        /// </summary>
        public string TempFolder { get; }

        /// <summary>
        /// папка кэша программы
        /// </summary>
        public string CacheFolder { get; set; }

        /// <summary>
        /// последняя папка сохранения или открытия файла
        /// </summary>
        public string LastDirectory { get; set; }

        /// <summary>
        /// тип карты при выборе точек
        /// </summary>
        public MapProviders MapProvider { get; set; }

        /// <summary>
        /// адрес файла с данными об ограничениях скоростей ветра по регионам
        /// </summary>
        public string StaticRegionLimitsSourceFile { get; set; }

        /// <summary>
        /// адрес файла списка метеостанций и координат
        /// </summary>
        public string StaticMeteostationCoordinatesSourceFile { get; set; }

        /// <summary>
        /// адрес файла коэффициентов перевода скорости ветра на высоту
        /// </summary>
        public string StaticAMSDatabaseSourceFile { get; set; }

        /// <summary>
        /// адрес файла коэффициентов местности
        /// </summary>
        public string StaticFlugerDatabaseSourceFile { get; set; }

        /// <summary>
        /// адрес файла БД оборудования
        /// </summary>
        public string StaticEquipmentDatabaseSourceFile { get; set; }

        /// <summary>
        /// длина отрезка (количество измерений) при разбиении на промежутки для поиска разделов интервалов наблюдений
        /// </summary>
        public int QualifierSectionLength { get; set; }

        /// <summary>
        /// минимальное количество дней измерений с одинаковым интервалом, чтоб это можно было считать новым дипазоном измерений
        /// </summary>
        public int QualifierDaysToNewInterval { get; set; }


        /// <summary>
        /// минимальная разница отклонений скоростей м/с при которой они считаются равными
        /// </summary>
        public double MinimalSpeedDeviation { get; set; }

        /// <summary>
        /// плотность воздуха кг/м3
        /// </summary>
        public double AirDensity { get; set; }

        /// <summary>
        /// максимальное расстоние в метрах для поиска ближайших МС
        /// </summary>
        public double NearestMSRadius { get; set; }

        /// <summary>
        /// минимальный коэффициент корреляции для достаточной точности предсказания
        /// </summary>
        public double MinimalCorrelationCoeff { get; set; }

        /// <summary>
        /// список параметров, для которых будет проверяться MinimalCorrelationCoeff
        /// </summary>
        public List<MeteorologyParameters> MinimalCorrelationControlParametres { get; set; }

        /// <summary>
        /// диапазон попадания критерия Пирсона для нормального закона распределения
        /// </summary>
        public Diapason<double> NormalLawPirsonCoefficientDiapason { get; set; }

        /// <summary>
        /// тип градаций скорости при расчётах
        /// </summary>
        public GradationTypes CurrentSpeedGradationType { get; set; }

        /// <summary>
        /// возвращает  градации скорости в соответствии с текущими настройками
        /// </summary>
        public GradationInfo<GradationItem> CurrentSpeedGradation
        {
            get
            {
                switch (CurrentSpeedGradationType)
                {
                    case GradationTypes.NASA:
                        return GradationInfo<GradationItem>.NASAGradations;
                    case GradationTypes.Bofort:
                        return GradationInfo<GradationItem>.BofortGradations;
                    case GradationTypes.Voeykow:
                        return GradationInfo<GradationItem>.VoeykowGradations;
                    case GradationTypes.User:
                        return UserSpeedGradationInfo;
                    default: throw new WindEnergyException("Этот тип градаций не реализован");
                }
            }
        }

        /// <summary>
        /// GUID экземпляра программы
        /// </summary>
        public string ApplicationGuid
        {
            get
            {
#if (DEBUG)
                return "debug";
#else
                RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\WindEnergy");
                object guido = key.GetValue("Guid");
                string guid;
                if (guido == null)
                {
                    guid = Guid.NewGuid().ToString();
                    key.SetValue("Guid", guid);
                }
                else
                    guid = (string)guido;
                return guid;
#endif
            }
        }

        /// <summary>
        /// пользовательские градации скоростей
        /// </summary>
        public GradationInfo<GradationItem> UserSpeedGradationInfo
        {
            get
            {
                return new GradationInfo<GradationItem>(UserSpeedGradation.From, UserSpeedGradation.Step, UserSpeedGradation.To);
            }
        }

        /// <summary>
        /// настройки пользовательских градаций скорости
        /// </summary>
        public UserGradation UserSpeedGradation { get; set; }

        /// <summary>
        /// состояние импорта из тексстовых файлов
        /// </summary>
        public TextImporterState TextImportState { get;  set; }

        /// <summary>
        /// если истина, то в расчётах плотность воздуха будет рассчитываться по параметрам ряда и высоте над у. м.
        /// </summary>
        public bool CalculateAirDensity { get; set; }

        /// <summary>
        /// папка с базой данных ETOPO2
        /// </summary>
        public string ETOPO2Folder { get;  set; }

        /// <summary>
        /// адрес сайта
        /// </summary>
        public string SiteAddress { get { return "https://velomapa.ru/"; } }

        /// <summary>
        /// минимальное количество дней, после которого считается пропуск данных как отдельный интервал
        /// </summary>
        public double QualifierDaysToBeginMissing { get; set; }

        /// <summary>
        /// версия приложения число
        /// </summary>
        public float VersionInt
        {
            get
            {
                return Convert.ToSingle(Application.ProductVersion.Replace(".", ""));
            }
        }

        /// <summary>
        /// версия приложения в виде текста
        /// </summary>
        public string VersionText
        {
            get
            {
                return Application.ProductVersion;
            }
        }


        /// <summary>
        /// поведение диалога обновления программы
        /// </summary>
        public UpdateDialogAnswer UpdateMode { get; set; }

        /// <summary>
        /// адрес для связи в Telegram
        /// </summary>
        public string TelegramAddress { get { return "tg://resolve?domain=vasichmen"; } }

        /// <summary>
        /// адрес репозитория гитхаб
        /// </summary>
        public string GitHubRepository { get { return "https://github.com/vasichmen/WindEnergy"; } }

        /// <summary>
        /// сохранение настроек в файл
        /// </summary>
        /// <param name="filename">адрес папки, куда сохранить файл</param>
        public void Save(string filename)
        {
            if (File.Exists(filename))
                File.Delete(filename);
            xmlSerialize(filename);
        }

        /// <summary>
        /// Загрузка файла настроек
        /// </summary>
        /// <param name="filename">адрес файла</param>
        /// <returns></returns>
        public static Options Load(string filename)
        {
            try
            {
                return xmlDeserialize(filename);
            }
            catch (Exception) { return new Options(); }
        }

        #region сериализация

        /// <summary>
        /// сериализация в XML
        /// </summary>
        /// <param name="FilePath">путь к файлу</param>
        private void xmlSerialize(string FilePath)
        {
            File.Delete(FilePath);
            XmlSerializer se = new XmlSerializer(typeof(Options));
            FileStream fs = new FileStream(FilePath, FileMode.Create);
            se.Serialize(fs, this);
            fs.Close();
        }

        /// <summary>
        /// десериализация XML
        /// </summary>
        /// <param name="FilePath">путь к файлу</param>
        /// <returns></returns>
        private static Options xmlDeserialize(string FilePath)
        {
            if (!File.Exists(FilePath))
                return new Options();


            FileStream fs = new FileStream(FilePath, FileMode.Open);
            XmlSerializer se = new XmlSerializer(typeof(Options));
            try
            {
                Options res = (Options)se.Deserialize(fs);
                res.FilePath = FilePath;
                return res;
            }
            catch (Exception)
            {
                return new Options();
            }
            finally
            {
                fs.Close();
            }
        }


        #endregion

        /// <summary>
        /// получить параметры компьютера
        /// </summary>
        /// <returns></returns>
        public static string GetEngineParams()
        {
            return "valid";
        }

    }
}
