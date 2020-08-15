using CommonLib.Classes;
using CommonLib.Geomodel;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEnergy.WindLib.Classes;
using WindEnergy.WindLib.Classes.Collections;
using WindEnergy.WindLib.Classes.Structures;
using WindEnergy.WindLib.Data.Interfaces;
using WindEnergy.WindLib.Data.Providers;
using WindEnergy.WindLib.Data.Providers.InternetServices;
using WindEnergy.WindLib.Operations.Structures;
using WindEnergy.WindLib.Statistic.Calculations;
using WindEnergy.WindLib.Statistic.Collections;
using WindEnergy.WindLib.Statistic.Structures;
using WindLib;

namespace WindEnergy.WindLib.Operations.Interpolation
{
    /// <summary>
    /// интерполирование функции на основе данных ближайшей метеостанции
    /// </summary>
    public class NearestMSInterpolateMethod : IInterpolateMethod
    {
        private readonly Dictionary<double, double> func;
        private readonly MeteorologyParameters parameterType;
        private readonly RawRange nearestRange;
        private readonly Func<double, double> getRes = null;
        private readonly Diapason<double> interpolationDiapason;
        public readonly bool Empty;


        /// <summary>
        /// шаг градаций скорости в м/с
        /// </summary>
        private const double SPEED_GRADATION_STEP = 5;

        /// <summary>
        /// шаг градаций температуры в градусах Цельсия
        /// </summary>
        private const double TEMPERATURE_GRADATION_STEP = 8;

        /// <summary>
        /// шаг градаций владности в %
        /// </summary>
        private const double WETNESS_GRADATION_STEP = 10;

        /// <summary>
        /// создаёт новый интерполятор для заданной точки с заданной функций и типом расчетного параметра
        /// </summary>
        /// <param name="func">известные значения функции в заданной точке</param>
        /// <param name="coordinates">координаты точки, для которой известны значения func</param>
        /// <param name="parameterType">тип мтеорологического параметра</param>
        public NearestMSInterpolateMethod(Dictionary<double, double> func, PointLatLng coordinates, MeteorologyParameters parameterType)
            : this(func, getNearestRange(func, coordinates), parameterType) { }


        /// <summary>
        /// создание нового интерполятора с заданными значениями на основе существующего интерполятора (в том случае, если для этой точки уже был создан интерполятор)
        /// </summary>
        /// <param name="func">известные значения функции</param>
        /// <param name="baseInterpolator">интерполятор, откуда взять ряд наблюдения для этого экземпляра</param>
        /// <param name="parameterType">тип параметра для восстановления</param>
        public NearestMSInterpolateMethod(Dictionary<double, double> func, NearestMSInterpolateMethod baseInterpolator, MeteorologyParameters parameterType)
            : this(func, baseInterpolator.nearestRange, parameterType) { }

        /// <summary>
        /// создание нового интерполятора с заданными значениями на основе существующего базового ряда наблюдения
        /// </summary>
        /// <param name="func">известные значения функции в заданной точке</param>
        /// <param name="baseRange">базовый ряд (с ближайшей МС), на основе которого будет происходить восстановление</param>
        /// <param name="parameterType">тип мтеорологического параметра</param>
        public NearestMSInterpolateMethod(Dictionary<double, double> func, RawRange baseRange, MeteorologyParameters parameterType)
        {
            if (func.Keys.Count == 0)
            { Empty = true; return; }
            Empty = false;

            this.parameterType = parameterType;
            this.func = func;
            this.nearestRange = baseRange;

            //расчет диапазона сделанных измерений
            baseRange = new RawRange(baseRange.OrderBy(x => x.Date).ToList());
            interpolationDiapason.From = Math.Max(baseRange[0].DateArgument, func.Keys.Min()); //максимальную дату из начал каждой функции
            interpolationDiapason.To = Math.Min(baseRange[baseRange.Count - 1].DateArgument, func.Keys.Max()); //минимальную дату из концов каждой функции

            double a = 0, b = 0, r = 0; //коэффициенты прямой  и коэффициент корреляции

            Dictionary<double, double> baseFunc = baseRange.GetFunction(parameterType); //функция базового ряда
            List<double>[] tableCoeff = calcTableCoeff(func, baseFunc); //таблица для расчёта коэффициентов a, b, r

            a = getParameterA(tableCoeff);//коэффициенты прямой
            b = getParameterB(tableCoeff, a);
            r = getParameterR(tableCoeff);  //коэффициент корреляции

            //проверка попадания коэфф корреляции в допустимый диапазон (если для этого параметра надо проверять диапазон)
            if (r < Vars.Options.MinimalCorrelationCoeff && Vars.Options.MinimalCorrelationControlParametres.Contains(parameterType))
                throw new Exception("Недостаточное коррелирование функций");

            //ФУНКЦИЯ ПОЛУЧЕНИЯ ЗНАЧЕНИЯ
            getRes = new Func<double, double>((x) =>
            {
                //если в исходном ряде есть это значение, то его и возвращаем
                if (func.ContainsKey(x))
                    return func[x];

                //Если в базовой функции нет измерения за этой время, то возвращаем NaN
                //иначе расчитываем скорость по полученной зависимости. 
                if (!baseFunc.ContainsKey(x))
                    return double.NaN;
                else
                    return a * baseFunc[x] + b;
            });
        }

