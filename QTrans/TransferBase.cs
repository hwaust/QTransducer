using System;
using System.Collections.Generic;
using QDAS;
using System.Threading;
using System.IO;
using QTrans.Classes;
using QTrans.Helpers;
using System.Reflection;
using System.Windows.Forms;

namespace QTrans
{
    public class TransferBase
    {
        #region 属性
        /// <summary>
        /// System configuration file.
        /// </summary>
        public ParamaterData pd = null;

        /// <summary>
        /// A unique integer ID for each tranducer. Format: YYYYXX, where YYYY is year and XX is a concecutive number. 
        /// </summary>
        public string TransducerID;

        public static TransferBase getTransducer()
        {
            // Return the latest transducer ordered by name 
            Assembly asm = Assembly.LoadFile(Application.StartupPath + "\\QTrans.dll");
            List<Type> types = new List<Type>();
            foreach (Type t in asm.GetTypes())
                if (t.FullName.StartsWith("QTrans.Company."))
                    types.Add(t);

            TransferBase tb = (TransferBase) Activator.CreateInstance(types[0]);

            return tb;
        }

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
        #endregion

        public TransferBase()
        {
            string dir = "..\\..\\..\\config_files\\";
            appconfig = Directory.Exists(dir) ? dir + GetType().Name.Substring(0, 7) + ".xml" : ".\\config.xml";
            pd = ParamaterData.Load(appconfig);
            pd.extentions.Clear();
            Initialize();
        }

        /// <summary>
        ///  Initialize transducer.
        /// </summary> 
        public virtual void Initialize()
        {

        }

        #region 可重载函数

        /// <summary>
        /// 转换指定目录下的所有文件。
        /// </summary>
        /// <param name="path"></param>
        public virtual void DealFolder(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string s in files)
            {
                ProcessFile(s);
                Percentage++;
            }

            if (this.pd.TraverseSubfolders)
            {
                String[] folders = Directory.GetDirectories(path);
                foreach (String folder in folders)
                {
                    DealFolder(folder);
                }
            }
        }

        public void ProcessInput(InputPath ip)
        {
            Thread.Sleep(100);
            LogList.Clear();
            try
            {
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

                    // construct outputfile with a unique file name. 
                    string outfile = FileHelper.AddIncreamentId(backupFolder + "\\" + fi.Filename + fi.Extention);
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

                case 3: // customed.
                    break;
            }

            return log.LogType == LogType.Success;
        }
        #endregion

        /// <summary>
        /// 根据QFile和输入路径，自动保存DFQ文件至相应的输出路径，AppendTimeStamp属性决定是否追加时间戳。
        /// </summary>
        /// <param name="qf">需要保存的QFile。</param>
        /// <param name="infile">输入的文件路径。</param>
        /// <returns></returns>
        public bool SaveDfqByInpath(QFile qf, string infile)
        {
            string outdir = Path.Combine(pd.OutputFolder, FileHelper.GetOutputPath(infile, pd.OutputFolder));
            string outfile = string.Format("{0}\\{1}.dfq", outdir, Path.GetFileNameWithoutExtension(infile));

            if (pd.AppendTimeStamp)
            {
                outfile = FileHelper.AppendSuffix(outfile, "_" + DateTimeHelper.ToFullString(DateTime.Now));
            }

            return SaveDfq(qf, infile, outfile);
        }


        /// <summary>
        /// 将转换好的DFQ文件进行保存。
        /// </summary>
        /// <param name="qf">需要保存的DFQ数据。</param>
        /// <param name="outpath">输出的DFQ文件。</param>
        /// <returns></returns>
        public bool SaveDfq(QFile qf, string outpath)
        {
            return SaveDfq(qf, CurrentInFile, outpath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qfile"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public bool SaveDfq(QFile qf, string infile, string outfile)
        {
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

            TransLog log = null;
            try
            {
                if (qf.SaveToFile(outfile))
                {
                    LastOutputDfqFile = outfile;
                    log = new TransLog(infile, outfile, "保存成功", LogType.Success);
                }
                else
                {
                    LastOutputDfqFile = null;
                    log = new TransLog(infile, outfile, "保存失败，原因路径不存在，或者没有写入权限。", LogType.Fail);
                }
            }
            catch (Exception ex)
            {
                log = new TransLog(infile, outfile, "保存失败，原因：" + ex.Message, LogType.Fail);
            }
            if (log.LogType != LogType.Success)
                LogList.Add(log);

            return log.LogType == LogType.Success;
        }

    }
}
