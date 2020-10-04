using System.Collections.Generic;
using Bing.WeChatWork.Robots.Models;
using WebApiClient;
using WebApiClient.Attributes;

namespace Bing.WeChatWork.Robots
{
    /// <summary>
    /// 企业微信机器人API
    /// </summary>
    [HttpHost("https://qyapi.weixin.qq.com")]
    [TraceFilter(OutputTarget = OutputTarget.LoggerFactory)]
    public interface IWeChatWorkRobotApi : IHttpApi
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="key">企业微信机器人密钥</param>
        /// <param name="request">请求正文</param>
        [HttpPost("cgi-bin/webhook/send")]
        ITask<WeChatWorkRobotResponse> SendAsync([PathQuery] string key, [JsonContent] IDictionary<string, object> request);
    }
}
