namespace QDasTransfer
{
	partial class NewMainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMainForm));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("C:\\Qdas\\data\\201502232", "Folder16X16.png");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("D:\\text.txt", 0);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnNewConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSaveConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnExportConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.生成RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiStartTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnTranduceSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.设置OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiSystemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tiClosingPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.关于HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tbMain = new System.Windows.Forms.TabControl();
            this.tpMainPage = new System.Windows.Forms.TabPage();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tiAddFile = new System.Windows.Forms.ToolStripButton();
            this.tiAddFolder = new System.Windows.Forms.ToolStripButton();
            this.tiAddPathManually = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tiSelectAll = new System.Windows.Forms.ToolStripButton();
            this.tiCancelAll = new System.Windows.Forms.ToolStripButton();
            this.tiSelectReverse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tiDeletePathes = new System.Windows.Forms.ToolStripButton();
            this.tiDeleteUnexisted = new System.Windows.Forms.ToolStripButton();
            this.mnFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnTransduce = new System.Windows.Forms.ToolStripMenuItem();
            this.mnOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnOpenInputFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tpOutputFilesPage = new System.Windows.Forms.TabPage();
            this.tiSaveOutputFiles = new System.Windows.Forms.ToolStrip();
            this.tiSaveResults = new System.Windows.Forms.ToolStripButton();
            this.tiClear = new System.Windows.Forms.ToolStripButton();
            this.tbLogPage = new System.Windows.Forms.TabPage();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tiOpenLogFile = new System.Windows.Forms.ToolStripButton();
            this.tiClearLogList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tiStart = new System.Windows.Forms.ToolStripSplitButton();
            this.tiTranduceAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tiTransduceSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tiStop = new System.Windows.Forms.ToolStripButton();
            this.tiOpenAppFolder = new System.Windows.Forms.ToolStripButton();
            this.tiOpenOutputFolder = new System.Windows.Forms.ToolStripButton();
            this.tiLock = new System.Windows.Forms.ToolStripButton();
            this.tiConfig = new System.Windows.Forms.ToolStripButton();
            this.lvInputList = new QDasTransfer.Classes.MyListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvResults = new QDasTransfer.Classes.MyListView();
            this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chInput = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chOutput = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvLogs = new QDasTransfer.Classes.MyListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEvent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.tbMain.SuspendLayout();
            this.tpMainPage.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.mnFolder.SuspendLayout();
            this.tpOutputFilesPage.SuspendLayout();
            this.tiSaveOutputFiles.SuspendLayout();
            this.tbLogPage.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "File16X18.png");
            this.imageList1.Images.SetKeyName(1, "Folder16X18.png");
            this.imageList1.Images.SetKeyName(2, "Tranduce16X16.png");
            this.imageList1.Images.SetKeyName(3, "RedCross16X16.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.生成RToolStripMenuItem,
            this.设置OToolStripMenuItem,
            this.关于HToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1006, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnNewConfig,
            this.mnSaveConfig,
            this.mnExportConfig,
            this.toolStripMenuItem2,
            this.mnExit});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // mnNewConfig
            // 
            this.mnNewConfig.Name = "mnNewConfig";
            this.mnNewConfig.Size = new System.Drawing.Size(142, 22);
            this.mnNewConfig.Text = "重置配置(&N)";
            this.mnNewConfig.Click += new System.EventHandler(this.mnNewConfig_Click);
            // 
            // mnSaveConfig
            // 
            this.mnSaveConfig.Name = "mnSaveConfig";
            this.mnSaveConfig.Size = new System.Drawing.Size(142, 22);
            this.mnSaveConfig.Text = "保存配置(&S)";
            this.mnSaveConfig.Click += new System.EventHandler(this.mnSaveConfig_Click);
            // 
            // mnExportConfig
            // 
            this.mnExportConfig.Name = "mnExportConfig";
            this.mnExportConfig.Size = new System.Drawing.Size(142, 22);
            this.mnExportConfig.Text = "导出配置(A)";
            this.mnExportConfig.Click += new System.EventHandler(this.mnExportConfig_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(139, 6);
            // 
            // mnExit
            // 
            this.mnExit.Name = "mnExit";
            this.mnExit.Size = new System.Drawing.Size(142, 22);
            this.mnExit.Text = "退出(&X)";
            // 
            // 生成RToolStripMenuItem
            // 
            this.生成RToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiStartTransfer,
            this.mnTranduceSelected});
            this.生成RToolStripMenuItem.Name = "生成RToolStripMenuItem";
            this.生成RToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.生成RToolStripMenuItem.Text = "生成(&R)";
            // 
            // tiStartTransfer
            // 
            this.tiStartTransfer.Name = "tiStartTransfer";
            this.tiStartTransfer.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tiStartTransfer.Size = new System.Drawing.Size(161, 22);
            this.tiStartTransfer.Text = "转换全部(&B)";
            this.tiStartTransfer.Click += new System.EventHandler(this.tiStart_Click);
            // 
            // mnTranduceSelected
            // 
            this.mnTranduceSelected.Name = "mnTranduceSelected";
            this.mnTranduceSelected.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.mnTranduceSelected.Size = new System.Drawing.Size(161, 22);
            this.mnTranduceSelected.Text = "转换选中(&P)";
            this.mnTranduceSelected.Click += new System.EventHandler(this.tiTransduceSelected_Click);
            // 
            // 设置OToolStripMenuItem
            // 
            this.设置OToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiSystemConfig,
            this.toolStripMenuItem1,
            this.tiClosingPassword});
            this.设置OToolStripMenuItem.Name = "设置OToolStripMenuItem";
            this.设置OToolStripMenuItem.Size = new System.Drawing.Size(62, 21);
            this.设置OToolStripMenuItem.Text = "设置(&O)";
            // 
            // tiSystemConfig
            // 
            this.tiSystemConfig.Name = "tiSystemConfig";
            this.tiSystemConfig.Size = new System.Drawing.Size(140, 22);
            this.tiSystemConfig.Text = "系统设置(&C)";
            this.tiSystemConfig.Click += new System.EventHandler(this.tiSystemConfig_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(137, 6);
            // 
            // tiClosingPassword
            // 
            this.tiClosingPassword.Name = "tiClosingPassword";
            this.tiClosingPassword.Size = new System.Drawing.Size(140, 22);
            this.tiClosingPassword.Text = "关机密码(&P)";
            // 
            // 关于HToolStripMenuItem
            // 
            this.关于HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于AToolStripMenuItem});
            this.关于HToolStripMenuItem.Name = "关于HToolStripMenuItem";
            this.关于HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.关于HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 关于AToolStripMenuItem
            // 
            this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
            this.关于AToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.关于AToolStripMenuItem.Text = "关于(&A)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1006, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tbMain
            // 
            this.tbMain.Controls.Add(this.tpMainPage);
            this.tbMain.Controls.Add(this.tpOutputFilesPage);
            this.tbMain.Controls.Add(this.tbLogPage);
            this.tbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMain.Location = new System.Drawing.Point(0, 81);
            this.tbMain.Name = "tbMain";
            this.tbMain.SelectedIndex = 0;
            this.tbMain.Size = new System.Drawing.Size(1006, 458);
            this.tbMain.TabIndex = 19;
            // 
            // tpMainPage
            // 
            this.tpMainPage.Controls.Add(this.toolStrip2);
            this.tpMainPage.Controls.Add(this.lvInputList);
            this.tpMainPage.Location = new System.Drawing.Point(4, 22);
            this.tpMainPage.Name = "tpMainPage";
            this.tpMainPage.Padding = new System.Windows.Forms.Padding(3);
            this.tpMainPage.Size = new System.Drawing.Size(998, 432);
            this.tpMainPage.TabIndex = 0;
            this.tpMainPage.Text = "输入配置";
            this.tpMainPage.UseVisualStyleBackColor = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiAddFile,
            this.tiAddFolder,
            this.tiAddPathManually,
            this.toolStripSeparator3,
            this.tiSelectAll,
            this.tiCancelAll,
            this.tiSelectReverse,
            this.toolStripSeparator5,
            this.tiDeletePathes,
            this.tiDeleteUnexisted});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(992, 31);
            this.toolStrip2.TabIndex = 29;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tiAddFile
            // 
            this.tiAddFile.Image = ((System.Drawing.Image)(resources.GetObject("tiAddFile.Image")));
            this.tiAddFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiAddFile.Name = "tiAddFile";
            this.tiAddFile.Size = new System.Drawing.Size(84, 28);
            this.tiAddFile.Text = "添加文件";
            this.tiAddFile.Click += new System.EventHandler(this.tiAddFile_Click);
            // 
            // tiAddFolder
            // 
            this.tiAddFolder.Image = global::QDasTransfer.Properties.Resources.AddFolder;
            this.tiAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiAddFolder.Name = "tiAddFolder";
            this.tiAddFolder.Size = new System.Drawing.Size(96, 28);
            this.tiAddFolder.Text = "添加文件夹";
            this.tiAddFolder.Click += new System.EventHandler(this.tiAddFolder_Click);
            // 
            // tiAddPathManually
            // 
            this.tiAddPathManually.Image = global::QDasTransfer.Properties.Resources.AddPath;
            this.tiAddPathManually.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiAddPathManually.Name = "tiAddPathManually";
            this.tiAddPathManually.Size = new System.Drawing.Size(84, 28);
            this.tiAddPathManually.Text = "手工添加";
            this.tiAddPathManually.Click += new System.EventHandler(this.tiAddPathManually_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tiSelectAll
            // 
            this.tiSelectAll.Image = global::QDasTransfer.Properties.Resources.SelectAll_24;
            this.tiSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiSelectAll.Name = "tiSelectAll";
            this.tiSelectAll.Size = new System.Drawing.Size(60, 28);
            this.tiSelectAll.Text = "全选";
            this.tiSelectAll.Click += new System.EventHandler(this.tiSelectAll_Click);
            // 
            // tiCancelAll
            // 
            this.tiCancelAll.Image = global::QDasTransfer.Properties.Resources.CancelAll_24;
            this.tiCancelAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiCancelAll.Name = "tiCancelAll";
            this.tiCancelAll.Size = new System.Drawing.Size(72, 28);
            this.tiCancelAll.Text = "全取消";
            this.tiCancelAll.Click += new System.EventHandler(this.tiCancelAll_Click);
            // 
            // tiSelectReverse
            // 
            this.tiSelectReverse.Image = global::QDasTransfer.Properties.Resources.ReverseAll_24;
            this.tiSelectReverse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiSelectReverse.Name = "tiSelectReverse";
            this.tiSelectReverse.Size = new System.Drawing.Size(60, 28);
            this.tiSelectReverse.Text = "反选";
            this.tiSelectReverse.Click += new System.EventHandler(this.tiSelectReverse_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // tiDeletePathes
            // 
            this.tiDeletePathes.Image = global::QDasTransfer.Properties.Resources.delete;
            this.tiDeletePathes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiDeletePathes.Name = "tiDeletePathes";
            this.tiDeletePathes.Size = new System.Drawing.Size(96, 28);
            this.tiDeletePathes.Text = "删除选中项";
            this.tiDeletePathes.Click += new System.EventHandler(this.tiDeletePathes_Click);
            // 
            // tiDeleteUnexisted
            // 
            this.tiDeleteUnexisted.Image = global::QDasTransfer.Properties.Resources.clear;
            this.tiDeleteUnexisted.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiDeleteUnexisted.Name = "tiDeleteUnexisted";
            this.tiDeleteUnexisted.Size = new System.Drawing.Size(108, 28);
            this.tiDeleteUnexisted.Text = "移除已转换项";
            // 
            // mnFolder
            // 
            this.mnFolder.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnTransduce,
            this.mnOpenFile,
            this.mnOpenInputFolder,
            this.mnDeleteItem,
            this.toolStripSeparator4,
            this.取消ToolStripMenuItem});
            this.mnFolder.Name = "mnFolder";
            this.mnFolder.Size = new System.Drawing.Size(183, 140);
            // 
            // mnTransduce
            // 
            this.mnTransduce.Image = global::QDasTransfer.Properties.Resources.Tranduce16X16;
            this.mnTransduce.Name = "mnTransduce";
            this.mnTransduce.Size = new System.Drawing.Size(182, 26);
            this.mnTransduce.Text = "转换选中项(&V)";
            this.mnTransduce.Click += new System.EventHandler(this.mnTransduce_Click);
            // 
            // mnOpenFile
            // 
            this.mnOpenFile.Name = "mnOpenFile";
            this.mnOpenFile.Size = new System.Drawing.Size(182, 26);
            this.mnOpenFile.Text = "打开当前文件(&F)";
            this.mnOpenFile.Click += new System.EventHandler(this.mnOpenFile_Click);
            // 
            // mnOpenInputFolder
            // 
            this.mnOpenInputFolder.Name = "mnOpenInputFolder";
            this.mnOpenInputFolder.Size = new System.Drawing.Size(182, 26);
            this.mnOpenInputFolder.Text = "打开当前文件夹(&O)";
            this.mnOpenInputFolder.Click += new System.EventHandler(this.mnOpenInputFolder_Click);
            // 
            // mnDeleteItem
            // 
            this.mnDeleteItem.Name = "mnDeleteItem";
            this.mnDeleteItem.Size = new System.Drawing.Size(182, 26);
            this.mnDeleteItem.Text = "移除选中项(&D)";
            this.mnDeleteItem.Click += new System.EventHandler(this.mnDeleteItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(179, 6);
            // 
            // 取消ToolStripMenuItem
            // 
            this.取消ToolStripMenuItem.Name = "取消ToolStripMenuItem";
            this.取消ToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.取消ToolStripMenuItem.Text = "取消(&X)";
            // 
            // tpOutputFilesPage
            // 
            this.tpOutputFilesPage.Controls.Add(this.tiSaveOutputFiles);
            this.tpOutputFilesPage.Controls.Add(this.lvResults);
            this.tpOutputFilesPage.Location = new System.Drawing.Point(4, 22);
            this.tpOutputFilesPage.Name = "tpOutputFilesPage";
            this.tpOutputFilesPage.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutputFilesPage.Size = new System.Drawing.Size(998, 432);
            this.tpOutputFilesPage.TabIndex = 1;
            this.tpOutputFilesPage.Text = "输出列表";
            this.tpOutputFilesPage.UseVisualStyleBackColor = true;
            // 
            // tiSaveOutputFiles
            // 
            this.tiSaveOutputFiles.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tiSaveOutputFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiSaveResults,
            this.tiClear});
            this.tiSaveOutputFiles.Location = new System.Drawing.Point(3, 3);
            this.tiSaveOutputFiles.Name = "tiSaveOutputFiles";
            this.tiSaveOutputFiles.Size = new System.Drawing.Size(992, 31);
            this.tiSaveOutputFiles.TabIndex = 28;
            this.tiSaveOutputFiles.Text = "toolStrip4";
            // 
            // tiSaveResults
            // 
            this.tiSaveResults.Image = global::QDasTransfer.Properties.Resources.Save;
            this.tiSaveResults.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiSaveResults.Name = "tiSaveResults";
            this.tiSaveResults.Size = new System.Drawing.Size(108, 28);
            this.tiSaveResults.Text = "保存转换结果";
            this.tiSaveResults.Click += new System.EventHandler(this.tiSaveResults_Click);
            // 
            // tiClear
            // 
            this.tiClear.Image = global::QDasTransfer.Properties.Resources.clear;
            this.tiClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiClear.Name = "tiClear";
            this.tiClear.Size = new System.Drawing.Size(84, 28);
            this.tiClear.Text = "清空列表";
            this.tiClear.Click += new System.EventHandler(this.tiClear_Click);
            // 
            // tbLogPage
            // 
            this.tbLogPage.Controls.Add(this.toolStrip3);
            this.tbLogPage.Controls.Add(this.lvLogs);
            this.tbLogPage.Location = new System.Drawing.Point(4, 22);
            this.tbLogPage.Name = "tbLogPage";
            this.tbLogPage.Padding = new System.Windows.Forms.Padding(3);
            this.tbLogPage.Size = new System.Drawing.Size(998, 432);
            this.tbLogPage.TabIndex = 2;
            this.tbLogPage.Text = "系统日志";
            this.tbLogPage.UseVisualStyleBackColor = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiOpenLogFile,
            this.tiClearLogList});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(992, 31);
            this.toolStrip3.TabIndex = 23;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // tiOpenLogFile
            // 
            this.tiOpenLogFile.Image = global::QDasTransfer.Properties.Resources.Open;
            this.tiOpenLogFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiOpenLogFile.Name = "tiOpenLogFile";
            this.tiOpenLogFile.Size = new System.Drawing.Size(108, 28);
            this.tiOpenLogFile.Text = "打开日志文件";
            this.tiOpenLogFile.Click += new System.EventHandler(this.tiOpenLogFile_Click);
            // 
            // tiClearLogList
            // 
            this.tiClearLogList.Image = global::QDasTransfer.Properties.Resources.clear;
            this.tiClearLogList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiClearLogList.Name = "tiClearLogList";
            this.tiClearLogList.Size = new System.Drawing.Size(108, 28);
            this.tiClearLogList.Text = "清除日志列表";
            this.tiClearLogList.Click += new System.EventHandler(this.tiClearLogList_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 56);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 56);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiStart,
            this.tiStop,
            this.toolStripSeparator1,
            this.tiOpenAppFolder,
            this.tiOpenOutputFolder,
            this.toolStripSeparator2,
            this.tiLock,
            this.tiConfig});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1006, 56);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tiStart
            // 
            this.tiStart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiTranduceAll,
            this.tiTransduceSelected});
            this.tiStart.Image = global::QDasTransfer.Properties.Resources.Transduce24X24;
            this.tiStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiStart.Name = "tiStart";
            this.tiStart.Size = new System.Drawing.Size(87, 53);
            this.tiStart.Text = "转换全部(&T)";
            this.tiStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tiStart.ButtonClick += new System.EventHandler(this.tiStart_ButtonClick);
            // 
            // tiTranduceAll
            // 
            this.tiTranduceAll.Image = global::QDasTransfer.Properties.Resources.Tranduce16X16;
            this.tiTranduceAll.Name = "tiTranduceAll";
            this.tiTranduceAll.Size = new System.Drawing.Size(163, 22);
            this.tiTranduceAll.Text = "转换全部(&T)";
            this.tiTranduceAll.Click += new System.EventHandler(this.tiStart_Click);
            // 
            // tiTransduceSelected
            // 
            this.tiTransduceSelected.Name = "tiTransduceSelected";
            this.tiTransduceSelected.Size = new System.Drawing.Size(163, 22);
            this.tiTransduceSelected.Text = "只转换选中的(&S)";
            this.tiTransduceSelected.Click += new System.EventHandler(this.tiTransduceSelected_Click);
            // 
            // tiStop
            // 
            this.tiStop.Enabled = false;
            this.tiStop.Image = global::QDasTransfer.Properties.Resources.Stop;
            this.tiStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiStop.Name = "tiStop";
            this.tiStop.Size = new System.Drawing.Size(75, 53);
            this.tiStop.Text = "停止转换(&S)";
            this.tiStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tiStop.Click += new System.EventHandler(this.tiStop_Click);
            // 
            // tiOpenAppFolder
            // 
            this.tiOpenAppFolder.Image = global::QDasTransfer.Properties.Resources.OpenAppFolder1;
            this.tiOpenAppFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiOpenAppFolder.Name = "tiOpenAppFolder";
            this.tiOpenAppFolder.Size = new System.Drawing.Size(99, 53);
            this.tiOpenAppFolder.Text = "打开程序目录(&P)";
            this.tiOpenAppFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tiOpenAppFolder.ToolTipText = "打开程序目录";
            // 
            // tiOpenOutputFolder
            // 
            this.tiOpenOutputFolder.Image = global::QDasTransfer.Properties.Resources.OpenAppFolder1;
            this.tiOpenOutputFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiOpenOutputFolder.Name = "tiOpenOutputFolder";
            this.tiOpenOutputFolder.Size = new System.Drawing.Size(102, 53);
            this.tiOpenOutputFolder.Text = "打开输出目录(&O)";
            this.tiOpenOutputFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tiOpenOutputFolder.Click += new System.EventHandler(this.tiOpenOutputFolder_Click);
            // 
            // tiLock
            // 
            this.tiLock.Image = global::QDasTransfer.Properties.Resources.Lock48X48;
            this.tiLock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiLock.Name = "tiLock";
            this.tiLock.Size = new System.Drawing.Size(74, 53);
            this.tiLock.Text = "锁定窗口(&L)";
            this.tiLock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tiLock.Click += new System.EventHandler(this.tiLock_Click);
            // 
            // tiConfig
            // 
            this.tiConfig.Image = global::QDasTransfer.Properties.Resources.gif_46_006;
            this.tiConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiConfig.Name = "tiConfig";
            this.tiConfig.Size = new System.Drawing.Size(52, 53);
            this.tiConfig.Text = "配置(&C)";
            this.tiConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tiConfig.Click += new System.EventHandler(this.tiConfig_Click);
            // 
            // lvInputList
            // 
            this.lvInputList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvInputList.CheckBoxes = true;
            this.lvInputList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvInputList.ContextMenuStrip = this.mnFolder;
            this.lvInputList.FullRowSelect = true;
            this.lvInputList.GridLines = true;
            this.lvInputList.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            this.lvInputList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvInputList.Location = new System.Drawing.Point(8, 37);
            this.lvInputList.MultiSelect = false;
            this.lvInputList.Name = "lvInputList";
            this.lvInputList.Size = new System.Drawing.Size(981, 389);
            this.lvInputList.SmallImageList = this.imageList1;
            this.lvInputList.TabIndex = 21;
            this.lvInputList.UseCompatibleStateImageBehavior = false;
            this.lvInputList.View = System.Windows.Forms.View.Details;
            this.lvInputList.DoubleClick += new System.EventHandler(this.lvInputList_DoubleClick);
            this.lvInputList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvInputList_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "输入路径";
            this.columnHeader1.Width = 492;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "文件个数";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 76;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "上次转换时间";
            this.columnHeader3.Width = 173;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "文件是否存在";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 135;
            // 
            // lvResults
            // 
            this.lvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTime,
            this.chInput,
            this.chState,
            this.chOutput});
            this.lvResults.ContextMenuStrip = this.mnFolder;
            this.lvResults.FullRowSelect = true;
            this.lvResults.GridLines = true;
            this.lvResults.HideSelection = false;
            this.lvResults.Location = new System.Drawing.Point(8, 37);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(982, 389);
            this.lvResults.SmallImageList = this.imageList1;
            this.lvResults.TabIndex = 22;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            // 
            // chTime
            // 
            this.chTime.DisplayIndex = 3;
            this.chTime.Text = "时间";
            this.chTime.Width = 103;
            // 
            // chInput
            // 
            this.chInput.DisplayIndex = 0;
            this.chInput.Text = "输入文件";
            this.chInput.Width = 318;
            // 
            // chState
            // 
            this.chState.DisplayIndex = 1;
            this.chState.Text = "状态";
            this.chState.Width = 92;
            // 
            // chOutput
            // 
            this.chOutput.DisplayIndex = 2;
            this.chOutput.Text = "输出文件";
            this.chOutput.Width = 424;
            // 
            // lvLogs
            // 
            this.lvLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colTime,
            this.colEvent,
            this.colContent,
            this.colFile});
            this.lvLogs.FullRowSelect = true;
            this.lvLogs.GridLines = true;
            this.lvLogs.HideSelection = false;
            this.lvLogs.Location = new System.Drawing.Point(8, 37);
            this.lvLogs.Name = "lvLogs";
            this.lvLogs.Size = new System.Drawing.Size(982, 389);
            this.lvLogs.TabIndex = 20;
            this.lvLogs.UseCompatibleStateImageBehavior = false;
            this.lvLogs.View = System.Windows.Forms.View.Details;
            // 
            // colID
            // 
            this.colID.Text = "编号";
            this.colID.Width = 63;
            // 
            // colTime
            // 
            this.colTime.Text = "时间";
            this.colTime.Width = 138;
            // 
            // colEvent
            // 
            this.colEvent.Text = "事件";
            this.colEvent.Width = 47;
            // 
            // colContent
            // 
            this.colContent.Text = "说明";
            this.colContent.Width = 212;
            // 
            // colFile
            // 
            this.colFile.Text = "相关文件";
            this.colFile.Width = 499;
            // 
            // NewMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 561);
            this.Controls.Add(this.tbMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1022, 596);
            this.Name = "NewMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QDAS Tranducer ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewMainForm_FormClosing);
            this.Load += new System.EventHandler(this.NewMainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tbMain.ResumeLayout(false);
            this.tpMainPage.ResumeLayout(false);
            this.tpMainPage.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.mnFolder.ResumeLayout(false);
            this.tpOutputFilesPage.ResumeLayout(false);
            this.tpOutputFilesPage.PerformLayout();
            this.tiSaveOutputFiles.ResumeLayout(false);
            this.tiSaveOutputFiles.PerformLayout();
            this.tbLogPage.ResumeLayout(false);
            this.tbLogPage.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnNewConfig;
		private System.Windows.Forms.ToolStripMenuItem mnSaveConfig;
		private System.Windows.Forms.ToolStripMenuItem mnExportConfig;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem mnExit;
		private System.Windows.Forms.ToolStripMenuItem 设置OToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tiSystemConfig;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tiClosingPassword;
		private System.Windows.Forms.ToolStripMenuItem 生成RToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tiStartTransfer;
		private System.Windows.Forms.ToolStripMenuItem 关于HToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.TabControl tbMain;
		private System.Windows.Forms.TabPage tpMainPage;
		private System.Windows.Forms.TabPage tpOutputFilesPage;
		private System.Windows.Forms.ColumnHeader colID;
		private System.Windows.Forms.ColumnHeader colTime;
		private System.Windows.Forms.ColumnHeader colEvent;
		private System.Windows.Forms.ColumnHeader colContent;
		private System.Windows.Forms.ColumnHeader colFile;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ContextMenuStrip mnFolder;
		private System.Windows.Forms.ToolStripMenuItem mnOpenInputFolder;
		private System.Windows.Forms.ToolStripMenuItem mnDeleteItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem 取消ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnTransduce;
        private System.Windows.Forms.ColumnHeader chInput;
        private System.Windows.Forms.ColumnHeader chState;
        private System.Windows.Forms.ColumnHeader chOutput;
		private System.Windows.Forms.ColumnHeader chTime;
		private System.Windows.Forms.ToolStripSplitButton tiStart;
		private System.Windows.Forms.ToolStripMenuItem tiTranduceAll;
		private System.Windows.Forms.ToolStripMenuItem tiTransduceSelected;
		private System.Windows.Forms.ToolStripButton tiStop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tiOpenAppFolder;
		private System.Windows.Forms.ToolStripButton tiOpenOutputFolder;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tiLock;
		private System.Windows.Forms.ToolStripButton tiConfig;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnTranduceSelected;
		private System.Windows.Forms.ToolStripMenuItem mnOpenFile;
		private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tiAddFile;
        private System.Windows.Forms.ToolStripButton tiAddFolder;
        private System.Windows.Forms.ToolStripButton tiAddPathManually;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tiSelectAll;
        private System.Windows.Forms.ToolStripButton tiCancelAll;
        private System.Windows.Forms.ToolStripButton tiSelectReverse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tiDeletePathes;
        private System.Windows.Forms.TabPage tbLogPage;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton tiOpenLogFile;
        private System.Windows.Forms.ToolStripButton tiClearLogList;
        private System.Windows.Forms.ToolStrip tiSaveOutputFiles;
        private System.Windows.Forms.ToolStripButton tiSaveResults;
        private System.Windows.Forms.ToolStripButton tiClear;
        private Classes.MyListView lvLogs;
        private Classes.MyListView lvInputList;
        private Classes.MyListView lvResults;
        private System.Windows.Forms.ToolStripButton tiDeleteUnexisted;
    }
}