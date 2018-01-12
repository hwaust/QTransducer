using QDAS;
using QDasTransfer.Classes;
using QTrans.Classes;
using QTrans.Excel;
using QTrans.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
1. K2110 和 K2111的分隔符有问题。
2. M列的值有无效的，如：
3. 文件名无效，如A9246:  216751_螺检3/809"
*/

namespace QTrans.Company.Y2017
{
    public class T201708_Antai_Density : TransferBase
    {
        public override void Initialize()
        {
            CompanyName = "北京安泰密度转换器";
            VertionInfo = "1.0 alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".xlsx");
        }

        public QCatalog getCatlog()
        {
            QCatalog qlog = null;
            string logfile = ".\\catlog.dfd";
            if (File.Exists(logfile))
            {
                qlog = QCatalog.load(logfile);
            }
            else
            {
                WindGoes.IO.IniAccess ia = new WindGoes.IO.IniAccess(".\\userconfig.ini");
                logfile = ia.ReadValue("catalog");
                qlog = File.Exists(logfile) ? QCatalog.load(logfile) : new QCatalog();
            }
            return qlog;
        }

        public override bool TransferFile(string infile)
        {
            COMReader reader = new COMReader(infile, ExcelVersion.Excel2003);
            string K0012 = reader.GetCell("G3");
            QCatalog qlog = getCatlog();
            List<QFile> qfs = new List<QFile>();

            DateTime date = new DateTime(1900, 1, 1);
            for (int row = 5; row < reader.GetRowCount(); row++)
            {
                try
                {
                    DateTime dt = date.AddDays(int.Parse(reader.GetCell("A" + row)) - 2);
                    string K1002 = reader.GetCell("B" + row);
                    string K0014 = reader.GetCell("C" + row);
                    string K0016 = reader.GetCell("D" + row);
                    string K0010 = reader.GetCell("E" + row);
                    string K1001 = reader.GetCell("F" + row);
                    string K0011 = reader.GetCell("I" + row) + '\\' + reader.GetCell("J" + row) + '\\' + reader.GetCell("K" + row) + '\\' + reader.GetCell("L" + row);
                    string K0001 = reader.GetCell("M" + row);

                    double value = double.Parse(K0001);
                    
                    string K2110 = "";
                    string K2111 = "";
                    string K2120 = "";

                    string colN = reader.GetCell(row, "N");
                    // 上下限写入同一单元格并用 "-"间隔，即：下限-上限。
                    // 将中横杠前的数据写入K2110，中横杠后的数据写入K2111。
                    if (colN.Contains("-"))
                    {
                        K2110 = colN.Split('-')[0];
                        K2111 = colN.Split('-')[1];
                    }
                    // 1、当密度要求值出现中横杠"_"时，代表该行信息不需要输出 
                    else if (colN.Contains("_"))
                    {

                    }
                    // 2、当密度要求值出现中横杠”≥”时,该参数只有下限K2110，
                    // 例如≥30，输出K2110 = 30，K2111为空，K2120 = 1
                    else if (colN.Contains("≥"))
                    {
                        K2110 = colN.Split('≥')[1];
                        K2120 = "1";
                    }
                    // 3、当密度要求值出现中横杠” >”时，该参数只有下限，且为下自然界线。
                    // 例如 > 30，输出K2110 = 30，K2111为空，K2120 = 2
                    else if (colN.Contains("＞"))
                    {
                        K2110 = colN.Split('＞')[1];
                        K2120 = "2";
                    }

                    string K0008 = reader.GetCell("Q" + row);

                    if (K1001 == "" && K1002 == "")
                    {
                        LogList.Add(new TransLog(infile, "", "K1001 和 K1002 均为空。", LogType.Fail));
                        break;
                    }

                    QFile qf = getQFile(qfs, K1001, K1002);

                    if (qf.Charactericstics.Count == 0)
                    {
                        QCharacteristic qc = new QCharacteristic();
                        qf.Charactericstics.Add(qc);
                        qc[2002] = "密度g/cm³";
                        qc[2022] = 8;
                        if (K2110.Length > 0)
                            qc[2110] = K2110;
                        if (K2111.Length > 0)
                            qc[2111] = K2111;
                        qc[2120] = 1;
                        qc[2121] = 1;
                        if (K2120.Length > 0)
                            qc[2120] = K2120;
                        qc[2142] = "g/cm³";
                        qc[8500] = 5;
                        qc[8501] = 0;
                    }

                    QDataItem qdi = new QDataItem { date = dt };
                    qdi.SetValue(K0001);
                    qdi[0008] = qlog.getCatalogPID("K4093", K0008);
                    qdi[0010] = qlog.getCatalogPID("K4063", K0010);
                    qdi[0011] = K0011;
                    qdi[0012] = qlog.getCatalogPID("K4073", K0012);
                    qdi[0014] = K0014;
                    qdi[0016] = K0016;
                    qf.Charactericstics[0].data.Add(qdi);

                    // Console.WriteLine("K0008({0})->K4093={1}", K0008, qlog.getCatalogPID("K4093", K0008));
                }
                catch (Exception ex)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Row " + row + ": ");
                    for (int col = 0; col < reader.GetColumnCount(); col++)
                    {
                        sb.Append(reader.GetCell(row, col) + ", ");
                    }
                    Console.WriteLine(sb.ToString() + ". Error message: " + ex.Message);
                }
            }

            Console.WriteLine("Begin output...");
            string outdir = pd.GetOutDirectory(infile);
            foreach (QFile qf in qfs)
            {
                qf.ToDMode();
                string outfile = outdir + "\\" + qf[1001] + "_" + qf[1002] + "_" + DateTimeHelper.ToFullString(DateTime.Now) + ".dfq";
                bool done = SaveDfq(qf, outfile);
                LogList.Add(new TransLog(infile, outfile, done ? "转换成功" : "转换失败。", done ? LogType.Success : LogType.Fail));
            }
            Console.WriteLine("Complete output...");
            return true;
        }

        private QFile getQFile(List<QFile> qfs, string k1001, string k1002)
        {
            for (int i = 0; i < qfs.Count; i++)
            {
                if (qfs[i][1001].ToString() == k1001 && qfs[i][1002].ToString() == k1002)
                    return qfs[i];
            }
            QFile qf = new QFile();
            qfs.Add(qf);
            qf[1001] = k1001;
            qf[1002] = k1002;

            return qf;
        }
    }
}
