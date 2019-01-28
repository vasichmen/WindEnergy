using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy
{
    /// <summary>
    /// направления ветра по 16 румбам
    /// </summary>
    public enum WindDirections
    {
        /// <summary>
        /// ветер с севера
        /// </summary>
        N,

        /// <summary>
        /// ветер с северо северо востока
        /// </summary>
        NNE,

        /// <summary>
        /// ветер с северо  востока
        /// </summary>
        NE,

        /// <summary>
        /// ветер с северо востока востока
        /// </summary>
        NEE,

        /// <summary>
        /// ветер с востока
        /// </summary>
        E,

        /// <summary>
        /// ветер с юго востока востока
        /// </summary>
        SEE,

        /// <summary>
        /// ветер с юго востока 
        /// </summary>
        SE,

        /// <summary>
        /// ветер с  юго юго востока
        /// </summary>
        SSE,

        /// <summary>
        /// ветер с юга
        /// </summary>
        S,

        /// <summary>
        /// ветер с юго юго запада
        /// </summary>
        SSW,

        /// <summary>
        /// ветер с юго  запада
        /// </summary>
        SW,

        /// <summary>
        /// ветер с  юго запада запада
        /// </summary>
        SWW,

        /// <summary>
        /// ветер с запада
        /// </summary>
        W,

        /// <summary>
        /// ветер с северо запада запада
        /// </summary>
        NWW,

        /// <summary>
        /// ветер с северо запада 
        /// </summary>
        NW,

        /// <summary>
        /// ветер с северо северо запада
        /// </summary>
        NNW,

        /// <summary>
        /// Переменное направление
        /// </summary>
        Variable
    }

    /// <summary>
    /// тип источника информации о погоде
    /// </summary>
    public enum MeteoSourceType
    {
        /// <summary>
        /// метеостанция
        /// </summary>
        Meteostation,

        /// <summary>
        /// аэропорт
        /// </summary>
        Airport,

        /// <summary>
        /// неофициальная метеостанция
        /// </summary>
        UnofficialMeteostation
    }

    /// <summary>
    /// все поддерживаемые форматы файлов
    /// </summary>
    public enum FileFormats
    {
        /// <summary>
        /// формат файлов, полученных из архива rp5 для аэропорта
        /// </summary>
        RP5MetarCSV,

        /// <summary>
        /// формат файлов, полученных из архива rp5 для метеостанции
        /// </summary>
        RP5WmoCSV,

        /// <summary>
        /// другой формат
        /// </summary>
        None
    }
}
