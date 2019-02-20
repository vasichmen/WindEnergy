using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

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
            TempFolder = Application.StartupPath+"\\tmp";
            LastDirectory = Application.StartupPath;
            CacheFolder = Application.StartupPath+"\\cache";
            MapProvider = MapProviders.YandexMap;
            StaticRegionLimitsSourceFile = Application.StartupPath + "\\staticRegionLimits.txt";
        }

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
        public string StaticRegionLimitsSourceFile { get;}

        /// <summary>
        /// сохранение настроек в файл
        /// </summary>
        /// <param name="Directory">адрес папки, куда сохранить файл</param>
        public void Save(string filename)
        {
            if (File.Exists(filename))
                File.Delete(filename);
            XMLSerialize(filename);
        }

        /// <summary>
        /// Загрузка файла настроек
        /// </summary>
        /// <param name="Directory">адрес файла</param>
        /// <returns></returns>
        public static Options Load(string filename)
        {
            try
            {
                return XMLDeserialize(filename);
            }
            catch (Exception) { return new Options(); }
        }

        #region сериализация

        /// <summary>
        /// сериализация в XML
        /// </summary>
        /// <param name="FilePath">путь к файлу</param>
        private void XMLSerialize(string FilePath)
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
        private static Options XMLDeserialize(string FilePath)
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

    }
}
