/***** How to use 
 * 
 * 更新日期： 2017-12-10
 * 首发日期： 2017-09-01
 * 用法：
 * - Initialize
 * ExcelReader er = new ExcelReader(exlfile); 
 *                // er = new ExcelReader(exlfile, MSOfficeVersion.Office2003);  
 * - Read data
 * er.getData("A1");      // get cell A1 from the 0-th table.
 * er.getData("C2", 4);  // get cell A1 from the 4-th table.
 * 
 * - Show Table
 * er.showTable(0);     // show the 0-th table.
 * 
  */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDasTransfer.Classes
{

    public class ExcelReader
    {
        public string ErrorMessage { get; internal set; }

        public DataTable[] Tables;


        public ExcelReader()
        {

        }

        public ExcelReader(string excelfile, MSOfficeVersion version = MSOfficeVersion.Office2003)
        {
            Tables = LoadSheetsFromExcel(excelfile, version);
        }

        public void showTable(int v)
        {
            DataTable dt = Tables[v];

            StringBuilder sb = new StringBuilder();

            // first line.
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                sb.Append("\t" + (char)(col + 65));
            }
            sb.AppendLine();

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                sb.Append((row + 1).ToString() + "\t");
                for (int col = 0; col < dt.Columns.Count ; col++)
                {
                    sb.Append(dt.Rows[row][col] + "\t");
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb);
        }

        /// <summary>
        /// 从Excel加载数据表，数据以DataSet集合的形式返回。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public DataTable[] LoadSheetsFromExcel(string path, MSOfficeVersion version = MSOfficeVersion.Office2003)
        {
            if (!File.Exists(path))
                return new DataTable[0];


            string provider = version == MSOfficeVersion.Office2000 ?
                                                                "Microsoft.Jet.OleDb.4.0" :        // offce  199x -- 2000
                                                                "Microsoft.Ace.OLEDB.12.0";    // office 2003 -- 2016 
            string strConn = string.Format("Provider={0};Data Source={1};Extended Properties='Excel 12.0;HDR=NO;IMEX=1';", provider, path);

            Console.WriteLine(strConn);
            try
            {
                OleDbConnection cn = new OleDbConnection(strConn);
                cn.Open();

                List<DataTable> dts = new List<DataTable>();
                DataTable dt = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                foreach (DataRow dr in dt.Rows)
                {
                    string sheetName = dr["TABLE_NAME"].ToString();
                    string strExcel = "select * from [" + sheetName + "]";
                    DataSet ds = new DataSet();
                    OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, strConn);
                    myCommand.Fill(ds, sheetName);
                    dts.Add(ds.Tables[0]);
                }

                cn.Close();

                return dts.ToArray();
            }
            catch (Exception e1)
            {
                ErrorMessage = e1.Message;
            }

            return null;
        }


        public string getData(string s, int tableindex = 0)
        {
            try
            {
                string str = s.ToUpper();
                int row = str[1] - 49;
                int col = str[0] - 65;

                return Tables[tableindex].Rows[row][col].ToString();
            }
            catch { }

            return "";
        }
    }
}
