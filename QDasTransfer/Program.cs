using System;
using System.Windows.Forms;

namespace QDasTransfer
{
    static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Control.CheckForIllegalCrossThreadCalls = false;

            values.transducer =  new QTrans.Company.Y2017.T201708_Antai_Density();

            //启动主窗体。
            NewMainForm mf = new NewMainForm();
			Application.Run(mf); 


		}

         

	}
}
