using QDAS;
using QDasTransfer.Classes;
using QTrans.Excel;
using QTrans.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTrans.Company.Y2017
{
    public class T201709 : TransferBase
    {
        public override void Initialize()
        {
            base.Initialize();
            CompanyName = "北京安泰Zeiss转换器";
            VertionInfo = "1.0 alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".xls");
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
            COMReader reader = new COMReader(infile);

            string K1900 = reader.GetCell("B5");//
            string K1041 = reader.GetCell("B8");//
            string K0008 = reader.GetCell("B11");
            double days = double.Parse(reader.GetCell("D5"));
            double time = double.Parse(reader.GetCell("D8"));
            DateTime K0004 = new DateTime(1900, 1, 1).AddDays(-2).AddDays(days + time);
            string K0012 = reader.GetCell("D11");
            string K1001 = reader.GetCell("F8");//
            string K0014 = reader.GetCell("F11"); 

            QCatalog qlog = getCatlog();

            int startRow = 13;
            int endRow = reader.GetRowCount();
            // A: char, B: Actual, C: Nominal, D: Upper Tol;  E: Lower Tol; F: Deviation

            QFile qf = new QFile();
            qf[1001] = K1001;
            qf[1041] = K1041;
            qf[1900] = K1900;

            for (int i = startRow; i < endRow; i++)
            {
                // Characteristic	Actual	Nominal	Upper Tol	Lower Tol	Deviation
                // K2002, K0001, K2101, K2113, K2112 
                string K2002 = reader.GetCell(i, 0);
                string K0001 = reader.GetCell(i, 1);
                string K2101 = reader.GetCell(i, 2);
                string K2113 = reader.GetCell(i, 3);
                string K2112 = reader.GetCell(i, 4);

                QCharacteristic qc = new QCharacteristic();
                qc[2001] = qf.Charactericstics.Count + 1;
                qc[2002] = K2002;
                qc[2022] = "8";
                qc[2101] = K2101;
                qc[2112] = K2112;
                qc[2113] = K2113;
                qc[2120] = 1;
                qc[2121] = 1;
                qc[2142] = "mm";
                qc[8500] = 5;
                qc[8501] = 0;

                QDataItem di = new QDataItem();
                di.date = K0004;
                di.SetValue(K0001);
                di[0008] = qlog.getCatalogPID("K4093", K0008);
                di[0012] = qlog.getCatalogPID("K4073", K0012);
                di[0014] = K0014;
                qc.data.Add(di);

                qf.Charactericstics.Add(qc);
            }

            qf.ToDMode();

            return SaveDfq(qf, string.Format("{0}\\{1}_{2}.dfq",
                                            pd.GetOutDirectory(infile), // output directory
                                            K1001,   // filename from all info.
                                            DateTimeHelper.ToFullString(DateTime.Now))); // time stamp. 
        }
    }
}
