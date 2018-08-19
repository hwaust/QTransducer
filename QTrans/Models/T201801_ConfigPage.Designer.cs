namespace QTrans.Models
{
    partial class T2018ConfigPage
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.dpDateTime = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "K0004 (时间)";
            // 
            // dpDateTime
            // 
            this.dpDateTime.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dpDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpDateTime.Location = new System.Drawing.Point(122, 16);
            this.dpDateTime.Name = "dpDateTime";
            this.dpDateTime.Size = new System.Drawing.Size(204, 21);
            this.dpDateTime.TabIndex = 3;
            // 
            // T2018ConfigPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dpDateTime);
            this.Controls.Add(this.label2);
            this.Name = "T2018ConfigPage";
            this.Size = new System.Drawing.Size(406, 227);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dpDateTime;
    }
}
