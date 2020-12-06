using CommonLib.Classes;
using CommonLib.Classes.Base;
using System;
using System.Collections.Generic;

namespace WindEnergy.WindLib.Classes.Structures
{
    /// <summary>
    /// представление данных одного элемента ряда наблюдений
    /// </summary>
    public class RawItem : RawItemBase
    {
        private double direction = double.NaN;
        private double wetness;
        private double speed;


        /// <summary>
        /// направление ветра в градусах
        /// </summary>
        public double Direction
        {
            get => direction;
            set
            {
                if (double.IsNaN(value))
                { direction = value; return; }
                if (value == (double)WindDirections16.Calm)
                { direction = (double)WindDirections16.Calm; return; }

                if (value < 0 || value >= 360)
                    throw new ArgumentOutOfRangeException("Направление ветра должно быть от 0 до 360 градусов");
                else
                    direction = value;
            }
        }

        public WindDirections16 directionRhumb = WindDirections16.Undefined;

        /// <summary>
        /// направление ветра по румбам
        /// </summary>
        public WindDirections16 DirectionRhumb
        {
            get
            {
                if (directionRhumb == WindDirections16.Undefined)
                    directionRhumb = GetRhumb(direction);
                return directionRhumb;
            }
            set
            {
                direction = GetDirection(value);

            }
        }

        /// <summary>
        /// клонирование объекта RawItem
        /// </summary>
        /// <returns></returns>
        internal RawItem Clone()
        {
            return new RawItem(this.DateArgument, this.Speed, this.Direction, this.Temperature, this.Wetness, this.Pressure);
        }

        /// <summary>
        /// скорость ветра в м/с
        /// </summary>
        public double Speed
        {
            get => speed;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Скорость не может быть отрицательной");
                else
                    speed = value;
            }
        }

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
            get => wetness;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("Влажность воздуха должна быть от 0 до 100 %");
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
        /// создает новый обект на основе метеоданных и даты наблюдения
        /// </summary>
        /// <param name="date"></param>
        /// <param name="parameters"></param>
        public RawItem(DateTime date, Dictionary<MeteorologyParameters, double> parameters)
        {
            if (!parameters.ContainsKey(MeteorologyParameters.Speed))
                throw new ArgumentException(nameof(parameters));
            Date = date;
            Speed = parameters[MeteorologyParameters.Speed] == -999 ? double.NaN : parameters[MeteorologyParameters.Speed];

            if (parameters.ContainsKey(MeteorologyParameters.Direction))
                Direction = parameters[MeteorologyParameters.Direction] == -999 ? double.NaN : parameters[MeteorologyParameters.Direction];
            else Direction = double.NaN;

            if (parameters.ContainsKey(MeteorologyParameters.Temperature))
                Temperature = parameters[MeteorologyParameters.Temperature] == -999 ? double.NaN : parameters[MeteorologyParameters.Temperature];
            else Temperature = double.NaN;

            if (parameters.ContainsKey(MeteorologyParameters.Pressure))
                Pressure = parameters[MeteorologyParameters.Pressure] == -999 ? double.NaN : parameters[MeteorologyParameters.Pressure];
            else Pressure = double.NaN;

            if (parameters.ContainsKey(MeteorologyParameters.Wetness))
                Wetness = parameters[MeteorologyParameters.Wetness] == -999 ? double.NaN : parameters[MeteorologyParameters.Wetness];
            else Wetness = double.NaN;
        }

        /// <summary>
        /// получить направление в градусах по заданному румбу
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double GetDirection(WindDirections16 value)
        {
            switch (value)
            {
                case WindDirections16.N:
                    return 0;
                case WindDirections16.NNE:
                    return 22.5;
                case WindDirections16.NE:
                    return 45;
                case WindDirections16.NEE:
                    return 67.5;
                case WindDirections16.E:
                    return 90;
                case WindDirections16.SEE:
                    return 112.5;
                case WindDirections16.SE:
                    return 135;
                case WindDirections16.SSE:
                    return 157.5;
                case WindDirections16.S:
                    return 180;
                case WindDirections16.SSW:
                    return 202.5;
                case WindDirections16.SW:
                    return 225;
                case WindDirections16.SWW:
                    return 247.5;
                case WindDirections16.W:
                    return 270;
                case WindDirections16.NWW:
                    return 292.5;
                case WindDirections16.NW:
                    return 315;
                case WindDirections16.NNW:
                    return 337.5;
                case WindDirections16.Calm:
                    return (double)WindDirections16.Calm;
                case WindDirections16.Variable:
                case WindDirections16.Undefined:
                    return double.NaN;
                default: throw new Exception("Такого румба нет");
            }
        }


