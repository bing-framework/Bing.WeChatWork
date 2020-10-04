using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bing.WeChatWork.Robots.Models
{
    /// <summary>
    /// 企业微信机器人请求
    /// </summary>
    [DataContract]
    public abstract class WeChatWorkRobotRequest
    {
        /// <summary>
        /// 参数字典
        /// </summary>
        protected readonly IDictionary<string, object> Params;

        /// <summary>
        /// 消息类型
        /// </summary>
        protected abstract string MessageType { get; }

        /// <summary>
        /// 初始化一个<see cref="WeChatWorkRobotRequest"/>类型的实例
        /// </summary>
        protected WeChatWorkRobotRequest() => Params = new Dictionary<string, object>();

        /// <summary>
        /// 预处理
        /// </summary>
        protected virtual void PreHandle()
        {
            Params[Const.MsgType] = MessageType;
            Params[MessageType] = GetContent();
        }

        /// <summary>
        /// 获取内容
        /// </summary>
        protected virtual object GetContent() => this;

        /// <summary>
        /// 校验
        /// </summary>
        protected virtual void Validate() { }

        /// <summary>
        /// 转换为请求正文
        /// </summary>
        public virtual IDictionary<string, object> ToRequestBody()
        {
            Validate();
            PreHandle();
            return Params;
        }
    }
}
