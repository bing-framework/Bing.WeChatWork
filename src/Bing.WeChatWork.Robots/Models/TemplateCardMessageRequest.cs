using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bing.WeChatWork.Robots.Models;

/// <summary>
/// 模板卡片消息请求
/// </summary>
public class TemplateCardMessageRequest<TCard> : WeChatWorkRobotRequest where TCard : TemplateCard, new()
{
    /// <summary>
    /// 消息类型
    /// </summary>
    protected override string MessageType => Const.TemplateCard;

    /// <summary>
    /// 模板卡片
    /// </summary>
    [JsonPropertyName("template_card")]
    public TCard TemplateCard { get; set; } = new TCard();

    /// <summary>
    /// 获取内容
    /// </summary>
    protected override object GetContent() => TemplateCard;
}

/// <summary>
/// 模板卡片
/// </summary>
public abstract class TemplateCard
{
    /// <summary>
    /// 卡片类型
    /// </summary>
    [JsonPropertyName("card_type")]
    public abstract string CardType { get; }

    /// <summary>
    /// 卡片来源样式信息，不需要来源样式可不填写。
    /// </summary>
    [JsonPropertyName("source")]
    public CardSourceItem Source { get; set; }

    /// <summary>
    /// 模板卡片的主要内容，包括一级标题和标题辅助信息。
    /// </summary>
    [JsonPropertyName("main_title")]
    public CardTitleItem MainTitle { get; set; }

    /// <summary>
    /// 引用文献样式，建议不与关键数据共用
    /// </summary>
    [JsonPropertyName("quote_area")]
    public CardQuoteAreaItem QuoteArea { get; set; }

    /// <summary>
    /// 二级标题+文本列表，该字段可为空数组，但有数据的话需确认对应字段是否必填，列表长度不超过6
    /// </summary>
    [JsonPropertyName("horizontal_content_list")]
    public List<CardContentItem> HorizontalContentList { get; set; }

    /// <summary>
    /// 跳转指引样式的列表，该字段可为空数组，但有数据的话需确认对应字段是否必填，列表长度不超过3
    /// </summary>
    [JsonPropertyName("jump_list")]
    public List<CardJumpItem> JumpList { get; set; }

    /// <summary>
    /// 整体卡片的点击跳转事件，text_notice模版卡片中该字段为必填项
    /// </summary>
    [JsonPropertyName("card_action")]
    public CardActionItem CardAction { get; set; }

    #region 基础组件

    /// <summary>
    /// 卡片来源
    /// </summary>
    public class CardSourceItem
    {
        /// <summary>
        /// 来源图片的URL
        /// </summary>
        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// 来源图片的描述，建议不超过13个字
        /// </summary>
        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        /// <summary>
        /// 来源文字的颜色，目前支持：0（默认）灰色，1 黑色，2 红色，3 绿色
        /// </summary>
        [JsonPropertyName("desc_color")]
        public int DescColor { get; set; } = 0;
    }

    /// <summary>
    /// 卡片标题
    /// </summary>
    public class CardTitleItem
    {
        /// <summary>
        /// 一级标题，建议不超过26个字
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// 标题辅助信息，建议不超过30个字
        /// </summary>
        [JsonPropertyName("desc")]
        public string Desc { get; set; }
    }

    /// <summary>
    /// 卡片内容
    /// </summary>
    public class CardContentItem
    {
        /// <summary>
        /// 链接类型，0或不填代表是普通文本，1 代表跳转url，2 代表下载附件，3 代表@员工
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; } = 0;

        /// <summary>
        /// 二级标题，建议不超过5个字
        /// </summary>
        [JsonPropertyName("keyname")]
        [Required]
        public string KeyName { get; set; }

        /// <summary>
        /// 二级文本，如果horizontal_content_list.type是2，该字段代表文件名称（要包含文件类型），建议不超过26个字
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// 链接跳转的url，horizontal_content_list.type是1时必填
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// 附件的media_id，horizontal_content_list.type是2时必填
        /// </summary>
        [JsonPropertyName("media_id")]
        public string MediaId { get; set; }

