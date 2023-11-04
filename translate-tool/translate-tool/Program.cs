

using static System.Net.Mime.MediaTypeNames;

namespace translate_tool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var DocSource = args[0];
            var DocPath = args[1];
            var TransDataPath = args[2];
            Translator.AliyunSecretId = args[3];
            Translator.AliyuntSecretKey = args[4];
            Console.WriteLine("Source: " + Path.GetFullPath(DocSource));
            Console.WriteLine("DocPath: " + Path.GetFullPath(DocPath));
            Console.WriteLine("Data: " + Path.GetFullPath(TransDataPath));

            //遍历DocSource下的所有文件夹，包括子文件夹
            var dirs = Directory.GetDirectories(DocSource, "*", SearchOption.AllDirectories);
            //去除.开头的文件夹
            dirs = dirs.Where(d => !(d.Contains("\\.") || d.Contains("/."))).ToArray();
            //在DocPath下新建所有文件夹
            foreach (var dir in dirs)
            {
                var newDir = dir.Replace(DocSource, DocPath);
                Directory.CreateDirectory(newDir);
                newDir = dir.Replace(DocSource, TransDataPath);
                Directory.CreateDirectory(newDir);
            }

            //遍历DocSource下的所有文件，包括子文件夹
            var files = Directory.GetFiles(DocSource, "*", SearchOption.AllDirectories);
            //去除.开头的文件
            files = files.Where(f => !(f.Contains("\\.") || f.Contains("/."))).ToArray();
            //处理过的文件计数
            var count = 0;
            //翻译所有文件
            foreach (var file in files)
            {
                Console.WriteLine(file);
                count++;
                //目标路径
                var newFile = file.Replace(DocSource, DocPath);
                if (Path.GetExtension(file) == ".py"||
                    Path.GetExtension(file) == ".html"||
                    Path.GetExtension(file) == ".js")
                {
                    var text = File.ReadAllText(file);
                    text = text.Replace("wiki.luatos.com", "wiki.luatos.org");
                    text = text.Replace("https://github.com/openluat/luatos-wiki", "https://github.com/openluat/luatos-wiki-en");
                    text = text.Replace("LuatOS团队", "LuatOS team");
                    text = text.Replace("评论区仅用于讨论文档内容。如有使用问题或新需求，请进支持群讨论或在官方仓库新建issue", "");
                    if (Path.GetFileName(file) == "conf.py")
                    {
                        text = text.Replace("language = 'zh_CN'", "language = 'en_US'");
                        text = text.Replace("html_search_language = 'zh'", "html_search_language = 'en'");
                    }
                    //写入新文件
                    File.WriteAllText(newFile, text);
                }
                //判断拓展名是不是.md
                else if (Path.GetExtension(file) != ".md")
                {
                    Console.WriteLine($"[{count}/{files.Length}] copy");
                    //原样复制到DocPath下
                    File.Copy(file, newFile, true);
                }
                else
                {
                    Console.WriteLine($"[{count}/{files.Length}] translating...");
                    //翻译数据路径
                    var translateFile = file.Replace(DocSource, TransDataPath);
                    //将translateFile路径拓展名改为txt
                    translateFile = Path.ChangeExtension(translateFile, ".txt");
                    //翻译文件
                    var markdownText = File.ReadAllText(file);
                    markdownText = markdownText.Replace("wiki.luatos.com", "wiki.luatos.org");
                    var result = Parse.TranslateMarkdown(markdownText, (s) =>
                    {
                        return Translator.Translate(s, translateFile);
                    });
                    //将翻译结果写入新文件
                    File.WriteAllText(newFile, result);
                }
            }

            Console.WriteLine("done");
        }
    }
}
