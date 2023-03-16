using Bing.WeChatWork.Robots.Models;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Bing.WeChatWork.Robots.Tests.Models
{
    /// <summary>
    /// 图片消息测试
    /// </summary>
    public class ImageMessageRequestTest : MessageRequestTest
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public ImageMessageRequestTest(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        /// <summary>
        /// 测试 - 转换为Json
        /// </summary>
        [Fact]
        public override void Test_ToJson()
        {
            var msg = new ImageMessageRequest();

            msg.Md5 = "MD5";
            msg.Base64 = "DATA";

            OutputJson(msg);
        }
    }
}
