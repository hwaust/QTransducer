using System;
using System.Collections.Generic;
using System.Text;

namespace QDAS
{
	/// <summary>
	/// 基础类，定义了一些操作方法。
	/// </summary>
	public class QBase
	{
		public static float SpecialFloatValue = float.MinValue + 1;
		public static double SpecialDoubleValue = double.MinValue + 1;
		public static int SpecialIntValue = int.MinValue + 1;

		public Dictionary<string, KItem> dic = new Dictionary<string, KItem>();


		//
		//public object Tag = null;
		/// <summary>
		/// 用于记录一些可能用到的信息。
		/// </summary>
		public object Tag { get; set; }

		static QBase()
		{
			KFields.InitKFields();
		}

		/// <summary>
		/// KItem k = new KItem();
		/// k["K1001"] = 20;
		/// object obj = k["K1001"]
		/// 
		/// k.setvalue("K1001", 20);
		/// obj = k.getvalue("K1001");
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public virtual object this[string key]
		{
			get
			{
				if (dic.ContainsKey(key))
				{
					return dic[key].Value;
				}
				return null;
			}
			set
			{
				if (dic.ContainsKey(key))
				{
					dic[key].Value = value;
				}
				else
				{
					KItem ki = KFields.GetItem(key);
					if (ki == null)
						ki = new KItem(key, "unkonwn", "not exist"); 
					ki.Value = value;
					dic.Add(key, ki); 
				}
			}
		}

		public object this[int key]
		{
			get
			{
				return this["K" + key.ToString("0000")];
			}
			set
			{
				this["K" + key.ToString("0000")] = value;
			}
		}


		public virtual void DealLine(QLineInfo line)
		{
			this[line.key] = line.value;
		}

		public virtual string GetKString()
		{
			return "";
		}

		public virtual void FromString(string s)
		{

		}

		/// <summary>
		/// 根据Key返回对应的字符串型键值。
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetString(string key)
		{
			return this[key] != null ? this[key].ToString() : null;
		}

		/// <summary>
		/// 根据Key返回对应的Double键值。
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public double GetDouble(string key)
		{
			return this[key] != null ? double.Parse(this[key].ToString()) : SpecialFloatValue;
		}


		public string DateString(DateTime date)
		{
			return string.Format("{0}.{1}.{2}/{3}:{4}:{5}",
				date.Day, date.Month, date.Year,
				date.Hour, date.Minute, date.Second);
		}

		public DateTime GetDate(string s)
		{
			//04.04.2012/12:29:11
			string[] data = s.Split('.', '/', ':');
			return new DateTime(
				int.Parse(data[2]), int.Parse(data[1]), int.Parse(data[0]),
				int.Parse(data[3]), int.Parse(data[4]), int.Parse(data[5]));
		}

		/// <summary>
		/// 获得当前对象的KString。如果id为0，则没有ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public string GetKString(int id = 0)
		{
			StringBuilder builder = new StringBuilder();
			string cid = id == 0 ? "" : "/" + id + " ";

			foreach (var item in dic.Values)
			{
				builder.AppendLine(item.KName + cid + item.Value);
			}

			return builder.ToString();
		}


		#region 废弃的代码

		public virtual string ConfuseString()
		{
			return "";
		}

		public int GetInt(string key)
		{
			object obj = this[key];
			if (obj != null)
			{
				try
				{
					return int.Parse(this[key].ToString());
				}
				catch
				{
					return SpecialIntValue;
				}
			}

			return SpecialIntValue;
		}

		public float GetFloat(string key)
		{
			object obj = this[key];
			if (obj != null)
			{
				try
				{
					return float.Parse(this[key].ToString());
				}
				catch
				{
					return SpecialFloatValue;
				}
			}
			return SpecialFloatValue;
		}
		#endregion

	}
}
