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


    }
}
