using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QTrans
{
	public class FilenameInfo
	{
		string filePath;
		/// <summary>
		/// 文件全路径，包括文件名，如：C:\abc\bbc.txt。
		/// </summary>
		public string FilePath
		{
			get { return filePath; }
			set { filePath = value; Analyze(); }
		}

		string fileName;
		/// <summary>
		/// 文件的名子。
		/// </summary>
		public string FileName
		{
			get { return fileName; }
			set { fileName = value; }
		}

		string extention;
		/// <summary>
		/// 扩展名(首位为点)。
		/// </summary>
		public string Extention
		{
			get { return extention; }
			set { extention = value; }
		}

		string fullDirectory;
		/// <summary>
		/// 全路径名称，不包括文件名，如C:\abc
		/// </summary>
		public string FullDirectory
		{
			get { return fullDirectory; }
			set { fullDirectory = value; }
		}

		string lastDirectoryName = string.Empty;
		/// <summary>
		/// 目录的最后一个名称，如C:\abc\bbc.txt，则此值为abc
		/// </summary>
		public string LastDirectoryName
		{
			get { return lastDirectoryName; }
			set { lastDirectoryName = value; }
		}


		public FilenameInfo(string fp)
		{
			FilePath = fp;
		}


		/// <summary>
		/// 根据指定全路径，计算路径，文件名和后缀名。
		/// </summary>
		public void Analyze()
		{
			fileName = Path.GetFileNameWithoutExtension(filePath);
			extention = Path.GetExtension(filePath);
			fullDirectory = Path.GetDirectoryName(filePath);

			string[] tmp = fullDirectory.Split('\\');

			if (tmp != null && tmp.Length > 1)
			{
				lastDirectoryName = tmp[tmp.Length - 1];
			}
		}


	}
}
