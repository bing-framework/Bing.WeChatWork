using Bing.WeChatWork.Robots.Models;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Bing.WeChatWork.Robots.Tests.Models
{
    /// <summary>
    /// Markdown消息测试
    /// </summary>
    public class MarkdownMessageRequestTest : MessageRequestTest
    {
        public MarkdownMessageRequestTest(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        /// <summary>
        /// 测试 - 转换为Json
        /// </summary>
        [Fact]
        public override void Test_ToJson()
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

            OutputJson(msg);
        }
    }
}
