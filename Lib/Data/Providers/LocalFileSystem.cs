using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Operations.Limits;

namespace WindEnergy.Lib.Data.Providers
{
    /// <summary>
    /// методы работы с файловой системой и файловыми БД
    /// </summary>
    public class LocalFileSystem
    {
        private List<MeteostationInfo> _meteostationList = null;
        private Dictionary<PointLatLng, ManualLimits> _staticSpeedLimits = null;

        /// <summary>
        /// список метеостанций и координат
        /// </summary>
        public List<MeteostationInfo> MeteostationList
        {
            get
            {
                if (_meteostationList == null || _meteostationList.Count == 0)
                    _meteostationList = LoadMeteostationList(Vars.Options.StaticMeteostationCoordinatesSourceFile);
                return _meteostationList;
            }
        }

        /// <summary>
        /// список ограничений по регионам
        /// </summary>
        public Dictionary<PointLatLng, ManualLimits> StaticSpeedLimits
        {
            get
            {
                if (_staticSpeedLimits == null || _staticSpeedLimits.Count == 0)
                    _staticSpeedLimits = loadStaticSpeedLimits(Vars.Options.StaticRegionLimitsSourceFile);
                return _staticSpeedLimits;
            }
        }


        /// <summary>
        /// создание временного файла
        /// </summary>
        /// <returns></returns>
        public string GetTempFileName()
        {
            if (!Directory.Exists(Vars.Options.TempFolder))
                Directory.CreateDirectory(Vars.Options.TempFolder);
            string res = Vars.Options.TempFolder + "\\" + Guid.NewGuid().ToString() + ".tmp";
            while (File.Exists(res))
                res = Vars.Options.TempFolder + "\\" + Guid.NewGuid().ToString() + ".tmp";
            return res;
        }

        /// <summary>
        /// создание временной папки
        /// </summary>
        /// <returns></returns>
        public string GetTempFolderName()
        {
            string res = Vars.Options.TempFolder + "\\" + Guid.NewGuid().ToString();
            while (Directory.Exists(res))
                res = Vars.Options.TempFolder + "\\" + Guid.NewGuid().ToString();
            Directory.CreateDirectory(res);
            return res;
        }

        /// <summary>
        ///  распаковка файла из архива GZip
        /// </summary>
        /// <param name="inFileName">файл архива</param>
        /// <param name="outFileName">название выходного файла</param>
        public static void UnGZip(string inFileName, string outFileName)
        {
            using (FileStream fileOpen = new FileStream(inFileName, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fileCreate = new FileStream(outFileName, FileMode.Create, FileAccess.Write))
                {
                    using (GZipStream fileGZip = new GZipStream(fileOpen, CompressionMode.Decompress))
                    {
                        //     сжатие файла целиком
                        if (true)
                        {
                            fileGZip.CopyTo(fileCreate, (int)fileOpen.Length);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// сжатие файла в архив GZip
        /// </summary>
        /// <param name="inFileName">адрес входного файла</param>
        /// <param name="outFileName">адрес выходного архива</param>
        public void GZip(string inFileName, string outFileName)
        {
            using (FileStream fileOpen = new FileStream(inFileName, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fileCreate = new FileStream(outFileName, FileMode.Create, FileAccess.Write))
                {
                    using (GZipStream fileGZip = new GZipStream(fileCreate, CompressionMode.Compress))
                    {
                        //     сжатие файла целиком
                        if (true)
                        {
                            fileOpen.CopyTo(fileGZip, (int)fileOpen.Length);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// пробует загрузить файл координат метеостанций и возвращает true, если  загрузка удалась
        /// </summary>
        /// <param name="fileName">адрес файла</param>
        /// <returns></returns>
        public bool CheckMSCoordinatesFile(string fileName)
        {
            try
            {
                LoadMeteostationList(fileName);
                return true;
            }
            catch (Exception)
            { return false; }
        }

        /// <summary>
        /// пробует загрузить файл ограничений и возвращает true, если  загрузка удалась
        /// </summary>
        /// <param name="fileName">адрес файла</param>
        /// <returns></returns>
        public bool CheckRegionLimitsFile(string fileName)
        {
            try
            {
                loadStaticSpeedLimits(fileName);
                return true;
            }
            catch (Exception)
            { return false; }
        }

        /// <summary>
        /// загрузить список ограничений скоростей по точкам
        /// </summary>
        /// <param name="filename">адрес файла ограничения скоростей</param>
        /// <returns></returns>
        private Dictionary<PointLatLng, ManualLimits> loadStaticSpeedLimits(string filename)
        {
            Dictionary<PointLatLng, ManualLimits> limits = new Dictionary<PointLatLng, ManualLimits>();
            StreamReader sr = new StreamReader(filename);
            sr.ReadLine();//пропускаем первую строку-заголовок
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] arr = line.Split(';'); //название;широта;долгота;минимальная скорость;максимальная скорость
                if (arr.Length < 5)
                    continue;
                Diapason<double> d = new Diapason<double>(double.Parse(arr[3].Replace('.', Vars.DecimalSeparator)), double.Parse(arr[4].Replace('.', Vars.DecimalSeparator)));
                PointLatLng p = new PointLatLng(double.Parse(arr[1].Replace('.', Vars.DecimalSeparator)), double.Parse(arr[2].Replace('.', Vars.DecimalSeparator)));
                ManualLimits ml = new ManualLimits(new List<Diapason<double>>(), new List<Diapason<double>>() { d }) { Position = p, Name = arr[0] };
                limits.Add(p, ml);
            }
            sr.Close();
            return limits;
        }

        /// <summary>
        /// загрузка списка метеостанций и координат
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static List<MeteostationInfo> LoadMeteostationList(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            sr.ReadLine(); //пропуск заголовка

            List<MeteostationInfo> res = new List<MeteostationInfo>();
            while (!sr.EndOfStream)
            {
                string[] arr = sr.ReadLine().Split(';');
                if (arr.Length < 3)
                    continue;
                string wmo = arr[0];
                string cc_code = arr[1];
                string name = arr[2];
                string address = arr[3];
                double lat = double.Parse(arr[4].Replace('.', Vars.DecimalSeparator).Replace(',', Vars.DecimalSeparator));
                double lon = double.Parse(arr[5].Replace('.', Vars.DecimalSeparator).Replace(',', Vars.DecimalSeparator));

                double alt = double.NaN;
                DateTime mfrom = DateTime.MinValue;
                if (arr.Length > 6)
                {
                    alt = double.Parse(arr[6].Replace('.', Vars.DecimalSeparator).Replace(',', Vars.DecimalSeparator));
                    mfrom = DateTime.Parse(arr[7]);
                }
                res.Add(new MeteostationInfo() { ID = wmo, Coordinates = new PointLatLng(lat, lon), Name = name, Altitude = alt, MonitoringFrom = mfrom, CC_Code=cc_code, Address=address });
            }

            sr.Close();
            return res;
        }

    }
}

