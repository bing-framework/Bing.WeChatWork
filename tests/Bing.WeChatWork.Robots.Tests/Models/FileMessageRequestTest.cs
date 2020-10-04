using Bing.WeChatWork.Robots.Models;
using Xunit;
using Xunit.Abstractions;

namespace Bing.WeChatWork.Robots.Tests.Models
{
    public class FileMessageRequestTest: MessageRequestTest
    {
        public FileMessageRequestTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// 测试 - 转换为Json
        /// </summary>
        [Fact]
        public override void Test_ToJson()
        {
            var msg = new FileMessageRequest();

            msg.MediaId = "3a8asd892asd8asd";

            OutputJson(msg);
        }
    }
}
