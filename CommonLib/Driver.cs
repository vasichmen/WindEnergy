﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace CommonLib
{
    internal class Creator : Convoluter
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

    public static class Driver
    {
        private static string cacheID1 = null;
        private static string cacheID2 = null;

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

        private static string getFullKey()
        {
            SHA256 crypto = SHA256.Create();
            byte[] buffer = Encoding.Unicode.GetBytes(keygen().ToString() + GetID1() + GetID2());
            byte[] hashBuffer = crypto.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBuffer)
                sb.Append(b);
            return sb.ToString();

        }

        public static bool CheckFullKey()
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\WindEnergy");
            if (regKey == null) return false;
            string value = (string)regKey.GetValue("Hash", null);
            string hash = getFullKey();
            return hash.Equals(value);
        }

        public static void GenerateFullKey()
        {
            string hash = getFullKey();
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\WindEnergy");
            regKey.SetValue("Hash", hash, RegistryValueKind.String);
        }


        #region USED
        //Метод для получения ProcessorID
        private static string GetID1()
        {
            if (cacheID1 != null)
                return cacheID1;
            string ProcessorID = string.Empty;
            SelectQuery query = new SelectQuery("Win32_processor");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = searcher.Get().GetEnumerator();
            while (enumerator.MoveNext())
            {
                ManagementObject info = (ManagementObject)enumerator.Current;
                ProcessorID = info["processorId"].ToString().Trim();
            }
            cacheID1 = ProcessorID;
            return ProcessorID;
        }
        //Метод для получения MotherBoardID
        private static string GetID2()
        {
            if (cacheID2 != null)
                return cacheID2;
            string MotherBoardID = string.Empty;
            SelectQuery query = new SelectQuery("Win32_BaseBoard");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);


            ManagementObjectCollection.ManagementObjectEnumerator enumerator = searcher.Get().GetEnumerator();
            while (enumerator.MoveNext())
            {
                ManagementObject info = (ManagementObject)enumerator.Current;
                MotherBoardID = info["SerialNumber"].ToString().Trim();
            }
            cacheID2 = MotherBoardID;
            return MotherBoardID;
        }
        #endregion
    }
}
