using Xunit.Abstractions;

namespace Bing.WeChatWork.Robots.Tests
{
    /// <summary>
    /// ²âÊÔ»ùÀà
    /// </summary>
    public abstract class TestBase
    {
        /// <summary>
        /// Êä³ö
        /// </summary>
        protected ITestOutputHelper Output { get; }

        protected TestBase(ITestOutputHelper output)
        {
            Output = output;
        }

    }
}
