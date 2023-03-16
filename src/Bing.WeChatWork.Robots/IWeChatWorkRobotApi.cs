using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bing.WeChatWork.Robots.Models;
using WebApiClientCore;
using WebApiClientCore.Attributes;
using WebApiClientCore.Parameters;

namespace Bing.WeChatWork.Robots
{
    /// <summary>
    /// 企业微信机器人API
    /// </summary>
    [HttpHost("https://qyapi.weixin.qq.com")]
    [LoggingFilter]
    public interface IWeChatWorkRobotApi : IHttpApi
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="key">企业微信机器人密钥</param>
        /// <param name="request">请求正文</param>
        /// <param name="cancellationToken">取消令牌</param>
        [HttpPost("cgi-bin/webhook/send")]
        Task<WeChatWorkRobotResponse> SendAsync([PathQuery] string key, [JsonContent] IDictionary<string, object> request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="key">企业微信机器人密钥</param>
        /// <param name="file">上传文件</param>
        /// <param name="type">类型</param>
        /// <param name="cancellationToken">取消令牌</param>
        [HttpPost("cgi-bin/webhook/upload_media")]
        Task<WeChatWorkRobotUploadResponse> UploadAsync([PathQuery] string key, FormDataFile file, [PathQuery] string type = Const.File, CancellationToken cancellationToken = default);
    }
}
