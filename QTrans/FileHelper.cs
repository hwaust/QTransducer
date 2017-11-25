using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace QTrans
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
