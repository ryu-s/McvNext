using NUnit.Framework;
using ryu_s.YouTubeLive.Message;

namespace YouTubeLiveSitePluginTests
{
    public class GetLiveChatParseTests
    {
        [Test]
        public void Test()
        {
            var s = Tools.GetSampleData("GetLiveChat.txt");
            var getLiveChat = GetLiveChat.Parse(s);

        }
        [Test]
        public void Test2()
        {
            var s = Tools.GetSampleData("GetLiveChat_reloadContinuation.txt");
            var getLiveChat = GetLiveChat.Parse(s);
            Assert.AreEqual(0, getLiveChat.Actions.Count);
        }
        [Test]
        public void NoContinuationTest()
        {
            var getLiveChat = GetLiveChat.Parse("{}");
            Assert.IsNull(getLiveChat.Continuation);
            Assert.AreEqual(0, getLiveChat.Actions.Count);
        }
    }
}