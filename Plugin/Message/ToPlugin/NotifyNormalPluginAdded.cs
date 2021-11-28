namespace Plugin.Message.ToPlugin
{
    public class NotifyNormalPluginAdded
    {
        public NotifyNormalPluginAdded(PluginId pluginId, string name)
        {
            PluginId = pluginId;
            Name = name;
        }

        public PluginId PluginId { get; }
        public string Name { get; }
    }
}