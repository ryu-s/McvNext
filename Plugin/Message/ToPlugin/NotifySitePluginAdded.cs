namespace Plugin.Message.ToPlugin
{
    public class NotifySitePluginAdded
    {
        public NotifySitePluginAdded(SiteId siteId, string name)
        {
            PluginId = siteId;
            Name = name;
        }

        public SiteId PluginId { get; }
        public string Name { get; }
    }
}