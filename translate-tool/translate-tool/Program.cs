

namespace translate_tool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var DocSource = args[0];
            var DocPath = args[1];
            var TransDataPath = args[2];
            Translator.DeepLKey = args[3];
            Console.WriteLine("Source: " + DocSource);
            Console.WriteLine("DocPath: " + DocPath);
            Console.WriteLine("Data: " + TransDataPath);

            //var testFile = @"D:\projects\git\luatos-wiki\archives.md";
            var testFile = @"D:\projects\git\luatos-wiki\luaGuide\luatask.md";
            var markdownText = File.ReadAllText(testFile);

            var result = Parse.TranslateMarkdown(markdownText, (s) =>
            {
                return Translator.Translate(s, "D:\\projects\\git\\luatos-wiki-en\\translation-data\\test.txt");
            });
            Console.WriteLine(result);

        }
    }
}