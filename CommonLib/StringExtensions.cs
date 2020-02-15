using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    /// <summary>
    /// расширения для типа String
    /// </summary>
    public static class StringExtensions
    {
        static Dictionary<char, char> invertor = new Dictionary<char, char>() {
            { 'a','a' },
            { 'b','и' },
            { 'c','с' },
            { 'd','в' },
            { 'e','у' },
            { 'f','а' },
            { 'g','п' },
            { 'h','р' },
            { 'i','ш' },
            { 'j','о' },
            { 'k','л' },
            { 'l','д' },
            { 'm','ь' },
            { 'n','т' },
            { 'o','щ' },
            { 'p','з' },
            { 'q','й' },
            { 'r','к' },
            { 's','ы' },
            { 't','е' },
            { 'u','г' },
            { 'v','м' },
            { 'w','ц' },
            { 'x','ч' },
            { 'y','н' },
            { 'z','я' },
        };


        /// <summary>
        /// инверсия строки при неправильной раскладке клавиатуры
        /// </summary>
        /// <param name="stringElement">строка</param>
        /// <returns></returns>
        public static string InvertEn_Ru(this String stringElement)
        {
            string res = "";
            foreach (char ch in stringElement)
                res += invertAnyChar(ch);
            return res;
        }

        /// <summary>
        /// инверсия символа Англ-Рус и наоборот
        /// </summary>
        /// <param name="c">символ</param>
        /// <returns>инвертированный символ</returns>
        private static char invertAnyChar(char c)
        {
            if (char.IsUpper(c))
                return char.ToUpper(invertLowcaseChar(char.ToLower(c)));// перевести в нижний регистр, инвертировать, перевести в верхний регистр
            if (char.IsLower(c))
                return invertLowcaseChar(c);

            return c; //если какой-то другой символ, то ничего не делаем
        }

        /// <summary>
        /// инверстирование символа нижнего регистра
        /// </summary>
        /// <param name="lowcaseChar"></param>
        /// <returns></returns>
        private static char invertLowcaseChar(char lowcaseChar)
        {
            if (invertor.ContainsKey(lowcaseChar)) //англ - рус по словарю
                return invertor[lowcaseChar];
            if (invertor.ContainsValue(lowcaseChar)) //рус - англ наоборот (ищем первое совапдение значения и возвращаем ключ)
                return (from kv in invertor where kv.Value == lowcaseChar select kv.Key).First();
            return lowcaseChar;
        }

    }
}
