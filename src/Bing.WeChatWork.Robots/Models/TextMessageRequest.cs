using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Bing.WeChatWork.Robots.Models
{
    /// <summary>
    /// 文本消息请求
    /// </summary>
    [DataContract]
    public class TextMessageRequest : WeChatWorkRobotRequest
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        protected override string MessageType => Const.Text;

        /// <summary>
        /// 文本内容，最长不超过2048个字节，必须是utf8编码
        /// </summary>
        [DataMember(Name = "content", IsRequired = true)]
        public string Content { get; set; }

        /// <summary>
        /// userid的列表，提醒群中的指定成员(@某个成员)，@all表示提醒所有人，如果开发者获取不到userid，可以使用mentioned_mobile_list
        /// </summary>
        [DataMember(Name = "mentioned_list", IsRequired = false, EmitDefaultValue = false)]
        public List<string> Users { get; set; }

        /// <summary>
        /// 手机号列表，提醒手机号对应的群成员(@某个成员)，@all表示提醒所有人
        /// </summary>
        [DataMember(Name = "mentioned_mobile_list", IsRequired = false, EmitDefaultValue = false)]
        public List<string> Phones { get; set; }

        /// <summary>
        /// 校验
        /// </summary>
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Content))
                throw new ArgumentNullException(nameof(Content), "文本内容不能为空");
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mentions">要额外提及这些人。应该使用 企业微信Id 或者 手机号码，而不是姓名；可混合传入，会自动识别</param>
        public TextMessageRequest Add(params string[] mentions)
        {
            var idList = mentions.Where(x => !x.All(l => l >= '0' && l <= '9')).ToList();
            var phoneList = mentions.Where(x => x.All(l => l >= '0' && l <= '9')).ToList();
            foreach (var id in idList)
                AddUser(id);
            foreach (var phone in phoneList)
                AddPhone(phone);
            return this;
        }

        /// <summary>
        /// 添加手机号
        /// </summary>
        /// <param name="phone">手机号</param>
        public TextMessageRequest AddPhone(string phone)
        {
            Phones ??= new List<string>();
            if (Phones.Exists(x => x == phone))
                return this;
            Phones.Add(phone);
            return this;
        }

        /// <summary>
        /// 添加所有手机号码接收
        /// </summary>
        public TextMessageRequest AddPhoneWithAll() => AddPhone(Const.All);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户名</param>
        public TextMessageRequest AddUser(string user)
        {
            if (Users == null)
                Users = new List<string>();
            if (Users.Exists(x => x == user))
                return this;
            Users.Add(user);
            return this;
        }

        /// <summary>
        /// 添加所有用户接收
        /// </summary>
        public TextMessageRequest AddUserWithAll() => AddUser(Const.All);
    }
}
