using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupTool
{
    public class ConfigObject
    {
        /// <summary>
        /// 設定擋路徑
        /// </summary>
        public string ConfigPath { get; set; }

        /// <summary>
        /// 備份設定列表
        /// </summary>
        public List<BackupSetting> BackupSettings { get; set; } = new List<BackupSetting>();

        /// <summary>
        /// 從檔案讀取設定資料
        /// </summary>
        public void Load()
        {
            BackupSettings.Clear();
            BackupSetting currectSetting = null;
            using (FileStream fs = new FileStream(ConfigPath, FileMode.OpenOrCreate))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine().Trim();

                    if (line.StartsWith("//")) continue;
                    else if (line.StartsWith("#"))
                    {
                        string name = line.Substring(1);
                        if (string.IsNullOrWhiteSpace(name)) continue;

                        currectSetting = new BackupSetting() { Name = name };
                        BackupSettings.Add(currectSetting);
                    }
                    else
                    {
                        string[] splitLine = line.Split('=');
                        if (splitLine.Length < 2) continue;

                        switch (splitLine[0].ToLower())
                        {
                            case "sourcefolder":
                                currectSetting.SourceFolder = splitLine[1].Trim();
                                break;
                            case "backupfolder":
                                currectSetting.BackupFolder = splitLine[1].Trim();
                                break;
                            case "backupfiles":
                                currectSetting.BackupFiles = splitLine[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                break;
                            case "igonefiles":
                                currectSetting.IgoneFiles = splitLine[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                break;
                            case "subdirectory":
                                currectSetting.Subdirectory = splitLine[1].Equals("true", StringComparison.OrdinalIgnoreCase);
                                break;
                            case "lastbackup":
                                if (DateTime.TryParse(splitLine[1], out DateTime date))
                                {
                                    currectSetting.LastBackup = date;
                                }
                                break;
                            case "tags":
                                currectSetting.Tags = splitLine[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                break;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 寫入設定資料至檔案
        /// </summary>
        public void Save()
        {
            using (FileStream fs = new FileStream(ConfigPath + ".tmp", FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (BackupSetting setting in BackupSettings)
                {
                    sw.WriteLine($"#{setting.Name}");
                    sw.WriteLine($"SourceFolder={setting.SourceFolder}");
                    sw.WriteLine($"BackupFolder={setting.BackupFolder}");
                    sw.WriteLine($"BackupFiles={string.Join(";", setting.BackupFiles)}");
                    sw.WriteLine($"IgoneFiles={string.Join(";", setting.IgoneFiles)}");
                    sw.WriteLine($"Subdirectory={setting.Subdirectory}");
                    sw.WriteLine($"Tags={string.Join(";", setting.Tags)}");
                    sw.WriteLine($"LastBackup={setting.LastBackup?.ToString("yyyy/MM/dd HH:mm:ss")??""}");
                }
            }

            if(File.Exists(ConfigPath)) File.Delete(ConfigPath);
            File.Move(ConfigPath + ".tmp", ConfigPath);
        }
    }
}
