using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bing.WeChatWork.Robots.Models;
using WebApiClient.Parameterables;

namespace Bing.WeChatWork.Robots
{
    /// <summary>
    /// 企业微信机器人提供程序
    /// </summary>
    public class WeChatWorkRobotProvider : IWeChatWorkRobotProvider
    {
        /// <summary>
        /// 企业微信机器人API
        /// </summary>
        private readonly IWeChatWorkRobotApi _api;

        /// <summary>
        /// 初始化一个<see cref="WeChatWorkRobotProvider"/>类型的实例
        /// </summary>
        /// <param name="api">企业微信机器人API</param>
        public WeChatWorkRobotProvider(IWeChatWorkRobotApi api) => _api = api ?? throw new ArgumentNullException(nameof(api));

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="appId">企业微信机器人密钥</param>
        /// <param name="request">请求</param>
        public async Task<WeChatWorkRobotResponse> SendAsync(string appId, IDictionary<string, object> request) => await _api.SendAsync(appId, request);

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="TMessageRequest">消息请求类型</typeparam>
        /// <param name="appId">企业微信机器人密钥</param>
        /// <param name="request">请求</param>
        public async Task<WeChatWorkRobotResponse> SendAsync<TMessageRequest>(string appId, TMessageRequest request) where TMessageRequest : WeChatWorkRobotRequest => await _api.SendAsync(appId, request.ToRequestBody());

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="appId">企业微信机器人密钥</param>
        /// <param name="file">文件路径</param>
        public async Task<WeChatWorkRobotUploadResponse> UploadAsync(string appId, string file) =>
            await _api.UploadAsync(appId, new MulitpartFile(file));
    }
}
