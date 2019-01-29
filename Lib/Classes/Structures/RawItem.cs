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

        /// <summary>
        /// направление ветра по румбам
        /// </summary>
        public WindDirections DirectionRhumb
        {
            get
            {
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
                    if (direction >= l[ii] && direction < r[ii])
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
                    default: throw new Exception("Что-то не так");

                }

            }
            set
            {
                switch (value)
                {
                    case WindDirections.N:
                        direction = 0;
                        break;
                    case WindDirections.NNE:
                        direction = 22.5;
                        break;
                    case WindDirections.NE:
                        direction = 45;
                        break;
                    case WindDirections.NEE:
                        direction = 67.5;
                        break;
                    case WindDirections.E:
                        direction = 90;
                        break;
                    case WindDirections.SEE:
                        direction = 112.5;
                        break;
                    case WindDirections.SE:
                        direction = 135;
                        break;
                    case WindDirections.SSE:
                        direction = 157.5;
                        break;
                    case WindDirections.S:
                        direction = 180;
                        break;
                    case WindDirections.SSW:
                        direction = 202.5;
                        break;
                    case WindDirections.SW:
                        direction = 225;
                        break;
                    case WindDirections.SWW:
                        direction = 247.5;
                        break;
                    case WindDirections.W:
                        direction = 270;
                        break;
                    case WindDirections.NWW:
                        direction = 292.5;
                        break;
                    case WindDirections.NW:
                        direction = 315;
                        break;
                    case WindDirections.NNW:
                        direction = 337.5;
                        break;
                    case WindDirections.Variable:
                        direction = 0;
                        break;
                    default: throw new Exception("Такого румба нет");
                }
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
        /// влажность воздуха в процентах
        /// </summary>
        public double Wetness { get => wetness; set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("влажность воздуха должна быть от 0 до 100 %");
                else
                    wetness = value;
            }
        }

        public RawItem()
        {
            direction = 0;
            Wetness = double.NaN;
            Date = DateTime.MinValue;
            Temperature = double.NaN;
            Speed = double.NaN;
        }
    }
}
