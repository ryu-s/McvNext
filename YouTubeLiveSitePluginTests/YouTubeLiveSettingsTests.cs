using Mcv.SitePlugin.YouTubeLive;
using NUnit.Framework;

namespace YouTubeLiveSitePluginTests
{
    public class YouTubeLiveSettingsTests
    {
        [Test]
        public void SerializeDeserializeTest()
        {
            var settings = new YouTubeLiveSettings()
            {
                 IsAllChat= true,
            };
            var json = settings.Serialize();
            var de = new YouTubeLiveSettings();
            de.Deserialize(json);
            Assert.IsTrue(de.IsAllChat);
        }
    }
}