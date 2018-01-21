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
        /// <summary>
        /// Return the last N levels of path. If N is larger than the number L of steps of a path, N is then set to L.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static string GetLastDirectory(string path, int N = 0)
        {
            N = N < 0 ? 65535 : N;
            if (path == null || path.Trim().Length == 0)
                return "";

            string[] folders = path.Trim().Split(new char[] { '\\', ':', '/' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            // sb.Append(folders[folders.Length - 1]);
            for (int i = 0; i < N && i < folders.Length - 1; i++)
            {
                sb.Insert(0, folders[folders.Length - 2 - i] + "\\");
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        /// <summary>
        /// Append a suffix to the path. 
        /// <example>"C:\data\a.txt" + "_20171222" => "C:\data\a_20171222.txt"</example>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string AppendSuffix(string path, string suffix)
        {
            if (path == null || path.Trim().Length == 0)
                return "";
            string dir = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);
            return string.Format("{0}\\{1}{2}{3}", dir, name, suffix, ext);
        }

        /// <summary>
        /// Append a prefix to the path.
        /// <example>"C:\data\a.txt" + "20171222_" => "C:\data\20171222_a.txt"</example>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string AppendPrefix(string path, string prefix)
        {
            if (path == null || path.Trim().Length == 0)
                return "";
            string dir = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);
            return string.Format("{0}\\{1}{2}{3}", dir, prefix, name, ext);
        }

        /// <summary>
        /// Adds a four-digit id to the filename infront of its extention.
        /// </summary>
        /// <param name="outputfile"></param>
        /// <returns></returns>
        public static String AddIncreamentId(string outputfile)
        {
            int suffixid = 0;
            string folder = Path.GetDirectoryName(outputfile);
            string filename = Path.GetFileNameWithoutExtension(outputfile);
            string ext = Path.GetExtension(outputfile);

            while (File.Exists(outputfile))
            {
                suffixid++;
                outputfile = string.Format("{0}\\{1}_{2}{3}", folder, filename, suffixid.ToString("0000"), ext);
            }

            return outputfile;
        }


        public static bool CopyFile(string inputfile, string outputfile)
        {
            // Try 5 times at most. Note: input file should exist and outputfile should not.
            int copyTimes = 0;
            while (true)
            {
                File.Copy(inputfile, outputfile);
                Thread.Sleep(100);
                if (File.Exists(outputfile) || copyTimes++ > 5)
                    break;
            }

            return File.Exists(outputfile);
        }



        /// <summary>
        /// Deletes the input file when backuping. Tries 5 times and adds to logs if fails.
        /// </summary>
        /// <param name="inputfile"></param>
        public static bool DeleteFile(string inputfile)
        {
            // Delete 5 times at most.
            int deleteTimes = 0;
            while (true)
            {
                File.Delete(inputfile);
                Thread.Sleep(100);

                if (!File.Exists(inputfile) || deleteTimes++ > 5)
                    break;
            }

            return !File.Exists(inputfile);
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
        /// 给定路径，获得下面指定层数的文件。
        /// </summary>
        /// <param name="dir">指定的路径。</param>
        /// <param name="lv">子目录层数，最少为0，即只获得当前目录下文件。</param>
        /// <returns></returns>
        public string[] GetFile(string dir, int lv)
        {
            List<string> list = new List<string>();
            list.AddRange(Directory.GetFiles(dir));
            if (lv > 0)
            {
                string[] dirs = Directory.GetDirectories(dir);

                foreach (string s in dirs)
                {
                    list.AddRange(GetFile(s, lv - 1));
                }
            }

            return list.ToArray();
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

        /// <summary>
        ///  infile: d:\data\2018\01\de_01.txt
        ///  inroot: d:\data
        ///  outdir: e:\out
        ///  outfile: e:\out\2018\01\de_01.txt
        /// </summary>
        /// <param name="infile"></param>
        /// <param name="inroot"></param>
        /// <param name="outdir"></param>
        /// <returns></returns>
        public static string GetOutFolder(string infile, string inroot, string outdir)
        {
            return (outdir.Trim('\\')  + "\\" + infile.Substring(inroot.Length).Trim('\\').Replace(":", "")).Trim('\\'); 
        }
    }
}
