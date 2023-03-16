using Bing.WeChatWork.Robots.Models;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Bing.WeChatWork.Robots.Tests.Models
{
    /// <summary>
    /// 文件消息测试
    /// </summary>
    public class FileMessageRequestTest : MessageRequestTest
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public FileMessageRequestTest(ILoggerFactory loggerFactory) : base(loggerFactory)
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
