using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindGoes.IO;

namespace QDasTransfer
{
	public partial class PasswordForm : Form
	{
		string psw = "";
		IniAccess ia = new IniAccess(values.config_ini);

		public PasswordForm()
		{
			InitializeComponent(); 
			ia.Section = "System";
			psw = ia.ReadValue("ClosePassword");
			txtPassword.Text = psw;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			ia.WriteValue("ClosePassword", txtPassword.Text);
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void PasswordForm_Load(object sender, EventArgs e)
		{
			txtPassword.Select();
		}
	}
}
