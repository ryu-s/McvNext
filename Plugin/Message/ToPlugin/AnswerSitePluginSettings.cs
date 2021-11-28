using System.Collections.Generic;

namespace Plugin.Message.ToPlugin
{
    public class AnswerSitePluginSettings
    {
        public List<IPluginSettings> PluginSettingsList { get; }
        public AnswerSitePluginSettings(List<IPluginSettings> pluginSettingsList)
        {
            PluginSettingsList = pluginSettingsList;
        }
    }
}