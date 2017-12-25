using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTrans.Excel
{
    public abstract class ExcelReaderBase
    {
        protected List<object[,]> tables;

        public abstract bool Load(string file, ExcelVersion version = ExcelVersion.Excel2003);

        public virtual string GetCell(int row, int column, int tableIndex = 0)
        {
            return tables[tableIndex][row, column] + "";
        }

        public virtual string GetCell(string cname, int tableIndex = 0)
        {
            Cell cell= new Cell(cname);
            return tables[tableIndex][cell.Row, cell.Column] + "";
        }

        public virtual int GetRowCount(int tableIndex = 0)
        {
            return tables[tableIndex].GetLength(0);
        }

        public virtual int GetColumnCount(int tableIndex = 0)
        {
            return tables[tableIndex].GetLength(1);
        }

        public void ShowTable(int tableindex = 0)
        {
            for (int row = 0; row < GetRowCount(tableindex); row++)
            {
                for (int column = 0; column < GetColumnCount(tableindex); column++)
                {
                    Console.Write(GetCell(row, column, tableindex) + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
