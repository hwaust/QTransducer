using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace QDasTransfer
{
	public partial class Form1 : Form
	{ 
		string path = "c:\\somedata\\2.xlsx";
		public Form1()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Excel导出数据
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static DataSet ImportFormExcel(string path)
		{
			DataSet ds = new DataSet();
			try
			{
				string strConn = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + path +
					";" + "Extended Properties=Excel 8.0;";
				OleDbConnection cn = new OleDbConnection(strConn);
				cn.Open();

				DataTable dt = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

				string tbname = "";
				foreach (DataRow dr in dt.Rows)
				{
					tbname = (String)dr["TABLE_NAME"];
				}

				string strExcel = "";
				OleDbDataAdapter myCommand = null;
				strExcel = "select * from [" + tbname + "]";
				myCommand = new OleDbDataAdapter(strExcel, strConn);

				myCommand.Fill(ds, "table1");
				cn.Close();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return ds;
		}

		private void button1_Click(object sender, EventArgs e)
		{


		}

		private void button2_Click(object sender, EventArgs e)
		{
			DataSet ds = ImportFormExcel(path);
			Console.WriteLine(ds);

			DataTable dt = ds.Tables[0];
			for (int i = 0; i < dt.Columns.Count; i++)
			{
				Console.WriteLine(dt.Columns[i].Caption);
			}
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					Console.WriteLine(dt.Rows[i][j]);
				}
			}
		}

	}
}
