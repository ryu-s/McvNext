namespace Plugin
{
    public class NormalConnection : INormalConnection
    {
        public string Name { get; set; } = default!;
        IInput IConnectionStatus.Input => new StringInput(Input);
        public string Input { get; set; } = default!;
        public bool IsConnected { get; set; }
        public SiteId SelectedSite { get; set; } = default!;
        public BrowserProfileId SelectedBrowser { get; set; } = default!;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="diff">変更要求</param>
        /// <returns>実際に変更があった部分</returns>
        public IConnectionStatusDiff SetDiff(IConnectionStatusDiff diff)
        {
            var ret = new NormalConnectionDiff();
            if (!(diff is NormalConnectionDiff b))
            {
                return ret;
            }
            if (b.Name != null && Name != b.Name)
            {
                ret.Name = Name = b.Name;
            }
            if (b.Input != null && Input != b.Input)
            {
                ret.Input = Input = b.Input;
            }
            if (b.IsConnected != null && IsConnected != b.IsConnected)
            {
                ret.IsConnected = IsConnected = b.IsConnected.Value;
            }
            if (b.SelectedSite != null && SelectedSite != b.SelectedSite)
            {
                ret.SelectedSite = SelectedSite = b.SelectedSite;
            }
            if (b.SelectedBrowser != null && SelectedBrowser != b.SelectedBrowser)
            {
                ret.SelectedBrowser = SelectedBrowser = b.SelectedBrowser;
            }
            return ret;
        }
        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}