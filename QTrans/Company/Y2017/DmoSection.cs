using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QTrans.Company.Y2017
{
    public class DmoSection
    {
        public List<string> list = new List<string>();

        public void reset()
        {
            list = new List<string>();
        }

        public void add(string s)
        {
            list.Add(s);
        }

        public void show()
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
        }

        public int size()
        {
            return list.Count;
        }

        public int groupsize()
        {
            return (list.Count + 1) / 6;
        }

        public bool empty()
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Trim().Length > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public string getK2002(int pos = 0)
        {
            // OUTPUT/F(L857188a),T(ZL857188)
            return list[pos * 6].Split(',')[1].Split('(')[1][0].ToString();
        }

        public string[] getAllAxes()
        {
            // input: OUTPUT/FA(L851015a),TA(XL851015),TA(YL851015),TA(ZL851015)
            // output: L851015a, XL851015, YL851015, ZL851015
            string[] axes = list[0].Split(',');
            for (int i = 0; i < axes.Length; i++)
                axes[i] = axes[i].Split('(')[1].Trim(')');

            return axes;
        }

        internal string[] getActualXYZ_Quartis()
        {
            string[] strs = list[1].Split(',');
            string[] xyz = new string[3];

            int p = 1;
            while (!isFLoatNumber(strs[p]))
                p++;

            for (int i = 0; i < xyz.Length; i++)
                xyz[i] = strs[p + i];

            return xyz;
        }

        private bool isFLoatNumber(string v)
        {
            if (v == null)
                return false;

            string value = v.Trim();
            if (value.Length == 0)
                return false;

            int p = value[0] == '-' ? 1 : 0;

            for (int i = p; i < value.Length; i++)
            {
                if (!char.IsDigit(value[i]) && value[i] != '.')
                    return false;
            }

            return true;
        }

        public string[] getTxyz(int pos)
        {

            string[] strs = list[6 * pos + 1].Split(',');
            string[] xyz = new string[3];


            int p = 0;
            while (!isFLoatNumber(strs[p]))
                p++;

            for (int i = 0; i < xyz.Length; i++)
                xyz[i] = strs[p + i];

            return xyz;
        }

        // getActualXYZ
        public string[] getAxyz(int pos)
        {
            string[] strs = list[6 * pos + 4].Split(',');
            string[] xyz = new string[3];
            int p = 0;
            while (!isFLoatNumber(strs[p]))
                p++;

            for (int i = 0; i < xyz.Length; i++)
                xyz[i] = strs[p + i];

            return xyz;
        }

        public string[] getK2113_K2112(int pos)
        {
            string[] k2113_k2112 = new string[2];
            int gs = groupsize();
            for (int i = 0; i < gs; i++)
            {
                if (getK2002(i) != "P")
                    continue;
                string[] strs = list[6 * i + 2].Split(',');
                k2113_k2112[0] = strs[strs.Length - 2];
                k2113_k2112[1] = strs[strs.Length - 1]; 
            } 

            if(string.IsNullOrEmpty(k2113_k2112[0]) && string.IsNullOrEmpty(k2113_k2112[1]))
            {
                string[] strs = list[6 * pos+ 2].Split(',');
                k2113_k2112[0] = strs[strs.Length - 2];
                k2113_k2112[1] = strs[strs.Length - 1];
            }

            return k2113_k2112;
        }

        public void format()
        {
            // 1) join a line ends with '$' with the following line.
            // 2) remove empty lines.
            int pos = 0;
            while (pos < list.Count)
            {
                if (list[pos].Trim().Length == 0)
                    list.RemoveAt(pos);
                else if (list[pos].EndsWith("$") && pos < list.Count - 1)
                {
                    list[pos] = list[pos].Trim(' ', '$') + list[pos + 1].Trim();
                    list.RemoveAt(pos + 1);
                }
                else
                    pos++;
            }

        }

        public string getType()
        {
            return list[1].Split(',')[0].Split('=')[1].Split('/')[1];
        }

        public string getK2001()
        {
            string s1 = list[0].Split(',')[0].Split('(')[1].Trim(')');
            return DMO.processK2001(s1);
        }
    }
}
