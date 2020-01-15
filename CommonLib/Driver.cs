﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    class Creator : Convoluter
    {
        private readonly int key;

        public Creator(int key) { this.key = key; }
        public byte[] Create(byte[] buff)
        {
            int res = key;
            Cript31(buff, buff.Length, ref res);
            return BitConverter.GetBytes(res);
        }
    }

    public class Driver
    {
        public static byte[] GetID()
        {
            string proc = GetID1();
            string mother = GetID2();
            string data = proc + mother;
            int key = keygen();
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            var c = new Creator(key);
            var res = c.Create(buffer);
            return res;

        }

        private static int keygen()
        {
            int[] buf = new int[] { Encoding.ASCII.GetBytes("*")[0] };
            var n = thunder(buf);
            List<byte> res = new List<byte>();
            foreach (var ff in n)
                res.AddRange(BitConverter.GetBytes(ff));
            int resS = 0;
            foreach (var i in res)
                resS += i;

            return resS;

        }

        private static int[] thunder(int[] init)
        {
            if (init.Length > 1)
            {
                int mask = init[init.Length - 1];
                int[] res = new int[init.Length + 1];
                int nkey = init[1] << mask;
                res[0] = nkey;
                for (int i = 1; i < init.Length; i++)
                {
                    nkey += i | mask;
                    res[i] = nkey;
                }
                res = init.Concat(res).ToArray();
                if (init.Length > init[0])
                    return init;
                else
                    return thunder(res.Concat(new int[] { init[0] }).ToArray());
            }
            else
                return thunder(new int[] { init[0], 80 });
        }


        public static byte[] LoadID(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                List<byte> res = new List<byte>();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.Length > 0)
                        res.Add(byte.Parse(line));
                }
                var result = res.ToArray();
                sr.Close();
                return result;
            }
        }


        #region USED
        //Метод для получения ProcessorID
        private static string GetID1()
        {
            string ProcessorID = string.Empty;
            SelectQuery query = new SelectQuery("Win32_processor");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = searcher.Get().GetEnumerator();
            while (enumerator.MoveNext())
            {
                ManagementObject info = (ManagementObject)enumerator.Current;
                ProcessorID = info["processorId"].ToString().Trim();
            }
            return ProcessorID;
        }
        //Метод для получения MotherBoardID
        private static string GetID2()
        {
            string MotherBoardID = string.Empty;
            SelectQuery query = new SelectQuery("Win32_BaseBoard");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);


            ManagementObjectCollection.ManagementObjectEnumerator enumerator = searcher.Get().GetEnumerator();
            while (enumerator.MoveNext())
            {
                ManagementObject info = (ManagementObject)enumerator.Current;
                MotherBoardID = info["SerialNumber"].ToString().Trim();
            }
            return MotherBoardID;
        }
        #endregion
    }
}