using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QDAS
{
	public class QFile : QBase
	{
		//用于保存所有参数信息
		public List<QCharacteristic> Charactericstics = new List<QCharacteristic>();

		//用于保存所有的行
		public List<QLineInfo> Lines = new List<QLineInfo>();

		//用于保存测量的数据
		public List<QData> Data = new List<QData>();

		/// <summary>
		/// 所有的零件，可以有多个零件，每个零件有和多个数据。
		/// </summary>
		public List<QPart> Parts = new List<QPart>();


		QFileMode dataMode = QFileMode.UnDefined;
		/// <summary>
		/// 文件的数据模式，默认为未指定。
		/// </summary>
		public QFileMode DataMode
		{
			get { return dataMode; }
			set { dataMode = value; }
		}


		public QFile()
		{
			Encoding = Encoding.Default;
		}

		public static void SetEncoding(string enc)
		{
			string cod = enc == null ? "" : enc.ToLower();


			switch (cod)
			{
				case "unicode":
					Encoding = Encoding.Unicode;
					break;

				case "utf8":
				case "utf-8":
					Encoding = Encoding.UTF8;
					break;


				case "utf7":
				case "utf-7":
					Encoding = Encoding.UTF7;
					break;

				case "ascii":
					Encoding = Encoding.ASCII;
					break;

				case "utf16":
				case "utf-16":
					Encoding = Encoding.BigEndianUnicode;
					break;

				default:
					Encoding = Encoding.Default;
					break;
			}
		}

		/// <summary>
		/// 用于从文件中加载DFQ文件。
		/// </summary>
		/// <param name="path">DFQ文件的完整路径。</param>
		/// <returns></returns>
		public static QFile LoadFile(string path)
		{
			QFile qf = new QFile();

			//参数列表
			List<QCharacteristic> ps = new List<QCharacteristic>();


			// 文件读取部分，完成了以下几件事：
			// 1、读取每行，生成每行的QLineInfo对象 
			// 2、先交给对应的参数处理，如果处理失败，再交给QFile处理
			// 3、如果不是QlineInfo对象，那么交给QFile的Data
			StreamReader sr = new StreamReader(path, Encoding.Default);

			while (!sr.EndOfStream)
			{
				try
				{
					//先读取一行进来。
					string s = sr.ReadLine();

					//如果为空或长度为0，则继续下一次循环。
					if (string.IsNullOrEmpty(s))
						continue;

					//去除前缀的空格，如果长度为0，则继续下一次循环。
					s = s.TrimStart();
					if (s.Length == 0)
						continue;

					//如果是K开头，那么就是定义，如果是‘/’那么就是注释，取消，否则就是数据。
					if (s[0] == 'K' || s[0] == 'k')
					{
						QLineInfo line = new QLineInfo(s);

						//如果是零配件信息，交由QF处理。"K1001 jet2013"
						if (s[1] == '1')
						{
							qf.DealLine(line);
						}
						//否则是参数信息，交由参数层处理
						else if (line.pid > 0)
						{
							if (line.pid > ps.Count)
							{
								for (int j = ps.Count; j < line.pid; j++)
								{
									QCharacteristic p = new QCharacteristic();
									p.id = j + 1;
									ps.Add(p);
								}
							}
							ps[line.pid - 1].DealLine(line);
						}
					}
					//如果是/，那么就是注释，忽略。
					else if (s[0] == '/')
					{

					}
					else
					{
						qf.Data.Add(new QData(s));
					}
				}
				catch { }
			}
			sr.Close();

			//取出生成的参数，添加至QFile中
			for (int i = 0; i < ps.Count; i++)
			{
				if (ps[i].id > 0)
				{
					qf.Charactericstics.Add(ps[i]);
				}
			}

			//此处将QFile中的Data中的DataItem与
			//每个QPramater的QDataItem数据组一一对应。
			for (int i = 0; i < qf.Data.Count; i++)
			{
				for (int j = 0; j < qf.Data[i].items.Count; j++)
				{
					qf.Charactericstics[j].data.Add(qf.Data[i].items[j]);
				}
			}

			return qf;
		}



		/// <summary>
		/// 从DFX中加载数据，如果必需数据格式与当前的DFD或DFQ匹配时才能正常加载，加载后为D模式。
		/// 返回值表示是否加载成功。
		/// </summary>
		/// <param name="path"></param>
		public static bool LoadFromDFX(QFile qf, string dfxpath)
		{
			if (qf == null || qf.Charactericstics.Count == 0)
				return false;

			StreamReader sr = new StreamReader(dfxpath, Encoding.Default);

			while (!sr.EndOfStream)
			{
				try
				{
					//先读取一行进来。
					string s = sr.ReadLine();

					//如果为空或长度为0，则继续下一次循环。
					if (string.IsNullOrEmpty(s))
						continue;

					//如果取得的数据个数与参数个数不同，则进入下一组数据。
					QData qd = new QData(s);
					if (qd.items.Count == qf.Charactericstics.Count)
						qf.Data.Add(qd);
				}
				catch
				{

				}
			}


			return true;
		}

		public static QFile LoadFromString(string content)
		{

			QFile qf = new QFile();

			//开始默认加载100个参数，方便下面的ps[line.pid - 1]的调用
			List<QCharacteristic> ps = new List<QCharacteristic>();
			for (int i = 0; i < 100; i++)
				ps.Add(new QCharacteristic());

			// 文件读取部分，完成了以下几件事：
			// 1、读取每行，生成每行的QLineInfo对象 
			// 2、先交给对应的参数处理，如果处理失败，再交给QFile处理
			// 3、如果不是QlineInfo对象，那么交给QFile的Data  

			string[] data = content.Split('\n');

			for (int i = 0; i < data.Length; i++)
			{
				string s = data[i];

				if (s == null || s.Length == 0)
				{
					continue;
				}

				if (s[s.Length - 1] == '\r')
				{
					s = s.Substring(0, s.Length - 1);
				}

				//如果是K开头，那么就是定义，如果是‘/’那么就是注释，取消，否则就是数据。
				if (s[0] == 'K' || s[0] == 'k')
				{
					QLineInfo line = new QLineInfo(s);

					//如果是零配件信息，交由QF处理。
					if (s[1] == '1')
					{
						qf.DealLine(line);
					}
					//否则是参数信息，交由参数层处理
					else if (line.pid > 0)
					{
						QCharacteristic p = ps[line.pid - 1];
						p.DealLine(line);
					}
				}
				//如果是/，那么就是注释，忽略。
				else if (s[0] == '/')
				{

				}
				else
				{
					qf.Data.Add(new QData(s));
				}
			}


			//取出生成的参数，添加至QFile中
			for (int i = 0; i < ps.Count; i++)
			{
				if (ps[i].id > 0)
				{
					qf.Charactericstics.Add(ps[i]);
				}
			}

			//此处将QFile中的Data中的DataItem与
			//每个QPramater的QDataItem数据组一一对应。
			for (int i = 0; i < qf.Data.Count; i++)
			{
				for (int j = 0; j < qf.Data[i].items.Count; j++)
				{
					qf.Charactericstics[j].data.Add(qf.Data[i].items[j]);
				}
			}

			return qf;
		}

		public static QFile LoadFromStrings(string[] data)
		{
			QFile qf = new QFile();

			//开始默认加载100个参数，方便下面的ps[line.pid - 1]的调用
			List<QCharacteristic> ps = new List<QCharacteristic>();
			for (int i = 0; i < 100; i++)
				ps.Add(new QCharacteristic());

			// 文件读取部分，完成了以下几件事：
			// 1、读取每行，生成每行的QLineInfo对象 
			// 2、先交给对应的参数处理，如果处理失败，再交给QFile处理
			// 3、如果不是QlineInfo对象，那么交给QFile的Data  


			for (int i = 0; i < data.Length; i++)
			{
				string s = data[i];

				if (s == null || s.Length == 0)
				{
					continue;
				}

				if (s[s.Length - 1] == '\r')
				{
					s = s.Substring(0, s.Length - 1);
				}

				//如果是K开头，那么就是定义，如果是‘/’那么就是注释，取消，否则就是数据。
				if (s[0] == 'K' || s[0] == 'k')
				{
					QLineInfo line = new QLineInfo(s);

					//如果是零配件信息，交由QF处理。
					if (s[1] == '1')
					{
						qf.DealLine(line);
					}
					//否则是参数信息，交由参数层处理
					else if (line.pid > 0)
					{
						QCharacteristic p = ps[line.pid - 1];
						p.DealLine(line);
					}
				}
				//如果是/，那么就是注释，忽略。
				else if (s[0] == '/')
				{

				}
				else
				{
					qf.Data.Add(new QData(s));
				}
			}


			//取出生成的参数，添加至QFile中
			for (int i = 0; i < ps.Count; i++)
			{
				if (ps[i].id > 0)
				{
					qf.Charactericstics.Add(ps[i]);
				}
			}

			//此处将QFile中的Data中的DataItem与
			//每个QPramater的QDataItem数据组一一对应。
			for (int i = 0; i < qf.Data.Count; i++)
			{
				for (int j = 0; j < qf.Data[i].items.Count; j++)
				{
					qf.Charactericstics[j].data.Add(qf.Data[i].items[j]);
				}
			}

			return qf;
		}


		public void Analyze()
		{
			//获得参数的个数。
			QLineInfo line = GetPram("K0100");

			//默认为-1，如果没读取值还是-1.
			int count = -1;
			if (line != null)
			{
				count = int.Parse(line.value);
				for (int j = 0; j < count; j++)
				{
					Charactericstics.Add(new QCharacteristic());
				}
			}

			//小于0的话就退出。
			if (count < 0) return;

			//对每一个参数进行初始化。
			for (int i = 0; i < Lines.Count; i++)
			{
				line = Lines[i];
				int id = line.pid;

				if (id > 0)
				{
					QCharacteristic p = Charactericstics[id - 1];
					p.DealLine(line);
				}
			}

			//参数进行调整
			for (int i = 0; i < Charactericstics.Count; i++)
			{
				//pramters[i].Adjust();
			}

			//对数据进行调整，移除第1个参数为256的项。
			for (int i = 0; i < Data.Count; i++)
			{
				QData qd = Data[i];
				for (int j = 0; j < qd.items.Count; j++)
				{
					if (!qd.items[j].addon[1].Equals("256"))
					{
						Charactericstics[j].data.Add(qd.items[j]);
					}
				}
			}
		}

		public QLineInfo GetPram(string key)
		{
			for (int i = 0; i < Lines.Count; i++)
			{
				if (Lines[i].key.Equals(key))
				{
					return Lines[i];
				}
			}
			return null;
		}

		public double GetXBar(int id)
		{
			double xb = 0;
			for (int i = 0; i < Data.Count; i++)
			{
				xb += Data[i].items[id].value;
			}

			return xb / Data.Count;
		}

		public static Encoding Encoding { get; set; }

		/// <summary>
		/// 将数据保存到指定的文件中。
		/// </summary>
		/// <param name="path">文件路径。</param>
		/// <returns></returns>
		public bool SaveToFile(string path)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(path, false, Encoding))
					sw.Write(GetKString());

				return true;
			}
			catch { }
			return false;
		}

		public void AdjustChID()
		{
			for (int i = 0; i < Charactericstics.Count; i++)
				Charactericstics[i].id = i + 1;
		}

		public string GetDFD()
		{
			AdjustChID();

			StringBuilder builder = new StringBuilder();

			/************************** 文件信息 ****************************/
			// 表示参数个数。
			builder.AppendLine("K0100 " + Charactericstics.Count);
			foreach (var item in dic.Values)
				builder.AppendLine(item.KName + " " + item.Value);
			builder.AppendLine();

			/************************** 参数信息 ****************************/
			for (int i = 0; i < Charactericstics.Count; i++)
				builder.AppendLine(Charactericstics[i].GetKString());

			return builder.ToString();
		}

		public string GetDFX()
		{
			StringBuilder builder = new StringBuilder();

			/************************** 数据信息 ****************************/
			for (int i = 0; i < Data.Count; i++)
				builder.AppendLine(Data[i].GetKString());

			return builder.ToString();
		}

		/// <summary>
		/// 输出K域字符串。
		/// </summary>
		/// <returns></returns>
		public override string GetKString()
		{
			AdjustChID();

			StringBuilder builder = new StringBuilder();

			/************************** 文件信息 ****************************/
			// 表示参数个数。
			builder.AppendLine("K0100 " + Charactericstics.Count);
			foreach (var item in dic.Values)
				builder.AppendLine(item.KName + " " + item.Value);
			builder.AppendLine();

			/************************** 参数信息 ****************************/
			for (int i = 0; i < Charactericstics.Count; i++)
				builder.AppendLine(Charactericstics[i].GetKString());


			/************************** 参数分组信息 ****************************/
			if (hasGrouping())
				getGroupingInfo(builder);

			/************************** 数据信息 ****************************/
			for (int i = 0; i < Data.Count; i++)
				builder.AppendLine(Data[i].GetKString());

			return builder.ToString();
		}

		public bool hasGrouping()
		{
			foreach (QCharacteristic qc in Charactericstics)
			{
				if (qc.children.Count > 0)
					return true;
			}
			return false;
		}



		//定义了2种模式，PMode和DMode
		//其中PMode是指数据保证在参数的数据列表中，
		//而DMode是指数据放置在数据列表中。
		/// <summary>
		/// PMode是指数据保证在参数的数据列表中
		/// </summary>
		public bool ToPMode()
		{
			// 如果已经是P模式了或者数据为空则返回。
			if (dataMode == QFileMode.PMode || Data.Count == 0)
				return true;


			try
			{
				//将data中的第一个QData分别给对应的参数赋值。
				for (int i = 0; i < Data.Count; i++)
				{
					for (int j = 0; j < Charactericstics.Count; j++)
					{
						QDataItem di = Data[i].items[j];
						// 只有p1为0时，才是数据，否则255或256表示无数据。
						if (di.p1 == 0)
							Charactericstics[j].data.Add(di);
					}
				}
				Data.Clear();
				dataMode = QFileMode.PMode;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				// 清空所有参数下的已转换数据。
				for (int j = 0; j < Charactericstics.Count; j++)
					Charactericstics[j].data.Clear();
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
			// 如果已经是D模式了或者数据为空则返回。
			if (dataMode == QFileMode.DMode || Charactericstics.Count == 0)
				return true;
			Data.Clear();

			//Step 1，取参数中数据最多的那个的参数个数保存在max中。
			int max = 0;
			for (int i = 0; i < Charactericstics.Count; i++)
				max = max >= Charactericstics[i].data.Count ? max : Charactericstics[i].data.Count;

			//Step 2，进行整理，把参数中带的数据，全放到数据QFile.Data中去。
			for (int i = 0; i < max; i++)
			{
				QData qd = new QData();
				//将参数中的数据复制到QFile的Data列表中。
				for (int j = 0; j < Charactericstics.Count; j++)
				{
					QDataItem di = new QDataItem();
					if (i >= Charactericstics[j].data.Count)
						di.p1 = 255;
					else
						di = Charactericstics[j].data[i];
					qd.items.Add(di);
				}
				Data.Add(qd);
			}

			//清除参数中的数据。
			for (int i = 0; i < Charactericstics.Count; i++)
				Charactericstics[i].data.Clear();


			dataMode = QFileMode.DMode;

			return true;
		}


		private void getGroupingInfo(StringBuilder sb)
		{
			sb.AppendLine("K5111/1 1");
			for (int i = 0; i < Charactericstics.Count; i++)
				sb.AppendLine(string.Format("K5112/{0} {1}", i + 2, Charactericstics[i].id)); //K5112/2 1

			sb.AppendLine();

			for (int i = 0; i < Charactericstics.Count; i++)
			{
				QCharacteristic qc = Charactericstics[i];

				if (qc.children.Count == 0)
					sb.AppendLine("K5102/1 " + qc.id);
				else
				{
					sb.AppendLine("K5103/1 " + (qc.id + 1));
					foreach (QCharacteristic qch in qc.children)
					{
						sb.AppendLine(string.Format("K5102/{0} {1}", qc.id + 1, qch.id));
					}
					i += qc.children.Count;
				}
			}
		}

		public string GetGroupingInfo()
		{
			AdjustChID();
			StringBuilder sb = new StringBuilder();
			getGroupingInfo(sb);
			return sb.ToString();
		}

	}
}
