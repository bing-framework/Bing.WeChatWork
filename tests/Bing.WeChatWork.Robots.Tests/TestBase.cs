using Xunit.Abstractions;

namespace Bing.WeChatWork.Robots.Tests
{
    /// <summary>
    /// ���Ի���
    /// </summary>
    public abstract class TestBase
    {
        /// <summary>
        /// ���
        /// </summary>
        protected ITestOutputHelper Output { get; }

        protected TestBase(ITestOutputHelper output)
        {
            Output = output;
        }

    }
}
