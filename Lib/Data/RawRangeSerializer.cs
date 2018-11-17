using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Data.Formats;

namespace WindEnergy.Lib.Data
{
    public static class RawRangeSerializer
    {
        public static RawRange OpenFile(string FileName, Action<double> onProgressChanged = null)
        {
            string ext = Path.GetExtension(FileName).ToLower();
            switch (ext)
            {
                case ".csv":
                    RawRange rang = LoadCSV(FileName);
                    return rang;
                default: throw new Exception("Открытие этого типа файлов не реализовано");
            }
        }


        private static RawRange LoadCSV(string file)
        {
            StreamReader sr = new StreamReader(file, Encoding.Default, true);

            //пропуск пустых строк
            for (int i = 0; i < 7; i++)
            {
                string l = sr.ReadLine();
            }

            string data = sr.ReadToEnd();
            sr.Close();
            string[] lines = data.Split('\n');
            RawRange res = new RawRange();
            foreach (string line in lines)
            {
                string[] elems = line.Split(';');
                if (elems.Length < 8)
                    continue;
                if (elems[6] == "")
                    continue;
                if (elems[7] == "")
                    continue;

                double temp = elems[1] == ""?double.NaN: double.Parse(elems[1].Replace(',', Vars.DecimalSeparator));
                DateTime dt = DateTime.Parse(elems[0]);
                double spd =  double.Parse(elems[7].Replace(',', Vars.DecimalSeparator));
                double wet = elems[5] == ""?double.NaN: double.Parse(elems[5].Replace(',', Vars.DecimalSeparator));
                string dirs = elems[6];
                WindDirections direct = RP5ru.GetWindDirectionFromString(dirs);
                res.Add(new RawItem() { Date = dt, DirectionRhumb = direct, Speed = spd, Temperature = temp, Wetness = wet });
            }
            res.FileName = Path.GetFileName(file);
            return res;
        }
    }
}
