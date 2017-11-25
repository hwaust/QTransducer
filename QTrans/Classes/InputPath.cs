using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QTrans.Classes
{
    public class InputPath
    {
        public string path = "";

        /// <summary>
        /// 0: file.  1: folder.
        /// </summary>
        private int type = 0;

        private DateTime lastTranducingTime;

        int fileCount = 1;

        /// <summary>
        ///  0:file, 1: folder.
        /// </summary>
        public int Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public DateTime LastTranducingTime
        {
            get
            {
                return lastTranducingTime;
            }

            set
            {
                lastTranducingTime = value;
            }
        }

        public int FileCount
        {
            get
            {
                return fileCount;
            }

            set
            {
                fileCount = value;
            }
        }


        public InputPath() { }

        /// <summary>
        /// 路径和类型（0为文件，1为文件夹）
        /// </summary>
        /// <param name="p"></param>
        /// <param name="t"></param>
		public InputPath(string path, int type)
        {
            this.path = path;
            Type = type;
            FileCount = Type == 0 ? 1 : Directory.GetFiles(path).Length;
            LastTranducingTime = new DateTime(2000, 1, 1);
        }

        public InputPath(string path)
        {
            this.path = path;
            Type = Directory.Exists(path) ? 1 : 0;
            FileCount = Type == 0 ? 1 : Directory.GetFiles(path).Length;
            LastTranducingTime = new DateTime(2000, 1, 1);
        }

        public string[] ToStrings()
        {
            string s = LastTranducingTime.Year == 2000 && LastTranducingTime.Month == 1 && LastTranducingTime.Day == 1 ?
                "尚未转换" : LastTranducingTime.ToString();
            return new string[] { path, FileCount + "", s, "○" };
        }
    }
}
