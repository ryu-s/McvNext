namespace Plugin
{
    public interface INormalConnection : IConnectionStatus
    {
        new string Input { get; }
        bool IsConnected { get; }
        SiteId SelectedSite { get; }
        BrowserProfileId SelectedBrowser { get; }
    }
}
