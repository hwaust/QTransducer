using QDAS;
using QDasTransfer.Classes;
using QTrans.Classes;
using QTrans.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

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
            ExcelReader reader = new ExcelReader(infile, MSOfficeVersion.Office2007);
            string K0012 = reader.getData(3, "G");
            QCatalog qlog = getCatlog();
            List<QFile> qfs = new List<QFile>();

            for (int i = 4; i < reader.getRowCount(); i++)
            {
                try
                {
                    DateTime dt = DateTime.Parse(reader.getData(i, 'A' - 65));
                    string K1002 = reader.getData(i, "B");
                    string K0014 = reader.getData(i, "C");
                    string K0016 = reader.getData(i, "D");
                    string K0010 = reader.getData(i, "E");
                    string K1001 = reader.getData(i, "F");
                    string K0011 = reader.getData(i, "I") + '\\' + reader.getData(i, "J") + '\\' + reader.getData(i, "K") + '\\' + reader.getData(i, "L");
                    string K0001 = reader.getData(i, "M");
                    string K2112 = reader.getData(i, "N").Split(new char[] { '-', '_' })[0];
                    string K2113 = reader.getData(i, "N").Split(new char[] { '-', '_' })[1];
                    string K0008 = reader.getData(i, "Q");

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
                        qc[2112] = K2112;
                        qc[2113] = K2113;
                        qc[2120] = 1;
                        qc[2121] = 1;
                        qc[2142] = "g/cm³";
                        qc[8500] = 5;
                        qc[8501] = 0;
                    }

                    QDataItem qdi = new QDataItem
                    {
                        date = dt
                    };
                    qdi.SetValue(K0001);
                    qdi[0008] = qlog.getCatalogPID("K4093", K0008);
                    qdi[0010] = qlog.getCatalogPID("K4063", K0010);
                    qdi[0011] = K0011;
                    qdi[0012] = qlog.getCatalogPID("K4073", K0012);
                    qdi[0014] = K0014;
                    qdi[0016] = K0016;
                    qf.Charactericstics[0].data.Add(qdi);

                    Console.WriteLine("K0008({0})->K4093={1}", K0008, qlog.getCatalogPID("K4093", K0008));
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

            string outdir = pd.GetOutDirectory(infile);
            foreach (QFile qf in qfs)
            {
                qf.ToDMode();
                string outfile = outdir + "\\" + qf[1001] + "_" + qf[1002] + "_" + DateTimeHelper.ToFullString(DateTime.Now) + ".dfq";
                bool done = SaveDfq(qf, outfile);
                LogList.Add(new TransLog(infile, outfile, done ? "转换成功" : "转换失败。", done ? LogType.Success : LogType.Fail));
            }

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
