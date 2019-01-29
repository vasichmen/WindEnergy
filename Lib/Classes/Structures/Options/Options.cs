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
            TempFolder = "\\tmp";
            LastDirectory = Application.StartupPath;
        }

        /// <summary>
        /// адрес файла настроек
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// временния папка 
        /// </summary>
        public string TempFolder { get;}
        public string LastDirectory { get; set; }

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
        public static Options Load(string Directory)
        {
            string[] arr = System.IO.Directory.GetFiles(Directory);
            List<string> files = new List<string>(arr);

            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                return XMLDeserialize(file);
            }

            return new Options();

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
