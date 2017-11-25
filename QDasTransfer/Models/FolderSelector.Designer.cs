namespace QDasTransfer.Models
{
	partial class FolderSelector
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
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.txtOutputPath = new System.Windows.Forms.TextBox();
			this.btOutputPath = new System.Windows.Forms.Button();
			this.groupBox8.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox8
			// 
			this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox8.Controls.Add(this.txtOutputPath);
			this.groupBox8.Controls.Add(this.btOutputPath);
			this.groupBox8.Location = new System.Drawing.Point(3, 0);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(480, 50);
			this.groupBox8.TabIndex = 14;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "输出文件夹 （转换好的文件输出位置）";
			// 
			// txtOutputPath
			// 
			this.txtOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutputPath.Location = new System.Drawing.Point(16, 17);
			this.txtOutputPath.Name = "txtOutputPath";
			this.txtOutputPath.Size = new System.Drawing.Size(387, 21);
			this.txtOutputPath.TabIndex = 9;
			this.txtOutputPath.Text = "D:\\Q-DAS_FILES";
			// 
			// btOutputPath
			// 
			this.btOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btOutputPath.Location = new System.Drawing.Point(409, 14);
			this.btOutputPath.Name = "btOutputPath";
			this.btOutputPath.Size = new System.Drawing.Size(48, 24);
			this.btOutputPath.TabIndex = 10;
			this.btOutputPath.Text = "选择";
			this.btOutputPath.UseVisualStyleBackColor = true;
			// 
			// FolderSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox8);
			this.Name = "FolderSelector";
			this.Size = new System.Drawing.Size(486, 52);
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.TextBox txtOutputPath;
		private System.Windows.Forms.Button btOutputPath;
	}
}
