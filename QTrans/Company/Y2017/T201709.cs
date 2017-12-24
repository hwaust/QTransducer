using QDAS;
using QDasTransfer.Classes;
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
            CompanyName = "北京安泰密度转换器";
            VertionInfo = "1.0 alpha";
            pd.SupportAutoTransducer = true;
            pd.extentions.Clear();
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
            ExcelReader reader = new ExcelReader(infile, MSOfficeVersion.Office2007);
             
            string K1900 = reader.getData("B5");//
            string K1041 = reader.getData("B8");//
            string K0008 = reader.getData("B11");
            double days = double.Parse(reader.getData("D5"));
            double time = double.Parse(reader.getData("D8"));
            DateTime K0004 = new DateTime(1900, 1, 1).AddDays(-2).AddDays(days + time);
            string K0012 = reader.getData("D11");
            string K1001 = reader.getData("F8");//
            string K0014 = reader.getData("F11");



            QCatalog qlog = getCatlog();

            int startRow = 13;
            int endRow = reader.getRowCount();
            // A: char, B: Actual, C: Nominal, D: Upper Tol;  E: Lower Tol; F: Deviation

            QFile qf = new QFile();
            qf[1001] = K1001;
            qf[1041] = K1041;
            qf[1900] = K1900;

            for (int i = startRow; i < endRow; i++)
            {
                // Characteristic	Actual	Nominal	Upper Tol	Lower Tol	Deviation
                // K2002, K0001, K2101, K2113, K2112 
                string K2002 = reader.getData(i, 0);
                string K0001 = reader.getData(i, 1);
                string K2101 = reader.getData(i, 2);
                string K2113 = reader.getData(i, 3);
                string K2112 = reader.getData(i, 4);

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
