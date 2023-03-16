using Bing.WeChatWork.Robots.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Bing.WeChatWork.Robots.Tests.Models
{
    /// <summary>
    /// 消息请求测试
    /// </summary>
    public abstract class MessageRequestTest
    {
        /// <summary>
        /// 日志工厂
        /// </summary>
        protected ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// 日志
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        protected MessageRequestTest(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
            Logger = LoggerFactory.CreateLogger(this.GetType());
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
            Logger.LogInformation(json);
        }
    }
}
