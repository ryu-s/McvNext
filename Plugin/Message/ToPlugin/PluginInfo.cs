using System.Collections.Generic;

namespace Plugin.Message.ToPlugin
{
    public class PluginInfo
    {
        public PluginInfo(PluginId id, string name, List<IPluginType> typeList)
        {
            Id = id;
            Name = name;
            TypeList = typeList;
        }

        public PluginId Id { get; }
        public string Name { get; }
        public List<IPluginType> TypeList { get; }
    }
}