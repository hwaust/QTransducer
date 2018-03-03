using System;
using System.Collections.Generic;
using QDAS;
using System.Threading;
using System.IO;
using QTrans.Classes;
using QTrans.Helpers;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;

namespace QTrans
{
    public class TransferBase
    {
        public static TransferBase getTransducer()
        {
            // Return the latest transducer ordered by name 
            var types = from t in Assembly.LoadFile(Application.StartupPath + "\\QTrans.dll").GetTypes()
                        where t.FullName.StartsWith("QTrans.Company.")
                        orderby t.Name descending
                        select t;

            // return (TransferBase)Activator.CreateInstance(types.First());
            return new Company.T201802_HuaCheng_BMW_CSV();
        }

        #region 属性
        /// <summary>
        /// System configuration file.
        /// </summary>
        public ParamaterData pd = null;

        /// <summary>
        /// A unique integer ID for each tranducer. Format: YYYYXX, where YYYY is year and XX is a concecutive number. 
        /// </summary>
        public string TransducerID;

        /// <summary>
        /// 显示的公司名称，这个用于显示在封面上，如Mairi。
        /// </summary>
        public string CompanyName;

        /// <summary>
        /// 转换器的版本信息，每家公司使用自己的版本号，如果为空则使用转换器的版本号。(不要带V）
        /// </summary>
        public string VertionInfo;

        /// <summary>
        /// 文件日志列表，转换后可读取此列表。
        /// </summary>
        public List<TransLog> LogList = new List<TransLog>();

        /// <summary>
        /// 转换完成的情况。
        /// </summary>
        public double Percentage = 0;

        /// <summary>
        /// 当前正在转换的文件。
        /// </summary>
        public string CurrentInFile;

        /// <summary>
        /// 最后一次输出的DFQ文件。
        /// </summary>
        public string LastOutputDfqFile;

        /// <summary>
        /// 当文件转换完成时发生此事件。
        /// </summary>
        public event TransFileCompleteEventHandler TransFileComplete;

        public static string appconfig;

        private InputPath currentInputPath;

        #endregion

        public TransferBase()
        {
            string dir = "..\\..\\..\\config_files\\";
            appconfig = Directory.Exists(dir) ? dir + GetType().Name.Substring(0, 7) + ".xml" : ".\\config.xml";
            pd = ParamaterData.Load(appconfig);
            pd.extentions.Clear();
            Initialize();
        }

        #region 可重载函数

        /// <summary>
        ///  Initialize transducer.
        /// </summary> 
        public virtual void Initialize()
        {

        }
        /// <summary>
        /// 转换指定目录下的所有文件。
        /// </summary>
        /// <param name="folder">Input folder that contains input files and sub folders to be processed.</param>
        public virtual void DealFolder(string folder)
        {
            string[] files = Directory.GetFiles(folder);
            foreach (string s in files)
            {
                ProcessFile(s);
                Percentage++;
            }

            if (pd.TraverseSubfolders)
            {
                String[] folders = Directory.GetDirectories(folder);
                foreach (string subfolder in folders)
                {
                    DealFolder(subfolder);
                }
            }
        }

