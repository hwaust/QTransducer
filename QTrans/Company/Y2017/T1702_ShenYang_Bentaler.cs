using QDAS;
using QTrans.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace QTrans.Company.Y2017
{
    public class T1702_ShenYang_Bentaler : TransferBase
    {
        string configfile = "BentelerStage.ini";

        double MinTorqueAngle = 1;
        double MaxTorqueAngle = 1000;//-----------用于首段曲线
        double MinTorqueValue = 0.5;//-------------用于末端曲线
        double MaxTorqueValue = 1.2;//-------------用于末端曲线


        Dictionary<String, double> dic = new Dictionary<string, double>();

        public override void SetConfig(ParamaterData pd)
        {
            CompanyName = "沈阳本特勒";
            VertionInfo = "1.0";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".txt");

            readConfig();

        }

        bool aya = false;
        public override bool TransferFile(string path)
        {
            if (aya)
            {
                Random rnd = new Random();
                Thread.Sleep(rnd.Next(6, 30) * 1000);
                AddLog(path, "数据结构不匹配。原因：数组索引超过给定范围。");
                return false;
            }

            /******************** Data preparation *************************/
            // current level
            JNode root = JNode.read(path);
            string k0016 = root.getNode("result");
            string k0055 = root.getNode("channel");
            DateTime date = DateTime.Parse(root.getNode("date"));
            string k0014 = root.getNode("id code");
            string k1007_1 = root.getNode("channel");
            string k1007_2 = root.getNode("last step row");
            string k1007_3 = root.getNode("last step column");

            string k2142 = root.getNode("torque unit"); // k2142   

            // next level  
            JNode node = root.get("tightening steps");
            node = (node.value as List<JNode>)[1];
            node = node.get("tightening functions");

            List<JNode> nodes = node.value as List<JNode>;
            List<JNode> ns = getNodes(nodes, "TF Torque");
            string k2101_1 = ns[1].value.ToString();
            string v1 = ns.Count > 2 ? ns[2].value.ToString() : null;

            ns = getNodes(nodes, "MF TorqueMin");
            string k2110_1 = ns[1].value.ToString();

            ns = getNodes(nodes, "MF TorqueMax");
            string k2111_1 = ns[1].value.ToString();

            ns = getNodes(nodes, "MF AngleMin");
            string k2110_2 = ns[1].value.ToString();
            string v2 = ns.Count > 2 ? ns[2].value.ToString() : null;

            ns = getNodes(nodes, "MF AngleMax");
            string k2111_2 = ns[1].value.ToString();


            // to get k1820
            node = root.get("tightening steps");
            node = (node.value as List<JNode>)[1];


            JNode node1 = node.get("angle threshold");
            string k1820 = node1.nodes.Count > 1 ? node1.nodes[1].value.ToString() : null;

            JNode node2 = node.get("graph").value as JNode;
            List<JNode> anglevalues = node2.nodes[0].value as List<JNode>;
            List<JNode> torquevalues = node2.nodes[1].value as List<JNode>;


            /******************** First DFQ: Data Result *************************/
            QFile qf = new QFile();
            if (k0014.Contains("_"))
                qf[1001] = k0014[0] + k0014.Split('_')[1];

            QCharacteristic q1 = new QCharacteristic();
            q1[2001] = "1";
            q1[2002] = "TorqueResult";
            q1[2101] = k2101_1;
            q1[2110] = k2110_1;
            q1[2111] = k2111_1;
            q1[2120] = 1;
            q1[2121] = 1;
            q1[2142] = k2142;

            QCharacteristic q2 = new QCharacteristic();
            q2[2001] = "2";
            q2[2002] = "TorqueResult";
            q2[2110] = k2110_2;
            q2[2111] = k2111_2;
            q2[2120] = 1;
            q2[2121] = 1;

            qf.Charactericstics.Add(q1);
            qf.Charactericstics.Add(q2);

            QDataItem qdi = new QDataItem();
            qdi[0014] = k0014;
            qdi[0016] = k0016;
            qdi[0055] = k0055;
            qdi.date = date;

            QDataItem di1 = qdi.Clone();
            di1.SetValue(v1);
            q1.data.Add(di1);

            QDataItem di2 = qdi.Clone();
            di2.SetValue(v2);
            q2.data.Add(di2);

            qf.ToDMode();

            string outfolder = GetOutFolder(path, pd.OutputFolder);
            string filename = Path.GetFileNameWithoutExtension(CurrentFile);
            string timestamp = DateTimeHelper.ToYYYYMMDDhhmmssString(DateTime.Now);
            string outpath_result = string.Format("{0}\\{1}_result_{2}.dfq", outfolder, filename, timestamp);
            string outpath_line = string.Format("{0}\\{1}_torque_{2}.dfq", outfolder, filename, timestamp);

            SaveDfq(qf, path, outpath_result);
            /******************** Second DFQ: Toque Result *************************/
            qf = new QFile();
            qf[1001] = k0014;
            qf[1004] = k0016;

            // K1007 数据获取特别说明：
            // 在JSON中是多个字段组合后的信息将“Channel”,”last steop row:”,”last step colum” 
            // 组合而成，字段间用“_”连接，例如：1_2_3A。
            // 无值留空，如 _2_3A，1007，1800，1810同。
            qf[1007] = k1007_1 + "_" + k1007_2 + "_" + k1007_3;
            // K1800 数据获取特别说明：
            // 在JSON中是多个字段组合后的信息将“TF Torque,”,” MF TorqueMax,”,” MF TorqueMin,”，” act:”组合而成，
            // 字段间用“_”连接，例如：'38.00_32.30_43.70_38.45(名义值_最大值_最小值_实际值)。
            qf[1800] = k2101_1 + "_" + k2111_1 + "_" + k2110_1 + "_" + v1;

            // K1810 数据获取特别说明：
            // 在JSON中是多个字段组合后的信息将“MF AngleMax,”,” MF AngleMin,”, ” act:”组合而成，
            // 字段间用“_”连接，例如：'32.30_43.70_38.45(最大值_最小值_实际值)。
            qf[1810] = k2111_2 + "_" + k2110_2 + "_" + v2;

            q1 = new QCharacteristic();
            qf.Charactericstics.Add(q1);
            q1[2001] = "1";
            q1[2002] = "TorqueCurveValue";
            q1[2101] = k2101_1;
            q1[2110] = k2110_1;
            q1[2111] = k2111_1;
            q1[2120] = 1;
            q1[2121] = 1;
            q1[2142] = k2142;
            q1[2022] = "2";

            qdi = new QDataItem();
            qdi[0016] = k0014;
            qdi[0016] = k0016;
            qdi[0055] = k0055;
            qdi.date = date;

            for (int i = 0; i < anglevalues.Count; i++)
            {
                QDataItem di = qdi.Clone();
                di.SetValue(torquevalues[i].value);
                di[0054] = anglevalues[i].value;
                q1.data.Add(di);

                //if (MinTorqueValue <= di.value && di.value <= MaxTorqueValue)
                //    di[0056] = "S1";
                //else if (MinTorqueAngle <= di.value && di.value <= MaxTorqueAngle)
                //    di[0056] = "SE"; 
            }

            qf.ToDMode();

            SaveDfq(qf, path, outpath_line);

            return true;
        }



        void readConfig()
        {
            if (!File.Exists(configfile))
            {
                using (StreamWriter sw = new StreamWriter(configfile))
                {
                    sw.WriteLine("MinTorqueAngle: 1");
                    sw.WriteLine("MaxTorqueAngle: 1000");
                    sw.WriteLine("MinTorqueValue: 0.5");
                    sw.WriteLine("MaxTorqueValue: 1.2");
                }
            }

            string[] lines = File.ReadAllLines(configfile);
            dic.Clear();
            foreach (string line in lines)
            {
                string[] ss = line.Split(':');
                dic.Add(ss[0].Trim(), double.Parse(ss[1]));
            }

            try
            {
                MinTorqueAngle = dic["MinTorqueAngle"];
                MaxTorqueAngle = dic["MaxTorqueAngle"];
                MinTorqueValue = dic["MinTorqueValue"];
                MaxTorqueValue = dic["MaxTorqueValue"];
            }
            catch (Exception ex)
            {
                AddLog("BentelerStage", "格式不正确，参数读取失败。原因：" + ex.Message);
            }

        }

        private List<JNode> getNodes(List<JNode> nodes, string v)
        {
            List<JNode> ns = new List<JNode>();

            for (int i = 0; i < nodes.Count; i++)
            {
                ns = nodes[i].nodes;// as List<JNode>;
                if (ns[0].value.ToString() == v)
                    return ns;
            }

            return ns;
        }

    }
}
