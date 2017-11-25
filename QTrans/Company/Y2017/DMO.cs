using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QTrans.Company.Y2017
{
    public class DMO
    {
        public DateTime datetime = new DateTime(2000, 1, 1);
        public List<DmoSection> sections = new List<DmoSection>();
        public string K0001_SachNr;
        public string K0002_Benennung;
        public DMOType fileType = DMOType.UNKNOWN;

        private string Sach_Nr_Teil = "Sach-Nr. Teil";
        private string Benennung_Teil = "Benennung Teil";

        public void read(string path)
        {
            string[] lines = File.ReadAllLines(path);
            DmoSection sec = new DmoSection();
            bool istextoutfile = false;
            string date = "";
            datetime = new DateTime(2000, 1, 1);
            fileType = DMOType.UNKNOWN;
            sections.Clear();
            K0001_SachNr = null;
            K0002_Benennung = null;


            for (int i = 0; i < lines.Length; i++)
            {
                string tmp = lines[i].TrimStart();
                if (tmp.StartsWith("ENDFIL"))
                    break;

                if (tmp.StartsWith("$$"))
                {
                    if (fileType == DMOType.UNKNOWN)
                    {
                        if (tmp.Contains("Metrosoft CM"))
                            fileType = DMOType.CM;
                        else if (tmp.Contains("Metrosoft QUARTIS"))
                            fileType = DMOType.QUARTIS;
                    }

                    continue;
                }

                // remarks
                if (tmp.StartsWith("FILNAM")
                    || tmp.StartsWith("OUTPUT/R")
                    || tmp.StartsWith("RECALL")
                    || tmp.StartsWith("LAENGSTRAEGER"))
                    continue;

                if (tmp.StartsWith("DATE"))
                {
                    if (datetime.Year == 2000)
                        date += " " + tmp.Split('=')[1].Trim();
                    continue;
                }

                if (tmp.StartsWith("TIME"))
                {
                    if (datetime.Year == 2000)
                    {
                        date += " " + tmp.Split('=')[1].Trim();
                        datetime = DateTime.Parse(date);
                        //  Console.WriteLine(datetime);
                    }
                    continue;
                }

                // text / out file
                if (tmp.StartsWith("TEXT/OUTFIL"))
                {
                    if (K0001_SachNr == null && tmp.Contains(Sach_Nr_Teil))
                        K0001_SachNr = tmp.Substring(tmp.IndexOf(Sach_Nr_Teil) + Sach_Nr_Teil.Length + 1).Trim(' ', '\'');

                    if (K0002_Benennung == null && tmp.Contains(Benennung_Teil))
                        K0002_Benennung = tmp.Substring(tmp.IndexOf(Benennung_Teil) + Benennung_Teil.Length + 1).Trim(' ', '\'');

                    if (!istextoutfile)
                    {
                        if (sections.Count > 0 && sections.Last().empty()) // remove empty sections
                            sections.RemoveAt(sections.Count - 1);

                        sec = new DmoSection();
                        sections.Add(sec);
                        istextoutfile = true;
                    }
                    continue;
                }

                istextoutfile = false;


                sec.add(tmp);
            }

            try { datetime = DateTime.Parse(date); } catch { }
            foreach (DmoSection section in sections)
                section.format();

            for (int i = sections.Count - 1; i >= 0; i--)
            {
                if (sections[i].size() < 3)
                {
                    Console.WriteLine(sections[i].list[0]);
                    sections.RemoveAt(i);
                    continue;
                }
                string type = sections[i].getType();
                if (type == "PLANE" || type == "LINE")
                    sections.RemoveAt(i);

            }

            Console.WriteLine();

        }

        public static string processK2001(string str)
        {
            //  格式：首字母后两位_首字母和4到7位 (如果4位前几位包含“0”，省略“0”只取数值 “0034”只取 “34”)_剩下最后字母1位或2位
            // 例如：FA（L857188a） 转换为 85_L7188_a
            string s2 = str.Substring(1, 2);
            string s4 = int.Parse(str.Substring(3, 4)).ToString();
            return s2 + "_" + str[0] + s4 + "_" + str.Substring(7);
        }
    }
}