        /// <summary>
        /// направление ветра по 8 румбам
        /// </summary>
        public WindDirections8 DirectionRhumb8
        {
            get
            {
                switch (DirectionRhumb)
                {
                    case WindDirections16.N:
                    case WindDirections16.NNE:
                        return WindDirections8.N;
                    case WindDirections16.NE:
                    case WindDirections16.NEE:
                        return WindDirections8.NE;
                    case WindDirections16.E:
                    case WindDirections16.SEE:
                        return WindDirections8.E;
                    case WindDirections16.SE:
                    case WindDirections16.SSE:
                        return WindDirections8.SE;
                    case WindDirections16.S:
                    case WindDirections16.SSW:
                        return WindDirections8.S;
                    case WindDirections16.SW:
                    case WindDirections16.SWW:
                        return WindDirections8.SW;
                    case WindDirections16.W:
                    case WindDirections16.NWW:
                        return WindDirections8.W;
                    case WindDirections16.NW:
                    case WindDirections16.NNW:
                        return WindDirections8.NW;
                    case WindDirections16.Undefined:
                        return WindDirections8.Undefined;
                    case WindDirections16.Variable:
                        return WindDirections8.Variable;
                    case WindDirections16.Calm:
                        return WindDirections8.Calm;
                    default: throw new Exception("Этот румб не существует");
                }
            }
            set
            {
                switch (value)
                {
                    case WindDirections8.Calm:
                        DirectionRhumb = WindDirections16.Calm;
                        break;
                    case WindDirections8.E:
                        DirectionRhumb = WindDirections16.E;
                        break;
                    case WindDirections8.N:
                        DirectionRhumb = WindDirections16.N;
                        break;

                    case WindDirections8.NE:
                        DirectionRhumb = WindDirections16.NE;
                        break;
                    case WindDirections8.NW:
                        DirectionRhumb = WindDirections16.NW;
                        break;
                    case WindDirections8.S:
                        DirectionRhumb = WindDirections16.S;
                        break;
                    case WindDirections8.SE:
                        DirectionRhumb = WindDirections16.SE;
                        break;
                    case WindDirections8.SW:
                        DirectionRhumb = WindDirections16.SW;
                        break;
                    case WindDirections8.Undefined:
                        DirectionRhumb = WindDirections16.Undefined;
                        break;
                    case WindDirections8.Variable:
                        DirectionRhumb = WindDirections16.Variable;
                        break;
                    case WindDirections8.W:
                        DirectionRhumb = WindDirections16.W;
                        break;
                    default: throw new Exception("Этот румб не существует");
                }
            }
        }

        /// <summary>
        /// получить румб по направлению ветра
        /// </summary>
        /// <param name="directionValue">направление вестра в градусах</param>
        /// <returns></returns>
        public static WindDirections16 GetRhumb(double directionValue)
        {
            if (double.IsNaN(directionValue))
                return WindDirections16.Undefined;

            if (directionValue == (double)WindDirections16.Calm)
                return WindDirections16.Calm;

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
                    return WindDirections16.N;
                case 1:
                    return WindDirections16.NNE;
                case 2:
                    return WindDirections16.NE;
                case 3:
                    return WindDirections16.NEE;
                case 4:
                    return WindDirections16.E;
                case 5:
                    return WindDirections16.SEE;
                case 6:
                    return WindDirections16.SE;
                case 7:
                    return WindDirections16.SSE;
                case 8:
                    return WindDirections16.S;
                case 9:
                    return WindDirections16.SSW;
                case 10:
                    return WindDirections16.SW;
                case 11:
                    return WindDirections16.SWW;
                case 12:
                    return WindDirections16.W;
                case 13:
                    return WindDirections16.NWW;
                case 14:
                    return WindDirections16.NW;
                case 15:
                    return WindDirections16.NNW;
                default: throw new WindEnergyException("Что-то не так");

            }
        }
    }
}
