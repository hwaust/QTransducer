using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;

namespace QTrans
{
    public class funs
    {  


        /// <summary>
        /// 根据给定输入路径，给出输出路径，主要用于解决输入为多级目录时的目录关系。
        /// 情况1：输入C:\abc.txt， 输入目录为空，输出D:\QDas，则输出为：abc.dfq
        /// 情况2：输入C:\abc.txt， 输入目录为C:，输出D:\QDas，则输出为：abc.dfq
        /// 情况3：输入C:\data\abc.txt， 输入目录为C:\data，输出D:\QDas，则输出为：data\abc.dfq
        /// 情况4：输入C:\data\2012\abc.txt， 输入目录为C:\data，输出D:\QDas，则输出为：data\2012\abc.dfq
        /// 那么输出为相对路径为： D:\QDas\abc\a.dfq，此函数返回结果为abc
        /// </summary>
        /// <param name="path">待检测的输入路径。</param> 
        /// <param name="infolder">输入路径。</param>
        /// <returns></returns> 
        public static string GetOutputPath(string path, string infolder)
        {
            string op = Path.GetFileName(path);

            //如果当前输入目录为空，则返回全文件名。
            if (string.IsNullOrEmpty(infolder) || path.Length < 3)
                return op;

            //则冒号后面的长度小于2，则表示是根目录, 如C:   C:\，。
            string[] data = path.Split(':');
            if (data.Length < 2 || data[1].Length < 2)
                return op;

            return path.Substring(infolder.Length);
        }

        public static string GetOutputFolder(string path, string infolder)
        {
            string op = "C:\\" + GetOutputPath(path, infolder);
            op = Path.GetDirectoryName(op);
            return op.Substring(3);
        }


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



        /// <summary>
        /// 删除指定文件夹下的所有文件，通过递归实现。
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFiles(string path)
        {
            //删除指定文件夹下的所有文件。
            string[] files = Directory.GetFiles(path);
            foreach (string s in files)
            {
                try
                {
                    File.Delete(s);
                }
                catch { }
            }

            //删除文件夹。
            string[] folders = Directory.GetDirectories(path);
            foreach (string fd in folders)
            {
                try
                {
                    DeleteFiles(fd);
                    Directory.Delete(fd);
                }
                catch { }
            }
        }

        public static string GetAttribute(object obj)
        {
            try
            {
                Type t = obj.GetType();
                PropertyInfo[] pis = t.GetProperties();
                StringBuilder builder = new StringBuilder();
                foreach (PropertyInfo pi in pis)
                {
                    builder.Append(pi.Name + " = " + pi.GetValue(obj, null) + ";");
                }
                return builder.ToString();
            }
            catch { }
            return null;
        }




    }
}
