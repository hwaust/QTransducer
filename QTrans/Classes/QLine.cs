
using System.Collections.Generic;
using System.IO;

namespace QTrans.Classes
{
    public class QLine
    {
        public string key;
        public string id;
        public string value;


        public override string ToString()
        {
            return string.Format("{0}/{1} {2}", key, id, value);
        }


        public static List<QLine> read(string file)
        {
            string[] lines = File.ReadAllLines(file);
            List<QLine> qlines = new List<QLine>();
            for (int i = 0; i < lines.Length; i++)
            {
                // QLineInfo.Parse(lines[i]);
                QLine qinfo = parse(lines[i]);
                if (qinfo != null)
                    qlines.Add(qinfo);

            }

            return qlines;
        }

        public static QLine parse(string line)
        {
            try
            {
                if (line == null || line.Trim() == "")
                    return null;

                int p = line.IndexOf(' ');
                QLine qline = new QLine();

                qline.key = line.Substring(0, p).Split('/')[0].ToUpper().Trim();
                qline.id = line.Substring(0, p).Split('/')[1].Trim();
                qline.value = line.Substring(p + 1).Trim();

                return qline;
            }
            catch { }

            return null;
        }
    }


}
