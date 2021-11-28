namespace Plugin
{
    public class NormalConnectionDiff : INormalConnectionDiff
    {
        public string? Input { get; set; }
        public bool? IsConnected { get; set; }
        public string? Name { get; set; }
        public SiteId? SelectedSite { get; set; }
        public BrowserProfileId? SelectedBrowser { get; set; }
        IInput? IConnectionStatusDiff.Input
        {
            get
            {
                return Input != null ? new StringInput(Input) : null;
            }
            set
            {

            }
        }
        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
