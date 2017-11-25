using QDAS;
using QTrans.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QTrans.Company.Y2017
{
	public class T201703_Tianjin_Xiongbang : TransferBase
	{
		List<QLine> qlines;

		public override void SetConfig(ParamaterData pd)
		{
			CompanyName = "天津雄邦";
			VertionInfo = "1.0";
			pd.SupportAutoTransducer = true;
			pd.AddExt(".xls");

			WindGoes.IO.IniAccess ia = new WindGoes.IO.IniAccess(Application.StartupPath + "\\userconfig.ini");
			string catalog = ia.ReadValue("catalog");
			try
			{
				qlines = QLine.read(catalog);
			}
			catch (Exception ex)
			{
				AddLog(catalog, ex.Message);
				MessageBox.Show("目录读取失败，文件名：\n" + catalog);
			}
		}

		string getCatalog(string key, string value)
		{
			foreach (QLine qline in qlines)
			{
				if (qline.key == key && qline.value == value)
				{
					return qline.id;
				}
			}

			return "";
		}

		public override bool TransferFile(string path)
		{
			DataTable[] tbs = ExcelReaderOld.LoadSheetsFromExcel(path);

			DataTable body = tbs[1];


			string datetime = getDate(body.Rows[3][3].ToString()) + " " + body.Rows[6][3].ToString();

			string k1001 = body.Rows[6][1].ToString();
			string k0007_0010_0014_1086 = body.Rows[6][5].ToString();

			string k0008 = body.Rows[9][1].ToString();
			string k0012 = body.Rows[9][3].ToString();

			QFile qf = new QFile();
			qf[1001] = k1001;

			QDataItem datatemp = new QDataItem();
			datatemp.date = DateTime.Parse(datetime);
			// 命名规则为：日期（8位）_班次(1位)_线体号（2位）_工位号（3位）_流水码（4位）
			// K0007: 截取线体号（2位）后和目录信息比较提取REFERENCE NO.
			// K0010: 截取工位号（3位）后和目录信息比较提取REFERENCE NO
			// K1086: 将工位号（3位）直接写入K1086
			if (k0007_0010_0014_1086.Length == 18)
			{
				qf[1086] = k0007_0010_0014_1086.Substring(11, 3); // 工位号
				datatemp[0007] = getCatalog("K4252", k0007_0010_0014_1086.Substring(9, 2));  // 线体号
				datatemp[0010] = getCatalog("K4062", k0007_0010_0014_1086.Substring(11, 3)); // 工位号
			}
			// 照日期-班次-线体号-流水号
			else if (k0007_0010_0014_1086.Length == 17)
			{
				string[] parts = k0007_0010_0014_1086.Split('-');
				// qf[1086] = k0007_0010_0014_1086.Substring(11, 3);
				datatemp[0007] = getCatalog("K4252", parts[2]);  // 线体号
				// datatemp[0010] = getCatalog("K4062", k0007_0010_0014_1086.Substring(11, 3)); // 工位号
			}
			else
			{
				AddLog(path, "组合参数信息长度不符合规范，转换失败。");
				return false;
			}

			datatemp[0008] = getCatalog("K4092", k0008);
			datatemp[0012] = getCatalog("K4072", k0012);
			datatemp[0014] = k0007_0010_0014_1086; //

			for (int i = 12; i < body.Rows.Count; i++)
			{
				DataRow dr = body.Rows[i];
				QCharacteristic ch = new QCharacteristic();
				qf.Charactericstics.Add(ch);
				ch[2001] = i;
				ch[2002] = dr[0];
				ch[2022] = 8;
				ch[2101] = dr[2];
				ch[2112] = dr[4];
				ch[2113] = dr[3];
				ch[2120] = 1;
				ch[2121] = 1;
				ch[2142] = "mm";
				ch[8500] = 5;
				ch[8501] = 5;

				QDataItem di = datatemp.Clone();
				di.SetValue(dr[1]);
				ch.data.Add(di);
			}

			qf.ToDMode();

			return SaveDfqByInpath(qf, path);
		}

		private string getDate(string v)
		{
			string date = v.Trim();
			string[] month1 = { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月", };
			string[] month2 = { "一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月", };
			// AddLog(path, "Date: " + date, LogType.Debug);
			string[] sects = date.Split('-');

			for (int i = 0; i < 12; i++)
			{
				if (sects[1] == month1[i])
				{
					sects[1] = month2[i];
					break;
				}
			}

			date = "20" + sects[2] + "-" + sects[1] + "-" + sects[0];

			return date;
		}

		void showTable(DataTable dt)
		{
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					Console.Write(dt.Rows[i][j] + ", ");
				}
				Console.WriteLine();
			}
		}
	}
}
