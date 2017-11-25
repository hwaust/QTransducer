using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QDAS;

namespace QDasTransfer.Transfer
{
	public class Mairi:TransferBase
	{
		public Mairi()
		{
			CompanyName = "迈日";
			Exts.Add(".xls");
			Exts.Add(".xlt");
			Exts.Add(".xlsx");
		}

		public override void DealFile(string path)
		{
			try
			{
				DataTable dt = ImportFormExcel(path).Tables[0];
				QFile qf = new QFile();

				string lineno = dt.Rows[0][2].ToString().Trim();
				string tmp = dt.Rows[0][3].ToString().Trim();


				for (int i = 4; i < dt.Columns.Count; i++)
				{
					QParamter p = new QParamter();
					p[2001] =  (i-3).ToString();
					p[2002] = dt.Columns[i].Caption.Trim();// dt.Rows[0][i].ToString().Trim();
					p[2024] = tmp;

					Console.Write(dt.Columns[i].Caption);
				}



				for (int i = 0; i < dt.Rows.Count; i++)
				{
					if (dt.Rows[i][0].ToString().Length < 10)
					{
						break;
					}


					for (int j = 0; j < dt.Columns.Count; j++)
					{
						Console.WriteLine("{0}, {1}: {2}", i, j, dt.Rows[i][j].ToString().Trim());
					} 
				}
				 
			}
			catch (Exception e1)
			{
				AddFailedFile(path, e1.Message);
			}
		}


		public override void DealFolder(string path)
		{
			 
		}
	}
}
