using System;
using System.Collections.Generic;
using System.Text;

namespace QDAS
{
	/// <summary>
	/// 文件的模式，包括PMode和DMode两种模式。
	/// </summary>
	public enum QFileMode:int
	{
		/// <summary>
		/// 数据保存在参数的数据列表中。
		/// </summary>
		PMode,
		/// <summary>
		/// 数据保存在QFile中的数据列表中。
		/// </summary>
		DMode,
		/// <summary>
		/// 未指定。
		/// </summary>
		UnDefined
	}
}