        /// <summary>
        /// 被@的成员的userid，horizontal_content_list.type是3时必填
        /// </summary>
        [JsonPropertyName("userid")]
        public string UserId { get; set; }
    }

    /// <summary>
    /// 引用文献样式，建议不与关键数据共用
    /// </summary>
    public class CardQuoteAreaItem
    {
        /// <summary>
        /// 引用文献样式区域点击事件，0或不填代表没有点击事件，1 代表跳转url，2 代表跳转小程序
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; } = 0;

        /// <summary>
        /// 点击跳转的url，quote_area.type是1时必填
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// 点击跳转的小程序的appid，quote_area.type是2时必填
        /// </summary>
        [JsonPropertyName("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 点击跳转的小程序的pagepath，quote_area.type是2时选填
        /// </summary>
        [JsonPropertyName("pagepath")]
        public string PagePath { get; set; }

        /// <summary>
        /// 引用文献样式的标题
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// 引用文献样式的引用文案
        /// </summary>
        [JsonPropertyName("quote_text")]
        public string QuoteText { get; set; }
    }

    /// <summary>
    /// 跳转指引样式的列表，该字段可为空数组，但有数据的话需确认对应字段是否必填，列表长度不超过3
    /// </summary>
    public class CardJumpItem
    {
        /// <summary>
        /// 跳转链接类型，0或不填代表不是链接，1 代表跳转url，2 代表跳转小程序
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; } = 0;

        /// <summary>
        /// 跳转链接样式的文案内容，建议不超过13个字
        /// </summary>
        [JsonPropertyName("title")]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 跳转链接的url，jump_list.type是1时必填
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// 跳转链接的小程序的appid，jump_list.type是2时必填
        /// </summary>
        [JsonPropertyName("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 跳转链接的小程序的pagepath，jump_list.type是2时选填
        /// </summary>
        [JsonPropertyName("pagepath")]
        public string PagePath { get; set; }
    }

    /// <summary>
    /// 整体卡片的点击跳转事件，text_notice模版卡片中该字段为必填项
    /// </summary>
    public class CardActionItem
    {
        /// <summary>
        /// 卡片跳转类型，1 代表跳转url，2 代表打开小程序。text_notice模版卡片中该字段取值范围为[1,2]
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; } = 0;

        /// <summary>
        /// 跳转事件的url，card_action.type是1时必填
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// 跳转事件的小程序的appid，card_action.type是2时必填
        /// </summary>
        [JsonPropertyName("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 跳转事件的小程序的pagepath，card_action.type是2时选填
        /// </summary>
        [JsonPropertyName("pagepath")]
        public string PagePath { get; set; }
    }

    #endregion

    #region 自定义组件

    /// <summary>
    /// 卡片图片
    /// </summary>
    public class CardImageItem
    {
        /// <summary>
        /// 图片的url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// 图片的宽高比，宽高比要小于2.25，大于1.3，不填该参数默认1.3
        /// </summary>
        [JsonPropertyName("aspect_ratio")]
        public double AspectRatio { get; set; } = 1.3;
    }

    /// <summary>
    /// 卡片图文
    /// </summary>
    public class CardNewsItem
    {
        /// <summary>
        /// 左图右文样式区域点击事件，0或不填代表没有点击事件，1 代表跳转url，2 代表跳转小程序
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; } = 0;

        /// <summary>
        /// 点击跳转的url，image_text_area.type是1时必填
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// 点击跳转的小程序的appid，必须是与当前应用关联的小程序，image_text_area.type是2时必填
        /// </summary>
        [JsonPropertyName("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 点击跳转的小程序的pagepath，image_text_area.type是2时选填
        /// </summary>
        [JsonPropertyName("pagepath")]
        public string PagePath { get; set; }

        /// <summary>
        /// 左图右文样式的标题
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// 左图右文样式的描述
        /// </summary>
        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        /// <summary>
        /// 左图右文样式的图片url
        /// </summary>
        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }
    }

    #endregion

}

/// <summary>
/// 文本通知模板卡片
/// </summary>
public class TextNoticeTemplateCard : TemplateCard
{
    /// <summary>
    /// 卡片类型
    /// </summary>
    [JsonPropertyName("card_type")]
    public override string CardType => Const.TextNotice;

    /// <summary>
    /// 关键数据样式
    /// </summary>
    [JsonPropertyName("emphasis_content")]
    public CardTitleItem EmphasisContent { get; set; }

    /// <summary>
    /// 二级普通文本，建议不超过112个字。模版卡片主要内容的一级标题main_title.title和二级普通文本sub_title_text必须有一项填写
    /// </summary>
    [JsonPropertyName("sub_title_text")]
    public string SubTitleText { get; set; }
}

/// <summary>
/// 图文展示模板卡片
/// </summary>
public class NewsNoticeTemplateCard : TemplateCard
{
    /// <summary>
    /// 卡片类型
    /// </summary>
    [JsonPropertyName("card_type")]
    public override string CardType => Const.NewsNotice;

    /// <summary>
    /// 图片样式
    /// </summary>
    [JsonPropertyName("card_image")]
    [Required]
    public CardImageItem CardImage { get; set; }

    /// <summary>
    /// 左图右文样式
    /// </summary>
    [JsonPropertyName("image_text_area")]
    public CardNewsItem ImageTextArea { get; set; }

    /// <summary>
    /// 卡片二级垂直内容，该字段可为空数组，但有数据的话需确认对应字段是否必填，列表长度不超过4
    /// </summary>
    [JsonPropertyName("vertical_content_list")]
    public List<CardTitleItem> VerticalContentList { get; set; }
}
