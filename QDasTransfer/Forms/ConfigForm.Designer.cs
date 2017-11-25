﻿namespace QDasTransfer.Forms
{
	partial class ConfigForm
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.pgOutput = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.rbIncrementIndex = new System.Windows.Forms.RadioButton();
			this.label8 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.rbReplaceOld = new System.Windows.Forms.RadioButton();
			this.rbAddTimeToNew = new System.Windows.Forms.RadioButton();
			this.numlblKeepFolderLevel = new System.Windows.Forms.NumericUpDown();
			this.lblKeepFolderStruct = new System.Windows.Forms.Label();
			this.ckKeepFolderStruct = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtTempFolder = new System.Windows.Forms.TextBox();
			this.btnTempFolder = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtOutputFolder = new System.Windows.Forms.TextBox();
			this.btOutputFolder = new System.Windows.Forms.Button();
			this.pgBackups = new System.Windows.Forms.TabPage();
			this.label9 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.rbDelete = new System.Windows.Forms.RadioButton();
			this.rbBackup = new System.Windows.Forms.RadioButton();
			this.rbNoChange = new System.Windows.Forms.RadioButton();
			this.gbBackupFolders = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSuccessfulFolder = new System.Windows.Forms.Button();
			this.txtFailedFolder = new System.Windows.Forms.TextBox();
			this.txtSuccessfulFolder = new System.Windows.Forms.TextBox();
			this.btnFailedFolder = new System.Windows.Forms.Button();
			this.pgAutoTransducer = new System.Windows.Forms.TabPage();
			this.gbAutoConfig = new System.Windows.Forms.GroupBox();
			this.cbCircleUnit = new System.Windows.Forms.ComboBox();
			this.dpEndTime = new System.Windows.Forms.DateTimePicker();
			this.dpStartTime = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.numCircleValue = new System.Windows.Forms.NumericUpDown();
			this.ckAutoTransduce = new System.Windows.Forms.CheckBox();
			this.pgOthers = new System.Windows.Forms.TabPage();
			this.ckTraverseSubfolders = new System.Windows.Forms.CheckBox();
			this.ckRunAfterStart = new System.Windows.Forms.CheckBox();
			this.ckAutoStart = new System.Windows.Forms.CheckBox();
			this.label16 = new System.Windows.Forms.Label();
			this.ckConfirmClose = new System.Windows.Forms.CheckBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pgEncoding = new System.Windows.Forms.TabPage();
			this.label12 = new System.Windows.Forms.Label();
			this.lbEncodings = new System.Windows.Forms.ListBox();
			this.tabControl1.SuspendLayout();
			this.pgOutput.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numlblKeepFolderLevel)).BeginInit();
			this.pgBackups.SuspendLayout();
			this.gbBackupFolders.SuspendLayout();
			this.pgAutoTransducer.SuspendLayout();
			this.gbAutoConfig.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numCircleValue)).BeginInit();
			this.pgOthers.SuspendLayout();
			this.pgEncoding.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.pgOutput);
			this.tabControl1.Controls.Add(this.pgBackups);
			this.tabControl1.Controls.Add(this.pgAutoTransducer);
			this.tabControl1.Controls.Add(this.pgEncoding);
			this.tabControl1.Controls.Add(this.pgOthers);
			this.tabControl1.Location = new System.Drawing.Point(16, 15);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(920, 385);
			this.tabControl1.TabIndex = 0;
			// 
			// pgOutput
			// 
			this.pgOutput.Controls.Add(this.groupBox2);
			this.pgOutput.Controls.Add(this.numlblKeepFolderLevel);
			this.pgOutput.Controls.Add(this.lblKeepFolderStruct);
			this.pgOutput.Controls.Add(this.ckKeepFolderStruct);
			this.pgOutput.Controls.Add(this.label2);
			this.pgOutput.Controls.Add(this.txtTempFolder);
			this.pgOutput.Controls.Add(this.btnTempFolder);
			this.pgOutput.Controls.Add(this.label1);
			this.pgOutput.Controls.Add(this.txtOutputFolder);
			this.pgOutput.Controls.Add(this.btOutputFolder);
			this.pgOutput.Location = new System.Drawing.Point(4, 25);
			this.pgOutput.Margin = new System.Windows.Forms.Padding(4);
			this.pgOutput.Name = "pgOutput";
			this.pgOutput.Padding = new System.Windows.Forms.Padding(4);
			this.pgOutput.Size = new System.Drawing.Size(912, 356);
			this.pgOutput.TabIndex = 0;
			this.pgOutput.Text = "输出选项";
			this.pgOutput.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.rbIncrementIndex);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.rbReplaceOld);
			this.groupBox2.Controls.Add(this.rbAddTimeToNew);
			this.groupBox2.Location = new System.Drawing.Point(29, 164);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox2.Size = new System.Drawing.Size(851, 79);
			this.groupBox2.TabIndex = 49;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "存在同名文件的处理方式";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label10.ForeColor = System.Drawing.Color.Black;
			this.label10.Location = new System.Drawing.Point(612, 49);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(161, 14);
			this.label10.TabIndex = 5;
			this.label10.Text = "如: 1.dfq -> 1_001.dfq";
			// 
			// rbIncrementIndex
			// 
			this.rbIncrementIndex.AutoSize = true;
			this.rbIncrementIndex.Location = new System.Drawing.Point(600, 25);
			this.rbIncrementIndex.Margin = new System.Windows.Forms.Padding(4);
			this.rbIncrementIndex.Name = "rbIncrementIndex";
			this.rbIncrementIndex.Size = new System.Drawing.Size(103, 19);
			this.rbIncrementIndex.TabIndex = 4;
			this.rbIncrementIndex.Text = "加增量编号";
			this.rbIncrementIndex.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label8.ForeColor = System.Drawing.Color.Black;
			this.label8.Location = new System.Drawing.Point(280, 49);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(238, 14);
			this.label8.TabIndex = 3;
			this.label8.Text = "如: 1.dfq -> 1_20120516183025.dfq";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("宋体", 8F);
			this.label11.Location = new System.Drawing.Point(37, 49);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(182, 14);
			this.label11.TabIndex = 2;
			this.label11.Text = "用新生成的文件替换原文件.";
			// 
			// rbReplaceOld
			// 
			this.rbReplaceOld.AutoSize = true;
			this.rbReplaceOld.Checked = true;
			this.rbReplaceOld.Location = new System.Drawing.Point(21, 25);
			this.rbReplaceOld.Margin = new System.Windows.Forms.Padding(4);
			this.rbReplaceOld.Name = "rbReplaceOld";
			this.rbReplaceOld.Size = new System.Drawing.Size(88, 19);
			this.rbReplaceOld.TabIndex = 0;
			this.rbReplaceOld.TabStop = true;
			this.rbReplaceOld.Text = "直接覆盖";
			this.rbReplaceOld.UseVisualStyleBackColor = true;
			// 
			// rbAddTimeToNew
			// 
			this.rbAddTimeToNew.AutoSize = true;
			this.rbAddTimeToNew.Location = new System.Drawing.Point(268, 25);
			this.rbAddTimeToNew.Margin = new System.Windows.Forms.Padding(4);
			this.rbAddTimeToNew.Name = "rbAddTimeToNew";
			this.rbAddTimeToNew.Size = new System.Drawing.Size(88, 19);
			this.rbAddTimeToNew.TabIndex = 1;
			this.rbAddTimeToNew.Text = "加时间戳";
			this.rbAddTimeToNew.UseVisualStyleBackColor = true;
			// 
			// numlblKeepFolderLevel
			// 
			this.numlblKeepFolderLevel.Enabled = false;
			this.numlblKeepFolderLevel.Location = new System.Drawing.Point(127, 295);
			this.numlblKeepFolderLevel.Margin = new System.Windows.Forms.Padding(4);
			this.numlblKeepFolderLevel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numlblKeepFolderLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numlblKeepFolderLevel.Name = "numlblKeepFolderLevel";
			this.numlblKeepFolderLevel.Size = new System.Drawing.Size(87, 25);
			this.numlblKeepFolderLevel.TabIndex = 48;
			this.numlblKeepFolderLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numlblKeepFolderLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblKeepFolderStruct
			// 
			this.lblKeepFolderStruct.AutoSize = true;
			this.lblKeepFolderStruct.Location = new System.Drawing.Point(48, 300);
			this.lblKeepFolderStruct.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblKeepFolderStruct.Name = "lblKeepFolderStruct";
			this.lblKeepFolderStruct.Size = new System.Drawing.Size(67, 15);
			this.lblKeepFolderStruct.TabIndex = 47;
			this.lblKeepFolderStruct.Text = "保持级数";
			// 
			// ckKeepFolderStruct
			// 
			this.ckKeepFolderStruct.AutoSize = true;
			this.ckKeepFolderStruct.Location = new System.Drawing.Point(29, 261);
			this.ckKeepFolderStruct.Margin = new System.Windows.Forms.Padding(4);
			this.ckKeepFolderStruct.Name = "ckKeepFolderStruct";
			this.ckKeepFolderStruct.Size = new System.Drawing.Size(164, 19);
			this.ckKeepFolderStruct.TabIndex = 46;
			this.ckKeepFolderStruct.Text = "是否保留原目录结构";
			this.ckKeepFolderStruct.UseVisualStyleBackColor = true;
			this.ckKeepFolderStruct.Click += new System.EventHandler(this.ckKeepFolderStruct_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(29, 96);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(300, 15);
			this.label2.TabIndex = 16;
			this.label2.Text = "临时文件夹 （用于保存转换时的临时数据）";
			// 
			// txtTempFolder
			// 
			this.txtTempFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTempFolder.Location = new System.Drawing.Point(32, 115);
			this.txtTempFolder.Margin = new System.Windows.Forms.Padding(4);
			this.txtTempFolder.Name = "txtTempFolder";
			this.txtTempFolder.Size = new System.Drawing.Size(757, 25);
			this.txtTempFolder.TabIndex = 14;
			this.txtTempFolder.Text = "D:\\QDAS\\Temp";
			// 
			// btnTempFolder
			// 
			this.btnTempFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTempFolder.Location = new System.Drawing.Point(799, 115);
			this.btnTempFolder.Margin = new System.Windows.Forms.Padding(4);
			this.btnTempFolder.Name = "btnTempFolder";
			this.btnTempFolder.Size = new System.Drawing.Size(81, 26);
			this.btnTempFolder.TabIndex = 15;
			this.btnTempFolder.Text = "选择...";
			this.btnTempFolder.UseVisualStyleBackColor = true;
			this.btnTempFolder.Click += new System.EventHandler(this.btnTempFolder_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(29, 29);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(270, 15);
			this.label1.TabIndex = 13;
			this.label1.Text = "输出文件夹 （转换好的文件输出位置）";
			// 
			// txtOutputFolder
			// 
			this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutputFolder.Location = new System.Drawing.Point(32, 48);
			this.txtOutputFolder.Margin = new System.Windows.Forms.Padding(4);
			this.txtOutputFolder.Name = "txtOutputFolder";
			this.txtOutputFolder.Size = new System.Drawing.Size(757, 25);
			this.txtOutputFolder.TabIndex = 11;
			this.txtOutputFolder.Text = "D:\\QDAS\\Output";
			// 
			// btOutputFolder
			// 
			this.btOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btOutputFolder.Location = new System.Drawing.Point(799, 46);
			this.btOutputFolder.Margin = new System.Windows.Forms.Padding(4);
			this.btOutputFolder.Name = "btOutputFolder";
			this.btOutputFolder.Size = new System.Drawing.Size(81, 26);
			this.btOutputFolder.TabIndex = 12;
			this.btOutputFolder.Text = "选择...";
			this.btOutputFolder.UseVisualStyleBackColor = true;
			this.btOutputFolder.Click += new System.EventHandler(this.btOutputFolder_Click);
			// 
			// pgBackups
			// 
			this.pgBackups.Controls.Add(this.label9);
			this.pgBackups.Controls.Add(this.label7);
			this.pgBackups.Controls.Add(this.rbDelete);
			this.pgBackups.Controls.Add(this.rbBackup);
			this.pgBackups.Controls.Add(this.rbNoChange);
			this.pgBackups.Controls.Add(this.gbBackupFolders);
			this.pgBackups.Location = new System.Drawing.Point(4, 25);
			this.pgBackups.Margin = new System.Windows.Forms.Padding(4);
			this.pgBackups.Name = "pgBackups";
			this.pgBackups.Padding = new System.Windows.Forms.Padding(4);
			this.pgBackups.Size = new System.Drawing.Size(912, 356);
			this.pgBackups.TabIndex = 1;
			this.pgBackups.Text = "原文件处理方式";
			this.pgBackups.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(53, 285);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(502, 15);
			this.label9.TabIndex = 45;
			this.label9.Text = "转换后直接删除原文件。主要用于一些程序产生的重要性不高的过程文件。";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(53, 219);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(292, 15);
			this.label7.TabIndex = 44;
			this.label7.Text = "不对原文件进行处理。主要用于增量转换。";
			// 
			// rbDelete
			// 
			this.rbDelete.AutoSize = true;
			this.rbDelete.Location = new System.Drawing.Point(29, 260);
			this.rbDelete.Margin = new System.Windows.Forms.Padding(4);
			this.rbDelete.Name = "rbDelete";
			this.rbDelete.Size = new System.Drawing.Size(88, 19);
			this.rbDelete.TabIndex = 43;
			this.rbDelete.Text = "直接删除";
			this.rbDelete.UseVisualStyleBackColor = true;
			this.rbDelete.CheckedChanged += new System.EventHandler(this.rbDelete_CheckedChanged);
			// 
			// rbBackup
			// 
			this.rbBackup.AutoSize = true;
			this.rbBackup.Checked = true;
			this.rbBackup.Location = new System.Drawing.Point(29, 8);
			this.rbBackup.Margin = new System.Windows.Forms.Padding(4);
			this.rbBackup.Name = "rbBackup";
			this.rbBackup.Size = new System.Drawing.Size(148, 19);
			this.rbBackup.TabIndex = 42;
			this.rbBackup.TabStop = true;
			this.rbBackup.Text = "移动至指定文件夹";
			this.rbBackup.UseVisualStyleBackColor = true;
			this.rbBackup.CheckedChanged += new System.EventHandler(this.rbBackup_CheckedChanged);
			// 
			// rbNoChange
			// 
			this.rbNoChange.AutoSize = true;
			this.rbNoChange.Location = new System.Drawing.Point(29, 195);
			this.rbNoChange.Margin = new System.Windows.Forms.Padding(4);
			this.rbNoChange.Name = "rbNoChange";
			this.rbNoChange.Size = new System.Drawing.Size(88, 19);
			this.rbNoChange.TabIndex = 41;
			this.rbNoChange.Text = "保持不变";
			this.rbNoChange.UseVisualStyleBackColor = true;
			this.rbNoChange.CheckedChanged += new System.EventHandler(this.rbNoChange_CheckedChanged);
			// 
			// gbBackupFolders
			// 
			this.gbBackupFolders.Controls.Add(this.label4);
			this.gbBackupFolders.Controls.Add(this.label3);
			this.gbBackupFolders.Controls.Add(this.btnSuccessfulFolder);
			this.gbBackupFolders.Controls.Add(this.txtFailedFolder);
			this.gbBackupFolders.Controls.Add(this.txtSuccessfulFolder);
			this.gbBackupFolders.Controls.Add(this.btnFailedFolder);
			this.gbBackupFolders.Enabled = false;
			this.gbBackupFolders.Location = new System.Drawing.Point(56, 35);
			this.gbBackupFolders.Margin = new System.Windows.Forms.Padding(4);
			this.gbBackupFolders.Name = "gbBackupFolders";
			this.gbBackupFolders.Padding = new System.Windows.Forms.Padding(4);
			this.gbBackupFolders.Size = new System.Drawing.Size(853, 152);
			this.gbBackupFolders.TabIndex = 39;
			this.gbBackupFolders.TabStop = false;
			this.gbBackupFolders.Text = "移动输入文件至以下文件夹";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(19, 28);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(300, 15);
			this.label4.TabIndex = 19;
			this.label4.Text = "成功文件夹 （用于保存转换成功的文件夹）";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(19, 96);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(300, 15);
			this.label3.TabIndex = 22;
			this.label3.Text = "失败文件夹 （用于保存转换失败的文件夹）";
			// 
			// btnSuccessfulFolder
			// 
			this.btnSuccessfulFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSuccessfulFolder.Location = new System.Drawing.Point(749, 46);
			this.btnSuccessfulFolder.Margin = new System.Windows.Forms.Padding(4);
			this.btnSuccessfulFolder.Name = "btnSuccessfulFolder";
			this.btnSuccessfulFolder.Size = new System.Drawing.Size(81, 26);
			this.btnSuccessfulFolder.TabIndex = 18;
			this.btnSuccessfulFolder.Text = "选择...";
			this.btnSuccessfulFolder.UseVisualStyleBackColor = true;
			this.btnSuccessfulFolder.Click += new System.EventHandler(this.btnSuccessfulFolder_Click);
			// 
			// txtFailedFolder
			// 
			this.txtFailedFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFailedFolder.Location = new System.Drawing.Point(21, 115);
			this.txtFailedFolder.Margin = new System.Windows.Forms.Padding(4);
			this.txtFailedFolder.Name = "txtFailedFolder";
			this.txtFailedFolder.Size = new System.Drawing.Size(719, 25);
			this.txtFailedFolder.TabIndex = 20;
			this.txtFailedFolder.Text = "D:\\QDAS\\Backups\\Failed";
			// 
			// txtSuccessfulFolder
			// 
			this.txtSuccessfulFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSuccessfulFolder.Location = new System.Drawing.Point(21, 46);
			this.txtSuccessfulFolder.Margin = new System.Windows.Forms.Padding(4);
			this.txtSuccessfulFolder.Name = "txtSuccessfulFolder";
			this.txtSuccessfulFolder.Size = new System.Drawing.Size(719, 25);
			this.txtSuccessfulFolder.TabIndex = 17;
			this.txtSuccessfulFolder.Text = "D:\\QDAS\\Backups\\Success";
			// 
			// btnFailedFolder
			// 
			this.btnFailedFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFailedFolder.Location = new System.Drawing.Point(749, 115);
			this.btnFailedFolder.Margin = new System.Windows.Forms.Padding(4);
			this.btnFailedFolder.Name = "btnFailedFolder";
			this.btnFailedFolder.Size = new System.Drawing.Size(81, 26);
			this.btnFailedFolder.TabIndex = 21;
			this.btnFailedFolder.Text = "选择...";
			this.btnFailedFolder.UseVisualStyleBackColor = true;
			this.btnFailedFolder.Click += new System.EventHandler(this.btnFailedFolder_Click);
			// 
			// pgAutoTransducer
			// 
			this.pgAutoTransducer.Controls.Add(this.gbAutoConfig);
			this.pgAutoTransducer.Controls.Add(this.ckAutoTransduce);
			this.pgAutoTransducer.Location = new System.Drawing.Point(4, 25);
			this.pgAutoTransducer.Margin = new System.Windows.Forms.Padding(4);
			this.pgAutoTransducer.Name = "pgAutoTransducer";
			this.pgAutoTransducer.Padding = new System.Windows.Forms.Padding(4);
			this.pgAutoTransducer.Size = new System.Drawing.Size(912, 356);
			this.pgAutoTransducer.TabIndex = 5;
			this.pgAutoTransducer.Text = "自动转换";
			this.pgAutoTransducer.UseVisualStyleBackColor = true;
			// 
			// gbAutoConfig
			// 
			this.gbAutoConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbAutoConfig.Controls.Add(this.cbCircleUnit);
			this.gbAutoConfig.Controls.Add(this.dpEndTime);
			this.gbAutoConfig.Controls.Add(this.dpStartTime);
			this.gbAutoConfig.Controls.Add(this.label5);
			this.gbAutoConfig.Controls.Add(this.label6);
			this.gbAutoConfig.Controls.Add(this.label17);
			this.gbAutoConfig.Controls.Add(this.numCircleValue);
			this.gbAutoConfig.Enabled = false;
			this.gbAutoConfig.Location = new System.Drawing.Point(48, 45);
			this.gbAutoConfig.Margin = new System.Windows.Forms.Padding(4);
			this.gbAutoConfig.Name = "gbAutoConfig";
			this.gbAutoConfig.Padding = new System.Windows.Forms.Padding(4);
			this.gbAutoConfig.Size = new System.Drawing.Size(853, 116);
			this.gbAutoConfig.TabIndex = 27;
			this.gbAutoConfig.TabStop = false;
			this.gbAutoConfig.Text = "自动转换配置项";
			// 
			// cbCircleUnit
			// 
			this.cbCircleUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCircleUnit.FormattingEnabled = true;
			this.cbCircleUnit.Items.AddRange(new object[] {
            "秒",
            "分钟",
            "小时"});
			this.cbCircleUnit.Location = new System.Drawing.Point(169, 32);
			this.cbCircleUnit.Margin = new System.Windows.Forms.Padding(4);
			this.cbCircleUnit.Name = "cbCircleUnit";
			this.cbCircleUnit.Size = new System.Drawing.Size(121, 23);
			this.cbCircleUnit.TabIndex = 35;
			// 
			// dpEndTime
			// 
			this.dpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dpEndTime.Location = new System.Drawing.Point(328, 72);
			this.dpEndTime.Margin = new System.Windows.Forms.Padding(4);
			this.dpEndTime.Name = "dpEndTime";
			this.dpEndTime.ShowUpDown = true;
			this.dpEndTime.Size = new System.Drawing.Size(101, 25);
			this.dpEndTime.TabIndex = 33;
			this.dpEndTime.Value = new System.DateTime(2012, 6, 12, 23, 59, 59, 0);
			// 
			// dpStartTime
			// 
			this.dpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dpStartTime.Location = new System.Drawing.Point(88, 72);
			this.dpStartTime.Margin = new System.Windows.Forms.Padding(4);
			this.dpStartTime.Name = "dpStartTime";
			this.dpStartTime.ShowUpDown = true;
			this.dpStartTime.Size = new System.Drawing.Size(124, 25);
			this.dpStartTime.TabIndex = 32;
			this.dpStartTime.Value = new System.DateTime(2012, 6, 12, 0, 0, 0, 0);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(249, 80);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 15);
			this.label5.TabIndex = 27;
			this.label5.Text = "结束时间";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(9, 78);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(67, 15);
			this.label6.TabIndex = 26;
			this.label6.Text = "开始时间";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(9, 35);
			this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(67, 15);
			this.label17.TabIndex = 24;
			this.label17.Text = "监控周期";
			// 
			// numCircleValue
			// 
			this.numCircleValue.Location = new System.Drawing.Point(88, 31);
			this.numCircleValue.Margin = new System.Windows.Forms.Padding(4);
			this.numCircleValue.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numCircleValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numCircleValue.Name = "numCircleValue";
			this.numCircleValue.Size = new System.Drawing.Size(75, 25);
			this.numCircleValue.TabIndex = 23;
			this.numCircleValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numCircleValue.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// ckAutoTransduce
			// 
			this.ckAutoTransduce.AutoSize = true;
			this.ckAutoTransduce.Location = new System.Drawing.Point(21, 20);
			this.ckAutoTransduce.Margin = new System.Windows.Forms.Padding(4);
			this.ckAutoTransduce.Name = "ckAutoTransduce";
			this.ckAutoTransduce.Size = new System.Drawing.Size(149, 19);
			this.ckAutoTransduce.TabIndex = 26;
			this.ckAutoTransduce.Text = "是否开启自动转换";
			this.ckAutoTransduce.UseVisualStyleBackColor = true;
			this.ckAutoTransduce.Click += new System.EventHandler(this.ckAutoTransfer_Click);
			// 
			// pgOthers
			// 
			this.pgOthers.Controls.Add(this.ckTraverseSubfolders);
			this.pgOthers.Controls.Add(this.ckRunAfterStart);
			this.pgOthers.Controls.Add(this.ckAutoStart);
			this.pgOthers.Controls.Add(this.label16);
			this.pgOthers.Controls.Add(this.ckConfirmClose);
			this.pgOthers.Location = new System.Drawing.Point(4, 25);
			this.pgOthers.Margin = new System.Windows.Forms.Padding(4);
			this.pgOthers.Name = "pgOthers";
			this.pgOthers.Padding = new System.Windows.Forms.Padding(4);
			this.pgOthers.Size = new System.Drawing.Size(912, 356);
			this.pgOthers.TabIndex = 7;
			this.pgOthers.Text = "其他";
			this.pgOthers.UseVisualStyleBackColor = true;
			// 
			// ckTraverseSubfolders
			// 
			this.ckTraverseSubfolders.AutoSize = true;
			this.ckTraverseSubfolders.Location = new System.Drawing.Point(35, 27);
			this.ckTraverseSubfolders.Margin = new System.Windows.Forms.Padding(4);
			this.ckTraverseSubfolders.Name = "ckTraverseSubfolders";
			this.ckTraverseSubfolders.Size = new System.Drawing.Size(179, 19);
			this.ckTraverseSubfolders.TabIndex = 31;
			this.ckTraverseSubfolders.Text = "转换目录时遍历子目录";
			this.ckTraverseSubfolders.UseVisualStyleBackColor = true;
			// 
			// ckRunAfterStart
			// 
			this.ckRunAfterStart.AutoSize = true;
			this.ckRunAfterStart.Location = new System.Drawing.Point(35, 128);
			this.ckRunAfterStart.Margin = new System.Windows.Forms.Padding(4);
			this.ckRunAfterStart.Name = "ckRunAfterStart";
			this.ckRunAfterStart.Size = new System.Drawing.Size(164, 19);
			this.ckRunAfterStart.TabIndex = 30;
			this.ckRunAfterStart.Text = "启动后立即开始转换";
			this.ckRunAfterStart.UseVisualStyleBackColor = true;
			// 
			// ckAutoStart
			// 
			this.ckAutoStart.AutoSize = true;
			this.ckAutoStart.Location = new System.Drawing.Point(35, 176);
			this.ckAutoStart.Margin = new System.Windows.Forms.Padding(4);
			this.ckAutoStart.Name = "ckAutoStart";
			this.ckAutoStart.Size = new System.Drawing.Size(134, 19);
			this.ckAutoStart.TabIndex = 28;
			this.ckAutoStart.Text = "开机时自动启动";
			this.ckAutoStart.UseVisualStyleBackColor = true;
			this.ckAutoStart.Visible = false;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("宋体", 8F);
			this.label16.Location = new System.Drawing.Point(40, 200);
			this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(413, 14);
			this.label16.TabIndex = 29;
			this.label16.Text = "（注意：此项改动会影响注册表，从而可能会引起安全软件报警）";
			this.label16.Visible = false;
			// 
			// ckConfirmClose
			// 
			this.ckConfirmClose.AutoSize = true;
			this.ckConfirmClose.Location = new System.Drawing.Point(35, 76);
			this.ckConfirmClose.Margin = new System.Windows.Forms.Padding(4);
			this.ckConfirmClose.Name = "ckConfirmClose";
			this.ckConfirmClose.Size = new System.Drawing.Size(134, 19);
			this.ckConfirmClose.TabIndex = 27;
			this.ckConfirmClose.Text = "关闭时进行确认";
			this.ckConfirmClose.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(640, 408);
			this.btnSave.Margin = new System.Windows.Forms.Padding(4);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(141, 42);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "保存";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(789, 408);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(141, 42);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// pgEncoding
			// 
			this.pgEncoding.Controls.Add(this.lbEncodings);
			this.pgEncoding.Controls.Add(this.label12);
			this.pgEncoding.Location = new System.Drawing.Point(4, 25);
			this.pgEncoding.Name = "pgEncoding";
			this.pgEncoding.Padding = new System.Windows.Forms.Padding(3);
			this.pgEncoding.Size = new System.Drawing.Size(912, 356);
			this.pgEncoding.TabIndex = 8;
			this.pgEncoding.Text = "编码";
			this.pgEncoding.UseVisualStyleBackColor = true;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(17, 13);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(347, 15);
			this.label12.TabIndex = 0;
			this.label12.Text = "输入文件编码 （注：输出编码为QDAS识别编码。）";
			// 
			// lbEncodings
			// 
			this.lbEncodings.FormattingEnabled = true;
			this.lbEncodings.ItemHeight = 15;
			this.lbEncodings.Items.AddRange(new object[] {
            "Default",
            "BIG5",
            "Unicode",
            "UTF-8",
            "UTF-16"});
			this.lbEncodings.Location = new System.Drawing.Point(20, 46);
			this.lbEncodings.Name = "lbEncodings";
			this.lbEncodings.Size = new System.Drawing.Size(344, 259);
			this.lbEncodings.TabIndex = 1;
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(952, 465);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfigForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "综合设置";
			this.Load += new System.EventHandler(this.ConfigForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.pgOutput.ResumeLayout(false);
			this.pgOutput.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numlblKeepFolderLevel)).EndInit();
			this.pgBackups.ResumeLayout(false);
			this.pgBackups.PerformLayout();
			this.gbBackupFolders.ResumeLayout(false);
			this.gbBackupFolders.PerformLayout();
			this.pgAutoTransducer.ResumeLayout(false);
			this.pgAutoTransducer.PerformLayout();
			this.gbAutoConfig.ResumeLayout(false);
			this.gbAutoConfig.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numCircleValue)).EndInit();
			this.pgOthers.ResumeLayout(false);
			this.pgOthers.PerformLayout();
			this.pgEncoding.ResumeLayout(false);
			this.pgEncoding.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage pgOutput;
		private System.Windows.Forms.TabPage pgBackups;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtOutputFolder;
		private System.Windows.Forms.Button btOutputFolder;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtTempFolder;
		private System.Windows.Forms.Button btnTempFolder;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage pgAutoTransducer;
		private System.Windows.Forms.GroupBox gbAutoConfig;
		private System.Windows.Forms.ComboBox cbCircleUnit;
		private System.Windows.Forms.DateTimePicker dpEndTime;
		private System.Windows.Forms.DateTimePicker dpStartTime;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.NumericUpDown numCircleValue;
		private System.Windows.Forms.CheckBox ckAutoTransduce;
		private System.Windows.Forms.GroupBox gbBackupFolders;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnSuccessfulFolder;
		private System.Windows.Forms.TextBox txtFailedFolder;
		private System.Windows.Forms.TextBox txtSuccessfulFolder;
		private System.Windows.Forms.Button btnFailedFolder;
		private System.Windows.Forms.TabPage pgOthers;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.RadioButton rbReplaceOld;
		private System.Windows.Forms.RadioButton rbAddTimeToNew;
		private System.Windows.Forms.NumericUpDown numlblKeepFolderLevel;
		private System.Windows.Forms.Label lblKeepFolderStruct;
		private System.Windows.Forms.CheckBox ckKeepFolderStruct;
		private System.Windows.Forms.CheckBox ckRunAfterStart;
		private System.Windows.Forms.CheckBox ckAutoStart;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.CheckBox ckConfirmClose;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.RadioButton rbDelete;
		private System.Windows.Forms.RadioButton rbBackup;
		private System.Windows.Forms.RadioButton rbNoChange;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.RadioButton rbIncrementIndex;
		private System.Windows.Forms.CheckBox ckTraverseSubfolders;
		private System.Windows.Forms.TabPage pgEncoding;
		private System.Windows.Forms.ListBox lbEncodings;
		private System.Windows.Forms.Label label12;
	}
}