        /// <summary>
        /// ищет наиболее подходящую к заданной точке МС и получает её ряд. Если ряд не найден, то возвращает null
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="r"></param>
        /// <param name="actionPercent"></param>
        /// <param name="Range">ряд, для которого подбирается функция</param>
        /// <exception cref="GetBaseRangeException">Возвращает иснформацию о параметрах, мешающих получить ближайшую МС</exception>
        /// <returns></returns>
        internal static RawRange TryGetBaseRange(RawRange Range, PointLatLng coordinates, out double r, Action<int, string> actionPercent)
        {
            bool nlaw = CheckNormalLaw(Range, Vars.Options.NormalLawPirsonCoefficientDiapason);
            if (!nlaw)
                throw new WindEnergyException("Исходный ряд не подчиняется нормальному закону распределения");

            DateTime from = Range.Min((ri) => ri.Date).Date, to = Range.Max((ri) => ri.Date).Date;

            List<RP5MeteostationInfo> mts = Vars.RP5Meteostations.GetNearestMS(coordinates, Vars.Options.NearestMSRadius, false);
            Dictionary<double, double> funcSpeed = Range.GetFunction(MeteorologyParameters.Speed); //функция скорости на заданном ряде
            RawRange res = null;
            double rmax = double.MinValue, total_rmax = double.MinValue;
            RP5ru provider = new RP5ru(Vars.Options.CacheFolder + "\\rp5.ru");

            for (int i = 0; i < mts.Count; i++)
            {
                if (actionPercent != null)
                    actionPercent.Invoke((int)((i * 1d / mts.Count) * 100d), "Поиск подходящей МС...");

                RP5MeteostationInfo m = mts[i];

                //если нет диапазона измерений в БД, то загружаем с сайта
                if (m.MonitoringFrom == DateTime.MinValue)
                    provider.GetMeteostationExtInfo(ref m);

                //если для этой МС нет наблюдений в этом периоде, то переходим на другую
                if (m.MonitoringFrom > from)
                    continue;

                //загрузка ряда с очередной МС
                RawRange curRange = null;
                try
                { curRange = provider.GetRange(from, to, m); }
                catch (WindEnergyException wex) // если не удалось получить ряд этой МС, то переходим к следующей
                { continue; }


                curRange = Checker.ProcessRange(curRange, new CheckerParameters(LimitsProviders.StaticLimits, curRange.Position), out CheckerInfo info, null); //исправляем ошибки


                //СКОРОСТЬ
                MeteorologyParameters parameter = MeteorologyParameters.Speed;

                Dictionary<double, double> funcSpeedCurrentNearest = curRange.GetFunction(parameter); //функция скорости на текущей МС

                //проверка на нормальный закон распределения
                bool normal = CheckNormalLaw(curRange, Vars.Options.NormalLawPirsonCoefficientDiapason);
                if (!normal)
                    continue;

                //расчёт и проверка коэфф корреляции
                List<double>[] table = calcTableCoeff(funcSpeed, funcSpeedCurrentNearest); //таблица для расчет коэффициентов
                double current_r = getParameterR(table); //коэффициент корреляции

                //общий максимальный коэфф корреляции
                if (current_r > total_rmax)
                    total_rmax = current_r;

                //проверяем, можно ли взять эту МС
                if (current_r > rmax)
                {
                    //истина, если надо проверять этот параметр на допустимый диапазон корреляции
                    bool needCheck = Vars.Options.MinimalCorrelationControlParametres.Contains(parameter);
                    if ((needCheck && current_r >= Vars.Options.MinimalCorrelationCoeff) || !needCheck)
                    {
                        rmax = current_r;
                        res = curRange;
                    }
                }
            }
            r = rmax;
            if (res == null)
            {
                RP5MeteostationInfo mi = Vars.RP5Meteostations.GetNearestMS(coordinates);
                double l = EarthModel.CalculateDistance(mi.Position, coordinates);
                throw new GetBaseRangeException(total_rmax, Vars.Options.MinimalCorrelationCoeff, l, mts.Count, Vars.Options.NearestMSRadius, coordinates);

            }
            return res;
        }

