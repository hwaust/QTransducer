using QDAS;
using System;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using WindGoes.IO;
using System.Windows.Forms;

namespace QTrans.Company
{
    public class T201805_Tongyong : TransferBase
    {
        string userConfig = "userconfig.ini";
        int startColumn = 7;

        public T201805_Tongyong() { }

        public override void Initialize()
        {
            base.Initialize();
            CompanyName = "EXCEL 通用转换器";
            VertionInfo = "1.0 Alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".xls");
            pd.AddExt(".xlsx");
        }


        public override bool TransferFile(string path)
        {
            // 读取catalog 文件
            string catafile = new IniAccess(userConfig).ReadValue("catalog");
            if (!File.Exists(catafile))
            {
                MessageBox.Show("Catalog 文件不存在,请配置后再 转换。");
                return false;
            }

            // 加载 catelog
            QCatalog catalog = QCatalog.load(catafile);

            // 加载Excel数据
            string[,] data = loadExcel(path);

            // 数据从第7列开始，如果没有数据，就返回为空。
            if (data.GetLength(1) - startColumn == 0)
                return false;

            // 对应的DFQ输出文件列表。 
            List<QFile> qfs = new List<QFile>();

            // 从第2行开始处理参数和数据
            for (int row = 2; row < data.GetLength(0); row++)
            {
                // 根据指定行的数据获得qf，不存在则新建。
                QFile qf = findDfq(qfs, row, data);

                int cataid = catalog.getCatalogPID("K4073", data[0, 0]);
                if (cataid == -1) // 待处理: 找不到的情况要提示.
                {
                    LogList.Add(new Classes.TransLog(path, "", "未找到对应的catlog的值。", Classes.LogType.Fail));
                }


                for (int col = startColumn; col < data.GetLength(1); col++)
                {
                    // 获得值，可能为空。
                    string value = data[row, col];

                    // 空值跳过
                    if (value == null || value.ToString().Trim().Length == 0)
                        continue;

                    // 处理数据
                    QDataItem qdi = new QDataItem();
                    qdi[0007] = cataid;
                    qdi[0012] = cataid;
                    qdi.SetValue(value);
                    qdi.date = parseDatetime(data[row, 2], data[row, 3]);
                    qdi["K0006"] = data[row, 4];
                    qdi["K0016"] = data[row, 5];
                    qdi["K0014"] = data[row, 6];
                    // 添加数据
                    qf.Charactericstics[col - startColumn].data.Add(qdi);
                }

            }

            // 输出数据
            for (int i = 0; i < qfs.Count; i++)
            {
                qfs[i].ToDMode();
                SaveDfqByFilename(qfs[i], qfs[i][1001] + "_" + qfs[i][1002] + ".dfq");
            }


            return true;
        }

        // Date time format:  yyyy/MM/ss
        private DateTime parseDatetime(string date, string time)
        {
            DateTime datetime = DateTime.Now;
            try
            {
                string[] dt = date.Split(' ')[0].Split('/', '-');
                string[] tm = time == null || time.Trim().Length == 0 ? new string[] { "0", "0", "0" } : time.Split(' ')[1].Split(':');
                datetime = new DateTime(int.Parse(dt[0]), int.Parse(dt[1]), int.Parse(dt[2]), int.Parse(tm[0]), int.Parse(tm[1]), int.Parse(tm[2]));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return datetime;
        }

        private QFile findDfq(List<QFile> qfs, int row, string[,] data)
        {
            string key = data[row, 0] + "<-|->" + data[row, 1];
            foreach (QFile q in qfs)
                if (q[1001] + "<-|->" + q[1002] == key)
                    return q;

            // 新建并加入至qfs列表
            QFile qf = new QFile();
            qfs.Add(qf);

            // 查找QFile        
            qf[1001] = data[row, 0];
            qf[1002] = data[row, 1];
            for (int i = startColumn; i < data.GetLength(1); i++)
            {
                QCharacteristic qc = new QCharacteristic();
                qc[2001] = i - startColumn + 1;
                qc[2002] = data[1, i];
                qc[2202] = 4;
                qc[8500] = 5;
                qc[8501] = 0;
                qf.Charactericstics.Add(qc);
            }

            return qf;
        }


        public string[,] loadExcel(String path, int sheetid = 0)
        {
            // 初始化，加载Excel数据
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            HSSFWorkbook wb = new HSSFWorkbook(file);
            ISheet sheet = wb.GetSheetAt(sheetid);
            string[,] data = new string[sheet.LastRowNum + 1, sheet.GetRow(0).LastCellNum];
            for (int row = 0, rows = data.GetLength(0); row < rows; row++)
            {
                for (int col = 0, cols = data.GetLength(1); col < cols; col++)
                {
                    ICell cell = sheet.GetRow(row).GetCell(col);
                    if (cell == null)
                    {
                        data[row, col] = null;
                        continue;
                    }
                    else
                    {
                        data[row, col] = (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell)) ? cell.DateCellValue.ToString() : cell.ToString();
                    }
                }
            }
            file.Close();
            return data;
        }

    }
}
