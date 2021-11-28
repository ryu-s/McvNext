namespace Plugin.Message.ToPlugin
{
    public class NotifySitePluginRemoved
    {
        public NotifySitePluginRemoved(SiteId pluginId)
        {
            PluginId = pluginId;
        }

        public SiteId PluginId { get; }
    }
}