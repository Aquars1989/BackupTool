using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupTool
{
    public partial class BackupSettingForm : Form
    {
        public BackupSetting BackupSetting { get; set; }

        public BackupSettingForm()
        {
            InitializeComponent();
        }

        public BackupSettingForm(BackupSetting backupSetting) : this()
        {
            BackupSetting = backupSetting;
            txtName.Text = txtName.OldValue = BackupSetting.Name;
            txtSourceFolder.Text = txtSourceFolder.OldValue = BackupSetting.SourceFolder;
            txtBackupFolder.Text = txtBackupFolder.OldValue = BackupSetting.BackupFolder;
            txtBackupFiles.Text = txtBackupFiles.OldValue = string.Join(";", BackupSetting.BackupFiles);
            txtIgoneFiles.Text = txtIgoneFiles.OldValue = string.Join(";", BackupSetting.IgoneFiles);
            txtTags.Text = txtTags.OldValue = string.Join(";", BackupSetting.Tags);
            chkSubdirectory.Checked = chkSubdirectory.OldValue = BackupSetting.Subdirectory;
            txtName.CheckValueChanged = true;
            txtSourceFolder.CheckValueChanged = true;
            txtBackupFolder.CheckValueChanged = true;
            txtBackupFiles.CheckValueChanged = true;
            txtIgoneFiles.CheckValueChanged = true;
            txtTags.CheckValueChanged = true;
            chkSubdirectory.CheckValueChanged = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!txtName.Check(Text)) return;
            if (!txtSourceFolder.Check(Text)) return;
            if (!txtBackupFolder.Check(Text)) return;
            if (!txtBackupFiles.Check(Text)) return;
            if (!txtIgoneFiles.Check(Text)) return;

            if (BackupSetting == null) BackupSetting = new BackupSetting();

            BackupSetting.Name = txtName.Text;
            BackupSetting.SourceFolder = txtSourceFolder.Text;
            BackupSetting.BackupFolder = txtBackupFolder.Text;
            BackupSetting.BackupFiles = txtBackupFiles.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            BackupSetting.IgoneFiles = txtIgoneFiles.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            BackupSetting.Tags = txtTags.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            BackupSetting.Subdirectory = chkSubdirectory.Checked;
            DialogResult = DialogResult.OK;
        }
    }
}
