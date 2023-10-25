using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupTool
{
    /// <summary>
    /// 備份資料設定
    /// </summary>
    public class BackupSetting
    {
        /// <summary>
        /// 設定名稱
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 來源目錄
        /// </summary>
        public string SourceFolder { get; set; } = "";

        /// <summary>
        /// 備份目錄
        /// </summary>
        public string BackupFolder { get; set; } = "";

        /// <summary>
        /// 來源目錄檔名篩選設定
        /// </summary>
        public string[] BackupFiles { get; set; } = new string[1];

        /// <summary>
        /// 排除資料檔名篩選設定
        /// </summary>
        public string[] IgoneFiles { get; set; } = new string[1];

        /// <summary>
        /// 是否包含子目錄資料
        /// </summary>
        public bool Subdirectory { get; set; } = false;

        /// <summary>
        /// 設定的標籤
        /// </summary>
        public string[] Tags { get; set; } = new string[1];

        /// <summary>
        /// 排除資料檔名篩選設定
        /// </summary>
        public DateTime? LastBackup { get; set; }

    }
}
