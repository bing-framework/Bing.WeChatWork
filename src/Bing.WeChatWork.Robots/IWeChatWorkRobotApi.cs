using System.Collections.Generic;
using Bing.WeChatWork.Robots.Models;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Parameterables;

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

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="key">企业微信机器人密钥</param>
        /// <param name="file">上传文件</param>
        /// <param name="type">类型</param>
        [HttpPost("cgi-bin/webhook/upload_media")]
        ITask<WeChatWorkRobotUploadResponse> UploadAsync([PathQuery] string key, MulitpartFile file, [PathQuery] string type = Const.File);
    }
}
