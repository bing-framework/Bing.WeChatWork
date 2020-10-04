using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Bing.WeChatWork.Robots.Models
{
    /// <summary>
    /// 图文消息请求
    /// </summary>
    [DataContract]
    public class NewsMessageRequest : WeChatWorkRobotRequest
    {

        /// <summary>
        /// 消息类型
        /// </summary>
        protected override string MessageType => Const.News;

        /// <summary>
        /// 文章列表
        /// </summary>
        public List<NewsArticle> Articles { get; set; } = new List<NewsArticle>();

        /// <summary>
        /// 获取内容
        /// </summary>
        protected override object GetContent() => new
        {
            articles = Articles
        };

        /// <summary>
        /// 校验
        /// </summary>
        protected override void Validate()
        {
            if (!Articles.Any())
                throw new ArgumentNullException(nameof(Articles), "图文消息不能为空");
            if (Articles.Count < 1 || Articles.Count > 8)
                throw new ArgumentOutOfRangeException(nameof(Articles), "图文消息仅支持1到8条图文");
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="url">链接</param>
        /// <param name="picUrl">图片链接</param>
        public NewsMessageRequest Add(string title, string description, string url, string picUrl)
        {
            var article = new NewsArticle { Title = title, Description = description, Url = url, PicUrl = picUrl };
            Articles.Add(article);
            return this;
        }
    }

    /// <summary>
    /// 图文 - 文章
    /// </summary>
    [DataContract]
    public class NewsArticle
    {
        /// <summary>
        /// 标题，不超过128个字节，超过会自动截断
        /// </summary>
        [DataMember(Name = "title", IsRequired = true)]
        public string Title { get; set; }

        /// <summary>
        /// 描述，不超过512个字节，超过会自动截断
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// 点击后跳转的链接
        /// </summary>
        [DataMember(Name = "url", IsRequired = true)]
        public string Url { get; set; }

        /// <summary>
        /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图 1068*455，小图150*150。
        /// </summary>
        [DataMember(Name = "picurl")]
        public string PicUrl { get; set; }

        /// <summary>
        /// 初始化一个<see cref="NewsArticle"/>类型的实例
        /// </summary>
        public NewsArticle() { }

        /// <summary>
        /// 初始化一个<see cref="NewsArticle"/>类型的实例
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="url">链接</param>
        /// <param name="picUrl">图片链接</param>
        public NewsArticle(string title, string description, string url, string picUrl)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Description = description;
            PicUrl = picUrl;
        }
    }
}
