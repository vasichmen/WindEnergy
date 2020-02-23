using CommonLib;
using CommonLib.Classes;
using CommonLib.Classes.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WindEnergy.WindLib.Statistic.Structures;

namespace WindEnergy.WindLib.Classes.Structures.Options
{
    /// <summary>
    /// Настройки программы
    /// </summary>
    [Serializable]
    public class Options : OptionsBase
    {
        /// <summary>
        /// создает новый экземпляр
        /// </summary>
        public Options()
        {
            StaticRegionLimitsSourceFile = Application.StartupPath + "\\Data\\staticRegionLimits.txt";
            StaticMeteostationCoordinatesSourceFile = Application.StartupPath + "\\Data\\staticMeteostationCoordinates.txt";
            StaticAMSDatabaseSourceFile = Application.StartupPath + "\\Data\\AMS.database.txt";
            StaticFlugerDatabaseSourceFile = Application.StartupPath + "\\Data\\Fluger.database.txt";
            StaticEquipmentDatabaseSourceFile = Application.StartupPath + "\\Data\\Equipment.database.txt";
            StaticMicroclimateTableDatabaseSourceFile = Application.StartupPath + "\\Data\\Microclimate.table.txt";
            StaticMesoclimateTableDatabaseSourceFile = Application.StartupPath + "\\Data\\Mesoclimate.table.txt";
            StaticRP5DatabaseSourceDirectory = Application.StartupPath + "\\Data\\rp5.Database";
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
            RP5SourceEngine = RP5SourceType.OnlineAPI;
        }

        /// <summary>
        /// источник поиска по метеостанциям РП5
        /// </summary>
        public RP5SearchEngine RP5SearchEngine { get; set; }

        /// <summary>
        /// источник поиска по метеостанциям РП5
        /// </summary>
        public RP5SourceType RP5SourceEngine { get; set; }

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
        /// адрес файла БД мезоклиматических коэффициентов
        /// </summary>
        public string StaticMesoclimateTableDatabaseSourceFile { get; set; }

        /// <summary>
        /// адрес файла БД микроклиматических коэффициентов
        /// </summary>
        public string StaticMicroclimateTableDatabaseSourceFile { get; set; }

        /// <summary>
        /// адрес папки БД рп5
        /// </summary>
        public string StaticRP5DatabaseSourceDirectory { get; set; }

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
        public TextImporterState TextImportState { get; set; }

        /// <summary>
        /// если истина, то в расчётах плотность воздуха будет рассчитываться по параметрам ряда и высоте над у. м.
        /// </summary>
        public bool CalculateAirDensity { get; set; }

        /// <summary>
        /// папка с базой данных ETOPO2
        /// </summary>
        public string ETOPO2Folder { get; set; }


        /// <summary>
        /// минимальное количество дней, после которого считается пропуск данных как отдельный интервал
        /// </summary>
        public double QualifierDaysToBeginMissing { get; set; }


        /// <summary>
        /// Загрузка файла настроек
        /// </summary>
        /// <param name="filename">адрес файла</param>
        /// <returns></returns>
        public static Options Load(string filename)
        {
            try
            {
                Options res = xmlDeserialize<Options>(filename);
                return res == null ? new Options() : res;
            }
            catch (Exception) { return new Options(); }
        }


    }
}
