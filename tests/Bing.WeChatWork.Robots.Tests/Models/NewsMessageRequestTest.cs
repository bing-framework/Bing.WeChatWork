using Bing.WeChatWork.Robots.Models;
using Xunit;
using Xunit.Abstractions;

namespace Bing.WeChatWork.Robots.Tests.Models
{
    public class NewsMessageRequestTest:MessageRequestTest
    {
        public NewsMessageRequestTest(ITestOutputHelper output) : base(output)
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
