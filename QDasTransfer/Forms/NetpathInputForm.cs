using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QDasTransfer.Forms
{
    public partial class NetpathInputForm : Form
    {
        /// <summary>
        /// 设定的网络路径，默认为空。
        /// </summary>
        public string NetPath
        {
            get
            {
                return txtContent.Text;
            }
        }

        public bool Confirmed { get; set; }

        public NetpathInputForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtContent.Text) && !File.Exists(txtContent.Text))
            {
                MessageBox.Show("路径不存在，请重新输入。");
                txtContent.Focus();
            }
            else
            {
                Confirmed = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NetpathInputForm_Load(object sender, EventArgs e)
        {
            txtContent.Focus();
            Confirmed = false;
        }

        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }
    }
}
