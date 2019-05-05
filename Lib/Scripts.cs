using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.Lib.Classes;
using WindEnergy.Lib.Classes.Structures;
using WindEnergy.Lib.Data;
using WindEnergy.Lib.Data.Interfaces;
using WindEnergy.Lib.Data.Providers;
using WindEnergy.Lib.Data.Providers.ETOPO;

namespace WindEnergy.Lib
{
    /// <summary>
    /// вспомогательные скрипты (обработка данных, загрузка информации с сайтов)
    /// </summary>
    public static class Scripts
    {
        /// <summary>
        /// записывает в список метеостанций mts высоты и даты начала наблюдений и сохраняет в файл toFile
        /// </summary>
        /// <param name="mts">список метеостанций</param>
        /// <param name="toFile">файл, в который сохранится список метеостанций после обработки</param>
        /// <param name="alts">источник данных о высотах точек</param>
        /// <param name="action">действие при изменении процента выполнения</param>
        public static void DownloadMeteostationExtInfo(List<MeteostationInfo> mts, string toFile, IGeoInfoProvider alts, Action<int> action = null)
        {
            RP5ru provider = new RP5ru(Vars.Options.CacheFolder + "\\rp5.ru");
            double i = 0;
            foreach (var mt in mts)
            {
                if (Math.IEEERemainder(i++, 50) == 0 && action != null)
                    action.Invoke((int)((i / mts.Count) * 100));
                MeteostationInfo tmp = new MeteostationInfo();
                tmp.Link = mt.Link;
                try
                {
                    provider.GetMeteostationExtInfo(ref tmp);
                    mt.MonitoringFrom = tmp.MonitoringFrom;
                }
                catch (WindEnergyException wex)
                { }
                mt.Altitude = alts.GetElevation(mt.Coordinates);
            }
            FileConverter.ExportMeteostationList(mts, toFile);
        }
    }
}
