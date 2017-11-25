using System;
using System.Collections.Generic;
using System.Text;

namespace QDAS
{
	public class QLineInfo: QBase
	{
		// 一条指令的由三部分组成：K值，组别和参数，如：
		// K1001/1 Test001
		/// <summary>
		/// K值，如K1001
		/// </summary>
		public string key;
		/// <summary>
		/// K值后面的编号，如K1001/1 Test001 后面的/1的值 1
		/// </summary>
		public int pid = 0;
		/// <summary>
		/// 最后的值，如K1001/1 Test001 后面的 Test001
		/// </summary>
		public string value;


		public QLineInfo(string s)
		{
			FromString(s);
		} 
	
		public QLineInfo(string k, int id, string p)
		{
			key = k;
			pid = id;
			value = p;
		}

		public QLineInfo(int k, int id, string p)
		{
			key = k.ToString();
			pid = id;
			value = p;
		}


		public override void FromString(string s)
		{
			int p = s.IndexOf(' ');
			string s1 = s.Substring(0, p);
			value = s.Substring(p + 1);
			p = s1.IndexOf('/');
			//如果p > 0说明是有分组编号的，否则没有。
			if (p > 0)
			{
				key = s1.Substring(0, p);
				pid = int.Parse(s1.Substring(p + 1));
			}
			else
			{
				key = s1;
				pid = 0;
			}
		}

		public override string GetKString()
		{
			if (pid > 0)
			{
				return "K" + key + "\\" + pid + " " + value;
			}

			return "K" + key + " " + value;
		}
	}
}
