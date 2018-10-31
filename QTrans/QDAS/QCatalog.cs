using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAS
{
    public class QCatalog
    {
        public List<QLineInfo> qlines = new List<QLineInfo>();

        public static QCatalog load(string file)
        {
            if (!File.Exists(file))
            {
                return null;
            }

            try
            {
                QCatalog qc = new QCatalog();
                qc.qlines = QLineInfo.read(file);
                return qc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// 根据指定的key （例如 “K4092”）分析输入的值（value）对应的索引值。 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int getCatalogPID(string key, string value)
        {
            foreach (QLineInfo qline in qlines)
            {
                if (qline.key == key && qline.value == value)
                {
                    return qline.pid;
                }
            }

            return -1;
        }

        public string GetCatalogPIDString(string key, string value)
        {
            foreach (QLineInfo qline in qlines)
            {
                if (qline.key == key && qline.value == value)
                {
                    return qline.pid.ToString();
                }
            }

            return "";
        }

        /// <summary>
        /// 默认从当前程序目录下读取catlog.dfd。
        /// 如果不存在，则从同目录下读取配置文件userconfig.ini的others.catlog字段。
        /// 此字段用于指示Catalog文件的位置。
        /// </summary>
        /// <param name="catlogfile">缺省的catalog文件。</param>
        /// <param name="configfile">配置文件。</param>
        /// <returns></returns>
        public static QCatalog GetCatlog(string catlogfile = ".\\catalog.dfd", string configfile = ".\\userconfig.ini")
        {
            QCatalog qlog = null;
            if (File.Exists(catlogfile))
            {
                qlog = QCatalog.load(catlogfile);
            }
            else
            {
                WindGoes.IO.IniAccess ia = new WindGoes.IO.IniAccess(configfile);
                catlogfile = ia.ReadValue("catalog");
                qlog = File.Exists(catlogfile) ? QCatalog.load(catlogfile) : new QCatalog();
            }
            return qlog;
        }

    }
}
