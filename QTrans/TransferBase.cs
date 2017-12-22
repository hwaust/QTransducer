using System;
using System.Collections.Generic;
using System.Text;
using QDAS;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using QTrans.Classes;
using System.Xml;
using QTrans.Helpers;

namespace QTrans
{
    public class TransferBase
    {
        public ParamaterData pd = new ParamaterData();

        #region 属性
        /// <summary>
        /// A unique integer ID for each tranducer. Format: YYYYXX, where YYYY is year and XX is a concecutive number. 
        /// </summary>
        public string TransducerID { get; set; }


        /// <summary>
        /// 显示的公司名称，这个用于显示在封面上，如Mairi。
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 转换器的版本信息，每家公司使用自己的版本号，如果为空则使用转换器的版本号。(不要带V）
        /// </summary>
        public string VertionInfo { get; set; }

        /// <summary>
        /// 文件日志列表，转换后可读取此列表。
        /// </summary>
        public List<TransLog> LogList = new List<TransLog>();

        /// <summary>
        /// 打开文件时，选择的默认索引序号同，如“txt文件|*.txt|所有文件|*.*”
        /// 注意：此值下标从1开始，所以默认为1。
        /// </summary>
        public int FilterIndex { get; set; }


        /// <summary>
        /// 是否显示文件选项，有个别不是根据文件保存的，而是文件组，如COMAU，这样这时就不显示文件选项了。
        /// </summary>
        public bool ShowFileOption { get; set; }

        /// <summary>
        /// 转换完成的情况。
        /// </summary>
        public double Percentage { get; set; }

        /// <summary>
        /// 正在转换的目录。
        /// </summary>
        public string CurrentFolder { get; set; }

        /// <summary>
        /// 当前正在转换的文件。
        /// </summary>
        public string CurrentFile { get; set; }

        /// <summary>
        /// 最后一次输出的DFQ文件。
        /// </summary>
        public string LastDfqFile { get; set; }



        /// <summary>
        /// 当文件转换完成时发生此事件。
        /// </summary>
        public event TransFileCompleteEventHandler TransFileComplete;

        #endregion



        public TransferBase()
        {
            ShowFileOption = true;
            FilterIndex = 1;
            //主要是为了添加操作失误信息方便。 
            pd = ParamaterData.Load(".\\config.xml");
            SetConfig(pd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pd"></param>
        public virtual void SetConfig(ParamaterData pd)
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
            Thread.Sleep(200);
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
            //如果后缀名不对或者文件不存在，就直接忽略，不产生任何记录。
            if (!CheckExt(infile) || !File.Exists(infile))
                return false;

            //这是对当前转换的文件和文件路径进行记录。
            CurrentFile = infile;
            CurrentFolder = Path.GetDirectoryName(CurrentFile);

            // 转换数据
            TransLog log = null;
            try
            {
                TransferFile(infile);
                log = new TransLog(infile, LastDfqFile, "转换成功", LogType.Success);
            }
            catch (Exception ex)
            {
                log = new TransLog(infile, LastDfqFile, "转换失败，原因：" + ex.Message, LogType.Fail);
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
                    string outputfile = common.AddIncreamentId(backupFolder + "\\" + fi.Filename + fi.Extention);

                    // copy file. If failed, add the error to logs.
                    if (!copyFile(infile, outputfile))
                    {
                        LogList.Add(new TransLog(infile, outputfile, String.Format("复制文件 '{0}' 至 '{1}' 失败。", infile, outputfile), LogType.Backup));
                    }

                    // delete source file.
                    if (!deleteFile(infile))
                    {
                        LogList.Add(new TransLog(infile, outputfile, String.Format("文件 {0} 删除失败。", infile), LogType.Backup));
                    }

                    break;

                case 1: // no change.
                    break;

                case 2: // delete files.
                    deleteFile(infile);
                    break;

                case 3: // customed.
                    break;
            }

            return log.LogType == LogType.Success;
        }


        private bool copyFile(string inputfile, string outputfile)
        {
            // Try 5 times at most. Note: input file should exist and outputfile should not.
            int copyTimes = 0;
            while (true)
            {
                File.Copy(inputfile, outputfile);
                Thread.Sleep(100);
                if (File.Exists(outputfile) || copyTimes++ > 5)
                    break;
            }

            return File.Exists(outputfile);
        }

        /// <summary>
        /// Deletes the input file when backuping. Tries 5 times and adds to logs if fails.
        /// </summary>
        /// <param name="inputfile"></param>
        private bool deleteFile(string inputfile)
        {
            // Delete 5 times at most.
            int deleteTimes = 0;
            while (true)
            {
                File.Delete(inputfile);
                Thread.Sleep(100);

                if (!File.Exists(inputfile) || deleteTimes++ > 5)
                    break;
            }

            return !File.Exists(inputfile);
        }

        /// <summary>
        /// 在开始转换以后需要做的准备工作，转换时最先执行并只执行一次。
        /// </summary>
        public virtual bool Initialize()
        {
            return true;
        }

        #endregion



        /// <summary>
        /// 根据QFile和输入路径，自动保存DFQ文件至相应的输出路径。
        /// </summary>
        /// <param name="qf">需要保存的QFile。</param>
        /// <param name="infile">输入的文件路径。</param>
        /// <returns></returns>
        public bool SaveDfqByInpath(QFile qf, string infile)
        {
            string outpath = pd.OutputFolder + "\\" + FileHelper.GetOutputPath(infile, pd.OutputFolder);
            try
            {
                return SaveDfq(qf, infile, outpath);
            }
            catch (Exception ex)
            {
                LogList.Add(new TransLog(infile, outpath, "转换失败，原因：" + ex.Message, LogType.Fail));
            }
            return false;
        }


        /// <summary>
        /// 将转换好的DFQ文件进行保存。
        /// </summary>
        /// <param name="qf">需要保存的DFQ数据。</param>
        /// <param name="outpath">输出的DFQ文件。</param>
        /// <returns></returns>
        public bool SaveDfq(QFile qf, string outpath)
        {
            return SaveDfq(qf, CurrentFile, outpath);
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
                        deleteFile(outfile);
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
                    LastDfqFile = outfile;
                    log = new TransLog(infile, outfile, "转换成功", LogType.Success);
                }
                else
                {
                    LastDfqFile = null;
                    log = new TransLog(infile, outfile, "转换失败，原因：TransferBase.SaveDfq.unknown.", LogType.Fail);
                }
            }
            catch (Exception ex)
            {
                log = new TransLog(infile, outfile, "转换失败，原因：" + ex.Message, LogType.Fail);
            }
            LogList.Add(log);

            return log.LogType == LogType.Success;
        }

        /// <summary>
        /// 检测文件的后缀名是否在现有后缀名列表中。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual bool CheckExt(string path)
        {
            try
            {
                return pd.extentions.IndexOf(Path.GetExtension(path).ToLower()) >= 0;
            }
            catch { }
            return false;
        }
    }
}
