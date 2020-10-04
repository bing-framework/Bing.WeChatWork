using Bing.WeChatWork.Robots.Models;
using Xunit;
using Xunit.Abstractions;

namespace Bing.WeChatWork.Robots.Tests.Models
{
    public class ImageMessageRequestTest:MessageRequestTest
    {
        public ImageMessageRequestTest(ITestOutputHelper output) : base(output)
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
