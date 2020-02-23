using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.WindLib.Classes.Structures
{
    /// <summary>
    /// структура таблицы мезоклиматических коэффициентов
    /// </summary>
   public class MesoclimateItemInfo
    {
        /// <summary>
        /// название, описание рельефа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// диапазон коэффициентов
        /// </summary>
        public Diapason<double> Value { get; set; }
    }
}
