using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindEnergy.Lib.Data.Providers
{
    public class LocalFileSystem
    {
        /// <summary>
        /// создание временного файла
        /// </summary>
        /// <returns></returns>
        public string GetTempFileName()
        {
            if (!Directory.Exists(Application.StartupPath + Vars.Options.TempFolder))
                Directory.CreateDirectory(Application.StartupPath + Vars.Options.TempFolder);
            string res = Application.StartupPath + Vars.Options.TempFolder + "\\" + Guid.NewGuid().ToString() + ".tmp";
            while (File.Exists(res))
                res = Application.StartupPath + Vars.Options.TempFolder + "\\" + Guid.NewGuid().ToString() + ".tmp";
            return res;
        }

        /// <summary>
        /// создание временной папки
        /// </summary>
        /// <returns></returns>
        public string GetTempFolderName()
        {
            string res = Application.StartupPath + Vars.Options.TempFolder + "\\" + Guid.NewGuid().ToString();
            while (Directory.Exists(res))
                res = Application.StartupPath + Vars.Options.TempFolder + "\\" + Guid.NewGuid().ToString();
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
                        //     сжатие файла по одному байту
                        if (false)
                        {
                            int buf;
                            while ((buf = fileGZip.ReadByte()) > 0)
                            {
                                fileCreate.WriteByte((byte)buf);
                            }
                        }

                        //     сжатие файла целиком
                        if (true)
                        {
                            fileGZip.CopyTo(fileCreate, (int)fileOpen.Length);
                        }

                        //     сжатие файла с буффером по умолчанию(4096 байт)
                        if (false)
                        {
                            fileGZip.CopyTo(fileCreate);
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
                        //     сжатие файла по одному байту
                        if (false)
                        {
                            int buf;
                            while ((buf = fileOpen.ReadByte()) > 0)
                            {
                                fileGZip.WriteByte((byte)buf);
                            }
                        }

                        //     сжатие файла целиком
                        if (true)
                        {
                            fileOpen.CopyTo(fileGZip, (int)fileOpen.Length);
                        }

                        //     сжатие файла с буффером по умолчанию(4096 байт)
                        if (false)
                        {
                            fileOpen.CopyTo(fileGZip);
                        }
                    }
                }
            }
        }
    }
}

