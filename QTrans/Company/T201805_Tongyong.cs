using QDAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTrans.Company
{
    public class T201805_Tongyong:TransferBase
    {
        public override void Initialize()
        {
            base.Initialize();
            CompanyName = "EXCEL 通用转换器";
            VertionInfo = "1.0 Alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".csv");
        }


        public override bool TransferFile(string path)
        {
            // K1xxx 表示零件层信息 ->QFile
            // K2xxx 表示参数层信息 ->QCharacteristic 
            // K0xxx 表示测量数据　-> QDataItem
            QFile qf = new QFile();
            qf[1001] = "excel[0][1]";

            QCharacteristic qc = new QCharacteristic();
            qc[2001] = "k2001";

            qf.Charactericstics.Add(qc);


            QDataItem dataItem = new QDataItem();
            dataItem.SetValue("excel[x][y]");
            dataItem.date = DateTime.Parse("2018-1-1 15:00:00");
            dataItem[0006] = "";

            qc.data.Add(dataItem);



            qf.ToDMode();

            qf.SaveToFile("d:\\abcd.dfq");

            return true;
        }
    }
}
