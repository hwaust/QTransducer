using QDAS;
using QTrans.Excel;
using QTrans.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTrans.Company.Y2017
{
    public class T201710 : TransferBase
    {
        public override void Initialize()
        {
            base.Initialize();
            CompanyName = "重庆 Nemak 转换器";
            VertionInfo = "1.1.0";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".xls");
        }

        public override bool TransferFile(string infile)
        {
            QCatalog qlog = QCatalog.GetCatlog(@"Z:\Projects\QTransducer\2017年\2017_10_重庆_Nemak\20180112_Problems");
            COMReader reader = new COMReader(infile);

            /************ 从Excel文件读取标题K域 **************/
            string K1001 = reader.GetCell("B8");
            string K0008 = reader.GetCell("B11");

            double days = double.Parse(reader.GetCell("D5"));
            double time = double.Parse(reader.GetCell("D8"));
            DateTime dt = new DateTime(1900, 1, 1).AddDays(-2).AddDays(days + time);

            string K0012 = reader.GetCell("D11");

            string K0061 = reader.GetCell("F5");
            string K0014 = reader.GetCell("F8");
            string K1002 = reader.GetCell("F11");

            // 年份（2位）_天数（3位）_班次(1位)_设备号（1位）_当日序列号（3位）_设备累计序列号（7位）_工件状态（1位） 
            string K0006 = null;
            string K0007 = null;
            string K0010 = null;

            try
            {
                K0006 = K0014.Substring(0, 5);// K0007: 截取班次（1位）.
                K0007 = K0014.Substring(5, 1);// K0007: 截取班次（1位）.
                K0010 = K0014.Substring(6, 1);// K0010: 截取设备号（1位）
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            /************ K值模板 **************/
            QFile qf = new QFile();
            qf[1001] = K1001;
            qf[1002] = K1002;

            QDataItem qdi = new QDataItem();
            if (K0006 != null)
                qdi[0006] = K0006;
            if (K0007 != null)
                qdi[0007] = qlog.GetCatalogPIDString("K4252", K0007);
            qdi[0008] = qlog.GetCatalogPIDString("K4092", K0008);
            if (K0010 != null)
                qdi[0010] = qlog.GetCatalogPIDString("K4062", K0010);
            qdi[0012] = qlog.GetCatalogPIDString("K4072", K0012);
            qdi[0014] = K0014;
            qdi[0061] = qlog.GetCatalogPIDString("K4272", K0061);

            // The first row of data. If it is not fixed, modify this variable.
            int firstRow = 13;
            int lastRow = reader.GetRowCount();

            for (int i = firstRow; i < reader.GetRowCount(); i++)
            {
                // Characteristic	Actual	Nominal	Upper Tol	Lower Tol	Deviation
                // K2002, K0001, K2101, K2113, K2112 
                string K2002 = reader.GetCell(i, 0);
                string K0001 = reader.GetCell(i, 1);
                string K2101 = reader.GetCell(i, 2);
                string K2113 = reader.GetCell(i, 3);
                string K2112 = reader.GetCell(i, 4);

                if (K2002 == null || K2002.Trim().Length == 0)
                    break;  // 根据20180112 问题 第2条 修改。

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
                di.date = dt; // DateTime.Now; // 根据20180112 问题 第3条 修改。
                qc.data.Add(di);

                qf.Charactericstics.Add(qc);
            }

            qf.ToDMode();

            return SaveDfq(qf, string.Format("{0}\\{1}_{2}.dfq",
                pd.GetOutDirectory(infile), // output directory
                K0014,   // filename from all info.
                DateTimeHelper.ToFullString(DateTime.Now))); // time stamp. 
        }
    }
}
