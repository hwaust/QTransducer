using System;
using System.IO;

namespace QTrans.Helpers
{
    public class DateTimeHelper
    {
        /// <summary>
        /// Returns a unique name with time tick added. (if still conficts, four-digit suffix will be added)
        /// </summary>
        /// <param name="inputfile"></param>
        /// <returns></returns>
        public static String AppendFullDateTime(String inputfile)
        {
            string folder = Path.GetDirectoryName(inputfile);
            string filename = Path.GetFileNameWithoutExtension(inputfile);
            string ext = Path.GetExtension(inputfile);

            string outputfile = folder + "\\" + filename + "_" + DateTimeHelper.ToFullDateTime(DateTime.Now) + ext;

            int suffixid = 0;
            while (File.Exists(outputfile))
            {
                outputfile = folder + "\\" + filename + "_" + DateTimeHelper.ToFullDateTime(DateTime.Now) + "_" + suffixid.ToString("0000") + ext;
                suffixid++;
            }

            return outputfile;
        }

        /// <summary>
		/// 指定的格式为：20121105 -> 2012-11-05 00:00:00
		/// </summary>
		/// <param name="dts"></param>
		/// <returns></returns>
		public static DateTime ParseFromDate(string dts)
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
        public static DateTime ParseFromFullDateTime(string dts)
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
        public static string ToFullDateTime(DateTime dt)
        {
            return ToFullDate(dt) + ToFullTime(dt);
        }


        public static string ToFullDate(DateTime dt)
        {
            return string.Format("{0}{1}{2}", dt.Year, dt.Month.ToString("00"), dt.Day.ToString("00"));
        }

        public static string ToFullTime(DateTime dt)
        {
            return string.Format("{0}{1}{2}", dt.Hour.ToString("00"), dt.Minute.ToString("00"), dt.Second.ToString("00"));
        }

    }
}
