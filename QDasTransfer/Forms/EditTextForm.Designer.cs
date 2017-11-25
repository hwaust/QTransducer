namespace QDasTransfer.Forms
{
	partial class EditTextForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtK2003 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.lblK2002 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtK2003
			// 
			this.txtK2003.Location = new System.Drawing.Point(124, 42);
			this.txtK2003.Name = "txtK2003";
			this.txtK2003.Size = new System.Drawing.Size(132, 21);
			this.txtK2003.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(33, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "基准项(K2002)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(95, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "基准参数(K2003)";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(180, 78);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(76, 26);
			this.button1.TabIndex = 3;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// lblK2002
			// 
			this.lblK2002.AutoSize = true;
			this.lblK2002.Location = new System.Drawing.Point(122, 18);
			this.lblK2002.Name = "lblK2002";
			this.lblK2002.Size = new System.Drawing.Size(83, 12);
			this.lblK2002.TabIndex = 4;
			this.lblK2002.Text = "基准项(K2002)";
			// 
			// EditTextForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(298, 116);
			this.Controls.Add(this.lblK2002);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtK2003);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditTextForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "参考基准";
			this.Load += new System.EventHandler(this.EditTextForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtK2003;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label lblK2002;
	}
}