using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Bing.WeChatWork.Robots.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xunit;

namespace Bing.WeChatWork.Robots.Tests
{
    /// <summary>
    /// 企业微信机器人 测试
    /// </summary>
    public class WeChatWorkRobotProviderTest
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected ILogger<WeChatWorkRobotProviderTest> Logger { get; }

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

        /// <summary>
        /// 初始化一个<see cref="WeChatWorkRobotProviderTest"/>类型的实例
        /// </summary>
        public WeChatWorkRobotProviderTest(ILogger<WeChatWorkRobotProviderTest> logger, IWeChatWorkRobotProvider provider)
        {
            Provider = provider;
            Logger = logger;
            AppId = "";
            CurrentDir = Environment.CurrentDirectory;
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
                var file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                var retVal = md5.ComputeHash(file);
                file.Close();

                var sb = new StringBuilder();
                for (var i = 0; i < retVal.Length; i++)
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

            if(resp.Success())
            {
                var msg = new FileMessageRequest();

                msg.MediaId = resp.MediaId;

                OutputRequest(msg);
                var sendResp = await Provider.SendAsync(AppId, msg);
                OutputResponse(sendResp);
            }
        }

        /// <summary>
        /// 测试 - 发送模板卡片 - 文本通知类型卡片
        /// </summary>
        [Fact]
        public async Task Test_SendTemplateCard_TextNotice_Async()
        {
            var msg = new TemplateCardMessageRequest<TextNoticeTemplateCard>();
            msg.TemplateCard.Source = new TemplateCard.CardSourceItem
            {
                IconUrl = "https://wework.qpic.cn/wwpic/252813_jOfDHtcISzuodLa_1629280209/0",
                Desc = "企业微信",
            };
            msg.TemplateCard.MainTitle = new TemplateCard.CardTitleItem
            {
                Title = "欢迎使用BING团队", 
                Desc = "您的好友正在邀请您加入企业微信"
            };
            msg.TemplateCard.EmphasisContent = new TemplateCard.CardTitleItem { Title = "100", Desc = "数据含义" };
            msg.TemplateCard.QuoteArea = new TemplateCard.CardQuoteAreaItem
            {
                Type = 1,
                Url = "https://work.weixin.qq.com/?from=openApi",
                Title = "引用文本标题",
                QuoteText = "Jack：企业微信真的很好用~\nBalian：超级好的一款软件！"
            };
            msg.TemplateCard.SubTitleText = "下载企业微信还能抢红包！";
            msg.TemplateCard.HorizontalContentList = new List<TemplateCard.CardContentItem>();
            msg.TemplateCard.HorizontalContentList.Add(new TemplateCard.CardContentItem
            {
                KeyName = "邀请人",
                Value = "糟老头子"
            });
            msg.TemplateCard.HorizontalContentList.Add(new TemplateCard.CardContentItem
            {
                KeyName = "企微官网",
                Value = "点击访问",
                Type = 1,
                Url = "https://work.weixin.qq.com/?from=openApi"
            });
            msg.TemplateCard.JumpList = new List<TemplateCard.CardJumpItem>();
            msg.TemplateCard.JumpList.Add(new TemplateCard.CardJumpItem
            {
                Type = 1,
                Url = "https://work.weixin.qq.com/?from=openApi",
                Title = "企业微信官网"
            });
            msg.TemplateCard.CardAction = new TemplateCard.CardActionItem
            {
                Type = 1,
                Url = "https://work.weixin.qq.com/?from=openApi"
            };
            OutputRequest(msg);
            var resp = await Provider.SendAsync(AppId, msg);
            OutputResponse(resp);
        }

        /// <summary>
        /// 输出请求
        /// </summary>
        protected void OutputRequest<TRequest>(TRequest request) where TRequest : WeChatWorkRobotRequest
        {
            Logger.LogInformation("--------------------------- Request ---------------------------");
            var json = JsonConvert.SerializeObject(request.ToRequestBody());
            Logger.LogInformation(json);
        }

        /// <summary>
        /// 输出响应
        /// </summary>
        protected void OutputResponse<TResponse>(TResponse response) where TResponse : WeChatWorkRobotResponse
        {
            Logger.LogDebug("--------------------------- Response ---------------------------");
            var json = JsonConvert.SerializeObject(response);
            Logger.LogInformation(json);
        }
    }
}
