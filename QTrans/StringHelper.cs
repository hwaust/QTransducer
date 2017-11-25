using QTrans.Classes;
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
                return path + "\\" + filename + "_" + DateTimeHelper.ToYYYYMMDDhhmmssString(DateTime.Now) + ext;
            }
            catch { }

            return dst;
        }



    }
}
