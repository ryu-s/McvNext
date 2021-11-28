namespace Plugin.Message.ToPlugin
{
    public class RequestUpdateSettings
    {
        public RequestUpdateSettings(IPluginSettingsDiff modified)
        {
            Modified = modified;
        }

        public IPluginSettingsDiff Modified { get; }
    }
}