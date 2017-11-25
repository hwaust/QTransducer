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
	public partial class EditTextForm : Form
	{
		public bool Confirmed = false;

		public EditTextForm()
		{
			InitializeComponent();
		}

		public void SetValue(string k2002, string k2003)
		{
			lblK2002.Text = k2002;
			txtK2003.Text = k2003;
		}

		public string getK20022003()
		{
			return lblK2002.Text +"="+ txtK2003.Text;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Confirmed = true;
			Close();
		}

		private void EditTextForm_Load(object sender, EventArgs e)
		{
			txtK2003.Select();
		}
	}
}
