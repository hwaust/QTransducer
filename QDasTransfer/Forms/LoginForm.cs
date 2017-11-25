using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QDasTransfer
{
	public partial class LoginForm : Form
	{
		public LoginForm()
		{
			InitializeComponent();
		}


		int t = 0;
		private void timer1_Tick(object sender, EventArgs e)
		{
			t++;
			if (t < 200)
			{
				return;
			}

			Width = Width - 2;
			Height -= 2;

			if (Height < 10)
			{
				timer1.Stop();
				Close();
			}
		}

		Font font = new Font("微软雅黑", 16);//, FontStyle.Bold);
		SolidBrush brush = new SolidBrush(Color.Gray); 

		private void LoginForm_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			string cn = "Q-DAS Transfer For " + values.transducer.CompanyName;
			SizeF sf = g.MeasureString(cn, font);
			
			g.DrawString(cn , font, brush, new PointF(560 - sf.Width, 36));
		}

		private void LoginForm_Load(object sender, EventArgs e)
		{
			timer1.Start();
		}
	}
}
