using QDAS;
using QTrans.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QTrans.Company.Y2017
{
	public class T01_ShangHai_EnYao : TransferBase
	{
		public override void SetConfig(ParamaterData pd)
		{
			CompanyName = "上海恩耀";
			VertionInfo = "1.7";
			pd.SupportAutoTransducer = true;
			pd.AddExt(".asc");

			//foreach(string s in Enum.GetNames(typeof(Encoding)))
			//	Console.WriteLine(s);
		}

		bool isSuccessful = true;
		public override bool TransferFile(string path)
		{
			isSuccessful = true;
			// load files from input. Multiple files could be stored in a single file.
			List<List<string>> files = getFiles(path);

			// process all the files. (Each list is a single file)
			for (int i = 0; i < files.Count; i++)
			{
				QFile qf = processFile(files[i], path);
				SaveDfq(qf, getOutputFileName(path, i));
			}

			return isSuccessful;
		}


		/// <summary>
		///  Gets files that are represented by lists of strings .
		/// </summary>
		/// <param name="filepath"> input file.</param>
		/// <returns></returns>
		List<List<string>> getFiles(string filepath)
		{
			List<List<string>> files = new List<List<string>>();
			List<string> list = new List<string>();
			foreach (string s in File.ReadAllLines(filepath, ParamaterData.Encodings[pd.EncodingID]))
				if (!string.IsNullOrEmpty(s))
					list.Add(s.Length < 66 ? s + new string(' ', 66 - s.Length) : s.Substring(0, 66));



			List<String> file = new List<string>();
			for (int i = 0; i < list.Count; i++)
			{
				file.Add(list[i]);
				// 如果连续2行以**开头，则是一个新文件的开始
				if (i + 2 < list.Count && !list[i].StartsWith("**") && list[i + 1].StartsWith("**") && list[i + 2].StartsWith("**"))
				{
					files.Add(file);
					file = new List<string>();
				}
			}

			if (file.Count > 0)
				files.Add(file);

			return files;
		}


		// outfolder\\originalName_00.dfq
		string getOutputFileName(string path, int id)
		{
			string outfolder = pd.OutputFolder;
			if (pd.OriginalLevelKept > 0)
			{
				string[] folders = Path.GetDirectoryName(path).Split(new char[] { ':', '\\' }, StringSplitOptions.RemoveEmptyEntries);
				string subfolder = "";
				for (int i = 0; i < pd.OriginalLevelKept; i++)
				{
					if (i >= folders.Length)
						break;
					subfolder = folders[folders.Length - 1 - i] + "\\" + subfolder;
				}
				outfolder = Path.Combine(pd.OutputFolder, subfolder);
				Directory.CreateDirectory(outfolder);
			}

			return outfolder + "\\" + Path.GetFileNameWithoutExtension(path) + "_" + id.ToString("00") + ".dfq";
		}



		private QFile processFile(List<string> file, string path)
		{
			QFile qf = new QFile();
			qf.Data.Add(new QData());
			int dataline = 9;

			qf[1001] = file[0].Split('-')[0].Trim().Substring(2);           // K1001-K1002-K1003: **DA42 - filter - 123456
			qf[1002] = file[0].Split('-')[1].Trim();                                // K1001-K1002-K1003: **DA42 - filter - 123456 
			qf[1003] = file[0].Split('-')[2].Trim();                            // K1001-K1002-K1003: **DA42 - filter - 123456  
			string k0061 = "";      // K0061: (1) **test reason: 首件(40000)
			string k0010 = "";       // K0010: (2) **machine number: E09(100000)
			string k0007 = "";        // K0007: (3) **fixture:1#
			string k0016 = "";     // K0016: (4) **casting:20161221 - 1
			string k0017 = "";     // K0017: (4) **casting: 20161221 - 1
			string k0053 = "";     // K0053: (5) **machining:20161223 - 1
			string k0008 = "";      // K0008: (5) **machining:20161223 - 1
			string k0014 = "";       // K0014: (6) **SN:1612123060
									 // string k0086 = "";      // K0086: (8) **machine:J1         
			string datestr = "";
			DateTime date = DateTime.Now;      // K0004: (7) **measure:2016 - 12 - 16 8:41:05

			// Estimate the length of head part.
			int end = 2;
			for (int i = 0; i < file.Count; i++)
			{
				if (i + 2 < file.Count && !file[i + 2].StartsWith("**"))
				{
					end = i + 1;
					Console.WriteLine("head.length = {0}", end);
					break;
				}
			}


			for (int i = 1; i < end; i++)
			{
				try
				{
					// 支持大小写，（，），：自动转换为斗角。
					string line = file[i].ToLower().Trim(' ', '*').Replace('（', '(').Replace('）', ')').Replace('：', ':');
					string key = file[i].Split(':')[0].Trim();
					string value = file[i].Split(':')[1].Trim();

					if (line.StartsWith("test reason"))
						k0061 = value;                          // K0061: (1) **test reason: 首件(40000)
					else if (line.StartsWith("machine number"))
						k0010 = value;                           // K0010: (2) **machine number: E09(100000)
					else if (line.StartsWith("fixture"))
						k0007 = value;                          // K0007: (3) **fixture:1#
					else if (line.StartsWith("casting"))
					{
						if (value.Contains('-'))
						{
							k0016 = value.Split('-')[0].Trim();         // K0016: (4) **casting:20161221 - 1
							k0017 = value.Split('-')[1].Trim();         // K0017: (4) **casting: 20161221 - 1
						}
					}
					else if (line.StartsWith("machining"))
					{
						if (value.Contains('-'))
						{
							k0053 = value.Split('-')[0].Trim();          // K0053: (5) **machining:20161223 - 1
							k0008 = value.Split('-')[1].Trim();           // K0008: (5) **machining:20161223 - 1
						}
					}
					else if (line.StartsWith("sn"))
						k0014 = value;                                  // K0014: (6) **SN:1612123060
					else if (line.StartsWith("measure"))
					{
						datestr = file[i].Split(new string[] { "measure" }, StringSplitOptions.RemoveEmptyEntries)[1];
						date = DateTime.Parse(datestr.Trim(':', ' '));                                 // K0004: (7) **measure:2016 - 12 - 16 8:41:05
					}
					else if (line.StartsWith("machine"))
						qf[1086] = value;                                  // K1086: (8) **SN:1612123060
					else
					{
						dataline = i - 1;
						break;
					}
				}
				catch (Exception ex)
				{
					isSuccessful = false;
					Console.WriteLine(ex.Message);
				}
			}

			// 只取括号里的内容。
			if (k0061.Contains('('))
				k0061 = k0061.Split('(')[1].Trim('(', ')');

			// 只取括号里的内容。
			if (k0010.Contains('('))
				k0010 = k0010.Split('(')[1].Trim('(', ')');

			// 取#前的内容。
			if (k0007.Contains('#'))
				k0007 = k0007.Split('#')[0];


			QDataItem di = new QDataItem();
			di.date = date;
			di[0014] = k0014;
			di[0016] = k0016;
			di[0017] = k0017;
			di.p5caoxue = k0007;
			di[0008] = k0008;
			di.p7machine = k0010;
			di[0053] = k0053;
			di[0061] = k0061;
			// di[0086] = k0086;

			// a file -> multiple modules
			List<List<string>> modules = new List<List<string>>();
			List<string> temp = new List<string>();
			for (int i = dataline; i < file.Count; i++)
			{
				if (file[i].StartsWith("**"))
				{
					if (temp.Count > 1)
						modules.Add(temp);
					temp = new List<string>();
				}
				temp.Add(file[i]);
			}
			if (temp.Count > 0)
			{
				modules.Add(temp);
				temp = new List<string>();
			}

			for (int i = 0; i < modules.Count; i++)
			{
				try
				{
					QFile partfile = processModule(modules[i], di);
					qf.Charactericstics.AddRange(partfile.Charactericstics);
					qf.Data[0].items.AddRange(partfile.Data[0].items);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error " + i + ": " + ex.Message);
				}
			}

			qf.ToDMode();

			return qf;
		}

		private QFile processModule(List<string> m, QDataItem di)
		{
			QFile qf = new QFile();
			qf.Data.Add(new QData());
			string name = getParamaterName(m[0]);
			Console.WriteLine("name = " + name);
			for (int i = 1; i < m.Count; i += 2)
			{
				//	 the returning array has two case:
				// case 1: it has a length of 5 in the format: k2002, k2101, k2113, data, irrelavent.
				// case 2: it has a length of 3 in the format: k2002, data, irrelavent
				string[] strs1 = getSections(m[i]);
				string[] strs2 = getSections(m[i + 1]);

				// 位置度。
				if (strs1[0].ToLower().Contains("position") || strs1[0].ToLower().Contains("concentr"))
				{
					QCharacteristic c1 = new QCharacteristic();
					QCharacteristic c2 = new QCharacteristic();
					QCharacteristic c3 = new QCharacteristic();

					c1.children.Add(c2);
					c1.children.Add(c3);

					qf.Charactericstics.Add(c1);
					qf.Charactericstics.Add(c2);
					qf.Charactericstics.Add(c3);

					// first characteristic
					c1[2001] = name;
					c1[2002] = strs1[0];
					c1[2101] = 0;
					c1[2112] = 0;
					c1[2113] = strs1[2];
					c1[2022] = 4;
					QDataItem qdi1 = di.Clone();
					// qdi1.SetValue(strs1[4]);
					set(qdi1, strs1[4]);

					c1.data.Add(qdi1);

					double dv = Math.Abs(double.Parse(strs1[2])) / 2;

					c2[2001] = name;
					c2[2002] = "1.1";
					c2[2101] = strs1[1];
					c2[2112] = -dv;
					c2[2113] = dv;
					c2[2022] = 4;
					QDataItem qdi2 = di.Clone();
					// qdi2.SetValue(strs1[3]);
					set(qdi2, strs1[3]);
					// qf.Data[0].items.Add(qdi2); 
					c2.data.Add(qdi2);

					c3[2001] = name;
					c3[2002] = "1.2";
					c3[2101] = strs2[1];
					c3[2112] = -dv;
					c3[2113] = dv;
					c3[2022] = 4;
					QDataItem qdi3 = di.Clone();
					// qdi3.SetValue(strs2[3]);
					set(qdi3, strs2[3]);

					c3.data.Add(qdi3);
				}
				// 不是位置度。
				else
				{
					QCharacteristic p = new QCharacteristic();
					p[2001] = name;
					p[2002] = strs1[0];
					p[2022] = 4;
					p[2101] = strs1[1];
					p[2113] = strs1[2];
					p[2112] = strs2[2];
					qf.Charactericstics.Add(p);
					QDataItem qdi = di.Clone();
					// qdi.SetValue(strs1[3]);
					set(qdi, strs1[3]);
					p.data.Add(qdi);
				}
			}

			adjustSameNames(qf);

			return qf;
		}

		private void set(QDataItem qdi, string v)
		{
			if (v.Contains(":"))
			{
				try
				{
					string[] parts = v.Split(':');
					qdi.value = double.Parse(parts[0]) + double.Parse(parts[1]) / 60 + double.Parse(parts[2]) / 3600;
					qdi.p1 = 0;
				}
				catch (Exception ex) { AddLog(CurrentFile, "set value: " + v + ". Error: " + ex.Message); }
			}
			else
				qdi.SetValue(v);
		}


		// 如果参数名称（K2002）相同，则在文件尾自动加_x， x为自加1编号。
		private void adjustSameNames(QFile qf)
		{
			int[] vs = new int[qf.Charactericstics.Count];

			for (int i = 0; i < qf.Charactericstics.Count - 1; i++)
			{
				string ni = qf.Charactericstics[i][2002].ToString();
				//表示已经标记过 或者是位置度不标记。 
				if (vs[i] > 0 || string.IsNullOrEmpty(ni) || ni.ToLower().Contains("position") || ni.ToLower().Contains("concentr") || ni == "1.1" || ni == "1.2")
					continue;

				int count = 1;
				for (int j = 0; j < qf.Charactericstics.Count; j++)
				{
					string nj = qf.Charactericstics[j][2002].ToString();
					if (ni == nj)
					{
						vs[j] = count;
						count++;
					}
				}

				if (count > 1)
					vs[i] = 1;
			}

			for (int i = 0; i < qf.Charactericstics.Count; i++)
			{
				if (vs[i] > 0)
				{
					string name = qf.Charactericstics[i][2002].ToString();
					qf.Charactericstics[i][2002] = name + "_" + vs[i];
				}

			}
		}


		/// <summary>
		/// Get data from each model
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		private string[] getSections(string v)
		{
			string[] d = new string[5];
			d[0] = v.Substring(10, 12).Trim();
			d[1] = v.Substring(22, 12).Trim();
			d[2] = v.Substring(34, 12).Trim();
			d[3] = v.Substring(46, 10).Trim();
			d[4] = v.Substring(60).Trim();

			for (int i = 1; i < d.Length; i++)
				d[i] = d[i].Length == 0 ? "0" : d[i];
			return d;
		}

		private string getParamaterName(String s)
		{
			return s.Substring(s.IndexOf("NO.") + 3).Trim();
		}


		private void processParamater(List<string> ls, QFile qf)
		{
			if (ls.Count == 0)
				return;

			if (ls.Count == 3)
			{
				string name = "";
				try
				{
					if (ls[0].Contains("编号"))
					{
						name = ls[0].Trim().Split(new char[] { '：', ':' })[1].Trim();
						//Console.WriteLine(name);
					}
					else if (ls[0].Contains("NO"))
					{
						name = ls[0].Trim().Split(new char[] { '.', ':', '：' })[1].Trim();
						Console.WriteLine(name);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}




		// for test
		private static void saveFiles(List<List<string>> files)
		{
			Console.WriteLine("file count = " + files.Count);
			Directory.CreateDirectory("D:\\data\\sanfeng\\");
			int t = 0;
			foreach (List<string> file in files)
			{
				StreamWriter sw = new StreamWriter("D:\\data\\sanfeng\\" + t.ToString("00") + ".txt");
				sw.WriteLine("-------------- new file -----------------");
				foreach (string item in file)
					sw.WriteLine(item);
				sw.WriteLine("\n\n\n\n");
				sw.Close();
				t++;
			}
		}

	}
}
