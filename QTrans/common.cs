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
