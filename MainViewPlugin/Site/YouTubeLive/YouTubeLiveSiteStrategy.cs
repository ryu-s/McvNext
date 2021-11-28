using Mcv.Plugin.MainViewPlugin.Settings;
using Mcv.YouTubeLive;
using Plugin;
using System.Linq;
using System.Text;

namespace Mcv.Plugin.MainViewPlugin.Site.YouTubeLive;

class YouTubeLiveSiteStrategy : ISiteStrategy
{
    public IOptionsTabPage CreateTabPage()
    {
        return new YouTubeLiveOptionsTabPage("YouTube", new YouTubeLiveOptionsPanel());
    }

    public ISiteSettingsViewModel CreateViewModel(IPluginSettings settings)
    {
        return new YouTubeLiveSiteSettingsViewModel();
    }

    public bool IsTarget(IPluginSettings settings)
    {
        return settings is IYouTubeLiveSettings;
    }
    public bool TryParseMessage(ISiteMessage message, ConnectionNameViewModel connName,IAdapter adapter, out IMcvCommentViewModel? cvm)
    {
        if(message is not IYouTubeLiveMessage)
        {
            cvm = null;
            return false;
        }
        if (message is IYouTubeLiveComment comment)
        {
            cvm = new YouTubeLiveCommentViewModel(comment, connName, adapter);
            return true;
        }
        else if(message is IYouTubeLiveSuperChat superChat)
        {
            cvm = null;
            return false;
        }
        else
        {
            cvm = null;
            return false;
        }
    }
}
