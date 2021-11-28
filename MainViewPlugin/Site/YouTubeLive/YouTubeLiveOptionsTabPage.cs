using Mcv.Plugin.MainViewPlugin.Settings;

namespace Mcv.Plugin.MainViewPlugin.Site.YouTubeLive;

class YouTubeLiveOptionsTabPage : IOptionsTabPage
{
    public string HeaderText { get; }

    public System.Windows.Controls.UserControl TabPagePanel => _panel;

    public void Apply()
    {
        var optionsVm = _panel.GetViewModel();
        //optionsVm.OriginOptions.Set(optionsVm.ChangedOptions);
    }

    public void Cancel()
    {
    }

    public ISiteSettingsViewModel GetViewModel()
    {
        return _panel.GetViewModel();
    }

    private readonly YouTubeLiveOptionsPanel _panel;
    public YouTubeLiveOptionsTabPage(string displayName, YouTubeLiveOptionsPanel panel)
    {
        HeaderText = displayName;
        _panel = panel;
    }
}