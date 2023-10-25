
namespace BackupTool
{
    partial class BackupSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupSettingForm));
            this.evLabel1 = new EvanLib.EvLabel();
            this.txtName = new EvanLib.EvTextBox();
            this.txtSourceFolder = new EvanLib.EvTextBox();
            this.evLabel2 = new EvanLib.EvLabel();
            this.txtBackupFiles = new EvanLib.EvTextBox();
            this.evLabel3 = new EvanLib.EvLabel();
            this.txtIgoneFiles = new EvanLib.EvTextBox();
            this.evLabel4 = new EvanLib.EvLabel();
            this.chkSubdirectory = new EvanLib.EvCheckBox();
            this.btnOK = new EvanLib.EvButton();
            this.btnCancel = new EvanLib.EvButton();
            this.txtBackupFolder = new EvanLib.EvTextBox();
            this.evLabel5 = new EvanLib.EvLabel();
            this.txtTags = new EvanLib.EvTextBox();
            this.evLabel6 = new EvanLib.EvLabel();
            this.SuspendLayout();
            // 
            // evLabel1
            // 
            this.evLabel1.BindInput = this.txtName;
            this.evLabel1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.evLabel1.Location = new System.Drawing.Point(17, 26);
            this.evLabel1.Name = "evLabel1";
            this.evLabel1.Size = new System.Drawing.Size(142, 23);
            this.evLabel1.TabIndex = 0;
            this.evLabel1.Text = "Name";
            this.evLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtName.BindLabel = this.evLabel1;
            this.txtName.CheckEmpty = true;
            this.txtName.ColumnName = null;
            this.txtName.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtName.Location = new System.Drawing.Point(165, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(419, 31);
            this.txtName.TabIndex = 0;
            // 
            // txtSourceFolder
            // 
            this.txtSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceFolder.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtSourceFolder.BindLabel = this.evLabel2;
            this.txtSourceFolder.CheckEmpty = true;
            this.txtSourceFolder.ColumnName = null;
            this.txtSourceFolder.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtSourceFolder.Location = new System.Drawing.Point(165, 75);
            this.txtSourceFolder.Name = "txtSourceFolder";
            this.txtSourceFolder.Size = new System.Drawing.Size(419, 31);
            this.txtSourceFolder.TabIndex = 1;
            // 
            // evLabel2
            // 
            this.evLabel2.BindInput = this.txtSourceFolder;
            this.evLabel2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.evLabel2.Location = new System.Drawing.Point(17, 77);
            this.evLabel2.Name = "evLabel2";
            this.evLabel2.Size = new System.Drawing.Size(142, 23);
            this.evLabel2.TabIndex = 2;
            this.evLabel2.Text = "SourceFolder";
            this.evLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBackupFiles
            // 
            this.txtBackupFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBackupFiles.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtBackupFiles.BindLabel = this.evLabel3;
            this.txtBackupFiles.CheckEmpty = true;
            this.txtBackupFiles.ColumnName = null;
            this.txtBackupFiles.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtBackupFiles.Location = new System.Drawing.Point(165, 173);
            this.txtBackupFiles.Name = "txtBackupFiles";
            this.txtBackupFiles.Size = new System.Drawing.Size(419, 31);
            this.txtBackupFiles.TabIndex = 3;
            // 
            // evLabel3
            // 
            this.evLabel3.BindInput = this.txtBackupFiles;
            this.evLabel3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.evLabel3.Location = new System.Drawing.Point(17, 175);
            this.evLabel3.Name = "evLabel3";
            this.evLabel3.Size = new System.Drawing.Size(142, 23);
            this.evLabel3.TabIndex = 4;
            this.evLabel3.Text = "BackupFiles";
            this.evLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIgoneFiles
            // 
            this.txtIgoneFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIgoneFiles.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtIgoneFiles.BindLabel = this.evLabel4;
            this.txtIgoneFiles.ColumnName = null;
            this.txtIgoneFiles.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtIgoneFiles.Location = new System.Drawing.Point(165, 223);
            this.txtIgoneFiles.Name = "txtIgoneFiles";
            this.txtIgoneFiles.Size = new System.Drawing.Size(419, 31);
            this.txtIgoneFiles.TabIndex = 4;
            // 
            // evLabel4
            // 
            this.evLabel4.BindInput = this.txtIgoneFiles;
            this.evLabel4.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.evLabel4.Location = new System.Drawing.Point(17, 225);
            this.evLabel4.Name = "evLabel4";
            this.evLabel4.Size = new System.Drawing.Size(142, 23);
            this.evLabel4.TabIndex = 6;
            this.evLabel4.Text = "IgoneFiles";
            this.evLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkSubdirectory
            // 
            this.chkSubdirectory.AutoSize = true;
            this.chkSubdirectory.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.chkSubdirectory.ColumnName = null;
            this.chkSubdirectory.Font = new System.Drawing.Font("Consolas", 12F);
            this.chkSubdirectory.Location = new System.Drawing.Point(165, 322);
            this.chkSubdirectory.Name = "chkSubdirectory";
            this.chkSubdirectory.Size = new System.Drawing.Size(170, 33);
            this.chkSubdirectory.TabIndex = 6;
            this.chkSubdirectory.Text = "Subdirectory";
            this.chkSubdirectory.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnOK.CheckedBackColor = System.Drawing.Color.Empty;
            this.btnOK.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnOK.Location = new System.Drawing.Point(491, 322);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 33);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnCancel.CheckedBackColor = System.Drawing.Color.Empty;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnCancel.Location = new System.Drawing.Point(392, 321);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 33);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // txtBackupFolder
            // 
            this.txtBackupFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBackupFolder.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtBackupFolder.BindLabel = this.evLabel5;
            this.txtBackupFolder.CheckEmpty = true;
            this.txtBackupFolder.ColumnName = null;
            this.txtBackupFolder.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtBackupFolder.Location = new System.Drawing.Point(165, 125);
            this.txtBackupFolder.Name = "txtBackupFolder";
            this.txtBackupFolder.Size = new System.Drawing.Size(419, 31);
            this.txtBackupFolder.TabIndex = 2;
            // 
            // evLabel5
            // 
            this.evLabel5.BindInput = this.txtBackupFolder;
            this.evLabel5.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.evLabel5.Location = new System.Drawing.Point(17, 127);
            this.evLabel5.Name = "evLabel5";
            this.evLabel5.Size = new System.Drawing.Size(142, 23);
            this.evLabel5.TabIndex = 11;
            this.evLabel5.Text = "BackupFolder";
            this.evLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTags
            // 
            this.txtTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTags.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTags.BindLabel = this.evLabel6;
            this.txtTags.ColumnName = null;
            this.txtTags.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtTags.Location = new System.Drawing.Point(165, 271);
            this.txtTags.Name = "txtTags";
            this.txtTags.Size = new System.Drawing.Size(419, 31);
            this.txtTags.TabIndex = 5;
            // 
            // evLabel6
            // 
            this.evLabel6.BindInput = this.txtTags;
            this.evLabel6.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.evLabel6.Location = new System.Drawing.Point(17, 273);
            this.evLabel6.Name = "evLabel6";
            this.evLabel6.Size = new System.Drawing.Size(142, 23);
            this.evLabel6.TabIndex = 13;
            this.evLabel6.Text = "Tags";
            this.evLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BackupSettingForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(608, 375);
            this.Controls.Add(this.txtTags);
            this.Controls.Add(this.evLabel6);
            this.Controls.Add(this.txtBackupFolder);
            this.Controls.Add(this.evLabel5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkSubdirectory);
            this.Controls.Add(this.txtIgoneFiles);
            this.Controls.Add(this.evLabel4);
            this.Controls.Add(this.txtBackupFiles);
            this.Controls.Add(this.evLabel3);
            this.Controls.Add(this.txtSourceFolder);
            this.Controls.Add(this.evLabel2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.evLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BackupSettingForm";
            this.Text = "Backup Setting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EvanLib.EvLabel evLabel1;
        private EvanLib.EvTextBox txtName;
        private EvanLib.EvTextBox txtSourceFolder;
        private EvanLib.EvLabel evLabel2;
        private EvanLib.EvTextBox txtBackupFiles;
        private EvanLib.EvLabel evLabel3;
        private EvanLib.EvTextBox txtIgoneFiles;
        private EvanLib.EvLabel evLabel4;
        private EvanLib.EvCheckBox chkSubdirectory;
        private EvanLib.EvButton btnOK;
        private EvanLib.EvButton btnCancel;
        private EvanLib.EvTextBox txtBackupFolder;
        private EvanLib.EvLabel evLabel5;
        private EvanLib.EvTextBox txtTags;
        private EvanLib.EvLabel evLabel6;
    }
}