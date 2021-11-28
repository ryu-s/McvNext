using Mcv.SitePlugin.YouTubeLive;
using NUnit.Framework;
using ryu_s.YouTubeLive.Message;
using ryu_s.YouTubeLive.Message.Action;
using System.Collections.Generic;

namespace YouTubeLiveSitePluginTests
{
    public class SiteMessageParseTests
    {
        [Test]
        public void Test()
        {
            var s = @"{""item"":{""liveChatTextMessageRenderer"":{""message"":{""runs"":[{""text"":""abc""}]},""authorName"":{""simpleText"":""name""},""authorPhoto"":{""thumbnails"":[{""url"":""https://yt4.ggpht.com/ytc/AKedOLTicsJrdOe2bVq7fTx5iNAvyEBPNkL8QcspMO0s=s32-c-k-c0x00ffffff-no-rj"",""width"":32,""height"":32},{""url"":""https://yt4.ggpht.com/ytc/AKedOLTicsJrdOe2bVq7fTx5iNAvyEBPNkL8QcspMO0s=s64-c-k-c0x00ffffff-no-rj"",""width"":64,""height"":64}]},""id"":""id"",""timestampUsec"":""1631202054190683"",""authorBadges"":[{""liveChatAuthorBadgeRenderer"":{""icon"":{""iconType"":""MODERATOR""},""tooltip"":""モデレーター""}},{""liveChatAuthorBadgeRenderer"":{""customThumbnail"":{""thumbnails"":[{""url"":""https://example.com/1""},{""url"":""https://example.com/2""}]},""tooltip"":""メンバー（6 か月）""}}],""authorExternalChannelId"":""UC6vWy2N0Ochgx7uIeOPg9nQ""}},""clientId"":""CJSj3v2Y8vICFZYgYAodcEcE0g3""}";
            var text = TextMessage.Parse(s);
            var message = SiteMessageImplFactory.Parse(text);
            if (message is not YouTubeLiveComment comment)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual(new List<IBadge>
            {
                new SvgIconPart("<svg viewBox=\"0 0 16 16\" preserveAspectRatio=\"xMidYMid meet\" focusable=\"false\" class=\"style-scope yt-icon\" style=\"pointer-events: none; display: block; width: 100%; height: 100%;\" xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\"><g class=\"style-scope yt-icon\"><path d=\"M9.64589146,7.05569719 C9.83346524,6.562372 9.93617022,6.02722257 9.93617022,5.46808511 C9.93617022,3.00042984 7.93574038,1 5.46808511,1 C4.90894765,1 4.37379823,1.10270499 3.88047304,1.29027875 L6.95744681,4.36725249 L4.36725255,6.95744681 L1.29027875,3.88047305 C1.10270498,4.37379824 1,4.90894766 1,5.46808511 C1,7.93574038 3.00042984,9.93617022 5.46808511,9.93617022 C6.02722256,9.93617022 6.56237198,9.83346524 7.05569716,9.64589147 L12.4098057,15 L15,12.4098057 L9.64589146,7.05569719 Z\" class=\"style-scope yt-icon\"></path></g></svg>",16,16,"モデレーター"),
                new RemoteIconPart("https://example.com/2",16,16,"メンバー（6 か月）"),
            }, comment.AuthorBadges);
            return;
        }
    }
    public class AuthorBadgeParseTests
    {
        [Test]
        public void CustomBadgeTest()
        {
            var s = @"{""liveChatAuthorBadgeRenderer"":{""customThumbnail"":{""thumbnails"":[{""url"":""https://example.com/1""},{""url"":""https://example.com/2""}]},""tooltip"":""メンバー（6 か月）""}}";
            var badge = AuthorBadgeFactory.Parse(s);
            if (badge is not AuthorBadgeCustomThumb custom)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual(new List<Thumbnail1> { new Thumbnail1("https://example.com/1"), new Thumbnail1("https://example.com/2") }, custom.Thumbnails);
            Assert.AreEqual("メンバー（6 か月）", custom.Tooltip);
        }
        [Test]
        public void IconTest()
        {
            var s = @"{""liveChatAuthorBadgeRenderer"":{""icon"":{""iconType"":""VERIFIED""},""tooltip"":""確認済み""}}";
            var badge = AuthorBadgeFactory.Parse(s);
            if (badge is not AuthorBadgeIcon icon)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("VERIFIED", icon.IconType);
            Assert.AreEqual("確認済み", icon.Tooltip);
        }
    }
}