using Excel;
using QDasTransfer.Classes;
using QTrans.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace QTrans.Company.Y2017
{
    public class T201707_ZEISS : TransferBase
    {

        ExcelReader reader;
        string input = @"Z:\Projects\QTransducer\2017年\2017_07_Zeiss\sample.xls";

        public override void SetConfig(ParamaterData pd)
        {
            base.SetConfig(pd);

            reader = new ExcelReader(input);

 

            double days = double.Parse(reader.getData("D5"));
            double time = double.Parse(reader.getData("F5"));
            DateTime dt = new DateTime(1900, 1, 1).AddDays(-2);

            reader.showTable(0);
            string allinfo = reader.getData("B8");
            string K1001 = reader.getData("F11");
            string K1086 = allinfo.Split('_')[1];
            string K0008 = reader.getData("B11");
            string K0010 = reader.getData("F11");
            string K0012 = reader.getData("D11");
            string K0014 = allinfo.Split('_')[0];
            string K0061 = allinfo.Split('_')[2];
            string K2022 = "8";

             


            for (int i = 0; i < reader.getRowCount(1); i++)
            {
                for (int j = 0; j < reader.geColumnCount(1); j++)
                {
                    Console.Write(reader.getData(i, j, 1));
                }
                Console.WriteLine();
            }


        }

        public override bool TransferFile(string path)
        {
            return base.TransferFile(path);
        }


    }
}
