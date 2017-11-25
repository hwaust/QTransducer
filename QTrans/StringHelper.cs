using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QTrans
{
	class StringHelper
	{
		/// <summary>
		/// 指定的格式为：20121105 -> 2012-11-05 00:00:00
		/// </summary>
		/// <param name="dts"></param>
		/// <returns></returns>
		public static DateTime GetDateFromYYYYMMDDString(string dts)
		{
			DateTime dt = new DateTime(2012, 1, 1, 0, 0, 0);
			try
			{
				dt = new DateTime(
					int.Parse(dts.Substring(0, 4)),
					int.Parse(dts.Substring(4, 2)),
					int.Parse(dts.Substring(6, 2)));
			}
			catch { }
			return dt;
		}

		/// <summary>
		/// 指定的格式为：20121105153246 -> 2012-11-05 15:32:46
		/// </summary>
		/// <param name="dts"></param>
		/// <returns></returns>
		public static DateTime GetDateFromYYYYMMDDhhmmssString(string dts)
		{
			DateTime dt = new DateTime(2012, 1, 1, 0, 0, 0);
			try
			{
				dt = new DateTime(
					int.Parse(dts.Substring(0, 4)),
					int.Parse(dts.Substring(4, 2)),
					int.Parse(dts.Substring(6, 2)),
					int.Parse(dts.Substring(8, 2)),
					int.Parse(dts.Substring(10, 2)),
					int.Parse(dts.Substring(12, 2)));
			}
			catch { }
			return dt;
		}


		/// <summary>
		/// 给定日期，返回字符串，格式为：2013-04-09 22:52:35 -> 20130409225235
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static string ToYYYYMMDDhhmmssString(DateTime dt)
		{
			return string.Format("{0}{1}{2}{3}{4}{5}",
				dt.Year,
				dt.Month.ToString("00"),
				dt.Day.ToString("00"),
				dt.Hour.ToString("00"),
				dt.Minute.ToString("00"),
				dt.Second.ToString("00"));
		}


		/// <summary>
		/// 给指定文件名加时间戳。
		/// </summary>
		/// <param name="dst">目标地址。</param>
		/// <returns></returns>
		public static string AppendTimeToFileName(string dst)
		{
			try
			{
				string path = Path.GetDirectoryName(dst);
				string filename = Path.GetFileNameWithoutExtension(dst);
				string ext = Path.GetExtension(dst);
				return path + "\\" + filename + "_" + ToYYYYMMDDhhmmssString(DateTime.Now) + ext;
			}
			catch { }

			return dst;
		}

	}
}
