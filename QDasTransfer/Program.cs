﻿using System;
using System.Linq;
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

            values.initialize(); 

            //启动主窗体。
            NewMainForm mf = new NewMainForm();
			Application.Run(mf); 


		}

         

	}
}
