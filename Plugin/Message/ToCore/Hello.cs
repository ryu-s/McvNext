using System.Collections.Generic;

namespace Plugin.Message.ToCore
{
    public class Hello
    {
        public Hello(PluginId pluginId, List<IPluginType> types)
        {
            PluginId = pluginId;
            PluginTypes = types;
        }

        public PluginId PluginId { get; }
        public List<IPluginType> PluginTypes { get; }
    }
}