        public void ProcessInput(InputPath ip)
        {
            Thread.Sleep(100);
            LogList.Clear();
            try
            {
                currentInputPath = ip;
                if (ip.Type == 0)
                    ProcessFile(ip.path);
                else
                    DealFolder(ip.path);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        /// <summary>
        /// 转换文件函数，一般只需要重写这个函数即可，会被DealFile()调用。
        /// </summary>
        /// <param name="path">需要转换的文件路径。</param>
        /// <returns></returns>
        public virtual bool TransferFile(string path)
        {
            return true;
        }

        /// <summary>
        /// 处理文件函数，包括处理前后的主要业务逻辑。
        /// </summary>
        /// <param name="infile"></param>
        /// <returns></returns>
        public virtual bool ProcessFile(string infile)
        {
            // 如果文件不存在或后缀名不匹配，就直接忽略，不产生任何记录。
            if (!File.Exists(infile) || pd.extentions.IndexOf(Path.GetExtension(infile).ToLower()) < 0)
                return false;

            //当前正在转换的文件。
            CurrentInFile = infile;

            // 转换数据
            TransLog log = null;
            try
            {
                TransferFile(infile);
                log = new TransLog(infile, LastOutputDfqFile, "转换成功", LogType.Success);
            }
            catch (Exception ex)
            {
                log = new TransLog(infile, LastOutputDfqFile, "转换失败，原因：" + ex.Message, LogType.Fail);
            }
            LogList.Add(log);

            // invoke the TransFileComplete event. Note ? denotes nullable.
            TransFileComplete.Invoke(this, log);

            // Processes input file.
            switch (pd.ProcessSourceFileType)
            {
                // move inputfile ==> backup folder. 
                case 0:
                    // the fileinfo of input file.
                    FilenameInfo fi = FilenameInfo.Parse(infile);

                    // select the backup folder according to the processing result.
                    string backupFolder = log.LogType == LogType.Success ? pd.FolderForSuccessed : pd.FolderForFailed;

                    // create backup directory.
                    Directory.CreateDirectory(backupFolder);
                    if (!Directory.Exists(backupFolder))
                    {
                        LogList.Add(new TransLog(infile, backupFolder, "备份文件夹'" + backupFolder + "' 创建失败。", LogType.Backup));
                        break;
                    }

                    string outfile = null;
                    switch (pd.KeepBackupFolderStructType)
                    {
                        case 0:
                            // construct outputfile with a unique file name. 
                            outfile = FileHelper.AddIncreamentId(backupFolder + "\\" + fi.Filename + fi.Extention);
                            break;
                        case 1:
                            outfile = FileHelper.GetOutFolder(infile,
                               currentInputPath.Type == 0 ? "" : currentInputPath.path,
                               backupFolder);
                            Directory.CreateDirectory(Path.GetDirectoryName(outfile));
                            break;
                    }
                    // copy file. If failed, add the error to logs.
                    if (!FileHelper.CopyFile(infile, outfile))
                    {
                        LogList.Add(new TransLog(infile, outfile, String.Format("复制文件 '{0}' 至 '{1}' 失败。", infile, outfile), LogType.Backup));
                        break;
                    }

                    // delete source file.
                    if (!FileHelper.DeleteFile(infile))
                    {
                        LogList.Add(new TransLog(infile, outfile, String.Format("文件 {0} 删除失败。", infile), LogType.Backup));
                    }
                    break;

                case 1: // no change.
                    break;

                case 2: // delete files.
                    FileHelper.DeleteFile(infile);
                    break;

                case 3: // customized.
                    break;
            }

            return log.LogType == LogType.Success;
        }
        #endregion

        /// <summary>
        /// 将转换好的DFQ文件进行保存。
        /// </summary>
        /// <param name="qf">需要保存的DFQ数据。</param>
        /// <param name="outfile">输出的DFQ文件。</param>
        /// <returns></returns>
        public bool SaveDfqByInDir(QFile qf, string outfile)
        {
            switch (pd.KeepOutFolderStructType)
            {
                case 0:
                    SaveDfq(qf, outfile);
                    break;
                case 1:
                    outfile = FileHelper.GetOutFolder(CurrentInFile,
                        currentInputPath.Type == 0 ? "" : currentInputPath.path,
                        pd.OutputFolder);
                    SaveDfq(qf, outfile);
                    break;
            }

            return SaveDfq(qf, outfile);
        }

        public string ProcessOutputFileNameIfRepeated(string outfile)
        {
            if (pd.AddTimeTickToOutDFQfile)
                outfile = DateTimeHelper.AppendFullDateTime(outfile);

            if (File.Exists(outfile))
            {
                switch (pd.DealSameOutputFileNameType)
                {
                    case 0: // 直接覆盖。
                        FileHelper.DeleteFile(outfile);
                        break;

                    case 1: //添加时间戳
                        outfile = DateTimeHelper.AppendFullDateTime(outfile);
                        break;

                    case 2: //添加自增长编号。
                        outfile = FileHelper.AddIncreamentId(outfile);
                        break;
                }
                Directory.CreateDirectory(Path.GetDirectoryName(outfile));
            }

            return outfile;
        }


        /// <summary>
        /// 根据文件名保存qf。其中filename不包括路径。
        /// </summary>
        /// <param name="qf"></param>
        /// <param name="filename"></param>
        public void SaveDfqByFilename(QFile qf, string filename)
        {
            // replace illegal characters in filename with '_'
            string illegals = "\\/:?\"<>|";
            for (int i = 0; i < illegals.Length; i++)
            {
                filename = filename.Replace(illegals[i], '_');
            }

            switch (pd.KeepOutFolderStructType)
            {
                case 0:
                    SaveDfq(qf, pd.OutputFolder + "\\" + filename);
                    break;
                case 1:
                    string inroot = currentInputPath.Type == 0 ? "" : currentInputPath.path;
                    string indir = Path.GetDirectoryName(CurrentInFile);
                    string outfile = FileHelper.GetOutFolder(indir + "\\" + filename, inroot, pd.OutputFolder);
                    SaveDfq(qf, outfile);
                    break;
            }
        }

        /// <summary>
        /// 将一个qf保存至outfile. 其中outfile为完整路径。
        /// </summary>
        /// <param name="qf"></param>
        /// <param name="outfile"></param>
        /// <returns></returns>
        public bool SaveDfq(QFile qf, string outfile)
        {
            outfile = ProcessOutputFileNameIfRepeated(outfile);

            string outdir = Path.GetDirectoryName(outfile);
            if (!Directory.Exists(outdir))
                Directory.CreateDirectory(outdir);

            TransLog log = null;
            try
            {
                if (qf.SaveToFile(outfile))
                {
                    LastOutputDfqFile = outfile;
                    log = new TransLog(CurrentInFile, outfile, "保存成功", LogType.Success);
                }
                else
                {
                    LastOutputDfqFile = null;
                    log = new TransLog(CurrentInFile, outfile, "保存失败，原因路径不存在，或者没有写入权限。", LogType.Fail);
                }
            }
            catch (Exception ex)
            {
                log = new TransLog(CurrentInFile, outfile, "保存失败，原因：" + ex.Message, LogType.Fail);
            }
            if (log.LogType != LogType.Success)
                LogList.Add(log);

            return log.LogType == LogType.Success;
        }



    }
}
