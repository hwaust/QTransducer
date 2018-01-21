using QTrans.Classes;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace QDasTransfer.Forms
{
    public partial class ConfigForm : Form
    {
        public ParamaterData pd = new ParamaterData();
        public bool needSave = false;

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void setFolder(TextBox tb)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = tb.Text;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb.Text = fbd.SelectedPath;
            }
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            this.Text += "  (当前配置文件: " + new FileInfo(QTrans.TransferBase.appconfig).FullName + ")";

            // pgOutput
            txtOutputFolder.Text = pd.OutputFolder;
            txtTempFolder.Text = pd.TempFolder;
            if (pd.DealSameOutputFileNameType == 0)
                rbReplaceOld.Checked = true;
            else if (pd.DealSameOutputFileNameType == 1)
                rbAddTimeToNew.Checked = true;
            else if (pd.DealSameOutputFileNameType == 2)
                rbIncrementIndex.Checked = true;

            if (pd.KeepOutFolderStructLevel > 0)
            {
                ckKeepOutFolderStruct.Checked = true;
                numlblKeepFolderLevel.Value = pd.KeepOutFolderStructLevel;
                numlblKeepFolderLevel.Enabled = true;
            }
            else
            {
                ckKeepOutFolderStruct.Checked = false;
            }

            ckAddTimeTickToOutDFQfile.Checked = pd.AddTimeTickToOutDFQfile;
            ckKeepOutFolderStruct.Checked = pd.KeepOutFolderStructType == 1;

            //pgBackups 
            rbBackup.Checked = pd.ProcessSourceFileType == 0;
            rbNoChange.Checked = pd.ProcessSourceFileType == 1;
            rbDelete.Checked = pd.ProcessSourceFileType == 2;
            gbBackupFolders.Enabled = rbBackup.Checked;
            txtSuccessfulFolder.Text = pd.FolderForSuccessed;
            txtFailedFolder.Text = pd.FolderForFailed;
            ckKeepBackupFolderStruct.Checked = pd.KeepBackupFolderStructType == 1;

            //pgAutoTransducer
            cbCircleUnit.SelectedIndex = 1;
            if (pd.SupportAutoTransducer)
            {
                ckAutoTransduce.Checked = pd.AutoTransducerAvaliable;
                gbAutoConfig.Enabled = pd.AutoTransducerAvaliable;
                numCircleValue.Value = pd.CircleValue;
                cbCircleUnit.SelectedIndex = pd.CircleUnit;
                dpStartTime.Value = pd.StartTime;
                dpEndTime.Value = pd.EndTime;
            }



            if (!pd.SupportAutoTransducer)
            {
                tabControl1.TabPages.Remove(pgAutoTransducer);
            }

            ckTraverseSubfolders.Checked = pd.TraverseSubfolders;

            /***** encoding page *****/
            lbEncodings.Items.Clear();
            foreach (Encoding ed in ParamaterData.Encodings)
                lbEncodings.Items.Add(ed.BodyName);
            lbEncodings.Items[0] = "default";
            lbEncodings.SelectedIndex = pd.EncodingID;
        }

        private void btOutputFolder_Click(object sender, EventArgs e)
        {
            setFolder(txtOutputFolder);
        }

        private void btnTempFolder_Click(object sender, EventArgs e)
        {
            setFolder(txtTempFolder);
        }

        private void btnSuccessfulFolder_Click(object sender, EventArgs e)
        {
            setFolder(txtSuccessfulFolder);
        }

        private void btnFailedFolder_Click(object sender, EventArgs e)
        {
            setFolder(txtFailedFolder);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            needSave = true;

            // pgOutput
            pd.OutputFolder = txtOutputFolder.Text;
            pd.TempFolder = txtTempFolder.Text;
            if (rbReplaceOld.Checked)
                pd.DealSameOutputFileNameType = 0;
            else if (rbAddTimeToNew.Checked)
                pd.DealSameOutputFileNameType = 1;
            else
                pd.DealSameOutputFileNameType = 2;
            pd.KeepOutFolderStructType = ckKeepOutFolderStruct.Checked ? 1 : 0;
            pd.AddTimeTickToOutDFQfile = ckAddTimeTickToOutDFQfile.Checked;

            //pgBackups
            if (rbBackup.Checked)
                pd.ProcessSourceFileType = 0;
            else if (rbNoChange.Checked)
                pd.ProcessSourceFileType = 1;
            else if (rbDelete.Checked)
                pd.ProcessSourceFileType = 2;

            pd.KeepBackupFolderStructType = ckKeepBackupFolderStruct.Checked ? 1 : 0;
            pd.FolderForSuccessed = txtSuccessfulFolder.Text;
            pd.FolderForFailed = txtFailedFolder.Text;

            //pgAutoTransducer
            pd.AutoTransducerAvaliable = ckAutoTransduce.Checked;
            if (pd.AutoTransducerAvaliable)
            {
                pd.CircleValue = (int)numCircleValue.Value;
                pd.CircleUnit = cbCircleUnit.SelectedIndex;
                pd.StartTime = dpStartTime.Value;
                pd.EndTime = dpEndTime.Value;
            }

            pd.TraverseSubfolders = ckTraverseSubfolders.Checked;

            /****** Encoding page ******/
            pd.EncodingID = lbEncodings.SelectedIndex;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ckKeepFolderStruct_Click(object sender, EventArgs e)
        {
            numlblKeepFolderLevel.Enabled = ckKeepOutFolderStruct.Checked;
        }

        private void ckAutoTransfer_Click(object sender, EventArgs e)
        {
            gbAutoConfig.Enabled = ckAutoTransduce.Checked;
        }

        private void rbBackup_CheckedChanged(object sender, EventArgs e)
        {
            gbBackupFolders.Enabled = true;
        }

        private void rbNoChange_CheckedChanged(object sender, EventArgs e)
        {
            gbBackupFolders.Enabled = false;
        }

        private void rbDelete_CheckedChanged(object sender, EventArgs e)
        {
            gbBackupFolders.Enabled = false;
        }
    }
}
