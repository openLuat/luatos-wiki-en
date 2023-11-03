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
using TencentCloud.Common;
using TencentCloud.Tmt.V20180321;
using TencentCloud.Tmt.V20180321.Models;
using Tea;

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
            var max = 100;
            if (s.Count < max)//字数没超，不特殊处理
                return deepL(s);
            //字数超了，分段分次请求处理
            List<string> r = new();
            for (int i = 0; i < s.Count; i += max)
                r.AddRange(deepL(s.Skip(i).Take(max).ToList()));
            return r;
        }


        public static string TencentSecretId = "";
        public static string TencentSecretKey = "";

        private static List<string> tencent(IList<string> s)
        {
            Credential cred = new Credential
            {
                SecretId = TencentSecretId,
                SecretKey = TencentSecretKey
            };
            // 实例化要请求产品的client对象,clientProfile是可选的
            TmtClient client = new TmtClient(cred, "ap-shanghai");
            // 实例化一个请求对象,每个接口都会对应一个request对象
            TextTranslateBatchRequest req = new TextTranslateBatchRequest
            {
                SourceTextList = s.ToArray(),
                Source = "zh",
                Target = "en",
                ProjectId = 0
            };
            // 返回的resp是一个TextTranslateResponse的实例，与请求对象对应
            TextTranslateBatchResponse resp = client.TextTranslateBatchSync(req);
            return resp.TargetTextList.ToList();
        }

        /// <summary>
        /// 每秒最大请求数量
        /// </summary>
        private static readonly double TencentReqLimit = 4;
        private static readonly TimeSpan TencentMinReq = TimeSpan.FromSeconds(TencentReqLimit / 4.0);
        private static DateTime TencentLastReq = DateTime.Now;
        public static List<string> Tencent(IList<string> s)
        {
            //如果和上次请求的间隔时间太短，等一等再请求
            var now = DateTime.Now;
            var diff = now - TencentLastReq;
            if (diff < TencentMinReq)
                Thread.Sleep(TencentMinReq - diff);
            TencentLastReq = now;
            //最大请求字数
            var max = 20;
            if (s.Count < max)//字数没超，不特殊处理
                return tencent(s);
            //字数超了，分段分次请求处理
            List<string> r = new();
            for (int i = 0; i < s.Count; i += max)
                r.AddRange(tencent(s.Skip(i).Take(max).ToList()));
            return r;
        }

        public static string AliyunSecretId = "";
        public static string AliyuntSecretKey = "";

        private static List<string> aliyun(IList<string> s)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                AccessKeyId = AliyunSecretId,
                AccessKeySecret = AliyuntSecretKey,
                // Endpoint 请参考 https://api.aliyun.com/product/alimt
                Endpoint = "mt.aliyuncs.com",
            };
            var client = new AlibabaCloud.SDK.Alimt20181012.Client(config);
            Dictionary<string, string> toTrans = new();
            for (int i = 0; i < s.Count; i++)
                toTrans.Add(i.ToString(), s[i]);
            AlibabaCloud.SDK.Alimt20181012.Models.GetBatchTranslateRequest getBatchTranslateRequest = new AlibabaCloud.SDK.Alimt20181012.Models.GetBatchTranslateRequest
            {
                FormatType = "text",
                TargetLanguage = "en",
                SourceLanguage = "zh",
                Scene = "general",
                ApiType = "translate_ecommerce",
                SourceText = JsonSerializer.Serialize(toTrans),
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            var res = client.GetBatchTranslateWithOptions(getBatchTranslateRequest, runtime);
            foreach (var item in res.Body.TranslatedList)
                toTrans[item["index"].ToString()!] = item["translated"].ToString()!;
            List<string> r = new();
            for(int i=0;i<s.Count;i++)
                r.Add(toTrans[i.ToString()]!);
            return r;
        }
        /// <summary>
        /// 每秒最大请求数量
        /// </summary>
        private static readonly double AliyunReqLimit = 40;
        private static readonly TimeSpan AliyunMinReq = TimeSpan.FromSeconds(AliyunReqLimit / 4.0);
        private static DateTime AliyunLastReq = DateTime.Now;
        public static List<string> Aliyun(IList<string> s)
        {
            //如果和上次请求的间隔时间太短，等一等再请求
            var now = DateTime.Now;
            var diff = now - AliyunLastReq;
            if (diff < AliyunMinReq)
                Thread.Sleep(AliyunMinReq - diff);
            AliyunLastReq = now;
            //最大请求字数
            var max = 20;
            if (s.Count < max)//字数没超，不特殊处理
                return aliyun(s);
            //字数超了，分段分次请求处理
            List<string> r = new();
            for (int i = 0; i < s.Count; i += max)
                r.AddRange(aliyun(s.Skip(i).Take(max).ToList()));
            return r;
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
        /// 现在用的翻译服务提供商是哪家
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>返回值</returns>
        public static List<string> translate(IList<string> s) => Aliyun(s);

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
            {
                //多试几次
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        toTranslate = translate(toTranslate);
                        break;
                    }
                    catch
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
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
