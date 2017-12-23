using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using QTrans.Classes;

namespace QDasTransfer
{
	public class Funs
	{
		/// <summary>
		/// 判断在一个ListBox中是否存在某个字符串。
		/// </summary>
		/// <param name="lb">指定的ListBox</param>
		/// <param name="s">指定字符串。</param>
		/// <returns></returns>
		public static bool ExistItem(ListBox lb, string s)
		{
			foreach (object item in lb.Items)
			{
				if (item.ToString().Equals(s))
				{
					return true;
				}
			}
			return false;
		}


		/// <summary>
		/// 从程序的资源文件中拷贝数据文件到指定路径。
		/// 如："PolisInfo.Data.PolisInfo.pda" -> D:\Data\info.data
		/// </summary>
		/// <param name="dest">需要拷贝出的目标路径。</param>
		/// <param name="source">在程序中的源路径，默认为QDasTransfer.BinRes.PdfToTxt.exe。</param>
		public static bool CopyResourceDataFile(string dest, string source = "QDasTransfer.BinRes.PdfToTxt.exe")
		{
			try
			{
				Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(source);
				int t = (int)stream.Length;
				FileStream fs = new FileStream(dest, FileMode.Create, FileAccess.Write);
				byte[] bts = new byte[t];
				stream.Read(bts, 0, bts.Length);
				fs.Write(bts, 0, bts.Length);
				fs.Close();
				stream.Close();
			}
			catch (Exception e1)
			{
				Console.WriteLine(e1.Message);
				return false;
			}
			return true;
		}


