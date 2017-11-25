using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QDasTransfer.Forms
{
	public partial class LockerForm : Form
	{
		/// <summary>
		///  是否已经解锁.
		/// </summary>
		public bool Unlocked { get; set; }

		public LockerForm()
		{
			InitializeComponent();
		}

		private void btnUnlock_Click(object sender, EventArgs e)
		{
			if (true)
			{
				Unlocked = true;
				Close();
			}
		}
	}
}
