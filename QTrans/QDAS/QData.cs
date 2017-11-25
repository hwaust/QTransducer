using System;
using System.Collections.Generic;
using System.Text;

namespace QDAS
{
	public class QData : QBase
	{
		public char splitter = (char)15;
		public List<QDataItem> items = new List<QDataItem>();

		public QData() { }
		public QData(string s)
		{
			FromString(s);
		}

		public override void FromString(string s)
		{
			string[] dt = s.Split(splitter);
			for (int i = 0; i < dt.Length; i++)
			{
				QDataItem qdi = new QDataItem(dt[i]);
				if (qdi.Available)
					items.Add(qdi);
			}
		}

		public override string GetKString()
		{
			StringBuilder builder = new StringBuilder();

			for (int i = 0; i < items.Count; i++)
			{
				builder.Append(items[i].GetKString() + splitter);
			}

			//去除最后多加的那个分隔符。
			if (builder.Length > 0)
			{
				builder.Remove(builder.Length - 1, 1);
				builder.AppendLine();
			}


			//特殊K值的输出，如K0014
			for (int i = 0; i < items.Count; i++)
			{
				builder.Append(items[i].GetKString(i + 1));
			}

			return builder.ToString();
		}
	}
}
