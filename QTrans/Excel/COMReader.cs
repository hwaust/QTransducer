using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTrans.Excel
{
    public class COMReader : ExcelReaderBase
    {
        public COMReader(string excelfile, ExcelVersion version = ExcelVersion.Excel2003)
        {
            Load(excelfile, version);
        }

        public override string GetCell(int row, int column, int tableIndex = 0)
        {
            return tables[tableIndex][row + 1, column + 1] + "";
        }

        public override string GetCell(int row, char column, int tableIndex = 0)
        {
            return tables[tableIndex][row + 1, (int)column + 1] + "";
        }

        public override string GetCell(int row, string column, int tableIndex = 0)
        {
            int col = 0;
            for (int i = column.Length - 1; i >= 0; i--)
            {
                col = col * 26 + (column[i] - 65);
            }

            return tables[tableIndex][row + 1, col + 1] + "";
        }


        public override string GetCell(string cellName, int tableIndex = 0)
        {
            Cell cell = new Cell(cellName);
            return tables[tableIndex][cell.Row + 1, cell.Column + 1] + "";
        }

        public override bool Load(string path, ExcelVersion versin = ExcelVersion.Excel2003)
        {
            Application excel = new Application
            {
                Visible = false,
                UserControl = true,
                DisplayAlerts = false,
                AlertBeforeOverwriting = false
            };
            object missing = System.Reflection.Missing.Value;

            Workbook wb = excel.Application.Workbooks.Open(path, missing, missing, missing, missing, missing,
                                        missing, missing, missing, missing, missing, missing, missing, missing, missing);
            tables = new List<object[,]>();
            for (int i = 0; i < wb.Worksheets.Count; i++)
            {
                // get the values in the range, excluding titles.  
                Worksheet ws = (Worksheet)wb.Worksheets.get_Item(i + 1);
                int rows = ws.UsedRange.Cells.Rows.Count; //得到行数
                int columns = ws.UsedRange.Cells.Columns.Count;//得到列数
                string lefttop = "A1";
                string rightbottom = new Cell(rows, columns).ToString();
                tables.Add((object[,])ws.Cells.get_Range(lefttop, rightbottom).Value2);
            } 
            wb.Close();
            return true;
        }

    }
}
