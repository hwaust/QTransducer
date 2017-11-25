using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QDasTransfer
{
    /// <summary>
    /// 用于数据输入，主要使用方法为 ShowForm
    /// </summary>
    public partial class InputBoxForm : Form
    {
        /// <summary>
        /// 标题所显示的内容。
        /// </summary>
        public string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        /// <summary>
        /// 数据。
        /// </summary>
        public string Data
        {
            get { return txtContent.Text; }
            set { txtContent.Text = value; }
        }

        protected bool confirmed = false;
        /// <summary>
        /// 是否已经确定。
        /// </summary>
        public bool Confirmed
        {
            get { return confirmed; }
            set { confirmed = value; }
        }

        /// <summary>
        /// 显示窗体，返回字符串，如果取消，则返回空字符串(String.Empty)
        /// </summary>
        /// <returns></returns>
        public string ShowForm()
        {
            this.ShowDialog();
            return txtContent.Text;
        }

        /// <summary>
        /// 显示窗体，返回字符串，如果取消，则返回空字符串(String.Empty)
        /// </summary>
        /// <param name="title">需要显示的窗体上的标签的名称。</param>
        /// <returns></returns>
        public string ShowForm(string title)
        {
            lblTitle.Text = title;
            this.ShowDialog();
            return txtContent.Text;
        }

        /// <summary>
        /// 显示窗体，返回字符串，如果取消，则返回空字符串(String.Empty)
        /// </summary>
        /// <param name="head">窗体的标题栏上的名称。</param>
        /// <param name="title">需要显示的窗体上的标签的名称。</param>
        /// <returns></returns>
        public string ShowForm(string head, string title)
        {
            lblTitle.Text = title;
            this.Text = head;
            this.ShowDialog();
            return txtContent.Text;
        }

        /// <summary>
        /// 用于数据输入，主要使用方法为 ShowForm
        /// </summary>
        public InputBoxForm()
        {
            InitializeComponent();
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            confirmed = false;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            confirmed = true;
            Close();
        }

        private void InputBoxForm_Load(object sender, EventArgs e)
        {

        }


    }
}
