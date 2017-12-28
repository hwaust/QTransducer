using QDAS;
using QDasTransfer.Classes;
using QTrans.Classes;
using QTrans.Excel;
using QTrans.Helpers;
using System;
using System.IO;

namespace QTrans.Company.Y2017
{
    public class T201707_ZEISS : TransferBase
    {  
        public override void Initialize()
        { 
            CompanyName = "ZEISS";
            VertionInfo = "1.0.1";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".xls");
        }

        public override bool TransferFile(string infile)
        {
            QCatalog qlog = QCatalog.GetCatlog();
            COMReader reader = new COMReader(infile);

            double days = double.Parse(reader.GetCell("D5"));
            double time = double.Parse(reader.GetCell("F5"));
            DateTime dt = new DateTime(1900, 1, 1).AddDays(-2).AddDays(days + time);
            string allinfo = reader.GetCell("B8");
            string K1001 = reader.GetCell("F8");
            string K1086 = allinfo.Split('_')[1];
            string K0006 = reader.GetCell("D8");
            string K0008 = reader.GetCell("B11");
            string K0010 = reader.GetCell("F11");
            string K0012 = reader.GetCell("D11");
            string K0014 = allinfo.Split('_')[0];
            string K0061 = allinfo.Split('_')[2]; 

            QDataItem qdi = new QDataItem();
            qdi[0006] = K0006;
            qdi[0008] = qlog.GetCatalogPIDString("K4092", K0008);
            qdi[0010] = qlog.GetCatalogPIDString("K4062", K0010);
            qdi[0012] = qlog.GetCatalogPIDString("K4072", K0012);
            qdi[K0061] = qlog.GetCatalogPIDString("K4272", K0061); 
            qdi[0014] = K0014; 

            // The first row of data. If it is not fixed, modify this variable.
            int startrow = 13; 
            QFile qf = new QFile();
            qf[1001] = K1001;
            qf[1086] = K1086;
           

            for (int i = startrow; i < reader.GetRowCount(); i++)
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
