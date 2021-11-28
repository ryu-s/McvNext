using System;

namespace Plugin
{
    public interface IPluginType { }
    public class PluginTypeSite : IPluginType
    {
        public PluginTypeSite(SiteId siteId, string siteName)
        {
            SiteId = siteId;
            SiteName = siteName;
        }

        public SiteId SiteId { get; }
        public string SiteName { get; }

        /// <summary>
        /// 投稿データの形式
        /// </summary>
        Type? PostingDataType { get; set; }
    }
    public class PluginTypeConnectionManager : IPluginType
    {

    }
    public class PluginTypeMainView : IPluginType
    {

    }
    public class PluginTypeNormal : IPluginType
    {

    }
    public class PluginTypeCookieStore : IPluginType
    {
        public PluginTypeCookieStore(BrowserProfileId browserProfileId, string browserName, string? profileName)
        {
            BrowserProfileId = browserProfileId;
            BrowserName = browserName;
            ProfileName = profileName;
        }

        public BrowserProfileId BrowserProfileId { get; }
        public string BrowserName { get; }
        public string? ProfileName { get; }
    }
}