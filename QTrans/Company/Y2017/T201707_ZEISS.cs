using QDasTransfer.Classes;
using QTrans.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QTrans.Company.Y2017
{
    public class T201707_ZEISS : TransferBase
    {
   
        ExcelReader reader;

        public override void SetConfig(ParamaterData pd)
        {
            base.SetConfig(pd);

            reader = new ExcelReader(@"Z:\Projects\QTransducer\2017年\2017_07_Zeiss\sample.xls");
 


            double days = double.Parse(reader.getData("D5"));
            double time = double.Parse(reader.getData("F5"));

            DateTime dt = new DateTime(1900,1,1).AddDays(-2);
            Console.WriteLine(dt);
            dt = dt.AddDays(days + time);
            Console.WriteLine( dt.ToString());


            reader.showTable(0);



        }

        public override bool TransferFile(string path)
        {
            return base.TransferFile(path);
        }

 
    }
}
