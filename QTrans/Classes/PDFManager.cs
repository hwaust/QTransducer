using QTrans.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QTrans.Classes
{
	public class PDFManager
	{
		/// <summary>
		/// Dll转换模块所在路径， 格式后面不带斜杠，标准为： C:\Windows\System32。
		/// </summary>
		public string PdfDllFolder { get; set; }



		/// <summary>
		/// 读取将指定的PDF文件中的文本内容，返回转换后的文本文件内容，需要一堆Dll文件。
		/// </summary>
		/// <param name="path">需要转换的文件路径。</param>
		/// <returns></returns>
		public  string PdfToTxt(string path)
		{
			try
			{
				string input = path;
				string outfolder = Application.StartupPath + "\\temp\\" + WindGoes.DateHelper.ToStringYYYYMMDD(DateTime.Now).Substring(0, 8) + "\\";
				Directory.CreateDirectory(outfolder);
				string filename = StringHelper.StringHash(path) + ".tmp";
				string output = outfolder + filename;

				Process p = new Process();
				p.StartInfo.FileName = PdfDllFolder + "\\pdftotxt.exe";
				p.StartInfo.Arguments = input + "?" + output;
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.RedirectStandardInput = true;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.RedirectStandardError = true;
				p.StartInfo.CreateNoWindow = true;
				p.Start();

				p.StandardInput.WriteLine("exit");
				string outputstring = p.StandardOutput.ReadToEnd();
				p.WaitForExit();
				p.Close();

				Application.DoEvents();
				Thread.Sleep(500);
				string content = "";
				//编号是固定的，都是UTF8
				if (!string.IsNullOrEmpty(output))
				{
					using (StreamReader sr = new StreamReader(output, Encoding.UTF8))
					{
						content = sr.ReadToEnd();
					}
				}

				return content;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return null;
		}

	}
}
