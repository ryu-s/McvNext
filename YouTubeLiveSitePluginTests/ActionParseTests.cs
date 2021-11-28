using Newtonsoft.Json;
using NUnit.Framework;
using ryu_s.YouTubeLive.Message;
using ryu_s.YouTubeLive.Message.Action;
using System.Collections.Generic;

namespace YouTubeLiveSitePluginTests
{
    public class ActionParseTests
    {
        [Test]
        public void ParseInvalidDataTest()
        {
            var s = "abc";
            var err = ActionFactory.Parse(s) as ParseError;
            if(err == null)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("abc", err.Raw);
        }
        [Test]
        public void ParseTextMessageTest()
        {
            var s = @"{""addChatItemAction"":{""item"":{""liveChatTextMessageRenderer"":{""message"":{""runs"":[{""text"":""abc""}]},""authorName"":{""simpleText"":""name""},""authorPhoto"":{""thumbnails"":[{""url"":""https://yt4.ggpht.com/ytc/AKedOLTicsJrdOe2bVq7fTx5iNAvyEBPNkL8QcspMO0s=s32-c-k-c0x00ffffff-no-rj"",""width"":32,""height"":32},{""url"":""https://yt4.ggpht.com/ytc/AKedOLTicsJrdOe2bVq7fTx5iNAvyEBPNkL8QcspMO0s=s64-c-k-c0x00ffffff-no-rj"",""width"":64,""height"":64}]},""id"":""id"",""timestampUsec"":""1631202054190683"",""authorBadges"":[{""liveChatAuthorBadgeRenderer"":{""customThumbnail"":{""thumbnails"":[{""url"":""https://example.com/1""},{""url"":""https://example.com/2""}]},""tooltip"":""メンバー（6 か月）""}}],""authorExternalChannelId"":""UC6vWy2N0Ochgx7uIeOPg9nQ""}},""clientId"":""CJSj3v2Y8vICFZYgYAodcEcE0g3""}}";
            var a = ActionFactory.Parse(s);
            if (a is not TextMessage text)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("name", text.AuthorName);
            Assert.AreEqual("id", text.Id);
            Assert.AreEqual(1631202054190683, text.TimestampUsec);
            Assert.AreEqual(new List<IMessagePart> {
                new TextPart("abc")
            }, text.MessageItems);

        }
    }
}