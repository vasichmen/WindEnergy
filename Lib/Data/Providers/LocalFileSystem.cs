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
                if (_meteostationList == null || _meteostationList.Count==0)
                    _meteostationList = loadMeteostationList(Vars.Options.StaticMeteostationCoordinatesSourceFile);
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
        private List<MeteostationInfo> loadMeteostationList(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            sr.ReadLine(); //пропуск заголовка

            List<MeteostationInfo> res = new List<MeteostationInfo>();
            while (!sr.EndOfStream)
            {
                string[] arr = sr.ReadLine().Split(';');
                string wmo = arr[0];
                string name = arr[1];
                double lat = double.Parse(arr[2].Replace('.', Vars.DecimalSeparator));
                double lon = double.Parse(arr[3].Replace('.', Vars.DecimalSeparator));
                res.Add(new MeteostationInfo() { ID = wmo, Coordinates = new PointLatLng(lat, lon), Name = name });
            }

            sr.Close();
            return res;
        }
    }
}

