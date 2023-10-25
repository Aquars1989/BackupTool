using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using EvanLib;
using FastColoredTextBoxNS;

namespace BackupTool
{
    public partial class MainForm : Form
    {
        private string _ConfigPath = "Config_BackupTool.txt";
        private ConfigObject _Config = new ConfigObject();
        private HashSet<string> _Tags = new HashSet<string>();

        public MainForm(string[] args)
        {
            InitializeComponent();
            Text += " - Ver：" + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();

            _Config.ConfigPath = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]) ? args[0] : _ConfigPath; //設定檔位置
            _Config.Load();

            foreach (BackupSetting setting in _Config.BackupSettings)
            {
                foreach (string tag in setting.Tags)
                {
                    if (!string.IsNullOrWhiteSpace(tag)) _Tags.Add(tag);
                }
            }

            foreach (string tag in _Tags)
            {
                AddTagButton(tag);
            }

            backupListView1.Config = _Config;
        }

        private void AddTagButton(string tag)
        {
            EvButton btn = new EvButton()
            {
                Text = tag,
                Font = new Font("Consolas", 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = SystemColors.GradientInactiveCaption,
                CheckedBackColor = Color.Yellow,
                Width = 0,
                AutoSize = true
            };
            btn.Click += btnTag_Click;
            flowLayoutPanel1.Controls.Add(btn);
        }

        private void btnTag_Click(object sender, EventArgs e)
        {
            EvButton btn = sender as EvButton;
            btn.Checked = !btn.Checked;

            HashSet<string> tags = new HashSet<string>();
            foreach (EvButton button in flowLayoutPanel1.Controls)
            {
                if (button.Checked)
                {
                    tags.Add(button.Text);
                }
            }
            backupListView1.ApplyFilter(tags);
        }

        private void backupListView1_LogOutput(object sender, string text)
        {
            fastColoredTextBox1.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + text + "\n");
            fastColoredTextBox1.ScrollLeft();
            fastColoredTextBox1.Navigate(fastColoredTextBox1.Lines.Count - 1);
        }

        private TextStyle _NewStyle = new TextStyle(Brushes.Blue, Brushes.Transparent, FontStyle.Regular);
        private TextStyle _UpdateStyle = new TextStyle(Brushes.DarkGreen, Brushes.Transparent, FontStyle.Regular);
        private TextStyle _ErrorStyle = new TextStyle(Brushes.Red, Brushes.Transparent, FontStyle.Regular);
        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            e.ChangedRange.SetStyle(_NewStyle, @"\(New\).*", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(_UpdateStyle, @"\(Update\).*", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(_UpdateStyle, @"\(OlderFile\).*", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(_ErrorStyle, @"##### Failure.*", RegexOptions.Singleline);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (BackupSettingForm form = new BackupSettingForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _Config.BackupSettings.Add(form.BackupSetting);
                    _Config.Save();
                    foreach (string tag in form.BackupSetting.Tags)
                    {
                        if (string.IsNullOrWhiteSpace(tag) || _Tags.Contains(tag)) continue;
                        AddTagButton(tag);
                    }

                    foreach (EvButton btn in flowLayoutPanel1.Controls)
                    {
                        btn.Checked = false;
                    }
                    backupListView1.Reload();
                }
            }
        }

        private void backupListView1_ItemRightClick(object sender, BackupListView.BackupListViewItem item)
        {
            toolStripMenuItem1.Text = item.Setting.Name;
            //editToolStripMenuItem.Enabled = deleteToolStripMenuItem.Enabled = backupListView1.BackingSetting == null;
            contextMenuStrip1.Tag = item;
            contextMenuStrip1.Show(Cursor.Position);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupListView.BackupListViewItem item = contextMenuStrip1.Tag as BackupListView.BackupListViewItem;
            if (item == null) return;

            using (BackupSettingForm form = new BackupSettingForm(item.Setting))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _Config.Save();
                    foreach (string tag in form.BackupSetting.Tags)
                    {
                        if (string.IsNullOrWhiteSpace(tag) || _Tags.Contains(tag)) continue;
                        AddTagButton(tag);
                    }
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupListView.BackupListViewItem item = contextMenuStrip1.Tag as BackupListView.BackupListViewItem;
            if (item == null) return;

            _Config.BackupSettings.Remove(item.Setting);
            _Config.Save();
            backupListView1.RemoveItem(item);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (EvButton btn in flowLayoutPanel1.Controls)
            {
                btn.Checked = false;
            }
            backupListView1.ApplyFilter(null);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            backupListView1.StopTask();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            backupListView1.StopAllTask();
        }

        private void backupListView1_BackupStart(object sender, EventArgs e)
        {
            //linkLabel1.Visible = false;
            //linkLabel3.Visible = true;
            //linkLabel4.Visible = true;
            //editToolStripMenuItem.Visible = false;
            //deleteToolStripMenuItem.Visible = false;
            //toolStripMenuItem2.Visible = true;
        }

        private void backupListView1_BackupEnd(object sender, EventArgs e)
        {
            evLabel2.Text = "Log";
            linkLabel1.Visible = true;
            linkLabel3.Visible = false;
            linkLabel4.Visible = false;
            editToolStripMenuItem.Visible = true;
            deleteToolStripMenuItem.Visible = true;
            toolStripMenuItem2.Visible = false;
        }

        private void backupListView1_ItemBackupStart(object sender, BackupListView.BackupListViewItem item)
        {
            evLabel2.Text = $"Backup: {item.Setting.Name}";
            linkLabel1.Visible = false;
            linkLabel3.Visible = true;
            linkLabel4.Visible = true;
            editToolStripMenuItem.Visible = false;
            deleteToolStripMenuItem.Visible = false;
            toolStripMenuItem2.Visible = true;
        }
    }
}
