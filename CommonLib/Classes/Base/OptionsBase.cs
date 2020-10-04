using CommonLib.Data.Providers.InternetServices;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CommonLib.Classes.Base
{
    /// <summary>
    /// Настройки программы
    /// </summary>
    [Serializable]
    public abstract class OptionsBase
    {

        protected OptionsBase()
        {
            TempFolder = Application.StartupPath + "\\tmp";
            LastDirectory = Application.StartupPath;
            CacheFolder = Application.StartupPath + "\\cache";
            MapProvider = MapProviders.YandexMap;
        }


        /// <summary>
        /// адрес сайта
        /// </summary>
        public string SiteAddress { get { return Velomapa.SITE_ADDRESS; } }

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
        /// поведение диалога обновления программы
        /// </summary>
        public UpdateDialogAnswer UpdateMode { get; set; }

        /// <summary>
        /// адрес для связи в Telegram
        /// </summary>
        public string TelegramAddress { get { return "https://t.me/langstor"; } }

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



        #region сериализация

        /// <summary>
        /// сериализация в XML
        /// </summary>
        /// <param name="FilePath">путь к файлу</param>
        private void xmlSerialize(string FilePath)
        {
            File.Delete(FilePath);
            XmlSerializer se = new XmlSerializer(this.GetType());
            FileStream fs = new FileStream(FilePath, FileMode.Create);
            se.Serialize(fs, this);
            fs.Close();
        }

        /// <summary>
        /// десериализация XML
        /// </summary>
        /// <param name="FilePath">путь к файлу</param>
        /// <returns></returns>
        protected static T xmlDeserialize<T>(string FilePath)
        {
            if (!File.Exists(FilePath))
                return default;


            FileStream fs = new FileStream(FilePath, FileMode.Open);
            XmlSerializer se = new XmlSerializer(typeof(T));
            try
            {
                T res = (T)se.Deserialize(fs);
                return res;
            }
            catch (Exception)
            {
                return default;
            }
            finally
            {
                fs.Close();
            }
        }


        #endregion

    }
}
