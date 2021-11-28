using System.Collections.Generic;

namespace Plugin.Message.ToPlugin
{
    public class AnswerPlugins
    {
        public AnswerPlugins(List<PluginInfo> plugins)
        {
            Plugins = plugins;
        }

        public List<PluginInfo> Plugins { get; }
    }
}