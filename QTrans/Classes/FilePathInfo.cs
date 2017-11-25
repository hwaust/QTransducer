using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QTrans.Classes
{
	/// <summary>
	/// 用于表示一个文件路径信息的类，包括文件名，文件后缀名等信息。
	/// </summary>
	public class FilePathInfo
	{
		string filePath = string.Empty;
		/// <summary>
		/// 当前所表示的路径。
		/// </summary>
		public string FilePath
		{
			get { return filePath; }
			set
			{
				if (!string.IsNullOrEmpty(value) && !value.Equals(filePath))
				{
					filePath = value;
					SetPath(filePath);
				}
			}
		}

		/// <summary>
		/// 是否是文件夹。
		/// </summary>
		public bool IsFolder { get; set; }

		/// <summary>
		/// 文件名，不包括后缀名。
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// 文件的后缀名，不包括点，如 a.txt -> txt	
		/// </summary>
		public string FileExtention { get; set; }

		/// <summary>
		/// 文件所在的文件夹。
		/// </summary>
		public string FileFolder { get; set; }


		/// <summary>
		/// 当前路径的根目录，如： C:\, D:\ 等。 
		/// </summary>
		public string Root { get; set; }

		/// <summary>
		/// 文件的路径中的文件夹名，如 C:\temp\data\a.txt -> [0]:temp, [1] data;
		/// </summary>
		public string[] FolderNames { get; set; }

		/// <summary>
		/// 文件的路径深度。在根目录下为0，每一层加1。
		/// </summary>
		public int Depth
		{
			get { return FolderNames == null ? 0 : FolderNames.Length - 1; }
		}

		/// <summary>
		/// 使用一个标准的路径进行初始化。
		/// </summary>
		/// <param name="p">标准路径。</param>
		public FilePathInfo(string p)
		{
			FilePath = p;
		}

		private bool SetPath(string p)
		{
			if (string.IsNullOrEmpty(p))
				return false;
			filePath = p;

			try
			{
				FileName = Path.GetFileNameWithoutExtension(filePath);
				FileExtention = Path.GetExtension(filePath).Substring(1).ToLower();
				FileFolder = Path.GetDirectoryName(filePath);
				FolderNames = FileFolder.Split('\\');
				Root = FolderNames[0][0].ToString().ToLower();
			}
			catch
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// 与指定的路径信息A对比，仅当A是总目录，本体是总目录下的目录才可以比较。
		/// </summary>
		/// <param name="fi"></param>
		/// <returns></returns>
		public string CompareFolder(FilePathInfo fi)
		{
			if (fi == null || string.IsNullOrEmpty(fi.FilePath))
				return null;


			List<string> list = new List<string>();
			int dp = this.Depth - fi.Depth;

			if (dp <= 0)
				return "";

			//必需保证当前路径是给定路径的子目录。
			for (int i = 0; i < dp; i++)
			{
				if (!fi.FolderNames[i].Equals(this.FolderNames[i]))
				{
					return "";
				}
			}

			//将后面的目录添加到列表中，以便最终返回。
			for (int i = 0; i < dp; i++)
			{
				list.Add(FolderNames[this.Depth + i]);
			}

			return string.Join("\\", list.ToArray());
		}


		public string Output()
		{
			string s = "------  " + filePath + "  -------\n";
			s += "Root = " + Root + "\n";
			s += "Depth = " + Depth + "\n";
			s += "FileName = " + FileName + "\n";
			s += "FileExtention = " + FileExtention + "\n";
			s += "FileFolder = " + FileFolder + "\n";
			s += "FolderNames = " + string.Join(" -> ", FolderNames) + "\n";

			return s;
		}
	}

}
