using Excel;
using QDAS;
using QDasTransfer.Classes;
using QTrans.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using WindGoes.IO;

namespace QTrans.Company.Y2017
{
    public class T201707_ZEISS : TransferBase
    {
        string config = ".\\userconfig.ini";
       

        public override void SetConfig(ParamaterData pd)
        {
            TransducerID = "201707";
            CompanyName = "ZEISS";
            VertionInfo = "1.0 alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".xls"); 

            IniAccess ia = new IniAccess(config);
            string catlog = ia.ReadValue("catalog");

            if (File.Exists(catlog))
            {

            }
            else
            {
                Console.WriteLine("catlog doex not exist.");
            }


            base.SetConfig(pd);
        }

        public override bool TransferFile(string path)
        {


            //K0004 D5  42839
            //K0004 F5  0.63622685185.
            //K0014 etc   B8 S171018318458A_OP70.10_300.
            //K0008 B11 ZYZ.
            //K0012 D11 1
            //K0010 F11 OP70.3.
            //K0006 D8  3693953
            //K1001 F8  ISGH - CMM - 1. 

            // string input = @"Z:\Projects\QTransducer\2017年\2017_07_Zeiss\sample.xls";
            ExcelReader reader = new ExcelReader(path);

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

            QDataItem qdi = new QDataItem();
            qdi[0006] = K0006;
            qdi[0008] = K0008;
            qdi[0010] = K0010;
            qdi[0012] = K0012;
            qdi[0014] = K0014;
            qdi[0061] = K0061;

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

            string folder = GetOutFolder(path, pd.OutputFolder);
            string timetick = DateTimeHelper.ToYYYYMMDDhhmmssString(DateTime.Now);
            string filename = string.Format("{0}\\{1}_{2}.dfq", folder, allinfo, timetick);
            
            return SaveDfq(qf, filename);
        }


    }
}
