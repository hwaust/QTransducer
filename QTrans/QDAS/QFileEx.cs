using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QDAS
{
	public class QFileEx : QBase
	{
		//用于保存所有参数信息
		public List<QCharacteristic> pramters = new List<QCharacteristic>();

		//用于保存所有的行
		public List<QLineInfo> lines = new List<QLineInfo>();

		//用于保存测量的数据
		public List<QData> data = new List<QData>();

		/// <summary>
		/// 所有的零件，可以有多个零件，每个零件有和多个数据。
		/// </summary>
		public List<QPart> parts = new List<QPart>();



		QFileMode dataMode = QFileMode.UnDefined;
		/// <summary>
		/// 文件的数据模式，默认为未指定。
		/// </summary>
		public QFileMode DataMode
		{
			get { return dataMode; }
			set { dataMode = value; }
		}

		/// <summary>
		/// 从文本文件中读取到所有数据的数组，每一行为数组中的第一个元素。去除空行和左侧的空格。
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public List<string> GetLines(string path)
		{
			List<string> data = new List<string>();
			try
			{
				StreamReader sr = new StreamReader(path, Encoding.Default);
				string s = null;
				while (!sr.EndOfStream)
				{
					s = sr.ReadLine();
					if (!string.IsNullOrEmpty(s))
					{
						s = s.TrimStart();
						if (s.Length > 0)
							data.Add(s.TrimStart());
					}
				}
				sr.Close();
			}
			catch { }
			return data;
		}

		public bool LoadFile(string path)
		{
			//参数列表
			List<QCharacteristic> ps = new List<QCharacteristic>();

			// 文件读取部分，完成了以下几件事：
			// 1、读取每行，生成每行的QLineInfo对象 
			// 2、先交给对应的参数处理，如果处理失败，再交给QFile处理
			// 3、如果不是QlineInfo对象，那么交给QFile的Data 
			List<string> strs = GetLines(path);


			int partid = 0;
			QPart part = new QPart();

			foreach (string str in strs)
			{
				try
				{
					//如果是K开头，那么就是定义，如果是‘/’那么就是注释，取消，否则就是数据。
					if (str[0] == 'K' || str[0] == 'k')
					{
						QLineInfo line = new QLineInfo(str);

						//如果是零配件信息，交由QF处理。"K1001 jet2013"
						if (str[1] == '1')
						{
							partid = line.pid > 0 ? line.pid - 1 : 0;
							if (partid + 1 > parts.Count)
							{
								QPart pt = new QPart();
								pt.partid = line.pid;
								parts.Add(pt); 
							}
							parts[partid].DealLine(line);
						}
						//否则是参数信息，交由参数层处理
						else if (str[1] == '2')
						{
							if (line.pid > ps.Count)
							{
								for (int j = ps.Count; j < line.pid; j++)
								{
									QCharacteristic p = new QCharacteristic();
									p.partId = partid + 1;
									p.id = j + 1;
									ps.Add(p);
									parts[partid].pramters.Add(p);
								}
							}
							ps[line.pid - 1].DealLine(line);
						}
					}
					//如果是/，那么就是注释，忽略。
					else if (str[0] == '/')
					{

					}
					else
					{
						this.data.Add(new QData(str));
					}
				}
				catch { }
			}

			//取出生成的参数，添加至QFile中
			for (int i = 0; i < ps.Count; i++)
			{
				if (ps[i].id > 0)
				{
					this.pramters.Add(ps[i]);
				}
			}

			//此处将QFile中的Data中的DataItem与
			//每个QPramater的QDataItem数据组一一对应。
			for (int i = 0; i < this.data.Count; i++)
			{
				for (int j = 0; j < this.data[i].items.Count; j++)
				{
					this.pramters[j].data.Add(this.data[i].items[j]);
				}
			}


			return true;
		}





		/// <summary>
		/// 将数据保存到指定的文件中。
		/// </summary>
		/// <param name="path">文件路径。</param>
		/// <returns></returns>
		public bool SaveToFile(string path)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
				{
					sw.Write(GetKString());
				}

				return true;
			}
			catch { }
			return false;
		}


		/// <summary>
		/// 输出K域字符串。
		/// </summary>
		/// <returns></returns>
		public override string GetKString()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLine("K0100 " + pramters.Count);

			foreach (QPart pt in parts)
			{
				builder.AppendLine(pt.GetKString());
			}
			builder.AppendLine();

			for (int i = 0; i < data.Count; i++)
			{
				builder.AppendLine(data[i].GetKString());
			}

			return builder.ToString();
		}


		//定义了2种模式，PMode和DMode
		//其中PMode是指数据保证在参数的数据列表中，
		//而DMode是指数据放置在数据列表中。
		/// <summary>
		/// PMode是指数据保证在参数的数据列表中
		/// </summary>
		public bool ToPMode()
		{
			if (dataMode == QFileMode.PMode || data.Count == 0)
			{
				return true;
			}

			int count = 0;
			try
			{
				//将data中的第一个QData分别给对应的参数赋值。
				for (int i = 0; i < data.Count; i++)
				{
					for (int j = 0; j < pramters.Count; j++)
					{
						QCharacteristic p = pramters[j];
						QDataItem di = data[i].items[j];
						if (di.p1 == 0)
						{
							p.data.Add(di);
							count++;
						}

					}
				}

				data.Clear();
				dataMode = QFileMode.PMode;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}


			return true;
		}


		/// <summary>
		/// 将数据转换为DMode，所有数据保存在QFile中的data列表中。
		/// </summary>
		/// <returns></returns>
		public bool ToDMode()
		{
			if (dataMode == QFileMode.DMode || pramters.Count == 0)
			{
				return true;
			}
			int count = 0;
			try
			{
				//第一步，取参数中数据最多的那个。
				int max = 0;
				for (int i = 0; i < pramters.Count; i++)
				{
					if (max <= pramters[i].data.Count)
					{
						max = pramters[i].data.Count;
					}
				}

				//第二步，进行整理，把参数中带的数据，全放到数据QFile.Data中去。
				for (int i = 0; i < max; i++)
				{
					QData qd = new QData();
					for (int j = 0; j < pramters.Count; j++)
					{
						QDataItem di = new QDataItem();
						if (i >= pramters[j].data.Count)
						{
							di.p1 = 256;
						}
						else
						{
							di = pramters[j].data[i];
							count++;
						}
						qd.items.Add(di);
					}
					data.Add(qd);
				}

				//清除参数中的数据。
				for (int i = 0; i < pramters.Count; i++)
				{
					pramters[i].data.Clear();
				}

				dataMode = QFileMode.DMode;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
			//Console.WriteLine(count);
			return true;
		}
	}
}
