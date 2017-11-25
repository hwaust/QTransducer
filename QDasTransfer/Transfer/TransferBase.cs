using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QDAS;
using System.Threading;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace QDasTransfer.Transfer
{
	public class TransferBase
	{
		public string Error = "读取失败。";

		/// <summary>
		/// 文件日志列表，转换后可读取此列表。
		/// </summary>
		public List<string> LogList = new List<string>();

		public List<string> Exts = new List<string>();

		/// <summary>
		/// 输出文件夹
		/// </summary>
		public string OutputFolder { get; set; }

		/// <summary>
		/// 转换成功的备份文件夹。
		/// </summary>
		public string BackupGoodFolder { get; set; }

		/// <summary>
		/// 转换失败的备份文件夹。
		/// </summary>
		public string BackupBadFolder { get; set; }

		/// <summary>
		/// 在保存时，是否添加时间戳，TRUE为添加，False直接覆盖。
		/// </summary>
		public bool AddTimeToNew { get; set; }

		string companyName = "Unknown";
		/// <summary>
		/// 显示的公司名称。
		/// </summary>
		public string CompanyName
		{
			get { return companyName; }
			set { companyName = value; }
		}


		bool showFileOption = true;
		/// <summary>
		/// 是否显示文件选项。
		/// </summary>
		public bool ShowFileOption
		{
			get { return showFileOption; }
			set { showFileOption = value; }
		}


		public TransferBase()
		{
			OutputFolder = "D:\\Q-DAS_FILES";
		}

		public virtual void DealFolder(string path)
		{

		}

		public virtual void DealFile(string path)
		{

		}

		public virtual QFile Transfer(string path)
		{
			QFile qf;
			try
			{
				qf = new QFile();
				Thread.Sleep(500);
			}
			catch
			{
				qf = null;
			}
			return qf;
		}



		private string sheetName = "";
		/// <summary>
		/// 表单名称，在读取Excel表时会有此数据。
		/// </summary>
		public string SheetName
		{
			get { return sheetName; }
			set { sheetName = value; }
		}

		/// <summary>
		/// Excel导出数据
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public DataSet ImportFormExcel(string path)
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
					SheetName = tbname.Substring(1, tbname.Length - 3);
					break;
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
				Error = ex.Message;
			}

			return ds;
		}

		public bool SaveDFQ(QFile qf, string path)
		{
			if (File.Exists(path) && AddTimeToNew)
			{
				path = Funs.AppendTimeToFileName(path);
			}

			try
			{
				if (qf.SaveToFile(path))
					AddSuccessedFile(path);
				else
				{
					AddFailedFile(path, "保存失败");
					return false;
				}
			}
			catch (Exception e1)
			{
				AddFailedFile(path, e1.Message);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 检测文件的后缀名
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public virtual bool CheckExt(string path)
		{
			try
			{
				string ext = Path.GetExtension(path).ToLower();

				if (Exts.IndexOf(ext) < 0)
				{
					return false;
				}
			}
			catch { return false; }

			return true;
		}

		/// <summary>
		/// 获得可用的扩展名。
		/// </summary>
		/// <returns></returns>
		public string GetExtFilter()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("待转换的数据文件|");

			for (int i = 0; i < Exts.Count; i++)
			{
				builder.Append("*" + Exts[i] + ";");
			}

			builder.Remove(builder.Length - 1, 1);
			
			builder.Append("|所有文件|*.*");
			 
			return builder.ToString();
		}

		public void AddSuccessedFile(string file)
		{
			LogList.Add(DateTime.Now.ToString() + "|" + file + "|成功|无");
		}

		public void AddFailedFile(string file, string reson)
		{
			LogList.Add(DateTime.Now.ToString() + "|" + file + "|失败|" + reson);
		}

		public void AddLog(string file, string log)
		{
			LogList.Add(DateTime.Now.ToString() + "|" + file + "|日志|" +  log);
		}
	}
}
