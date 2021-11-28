namespace Plugin.Message.ToCore
{
    public class AnswerPluginSettings
    {
        public IPluginSettings PluginSettings { get; }
        public AnswerPluginSettings(IPluginSettings pluginSettings)
        {
            PluginSettings = pluginSettings;
        }
    }
}
