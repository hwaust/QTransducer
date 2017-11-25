using System;
using System.Collections.Generic;
using System.Text;
using QDAS;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using QTrans.Classes;
using System.Xml;

namespace QTrans
{
    public class TransferBase
    {
        public ParamaterData pd = new ParamaterData();

        #region 属性
        /// <summary>
        /// 文件日志列表，转换后可读取此列表。
        /// </summary>
        public List<TransLog> LogList = new List<TransLog>();


        /// <summary>
        /// 在保存时，是否添加时间戳，TRUE为添加，False直接覆盖。
        /// </summary>
        public bool AddTimeToNew { get; set; }

        /// <summary>
        /// 注册钥匙的名称，默认为key.lic为示区别，需要专门设置。
        /// </summary>
        public string RegistryKeyName { get; set; }

        /// <summary>
        /// 显示的公司名称，这个用于显示在封面上，如Mairi。
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 是否在输出的DFQ文件名中强制追加时间截
        /// </summary>
        public bool ForcedAddDateToFileName { get; set; }

        /// <summary>
        /// 打开文件时，选择的默认索引序号同，如“txt文件|*.txt|所有文件|*.*”，注意：此值下标从1开始，所以默认为1。
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
        /// 用于向MainForm的菜单栏添加按键。
        /// </summary>
        public ToolStripButton[] tbs = null;


        /// <summary>
        /// 转换器的版本信息，每家公司使用自己的版本号，如果为空则使用转换器的版本号。(不要带V）
        /// </summary>
        public string VertionInfo { get; set; }

        /// <summary>
        /// 当文件转换完成时发生此事件。
        /// </summary>
        public event TransFileCompleteEventHandler TransFileComplete;

        #endregion


        public ParamaterData LoadParamater(string path)
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

        public TransferBase()
        {
            ShowFileOption = true;
            FilterIndex = 1;
            RegistryKeyName = "trial.lic";
            //主要是为了添加操作失误信息方便。
            FileHelper.trans = this;
            pd = LoadParamater(Application.StartupPath + "\\config.xml");
            pd.extentions.Clear();
            SetConfig(pd);
        }

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
        /// 根据指定的文件，生成唯一的DFQ的实例。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual QFile GetQFile(string path)
        {
            return new QFile();
        }



        /// <summary>
        /// 处理文件函数，包括处理前后的主要业务逻辑。
        /// </summary>
        /// <param name="inputfile"></param>
        /// <returns></returns>
        public virtual bool ProcessFile(string inputfile)
        {
            //如果后缀名不对或者文件不存在，就直接忽略，不产生任何记录。
            if (!CheckExt(inputfile) || !File.Exists(inputfile))
                return false;

            //这是对当前转换的文件和文件路径进行记录。
            CurrentFile = inputfile;
            CurrentFolder = Path.GetDirectoryName(CurrentFile);

            // 转换数据
            bool isTranSuccessful = true;
            try
            {
                TransferFile(inputfile);
            }
            catch (Exception ex)
            {
                isTranSuccessful = false;
                Console.WriteLine("Failed file: " + CurrentFile);
                AddLog("转换未成功。", ex.Message);
            }

            // invoke the TransFileComplete event. Note ? denotes nullable.
            TransFileComplete.Invoke(this, GetResultLog(CurrentFile, LastDfqFile, isTranSuccessful));

            // Processes input file.
            switch (pd.ProcessSourceFileType)
            {
                // move inputfile ==> backup folder. 
                case 0:
                    // the fileinfo of input file.
                    FilenameInfo fi = new FilenameInfo(inputfile);

                    // select the backup folder according to the processing result.
                    string backupFolder = isTranSuccessful ? pd.FolderForSuccessed : pd.FolderForFailed;

                    // create backup directory.
                    if (!createDirectory(backupFolder))
                        break;

                    // construct outputfile with a unique file name. 
                    string outputfile = common.AddIncreamentId(backupFolder + "\\" + fi.FileName + fi.Extention);

                    // copy file
                    copyFile(inputfile, outputfile);

                    // delete source file.
                    deleteFile(inputfile);

                    break;

                case 1: // no change.
                    break;

                case 2: // delete files.
                    deleteFile(inputfile);
                    break;

                case 3: // customed.
                    break;
            }


            return isTranSuccessful;
        }

