using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTrans.Classes
{
    /// <summary>
    /// 此类支持xls和xlsx两种格式的Excel文件，同时支持将日期直接转为.NET的DateTime格式。
    /// </summary>
    public class NPOIExcelReader
    {
        /// <summary>
        /// 从EXCEL文件中读取指定表单的数据返回为string[,]格式。
        /// </summary>
        /// <param name="path">输入Excel文件。</param>
        /// <param name="sheetid">表单的系列号。</param>
        /// <returns></returns>
        public static string[,] loadExcel(String path, int sheetid = 0)
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


        public static void show(string [, ] data)
        {
            for (int row = 0; row < data.GetLength(0); row++)
            {
                for (int col = 0; col < data.GetLength(1); col++)
                {
                    Console.Write(data[row, col] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
