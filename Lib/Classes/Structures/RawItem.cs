using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Classes.Structures
{
    /// <summary>
    /// представление данных одного элемента ряда наблюдений
    /// </summary>
    public class RawItem
    {
        private double direction = double.NaN;
        private double wetness;

        /// <summary>
        /// дата и время наблюдения
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// направление ветра в градусах
        /// </summary>
        public double Direction
        {
            get => direction;
            set
            {
                if (value < 0 || value >= 360)
                    throw new ArgumentOutOfRangeException("Направление ветра должно быть от 0 до 360 градусов");
                else
                    direction = value;
            }
        }

        public WindDirections directionRhumb = WindDirections.Undefined;
        /// <summary>
        /// направление ветра по румбам
        /// </summary>
        public WindDirections DirectionRhumb
        {
            get
            {
                if (directionRhumb == WindDirections.Undefined)
                    directionRhumb = GetRhumb(direction);
                return directionRhumb;

            }
            set
            {
                direction = GetDirection(value);

            }
        }

        /// <summary>
        /// скорость ветра в м/с
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// температура воздуха в градусах цельсия
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Давление на уровне МС, мм рт. ст. Для данных NASA загружается параметр PS (Surface Pressure)
        /// </summary>
        public double Pressure { get; set; }

        /// <summary>
        /// влажность воздуха в процентах
        /// </summary>
        public double Wetness
        {
            get => wetness; set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("влажность воздуха должна быть от 0 до 100 %");
                else
                    wetness = value;
            }
        }

        /// <summary>
        /// дата измерения, представленная в минутах от минимальной даты DateTime.MinValue
        /// </summary>
        public double DateArgument
        {
            get
            {
                //               9223372036854775807 maxLong
                //               2193385800000000 ticks
                //               219338580 seconds
                return (Date.Ticks / 1e7) / 60d;
            }
        }

        /// <summary>
        /// создаёт новый объект с значениями по умолчанию
        /// </summary>
        public RawItem()
        {
            direction = double.NaN;
            Wetness = double.NaN;
            Date = DateTime.MinValue;
            Temperature = double.NaN;
            Speed = double.NaN;
            Pressure = double.NaN;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateArgumentp">время измрения в минутах от DateTime.MinValue</param>
        /// <param name="speed">скорость м/с</param>
        /// <param name="direct">направление, град</param>
        /// <param name="temp">температура град,С</param>
        /// <param name="wet">влажность, %</param>
        /// <param name="press">давление, мм рт. ст.</param>
        public RawItem(double dateArgumentp, double speed, double direct, double temp, double wet, double press)
        {
            Speed = speed;
            Direction = direct;
            Temperature = temp;
            Wetness = wet;
            Pressure = press;
            Date = DateTime.MinValue + TimeSpan.FromMinutes(dateArgumentp);
        }

        /// <summary>
        /// получить направление в градусах по заданному румбу
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double GetDirection(WindDirections value)
        {
            switch (value)
            {
                case WindDirections.N:
                    return 0;
                case WindDirections.NNE:
                    return 22.5;
                case WindDirections.NE:
                    return 45;
                case WindDirections.NEE:
                    return 67.5;
                case WindDirections.E:
                    return 90;
                case WindDirections.SEE:
                    return 112.5;
                case WindDirections.SE:
                    return 135;
                case WindDirections.SSE:
                    return 157.5;
                case WindDirections.S:
                    return 180;
                case WindDirections.SSW:
                    return 202.5;
                case WindDirections.SW:
                    return 225;
                case WindDirections.SWW:
                    return 247.5;
                case WindDirections.W:
                    return 270;
                case WindDirections.NWW:
                    return 292.5;
                case WindDirections.NW:
                    return 315;
                case WindDirections.NNW:
                    return 337.5;
                case WindDirections.Calm:
                case WindDirections.Variable:
                    return 0;
                case WindDirections.Undefined:
                    return double.NaN;
                default: throw new Exception("Такого румба нет");
            }
        }

        /// <summary>
        /// получить румб по направлению ветра
        /// </summary>
        /// <param name="directionValue">направление вестра в градусах</param>
        /// <returns></returns>
        public static WindDirections GetRhumb(double directionValue)
        {
            if (double.IsNaN(directionValue))
                return WindDirections.Undefined;

            //получаем градации по румбам, начиная с севера
            double[] l = new double[17];
            double[] r = new double[17];
            double rumb = 360d / 16;

            l[0] = 0;
            r[0] = rumb / 2;
            int i = 1;
            for (double n = rumb / 2d; n < 360d - rumb / 2d; n += rumb)
            {
                l[i] = n;
                r[i] = n + rumb;
                i++;
            }
            l[i] = 360d - rumb / 2d;
            r[i] = 360;

            int ii;
            for (ii = 0; ii < 17; ii++)
                if (directionValue >= l[ii] && directionValue < r[ii])
                    break;
            switch (ii)
            {
                case 0:
                case 16:
                    return WindDirections.N;
                case 1:
                    return WindDirections.NNE;
                case 2:
                    return WindDirections.NE;
                case 3:
                    return WindDirections.NEE;
                case 4:
                    return WindDirections.E;
                case 5:
                    return WindDirections.SEE;
                case 6:
                    return WindDirections.SE;
                case 7:
                    return WindDirections.SSE;
                case 8:
                    return WindDirections.S;
                case 9:
                    return WindDirections.SSW;
                case 10:
                    return WindDirections.SW;
                case 11:
                    return WindDirections.SWW;
                case 12:
                    return WindDirections.W;
                case 13:
                    return WindDirections.NWW;
                case 14:
                    return WindDirections.NW;
                case 15:
                    return WindDirections.NNW;
                default: throw new WindEnergyException("Что-то не так");

            }
        }
    }
}
