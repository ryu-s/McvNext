using NUnit.Framework;
using ryu_s.YouTubeLive.Message;

namespace YouTubeLiveSitePluginTests
{
    public class YtInitialDataTests
    {
        [Test]
        public void Test()
        {
            var s = @"{""responseContext"":{""serviceTrackingParams"":[{""service"":""CSI"",""params"":[{""key"":""c"",""value"":""WEB""},{""key"":""cver"",""value"":""2.20210908.06.00""},{""key"":""yt_li"",""value"":""0""},{""key"":""GetLiveChat_rid"",""value"":""0xff21c6ab015b48ae""}]},{""service"":""GFEEDBACK"",""params"":[{""key"":""logged_in"",""value"":""0""},{""key"":""e"",""value"":""2""}]},{""service"":""GUIDED_HELP"",""params"":[{""key"":""logged_in"",""value"":""0""}]},{""service"":""ECATCHER"",""params"":[{""key"":""client.version"",""value"":""2.20210908""},{""key"":""client.name"",""value"":""WEB""}]}],""mainAppWebResponseContext"":{""loggedOut"":true},""webResponseContextExtensionData"":{""hasDecorated"":true}},""contents"":{""messageRenderer"":{""text"":{""runs"":[{""text"":""このライブ ストリームではチャットは無効です。""}]}}}}";
            var data = YtInitialData.Parse(s);
            Assert.IsNull(data.Continuation);
        }
    }
}