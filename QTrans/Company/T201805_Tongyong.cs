using QDAS;
using System;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;
 

namespace QTrans.Company
{
    public class T201805_Tongyong : TransferBase
    {
        ISheet sheet; // 当前的EXCEL表单
        List<QFile> qfs = new List<QFile>();  // 对应的DFQ输出文件列表。
        string catalogPath = @"D:\GitHub\QTransducer\data\AT&M.dfd";  //  Catalog配件文件的路径。
        QCatalog catalog;

        public T201805_Tongyong()
        {
            catalog = QCatalog.load(catalogPath);
        }

        public override void Initialize()
        {
            base.Initialize();
            CompanyName = "EXCEL 通用转换器";
            VertionInfo = "1.0 Alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".xls");
            pd.AddExt(".xlsx");
        }


        private String CellString(int row, int cell)
        {
            if (sheet != null)
            {
                ICell c = sheet.GetRow(row).GetCell(cell);
                if (c != null)
                {
                    return c.ToString();
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

       

        // K1xxx 表示零件层信息 ->QFile
        // K2xxx 表示参数层信息 ->QCharacteristic 
        // K0xxx 表示测量数据　-> QDataItem 
        public override bool TransferFile(string path)
        {
            // 初始化，加载Excel数据
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            HSSFWorkbook wb = new HSSFWorkbook(file);
            ISheet sheet = wb.GetSheet("Sheet1");

            int rowCount = 0; // 表示数据行数。 
            while (true)
                if (sheet.GetRow(rowCount++) == null)
                    break;

            // 查询总列数（即参数个数）
            int currentColumn = 7; // 从第7列开始，向右查询列。
            int totalColumn = 0; // 表示总共有多少列
            while (true)
            {
                if (CellString(2, currentColumn) != null)
                {
                    totalColumn++;
                    currentColumn++;
                    if (CellString(2, currentColumn) == null)
                    {
                        break;
                    }
                }
            }


            QFile qfile = new QFile(); 

            // 从第2行开始处理参数和数据

            for (int i = 2; i < rowCount; i++)
            {
                qfile = GeneterQfFor(i, totalColumn); 
                qfile.ToDMode();

                string name = Convert.ToString(qfile[1001]);
                string modified = new Regex("[\\\\/:*?\"<>|]").Replace(name, "_");
                string time = DateTime.Now.ToString("yyyyMMddHHmmss");

                qfile.SaveToFile($"d:\\{modified}_{time}.dfq");
                Thread.Sleep(1000);
            }

            file.Close(); 
            return true;
        }
 
 
        private QFile GeneterQfFor(int currentColumn, int totalColumn)
        {
            QFile qf = new QFile();
            string k1001 = qf[1001].ToString();
            bool isNew = false;

            // 做判断，查询在qfs中对应的K1001和K1002是否完全相同
            // 若有相同，则从qfs中获得qf. 然后逐一加到  qf.Charactericstics对应的data中
            // qf.Charactericstics[0].data.Add(new QDataItem());

            if (CellString(currentColumn, 0) != null) { qf[1001] = CellString(currentColumn, 0); }
            if (CellString(currentColumn, 1) != null) { qf[1002] = CellString(currentColumn, 1); }

            for (int i = 0; i < totalColumn; i++)
            {
                QCharacteristic qc = new QCharacteristic();
                qc[2001] = i + 1;
                if (CellString(1, 7 + i) != null) { qc[2002] = CellString(1, 7 + i); }
                QDataItem dataItem = new QDataItem();

                int value = catalog.getCatalogPID("K4073", CellString(0, 0));
                dataItem[0012] = value;
                dataItem.SetValue(CellString(currentColumn, 7 + i));
                dataItem.date = DateTime.ParseExact(CellString(currentColumn, 2) + " " + CellString(currentColumn, 3), "M/d/yy H:m:s", null);
                if (CellString(currentColumn, 4) != null) { dataItem[0006] = CellString(currentColumn, 4); }
                if (CellString(currentColumn, 5) != null) { dataItem[0016] = CellString(currentColumn, 5); }
                if (CellString(currentColumn, 6) != null) { dataItem[0014] = CellString(currentColumn, 6); }
                qc.data.Add(dataItem);
                qf.Charactericstics.Add(qc);
            }

            return qf;
        }

    }
}
