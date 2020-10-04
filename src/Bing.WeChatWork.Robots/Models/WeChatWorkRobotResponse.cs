using System.Runtime.Serialization;

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
        [DataMember(Name = "errcode")]
        public int Code { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [DataMember(Name = "errmsg")]
        public string Message { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success() => Code == 0;
    }
}
