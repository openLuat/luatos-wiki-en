using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;

namespace translate_tool
{
    internal class Translator
    {
        public static string DeepLKey = "";
        class DeepLResponseItem
        {
            public required string detected_source_language { get; set; }
            public required string text { get; set; }
        }
        class DeepLResponse
        {
            public required DeepLResponseItem[] translations { get; set; }
        }
        private static List<string> deepL(IList<string> s)
        {
            var client = new RestClient("https://api-free.deepl.com/v2/translate");
            var request = new RestRequest()
                .AddJsonBody(new
                {
                    text = s,
                    source_lang = "ZH",
                    target_lang = "EN"
                })
                .AddHeader("Authorization", $"DeepL-Auth-Key {DeepLKey}");
            var response = client.Get(request);
            if(!response.IsSuccessStatusCode)
                throw new Exception("DeepL API returned an error: " + response.StatusCode);
            var content = JsonSerializer.Deserialize<DeepLResponse>(response.Content!);
            List<string> strings = new();
            foreach( var item in content!.translations)
                strings.Add(item.text);
            return strings;
        }

        public static List<string> DeepL(IList<string> s)
        {
            if(s.Count < 100)//字数没超，不特殊处理
                return deepL(s);
            //字数超了，分段分次请求处理
            List<string> r = new();
            for (int i = 0; i < s.Count; i += 100)
                r.AddRange(deepL(s.Skip(i).Take(100).ToList()));
            return r;
        }

        public static string DeepL(string s)
        {
            return Translator.deepL(new string[] { s })[0];
        }

        /// <summary>
        /// 加载已有的翻译数据
        /// </summary>
        /// <param name="path">翻译数据的文件路径</param>
        /// <returns>翻译结果的键值对</returns>
        public static Dictionary<string,string> LoadExistTranslation(string path)
        {
            if (!File.Exists(path)) return new();
            Dictionary<string, string> r = new();
            var lines = File.ReadAllText(path).Replace("\r","").Split('\n');
            for(int i=0;i<lines.Length;i+=2)
            {
                if (string.IsNullOrEmpty(lines[i])) continue;
                r.Add(lines[i], lines[i+1]);
            }
            return r;
        }

        /// <summary>
        /// 保存新增的数据
        /// </summary>
        /// <param name="exist">已加载的数据</param>
        /// <param name="newData">新增的数据</param>
        /// <param name="path">翻译数据的文件路径</param>
        public static void SaveExistTranslation(Dictionary<string, string> exist, Dictionary<string, string> newData, string path)
        {
            //没新增数据
            if(newData.Count == 0) return;
            //塞进去
            foreach(var key in newData.Keys)
                exist.Add(key, newData[key]);
            //按每行放好
            List<string> l = new();
            foreach (var key in exist.Keys)
                l.Add($"{key}\r\n{exist[key]}");
            //排序一下
            l.Sort();
            //保存
            File.WriteAllText(path, string.Join("\r\n", l));
        }

        /// <summary>
        /// 获取翻译结果
        /// </summary>
        /// <param name="s">待翻译的数组列表</param>
        /// <param name="path">已有翻译数据txt的路径</param>
        /// <returns>翻译结果</returns>
        public static List<string> Translate(IList<string> s, string path)
        {
            //翻译结果
            List<string> r = new();
            foreach(var i in s)
                r.Add(i);
            //已经翻译好的数据
            var existData = LoadExistTranslation(path);
            //待翻译的数据
            List<string> toTranslate = new();
            List<int> toTranslateIndex = new();
            for(int i=0;i<s.Count ; i++)
            {
                //已有翻译
                if (existData.ContainsKey(r[i]))
                    r[i] = existData[r[i]];
                else
                {
                    //还没翻译
                    toTranslate.Add(r[i]);
                    toTranslateIndex.Add(i);
                }
            }
            //翻译一下
            if(toTranslate.Count > 0)
                toTranslate = DeepL(toTranslate);
            //新翻译的数据
            Dictionary<string, string> newData = new();
            for(int i=0;i< toTranslate.Count;i++)
            {
                //更新到翻译字典
                if (!newData.ContainsKey(r[toTranslateIndex[i]]))
                    newData.Add(r[toTranslateIndex[i]], toTranslate[i]);
                //更新到结果列表
                r[toTranslateIndex[i]] = toTranslate[i];
            }
            //保存新数据
            SaveExistTranslation(existData,newData,path);
            return r;
        }
    }
}
