using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QTrans.Classes
{
	public class ListViewData
	{

		public int[] ColWidths { get; set; }

		public ListViewData()
		{

		}

		public ListViewData(ListView lv)
		{
			GetData(lv);
		}

		public void GetData(ListView lv)
		{
			ColWidths = new int[lv.Columns.Count];
			for (int i = 0; i < lv.Columns.Count; i++)
			{
				ColWidths[i] = lv.Columns[i].Width;
			}
		}

		public void SetData(ListView lv)
		{
			if (ColWidths != null || ColWidths.Length == lv.Columns.Count)
				for (int i = 0; i < lv.Columns.Count; i++)
					lv.Columns[i].Width = ColWidths[i];
		}


	}
}
