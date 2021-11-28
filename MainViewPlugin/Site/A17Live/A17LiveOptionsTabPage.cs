using Mcv.Plugin.MainViewPlugin.Settings;

namespace Mcv.Plugin.MainViewPlugin.Site.A17Live;

class A17LiveOptionsTabPage : IOptionsTabPage
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

    private readonly A17LiveOptionsPanel _panel;
    public A17LiveOptionsTabPage(string displayName, A17LiveOptionsPanel panel)
    {
        HeaderText = displayName;
        _panel = panel;
    }
}