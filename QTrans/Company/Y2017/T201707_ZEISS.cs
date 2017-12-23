using QDAS;
using QDasTransfer.Classes;
using QTrans.Classes;
using QTrans.Helpers;
using System;
using System.IO;

namespace QTrans.Company.Y2017
{
    public class T201707_ZEISS : TransferBase
    { 
        int K4092 = -1;
        int K4062 = -1;
        int K4072 = -1;
        int K4272 = -1;
        QCatalog clog = null;

        public override void Initialize()
        { 
            CompanyName = "ZEISS";
            VertionInfo = "1.0 alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".xls");
        }

        public override bool TransferFile(string infile)
        {
            // string input = @"Z:\Projects\QTransducer\2017年\2017_07_Zeiss\sample.xls";
            ExcelReader reader = new ExcelReader(infile);

            double days = double.Parse(reader.getData("D5"));
            double time = double.Parse(reader.getData("F5"));
            DateTime dt = new DateTime(1900, 1, 1).AddDays(-2).AddDays(days + time);
            string allinfo = reader.getData("B8");
            string K1001 = reader.getData("F8");
            string K1086 = allinfo.Split('_')[1];
            string K0006 = reader.getData("D8");
            string K0008 = reader.getData("B11");
            string K0010 = reader.getData("F11");
            string K0012 = reader.getData("D11");
            string K0014 = allinfo.Split('_')[0];
            string K0061 = allinfo.Split('_')[2];


            if (clog != null)
            {
                K4092 = clog.getCatalogPID("K4092", K0008);
                K4062 = clog.getCatalogPID("K4062", K0010);
                K4072 = clog.getCatalogPID("K4072", K0012);
                K4272 = clog.getCatalogPID("K4272", K0061);
            }

            QDataItem qdi = new QDataItem();
            qdi[0006] = K0006;

            if (K4092 >= 0)
                qdi[0008] = K4092;

            if (K4062 >= 0)
                qdi[0010] = K4062;

            if (K4072 >= 0)
                qdi[0012] = K4072;

            qdi[0014] = K0014;

            if (K4272 >= 0)
                qdi[0061] = K4272;

            // The first row of data. If it is not fixed, modify this variable.
            int startrow = 13;

            QFile qf = new QFile();


            for (int i = startrow; i < reader.getRowCount(); i++)
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

                QDataItem di = qdi.Clone();
                di.SetValue(K0001);
                qc.data.Add(di);

                qf.Charactericstics.Add(qc);
            }

            qf.ToDMode();
              
            return SaveDfq(qf, string.Format("{0}\\{1}_{2}.dfq", 
                pd.GetOutDirectory(infile), // output directory
                allinfo,   // filename from all info.
                DateTimeHelper.ToFullString(DateTime.Now))); // time stamp.
        }
    }
}
