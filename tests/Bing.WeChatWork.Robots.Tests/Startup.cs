﻿using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace Bing.WeChatWork.Robots.Tests
{
    public class Startup
    {
        /// <summary>
        /// 配置主机
        /// </summary>
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((context, builder) =>
            {
            });
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // 注入日志
            services.AddWeChatWorkRobot();
        }

        /// <summary>
        /// 配置日志提供程序
        /// </summary>
        public void Configure(ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor)
        {
            // 添加单元测试日志提供程序，并配置日志过滤
            loggerFactory.AddProvider(new XunitTestOutputLoggerProvider(accessor, (s, logLevel) => logLevel >= LogLevel.Trace));

            var listener = new ActivityListener();
            listener.ShouldListenTo += _ => true;
            listener.Sample += delegate { return ActivitySamplingResult.AllDataAndRecorded; };

            ActivitySource.AddActivityListener(listener);
        }
    }
}