		/// <summary>
		/// 将一个字符串转成加密的数字编码。
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public static string EncodeString(string original)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < original.Length; i++)
			{
				int v = (int)original[i] + i;
				sb.Append(v.ToString("000"));
			}
			return sb.ToString();
		}

		/// <summary>
		/// 将一个字符串转成加密的数字编码。
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string DecodeString(string encoded)
		{
			if (string.IsNullOrEmpty(encoded))
				return null;

			try
			{
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < encoded.Length / 3; i++)
				{
					string tmp = encoded.Substring(i * 3, 3);
					int v = int.Parse(tmp) - i;

					sb.Append((char)v);
				}
				return sb.ToString();
			}
			catch { }

			return null;

		}

		/// <summary>
		/// 添加唯一字符串至ListBox，仅当ListBox中无此项才会添加。
		/// </summary>
		/// <param name="lb"></param>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool AddDistinctToListBox(ListBox lb, string s)
		{
			if (!ExistItem(lb, s))
			{
				lb.Items.Add(s);
				return true;
			}
			return false;
		} 

		/// <summary>
		/// 设置应用程序开机自动运行
		/// </summary>
		/// <param name="fileName">应用程序的文件名</param>
		/// <param name="isAutoRun">是否自动运行，为false时，取消自动运行</param>
		/// <exception cref="System.Exception">设置不成功时抛出异常</exception>
		public static void SetAutoRun(string fileName, bool isAutoRun)
		{
			RegistryKey reg = null;
			try
			{
				if (!System.IO.File.Exists(fileName))
					throw new Exception("该文件不存在!");

				String name = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
				reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
				if (reg == null)
					reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
				if (isAutoRun)
					reg.SetValue(name, fileName);
				else
					reg.SetValue(name, false);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
			finally
			{
				if (reg != null)
					reg.Close();
			}
		}
		//另外也可以写成服务，不过服务的话一般是在后台执行的，没有程序界面。

		/// <summary>
		/// 创建目录，如果创建失败，返回为false
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool CreateFolder(string path)
		{
			try
			{
				Directory.CreateDirectory(path);

				if (!Directory.Exists(path))
				{
					MessageBox.Show("输出文件夹设置不正确，请重新设置。");
					return false;
				}
			}
			catch// (Exception ex)
			{ //MessageBox.Show("路径创建失败，原因：" + ex.Message);
				return false;
			}

			return true;
		}

		/// <summary>
		/// 添加日志信息。
		/// </summary>
		/// <param name="log">日志内容。</param>
		public static void AddLog(string log)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter("runtime.log", true, Encoding.Default))
				{
					sw.WriteLine(DateTime.Now + "\t" + log);
				}
			}
			catch { }
		}

		/// <summary>
		/// 添加日志信息。
		/// </summary>
		/// <param name="log">日志内容。</param>
		public static void AddLog(QTrans.Classes.TransLog log)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter("runtime.log", true, Encoding.Default))
				{
					sw.WriteLine(log.ToFileString());
				}
			}
			catch { }
		}


		/// <summary>
		/// 添加文件到日志。
		/// </summary>
		/// <param name="events">事件类型。</param>
		/// <param name="reason">原因。</param>
		/// <param name="file">相关文件。</param>
		public static void AddLog(string events, string reason, string file)
		{
			Funs.AddLog(events + "\t" + reason + "\t" + file);
		}

		/// <summary>
		/// 添加文件到日志。
		/// </summary>
		/// <param name="events">事件类型。</param>
		/// <param name="reason">原因。</param>
		public static void AddLog(string events, string reason)
		{
			Funs.AddLog(events + "\t" + reason + "\t无");
		}


		public static void CreateShortCut(string path)
		{
			IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
			IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(path
			);

			shortcut.TargetPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			shortcut.WorkingDirectory = System.Environment.CurrentDirectory;
			shortcut.WindowStyle = 1;
			shortcut.Description = "Launch Allen’s Application";
			//shortcut.IconLocation = System.Environment.SystemDirectory + "\\" + "shell32.dll, 165";
			shortcut.Save();
		}

		public static string GetDateString(DateTime dt)
		{
			return string.Format("{0}{1}{2}{3}{4}{5}",
				dt.Year,
				dt.Month.ToString("00"),
				dt.Day.ToString("00"),
				dt.Hour.ToString("00"),
				dt.Minute.ToString("00"),
				dt.Second.ToString("00"));
		}

		/// <summary>
		/// 复制文件，注意几点：
		/// 1、如果源文件不存在，写文件不存在错误。
		/// 2、如果目标文件已经存在，加时间编号，如file.xls -> file20120519092732.dfq
		/// </summary>
		/// <param name="scr">源文件</param>
		/// <param name="dst">目标文件</param>
		public static void MoveFile(string scr, string dst)
		{
			try
			{
				if (!File.Exists(scr))
					AddLog("FILE\t复制时源文件: " + scr + " 不存在。");

				if (File.Exists(dst))
				{
					dst = AppendTimeToFileName(dst);
				}

				File.Copy(scr, dst);

				File.Delete(scr);
			}
			catch (Exception e1)
			{
				AddLog("FILE\t" + e1.Message);
			}
		}


		/// <summary>
		/// 给指定文件名加时间戳。
		/// </summary>
		/// <param name="dst">目标地址。</param>
		/// <returns></returns>
		public static string AppendTimeToFileName(string dst)
		{
			try
			{
				string path = Path.GetDirectoryName(dst);
				string filename = Path.GetFileNameWithoutExtension(dst);
				string ext = Path.GetExtension(dst);
				return path + "\\" + filename + "_" + GetDateString(DateTime.Now) + ext;
			}
			catch { }

			return dst;
		}

		/// <summary>
		/// 获取给定目录下的指定层数的文件。如：获得C：根目录下三层所有文件。
		/// </summary>
		/// <param name="dir">文件目录。</param>
		/// <param name="lv">层数最大为10，为0时，只访问当前目录下数据。</param>
		/// <returns></returns>
		public static string[] GetFiles(string dir, int lv)
		{
			List<string> list = new List<string>();
			list.AddRange(Directory.GetFiles(dir));
			lv = lv > 10 ? 10 : lv;

			if (lv > 0)
			{
				string[] dirs = Directory.GetDirectories(dir);
				foreach (string s in dirs)
				{
					list.AddRange(GetFiles(s, lv - 1));
				}
			}

			return list.ToArray();
		}


		/// <summary>
		/// 获得指定对象的事件名称的委托。
		/// </summary>
		/// <param name="p_Object"></param>
		/// <param name="p_EventName"></param>
		/// <returns></returns>
		public static Delegate[] GetObjectEventList(object p_Object, string p_EventName)
		{
			// Get event field  
			FieldInfo _Field = p_Object.GetType().GetField(p_EventName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
			if (_Field == null)
			{
				return null;
			}
			// get the value of event field which should be a delegate  
			object _FieldValue = _Field.GetValue(p_Object);

			// if it is a delegate  
			if (_FieldValue != null && _FieldValue is Delegate)
			{
				// cast the value to a delegate  
				Delegate _ObjectDelegate = (Delegate)_FieldValue;
				// get the invocation list  
				return _ObjectDelegate.GetInvocationList();
			}
			return null;
		}

		public static void SelectFolder(TextBox tb)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.SelectedPath = tb.Text;
			if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				tb.Text = fbd.SelectedPath;
			}
		}


		/// <summary>
		/// 在Windows浏览器中打开指定的路径，如果失败会报错。
		/// </summary>
		/// <param name="path"></param>
		public static void OpenFolderInWindows(string path)
		{
			try
			{
				if (File.Exists(path))
				{
					System.Diagnostics.Process.Start("explorer.exe", "/select, " + path + "\\" + ", /n");
				}
				else if (Directory.Exists(path))
				{
					System.Diagnostics.Process.Start("Explorer.exe", path);
				}
				else
				{
					MessageBox.Show("路径不存在，无法打开。", "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception e1)
			{
				MessageBox.Show("路径打开失败。原因：" + e1.Message, "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Funs.AddLog(e1.Message);
			}
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

	}
}

