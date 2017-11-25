using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QTrans.Classes
{
	public class Session
	{
		//使用字典来保存数据。
		Dictionary<string, string> dic = new Dictionary<string, string>();

		/// <summary>
		/// 读取或设置Session的值。
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string this[string key]
		{
			get
			{
				return dic.ContainsKey(key) ? dic[key] : null;
			}
			set
			{
				if (dic.ContainsKey(key))
				{
					dic[key] = value;
				}
				else
				{
					dic.Add(key, value);
				}
			}
		}

		public List<string> GetKeys()
		{
			List<string> list = new List<string>();

			foreach (string key in dic.Keys)
			{
				list.Add(key);
			}

			return list;
		}

		public int Count
		{
			get { return dic.Keys.Count; }
		}

		public string GetValue(string key)
		{
			return dic.Keys.Contains(key) ? dic[key] : "";
		}

		public void SetValue(string key, string value)
		{
			if (dic.Keys.Contains(key))
				dic[key] = value;
			else
				dic.Add(key, value);
		}

		public void Save(string path)
		{
			using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
			{
				foreach (string key in dic.Keys)
				{
					sw.WriteLine(key + "=" + dic[key]);
				}
			}
		}

		public void Load(string path)
		{
			try
			{
				string[] lines = File.ReadAllLines(path, Encoding.Default);
				dic = new Dictionary<string, string>();

				for (int i = 0; i < lines.Length; i++)
				{
					int p = lines[i].IndexOf('=');
					if (p <= 0)
						continue;
					SetValue(lines[i].Substring(0, p).Trim(), lines[i].Substring(p + 1).Trim());
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		internal bool ContainsKey(string key)
		{
			return dic.Keys.Contains(key);
		}

		internal string ContainedKey(string word)
		{
			foreach (string key in dic.Keys)
			{
				if (word.Contains(key))
					return dic[key];
			}

			return "200";
		}
	}
}
