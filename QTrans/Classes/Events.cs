using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QTrans.Classes
{ 
	public delegate void TransFileCompleteEventHandler(object sender, TransLog e);
	public class TransFileCompleteArgs : EventArgs
	{
		/// <summary>
		/// 是否转换成功。
		/// </summary>
		public bool Successed { get; set; }

		/// <summary>
		/// 待转换的原文件。
		/// </summary>
		public string SourceFile { get; set; }
	}

}
