using Mcv.YouTubeLive;
using Plugin;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Mcv.Plugin.MainViewPlugin.Site.YouTubeLive;

class YouTubeLiveSiteSettingsViewModel : ViewModelBase, ISiteSettingsViewModel, INotifyPropertyChanged
{
    public Color PaidCommentBackColor { get; set; } = Colors.Red;
    public Color PaidCommentForeColor { get; set; } = Colors.Blue;
    public Color MembershipBackColor { get; set; } = Colors.Yellow;
    public Color MembershipForeColor { get; set; } = Colors.Brown;
    public bool IsAutoSetNickname { get; set; } = true;
    public bool IsAllChat { get; set; } = false;

    public IPluginSettings GetSettings() => GetSettingsInternal();
    private IYouTubeLiveSettings GetSettingsInternal()
    {
        return new McvYouTubeLiveSettings
        {
            PaidCommentBackColor = PaidCommentBackColor,
            PaidCommentForeColor = PaidCommentForeColor,
            MembershipBackColor = MembershipBackColor,
            MembershipForeColor = MembershipForeColor,
            IsAutoSetNickname = IsAutoSetNickname,
            IsAllChat = IsAllChat,
        };
    }

    public IPluginSettingsDiff GetSettingsDiff()
    {
        if (_ytSettingsOrigin == null) return new McvA17LiveSettingsDiff();
        return IYouTubeLiveSettingsDiff.GetDiff(_ytSettingsOrigin, GetSettingsInternal());
    }

    public void SetSettings(IPluginSettings settings)
    {
        if (settings is not IYouTubeLiveSettings ytSettings)
        {
            throw new ArgumentException();
        }
        _ytSettingsOrigin = ytSettings;
        IsAllChat = ytSettings.IsAllChat;
    }
    private IYouTubeLiveSettings? _ytSettingsOrigin;
}

