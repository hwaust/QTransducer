using System.IO;

namespace QTrans
{
    /// <summary>
    /// Used to help deal with file names.  
    /// Useage: FilenameInfo fi = FilenameInfo.Parse("c:\\data\\file1.xml");
    /// </summary>
    public class FilenameInfo
    {
        string filePath;
        /// <summary>
        /// 文件全路径，包括文件名，如：C:\abc\bbc.txt。
        /// </summary>
        public string FilePath
        {
            get { return filePath; }
        }

        string filename;
        /// <summary>
        /// 文件的名子。
        /// </summary>
        public string Filename
        {
            get { return filename; }
        }

        string extention;
        /// <summary>
        /// 扩展名(首位为点)。
        /// </summary>
        public string Extention
        {
            get { return extention; }
        }

        string fullDirectory;
        /// <summary>
        /// 全路径名称，不包括文件名，如C:\abc
        /// </summary>
        public string FullDirectory
        {
            get { return fullDirectory; }
        }

        string lastDirectoryName;
        /// <summary>
        /// 目录的最后一个名称，如C:\abc\bbc.txt，则此值为abc
        /// </summary>
        public string LastDirectoryName
        {
            get { return lastDirectoryName; }
        }

        FilenameInfo() { }

        int depth;
        public int Depth { get { return depth; } }

        public static FilenameInfo Parse(string filePath)
        {
            FilenameInfo fi = new FilenameInfo();
            try
            {
                fi.filePath = filePath;
                fi.filename = Path.GetFileNameWithoutExtension(filePath);
                fi.extention = Path.GetExtension(filePath);
                fi.fullDirectory = Path.GetDirectoryName(filePath);
                string[] tmp = fi.fullDirectory.Split('\\');
                fi.depth = tmp.Length - 1;
                fi.lastDirectoryName = tmp[tmp.Length - 1];
            }
            catch
            {
                return null;
            }

            return fi;
        }



    }
}
