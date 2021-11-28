using Mcv.A17Live;
using Mcv.Plugin.MainViewPlugin.Settings;
using Mcv.YouTubeLive;
using Plugin;
//using YtMessage = ryu_s.YouTubeLive.Message;

namespace Mcv.Plugin.MainViewPlugin.Site.A17Live;

class A17LiveSiteStrategy : ISiteStrategy
{
    public IOptionsTabPage CreateTabPage()
    {
        return new A17LiveOptionsTabPage("YouTube", new A17LiveOptionsPanel());
    }

    public ISiteSettingsViewModel CreateViewModel(IPluginSettings settings)
    {
        return new A17LiveSiteSettingsViewModel();
    }

    public bool IsTarget(IPluginSettings settings)
    {
        return settings is IYouTubeLiveSettings;
    }
    public bool TryParseMessage(ISiteMessage message, ConnectionNameViewModel connName, IAdapter adapter, out IMcvCommentViewModel? cvm)
    {
        if (message is not IA17LiveMessage)
        {
            cvm = null;
            return false;
        }
        if (message is IA17LiveComment comment)
        {
            cvm = new A17LiveCommentViewModel(comment, connName, adapter);
            return true;
        }
        else
        {
            cvm = null;
            return false;
        }
    }
}
