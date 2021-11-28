using Plugin;
namespace Mcv.Plugin.MainViewPlugin
{
    interface ISiteStrategy
    {
        bool IsTarget(IPluginSettings settings);
        ISiteSettingsViewModel CreateViewModel(IPluginSettings settings);
        Settings.IOptionsTabPage CreateTabPage();

        bool TryParseMessage(ISiteMessage message, ConnectionNameViewModel connName,IAdapter adapter, out IMcvCommentViewModel? cvm);
    }
    interface ISiteSettingsViewModel
    {
        void SetSettings(IPluginSettings settings);
        IPluginSettings GetSettings();
        /// <summary>
        /// SetSettings()以降の差分
        /// </summary>
        /// <returns></returns>
        IPluginSettingsDiff GetSettingsDiff();
    }
}
