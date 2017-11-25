using System;
using System.Collections.Generic;
using System.Text;

namespace QDAS
{
	public class QPart : QBase
	{
		public int partid = 0;

		//用于保存所有参数信息
		public List<QCharacteristic> pramters = new List<QCharacteristic>();


		public override string GetKString()
		{
			StringBuilder builder = new StringBuilder();

			foreach (var item in dic.Values)
			{
				string mid = partid > 0 ? "/" + partid + " " : " ";
				builder.AppendLine(item.KName + mid + item.Value);
			}
			builder.AppendLine();

			for (int i = 0; i < pramters.Count; i++)
			{
				builder.AppendLine(pramters[i].GetKString());
			} 

			return builder.ToString();
		}
	}
}
