using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Bing.WeChatWork.Robots.Models;
using Newtonsoft.Json;
using WebApiClient;
using Xunit;
using Xunit.Abstractions;

namespace Bing.WeChatWork.Robots.Tests
{
    public class WeChatWorkRobotProviderTest : TestBase
    {
        /// <summary>
        /// 提供程序
        /// </summary>
        protected IWeChatWorkRobotProvider Provider { get; }

        /// <summary>
        /// 企业微信机器人密钥
        /// </summary>
        protected string AppId { get; }

        /// <summary>
        /// 当前目录
        /// </summary>
        protected string CurrentDir { get; }

        public WeChatWorkRobotProviderTest(ITestOutputHelper output) : base(output)
        {
            Init();
            var api = HttpApi.Resolve<IWeChatWorkRobotApi>();
            Provider = new WeChatWorkRobotProvider(api);
            AppId = "";
            CurrentDir = Environment.CurrentDirectory;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            HttpApi.Register<IWeChatWorkRobotApi>().ConfigureHttpApiConfig(c =>
            {
                c.FormatOptions.IgnoreNullProperty = true;
            });
        }

        /// <summary>
        /// 测试 - 发送文本
        /// </summary>
        [Fact]
        public async Task Test_SendTextAsync()
        {
            var msg = new TextMessageRequest();

            msg.Content = "hello world";

            OutputRequest(msg);

            var resp = await Provider.SendAsync(AppId, msg);
            OutputResponse(resp);
        }

        /// <summary>
        /// 测试 - 发送Markdown
        /// </summary>
        [Fact]
        public async Task Test_SendMarkdownAsync()
        {
            var builder = new WeChatWorkMarkdownBuilder();
            builder.AppendNormal("实时新增用户反馈")
                .AppendWarningMsg("132例")
                .AppendNormal("，请相关同事注意。")
                .NewLine()
                .AppendReference("类型:")
                .AppendCommentMsg("用户反馈")
                .NewLine()
                .AppendReference("普通用户反馈:")
                .AppendCommentMsg("117例")
                .NewLine()
                .AppendReference("VIP用户反馈:")
                .AppendCommentMsg("15例");

            var msg = new MarkdownMessageRequest();

            msg.Content = builder.ToString();

            OutputRequest(msg);

            var resp = await Provider.SendAsync(AppId, msg);
            OutputResponse(resp);
        }

        /// <summary>
        /// 测试 - 发送图片
        /// </summary>
        [Fact]
        public async Task Test_SendImageAsync()
        {
            var fileUrl = Path.Combine(CurrentDir, "Resources", "T1.png");
            var md5 = GetMD5HashFromFile(fileUrl);

            var bytes = await File.ReadAllBytesAsync(fileUrl);
            var base64 = Convert.ToBase64String(bytes);
            var msg = new ImageMessageRequest();

            msg.Md5 = md5;
            msg.Base64 = base64;

            OutputRequest(msg);

            var resp = await Provider.SendAsync(AppId, msg);
            OutputResponse(resp);
        }

        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns>MD5值</returns>
        // ReSharper disable once InconsistentNaming
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// 测试 - 发送图文
        /// </summary>
        [Fact]
        public async Task Test_SendNewsAsync()
        {
            var msg = new NewsMessageRequest();

            msg.Add("中秋节礼品领取", "今年中秋节公司有豪礼相送", "www.qq.com",
                "http://res.mail.qq.com/node/ww/wwopenmng/images/independent/doc/test_pic_msg1.png");

            OutputRequest(msg);

            var resp = await Provider.SendAsync(AppId, msg);
            OutputResponse(resp);
        }

        /// <summary>
        /// 测试 - 上传文件
        /// </summary>
        [Fact]
        public async Task Test_UploadFileAsync()
        {
            var fileUrl = Path.Combine(CurrentDir, "Resources", "T1.png");
            var resp = await Provider.UploadAsync(AppId, fileUrl);
            OutputResponse(resp);

            var msg = new FileMessageRequest();

            msg.MediaId = resp.MediaId;

            OutputRequest(msg);
            var sendResp = await Provider.SendAsync(AppId, msg);
            OutputResponse(sendResp);
        }

        /// <summary>
        /// 输出请求
        /// </summary>
        protected void OutputRequest<TRequest>(TRequest request) where TRequest : WeChatWorkRobotRequest
        {
            Output.WriteLine("--------------------------- Request ---------------------------");
            var json = JsonConvert.SerializeObject(request.ToRequestBody());
            Output.WriteLine(json);
        }

        /// <summary>
        /// 输出响应
        /// </summary>
        protected void OutputResponse<TResponse>(TResponse response) where TResponse : WeChatWorkRobotResponse
        {
            Output.WriteLine("--------------------------- Response ---------------------------");
            var json = JsonConvert.SerializeObject(response);
            Output.WriteLine(json);
        }
    }
}
