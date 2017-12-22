using QTrans.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace QTrans.Classes
{
    public class ParamaterData
    {

        public static Encoding[] Encodings = {
            Encoding.Default,
            Encoding.ASCII,
            Encoding.BigEndianUnicode,
            Encoding.Unicode,
            Encoding.UTF32,
            Encoding.UTF8,
            Encoding.UTF7};

        /*******************************************************
		 *  User Interface 
		 ********************************************************/
        public List<ListViewData> InputListViewWidth = new List<ListViewData>();

        /// <summary>
        /// How to process source file: 0.move; 1.no operation. 2. delete. 3. by program.
        /// </summary>
        public int ProcessSourceFileType = 0;


        /*******************************************************
		 *  Input & Output files and folders.
		 ********************************************************/
        public List<string> InputFolders = new List<string>();
        public List<string> InputFiles = new List<string>();
        public List<string> OutputFiles = new List<string>();

        public String OutputFolder = "C:\\QDas\\Output";
        public String TempFolder = "C:\\QDas\\Temp";
        public string FolderForSuccessed = "C:\\QDas\\Backups\\Success";
        public string FolderForFailed = "C:\\QDas\\Backups\\Failed";
        public List<InputPath> InputPaths = new List<InputPath>();


        /*******************************************************
		 *  System parameters
		 ********************************************************/
        /// <summary>
        ///  Determine how to deal with files with the same name as the output file. 
        ///  0: override.  1: reanme by adding time tick, such as 1_20120516183025.dfq
        /// </summary>
        public int DealSameOutputFileNameType = 0;
        /// <summary>
        /// How many original level kept for output.
        /// </summary>
        public int OriginalLevelKept = 0;
        /// <summary>
        /// Show confirming dialog while closing.
        /// </summary>
        public bool ConfirmWhileClosing = true;
        /// <summary>
        /// Immediately start transducing when this program is started.
        /// </summary>
        public bool StartTransducingWhenStartup = false;
        /// <summary>
        /// 打开文件时，选择的默认索引序号同，如“txt文件|*.txt|所有文件|*.*”
        /// 注意：此值下标从1开始，所以默认为1。
        /// </summary>
        public int FilterIndex = 1;
        /// <summary>
        /// 是否显示文件选项，有个别不是根据文件保存的，而是文件组，如COMAU，这样这时就不显示文件选项了。
        /// </summary>
        public bool ShowFileOption = true;

        /// <summary>
        /// Whether add a time stamp when saving from input file name.
        /// </summary>
        public bool AppendTimeStamp = true;
        /// <summary>
        /// 是否遍历子目录。
        /// </summary>
        public bool TraverseSubfolders = false;

        /// <summary>
        /// Start with windows.
        /// </summary>
        public bool StartWithWindows = false;

        /// <summary>
        /// 扩展名列表
        /// </summary>
        public List<string> extentions = new List<string>();

        /// <summary>
        /// Supports PDF files.
        /// </summary>
        public bool PdfMode;
        /// <summary>
        /// Output folder.
        /// </summary>
        public string PdfDllFolder;


        public int EncodingID = 0;

        /******************** Auto Transduce **********************/
        public DateTime StartTime = new DateTime();
        public DateTime EndTime = new DateTime();
        public int CircleValue = 1;
        public int CircleUnit = 60;

        /// <summary>
        /// whether turns on the autotrans. Note: 'SupportAutoTransducer' should be true. 
        /// </summary>
        public bool AutoTransducerAvaliable = false;
        /// <summary>
        /// Whether this transducer supports automatic transduction.
        /// </summary>
        public bool SupportAutoTransducer = false;


        public static ParamaterData Load(string path)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                ParamaterData pd = WindGoes.Data.Serializer.GetObject<ParamaterData>(doc);
                pd.extentions.Clear();
                return pd;
            }
            catch { }
            return new ParamaterData();
        }

        public ParamaterData()
        {
            string qds = "C:\\QDAS\\";
            OutputFolder = qds + "Output";
            TempFolder = qds + "Temp\\";
            FolderForSuccessed = qds + "Backups\\Success\\";
            FolderForFailed = qds + "Backups\\Failed";
            OriginalLevelKept = 0;
            ProcessSourceFileType = 1;
            ConfirmWhileClosing = true;
            StartTransducingWhenStartup = false;
            StartWithWindows = false;
            StartTime = new DateTime(2000, 1, 1, 0, 0, 0);
            EndTime = new DateTime(2000, 1, 1, 23, 59, 59);
            CircleValue = 10;
            CircleUnit = 1;

            TraverseSubfolders = false;
            extentions = new List<string>();
        }

        public string GetOutDirectory(string infile)
        {
            return Path.Combine(OutputFolder, FileHelper.GetLastDirectory(infile, OriginalLevelKept));
        }

        public ParamaterData Clone()
        {
            ParamaterData sp = new ParamaterData();

            for (int i = 0; i < InputFiles.Count; i++)
                sp.InputFiles.Add(InputFiles[i]);

            for (int i = 0; i < InputFolders.Count; i++)
                sp.InputFolders.Add(InputFolders[i]);


            sp.OutputFolder = OutputFolder;
            sp.TempFolder = TempFolder;
            sp.FolderForSuccessed = FolderForSuccessed;
            sp.FolderForFailed = FolderForFailed;

            sp.OriginalLevelKept = OriginalLevelKept;
            sp.ConfirmWhileClosing = ConfirmWhileClosing;
            sp.StartTransducingWhenStartup = StartTransducingWhenStartup;
            sp.StartWithWindows = StartWithWindows;
            sp.DealSameOutputFileNameType = DealSameOutputFileNameType;
            sp.FilterIndex = FilterIndex;
            sp.ShowFileOption = ShowFileOption;
            sp.AppendTimeStamp = AppendTimeStamp;

            sp.AutoTransducerAvaliable = AutoTransducerAvaliable;
            sp.SupportAutoTransducer = SupportAutoTransducer;
            sp.CircleValue = CircleValue;
            sp.CircleUnit = CircleUnit;
            sp.StartTime = StartTime;
            sp.EndTime = EndTime;

            return sp;
        }


        public int getCircleInterval()
        {
            return CircleValue * (int)Math.Pow(60, CircleUnit);
        }

        /// <summary>
        /// 添加可以被访问的扩展名，名称长度不限，加不加点都行。会自动转换为小写。
        /// </summary>
        /// <param name="name"></param>
        public void AddExt(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            if (name[0] != '.')
                name = "." + name;

            extentions.Add(name.ToLower());
        }

        /// <summary>
        /// 验证一个文件全路径是否是合法的文件类型。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool ValidateExtention(string path)
        {
            string ext = Path.GetExtension(path).ToLower();
            for (int i = 0; i < extentions.Count; i++)
            {
                if (extentions[i].Contains(ext))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获得可用的扩展名。这个是对话框所使用的。
        /// </summary>
        /// <returns></returns>
        public string GetExtFilter()
        {
            StringBuilder builder = new StringBuilder();

            if (extentions.Count == 0)
                return "所有文件|*.*";

            builder.Append("待转换的数据文件|");
            for (int i = 0; i < extentions.Count; i++)
                builder.Append("*" + extentions[i] + ";");
            builder.Remove(builder.Length - 1, 1);
            builder.Append("|所有文件|*.*");

            return builder.ToString();
        }

        /// <summary>
        /// Load from XML data.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ParamaterData LoadParamater(string path)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                return WindGoes.Data.Serializer.GetObject<ParamaterData>(doc);
            }
            catch { }
            return new ParamaterData();
        }

        public void Save(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
            WindGoes.Data.Serializer.GetXmlDoc(this).Save(path);
        }

        public string GetOutfileName(string infile)
        {
            string keptfolder = FileHelper.GetLastDirectory(infile, OriginalLevelKept);
            return string.Format("{0}\\{1}.dfq", Path.Combine(OutputFolder, keptfolder), Path.GetFileNameWithoutExtension(infile));
        }
    }
}
