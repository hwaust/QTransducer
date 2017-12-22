using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QTrans.Classes
{
    /// <summary>
    /// 转换时的日志类，用于记录一些转换信息，每个Log对象表示一条日志信息。
    /// </summary>
    public class TransLog
    {
        private char splitter = (char)15;

        /// <summary>
        /// 用于生成最新的编号，默认值为10000。
        /// </summary>
        public static int LogID = 10000;

        /// <summary>
        /// 日志编号。
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 日志的类型。
        /// </summary>
        public LogType LogType { get; set; }

        /// <summary>
        /// 转换时间。
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 输入文件或文件夹。
        /// </summary>
        public String Input { get; set; }

        /// <summary>
        /// 输出文件或文件组。
        /// </summary>
        public String Output { get; set; }

        /// <summary>
        /// 日志的内容。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 备注。
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 可以附加的数据。
        /// </summary>
        public object Tag { get; set; }

        public TransLog() { ID = ++LogID; }

        public TransLog(string infile, string outfile, string message, LogType type, string Remark = "")
        {
            ID = ++LogID;
            Date = DateTime.Now;
            LogType = type;
            Message = message;
            Input = infile;
            Output = outfile;
        }

        public String ToFileString()
        {
            StringBuilder b = new StringBuilder();
            String s = splitter.ToString();
            b.Append(this.ID + s);
            b.Append(WindGoes.DateHelper.ToStringYYYYMMDD(Date) + s);
            b.Append(this.LogType + s);
            b.Append(this.Message + s);
            b.Append(this.Remark + s);
            b.Append(this.Input + s);
            b.Append(this.Output);

            return b.ToString();
        }


        public string[] GetStrings()
        {
            //编号，0.时间，1.事件，2.原因，3.输出文件
            return new string[] { ID.ToString(), Date.ToString(), LogType.ToString(), Message, Output };
        }
    }

    /// <summary>
    /// 日志的类型。
    /// </summary>
    public enum LogType : int
    {
        /// <summary>
        /// 一般的日志。
        /// </summary>
        Nomal = 0,
        /// <summary>
        /// 转换成功 。
        /// </summary>
        Success = 1,
        /// <summary>
        /// 转换失败。
        /// </summary>
        Fail = 2,
        /// <summary>
        /// 系统事件。
        /// </summary>
        System = 4,
        /// <summary>
        /// 备份事件。
        /// </summary>
        Backup = 8,
        /// <summary>
        /// 用于测试。
        /// </summary>
        Debug = 16,
        /// <summary>
        /// 未知。
        /// </summary>
        Unknown = -1
    }
}
