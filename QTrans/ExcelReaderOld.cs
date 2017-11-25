using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace QTrans
{
    public class ExcelReaderOld
    {

        /// <summary>
        /// 用于读取时接收Excel中的表单名。
        /// </summary>
        public static string SheetName = "";



        /// <summary>
        /// 从Excel加载数据表，数据以DataSet集合的形式返回。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataTable[] LoadSheetsFromExcel(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return null;

                string strConn = "Provider=Microsoft.Ace.OLEDB.12.0; Data Source=" + path + ";" + "Extended Properties=Excel 8.0;Extended Properties='Excel 12.0;HDR=NO;IMEX=1';";

                OleDbConnection cn = new OleDbConnection(strConn);
                List<DataTable> dts = new List<DataTable>();

                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEL Reading error." + ex.Message);
                    string strConn1 = "Provider=Microsoft.Jet.OleDb.4.0;data source=" + path + ";Extended Properties='Excel 8.0; HDR=No; IMEX=1'";
                    cn = new OleDbConnection(strConn1);
                    cn.Open();
                }


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
                Console.WriteLine(e1.Message);
            }

            return null;
        }



        /// <summary>
        /// 从Excel文件导入数据，返回一个 DataSet。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataSet ImportFormExcel(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return null;

                DataSet ds = new DataSet();
                string strConn = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
                //  strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + path + ";Extended Properties='Excel 8.0; HDR=Yes; IMEX=1'";
                //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
                //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'"; ;
                OleDbConnection cn = new OleDbConnection(strConn);
                cn.Open();

                DataTable dt = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                string tbname = "";
                foreach (DataRow dr in dt.Rows)
                {
                    tbname = (String)dr["TABLE_NAME"];
                    SheetName = tbname.Substring(1, tbname.Length - 3);
                    break;
                }




                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                strExcel = "select * from [" + tbname + "]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);

                myCommand.Fill(ds, "table1");
                cn.Close();

                return ds;
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
            }

            return null;
        }



        public static DataSet ExcelToDataTable(string strExcelFileName, string strSheetName)
        {
            try
            {
                if (!File.Exists(strExcelFileName))
                {
                    return null;
                }
                //源的定义
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + strExcelFileName + ";" + "Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";

                //Sql语句
                string strExcel = string.Format("select * from [{0}$]", strSheetName); //这是一种方法


                //string strExcel = "select * from   [test data$]";
                //定义存放的数据表
                DataSet ds = new DataSet();
                //连接数据源
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                //适配到数据源
                OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, strConn);
                adapter.Fill(ds, strSheetName);
                conn.Close();
                return ds;
            }
            catch
            {
            }
            return null;
        }

    }
}
