using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace QTrans.Helpers
{
    public class FileHelper
    {

        // public static TransferBase trans = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="keptLevel"></param>
        /// <returns></returns>
        public string GetDirectory(string path, int keptLevel = 0)
        {
            keptLevel = keptLevel < 0 ? 65535 : keptLevel;
            if (path == null || path.Trim().Length == 0)
                return "";

            string[] folders = path.Trim().Split(new char[] { '\\', ':', '/' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            sb.Append(folders[folders.Length - 1]);
            for (int i = 0; i < keptLevel && i < folders.Length - 1; i++)
            {
                sb.Insert(0, folders[folders.Length - 2 - i] + "\\");
            }

            return sb.ToString();
        }

        public string AppendSuffix(string path, string suffix)
        {
            if (path == null || path.Trim().Length == 0)
                return "";
            string dir = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);
            return string.Format("{0}\\{1}{2}{3}", dir, name, suffix, ext);
        }

        public string AppendPrefix(string path, string prefix)
        {
            if (path == null || path.Trim().Length == 0)
                return "";
            string dir = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);
            return string.Format("{0}\\{1}{2}{3}", dir, prefix, name, ext);
        }

        /// <summary>
        /// 获得目录最后一个名子，如c:\abc\ddd 则返回ddd
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetDirectoryLastName(string path)
        {
            try
            {
                string[] sts = path.Split('\\');
                return sts[sts.Length - 1];
            }
            catch { }
            return "";
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
    }
}
