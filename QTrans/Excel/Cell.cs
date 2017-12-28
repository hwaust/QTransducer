using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTrans.Excel
{
    public class Cell
    {
        public int Column;
        public int Row;

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public Cell(string range)
        {
            cellToInt(range);
        }

        public override string ToString()
        {
            return Column < 26 ?
                    "" + (char)(64 + Column % 26 + 1) + (Row + 1) :
                    "" + (char)(64 + Column / 26) + (char)(64 + Column % 26 + 1) + (Row + 1);
        }


        public string IntToString(int col)
        {
            StringBuilder sb = new StringBuilder();
            col += 1;
            while (col > 0)
            {
                sb.Insert(0, (char)(64 + (col % 26)));
                col /= 26;
            }

            return sb.ToString();
        }

        void cellToInt(string s)
        {
            s = s.ToUpper();
            int col = 0;
            int row = 0;
            int p = 0;
            while (p < s.Length)
            {
                if (s[p] >= 'A' && s[p] <= 'Z')
                    col = col * 26 + (s[p] - 64);
                else
                    break;
                p++;
            }

            while (p < s.Length)
            {
                row = row * 10 + (s[p] - 48);
                p++;
            }


            Column = col - 1;
            Row = row - 1;
        }
    }
}
