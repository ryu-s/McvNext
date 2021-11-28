namespace Plugin.Message.ToPlugin
{
    public class NotifyBrowserRemoved
    {
        public NotifyBrowserRemoved(BrowserProfileId pluginId)
        {
            PluginId = pluginId;
        }

        public BrowserProfileId PluginId { get; }
    }
}