using System;
using System.Collections.Generic;
using System.Text;

namespace QDAS
{
	public class QCharacteristic : QBase
	{
		/// <summary>
		/// 文件中的编号，如 K2001/1 k10，其中id = 1。
		/// </summary>
		public int id = 0;

		/// <summary>
		/// 指零件层的编号，如果零件层是多个的话，那么此ID>0。
		/// </summary>
		public int partId = 0;

		public List<QCharacteristic> children = new List<QCharacteristic>();

		//所有的数据
		public List<QDataItem> data = new List<QDataItem>();

		/// <summary>
		/// 是否是定性参数。
		/// </summary>
		public bool IsDetermistic = false;


		public QCharacteristic() { }

		/// <summary>
		/// 设置id和2001为index. （id 为/1 这样的内容）
		/// </summary>
		/// <param name="index"></param>
		public QCharacteristic(int index)
		{
			id = index;
			this[2001] = id;
		}

		public override string GetKString()
		{
			StringBuilder b = new StringBuilder();
			string cid = "/" + id + " ";

			foreach (var item in dic)
			{
				b.AppendLine(item.Key + cid + item.Value.Value);
			}

			return b.ToString();
		}




		/// <summary>
		/// 获得参数编号，如果不存在返回null。
		/// </summary>
		/// <returns></returns>
		public string GetID()
		{
			return GetString("K2001");
		}

		public string GetName()
		{
			return GetString("K2002");
		}

		public bool HasValue()
		{
			if (data.Count == 0)
				return false;

			for (int i = 0; i < data.Count; i++)
			{
				if (data[i].p1 != 255 || data[i].p1 == 256)
					return true;
			}
			return false;
		}
	}
}