        private bool createDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                // failed to create the directory.
                if (!Directory.Exists(directory))
                {
                    AddLog(directory, "备份文件夹 '" + directory + "' 创建失败。");
                    return false;
                }
            }

            return true;
        }

        private void copyFile(string inputfile, string outputfile)
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

            // Check whether output is created. If not, add the error to logs.
            if (!File.Exists(outputfile))
                AddLog(inputfile, String.Format("复制文件 '{0}' 至 '{1}' 失败。", inputfile, outputfile));

        }

        /// <summary>
        /// Deletes the input file when backuping. Tries 5 times and adds to logs if fails.
        /// </summary>
        /// <param name="inputfile"></param>
        private void deleteFile(string inputfile)
        {
            // Delete 5 times at most.
            int deleteTimes = 0;
            while (true)
            {
                FileHelper.DeleteFile(inputfile);
                Thread.Sleep(100);
                if (!File.Exists(inputfile) || deleteTimes++ > 5)
                    break;
            }

            // Check whether input file is created. If so, add the error to logs.
            if (File.Exists(inputfile))
                AddLog(inputfile, String.Format("文件 {0} 删除失败。", inputfile));
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
        /// 根据输入文件和输出文件夹返回输出目录，如果需要保持路径，则返回输出+最后一级路径目录的方式。
        /// 如：输出为 D:\Output  输入为：C:\data\file.ext 那么，如果不保持路径，返回 D:\Output,
        /// 如果返回路径，则返回 D:\Output\data
        /// </summary>
        /// <param name="input">输入文件路径。</param>
        /// <param name="outfolder">输出文件夹。</param>
        /// <returns></returns>
        public string GetOutFolder(string input, string outfolder)
        {
            string folder = outfolder;

            if (pd.OriginalLevelKept > 0)
            {
                string[] fds = input.Split(new char[] { '\\', ':', '/' }, StringSplitOptions.RemoveEmptyEntries);

                List<string> folders = new List<string>();
                //如原目录为c:\f1\abc\d.dfq，则新结果为2个元素：[f1][abc]
                for (int i = 0; i < fds.Length - 1; i++)
                    folders.Add(fds[i]);

                //移除前面不需要的项
                int offet = folders.Count - pd.OriginalLevelKept;
                while (offet-- > 0)
                    folders.RemoveAt(0);

                //进行拼接，生成新的路径
                if (folders.Count > 0)
                    folder = Path.Combine(folder, string.Join("\\", folders.ToArray()));
            }

            return folder;
        }



        /// <summary>
        /// 根据QFile和输入路径，自动保存DFQ文件至相应的输出路径。
        /// </summary>
        /// <param name="qf">需要保存的QFile。</param>
        /// <param name="inpath">输入的文件路径。</param>
        /// <returns></returns>
        public bool SaveDfqByInpath(QFile qf, string inpath)
        {
            try
            {
                string outpath = GetOutFolder(inpath, pd.OutputFolder) + "\\" + Path.GetFileNameWithoutExtension(CurrentFile) + ".dfq";
                return SaveDfq(qf, inpath, outpath);
            }
            catch (Exception ex)
            {
                AddFailedFile(inpath, "文件保存失败，原因：" + ex.Message);
                return false;
            }
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
        public bool SaveDfq(QFile qf, string inpath, string outputfile)
        {
            outputfile = processOutputFile(outputfile);

            bool isSaveSuccessful = false;
            try
            {
                //进行保存
                isSaveSuccessful = saveQFile(qf, outputfile);
                if (isSaveSuccessful)
                {
                    LastDfqFile = outputfile;
                    AddSuccessedFile(outputfile);
                }
                else
                {
                    LastDfqFile = null;
                    AddFailedFile(inpath, "转换失败。");
                }
            }
            catch (Exception e1)
            {
                AddFailedFile(inpath, e1.Message);
                isSaveSuccessful = false;
            }

            return isSaveSuccessful;
        }

        public string processOutputFile(string outputfile)
        {
            if (File.Exists(outputfile))
            {
                switch (pd.DealSameOutputFileNameType)
                {
                    case 0: // 直接覆盖。
                        deleteFile(outputfile);
                        break;

                    case 1: //添加时间戳
                        outputfile = common.AddTimeTick(outputfile);
                        break;

                    case 2: //添加自增长编号。
                        outputfile = common.AddIncreamentId(outputfile);
                        break;
                    default:
                        break;
                }

                //创建输出目录。
                Directory.CreateDirectory(Path.GetDirectoryName(outputfile));
            }

            return outputfile;
        }

        protected virtual bool saveQFile(QFile qf, string outpath)
        {
            return qf.SaveToFile(outpath);
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
                string ext = Path.GetExtension(path).ToLower();

                if (pd.extentions.IndexOf(ext) < 0)
                {
                    return false;
                }
            }
            catch { return false; }

            return true;
        }



        public TransLog GetResultLog(string filename, string output, bool isSuccessful)
        {
            TransLog log = new TransLog();
            log.LogType = isSuccessful ? LogType.Success : LogType.Fail;
            log.Content = isSuccessful ? "转换成功。" : "转换失败。";
            log.Input = filename;
            log.Output = output;
            return log;
        }

        /// <summary>
        /// 添加转换成功的文件到日志列表。
        /// </summary>
        /// <param name="file"></param>
        public void AddSuccessedFile(string file)
        {
            //string s = string.Format("{0}|DONE|转换成功。|{1}", DateTime.Now, file);
            TransLog log = new TransLog();
            log.LogType = LogType.Success;
            log.Content = "转换成功。";
            log.Output = file;
            LogList.Add(log);
        }

        /// <summary>
        /// 添加转换失败的文件到日志列表。
        /// </summary>
        /// <param name="file"></param>
        /// <param name="reason"></param>
        public void AddFailedFile(string file, string reason)
        {
            //string s = string.Format("{0}|FAIL|转换失败，原因：{1}|{2}", DateTime.Now, reason, file);
            TransLog log = new TransLog();
            log.LogType = LogType.Fail;
            log.Content = "转换失败，原因：" + reason;
            log.Input = file;
            LogList.Add(log);
        }

        /// <summary>
        /// 添加日志。日志格式为 时间|文件路径|日志类型|日志内容
        /// </summary>
        /// <param name="file">相关文件。</param>
        /// <param name="log">日志信息</param>
        public void AddLog(string file, string content)
        {
            //string s = string.Format("{0}|LOG|{1}|{2}", DateTime.Now, log, file);
            //LogList.Add(s);
            TransLog log = new TransLog();
            log.LogType = LogType.Fail;
            log.Content = content;
            log.Input = file;
            LogList.Add(log);
        }

        public void AddLog(string file, string content, LogType logtype)
        {
            //string s = string.Format("{0}|LOG|{1}|{2}", DateTime.Now, log, file);
            //LogList.Add(s);
            TransLog log = new TransLog();
            log.LogType = logtype;
            log.Content = content;
            log.Input = file;
            LogList.Add(log);
        }



        /// <summary>
        /// 根据给定输入路径，给出输出路径，主要用于解决输入为多级目录时的目录关系。
        /// 情况1：输入C:\abc.txt， 输入目录为空，输出D:\QDas，则输出为：abc.dfq
        /// 情况2：输入C:\abc.txt， 输入目录为C:，输出D:\QDas，则输出为：abc.dfq
        /// 情况3：输入C:\data\abc.txt， 输入目录为C:\data，输出D:\QDas，则输出为：data\abc.dfq
        /// 情况4：输入C:\data\2012\abc.txt， 输入目录为C:\data，输出D:\QDas，则输出为：data\2012\abc.dfq
        /// 那么输出为相对路径为： D:\QDas\abc\a.dfq，此函数返回结果为abc
        /// </summary>
        /// <param name="path">待检测的输入路径。</param>
        /// <returns></returns>
        public string GetOutputPath(string path)
        {
            return funs.GetOutputPath(path, CurrentFolder);
        }

        /// <summary>
        /// 给定路径，获得下面指定层数的文件。
        /// </summary>
        /// <param name="dir">指定的路径。</param>
        /// <param name="lv">子目录层数，最少为0，即只获得当前目录下文件。</param>
        /// <returns></returns>
        public string[] GetFile(string dir, int lv)
        {
            List<string> list = new List<string>();
            list.AddRange(Directory.GetFiles(dir));
            if (lv > 0)
            {
                string[] dirs = Directory.GetDirectories(dir);

                foreach (string s in dirs)
                {
                    list.AddRange(GetFile(s, lv - 1));
                }
            }

            return list.ToArray();
        }







    }
}
