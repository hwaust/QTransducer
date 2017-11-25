using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QTrans
{
	class common
	{
        public static string config_folder= "..\\..\\..\\cfg_files";

		/// <summary>
		/// Returns a unique name with time tick added. (if still conficts, four-digit suffix will be added)
		/// </summary>
		/// <param name="inputfile"></param>
		/// <returns></returns>
		public static String AddTimeTick(String inputfile)
		{
			string folder = Path.GetDirectoryName(inputfile);
			string filename = Path.GetFileNameWithoutExtension(inputfile);
			string ext = Path.GetExtension(inputfile);

			string outputfile = folder + "\\" + filename + "_" + StringHelper.ToYYYYMMDDhhmmssString(DateTime.Now) + ext;

			int suffixid = 0;
			while (File.Exists(outputfile))
			{
				outputfile = folder + "\\" + filename + "_" + StringHelper.ToYYYYMMDDhhmmssString(DateTime.Now) + "_" + suffixid.ToString("0000") + ext;
				suffixid++;
			} 

			return outputfile;
		}

		/// <summary>
		/// Adds a four-digit id to the filename infront of its extention.
		/// </summary>
		/// <param name="outputfile"></param>
		/// <returns></returns>
		internal static String AddIncreamentId(string outputfile)
		{
			int suffixid = 0;
			string folder = Path.GetDirectoryName(outputfile);
			string filename = Path.GetFileNameWithoutExtension(outputfile);
			string ext = Path.GetExtension(outputfile);

			while (File.Exists(outputfile))
			{
				suffixid++;
				outputfile = string.Format("{0}\\{1}_{2}{3}", folder, filename, suffixid.ToString("0000"), ext);
			}

			return outputfile;
		}
	}
}
