using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Operations.Limits;

namespace WindEnergy.WindLib.Data.Providers.FileSystem
{
    /// <summary>
    /// методы работы с файловой системой и файловыми БД
    /// </summary>
    public class LocalFileSystem
    {

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
    }
}

