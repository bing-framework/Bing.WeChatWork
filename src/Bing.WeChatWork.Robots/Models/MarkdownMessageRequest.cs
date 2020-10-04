using System;
using System.Runtime.Serialization;

namespace Bing.WeChatWork.Robots.Models
{
    /// <summary>
    /// Markdown消息请求
    /// </summary>
    [DataContract]
    public class MarkdownMessageRequest : WeChatWorkRobotRequest
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        protected override string MessageType => Const.Markdown;

        /// <summary>
        /// markdown内容，最长不超过4096个字节，必须是utf8编码
        /// </summary>
        [DataMember(Name = "content", IsRequired = true)]
        public string Content { get; set; }

        /// <summary>
        /// 校验
        /// </summary>
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Content))
                throw new ArgumentNullException(nameof(Content), "Markdown内容不能为空");
        }

    }
}
