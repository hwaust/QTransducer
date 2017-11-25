using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QDAS
{
	public class KItem
	{
		public string KName = "";
		public string Description = "";
		public string DataType = "str"; //数据类型，包括string, int, double, datetime, 前三位缩写
		public object Value = null;

		public KItem() { }
		public KItem(string name, string dt, string desc)
		{
			KName = name;
			Description = desc;
			DataType = dt;
		}
		public KItem(string name, string dt, object value, string desc)
		{
			KName = name;
			Description = desc;
			Value = value;
			DataType = dt;
		}

		public KItem Clone()
		{
			return new KItem(KName, DataType, Value, Description); 
		}

		public override string ToString()
		{
			return ToMyString();
		}

		public string ToMyString()
		{
			return string.Format("Name = {0}, DateType = {1}, Value = {2}, Description = {3}.", 
				KName, DataType, Value, Description);
		}

		public double GetDouble()
		{
			return double.Parse(Value.ToString());
		}

		public int GetInt()
		{
			return int.Parse(Value.ToString());
		}

		public DateTime GetDateTime()
		{
			return DateTime.Parse(Value.ToString());
		}

		public string GetString()
		{
			return Value.ToString();
		}
		 
	}

}
