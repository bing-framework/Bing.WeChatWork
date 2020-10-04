using Bing.WeChatWork.Robots.Models;
using Xunit;
using Xunit.Abstractions;

namespace Bing.WeChatWork.Robots.Tests.Models
{
    public class TextMessageRequestTest : MessageRequestTest
    {
        public TextMessageRequestTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// 测试 - 转换为Json
        /// </summary>
        [Fact]
        public override void Test_ToJson()
        {
            var msg = new TextMessageRequest();

            msg.Content = "hello world";

            OutputJson(msg);
        }

        /// <summary>
        /// 测试 - 提醒用户
        /// </summary>
        [Fact]
        public void Test_RemindUser()
        {
            var msg = new TextMessageRequest();

            msg.Content = "广州今日天气：29度，大部分多云，降雨概率：60%";
            msg.Add("wangqing", "13800001111");
            msg.AddPhoneWithAll();
            msg.AddUserWithAll();

            OutputJson(msg);
        }
    }
}
