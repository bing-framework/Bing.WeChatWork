using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Bing.WeChatWork.Robots.Models
{
    /// <summary>
    /// 图片消息请求
    /// </summary>
    [DataContract]
    public class ImageMessageRequest : WeChatWorkRobotRequest
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        protected override string MessageType => Const.Image;

        /// <summary>
        /// 图片内容的base64编码
        /// </summary>
        [DataMember(Name = "base64", IsRequired = true)]
        [JsonPropertyName("base64")]
        public string Base64 { get; set; }

        /// <summary>
        /// 图片内容（base64编码前）的md5值
        /// </summary>
        [DataMember(Name = "md5", IsRequired = true)]
        [JsonPropertyName("md5")]
        public string Md5 { get; set; }

        /// <summary>
        /// 校验
        /// </summary>
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Base64))
                throw new ArgumentNullException(nameof(Base64), "图片内容不能为空");
            if (string.IsNullOrWhiteSpace(Md5))
                throw new ArgumentNullException(nameof(Md5), "图片MD5值不能为空");
        }
    }
}
