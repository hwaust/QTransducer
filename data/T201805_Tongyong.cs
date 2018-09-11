using QDAS;
using System;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using System.Threading;
using System.Text.RegularExpressions;

namespace QTrans.Company
{
    public class T201805_Tongyong:TransferBase
    {

        private HSSFWorkbook wb;
        private ISheet sheet;
        private QFile qfile;
        private FileStream file;

        public T201805_Tongyong()
        {

        }

        public override void Initialize()
        {
            base.Initialize();
            CompanyName = "EXCEL 通用转换器";
            VertionInfo = "1.0 Alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".csv");
        }

        private String CellString(int row,int cell)
        {
            if(sheet!= null)
            {
                ICell c = sheet.GetRow(row).GetCell(cell);
                if (c != null)
                {
                    return c.ToString();
                } else
                {
                    return null;
                }
            }
            return null;
        }


        public override bool TransferFile(string path)
        {

            string catalogPath = "D:AT&M.dfd";

            file = new FileStream(path, FileMode.Open, FileAccess.Read);
            wb = new HSSFWorkbook(file);
            
            sheet = wb.GetSheet("Sheet1");
            // K1xxx 表示零件层信息 ->QFile
            // K2xxx 表示参数层信息 ->QCharacteristic 
            // K0xxx 表示测量数据　-> QDataItem
            int lineNumber = 0;
            while(true)
            {
                IRow row = sheet.GetRow(lineNumber);
                if (row != null)
                {
                    lineNumber++;
                }
                else
                {
                    break;
                }
            }

            int rowValue = 7;
            int col = 0;
            while(true)
            {
                col = col + 1;
                if (CellString(2, rowValue) != null)
                {
                    rowValue = rowValue + 1;
                    if (CellString(2, rowValue) == null)
                    {
                        break;
                    }
                }
            }
            
            for(int i = 2;i < lineNumber;i++)
            {
                qfile = GeneterQfFor(i, col,catalogPath);
                string name = Convert.ToString(qfile[1001]);
                qfile.ToDMode();
                Regex reg = new Regex("[\\\\/:*?\"<>|]");
                string modified = reg.Replace(name, "_");
                qfile.SaveToFile("d:\\"+ modified + "_"+ GetTimeStamp() + ".dfq");
                Thread.Sleep(1000);
            }
            
            file.Close();
            return true;
        }


        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        private QFile GeneterQfFor(int LineNumber,int col, String catalogPath)
        {
            QFile qf = new QFile();
            if (CellString(LineNumber, 0) != null) { qf[1001] = CellString(LineNumber, 0); }
            if (CellString(LineNumber, 1) != null) { qf[1002] = CellString(LineNumber, 1); }

            
            for (int i = 0; i< col; i++)
            {
                QCharacteristic qc = new QCharacteristic();
                if (CellString(1, 7 + i) != null) { qc[2002] = CellString(1, 7 + i); }
                QDataItem dataItem = new QDataItem();

                QCatalog catalog = QCatalog.load(catalogPath);
                int value = catalog.getCatalogPID("K4073", CellString(0,0));
                dataItem[0012] = value;
                dataItem.SetValue(CellString(LineNumber, 7 + i));
                dataItem.date = DateTime.ParseExact(CellString(LineNumber, 2) + " " + CellString(LineNumber, 3), "M/d/yy H:m:s", null);
                if (CellString(LineNumber, 4) != null) { dataItem[0006] = CellString(LineNumber, 4); }
                if (CellString(LineNumber, 5) != null) { dataItem[0016] = CellString(LineNumber, 5); }
                if (CellString(LineNumber, 6) != null) { dataItem[0014] = CellString(LineNumber, 6); }
                qc.data.Add(dataItem);
                qf.Charactericstics.Add(qc);
            }
            
            return qf;
        }

    }
}
