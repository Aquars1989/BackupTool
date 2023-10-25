using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace BackupTool
{
    public partial class BackupListView : Control
    {
        public delegate void deBackupListViewItemHandle(object sender, BackupListViewItem item);
        public delegate void deStringHandle(object sender, string text);
        public delegate void deCallOnLogOutput(string text);
        public delegate void deCallOnItemBackupStart(BackupListViewItem item);

        /// <summary>
        /// 項目被點選右鍵
        /// </summary>
        public event deBackupListViewItemHandle ItemRightClick;

        /// <summary>
        /// Log輸出
        /// </summary>
        public event deStringHandle LogOutput;

        /// <summary>
        /// 項目作業開始執行
        /// </summary>
        public event deBackupListViewItemHandle ItemBackupStart;

        /// <summary>
        /// 開始新備份作業
        /// </summary>
        public event EventHandler BackupStart;

        /// <summary>
        /// 結束所有備份作業
        /// </summary>
        public event EventHandler BackupEnd;


        protected void OnItemRightClick(BackupListViewItem item)
        {
            if (ItemRightClick != null)
                ItemRightClick(this, item);
        }

        protected void OnItemBackupStart(BackupListViewItem item)
        {
            if (ItemBackupStart != null)
                ItemBackupStart(this, item);
        }

        protected void OnLogOutput(string log)
        {
            if (LogOutput != null)
                LogOutput(this, log);
        }

        protected void OnBackupStart()
        {
            if (BackupStart != null)
                BackupStart(this, null);
        }

        protected void OnBackupEnd()
        {
            if (BackupEnd != null)
                BackupEnd(this, null);
        }

        private void CallOnLogOutput(string text)
        {
            if (this.InvokeRequired) // 如果不是在主執行緒中
            {
                deCallOnLogOutput Wok = new deCallOnLogOutput(OnLogOutput);
                this.Invoke(Wok, text);  // 就在主執行緒中執行同函數
            }
            else
            {
                OnLogOutput(text); // 否則直接執行
            }
        }

        private void CallOnItemBackupStart(BackupListViewItem item)
        {
            if (this.InvokeRequired) // 如果不是在主執行緒中
            {
                deCallOnItemBackupStart Wok = new deCallOnItemBackupStart(OnItemBackupStart);
                this.Invoke(Wok, item);  // 就在主執行緒中執行同函數
            }
            else
            {
                OnItemBackupStart(item); // 否則直接執行
            }
        }

        private void CallOnBackupStart()
        {
            if (this.InvokeRequired) // 如果不是在主執行緒中
            {
                Action Wok = new Action(CallOnBackupStart);
                this.Invoke(Wok);  // 就在主執行緒中執行同函數
            }
            else
            {
                OnBackupStart(); // 否則直接執行
            }
        }

        private void CallOnBackupEnd()
        {
            if (this.InvokeRequired) // 如果不是在主執行緒中
            {
                Action Wok = new Action(CallOnBackupEnd);
                this.Invoke(Wok);  // 就在主執行緒中執行同函數
            }
            else
            {
                OnBackupEnd(); // 否則直接執行
            }
        }

        /// <summary>
        /// 畫面更新計時器
        /// </summary>
        private Timer _TimerAnimation;

        private List<BackupListViewItem> _Items = new List<BackupListViewItem>();


        private ConfigObject _Config = null;
        /// <summary>
        /// 設定檔物件
        /// </summary>
        public ConfigObject Config
        {
            get { return _Config; }
            set
            {
                if (_Config == value) return;
                _Config = value;
                RebuildItems();
            }
        }


        private int _ItemsHeight = 50;
        [DefaultValue(50), Description("列表項目高度")]
        public int ItemsHeight
        {
            get { return _ItemsHeight; }
            set
            {
                if (_ItemsHeight == value) return;
                _ItemsHeight = value;

                foreach (BackupListViewItem item in _Items)
                {
                    item.Height = _ItemsHeight;
                }
                SetItemsTarget();
            }
        }

        private int _ItemsWidth = 0;
        [DefaultValue(0), Description("列表項目寬度(自動設定)")]
        public int ItemsWidth
        {
            get { return _ItemsWidth; }
            private set
            {
                if (_ItemsWidth == value) return;
                _ItemsWidth = value;

                foreach (BackupListViewItem item in _Items)
                {
                    item.Width = _ItemsWidth;
                }
            }
        }

        private int _ItemsSpacing = 5;
        [DefaultValue(5), Description("項目間距")]
        public int ItemsSpacing
        {
            get { return _ItemsSpacing; }
            set
            {
                if (_ItemsSpacing == value) return;
                _ItemsSpacing = value;
                SetItemsTarget();
            }
        }

        private Padding _ItemsPadding = new Padding(3, 3, 3, 3);
        [DefaultValue(typeof(Padding), "3, 3, 3, 3"), Description("項目內間距")]
        public Padding ItemsPadding
        {
            get { return _ItemsPadding; }
            set
            {
                if (_ItemsPadding == value) return;
                _ItemsPadding = value;
                Invalidate();
            }
        }

        private Font _ItemsNameFont = new Font("Consolas", 10);
        [DefaultValue(typeof(Font), "Consolas, 10pt"), Description("設定名稱字形")]
        public Font ItemsNameFont
        {
            get { return _ItemsNameFont; }
            set
            {
                if (_ItemsNameFont == value) return;
                _ItemsNameFont = value;
                Invalidate();
            }
        }

        private Font _ItemsTipFont = new Font("Consolas", 8);
        [DefaultValue(typeof(Font), "Consolas, 8pt"), Description("說明文字字形")]
        public Font ItemsTipFont
        {
            get { return _ItemsTipFont; }
            set
            {
                if (_ItemsTipFont == value) return;
                _ItemsTipFont = value;
                Invalidate();
            }
        }

        private Color _ItemsBackColor = Color.White;
        [DefaultValue(typeof(Color), "White"), Description("項目背景色")]
        public Color ItemsBackColor
        {
            get { return _ItemsBackColor; }
            set
            {
                if (_ItemsBackColor == value) return;
                _ItemsBackColor = value;
                Invalidate();
            }
        }

        private Color _ItemsBackColorWait = Color.LightYellow;
        [DefaultValue(typeof(Color), "LightYellow"), Description("項目背景色(等待中)")]
        public Color ItemsBackColorWait
        {
            get { return _ItemsBackColorWait; }
            set
            {
                if (_ItemsBackColorWait == value) return;
                _ItemsBackColorWait = value;
                Invalidate();
            }
        }

        private Color _ItemsBackColorBackup = Color.LightSkyBlue;
        [DefaultValue(typeof(Color), "LightSkyBlue"), Description("項目背景色(執行中)")]
        public Color ItemsBackColorBackup
        {
            get { return _ItemsBackColorBackup; }
            set
            {
                if (_ItemsBackColorBackup == value) return;
                _ItemsBackColorBackup = value;
                Invalidate();
            }
        }


        private Color _ItemsNameForeColor = Color.Black;
        [DefaultValue(typeof(Color), "Black"), Description("項目名稱顏色")]
        public Color ItemsNameForeColor
        {
            get { return _ItemsNameForeColor; }
            set
            {
                if (_ItemsNameForeColor == value) return;
                _ItemsNameForeColor = value;
                Invalidate();
            }
        }

        private Color _ItemsTipForeColorNever = Color.Gray;
        [DefaultValue(typeof(Color), "Gray"), Description("項目名稱顏色(未備份)")]
        public Color ItemsTipForeColorNever
        {
            get { return _ItemsTipForeColorNever; }
            set
            {
                if (_ItemsTipForeColorNever == value) return;
                _ItemsTipForeColorNever = value;
                Invalidate();
            }
        }

        private Color _ItemsTipForeColorDone = Color.DarkGreen;
        [DefaultValue(typeof(Color), "DarkGreen"), Description("項目名稱顏色(今日備份)")]
        public Color ItemsTipForeColorDone
        {
            get { return _ItemsTipForeColorDone; }
            set
            {
                if (_ItemsTipForeColorDone == value) return;
                _ItemsTipForeColorDone = value;
                Invalidate();
            }
        }

        private Color _ItemsTipForeColor = Color.Black;
        [DefaultValue(typeof(Color), "Black"), Description("項目名稱顏色(近期備份)")]
        public Color ItemsTipForeColor
        {
            get { return _ItemsTipForeColor; }
            set
            {
                if (_ItemsTipForeColor == value) return;
                _ItemsTipForeColor = value;
                Invalidate();
            }
        }


        private Color _ItemsTipForeColorLong = Color.Red;
        [DefaultValue(typeof(Color), "Red"), Description("項目名稱顏色(久未備份)")]
        public Color ItemsTipForeColorLong
        {
            get { return _ItemsTipForeColorLong; }
            set
            {
                if (_ItemsTipForeColorLong == value) return;
                _ItemsTipForeColorLong = value;
                Invalidate();
            }
        }

        //滑鼠捲動值
        private int _ScrollValue = 0;

        //所有項目顯示區域高度
        private int _FullHeight = 0;

        public BackupListView()
        {
            InitializeComponent();
            DoubleBuffered = true;

            ItemsWidth = Width - Padding.Horizontal - 20;

            _TimerAnimation = new Timer() { Interval = 10 };
            _TimerAnimation.Tick += (x, e) =>
            {
                foreach (BackupListViewItem item in _Items)
                {
                    //畫面滾動歸位
                    if (_ScrollValue < 0)
                    {
                        int dist = -_ScrollValue;
                        _ScrollValue += Math.Min(dist, dist / 5 + 2);
                    }
                    else if (_ScrollValue > Math.Max(_FullHeight - Height, 0))
                    {
                        int dist = _ScrollValue - Math.Max(_FullHeight - Height, 0);
                        _ScrollValue -= Math.Min(dist, dist / 5 + 2);
                    }

                    //移動項目
                    if (item.Y != item.TargetY)
                    {
                        int dist = item.TargetY - item.Y;
                        item.Y += Math.Sign(dist) * Math.Min(Math.Abs(dist), Math.Abs(dist) / 15 + 2);
                    }
                    if (item.X != item.TargetX)
                    {
                        int dist = item.TargetX - item.X;
                        item.X += Math.Sign(dist) * Math.Min(Math.Abs(dist), Math.Abs(dist) / 15 + 2);
                    }

                    //設定透明度
                    item.Opacity = item.Visible ? Math.Min(item.Opacity + 0.05F, 1) : Math.Max(item.Opacity - 0.1F, 0);
                }
                Invalidate();
            };
            _TimerAnimation.Enabled = true;


            //定時檢查是否有新的備份作業待執行
            Task work = new Task(() =>
            {
                bool hasTask = false;
                while (true)
                {
                    Thread.Sleep(10);

                    bool hasTaskInCircle = false;
                    foreach (BackupListViewItem item in _Items)
                    {
                        if (item.Status == BackupListViewItemStatus.Waiting)
                        {
                            hasTaskInCircle = true;
                            item.ProgressMax = 100;
                            item.ProgressValue = 0;

                            item.Status = BackupListViewItemStatus.Backing;
                            CallOnItemBackupStart(item);

                            if (!hasTask) //目前無作業時觸發BackupStart
                            {
                                hasTask = true;
                                _StopTask = false;
                                CallOnBackupStart();
                            }

                            CallSetItemsTarget();
                            CallOnLogOutput($"===== Start << {item.Setting.Name} >> =====");
                            CallOnLogOutput($"Search folder [{item.Setting.SourceFolder}]");

                            try
                            {
                                List<string> files = PrjFun.GetMatchFiles(item.Setting.SourceFolder, item.Setting.BackupFiles, item.Setting.IgoneFiles, item.Setting.Subdirectory);

                                item.ProgressMax = files.Count;
                                CallOnLogOutput($"Get {files.Count:N0} files:");
                                files.ForEach(x => { CallOnLogOutput(x.Replace(item.Setting.SourceFolder, "")); });

                                CallOnLogOutput($"----- Check & Backup files -----");

                                int backupCount = 0;
                                foreach (string file in files)
                                {
                                    if (_StopTask)
                                    {
                                        _StopTask = false;
                                        throw new Exception("User Stop.");
                                    }

                                    string subFolder = file.Replace(item.Setting.SourceFolder, "");
                                    string copyToPath = item.Setting.BackupFolder + subFolder;

                                    if (File.Exists(copyToPath))
                                    {
                                        DateTime dateSource = File.GetLastWriteTime(file);
                                        DateTime dateTarget = File.GetLastWriteTime(copyToPath);
                                        if (dateSource == dateTarget)
                                        {
                                            byte[] md5Source = PrjFun.GetMD5HashByteFromFile(file);
                                            byte[] md5Target = PrjFun.GetMD5HashByteFromFile(copyToPath);
                                            if (md5Source.SequenceEqual(md5Target))
                                            {
                                                item.ProgressValue++;
                                                continue;
                                            }
                                        }
                                        if (dateSource < dateTarget)
                                        {
                                            CallOnLogOutput($"(OlderFile) {subFolder}");
                                            item.ProgressValue++;
                                            continue;
                                        }

                                        CallOnLogOutput($"(Update) {subFolder}");
                                        File.Copy(file, copyToPath, true);
                                        backupCount++;
                                        item.ProgressValue++;
                                    }
                                    else
                                    {
                                        string copyToFolder = Path.GetDirectoryName(copyToPath);
                                        CallOnLogOutput($"(New) {subFolder}");
                                        if (!Directory.Exists(copyToFolder))
                                        {
                                            CallOnLogOutput($"   Create folder [{copyToFolder}]");
                                            Directory.CreateDirectory(copyToFolder);
                                        }
                                        File.Copy(file, copyToPath);
                                        backupCount++;
                                        item.ProgressValue++;
                                    }
                                }

                                if (backupCount > 0)
                                {
                                    CallOnLogOutput($"Backup {backupCount:N0} files.");
                                }
                                else
                                {
                                    CallOnLogOutput($"No updated or new file.");
                                }
                                item.Setting.LastBackup = DateTime.Now;
                                _Config.Save();
                                CallOnLogOutput($"===== End << {item.Setting.Name} >> =====");
                            }
                            catch (Exception ex)
                            {
                                CallOnLogOutput($"##### Failure! {ex.Message}");
                            }
                            item.Status = BackupListViewItemStatus.None;
                            CallSetItemsTarget();
                            break;
                        }
                    }

                    if (hasTask && !hasTaskInCircle) //檢查一輪無可進行作業時,觸發BackupEnd
                    {
                        CallOnBackupEnd();
                        hasTask = false;
                    }
                }
            });
            work.Start();

            //if (DesignMode)
            //{
            //    ConfigObject demo = new ConfigObject();
            //    demo.BackupSettings.Add(new BackupSetting() { Name = "Test1", LastBackup = null });
            //    demo.BackupSettings.Add(new BackupSetting() { Name = "Test2", LastBackup = DateTime.Today.AddDays(-90) });
            //    demo.BackupSettings.Add(new BackupSetting() { Name = "Test3", LastBackup = DateTime.Today.AddDays(-10) });
            //    demo.BackupSettings.Add(new BackupSetting() { Name = "Test4", LastBackup = DateTime.Today });
            //    Config = demo;
            //}
        }

        //更新介面
        private void CallSetItemsTarget()
        {
            if (this.InvokeRequired) // 如果不是在主執行緒中
            {
                Action Wok = new Action(CallSetItemsTarget);
                this.Invoke(Wok);  // 就在主執行緒中執行同函數
            }
            else
            {
                SetItemsTarget(); // 否則直接執行
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            ItemsWidth = Width - Padding.Horizontal - 20;
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            SetItemsTarget();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);


            for (int i = 0; i < _Items.Count; i++)
            {
                BackupListViewItem item = _Items[i];
                //只繪製畫面內的項目
                if (item.X + item.Width > 0 && item.X < Width &&
                    item.Y + item.Height > _ScrollValue && item.Y < Height + _ScrollValue && item.Opacity > 0)
                {
                    Rectangle rect = new Rectangle(item.X, item.Y - _ScrollValue, item.Width, item.Height);
                    Rectangle rectTop = new Rectangle(item.X + ItemsPadding.Left, item.Y - _ScrollValue + ItemsPadding.Top, item.Width - ItemsPadding.Horizontal, (item.Height - ItemsPadding.Vertical) / 2);
                    Rectangle rectBottom = new Rectangle(item.X + ItemsPadding.Left, item.Y - _ScrollValue + ItemsPadding.Top + (item.Height - ItemsPadding.Vertical) / 2, item.Width - ItemsPadding.Horizontal, (item.Height - ItemsPadding.Vertical) / 2);

                    Color itemBack = _ItemsBackColor;
                    Color itemTip = _ItemsTipForeColor;
                    string tip = "";

                    if (item.Setting.LastBackup == null)
                    {
                        itemTip = _ItemsTipForeColorNever;
                        tip = "未備份";
                    }
                    else if (item.Setting.LastBackup?.Date == DateTime.Now.Date)
                    {
                        itemTip = _ItemsTipForeColorDone;
                        tip = "今日已備份";
                    }
                    else
                    {
                        int days = (DateTime.Now.Date - item.Setting.LastBackup.Value.Date).Days;
                        if (days > 14)
                        {
                            itemTip = _ItemsTipForeColorLong;
                        }
                        else
                        {
                            itemTip = _ItemsTipForeColor;
                        }
                        tip = $"{days:N0}天前備份";
                    }



                    switch (item.Status)
                    {
                        case BackupListViewItemStatus.Waiting:
                            itemBack = ItemsBackColorWait;
                            itemTip = _ItemsTipForeColor;
                            tip = "等待備份";
                            break;
                        case BackupListViewItemStatus.Backing:
                            itemBack = ItemsBackColorBackup;
                            itemTip = _ItemsTipForeColor;
                            tip = "備份中...";
                            break;
                    }

                    using (SolidBrush brushBack = new SolidBrush(Color.FromArgb((int)(255 * item.Opacity), itemBack)))
                    using (SolidBrush brushName = new SolidBrush(Color.FromArgb((int)(255 * item.Opacity), _ItemsNameForeColor)))
                    using (SolidBrush brushTip = new SolidBrush(Color.FromArgb((int)(255 * item.Opacity), itemTip)))
                    using (Pen brushBorder = new Pen(Color.FromArgb((int)(255 * item.Opacity), Color.Black), 1))
                    {
                        pe.Graphics.FillRectangle(brushBack, rect);

                        if (item.Equals(_HoverItem))
                        {
                            using (SolidBrush brushHover = new SolidBrush(Color.FromArgb((int)(100 * item.Opacity), Color.LightGray)))
                            {
                                pe.Graphics.FillRectangle(brushHover, rect);
                            }
                        }

                        pe.Graphics.DrawString(item.Setting.Name, ItemsNameFont, brushName, rectTop, _StringNameFormat);
                        pe.Graphics.DrawString(tip, ItemsTipFont, brushTip, rectBottom, _StringTipFormat);

                        if (item.Status == BackupListViewItemStatus.Backing)
                        {
                            Rectangle rectProcessBack = new Rectangle(item.X + ItemsPadding.Left + 4, item.Y - _ScrollValue + ItemsPadding.Top + (item.Height - ItemsPadding.Vertical) / 2 + 4, (int)((item.Width - ItemsPadding.Horizontal) * 0.7F) - 8, (item.Height - ItemsPadding.Vertical) / 2 - 8);
                            Rectangle rectProcessValue = new Rectangle(rectProcessBack.X + 1, rectProcessBack.Y + 1, (int)((rectProcessBack.Width - 2) * item.ProgressValue / item.ProgressMax), rectProcessBack.Height - 2);
                            using (SolidBrush brushProcessBack = new SolidBrush(Color.FromArgb((int)(255 * item.Opacity), Color.White)))
                            using (SolidBrush brushProcessValue = new SolidBrush(Color.FromArgb((int)(255 * item.Opacity), Color.CadetBlue)))
                            {
                                pe.Graphics.FillRectangle(brushProcessBack, rectProcessBack);
                                pe.Graphics.FillRectangle(brushProcessValue, rectProcessValue);
                            }
                        }

                        pe.Graphics.DrawRectangle(brushBorder, rect);
                    }
                }

            }

            pe.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
        }


        private StringFormat _StringNameFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisWord };
        private StringFormat _StringTipFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisWord };

        private BackupListViewItem _HoverItem = null;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            _HoverItem = null;
            Cursor = Cursors.Default;
            foreach (BackupListViewItem item in _Items)
            {
                if (e.X >= item.X && e.X <= item.X + item.Width &&
                    e.Y + _ScrollValue >= item.Y && e.Y + _ScrollValue <= item.Y + item.Height && item.Visible)
                {
                    _HoverItem = item;
                    Cursor = Cursors.Hand;
                    return;
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            _ScrollValue -= e.Delta / 2;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            //if (_HoverItem != null)
            //    OnItemDoubleClick(_HoverItem);

            if (_HoverItem != null)
            {
                switch (_HoverItem.Status)
                {
                    case BackupListView.BackupListViewItemStatus.None:
                        _HoverItem.Status = BackupListView.BackupListViewItemStatus.Waiting;
                        break;
                    case BackupListView.BackupListViewItemStatus.Waiting:
                        _HoverItem.Status = BackupListView.BackupListViewItemStatus.None;
                        break;
                }
                SetItemsTarget();
            }

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_HoverItem != null && e.Button == MouseButtons.Right)
            {
                OnItemRightClick(_HoverItem);
                _HoverItem = null;
            }

        }

        private void RebuildItems()
        {
            _Items.Clear();

            if (Config == null) return;

            int targetY = Padding.Top;
            int y = Height + 10;
            for (int i = 0; i < Config.BackupSettings.Count; i++)
            {
                BackupSetting setting = Config.BackupSettings[i];

                _Items.Add(new BackupListViewItem()
                {
                    Width = _ItemsWidth,
                    Height = _ItemsHeight,
                    X = Padding.Left,
                    Y = y,
                    TargetX = Padding.Left,
                    TargetY = targetY,
                    Opacity = 0,
                    Visible = true,
                    Status = BackupListViewItemStatus.None,
                    Setting = setting
                });
                targetY += _ItemsHeight + ItemsSpacing;
                y += (_ItemsHeight + ItemsSpacing) * 2;
            }
            _FullHeight = targetY - _ItemsSpacing + Padding.Bottom;
        }

        private void SetItemsTarget()
        {
            int top = Padding.Top;
            int left = Padding.Left;
            foreach (BackupListViewItem item in _Items)
            {
                if (!item.Visible) continue;
                item.TargetX = item.Status == BackupListViewItemStatus.Backing ? left + 15 : left;
                item.TargetY = top;
                top += _ItemsHeight + _ItemsSpacing;
            }

            _FullHeight = top - _ItemsSpacing + Padding.Bottom;
        }

        public void Reload()
        {
            RebuildItems();
        }

        public void ApplyFilter(HashSet<string> tags)
        {
            if (tags == null || tags.Count == 0)
            {
                int top = Padding.Top;
                foreach (BackupListViewItem item in _Items)
                {
                    if (item.Opacity <= 0) //右側插入
                    {
                        item.X = Width + top;
                        item.Y = top;
                    }
                    item.Visible = true;
                    top += _ItemsHeight + _ItemsSpacing;
                }
            }
            else
            {
                int top = Padding.Top;
                foreach (BackupListViewItem item in _Items)
                {
                    bool visible = false;
                    foreach (string itemTag in item.Setting.Tags)
                    {
                        if (tags.Contains(itemTag))
                        {
                            visible = true;
                            break;
                        }
                    }

                    if (visible) top += _ItemsHeight + _ItemsSpacing;
                    if (item.Visible == visible) continue;

                    if (visible && item.Opacity <= 0) //右側插入
                    {
                        item.X = Width + top;
                        item.Y = top;
                    }
                    item.Visible = visible;
                }
            }
            SetItemsTarget();
        }

        public void RemoveItem(BackupListViewItem item)
        {
            _Items.Remove(item);
            SetItemsTarget();
        }

        private bool _StopTask = false;
        /// <summary>
        /// 停止目前備份作業
        /// </summary>
        public void StopTask()
        {
            _StopTask = true;
        }

        /// <summary>
        /// 停止目前及排定的備份作業
        /// </summary>
        public void StopAllTask()
        {
            foreach (BackupListViewItem item in _Items)
            {
                if (item.Status == BackupListViewItemStatus.Waiting)
                {
                    item.Status = BackupListViewItemStatus.None;
                }
            }
            _StopTask = true;
        }

        public class BackupListViewItem
        {
            /// <summary>
            /// 項目寬度
            /// </summary>
            public int Width { get; set; }

            /// <summary>
            /// 項目高度
            /// </summary>
            public int Height { get; set; }


            /// <summary>
            /// 項目位置X
            /// </summary>
            public int X { get; set; }

            /// <summary>
            /// 項目位置Y
            /// </summary>
            public int Y { get; set; }


            /// <summary>
            /// 項目對應設定檔
            /// </summary>
            public BackupSetting Setting { get; set; }

            /// <summary>
            /// 項目移動目的座標X
            /// </summary>
            public int TargetX { get; set; }

            /// <summary>
            /// 項目移動目的座標Y
            /// </summary>
            public int TargetY { get; set; }

            /// <summary>
            /// 項目不透明度
            /// </summary>
            public float Opacity { get; set; }

            /// <summary>
            /// 項目可見度
            /// </summary>
            public bool Visible { get; set; }

            /// <summary>
            /// 項目進度最大值
            /// </summary>
            public float ProgressMax { get; set; }

            /// <summary>
            /// 項目進度已完成值
            /// </summary>
            public float ProgressValue { get; set; }

            /// <summary>
            /// 項目狀態
            /// </summary>
            public BackupListViewItemStatus Status { get; set; }

        }

        public enum BackupListViewItemStatus
        {
            None,
            Waiting,
            Backing
        }
    }
}
