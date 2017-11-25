using System;
using System.Collections.Generic;
using System.Text;

namespace QDAS
{
	public class QDataItem : QBase
	{
		public double value = 0;
		public int p1 = 0; //显示是0，不显示是255
		public DateTime date = DateTime.Now;
		public string p3event = ""; //不写

		string pici = "";
		public string p4pici
		{
			get { return pici; }
			set
			{
				pici = !string.IsNullOrEmpty(value) ? value.Trim() : "";
				if (pici.Length > 0 && pici[0] != '#')
					pici = "#" + pici;
			}
		}
		public string p5caoxue = "";
		public string p6operator = "";//
		public string p7machine = "";//
		public string p8process = "";
		public string p9tool = "";//
		public string p10 = "";
		public string p11 = "";

		//04.04.2012/12:29:11
		//分隔符
		char spliter = (char)20;
		//临时数据，分于保存分隔符
		public string[] addon = null;

		/// <summary>
		///  是否成功从字符串转换。
		/// </summary>
		public bool Available = false;

		/// <summary>
		/// 是否是定性变量。
		/// </summary>
		public bool IsDeterministic = false;

		public QDataItem() { }
		public QDataItem(string s)
		{
			FromString(s);
		}

		public override void FromString(string s)
		{
			try
			{
				if (String.IsNullOrEmpty(s))
				{
					Available = false;
					return;
				}

				addon = s.Split(spliter);
				value = double.Parse(addon[0]);
				p1 = int.Parse(addon[1]);

				if (p1 != 0)
				{
					Available = true;
					return;
				}

				date = GetDate(addon[2]);
				p3event = addon[3];
				p4pici = addon[4];
				p5caoxue = addon[5];

				if (addon.Length > 6)
					p6operator = addon[6];

				if (addon.Length > 7)
					p7machine = addon[7];

				if (addon.Length > 8)
					p8process = addon[8];

				if (addon.Length > 9)
					p9tool = addon[9];

				//p10 = addon[10];
				//p11 = addon[11];
				Available = true;
			}
			catch
			{
				Console.WriteLine("dd2");
				Available = false;
			}

		}

		/// <summary>
		/// 设置值，当设置失败后，p1变为256.
		/// </summary>
		/// <param name="v"></param>
		public void SetValue(object v)
		{ 
			p1 = 255;
			if (v == null)
				return;

			string s = v.ToString().Trim();

			if (s.Length == 0)
				return;

			try
			{
				value = double.Parse(s);
				p1 = 0;
			}
			catch (Exception ex) { Console.WriteLine("QDataItem.SetValue: " + ex.Message + "\nInput string: " + s); }
		}

		public string GetKStringSingleLine(int p)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine("K0001/" + p + " " + value.ToString("0.00000000000000E0000"));
			sb.AppendLine("K0002/" + p + " " + p1);
			sb.AppendLine("K0004/" + p + " " + DateString(date));
			sb.AppendLine("K0005/" + p + " " + p3event);
			sb.AppendLine("K0006/" + p + " " + p4pici);
			sb.AppendLine("K0007/" + p + " " + p5caoxue);
			sb.AppendLine("K0008/" + p + " " + p6operator);
			sb.AppendLine("K0010/" + p + " " + p7machine);
			sb.AppendLine("K0011/" + p + " " + p8process);
			sb.AppendLine("K0012/" + p + " " + p9tool);


			//特殊K值的输出，如K0014
			string cid = p == 0 ? "" : "/" + p + " ";

			foreach (var item in dic.Values)
			{
				sb.AppendLine(item.KName + cid + item.Value);
			}

			return sb.ToString();
		}



		public override string GetKString()
		{
			string[] data = new string[10];

			if (IsDeterministic)
			{
				data[0] = string.Format("1000{1}{0}{1}{0}{1}0{1}", (value == 0 ? "0" : "1"), spliter);
			}
			else
			{
				data[0] = value.ToString("0.00000000000000E0000");
			}


			data[1] = p1.ToString();
			//不可用状态时，只返回第一个数值和后面的属性255、256
			if (p1 == 256 || p1 == 255)
				return data[0] + spliter + data[1];

			data[2] = DateString(date);
			data[3] = p3event;
			data[4] = p4pici;
			data[5] = p5caoxue;
			data[6] = p6operator;
			data[7] = p7machine;
			data[8] = p8process;
			data[9] = p9tool;

			return string.Join(spliter.ToString(), data);
		}


		/// <summary>
		/// 键值，通过K值来访问相应的数据，访问时，需要加前面的K。
		/// </summary>
		/// <param name="key">键值，如K0001</param>
		/// <returns></returns>
		public override object this[string key]
		{
			get
			{
				switch (key)
				{
					case "K0001": return value;
					case "K0002": return p1;
					case "K0004": return date;
					case "K0005": return p3event;
					case "K0006": return p4pici;
					case "K0007": return p5caoxue;
					case "K0008": return p6operator;
					case "K0010": return p7machine;
					case "K0011": return p8process;
					case "K0012": return p9tool;
					default:
						return base[key];
				}
			}
			set
			{
				string v = value == null ? "" : value.ToString().Trim();
				switch (key)
				{
					case "K0001": SetValue(v); break;
					case "K0002": this.p1 = v == "0" ? 0 : 255; break;
					case "K0004": this.date = DateTime.Parse(v); break;
					case "K0005": p3event = v; break;
					case "K0006": p4pici = v[0] == '#' ? v : "#" + v; break;
					case "K0007": p5caoxue = v; break;
					case "K0008": p6operator = v; break;
					case "K0010": p7machine = v; break;
					case "K0011": p8process = v; break;
					case "K0012": p9tool = v; break;
					default: base[key] = value; break;
				}
			}
		}

		public QDataItem Clone()
		{
			QDataItem di = new QDataItem();
			di.date = this.date;
			di.addon = this.addon;
			di.p1 = this.p1;
			di.p3event = this.p3event;
			di.p4pici = this.p4pici;
			di.p5caoxue = this.p5caoxue;
			di.p6operator = this.p6operator;
			di.p7machine = this.p7machine;
			di.p8process = this.p8process;
			di.p9tool = this.p9tool;
			di.p10 = this.p10;
			di.p11 = this.p11;

			foreach (KItem ki in dic.Values)
			{
				di.dic.Add(ki.KName, ki);
			}

			return di;
		}
	}
}
