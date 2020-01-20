using CommonLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolarEnergy
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool is_accept = File.Exists(Application.StartupPath + "\\id.key");
            if (is_accept)
            {
                byte[] cur_key = Driver.GetID();
                byte[] file_key = Driver.LoadID(Application.StartupPath + "\\id.key");
                if (cur_key.Length == file_key.Length)
                    for (int i = cur_key.Length - 1; i >= 0; i--)
                        is_accept &= cur_key[i] == file_key[i];
                else
                    is_accept = false;
            }
            if (!is_accept)
            {
                MessageBox.Show("Ошибка при проверке файла ключа, программа будет закрыта\r\n");
                return;
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
