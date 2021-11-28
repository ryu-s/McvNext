using NUnit.Framework;
using ryu_s.YouTubeLive.Message;

namespace YouTubeLiveSitePluginTests
{
    public class LiveChatParseTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var s = Tools.GetSampleData("LiveChat.txt");
            var liveChat=LiveChat.Parse(s);
        }
    }
}