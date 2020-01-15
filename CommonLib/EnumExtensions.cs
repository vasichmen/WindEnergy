using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    /// <summary>
    /// расширения типов перечислений
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// значение атрибута Description у перечисления
        /// </summary>
        /// <param name="enumElement"></param>
        /// <returns></returns>
        public static string Description(this Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }

        /// <summary>
        /// получить все описания значений Enum для вывода в выпадающий список
        /// </summary>
        /// <returns></returns>
        public static List<string> GetItems(this Enum element)
        {
            List<string> res = new List<string>();
            foreach (Enum en in Enum.GetValues(element.GetType()))
                res.Add(en.Description());
            return res;
        }
        
        /// <summary>
        /// получить все значения Enum
        /// </summary>
        /// <returns></returns>
        public static List<Enum> GetEnumItems(this Enum element)
        {
            List<Enum> res = new List<Enum>();
            foreach (Enum en in Enum.GetValues(element.GetType()))
                res.Add(en);
            return res;
        }
    }


    /// <summary>
    /// преобразователь типов для перечислений
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumTypeConverter<T> : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (object.ReferenceEquals(sourceType, typeof(string)))
                return true;
            return base.CanConvertTo(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (object.ReferenceEquals(destinationType, typeof(string)))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// преобразование из любого объекта в тип перечисления
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (object.ReferenceEquals(value.GetType(), typeof(string)))
            {
                string str = value as string;
                foreach (Enum en in Enum.GetValues(typeof(T)))
                    if (en.Description().Equals(str))
                        return en;
                throw new Exception("Этой строки нет в заданном перечислении");
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// преобразование из перечисления в заданный тип
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (object.ReferenceEquals(destinationType, typeof(string)))
            {
                return (value as Enum).Description();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
