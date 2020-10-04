using Bing.WeChatWork.Robots.Models;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Bing.WeChatWork.Robots.Tests.Models
{
    /// <summary>
    /// 消息请求测试
    /// </summary>
    public abstract class MessageRequestTest : TestBase
    {
        protected MessageRequestTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// 测试 - 转换为Json
        /// </summary>
        public abstract void Test_ToJson();

        /// <summary>
        /// 输出Json
        /// </summary>
        protected void OutputJson<TMessageRequest>(TMessageRequest msg) where TMessageRequest : WeChatWorkRobotRequest
        {
            var request = msg.ToRequestBody();
            var json = JsonConvert.SerializeObject(request);
            Output.WriteLine(json);
        }
    }
}
