using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QTrans.Classes
{
	/// <summary>
	/// 转换时的日志类，用于记录一些转换信息，每个Log对象表示一条日志信息。
	/// </summary>
	public class TransLog
	{
		private char splitter = (char)15;

		/// <summary>
		/// 用于生成最新的编号，默认值为10000。
		/// </summary>
		public static int LogID = 10000;

		/// <summary>
		/// 日志编号。
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 日志的类型。
		/// </summary>
		public LogType LogType { get; set; }

		private DateTime date = DateTime.Now;
		/// <summary>
		/// 转换时间。
		/// </summary>
		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}

		/// <summary>
		/// 输入文件或文件夹。
		/// </summary>
		public String Input { get; set; }

		/// <summary>
		/// 输出文件或文件组。
		/// </summary>
		public String Output { get; set; }


		/// <summary>
		/// 日志的内容。
		/// </summary>
		public string Content { get; set; }


		/// <summary>
		/// 备注。
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// 可以附加的数据。
		/// </summary>
		public object Tag { get; set; }

		public TransLog() { ID = ++LogID; }
		/// <summary>
		/// 日志格式为 时间|文件路径|日志类型|日志内容
		/// </summary>
		/// <param name="output">文件路径</param>
		/// <param name="lt">日志类型</param>
		/// <param name="content">日志内容</param>
		public TransLog(string output, LogType lt, string content)
		{
			ID = ++LogID;
			LogType = lt;
			Output = output;
			Content = content;
		}

		public String ToFileString()
		{
			StringBuilder b = new StringBuilder();
			String s = splitter.ToString();
			b.Append(this.ID + s);
            b.Append(WindGoes.DateHelper.ToStringYYYYMMDD(date) + s);
			b.Append(this.LogType + s);
			b.Append(this.Content + s);
			b.Append(this.Remark + s);
			b.Append(this.Input + s);
			b.Append(this.Output);

			return b.ToString();
		}

		public void FromFileString(String data)
		{
			try
			{
				string[] strs = data.Split(splitter);
				ID = int.Parse(strs[0]);
				date = StringHelper.GetDateFromYYYYMMDDhhmmssString(strs[1]);
				LogType = (LogType)Enum.Parse(typeof(LogType), strs[2]);
				Content = strs[3];
				this.Remark = strs[4];
				this.Input = strs[5];
				this.Output = strs[6];
			}
			catch (Exception e1)
			{
				Console.WriteLine(e1.Message);
			}
		}

		public string[] GetStrings()
		{
			//编号，0.时间，1.事件，2.原因，3.输出文件
			return new string[] { ID.ToString(), Date.ToString(), LogType.ToString(), Content, Output };
		}
	}

	/// <summary>
	/// 日志的类型。
	/// </summary>
	public enum LogType : int
	{
		/// <summary>
		/// 一般的日志。
		/// </summary>
		Nomal = 0,
		/// <summary>
		/// 转换成功 。
		/// </summary>
		Success = 1,
		/// <summary>
		/// 转换失败。
		/// </summary>
		Fail = 2,
		/// <summary>
		/// 系统事件。
		/// </summary>
		System = 4,
		/// <summary>
		/// 备份事件。
		/// </summary>
		Backup = 8,
        /// <summary>
        /// 用于测试。
        /// </summary>
        Debug=16,
		/// <summary>
		/// 未知。
		/// </summary>
		Unknown = -1
	}
}
