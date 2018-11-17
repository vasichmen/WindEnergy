using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindEnergy.Lib
{
    public class RangeEqualizer
    {


        public static void ProcessRange(string fileNameMaxInterval, string fileNameMinInterval)
        {
            StreamReader sr1 = new StreamReader(fileNameMaxInterval, Encoding.Default);
            StreamReader sr2 = new StreamReader(fileNameMinInterval,Encoding.Default) ;

            //данные начинаются с 2 строчки
            sr1.ReadLine();
            sr1.ReadLine();
            sr2.ReadLine();
            sr2.ReadLine();


            List<string[]> res = new List<string[]>();

            string r1 = sr1.ReadToEnd();
            string r2 = sr2.ReadToEnd();

            string[] lines1 = r1.Split('\n');
            string[] lines2 = r2.Split('\n');
            List<string> lns2 = new List<string>();

            sr1.Close();
            sr2.Close();

            for (int i = 0; i < lines2.Length; i++)
            {
                string[] arr = lines2[i].Split(';');
                DateTime dtMax = DateTime.Parse(arr[0]);
                if (Math.IEEERemainder(dtMax.Hour, 3) == 0 && dtMax.Minute==0)
                    lns2.Add(lines2[i]);
            }


            //для каждого элемента из ряда с большим интервалом подбираем пару и другого ряда
            for (int i = 0; i < lines1.Length; i++)
            {
                string[] arrMax = lines1[i].Split(';');
                try
                {
                    DateTime dtMax = DateTime.Parse(arrMax[0]);

                    string[] arrMin = TryGetPair(dtMax, lns2, 0);
                    if (arrMin != null)
                    {
                        res.Add(arrMax);
                        res.Add(arrMin);
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            string resFile = Application.StartupPath + "\\result.csv";
            StreamWriter sw = new StreamWriter(resFile, false, Encoding.UTF8);
            sw.WriteLine("Дата, время; Скорость МС; Направление МС; Скорость из ВИК; Направление из ВИК");
            for (int i = 0; i < res.Count; i += 2)
            {
                string ln = string.Format("{0};{1};{2};{3};{4}", res[i][0], res[i][7], res[i][6], res[i + 1][5], res[i + 1][1]);
                sw.WriteLine(ln);
            }
            sw.Close();
            Process.Start(resFile);
        }

        private static string[] TryGetPair(DateTime dtMax, List<string> lines, int start)
        {
            for (int i = lines.Count - 1; i >= 0; i--)
            {
                string[] arr = lines[i].Split(';');
                try
                {
                    DateTime dt = DateTime.Parse(arr[0]);
                    if (dt == dtMax)
                        return arr;
                    DateTime ndt = dt + TimeSpan.FromDays(1);
                    if (ndt < dtMax)
                        return null;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            return null;
        }
    }
}
