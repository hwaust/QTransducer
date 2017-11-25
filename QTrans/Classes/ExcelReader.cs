 
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


        /// <summary>
        /// 从Excel加载数据表，数据以DataSet集合的形式返回。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public DataTable[] LoadSheetsFromExcel(string path, MSOfficeVersion version)
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

            return new DataTable[0];
        }


    }
}
