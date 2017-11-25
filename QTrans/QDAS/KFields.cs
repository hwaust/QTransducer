using System;
using System.Collections.Generic;
using System.Text;

namespace QDAS
{
	public  class KFields
	{
		static Dictionary<string, KItem> dic = new Dictionary<string, KItem>();

		public  static void  InitKFields()
		{ 
			List<KItem> items = new List<KItem>();

			//数据类型表示
			//double,dou; string,str; int, int;datetime, dat;
			//没有float，全使用double。

			//数据层信息
                    //case "K0001": return value;
                    //case "K0002": return p1;
                    //case "K0004": return date;
                    //case "K0005": return p3event;
                    //case "K0006": return p4pici;
                    //case "K0007": return p5caoxue;
                    //case "K0008": return p6operator;
                    //case "K0010": return p7machine;
                    //case "K0011": return p8process;
                    //case "K0012": return p9tool;
			items.Add(new KItem("K0009", "str", "文本")); 
			items.Add(new KItem("K0013", "str", "附加信息"));
			items.Add(new KItem("K0014", "str", "附加信息")); 
			items.Add(new KItem("K0015", "str", "附加信息"));
			items.Add(new KItem("K0016", "str", "附加信息"));
			items.Add(new KItem("K0017", "str", "附加信息"));
			items.Add(new KItem("K0018", "str", "附加信息")); 
			items.Add(new KItem("K0053", "str", "订单"));
			items.Add(new KItem("K0097", "dou", "附加信息"));
			items.Add(new KItem("K0100", "int", "参数个数"));
             
			//零件层信息
			items.Add(new KItem("K1001", "str", "零件型号（或编号）"));
			items.Add(new KItem("K1002", "str", "零件名称（或描述）"));
			items.Add(new KItem("K1003", "str", "零件简单描述"));
			items.Add(new KItem("K1005", "str", "特殊名称spname"));
			items.Add(new KItem("K1012", "str", "Unknown"));
			items.Add(new KItem("K1014", "str", "PartID"));

			items.Add(new KItem("K1022", "str", "文件类型，如：Q-DAS"));
			items.Add(new KItem("K1052", "str", "分析公司，如：Analysis Inc."));
			items.Add(new KItem("K1053", "str", "OP"));
			items.Add(new KItem("K1062", "str", "备注信息一，如：Assembly Works"));
			items.Add(new KItem("K1082", "str", "备注信息二，如：Machining & Assembly Cell"));

			items.Add(new KItem("K1100", "str", "Line"));
            items.Add(new KItem("K1102", "str", "Unknown"));

			items.Add(new KItem("K1202", "str", "测量地点，如：Measurement Laboratory"));
			items.Add(new KItem("K1203", "str", "测量方式，如：Machine Acceptance"));
			items.Add(new KItem("K1204", "str", "时间一"));
			items.Add(new KItem("K1205", "str", "时间二"));
 
			//参数层信息
			items.Add(new KItem("K2001", "str", "参数编号"));
			items.Add(new KItem("K2002", "str", "参数名称"));

			items.Add(new KItem("K2101", "dou", "名义值"));

			items.Add(new KItem("K2110", "dou", "下界限"));
			items.Add(new KItem("K2111", "dou", "上界限"));
			items.Add(new KItem("K2112", "dou", "公差下限"));
			items.Add(new KItem("K2113", "dou", "公差上限"));

			items.Add(new KItem("K2130", "dou", "下可信区间"));
			items.Add(new KItem("K2131", "dou", "上可信区间"));

			items.Add(new KItem("K2142", "str", "参数单位"));
			items.Add(new KItem("K2022", "int", "小数点长度"));
			items.Add(new KItem("K2024", "str", "测试压力"));
			items.Add(new KItem("K2025", "str", "充气时间"));
			items.Add(new KItem("K2026", "str", "平衡时间"));
			items.Add(new KItem("K2027", "str", "测量时间"));


            items.Add(new KItem("K8500", "int", "Group ID"));
            items.Add(new KItem("K8013", "dou", "上控制限"));
            items.Add(new KItem("K8012", "dou", "下控件限"));
			items.Add(new KItem("K8015", "dou", "上警戒限"));
			items.Add(new KItem("K8014", "dou", "下警戒限"));

			for (int i = 0; i < items.Count; i++)
				dic.Add(items[i].KName, items[i]); 
		}

		/// <summary>
		/// 根据key查询字典中的K域信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static KItem GetItem(string key)
		{
			if (dic.ContainsKey(key))
			{
				return dic[key].Clone();
			}
			return null;
		}
	}

	public enum QType:int
	{
		String,
		Double,
		Int,
		DateTime,
		Time,
		Date
	}
}
