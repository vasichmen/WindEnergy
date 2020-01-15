using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy
{
    class Convoluter
    {
        int m0 = 0x00000001;
        int m2 = 0x00000004;
        int m30 = 0x40000000;
        // Шаг обработки
        protected void Cript31Step(ref int a, int c)
        {
            int z, z2, z30;
            int b;
            int d;
            d = a & m30;
            if (d != 0) z30 = 1; else z30 = 0;
            d = a & m2;
            if (d != 0) z2 = 1; else z2 = 0;
            if (z30 == z2) z = 0; else z = 1;
            if (z == c) b = 1; else b = 0;
            a = a << 1;
            if (b != 0) a = a | m0;
        }
        // Прокрутка на n разрядов
        void Cript31Prokr(ref int a, int n)
        {
            int c = 0;
            for (int i = 0; i < n; i++)
                Cript31Step(ref a, c);
        }
        // Формирование свёртки
        public void Cript31(byte[] p, int n, ref int a)
        {
            byte r;
            int c, d;
            byte mask = 0x80;
            // Цикл по байтам
            for (int i = 0; i < n; i++)
            {
                r = p[i];
                // Цикл по битам байта
                for (int j = 0; j < 8; j++)
                {
                    d = r & mask;
                    if (d != 0) c = 1; else c = 0;
                    Cript31Step(ref a, c);
                    d = r << 1;
                    r = (byte)d;
                }
            }
            // Прокрутка на 31 разряд
            Cript31Prokr(ref a, 31);
        }
    }
}
