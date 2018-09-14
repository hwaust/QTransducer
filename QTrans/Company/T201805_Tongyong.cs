using QDAS;
using System;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using WindGoes.IO;

namespace QTrans.Company
{
    public class T201805_Tongyong : TransferBase
    {
        ISheet sheet; // 当前的EXCEL表单
        List<QFile> qfs = new List<QFile>();  // 对应的DFQ输出文件列表。 
        IniAccess ia = new IniAccess("userconfig.ini");
        QCatalog catalog;

        public T201805_Tongyong()
        {

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
            // 读取catalog 文件
            string catafile = ia.ReadValue("catalog");
            if (!File.Exists(catafile))
            {
                System.Windows.Forms.MessageBox.Show("Catalog 文件不存在,请配置后再测试.");
                return false;
            }
            catalog = QCatalog.load(catafile);


            // 初始化，加载Excel数据
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            HSSFWorkbook wb = new HSSFWorkbook(file);
            sheet = wb.GetSheet("Sheet1");

            int rowCount = 0; // 表示数据行数。 
            while (true)
                if (sheet.GetRow(rowCount++) == null)
                    break;

            // 查询总列数（即参数个数）
            int currentColumn = 7; // 从第7列开始，向右查询列。
            int totalColumn = 0; // 表示总共有多少列
            while (true)
            {
                if (CellString(1, currentColumn) != null)
                {
                    totalColumn++;
                    currentColumn++;
                    if (CellString(1, currentColumn) == null)
                        break;
                }
                else
                    break;
            }

            // 如果没有数据，就返回为空。
            if (totalColumn == 0)
            {
                file.Close();
                return false;
            }

            qfs.Clear();
            // 从第2行开始处理参数和数据
            for (int i = 2; i < rowCount; i++)
                try { processRow(i, totalColumn); } catch (Exception ex) { Console.WriteLine(ex.Message); }


            for (int i = 0; i < qfs.Count; i++)
            {
                qfs[i].ToDMode();
                string dfqname = new Regex("[\\\\/:*?\"<>|]").Replace(qfs[i][1001] + "_" + qfs[i][1002] + ".dfq", "_");
                qfs[i].SaveToFile(Path.Combine(pd.GetOutDirectory(path), dfqname));
            }

            file.Close();
            return true;
        }

        private void processRow(int currentColumn, int totalColumn)
        {
            QFile qf = new QFile(); // 每行对应一个QFile实例
            bool existed = false; // 表示这个QFile实例是否已存在 


            // 做判断，查询在qfs中对应的K1001和K1002是否完全相同
            // 若有相同，则从qfs中获得qf. 然后逐一加到  qf.Charactericstics对应的data中
            // qf.Charactericstics[i].data.Add(new QDataItem());
            // 查找QFile        
            if (CellString(currentColumn, 0) != null) { qf[1001] = CellString(currentColumn, 0); }
            if (CellString(currentColumn, 1) != null) { qf[1002] = CellString(currentColumn, 1); }
            foreach (QFile q in qfs)
            {
                if (q[1001].ToString().Equals(qf[1001].ToString()) && q[1002].ToString().Equals(qf[1002].ToString()))
                {
                    existed = true;
                    qf = q;
                    break;
                }
            }

            // 如果为新建, 则加入至qfs列表中
            if (!existed)
                qfs.Add(qf);



            for (int i = 0; i < totalColumn; i++)
            {
                // 获取数据
                QDataItem dataItem = new QDataItem();
                int cataid = catalog.getCatalogPID("K4073", CellString(0, 0));
                dataItem[0007] = cataid; // 待处理: 找不到的情况要提示.
                dataItem[0012] = cataid;
                dataItem.SetValue(CellString(currentColumn, 7 + i));
                dataItem.date = DateTime.ParseExact(CellString(currentColumn, 2) + " " + CellString(currentColumn, 3), "M/d/yy H:m:s", null);
                if (CellString(currentColumn, 4) != null) { dataItem[0006] = CellString(currentColumn, 4); }
                if (CellString(currentColumn, 5) != null) { dataItem[0016] = CellString(currentColumn, 5); }
                if (CellString(currentColumn, 6) != null) { dataItem[0014] = CellString(currentColumn, 6); }


                if (existed)
                {
                    qf.Charactericstics[i].data.Add(dataItem);
                }
                else
                {
                    QCharacteristic qc = new QCharacteristic();
                    qc[2001] = i + 1;
                    qc[2202] = 4;
                    qc[8500] = 5;
                    qc[8501] = 0;
                    if (CellString(1, 7 + i) != null) { qc[2002] = CellString(1, 7 + i); }
                    qc.data.Add(dataItem);
                    qf.Charactericstics.Add(qc);
                }
            }

            Console.WriteLine();

        }

    }
}
