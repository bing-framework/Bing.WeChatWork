using Bing.WeChatWork.Robots.Models;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Bing.WeChatWork.Robots.Tests.Models
{
    /// <summary>
    /// 图文消息测试
    /// </summary>
    public class NewsMessageRequestTest : MessageRequestTest
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public NewsMessageRequestTest(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        /// <summary>
        /// 测试 - 转换为Json
        /// </summary>
        [Fact]
        public override void Test_ToJson()
        {
            var msg = new NewsMessageRequest();

            msg.Add("中秋节礼品领取", "今年中秋节公司有豪礼相送", "www.qq.com",
                "http://res.mail.qq.com/node/ww/wwopenmng/images/independent/doc/test_pic_msg1.png");

            OutputJson(msg);
        }
    }
}
