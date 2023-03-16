using Microsoft.Extensions.DependencyInjection;

namespace Bing.WeChatWork.Robots
{
    /// <summary>
    /// 服务扩展 - 企业微信机器人
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 注册企业微信机器人
        /// </summary>
        /// <param name="services">服务集合</param>
        public static void AddWeChatWorkRobot(this IServiceCollection services)
        {
            services.AddHttpApi<IWeChatWorkRobotApi>().ConfigureHttpApi(c =>
            {
                c.JsonSerializeOptions.IgnoreNullValues = true;
            });
            services.AddScoped<IWeChatWorkRobotProvider, WeChatWorkRobotProvider>();
        }
    }
}
