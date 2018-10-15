using QDAS;
using QDasTransfer.Classes;
using QTrans.Classes;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTrans.Company
{
    public class T201806_Zhonghang_Excel : TransferBase
    {
        public override void Initialize()
        {
            base.Initialize();
            CompanyName = "中航Excel转换器";
            VertionInfo = "1.0 alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".xlsx");
        }

        public override bool TransferFile(string path)
        {
            Workbook wb = new Workbook();
            wb.LoadFromFile(path);
            Worksheet sheet = wb.Worksheets[0];

            for (int row = 1; row < sheet.Rows.Count() + 1; row++)
            {
                for (int col = 1; col < sheet.Columns.Count() + 1; col++)
                {
                    CellRange cell = sheet[row, col];

                    Console.Write(cell.Value + ",");
                }
                Console.WriteLine();
            }

            QFile qf = new QFile();
            qf[1001] = sheet[3, 2].Value;
            qf[1053] = sheet[3, 4].Value;
            qf[1086] = sheet[3, 5].Value;
            qf[1100] = sheet[3, 6].Value;

            int rowid = 5;
            while (true)
            {
                if(isEmpty(sheet, rowid))
                {
                    break;
                }


            }



            return base.TransferFile(path);
        }

        /// <summary>
        /// 判断表单某一行是否为空。
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool isEmpty(Worksheet sheet, int row)
        {
            int value_count = 0;
            for (int column = 0; column < sheet.Columns.Count(); column++)
            {
                if (!string.IsNullOrEmpty(sheet[row, column].Value))
                    value_count++;
            }

            return value_count > 3;
        }
    }
}
