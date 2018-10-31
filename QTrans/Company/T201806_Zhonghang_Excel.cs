using QDAS;
using QDasTransfer.Classes;
using QTrans.Classes;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindGoes.IO;

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
            IniAccess ia = new IniAccess("userconfig.ini");
            string catalogfile = ia.ReadValue("catalog");
            QCatalog catalog = QCatalog.GetCatlog(File.Exists(catalogfile) ? catalogfile : "catalog.dfd");


            Workbook wb = new Workbook();
            wb.LoadFromFile(path);
            Worksheet sheet = wb.Worksheets[0];

            QFile qf = new QFile();
            qf[1001] = sheet[3, 2].Value;
            qf[1053] = sheet[3, 4].Value;
            qf[1086] = sheet[3, 5].Value;
            qf[1110] = sheet[3, 6].Value; // 1100 -> 1110
            qf[1101] = sheet[2, 1].Value;
            qf[1102] = sheet[3, 1].Value;

            int rowid = 5;
            while (true)
            {
                if (isEmpty(sheet, rowid))
                    break;

                QCharacteristic qc = new QCharacteristic();
                qf.Charactericstics.Add(qc);
                string pid = sheet[rowid, 1].Value != null ? sheet[rowid, 1].Value.ToString() : "";
                qc[2001] = pid.Replace('(', ' ').Replace(')', ' ').Trim();
                qc[2002] = sheet[rowid, 2].Value;
                qc[2005] = sheet[rowid, 13].Value.Contains("是") ? 4 : 1; // 不包括是时为 1 还是0 ，文档中有错误。
                qc[2022] = 4;
                qc[2101] = sheet[rowid, 3].Value;
                qc[2113] = sheet[rowid, 4].Value;
                qc[2112] = sheet[rowid, 5].Value;
                qc[2120] = 1;
                qc[2121] = 1;
                qc[2142] = "mm";
                qc[8500] = 5;
                qc[8501] = 0;
             


                QDataItem di = new QDataItem();
                di.SetValue(sheet[rowid, 6].Value);
                di.date = sheet[3, 10].DateTimeValue;
                di[0006] = sheet[3, 3].Value;
                di[0008] = catalog.getCatalogPID("K4093", sheet[3, 11].Value);
                di[0010] = catalog.getCatalogPID("K4063", sheet[rowid, 11].Value);
                di[0011] = sheet[rowid, 10].Value;
                di[0012] = catalog.getCatalogPID("K4072", sheet[rowid, 10].Value);
                di[0061] = catalog.getCatalogPID("K4273", sheet[3, 7].Value);
                qc.data.Add(di);

                // Console.WriteLine("{0}, {0}, {0}, {0}", di[0008], di[0010], di[0012], di[0061]);

                rowid++;
            }

            qf.ToDMode();

            SaveDfqByFilename(qf, Path.GetFileNameWithoutExtension(path) + ".dfq");

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
            for (int column = 1; column < sheet.Columns.Count() + 1; column++)
            {
                if (!string.IsNullOrEmpty(sheet[row, column].Value))
                    value_count++;
            }

            return value_count < 3;
        }
    }
}
