using System.Collections.Generic;

namespace Plugin.Message.ToCore
{
    public class RequestUpdateSettingsList
    {
        public RequestUpdateSettingsList(List<IPluginSettingsDiff> modifiedSettingsList)
        {
            ModifiedSettingsList = modifiedSettingsList;
        }

        public List<IPluginSettingsDiff> ModifiedSettingsList { get; }
    }
}
