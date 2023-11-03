using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace translate_tool
{
    internal class Parse
    {

        /// <summary>
        /// 检查是否包含中文
        /// </summary>
        /// <param name="s">待检测字符串</param>
        /// <returns>结果</returns>
        public static bool ContainChinese(string s)
        {
            return Regex.IsMatch(s, Static.ChinesePattern);
        }

        /// <summary>
        /// 统计开头的空格数量
        /// </summary>
        /// <param name="s">一行的字</param>
        /// <returns>空格数量</returns>
        private static int CheckPrefix(string s)
        {
            int prefixCount = 0;
            for(int i=0;i<s.Length;i++)
            {
                if (s[i] == ' ')
                    prefixCount++;
                else
                    break;
            }
            return prefixCount;
        }
        /// <summary>
        /// 统计结尾的空格数量
        /// </summary>
        /// <param name="s">一行的字</param>
        /// <returns>空格数量</returns>
        private static int CheckSuffix(string s)
        {
            int suffixCount = 0;
            for (int i = s.Length-1; i >= 0; i--)
            {
                if (s[i] == ' ')
                    suffixCount++;
                else
                    break;
            }
            return suffixCount;
        }
        private static string CreatePrefix(int number, string prefix = " ")
        {
            StringBuilder sb = new();
            for(int i=0;i<number ; i++)
                sb.Append(prefix);
            return sb.ToString();
        }

        public static (string,string,string) Trim(string s)
        {
            var prefix = new StringBuilder();
            var suffix = new StringBuilder();
            //处理开头的emoji
            Match regexMatch = Regex.Match(s, $"{Static.EmojiPattern}+",RegexOptions.Compiled);
            if (regexMatch.Success)
            {
                var index = regexMatch.Groups[0].Index + regexMatch.Groups[0].Value.Length;
                prefix.Append(s[0..index]);
                s = s[index..];
            }

            //处理开头的非中文
            Match regexNonChineseMatch = Regex.Match(s, $"{Static.ChinesePattern}", RegexOptions.Compiled);
            if (regexNonChineseMatch.Success)
            {
                var index = regexNonChineseMatch.Groups[0].Index;
                prefix.Append(s[0..index]);
                s = s[index..];
            }
            //把空格搞定
            var spaceN = CheckPrefix(s);
            prefix.Append(CreatePrefix(spaceN));
            var spaceS = CheckSuffix(s);
            suffix.Append(CreatePrefix(spaceS));
            s = s.Trim();

            //处理结尾的非中文
            regexNonChineseMatch = Regex.Match(s, $"[^{Static.ChinesePattern}]+$", RegexOptions.Compiled);
            if (regexNonChineseMatch.Success)
            {
                var index = regexNonChineseMatch.Groups[0].Index;
                if(s[index..] != "。")//句号就别匹配了
                {
                    suffix.Append(s[index..]);
                    s = s[0..index];
                }
            }

            return (prefix.ToString(), suffix.ToString(), s);
        }

        /// <summary>
        /// 翻译这一页内容
        /// </summary>
        /// <param name="markdownText">markdown数据</param>
        /// <param name="translator">翻译处理函数</param>
        /// <returns>翻译完的markdown</returns>
        public static string TranslateMarkdown(string markdownText, Func<IList<string>, List<string>> translator)
        {
            //按行处理
            var lines = markdownText.Replace("\r", "").Split('\n');
            //待翻译的数据
            List<int> waitTranslateLine = new();
            List<string> waitTranslate = new();
            List<string> waitPrefix = new();
            List<string> waitSuffix = new();
            //遍历
            for (int i=0;i<lines.Length;i++)
            {
                var line = lines[i];
                //空字符串不解析
                if(string.IsNullOrEmpty(line)) continue;
                if(ContainChinese(line))
                {
                    waitTranslateLine.Add(i);
                    var (prefix, suffix, trimed) = Trim(line);
                    waitTranslate.Add(trimed);
                    waitPrefix.Add(prefix);
                    waitSuffix.Add(suffix);
                }
            }
            //翻译
            waitTranslate = translator(waitTranslate);
            //合并回去
            for(int i=0;i< waitTranslateLine.Count;i++)
            {
                var space1 = (
                        Regex.IsMatch(waitPrefix[i], @"\w$") &&
                        Regex.IsMatch(waitTranslate[i], @"^\w")
                    ) ? " " : "";
                var space2 = (
                        Regex.IsMatch(waitTranslate[i], @"\w$") &&
                        Regex.IsMatch(waitSuffix[i], @"^\w")
                    ) ? " " : "";
                lines[waitTranslateLine[i]] = $"{waitPrefix[i]}{space1}{waitTranslate[i]}{space2}{waitSuffix[i]}";
            }

            return string.Join("\r\n",lines);
        }
    }
}
