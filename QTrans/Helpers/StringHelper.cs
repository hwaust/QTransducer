using QTrans.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QTrans.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// 计算给定字符串的MD5码。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringHash(string str)
        {
            byte[] data = Encoding.Default.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            data = md5.ComputeHash(data);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }


    }
}
