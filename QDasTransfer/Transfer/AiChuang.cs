using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using QDAS;
using System.IO;

namespace QDasTransfer.Transfer
{
	public class AiChuang:TransferBase
	{

		public AiChuang()
		{
			Exts.Add(".xls");
			Exts.Add(".xlt");
			Exts.Add(".xlsx");
			CompanyName = "艾创";
		} 



		private QFile transfer(string path)
		{

			try
			{ 
				DataSet ds = ImportFormExcel(path);
				DataTable dt = ImportFormExcel(path).Tables[0];

				int rows = dt.Rows.Count + 1;
				int cols = dt.Columns.Count;

				object[,] objs = new object[cols, rows];
				for (int i = 0; i < dt.Columns.Count; i++)
				{
					objs[i, 0] = dt.Columns[i].Caption;
				}

				for (int i = 0; i < dt.Rows.Count; i++)
				{
					for (int j = 0; j < dt.Columns.Count; j++)
					{
						objs[j, i + 1] = dt.Rows[i][j];
					}
				}

				QFile qf = new QFile();
				int count = (dt.Columns.Count - 3) / 4;
				qf[1002] = SheetName;
				qf[1005] = SheetName;

				for (int i = 0; i < count; i++)
				{
					QParamter p = new QParamter();
					p.id = i+1;

					p[2001] = objs[i * 4 + 6, 0].ToString();
					p[2102] = double.Parse(objs[i * 4 + 3, 1].ToString());
					p[2110] = double.Parse(objs[i * 4 + 4, 1].ToString());
					p[2111] = double.Parse(objs[i * 4 + 5, 1].ToString());

					double data = double.Parse(objs[i * 4 + 6, 1].ToString());

					qf.pramters.Add(p);
				}

				int hcount = dt.Rows.Count / 2 + 1;

				for (int i = 0; i < hcount; i++)
				{
					QData data = new QData();
					DateTime date = DateTime.Parse(objs[0, i * 2 + 1].ToString() 
						+ " " + objs[1, i * 2 + 1].ToString());

					for (int j = 0; j < count; j++)
					{
						QDataItem di = new QDataItem();
						di.date = date;
						string v = objs[j * 4 + 3, i * 2 + 1].ToString();

						if (v == null || v.Length == 0)
							di.p1 = 256;
						else
							di.value = double.Parse(v);

						data.items.Add(di);
					}

					qf.data.Add(data);
				}


				return qf;
			}
			catch(Exception e1)
			{
				AddLog(path, e1.Message);
			}
			return null;
		}

		public override void DealFile(string path)
		{
			try
			{
				if (!CheckExt(path))
					return;
				
				QFile qf = transfer(path); 
				string filename = Path.GetFileNameWithoutExtension(path);
				string dfqpath = OutputFolder + "\\" + filename + ".DFQ";

				SaveDFQ(qf, dfqpath); 
			}
			catch (Exception e1)
			{
				AddFailedFile(path, e1.Message);
			} 
		}

		public override void DealFolder(string path)
		{
			try
			{
				string[] files = Directory.GetFiles(path);

				for (int i = 0; i < files.Length; i++)
				{
					DealFile(files[i]);
				}

			}
			catch (Exception e1)
			{
				AddLog(path, e1.Message);
			}
		}
	}
}
