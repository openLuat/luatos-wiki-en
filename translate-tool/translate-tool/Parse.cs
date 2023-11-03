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
            string Pattern = @"[\u4e00-\u9fff]";
            return Regex.IsMatch(s, Pattern);
        }

        /// <summary>
        /// 统计开头的空格数量
        /// </summary>
        /// <param name="s">一行的字</param>
        /// <returns>空格数量</returns>
        public static int CheckPrefix(string s)
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
        public static string CreatePrefix(int number, string prefix = " ")
        {
            StringBuilder sb = new();
            for(int i=0;i<number ; i++)
                sb.Append(prefix);
            return sb.ToString();
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
            List<int> waitPrefix = new();
            //遍历
            for (int i=0;i<lines.Length;i++)
            {
                var line = lines[i];
                //空字符串不解析
                if(string.IsNullOrEmpty(line)) continue;
                if(ContainChinese(line))
                {
                    waitTranslateLine.Add(i);
                    waitTranslate.Add(line.Trim());
                    waitPrefix.Add(CheckPrefix(line));
                }
            }
            //翻译
            waitTranslate = translator(waitTranslate);
            //合并回去
            for(int i=0;i< waitTranslateLine.Count;i++)
                lines[waitTranslateLine[i]] = CreatePrefix(waitPrefix[i]) + waitTranslate[i];

            return string.Join("\r\n",lines);
        }
    }
}
