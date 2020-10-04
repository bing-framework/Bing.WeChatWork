using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Bing.WeChatWork.Robots.Models;

namespace Bing.WeChatWork.Robots
{
    /// <summary>
    /// 企业微信机器人提供程序
    /// </summary>
    public interface IWeChatWorkRobotProvider
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="appId">企业微信机器人密钥</param>
        /// <param name="request">请求</param>
        Task<WeChatWorkRobotResponse> SendAsync(string appId, IDictionary<string, object> request);

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="TMessageRequest">消息请求类型</typeparam>
        /// <param name="appId">企业微信机器人密钥</param>
        /// <param name="request">请求</param>
        Task<WeChatWorkRobotResponse> SendAsync<TMessageRequest>(string appId, TMessageRequest request)
            where TMessageRequest : WeChatWorkRobotRequest;

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="appId">企业微信机器人密钥</param>
        /// <param name="file">文件路径</param>
        Task<WeChatWorkRobotUploadResponse> UploadAsync(string appId, string file);
    }
}
