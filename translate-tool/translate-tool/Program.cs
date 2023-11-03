

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
            Console.WriteLine("Source: " + DocSource);
            Console.WriteLine("DocPath: " + DocPath);
            Console.WriteLine("Data: " + TransDataPath);

            //遍历DocSource下的所有文件夹，包括子文件夹
            var dirs = Directory.GetDirectories(DocSource, "*", SearchOption.AllDirectories);
            //去除.开头的文件夹
            dirs = dirs.Where(d => !d.Contains("\\.")).ToArray();
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
            files = files.Where(f => !f.Contains("\\.")).ToArray();
            //处理过的文件计数
            var count = 0;
            //翻译所有文件
            foreach (var file in files)
            {
                Console.WriteLine(file);
                count++;
                //判断拓展名是不是.md
                if (Path.GetExtension(file) != ".md")
                {
                    Console.WriteLine($"[{count}/{files.Length}] copy");
                    //原样复制到DocPath下
                    var newFile = file.Replace(DocSource, DocPath);
                    File.Copy(file, newFile, true);
                }
                else
                {
                    Console.WriteLine($"[{count}/{files.Length}] translating...");
                    //目标路径
                    var newFile = file.Replace(DocSource, DocPath);
                    var translateFile = file.Replace(DocSource, TransDataPath);
                    //将translateFile路径拓展名改为txt
                    translateFile = Path.ChangeExtension(translateFile, ".txt");
                    //翻译文件
                    var markdownText = File.ReadAllText(file);
                    var result = Parse.TranslateMarkdown(markdownText, (s) =>
                    {
                        return Translator.Translate(s, translateFile);
                    });
                    //将翻译结果写入新文件
                    File.WriteAllText(newFile, result);
                }
            }

            Console.WriteLine("done");
            ////var testFile = @"D:\projects\git\luatos-wiki\archives.md";
            //var testFile = @"D:\projects\git\luatos-wiki\luaGuide\luatask.md";
            //var markdownText = File.ReadAllText(testFile);

            //var result = Parse.TranslateMarkdown(markdownText, (s) =>
            //{
            //    return Translator.Translate(s, "D:\\projects\\git\\luatos-wiki-en\\translation-data\\test.txt");
            //});
            //Console.WriteLine(result);

        }
    }
}