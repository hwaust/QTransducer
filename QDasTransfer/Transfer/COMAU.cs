using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QDAS;
using System.IO;

namespace QDasTransfer.Transfer
{
	public class COMAU:TransferBase
	{
		string[] names = { "零件类型", "零件描述", "PartID", "Line", "OP", "测量参数", "测试压力", "充气时间", "平衡时间", "测量时间", "公差上限", "公差下限" };
		string[] keys = { "K1001", "K1002", "K1014", "K1100", "K1053", "K2002", "K2024", "K2025", "K2026", "K2007", "K2113", "K2112" };
		 
		List<string> dataFiles = new List<string>();
		List<string> defFiles = new List<string>();
		List<QFile> qfs = new List<QFile>();

		//输入目录的最后一级目录名称，如C:\abc\ddb 那么此名称为ddb
		string folderpath = "";

		//输入路径
		string inputPath = "";

		public COMAU()
		{
			ShowFileOption = false;
			CompanyName = "COMAU试漏仪";
			Exts.Add(".txt"); 
		}

		public override QDAS.QFile Transfer(string path)
		{
			try
			{
				//读取txt文件列表
				if (!CheckFiles(path))
					return null;

				if (!LoadDefines())
					return null;

				bool r = false;
				for (int i = 0; i < dataFiles.Count; i++)
				{
					if (LoadData(dataFiles[i]))
						r = true;
				}

				if (!r)
				{
					Error = "数据文件未能加载成功。";
					return null;
				}

			}
			catch (Exception e1)
			{
				Error = e1.Message;
				return null;
			}
			 
			return null;
		}


		public override void DealFolder(string path)
		{
			inputPath = path;
			folderpath = GetDirectoryLastName(path);

			try
			{
				dealFolder(path);
				string[] subfolds = Directory.GetDirectories(path);
				for (int i = 0; i < subfolds.Length; i++)
				{
					dealFolder(subfolds[i]);
				}
			}
			catch (Exception e1)
			{
				AddFailedFile(path, e1.Message);
			}
			
		}

		public string GetDirectoryLastName(string path)
		{
			string[] sec = path.Split('\\');
			return sec[sec.Length - 1];
		}

		private void dealFolder(string path)
		{
			//清除日志和QFile列表。 
			qfs.Clear();


			//检测文件，对定义文件和数据文件进行筛选。
			CheckFiles(path);

			//读取定义文件，同时会在qfs中添加QFile
			LoadDefines();

			//读取数据文件。
			for (int i = 0; i < dataFiles.Count; i++)
			{
				LoadData(dataFiles[i]);
			}

			if (qfs.Count == 0) 
				return; 


			string foldname = GetDirectoryLastName(path); 

			//将qfs所有对象进行输出。
			for (int i = 0; i < qfs.Count; i++)
			{
				string name = (string)qfs[i].Tag;
				string p = inputPath == path ? OutputFolder : OutputFolder + "\\" + folderpath ;
				p += "\\" + foldname + "\\"; 
				Directory.CreateDirectory(p); 
				SaveDFQ(qfs[i], p + name + ".DFQ");

			}
		}

		/// <summary>
		/// 对指定路径进行检测，提取出定义文件和数据文件。
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		private bool CheckFiles(string path)
		{
			try
			{
				//清空数据
				defFiles.Clear();
				dataFiles.Clear();

				//载入路径所有文件
				string[] fls = Directory.GetFiles(path);

				//根据全名分辨是数据文件还是定义文件，并分别保存至defFile和files
				for (int i = 0; i < fls.Length; i++)
				{
					string filename = Path.GetFileNameWithoutExtension(fls[i]);
					string ext = Path.GetExtension(fls[i]);

					if (ext.ToLower().Equals(".txt"))
					{
						if (filename.Substring(0, 2) == "20")
						{
							dataFiles.Add(fls[i]);
						}
						else if (filename.Split('_').Length > 2)
						{
							defFiles.Add(fls[i]);
						}
					}
				}

				if (defFiles.Count == 0 && dataFiles.Count == 0)
				{
					return true;
				}

				//没有定义文件
				if (defFiles.Count == 0)
				{
					LogList.Add("缺少定义文件，请检查后再进行转换。");
					return false;
				}

				//没有数据文件
				if (dataFiles.Count == 0)
				{
					LogList.Add("缺少数据文件，请检查后再进行转换。");
					return false;
				}

			}
			catch (Exception e1)
			{
				LogList.Add(e1.Message);
				return false;
			}
			return true;
		}

		private bool LoadDefines()
		{
			for (int i = 0; i < defFiles.Count; i++)
			{
				LoadDefine(defFiles[i]);
			}
			
			return true;
		}


		private void LoadDefine(string path)
		{
			try
			{
				string content = "";
				using (StreamReader sr = new StreamReader(path, Encoding.Default))
				{
					content = sr.ReadToEnd();
				}

				QFile qf = new QFile();
				QParamter p = new QParamter();

				string[] data = content.Split(';');
				foreach (var item in data)
				{
					string[] dt = item.Split(',');
					for (int i = 0; i < names.Length; i++)
					{
						if (names[i] == dt[0])
						{
							p[keys[i]] = dt[1];
							break;
						}
					}
				}

				p.id = 1;
				qf.pramters.Add(p);
				qf.Tag = Path.GetFileNameWithoutExtension(path);

				qfs.Add(qf);
			}
			catch (Exception d1)
			{
				AddLog(path, d1.Message);
			}
		}

		private bool LoadData(string p)
		{
			try
			{
				string content = "";
				using (StreamReader sr = new StreamReader(p, Encoding.Default))
				{
					content = sr.ReadToEnd();
				}

				string[] lines = content.Split(new string[] { "\r\n"}, StringSplitOptions.RemoveEmptyEntries);

				for (int i = 0; i < lines.Length; i++)
				{
					try
					{
						//将第i行数据分成三分，分别为时间，数据和头文件。
						string[] sectors = lines[i].Split(new char[] { ',', '\r' }, StringSplitOptions.RemoveEmptyEntries);
						
						//将第二段数据拆分，拆分后，倒数第二段为数据。
						string[] pts = sectors[1].Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

						//保存数据。
						QData data = new QData();
						QDataItem di = new QDataItem();
						di.date = DateTime.Parse(sectors[0]);
						di.value = double.Parse(pts[pts.Length - 2]);
						data.items.Add(di);

						//根据第三段寻找头文件，如果找到则退出。
						bool added = false;
						for (int j = 0; j < qfs.Count; j++)
						{
							string fn = (string)qfs[j].Tag; 
							if (fn.ToLower().Contains(sectors[2].ToLower()))
							{
								qfs[j].data.Add(data);
								added = true;
								break;
							}
						}

						if (!added)
						{
							AddLog(p, string.Format("在转换 {0} 第 {1} 行数据时，未找到定义文件，数据发生丢失，数据为 {2}",
								p, i, lines[i]));
						}
					}
					catch (Exception e1)
					{
						string err = string.Format("在转换 {0} 第 {1} 行数据时发生错误，原因：", p, i);
						AddFailedFile(p, err +e1.Message);
						return false;
					}
				}

			}
			catch (Exception e1)
			{
				AddFailedFile(p, e1.Message);
				return false;
			}
			return true;
		}
	}
}