        /// <summary>
        /// проверяет скорости и направления в заданном ряду на соответствие нормальному закону распределения. 
        /// Возвращает false если ряд не соответствует
        /// </summary>
        /// <param name="baseRange">проверяемый ряд</param>
        /// <param name="acceptDiapason">допустимый диапазон</param>
        /// <returns></returns>
        internal static bool CheckNormalLaw(RawRange baseRange, Diapason<double> acceptDiapason)
        {
            return true;

            double Xi_speed = checkNormalLaw(baseRange, MeteorologyParameters.Speed);
            double Xi_dir = checkNormalLaw(baseRange, MeteorologyParameters.Direction);
            double f = Math.Max(Xi_dir, Xi_speed); //максимально расхождение
            return acceptDiapason.From < f && acceptDiapason.To > f;
        }

        /// <summary>
        /// проверка на соответствие нормальному закону распределния ряда. озвращая критерий согласия Пирсона для этого ряда
        /// http://www.ekonomstat.ru/kurs-lektsij-po-teorii-statistiki/403-proverka-sootvetstvija-rjada-raspredelenija.html
        /// https://life-prog.ru/2_84515_proverka-po-kriteriyu-hi-kvadrat.html критерий пирсона
        /// https://math.semestr.ru/group/example-normal-distribution.php для нормального распределения
        /// </summary>
        /// <param name="range">ряд</param>
        /// <param name="parameter">проверяемый параметр</param>
        /// <returns></returns>
        private static double checkNormalLaw(RawRange range, MeteorologyParameters parameter)
        {
            GradationInfo<GradationItem> grads;
            switch (parameter)
            {
                case MeteorologyParameters.Speed:
                    grads = new GradationInfo<GradationItem>(0, SPEED_GRADATION_STEP, range.Max((item) => { return item.Speed; })); //градации скорости
                    break;
                case MeteorologyParameters.Direction:
                    StatisticalRange<WindDirections16> srwd = StatisticEngine.GetDirectionExpectancy(range, GradationInfo<WindDirections16>.Rhumb16Gradations);

                    return 0;
                case MeteorologyParameters.Temperature:
                    grads = new GradationInfo<GradationItem>(0, TEMPERATURE_GRADATION_STEP, range.Max((item) => { return item.Temperature; })); //градации температуры
                    break;
                case MeteorologyParameters.Wetness:
                    grads = new GradationInfo<GradationItem>(0, WETNESS_GRADATION_STEP, range.Max((item) => { return item.Wetness; })); //градации влажности
                    break;
                default: throw new WindEnergyException("Этот параметр не реализован");
            }

            //РАСЧЕТ ДЛЯ ВСЕХ, КРОМЕ НАПРАВЛЕНИЙ
            StatisticalRange<GradationItem> stat_range = StatisticEngine.GetExpectancy(range, grads, parameter); //статистический ряд
            // TODO: расчет критерия пирсона для ряда

            return 0;
        }

        /// <summary>
        /// получить значение функции по заданному значению
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double GetValue(double x)
        {
            if (Empty)
                return double.NaN;
            if (x > interpolationDiapason.To || x < interpolationDiapason.From) //если х выходит за границы диапазона функции, то ошибка
                throw new ArgumentOutOfRangeException("Значение х должно быть внутри диапазона обеих функций");

            if (getRes != null)
                return getRes(x);
            else
                throw new Exception("Функция получения результата не задана");
        }

        #region вспомогательные методы



        #region Рассчет коэффициента корреляции и параметров прямой

        /// <summary>
        /// расчет таблицы для получения коэффициентов a,b,r
        /// </summary>
        /// <param name="func">исходная функция</param>
        /// <param name="baseFunc">функция ближайшей МС</param>
        /// <returns></returns>
        private static List<double>[] calcTableCoeff(Dictionary<double, double> func, Dictionary<double, double> baseFunc)
        {
            //V1 - заданная функция
            //V2 - функция на ближайшей МС
            //V1*V2
            //V1^2
            //dV1=V1-averV1
            //dV2=V2-averV2
            //dV1*dV2
            //dV1^2
            //dV2^2
            List<double>[] res = new List<double>[9];
            for (int i = 0; i < res.Length; i++) res[i] = new List<double>();

            double averV1 = func.Values.Average();
            double averV2 = baseFunc.Values.Average();

            //ищем совпадающие значения и расчитываем строки
            foreach (var k in func.Keys)
            {
                if (baseFunc.ContainsKey(k))
                {
                    //для коэфф a,b
                    double v1 = func[k];
                    double v2 = baseFunc[k];
                    res[0].Add(v1);
                    res[1].Add(v2);
                    res[2].Add(v1 * v2);
                    res[3].Add(v1 * v1);

                    //для коэфф корреляции
                    double dv1 = v1 - averV1;
                    double dv2 = v2 - averV2;
                    res[4].Add(dv1);
                    res[5].Add(dv2);
                    res[6].Add(dv1 * dv2);
                    res[7].Add(dv1 * dv1);
                    res[8].Add(dv2 * dv2);
                }
            }

            //суммы в конце рядов
            for (int i = 0; i < res.Length; i++)
            {
                double s = res[i].Sum();
                res[i].Add(s);
            }

            bool f = res[0].Count == res[1].Count && res[1].Count == res[2].Count && res[2].Count == res[3].Count &&
                     res[3].Count == res[4].Count && res[4].Count == res[5].Count && res[5].Count == res[6].Count &&
                     res[6].Count == res[7].Count && res[7].Count == res[8].Count;
            if (!f) { throw new Exception("расчет таблицы не удался"); }

            return res;
        }

