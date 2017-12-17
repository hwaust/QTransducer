using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QTrans.Classes;
using System.Data;
using QDAS;
using System.IO;
using QDasTransfer.Classes;

namespace QTrans.Company.Y2017
{
    public class point
    {
        public int col;
        public int row;

        public point(int row, int col)
        {
            this.col = col;
            this.row = row;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", row, col);
        }
    }

    public class T201705_Zhiqi_3D : TransferBase
    {
        public override void SetConfig(ParamaterData pd)
        {
            TransducerID = "201705";
            CompanyName = "智奇3D";
            VertionInfo = "1.0 beta";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".xls");

            ParamaterData.LoadParamater(common.config_folder + "\\" + TransducerID + ".xml");

            base.SetConfig(pd); 
        }

        public override bool TransferFile(string path)
        {
            FileInfo fi = new FileInfo(path);
            DateTime creationTime = fi.CreationTime;
            ExcelReader reader = new ExcelReader();

            DataTable[] tbs = reader.LoadSheetsFromExcel(path, MSOfficeVersion.Office2007);

            if (tbs.Length == 0)
            {
                AddLog(path, "Excel读取失败，原因：" + reader.ErrorMessage, LogType.Fail);
                return false;
            }

            DataTable dt = tbs[0];
            point k1001 = getCell(dt, "CNC - Control"); // K1001
            point k2101 = getCell(dt, "Value");  // 
            point k2112_2113 = getCell(dt, "Tolerance");
            point k0001 = getCell(dt, "Value measured");
            point pError = getCell(dt, "Error in");


            QData qd = new QData();
            List<QCharacteristic> chs = new List<QCharacteristic>();

            char[] XYZ = { 'X', 'Y', 'Z' };

            int firstdatarow = getFirstDataRow(dt);

            for (int row = firstdatarow; row < dt.Rows.Count; row++)
            {
                string K2001 = getCell(dt, row, 0);
                if (string.IsNullOrEmpty(K2001))
                    continue;

                QCharacteristic qc = new QCharacteristic();
                qc[2001] = K2001;
                qc[2004] = 0;
                qc[2008] = 2;
                chs.Add(qc);
                qd.items.Add(new QDataItem());

                for (int j = 0; j < 3; j++)
                {
                    QCharacteristic qsub = new QCharacteristic();
                    qsub[2001] = XYZ[j];
                    qsub[2002] = XYZ[j];
                    qsub[2004] = 0;
                    qsub[2008] = 0;
                    qsub[2101] = getCell(dt, row, k2101.col + j);
                    qsub[2112] = getCell(dt, row, k2112_2113.col + 0);
                    qsub[2113] = getCell(dt, row, k2112_2113.col + 1);

                    QDataItem qdi = new QDataItem();
                    qdi.SetValue(getCell(dt, row, k0001.col + j));
                    qdi.date = creationTime;
                    qd.items.Add(qdi);

                    chs.Add(qsub);
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("K0100 " + chs.Count);
            sb.AppendLine("K1001 " + getCell(dt, k1001.row + 1, k1001.col));

            // write charactaristics
            for (int i = 0; i < chs.Count; i++)
            {
                chs[i].id = i + 1;
                sb.AppendLine(chs[i].GetKString());
            }

            // write structural information
            sb.AppendLine("K5111/1 1");
            for (int i = 0; i < chs.Count; i++)
                sb.AppendLine(string.Format("K5112/{0} {1}", i + 2, i + 1));
            int count = chs.Count / 4;
            for (int i = 0; i < count; i++)
            {
                int start = i * 4 + 2;
                sb.AppendLine(string.Format("K5103/1 {0}", start));
                sb.AppendLine(string.Format("K5102/{0} {1}", start, start + 0));
                sb.AppendLine(string.Format("K5102/{0} {1}", start, start + 1));
                sb.AppendLine(string.Format("K5102/{0} {1}", start, start + 2));
            }

            // write actural values.
            sb.AppendLine(qd.GetKString());

            // save data.
            return Save(sb.ToString(), path);
        }

        private int getFirstDataRow(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string value = getCell(dt, i, 0).ToString().Trim();
                if (value == "Point")
                {
                    Console.WriteLine("start row: " + i);
                    return i + 3;
                }
            }

            return -1;
        }

        public bool Save(string content, string inpath)
        {
            string outpath = GetOutFolder(inpath, pd.OutputFolder) + "\\" + Path.GetFileNameWithoutExtension(CurrentFile) + ".dfq";
            string outputfilenew = processOutputFile(outpath);

            try
            {
                File.WriteAllText(outputfilenew, content);
                LastDfqFile = outputfilenew;
                AddSuccessedFile(outputfilenew);
                return true;
            }
            catch (Exception e1)
            {
                AddFailedFile(inpath, e1.Message);
            }

            return false;
        }

        private string getCell(DataTable dt, int row, int col)
        {
            return dt.Rows[row][col] == null ? null : dt.Rows[row][col].ToString().Trim();
        }

        private point getCell(DataTable tbl, string v)
        {
            for (int row = 0; row < tbl.Rows.Count; row++)
            {
                for (int col = 0; col < tbl.Columns.Count; col++)
                {
                    if (tbl.Rows[row][col].ToString() == v)
                    {
                        return new point(row, col);
                    }
                }
            }

            return new point(-1, -1);
        }
    }
}
