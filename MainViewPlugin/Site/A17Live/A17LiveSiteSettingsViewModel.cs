using Mcv.A17Live;
using Plugin;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Mcv.Plugin.MainViewPlugin.Site.A17Live;

class A17LiveSiteSettingsViewModel : ViewModelBase, ISiteSettingsViewModel, INotifyPropertyChanged
{
    public Color PaidCommentBackColor { get; set; } = Colors.Red;
    public Color PaidCommentForeColor { get; set; } = Colors.Blue;
    public Color MembershipBackColor { get; set; } = Colors.Yellow;
    public Color MembershipForeColor { get; set; } = Colors.Brown;
    public bool IsAutoSetNickname { get; set; } = true;
    public bool IsAllChat { get; set; } = false;

    public IPluginSettings GetSettings() => GetSettingsInternal();
    private IA17LiveSettings GetSettingsInternal()
    {
        return new McvA17LiveSettings
        {
        };
    }

    public IPluginSettingsDiff GetSettingsDiff()
    {
        if (_ytSettingsOrigin == null) return new McvA17LiveSettingsDiff();
        return IA17LiveSettingsDiff.GetDiff(_ytSettingsOrigin, GetSettingsInternal());
    }

    public void SetSettings(IPluginSettings settings)
    {
        if (settings is not IA17LiveSettings ytSettings)
        {
            throw new ArgumentException();
        }
        _ytSettingsOrigin = ytSettings;
    }
    private IA17LiveSettings? _ytSettingsOrigin;
}

