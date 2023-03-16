using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Bing.WeChatWork.Robots.Models
{
    /// <summary>
    /// 企业微信机器人响应
    /// </summary>
    [DataContract]
    public class WeChatWorkRobotResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonPropertyName("errcode")]
        public int Code { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonPropertyName("errmsg")]
        public string Message { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success() => Code == 0;
    }

    /// <summary>
    /// 企业微信机器人上传响应
    /// </summary>
    [DataContract]
    public class WeChatWorkRobotUploadResponse: WeChatWorkRobotResponse
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// 媒体文件上传后获取的唯一标识，3天内有效
        /// </summary>
        [JsonPropertyName("media_id")]
        public string MediaId { get; set; }

        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }
    }
}
