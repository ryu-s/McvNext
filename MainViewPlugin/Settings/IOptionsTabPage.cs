namespace Mcv.Plugin.MainViewPlugin.Settings;

interface IOptionsTabPage
{
    string HeaderText { get; }
    void Apply();
    void Cancel();
    ISiteSettingsViewModel GetViewModel();
    System.Windows.Controls.UserControl TabPagePanel { get; }
}
