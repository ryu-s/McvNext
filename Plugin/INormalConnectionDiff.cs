namespace Plugin
{

    public interface INormalConnectionDiff : IConnectionStatusDiff
    {
        new string? Input { get; set; }
        bool? IsConnected { get; set; }
        SiteId? SelectedSite { get; set; }
        BrowserProfileId? SelectedBrowser { get; set; }
    }
}
