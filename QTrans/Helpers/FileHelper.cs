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

        public static TransferBase trans = null;


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
        /// 将指定文件从一个地方移动到另一个地方，如果成功返回TRUE，如果出错，在LogList中会有相关错误信息。
        /// </summary>
        /// <param name="source">源文件。</param>
        /// <param name="dest">待转移文件。</param>
        /// <returns></returns>
        public static bool MoveFile(string source, string dest)
        {
            if (!CopyFile(source, dest))
                return false;

            if (!DeleteFile(source))
                return false;

            return true;
        }

        /// <summary>
        /// 将指定文件复制至指定路径，同时在LogList中会有相关错误信息。
        /// </summary>
        /// <param name="source">源文件路径。</param>
        /// <param name="dest">需要移动的目标路径。</param>
        /// <returns></returns>
        public static bool CopyFile(string source, string dest)
        {
            try
            {
                if (!File.Exists(source))
                {
                    if (trans != null)
                        trans.AddFailedFile(source, "源文件不存在。");
                    return false;
                }

                // sleep 100 ms for waiting the copy to totally complete.
                Thread.Sleep(100);

                if (File.Exists(dest))
                    File.Delete(dest);

                File.Copy(source, dest, true);
            }
            catch (Exception e1)
            {
                string error = source + " 复制失败，原因：" + e1.Message;
                WindGoes.Functions.AddLogToFile(error);
                if (trans != null)
                    trans.AddFailedFile(source, error);
                return false;
            }
            return true;
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
        /// 删除指定文件，如果出错会写入日志。
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool DeleteFile(string file)
        {
            try
            {
                File.Delete(file);
                Thread.Sleep(100);
            }
            catch (Exception e1)
            {
                if (trans != null)
                    trans.AddFailedFile(file, "文件删除失败。原因：" + e1.Message);
                return false;
            }

            return true;
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
        /// 将输出目录和输出文件名合成为输出路径。如 D:\output  + ofile -> d:\output\ofile.dfq
        /// </summary>
        /// <param name="folder">输出目录。</param>
        /// <param name="filename">输出文件名。</param>
        /// <returns></returns>
        public static string CombineOutPath(string folder, string filename)
        {
            return Path.Combine(folder, filename + ".dfq");
        }
    }
}
