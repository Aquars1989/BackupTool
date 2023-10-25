using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupTool
{
    public class PrjFun
    {
        public static List<string> GetMatchFiles(string folder, string[] filePatterns, string[] ignorePatterns, bool subDirectory)
        {
            string[] files = Directory.GetFiles(folder, "*", subDirectory ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            List<string> result = new List<string>();

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);

                bool match = false;
                foreach (string filePattern in filePatterns)
                {
                    if (!string.IsNullOrWhiteSpace(filePattern) && PatternMatch(fileName, filePattern))
                    {
                        match = true;
                        break;
                    }
                }

                if (match)
                {
                    foreach (string ignorePattern in ignorePatterns)
                    {
                        if (!string.IsNullOrWhiteSpace(ignorePattern) && PatternMatch(fileName, ignorePattern))
                        {
                            match = false;
                            break;
                        }
                    }
                }

                if (match)
                {
                    result.Add(file);
                }
            }

            return result;
        }

        public static bool PatternMatch(string text, string pattern)
        {
            int textIndex = 0;
            int patternIndex = 0;
            int asteriskIndex = -1;
            int asteriskTextIndex = 0;

            while (textIndex < text.Count())
            {
                bool isEndOfPattern = patternIndex >= pattern.Count();
                if (!isEndOfPattern && (text[textIndex] == pattern[patternIndex] || pattern[patternIndex] == '?'))
                {
                    textIndex++;
                    patternIndex++;
                }

                else if (!isEndOfPattern && pattern[patternIndex] == '*')
                {
                    asteriskIndex = patternIndex;
                    patternIndex += 1;
                    asteriskTextIndex = textIndex;
                }
                else if (asteriskIndex != -1) // 表示前面有出現過 *
                {
                    textIndex = ++asteriskTextIndex;
                    patternIndex = asteriskIndex + 1;
                }
                else
                {
                    return false;
                }
            }

            while (patternIndex < pattern.Count() && pattern[patternIndex] == '*')
                patternIndex++;

            return patternIndex == pattern.Count();
        }


        /// <summary>
        /// 獲取檔案MD5值
        /// </summary>
        /// <param name="fileName">檔案絕對路徑</param>
        /// <returns>MD5值</returns>
        public static byte[] GetMD5HashByteFromFile(string fileName)
        {
            try
            {
                using (FileStream file = new FileStream(fileName, FileMode.Open))
                {
                    System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    byte[] retVal = md5.ComputeHash(file);
                    file.Close();
                    return retVal;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashByteFromFile() fail,error:" + ex.Message);
            }
        }

    }
}