        /// <summary>
        /// получить коэффициент корреляции
        /// </summary>
        /// <param name="tableCoeff">таблица для расчета коэффициентов</param>
        /// <returns></returns>
        private static double getParameterR(List<double>[] tableCoeff)
        {
            int n = tableCoeff[0].Count - 1; //-1 тк последний элемент - сумма
            double s_dv1dv2 = tableCoeff[6][tableCoeff[6].Count - 1];
            double s_dv1dv1 = tableCoeff[7][tableCoeff[7].Count - 1];
            double s_dv2dv2 = tableCoeff[8][tableCoeff[8].Count - 1];
            return s_dv1dv2 / (Math.Sqrt(s_dv1dv1 * s_dv2dv2));
        }

        /// <summary>
        /// получить параметр b прямой 
        /// </summary>
        /// <param name="tableCoeff">таблица для расчета коэффициентов</param>
        /// <param name="a">параметр А</param>
        /// <returns></returns>
        private static double getParameterB(List<double>[] tableCoeff, double a)
        {
            int n = tableCoeff[0].Count - 1; //-1 тк последний элемент - сумма
            double s_v1 = tableCoeff[0][tableCoeff[0].Count - 1];
            double s_v2 = tableCoeff[1][tableCoeff[1].Count - 1];
            return (s_v2 - a * s_v1) / (n);
        }

        /// <summary>
        /// получить параметр а прямой 
        /// </summary>
        /// <param name="tableCoeff">таблица для расчета коэффициентов</param>
        /// <returns></returns>
        private static double getParameterA(List<double>[] tableCoeff)
        {
            int n = tableCoeff[0].Count - 1; //-1 тк последний элемент - сумма
            double s_v1 = tableCoeff[0][tableCoeff[0].Count - 1];
            double s_v2 = tableCoeff[1][tableCoeff[1].Count - 1];
            double s_v1v2 = tableCoeff[2][tableCoeff[2].Count - 1];
            double s_v1v1 = tableCoeff[3][tableCoeff[3].Count - 1];

            return (n * s_v1v2 - s_v1 * s_v2) / (n * s_v1v1 - Math.Pow(s_v1, 2));
        }

        #endregion

        /// <summary>
        /// загружает ряд наблюдений с ближайшей МС в заданном в анстройках радиусе
        /// </summary>
        /// <param name="func">функция исходного ряда (для определения начала и конца ряда)</param>
        /// <param name="coordinates">координаты исходного ряда</param>
        /// <returns></returns>
        private static RawRange getNearestRange(Dictionary<double, double> func, PointLatLng coordinates)
        {
            DateTime fr, to;
            fr = DateTime.MinValue + TimeSpan.FromMinutes(func.Keys.ToList().Min());
            to = DateTime.MinValue + TimeSpan.FromMinutes(func.Keys.ToList().Max());
            return getNearestRange(fr, to, coordinates);
        }

        /// <summary>
        /// загружает ряд наблюдений с ближайшей МС
        /// </summary>
        /// <param name="from">начало ряда</param>
        /// <param name="to">конец ряда</param>
        /// <param name="coordinates">координаты исходного ряда</param>
        /// <returns></returns>
        private static RawRange getNearestRange(DateTime from, DateTime to, PointLatLng coordinates)
        {
            if (from > to)
                throw new WindEnergyException("Дата from больше, чем to");
            RawRange res = null;

            RP5MeteostationInfo nearestMS = Vars.RP5Meteostations.GetNearestMS(coordinates);
            if (nearestMS == null)
                throw new Exception("Не удалось найти ближайшую метеостанцию в заданном радиусе");

            RP5ru provider = new RP5ru(Vars.Options.CacheFolder + "\\rp5.ru");
            provider.GetMeteostationExtInfo(ref nearestMS);

            if (from < nearestMS.MonitoringFrom) //если исходный ряд начинается
                from = nearestMS.MonitoringFrom;
            if (from > to)
                throw new Exception("Ряды не пересекаются: один из рядов заканчивается раньше, чем начинается другой");
            res = provider.GetRange(from, to, nearestMS);
            return res;
        }



        #endregion
    }
}
