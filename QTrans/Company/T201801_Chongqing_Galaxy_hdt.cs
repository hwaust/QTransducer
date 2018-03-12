using QDAS;
using QTrans.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTrans.Company
{
    public class T201801_Chongqing_Galaxy_hdt : TransferBase
    {
        string[] boxtypeNames = { "湿热试验箱", "低气压试验箱", "两箱温度冲击箱　", "三箱温度冲击试验箱", "光照试验箱", "温度试验箱", "低气压湿热试验箱" };
        string[][] testtypeNames;
        string[] k2002s = { "温度检测值", "湿度检测值", "箱壁温度检测值", "箱内压力检测值", "温度设定值", "湿度设定值", "箱壁温度设定值", "箱内压力设定值" }; // table2
        
        int[] boxtypes = { 1, -1, -1, -1, -1, 3, 2 };

        public override void Initialize()
        {
            base.Initialize();
            CompanyName = "重庆银河 转换器";
            VertionInfo = "1.0 alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".hdt");
            testtypeNames = new string[7][];
            testtypeNames[0] = new string[] { "恒温试验", "温度程序试验", "恒定湿热试验", "湿热程序试验" };
            testtypeNames[1] = new string[] { "恒温试验", "温度程序试验", "未使用", "未使用", "温度气压试验", "恒定气压试验", "气压交变试验" };
            testtypeNames[2] = new string[] { "冲击试验", "高温试验", "低温试验", "双温试验" };
            testtypeNames[3] = new string[] { "冲击试验", "高温试验", "低温试验" };
            testtypeNames[4] = new string[] { "定值试验", "程序试验" };
            testtypeNames[5] = new string[] { "温度定值试验", "温度程序试验" };
            testtypeNames[6] = new string[] { "恒温试验", "温度程序试验", "恒定湿热试验", "湿热程序试验", "温度气压试验", "恒定气压试验", "气压交变试验" };
        }


        public override bool TransferFile(string path)
        {
            // input file -> a list of string arrays
            // path = @"c:\\data\\test.hdt";
            string[] lines = File.ReadAllLines(path, Encoding.Default);
            List<string[]> list = new List<string[]>();
            foreach (string s in lines)
            {
                list.Add(s.Split('\t'));
            }

            string K1204 = list[0][0].Split('=')[1];
            string K0012_1202 = list[1][0].Split('=')[1];
            string K2202 = list[2][0].Split('=')[1];

            int boxtype = int.Parse(K0012_1202);
            int testtype = int.Parse(K2202);

            QFile qf = new QFile();
            qf[1202] = boxtypeNames[boxtype];
            qf[1204] = K1204;

            // cpv0 -- cpv3 and cspv0 -- cspv3
            for (int i = 0; i < 8; i++)
            {
                QCharacteristic qc = new QCharacteristic();
                qc[2001] = i + 1;
                qc[2002] = testtypeNames[boxtype][testtype] + " " + k2002s[i];
                qc[2022] = 3;
                qc[8500] = 5;
                qc[8501] = 0;
                qf.Charactericstics.Add(qc);
            }

            // 有两种行要去掉。分别是以分号开头和以\0开头的。
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i][0].StartsWith(";") || list[i][0].StartsWith("\0"))
                    list.RemoveAt(i);
            }


            for (int i = 0; i < list.Count; i++)
            {
                string[] ss = list[i];
                DateTime dt = DateTime.Parse(ss[0]);

                for (int j = 0; j < 8; j++)
                {
                    QDataItem qdi = new QDataItem();
                    qdi["K0012"] = boxtypes[boxtype];
                    qdi.date = dt;
                    qdi.SetValue(ss[j + 1]);
                    qf.Charactericstics[j].data.Add(qdi);
                }
            }
            qf.ToDMode();

            return SaveDfq(qf, string.Format("{0}\\yinhe_{1}.dfq",
                    pd.GetOutDirectory(path), // output directory 
                    DateTimeHelper.ToFullString(DateTime.Now))); // time stamp. 
        }
    }
}
