
using QTrans;
using QTrans.Classes; 
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace QDasTransfer
{
	public class values
	{
		/// <summary>
		/// 配置路径。
		/// </summary>
		public static string config_xml = Application.StartupPath + "\\config.xml";
		/// <summary>
		/// 配置文件全路径。
		/// </summary>
		public static string config_ini = Application.StartupPath + "\\Config.ini";
		/// <summary>
		/// 转换类，全局使用唯一一个。
		/// </summary>
		public static TransferBase transducer;

	}
}
