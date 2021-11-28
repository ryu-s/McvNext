using System;
using System.Collections.Generic;

namespace Plugin.Message.ToPlugin
{
    [Obsolete]
    public class NotifyPlugins
    {
        public List<PluginInfo> Infos { get; }
        public NotifyPlugins(List<PluginInfo> infos)
        {
            Infos = infos;
        }
    }
}